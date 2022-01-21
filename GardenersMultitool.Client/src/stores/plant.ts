import { writable } from 'svelte/store';
import { Client } from '$lib/client';
import settings from '../settings.json';

export const annuals = writable([]);

const dev = 'https://localhost:44363';
const prod = 'https://gardenermultitool.westus2.azurecontainer.io';

const fetchAnnuals = async () => {
	var _apiClient = new Client(dev);

	annuals.set(await _apiClient.plantAnnuals());
};
