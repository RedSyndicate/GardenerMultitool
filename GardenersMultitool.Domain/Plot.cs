using GardenersMultitool.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenersMultitool.Domain
{
    public class Plot
    {
        public PlotNeeds Needs { get; set; }
        public List<Plant> Plants { get; } = new List<Plant>();

        public Plot() { }

        public Plot(PlotNeeds needs)
        {
            Needs = needs;
        }

        public Plant AddPlant(Plant plant)
        {
            Needs &= ~plant.Functions;
            Plants.Add(plant);
            return plant;
        }
    }

    public enum PlotTemplate
    {
        SoilFixer
    }

    public static class PlotTemplates
    {
        public static Dictionary<PlotTemplate, Plot> Templates { get; }

        static PlotTemplates()
        {
            Templates = new Dictionary<PlotTemplate, Plot>
            {
                { PlotTemplate.SoilFixer, new Plot(PlotNeeds.NitrogenFixer) }
            };
        }
    }

    [Flags]
    public enum PlotNeeds
    {
        None = 0,
        NitrogenFixer = 1,
        Overstory = 2,
        Shrub = 4,
        groundCover = 8
    }

}
