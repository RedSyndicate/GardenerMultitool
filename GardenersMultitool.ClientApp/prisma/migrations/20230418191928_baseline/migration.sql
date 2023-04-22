-- CreateEnum
CREATE TYPE "PlantType" AS ENUM ('Annual', 'Aquatic', 'Biennial', 'DeciduousShrub', 'DeciduousTree', 'EvergreenShrub', 'EvergreenTree', 'Fern', 'Grass', 'Mosses', 'Perennial', 'Vine');

-- CreateTable
CREATE TABLE "Plant" (
    "Id" TEXT NOT NULL,
    "PlantId" INTEGER NOT NULL,
    "Name" TEXT NOT NULL,
    "ScientificName" TEXT NOT NULL,
    "Binomial" TEXT NOT NULL,
    "PlantType" "PlantType" NOT NULL,
    "Height" INTEGER NOT NULL,
    "Spread" INTEGER NOT NULL,
    "RootDepth" INTEGER NOT NULL,
    "SeasonalInterest" TEXT NOT NULL,
    "Notes" TEXT NOT NULL,
    "FlowerColor" TEXT NOT NULL,
    "RootType" TEXT NOT NULL,
    "BloomTime" TEXT NOT NULL,
    "FruitTime" TEXT NOT NULL,
    "Texture" TEXT NOT NULL,
    "Form" TEXT NOT NULL,
    "GrowthRate" TEXT NOT NULL,
    "InsectPredation" TEXT NOT NULL,
    "Disease" TEXT NOT NULL,
    "LightRequired" TEXT NOT NULL,
    "MinimumHardinessZone" INTEGER NOT NULL,
    "MaximumHardinessZone" INTEGER NOT NULL,
    "SoilMoisture" TEXT NOT NULL,
    "MinimumSoilpH" DOUBLE PRECISION NOT NULL,
    "MaximumSoilpH" DOUBLE PRECISION NOT NULL,

    CONSTRAINT "Plant_pkey" PRIMARY KEY ("Id")
);

-- CreateTable
CREATE TABLE "PlantLocation" (
    "Id" TEXT NOT NULL,
    "plantId" TEXT NOT NULL,
    "locationId" TEXT NOT NULL,

    CONSTRAINT "PlantLocation_pkey" PRIMARY KEY ("Id")
);

-- CreateTable
CREATE TABLE "Location" (
    "Id" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "HardinessZone" INTEGER NOT NULL,
    "pH" DOUBLE PRECISION NOT NULL,
    "Area" DECIMAL(65,30) NOT NULL,
    "Compaction" BOOLEAN NOT NULL,

    CONSTRAINT "Location_pkey" PRIMARY KEY ("Id")
);

-- CreateIndex
CREATE UNIQUE INDEX "Plant_PlantId_key" ON "Plant"("PlantId");

-- AddForeignKey
ALTER TABLE "PlantLocation" ADD CONSTRAINT "PlantLocation_plantId_fkey" FOREIGN KEY ("plantId") REFERENCES "Plant"("Id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "PlantLocation" ADD CONSTRAINT "PlantLocation_locationId_fkey" FOREIGN KEY ("locationId") REFERENCES "Location"("Id") ON DELETE RESTRICT ON UPDATE CASCADE;
