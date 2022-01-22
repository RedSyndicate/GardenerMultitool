import { writable } from 'svelte/store';
import { LocationClient, Location, CreateNewLocation } from '$lib/api/client';
import settings from '../settings.json';

export const locations = writable([]);

const dev = 'http://localhost:5000';
const prod = 'http://gardenermultitool.westus2.azurecontainer.io';
var _apiClient = new LocationClient(dev);

export const getLocations = async () => {
	locations.set(await _apiClient.locationGet());
};

export const postLocation = async (location: CreateNewLocation) => {
	return await _apiClient.locationPost(location);
};
