import { writable } from 'svelte/store';
import { Client, Plant } from '$lib/api/client';
import settings from '../settings.json';

export const plants = writable(Array<Plant>());

var _apiClient = new Client(settings.ApiUrl);

export const getPlantsAll = async () => {
	plants.set(await _apiClient.plantsAll());
};

export const getPlantsByFilter = async (total: number, page: number, perPage: number) =>
	plants.set(await _apiClient.plantsByFilter(total, page, perPage));

export const getPlantByPlantType = async (
	plantType: string,
	total: number,
	page: number,
	perPage: number
) => plants.set(await _apiClient.plantsByPlantType(plantType, total, page, perPage));
