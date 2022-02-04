import { writable } from 'svelte/store';
import { Client, Plant } from '$lib/api/client';
import settings from '../../settings.json';

export const plantStore = writable(Array<Plant>());

var _apiClient = new Client(settings.ApiUrl);

export const getPlantsAll = async () => {
	plantStore.set(await _apiClient.plantsAll());
};

export const getPlantByPlantId = async (plantId: number) => {
	plantStore.set(await _apiClient.plantsByPlantId(plantId));
};
export const getPlantsByFilter = async (total: number, page: number, perPage: number) =>
	plantStore.set(await _apiClient.plantsByFilter(total, page, perPage));

export const getPlantByPlantType = async (plantType: string) =>
	plantStore.set(await _apiClient.plantsByPlantType(plantType));
