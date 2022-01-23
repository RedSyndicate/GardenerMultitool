import { writable } from 'svelte/store';
import { LocationClient, CreateNewLocation } from '$lib/api/client';
import settings from '../settings.json';

export const locations = writable([]);

var _apiClient = new LocationClient(settings.ApiUrl);

export const getLocations = async () => {
	locations.set(await _apiClient.locationGet());
};

export const getLocation = async (id: string) => {
	return await _apiClient.by_id(id);
};

export const postLocation = async (location: CreateNewLocation) => {
	return await _apiClient.locationPost(location);
};
