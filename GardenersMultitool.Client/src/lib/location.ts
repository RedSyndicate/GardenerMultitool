import { writable } from 'svelte/store';
import { Client, CreateNewLocation } from '$lib/api/client';
import settings from '../settings.json';

export const locations = writable([]);

var _apiClient = new Client(settings.ApiUrl);

export const getLocations = async () => {
	locations.set(await _apiClient.locationsAll());
};

export const getLocation = async (id: string) => {
	return await _apiClient.locationsByLocationId(id);
};

export const postLocation = async (location: CreateNewLocation) => {
	return await _apiClient.locationsCreate(location);
};
