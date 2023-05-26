<script lang="ts">
	import type { LayoutData } from '../$types';

	export let data: LayoutData;
</script>
<template>
	<header class="bg-gray-900">
		<nav class="mx-auto flex max-w-7xl items-center justify-between p-6 lg:px-8" aria-label="Global">
		  <a href="/" class="-m-1.5 p-1.5">
			<span class="sr-only">Compañia</span>
			<img class="h-8 w-auto" src="https://cdn-icons-png.flaticon.com/512/8775/8775336.png" alt="">
		  </a>
		  <div class="flex lg:hidden">
			<button type="button" class="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5 text-gray-200">
			  <span class="sr-only">Abrir Menu</span>
			  <svg class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" aria-hidden="true">
				<path stroke-linecap="round" stroke-linejoin="round" d="M3.75 6.75h16.5M3.75 12h16.5m-16.5 5.25h16.5" />
			  </svg>
			</button>
		  </div>
		  <div class="hidden lg:flex lg:gap-x-12">
			<a href="/" class="text-sm font-semibold leading-6 text-gray-50">Inicio</a>
	  
			<a href="/company" class="text-sm font-semibold leading-6 text-gray-50">Compañia</a>
	  
			<a href="/contact" class="text-sm font-semibold leading-6 text-gray-50">Contactanos</a>
	  
			<a href="/login" class="text-sm font-semibold leading-6 text-gray-50">Iniciar Sesion <span aria-hidden="true">&rarr;</span></a>
		  </div>
		</nav>
		<!-- Mobile menu, show/hide based on menu open state. -->
		<div class="lg:hidden" role="dialog" aria-modal="true">
		  <!-- Background backdrop, show/hide based on slide-over state. -->
		  <div class="fixed inset-0 z-10"></div>
		  <div class="fixed inset-y-0 right-0 z-10 w-full overflow-y-auto bg-gray-900 px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-50/10">
			<div class="flex items-center justify-between">
			  <a href="/" class="-m-1.5 p-1.5">
				<span class="sr-only">Compañia</span>
				<img class="h-8 w-auto" src="https://tailwindui.com/img/logos/mark.svg?color=blue&shade=500" alt="">
			  </a>
			  <button type="button" class="-m-2.5 rounded-md p-2.5 text-gray-200">
				<span class="sr-only">Cerrar Menu</span>
				<svg class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" aria-hidden="true">
				  <path stroke-linecap="round" stroke-linejoin="round" d="M6 18L18 6M6 6l12 12" />
				</svg>
			  </button>
			</div>
			<div class="mt-6 flow-root">
			  <div class="-my-6 divide-y divide-gray-100/10">
				<div class="space-y-2 py-6">
				  <a href="/" class="-mx-3 block rounded-lg py-2 px-3 text-base font-semibold leading-7 text-gray-50 hover:bg-gray-50">Inicio</a>
	  
				  <a href="/contact" class="-mx-3 block rounded-lg py-2 px-3 text-base font-semibold leading-7 text-gray-50 hover:bg-gray-50">Contactanos</a>
	  
				  <a href="/company" class="-mx-3 block rounded-lg py-2 px-3 text-base font-semibold leading-7 text-gray-50 hover:bg-gray-50">Compañia</a>
				</div>
				<div class="py-6">
				  <a href="/login" class="-mx-3 block rounded-lg py-2.5 px-3 text-base font-semibold leading-7 text-gray-50 hover:bg-gray-50">Iniciar Sesion</a>
				</div>
			  </div>
			</div>
		  </div>
		</div>
	  </header>
<slot />

</template>
