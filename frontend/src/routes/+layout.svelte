<script lang="ts">
	import type { LayoutData } from './$types';

  import storage from '$lib/stores';

    import '../app.postcss';
	import type { Writable } from 'svelte/store';
	import { onMount } from 'svelte';
	export let data: LayoutData;

  interface PrivacyPolicy {
    privacy_show: boolean;
    privacy_accepted: boolean;
  };

   
   let auth: Writable<PrivacyPolicy>;
    onMount( async () => {
    await authInit();  
    });

    const authInit = async () => {
      auth = await storage<PrivacyPolicy>("privacy_policy", {privacy_accepted: false, privacy_show: true});
    }

     function acceptAll() {
      auth.set({privacy_show: false, privacy_accepted: true})
    }
     function rejectAll() {
      auth.set({privacy_show: false, privacy_accepted: false})
    }

</script>
<template>
    <slot/>
    <!-- Privacy Noticy -->
    {#await authInit()}
    {:then a}
    {#if $auth.privacy_show}
    <div class="pointer-events-none fixed inset-x-0 bottom-0 px-6 pb-6">
        <div class="pointer-events-auto max-w-xl rounded-xl bg-gray-900 p-6 shadow-lg ring-1 ring-gray-50/10">
          <p class="text-sm leading-6 text-gray-50">¡Bienvenido al reino mágico de nuestra página web! Aquí usamos cookies encantadas para mejorar tu experiencia de navegación. Son como pequeñas haditas digitales que nos ayudan a personalizar tu visita. No te preocupes, solo recopilamos información mágica y anónima. ¿Aceptas ser parte de esta aventura? ¡Haz clic en "Aceptar" y dejemos que la magia comience! 🍪✨. Mira más en <a href="#" class="font-semibold text-indigo-400">Política de cookies</a>.</p>
          <div class="mt-4 flex items-center gap-x-5">
            <button type="button" on:click={acceptAll} class="rounded-md bg-gray-50 px-3 py-2 text-sm font-semibold text-black shadow-sm hover:bg-gray-200 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-gray-200">Accept all</button>
            <button type="button" on:click={rejectAll} class="text-sm font-semibold leading-6 text-gray-50">Reject all</button>
          </div>
        </div>
      </div>
    {/if}
    {/await}
    

    
</template>