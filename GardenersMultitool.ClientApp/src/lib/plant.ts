import { writable } from 'svelte/store';
import { PlantClient } from '$lib/api/client';
import settings from '../settings.json';

export const annuals = writable([]);

var _apiClient = new PlantClient(settings.ApiUrl);

const fetchAnnuals = async () => {
	annuals.set(await _apiClient.annuals());
};
