using System;
using GardenersMultitool.Domain.Helpers;

namespace GardenersMultitool.Api;

public class PlantByFilter
{
    public Guid Id { get; set; }
    public int PlantId { get; set; }
    public Name Name { get; set; }
    public Name ScientificName { get; set; }
}