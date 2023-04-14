<script lang="ts">
	import { env } from '$env/dynamic/public';
	import { Client, House, HouseListResponse, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import type { PageData } from './$types';
    
    export let page_data: PageData;
    let homes = new Array<House>();
    onMount(async() => {
      let client = new Client(new IConfig(localStorage),env.PUBLIC_API_URL);
    let homie  =client.houseGET(undefined,undefined,"1");
    homes = (await homie).data!;
    })

</script>

<template>
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
      {#each homes as home}
      <div id="home_{home.id}" class="relative flex items-center space-x-3 rounded-lg border border-gray-300 bg-white px-6 py-5 shadow-sm focus-within:ring-2 focus-within:ring-indigo-500 focus-within:ring-offset-2 hover:border-gray-400">

          <div class="min-w-0 flex-1">
            <a href="/stats/home/{home.id}" class="focus:outline-none">
              <span class="absolute inset-0" aria-hidden="true"></span>
              <p class="text-sm font-medium text-gray-900">{home.name}</p>
              <p class="truncate text-sm text-gray-500">{home.city}/{home.address}</p>
            </a>
          </div>
        </div>
      {/each}
      
      </div>
      
</template>