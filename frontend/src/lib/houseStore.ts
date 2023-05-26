// A svelte storage for a list of House[]

import { readable, type StartStopNotifier, type Subscriber } from 'svelte/store';
import { Client, House, HouseListResponse, IConfig } from '$lib/clients';
import { env } from '$env/dynamic/public';

// We get from the server the list of houses
// and we store it in a svelte store

let client: Client | null = null;
let list: House[] = [];
let invalidate = false;
let storage: Storage | null = null;
let interval: NodeJS.Timer | null = null;
export const houseStore = readable<House[]>(
	[],
	 function start(set: Subscriber<House[]>) {
		// When a new house is added, deleted or updated, we update the store
		// Also every 30 seconds we update the store
        console.log("start");
        if(interval == null) {
            interval = setInterval(() => {
                houseGet(list)
                    .then((houses) => {
                        set(houses);   
                    })
                    .catch((e) => {
                        console.log(e);
                    });
            }, 500);
        }

			houseGet(list)
				.then((houses) => {
					set(houses);
				})
				.catch((e) => {
					console.log(e);
				});

		return function stop() {
            set([]);
            if(interval != null) {
                clearInterval(interval);
                interval = null;
            }

		};
	}
);

async function houseGet(old: House[] | null) {
	if (client == null && storage != null) {
        client = new Client(new IConfig(storage), env.PUBLIC_API_URL);
	}
	try {
		let housesResponse = await client?.houseGET(undefined, undefined, '1');
		list = housesResponse!.data!;
		return housesResponse!.data!;
	} catch (e) {
		console.log(e);
		return old || [];
	}
}

export function setStorage(s: Storage) {
	storage = s;
}


export function setInvalidate() {
    invalidate = true;
    houseGet(list).then((houses) => {
        list = houses;
    }).catch((e) => {
        console.log(e);
    });

}