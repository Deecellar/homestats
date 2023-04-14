import type { PageLoad } from './$types';
import json from  "../../../lib/data/mockup-data.json";
export const load = (async () => {
        // We first load the data from the server
        
        return json;

}) satisfies PageLoad;