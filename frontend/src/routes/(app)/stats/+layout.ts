import type { LayoutLoad } from './$types';
import json from  "../../../lib/data/mockup-data.json";
export const load = (async () => {
    return json;
}) satisfies LayoutLoad;