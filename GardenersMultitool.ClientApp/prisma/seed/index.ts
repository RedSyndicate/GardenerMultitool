import { PlantType, PrismaClient, type Plant } from '@prisma/client'
const prisma = new PrismaClient()
import annuals from "../../../lib/Permaculture_Plant_JSON/Annuals.json" assert { type: "json" };
import aquatics from "../../../lib/Permaculture_Plant_JSON/Aquatics.json" assert { type: "json" };
import biennials from "../../../lib/Permaculture_Plant_JSON/Biennials.json" assert { type: "json" };
import deciduousShrubs from "../../../lib/Permaculture_Plant_JSON/DeciduousShrubs.json" assert { type: "json" };
import deciduousTrees from "../../../lib/Permaculture_Plant_JSON/DeciduousTrees.json" assert { type: "json" };
import evergreenShrubs from "../../../lib/Permaculture_Plant_JSON/EvergreenShrubs.json" assert { type: "json" };
import evergreenTrees from "../../../lib/Permaculture_Plant_JSON/EvergreenTrees.json" assert { type: "json" };
import ferns from "../../../lib/Permaculture_Plant_JSON/Ferns.json" assert { type: "json" };
import grasses from "../../../lib/Permaculture_Plant_JSON/Grasses.json" assert { type: "json" };
import mosses from "../../../lib/Permaculture_Plant_JSON/Mosses.json" assert { type: "json" };
import perennials from "../../../lib/Permaculture_Plant_JSON/Perennials.json" assert { type: "json" };
import vines from "../../../lib/Permaculture_Plant_JSON/Vines.json" assert { type: "json" };


async function main() {
    annuals.forEach(async (p) => {
        await createPlant(p);
    })
    aquatics.forEach(async (p) => {
        await createPlant(p);
    })
    biennials.forEach(async (p) => {
        await createPlant(p);
    })
    deciduousShrubs.forEach(async (p) => {
        await createPlant(p);
    })
    deciduousTrees.forEach(async (p) => {
        await createPlant(p);
    })
    evergreenShrubs.forEach(async (p) => {
        await createPlant(p);
    })
    evergreenTrees.forEach(async (p) => {
        await createPlant(p);
    })
    ferns.forEach(async (p) => {
        await createPlant(p);
    })
    grasses.forEach(async (p) => {
        await createPlant(p);
    })
    mosses.forEach(async (p) => {
        await createPlant(p);
    })
    perennials.forEach(async (p) => {
        await createPlant(p);
    })
    vines.forEach(async (p) => {
        await createPlant(p);
    })
}

async function createPlant(p: any) {
    let minhzone = Number.parseInt(p['Hardiness Zone'].split(' -- ')[0])
    let maxhzone = Number.parseInt(p['Hardiness Zone'].split(' -- ')[1])

    if (isNaN(minhzone)) {
        minhzone = 0;
    }
    if (isNaN(maxhzone)) {
        maxhzone = minhzone;
    }

    await prisma.plant.create({
        data: {
            Name: p.Name,
            Binomial: p.Binomial,
            BloomTime: p['Bloom Time'],
            Disease: p.Disease,
            FlowerColor: p['Flower Color'],
            Form: p.Form,
            FruitTime: p['Fruit Time'],
            GrowthRate: p['Growth Rate'],
            Height: p.Height,
            InsectPredation: p['Insect Predation'],
            LightRequired: p.Light,
            MinimumHardinessZone: minhzone,
            MaximumHardinessZone: maxhzone,
            MinimumSoilpH: Number.parseFloat(p['Soil pH'].split(' - ')[0]) ?? 0,
            MaximumSoilpH: Number.parseFloat(p['Soil pH'].split(' - ')[1]) ?? 0,
            Notes: p.Notes,
            PlantId: p.Id,
            PlantType: await getPlantType(p['Plant Type']),
            RootDepth: p['Root Depth'],
            RootType: p['Root Type'],
            ScientificName: p['Scientific name'],
            SeasonalInterest: p['Seasonal Interest'],
            SoilMoisture: p['Soil Moisture'],
            Spread: p.Spread,
            Texture: p.Texture
        }
    })
}

async function getPlantType(type: string) {
    switch (type) {
        case "Annual":
            return PlantType.Annual
        case "Aquatic":
            return PlantType.Aquatic
        case "Biennial":
            return PlantType.Biennial
        case "DeciduousShrub":
            return PlantType.DeciduousShrub
        case "DeciduousTree":
            return PlantType.DeciduousTree
        case "EvergreenShrub":
            return PlantType.EvergreenShrub
        case "EvergreenTree":
            return PlantType.EvergreenTree
        case "Fern":
            return PlantType.Fern
        case "Grass":
            return PlantType.Grass
        case "Mosses":
            return PlantType.Mosses
        case "Perennial":
            return PlantType.Perennial
        case "Vine":
            return PlantType.Vine
        default:
            return PlantType.Annual;
    }
}

main()
    .then(async () => {
        await prisma.$disconnect()
    })
    .catch(async (e) => {
        console.error(e)
        await prisma.$disconnect()
        process.exit(1)
    })