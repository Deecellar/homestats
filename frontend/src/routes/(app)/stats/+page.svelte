<script lang="ts">
	import { env } from '$env/dynamic/public';
	import { Client, House, HouseListResponse, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import type { PageData } from './$types';
    
    export let page_data: PageData;
    let homes = new Array<House>();
      let client : Client | null = null;
    onMount(async() => {
      let clientInternal = new Client(new IConfig(localStorage),env.PUBLIC_API_URL);
    let homie  =clientInternal.houseGET(undefined,undefined,"1");
    homes = (await homie).data!;
    client = clientInternal;
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
      <!-- Buttons for Edit and Delete-->
      <div class="absolute inset-y-0 right-0 flex items-center space-x-3">
        <a href="/stats/home/update/{home.id}" class="inline-flex items-center justify-center p-2 rounded-md text-gray-400 hover:bg-gray-100 hover:text-gray-500 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500">
          <span class="sr-only">Edit</span>
          <!-- Heroicon name: solid/pencil -->
          <svg class="h-5 w-5" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" aria-hidden="true">
            <path fill-rule="evenodd" d="M14.293 3.293a1 1 0 011.414 0l2 2a1 1 0 010 1.414l-9 9a1 1 0 01-.39.242l-3 1a1 1 0 01-1.266-1.265l1-3a1 1 0 01.242-.391l9-9zM15 7l-8.707 8.707-1 3 3-1L17 5.414V7h-2z" clip-rule="evenodd"></path>
          </svg>
        </a>

      </div>
          </div>
        </div>
      {/each}
      <div class="relative flex items-center space-x-3 rounded-lg border border-gray-300 bg-white px-6 py-5 shadow-sm focus-within:ring-2 focus-within:ring-indigo-500 focus-within:ring-offset-2 hover:border-gray-400">
        <div class="min-w-0 flex-1">
          <a href="/stats/home/create" class="focus:outline-none">
            <span class="absolute inset-0" aria-hidden="true"></span>
            <p class="text-sm font-medium text-gray-900">Create New Home</p>
            <p class="truncate text-sm text-gray-500">Create a new home</p>
          </a>
        </div>
      </div>
    </div>
      
</template>