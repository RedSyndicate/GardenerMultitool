using System;
using System.Collections.Generic;
using System.Linq;

namespace GardenersMultitool.Domain.ValueObjects.EcologicalFunctions
{
    public static class EcologicalFunctions
    {
        private static readonly Dictionary<EcologicalFunction, Func<string, IEcologicalFunction>> _factoryFunctions;
        private static readonly Dictionary<string, EcologicalFunction> _enumMap;

        static EcologicalFunctions()
        {
            EcologicalFunction KeySelector(Type type) =>
                Enum.Parse<EcologicalFunction>(type.Name);

            Func<string, IEcologicalFunction> ElementSelector(Type factoryType)
                => function => factoryType.GetMethod("Create").Invoke(null, new object?[]{function}) as IEcologicalFunction;

            try
            {
                _factoryFunctions = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface && !type.IsAbstract)
                    .Where(type => type.IsClass)
                    .Where(type => type.IsAssignableTo(typeof(IEcologicalFunctionFactory)))
                    .ToDictionary(KeySelector, ElementSelector);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            _enumMap = Enum.GetValues<EcologicalFunction>()
                .Aggregate(new Dictionary<string, EcologicalFunction>(), AggregateToFunctionsMap);
        }

        private static Dictionary<string, EcologicalFunction> AggregateToFunctionsMap(
            Dictionary<string, EcologicalFunction> accumulator, 
            EcologicalFunction ecoFunction) => 
            ecoFunction.GetAttribute<EcologicalFunctionAttribute>()
                .EcologicalFunctions
                .Aggregate(accumulator, (ecologicalFunctions, str) =>
                {
                    ecologicalFunctions.Add(str, ecoFunction);
                    return ecologicalFunctions;
                });

        public static IEcologicalFunction Create(string function)
        {
            Console.WriteLine($"Creating Ecological Function Object: {function}.");
            var loweredStr = function.ToLowerInvariant();

            if (_enumMap.TryGetValue(loweredStr, out var ecologicalFunction))
                return _factoryFunctions[ecologicalFunction](loweredStr);

            Console.WriteLine($"Could not find key: {loweredStr}");
            throw new ArgumentException(function);
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class EcologicalFunctionAttribute : Attribute
        {
            public readonly List<string> EcologicalFunctions = new();

            public EcologicalFunctionAttribute(params string[] functions)
            {
                EcologicalFunctions.AddRange(functions);
            }
        }

        private static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public enum EcologicalFunction
        {
            [EcologicalFunction("domestic animal forage")]
            AnimalForage,
            [EcologicalFunction("chemical barrier", "hedge", "windbreak")]
            Barriers,
            [EcologicalFunction("fungicide", "herbicide")]
            ChemicalDeterrents,
            [EcologicalFunction("erosion control")]
            ErosionControl,
            [EcologicalFunction("groundcover")]
            GroundCover,
            [EcologicalFunction("nurse")]
            Nurse,
            [EcologicalFunction("insecticide", "aromatic pest confuser")]
            PestManagements,
            [EcologicalFunction(
                "air cleaner", 
                "reclamator", 
                "toxin absorber", 
                "water purifier")]
            Restorers,
            [EcologicalFunction(
                "dynamic accumulator", 
                "mulch maker", 
                "nitrogen fixer", 
                "nitrogen scavenger", 
                "soil builder",
                "soil cultivator")]
            SoilImprovers,
            [EcologicalFunction("insectory", 
                "wildlife food", 
                "wildlife habitat")]
            Wildlife,
        }
    }
}