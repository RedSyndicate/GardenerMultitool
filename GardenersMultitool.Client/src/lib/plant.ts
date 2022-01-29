import { writable } from 'svelte/store';
import { PlantClient } from '$lib/api/client';
import settings from '../settings.json';

export const annuals = writable([]);

var _apiClient = new PlantClient(settings.ApiUrl);

export const fetchAnnuals = async () => {
	annuals.set(await _apiClient.plantGet('annual'));
};
