import { db } from '$lib/db';
import type { PageServerLoad } from '../$types';

export const load: PageServerLoad = async ({ locals, params }) => {

    const plants = await db.plant.findMany();

    return {
        plants: plants
    }
}