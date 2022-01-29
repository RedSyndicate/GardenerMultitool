using System;
using System.Collections.Generic;
using System.Linq;
using GardenersMultitool.Domain.ValueObjects;
using GardenersMultitool.Domain.ValueObjects.EcologicalFunctions;

namespace DataImporter
{
    public static class EcologicalFunctions
    {
        private static readonly Dictionary<EcologicalFunctionCategory, Func<string, IEcologicalFunction>> _factoryFunctions;
        private static readonly Dictionary<string, EcologicalFunctionCategory> _enumMap;

        static EcologicalFunctions()
        {
            EcologicalFunctionCategory KeySelector(Type type) =>
                Enum.Parse<EcologicalFunctionCategory>(type.Name);

            Func<string, IEcologicalFunction> ElementSelector(Type factoryType)
                => function => factoryType.GetMethod("Create").Invoke(null, new object[]{function}) as IEcologicalFunction;

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

            _enumMap = Enum.GetValues<EcologicalFunctionCategory>()
                .Aggregate(new Dictionary<string, EcologicalFunctionCategory>(), AggregateToFunctionsMap);
        }

        private static Dictionary<string, EcologicalFunctionCategory> AggregateToFunctionsMap(
            Dictionary<string, EcologicalFunctionCategory> accumulator, 
            EcologicalFunctionCategory ecoFunction) => 
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

        
        private static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
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

        public enum EcologicalFunctionCategory
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
