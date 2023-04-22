import type { Plant } from '@prisma/client';
import { writable, type Writable } from 'svelte/store';

export const plants: Writable<Plant[]> = writable([]);