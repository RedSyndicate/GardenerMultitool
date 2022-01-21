import { writable } from 'svelte/store';
import { Client } from '$lib/client';
import settings from '../settings.json';

export const locations = writable([]);

const dev = 'https://localhost:44363';
const prod = 'https://gardenermultitool.westus2.azurecontainer.io';

const fetchLocations = async () => {
	var _apiClient = new Client(dev);

	locations.set(await _apiClient.locationGet());
};

fetchLocations();
