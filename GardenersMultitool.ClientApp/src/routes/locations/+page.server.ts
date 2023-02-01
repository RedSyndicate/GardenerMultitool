import { db } from '$lib/db';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = (async ({ locals, params }) => {
    const locations = await db.location.findMany();

    return {
        locations
    };
});