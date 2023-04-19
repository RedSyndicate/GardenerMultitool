// import { writable, type Writable } from 'svelte/store';
// import { LocationClient, CreateNewLocation, Location } from '$lib/api/client';
// import settings from '../settings.json';

// export const locations: Writable<Location[]> = writable([]);

// const _apiClient = new LocationClient(settings.ApiUrl);

// export const getLocations = async () => {
// 	locations.set(await _apiClient.locationGet());
// };

// export const getLocation = async (id: string) => {
// 	return await _apiClient.by_id(id);
// };

// export const postLocation = async (location: CreateNewLocation) => {
// 	return await _apiClient.locationPost(location);
// };
