import { db } from '$lib/db';
import type { PageServerLoad } from '../$types';

export const load: PageServerLoad = async ({ locals, params }) => {
    return {
        plants: await db.plant.findMany()
    }
}