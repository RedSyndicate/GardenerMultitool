import { writable } from 'svelte/store';
import { Client, Plant } from '$lib/api/client';
import settings from '../settings.json';

export const plants = writable([]);

var _apiClient = new Client(settings.ApiUrl);

export const getAllPlants = async (total: number) => plants.set(await _apiClient.plantGet(total));

export const getPlantByPlantType = async (plantType: string) =>
	plants.set(await _apiClient.plantGet(plantType));
