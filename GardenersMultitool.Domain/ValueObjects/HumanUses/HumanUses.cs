using System;
using System.Collections.Generic;
using System.Linq;

namespace GardenersMultitool.Domain.ValueObjects.HumanUses
{
    public static class HumanUses
    {
        private static readonly Dictionary<HumanUse, Func<string, IHumanUse>> FactoryFunctions;
        private static readonly Dictionary<string, HumanUse> EnumMap;

        static HumanUses()
        {
            HumanUse KeySelector(Type type) =>
                Enum.Parse<HumanUse>(type.Name);

            Func<string, IHumanUse> ElementSelector(Type factoryType)
                => function => factoryType.GetMethod("Create").Invoke(null, new object[]{function}) as IHumanUse;

            try
            {
                var factoryFunctionTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => !type.IsInterface && !type.IsAbstract)
                    .Where(type => type.IsClass)
                    .Where(type => type.IsAssignableTo(typeof(IHumanUseFactory)));
                    FactoryFunctions = factoryFunctionTypes.ToDictionary(KeySelector, ElementSelector);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            EnumMap = Enum.GetValues<HumanUse>()
                .Aggregate(new Dictionary<string, HumanUse>(), AggregateToFunctionsMap);
        }

        private static Dictionary<string, HumanUse> AggregateToFunctionsMap(Dictionary<string, HumanUse> accumulator, HumanUse humanUse) =>
            humanUse.GetAttribute<HumanUseAttribute>()
                .HumanUses
                .Aggregate(accumulator, (humanUses, str) =>
                {
                    humanUses.Add(str, humanUse);
                    return humanUses;
                });
        

        public static IHumanUse Create(string humanUse)  
        {
            Console.WriteLine($"Creating Human Use Object: {humanUse}.");
            var loweredStr = humanUse.ToLowerInvariant();

            if (EnumMap.TryGetValue(loweredStr, out var humanUseEnum))
                return FactoryFunctions[humanUseEnum](loweredStr);

            Console.WriteLine($"Could not find key: {loweredStr}");
            throw new ArgumentException(humanUse);
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class HumanUseAttribute : Attribute
        {
            public readonly List<string> HumanUses = new();

            public HumanUseAttribute(params string[] functions)
            {
                HumanUses.AddRange(functions);
            }
        }

        private static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public enum HumanUse
        {
            [HumanUse("aromatics/fragrance")]
            AromaticsFragrance,
            [HumanUse("biomass")]
            Biomass,
            [HumanUse("cleanser/scourer")]
            CleanserScourer,
            [HumanUse("compost")]
            Compost,
            [HumanUse("container garden")]
            ContainerGarden,
            [HumanUse("coppice")]
            Coppice,
            [HumanUse("cut flower")]
            CutFlower,
            [HumanUse("dried flower")]
            DriedFlower,
            [HumanUse("dye")]
            Dye,
            [HumanUse("essential oil")]
            EssentialOil,
            [HumanUse("fiber")]
            Fiber,
            [HumanUse("food")]
            Food,
            [HumanUse("hanging basket")]
            HangingBasket,
            [HumanUse("insecticide")]
            InsecticideSpray,
            [HumanUse("insect repellent")]
            InsectRepellent,
            [HumanUse("medicine")]
            Medicine,
            [HumanUse("oil")]
            OilWaxResinPolish,
            [HumanUse("ornamental")]
            Ornamental,
            [HumanUse("pollard")]
            Pollard,
            [HumanUse("soap")]
            Soap,
            [HumanUse("wood")]
            Wood,
        }
    }
}
