import { writable } from 'svelte/store';
import { PlantClient } from '$lib/api/client';
import settings from '../settings.json';

export const annuals = writable([]);

const dev = 'http://localhost:5000';
const prod = 'http://gardenermultitool.westus2.azurecontainer.io';

const fetchAnnuals = async () => {
	var _apiClient = new PlantClient(dev);

	annuals.set(await _apiClient.annuals());
};
