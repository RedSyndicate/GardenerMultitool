import type { Plant } from '@prisma/client';
import { writable, type Writable } from 'svelte/store';
import { db } from './db';

export const plants: Writable<Plant[]> = writable([]);

export const fetch = async () => {
	plants.set(await db.plant.findMany());
}