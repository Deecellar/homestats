<script lang="ts">
    import type { PageData } from './$types';
    
    export let data: PageData;
</script>

<template>
    <div class="grid grid-cols-1 gap-4 sm:grid-cols-2">
        {#each data.homes as home}
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