import { redirect } from '@sveltejs/kit';
import type { LayoutLoad } from './$types';
import { goto } from '$app/navigation';
export const load = (async () => {
    // We redirect if the user is not logged in
    return {}
}) satisfies LayoutLoad;