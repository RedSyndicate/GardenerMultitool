import { db } from '$lib/db';
import type { PageServerLoad } from '../$types';

export const load: PageServerLoad = async ({ locals, params }) => {

    if (typeof (params.PlantId) !== 'number') {
        return
    }

    console.log(params.PlantId)
    const plant = await db.plant.findUnique({
        where: {
            PlantId: 1
        }
    });

    return {
        plant
    }
}