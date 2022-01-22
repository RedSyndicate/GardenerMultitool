import { writable } from 'svelte/store';
import { Client } from '$lib/client';
import settings from '../settings.json';

export const locations = writable([]);

const dev = 'http://localhost:5000';
const prod = 'http://gardenermultitool.westus2.azurecontainer.io';

export const fetchLocations = async () => {
	var _apiClient = new Client(dev);

	locations.set(await _apiClient.locationGet());
};
