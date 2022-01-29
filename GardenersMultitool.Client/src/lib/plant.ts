import { writable } from 'svelte/store';
import { Client, Plant } from '$lib/api/client';
import settings from '../settings.json';

export const plants = writable(Array<Plant>());

var _apiClient = new Client(settings.ApiUrl);

export const getAllPlants = async (total: number, page: number, perPage: number) =>
	plants.set(await _apiClient.plantsAll(total, page, perPage));

export const getPlantByPlantType = async (
	plantType: string,
	total: number,
	page: number,
	perPage: number
) => plants.set(await _apiClient.plantsByPlantType(plantType, total, page, perPage));
