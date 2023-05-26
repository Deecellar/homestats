<script lang="ts">
	import { Client, UpdateHouseCommand, House, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';

	import type { PageData } from './$types';
	import { env } from '$env/dynamic/public';
	import { goto } from '$app/navigation';
	import { page } from '$app/stores';
	import { houseStore, setInvalidate } from '$lib/houseStore';

	export let data = {
		content: ''
	};

	let formData: UpdateHouseCommand | null = null;
	let id = $page.params.id;
	let client: Client | null = null;

	let formDataHouse: House = new House({
		name: 'place',
		address: '',
		city: '',
		state: ''
	});

    

	onMount(async () => {
		client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
		console.log(client);
		try {
			console.log('Trying');
			let response = await client.houseGET2(id, '1');
			console.log(response);
			if (response.succeeded) {
				formData = new UpdateHouseCommand({ house: response.data! });
				formDataHouse.address = formData.house!.address!;
				formDataHouse.city = formData.house!.city!;
				formDataHouse.state = formData.house!.state!;
				formDataHouse.name = formData.house!.name!;

				console.log('Success');
			} else {
				console.log('Failure');
			}
		} catch (error) {
			console.log(error);
		}
	});

	async function submit() {
		if (client && formData) {
			formData.house = formDataHouse;
			formData.id = id;

			try {
				let response = await client.housePUT(id, '1', formData);

				if (response.succeeded) {
					console.log('Success');
                    setInvalidate();
                    $houseStore.at(0);
					goto('/stats');
				} else {
					console.log('Failure');
                    setInvalidate();
                    $houseStore.at(0);
				}
			} catch (error) {
				console.log(error);
			}
		}
	}

	async function deleteHouse() {
		if (client) {
			let response = await client.houseDELETE(id, '1');
            setInvalidate();
            $houseStore.at(0);
			goto('/stats');
		}
	}

	function open() {
		let modal = document.getElementById('dialog') as HTMLDialogElement;
		modal.showModal();
	}

	function close() {
		let modal = document.getElementById('dialog') as HTMLDialogElement;
		modal.close();
	}
</script>

<template>
	<form class="form-tailwind-ui" on:submit|preventDefault={submit}>
		<label for="name" class="label-input">Name</label>
		<input type="text" id="name" class="input" bind:value={formDataHouse.name} />

		<label for="address" class="label-input">Address</label>
		<input type="text" id="address" class="input" bind:value={formDataHouse.address} />

		<label for="city" class="label-input">City</label>
		<input type="text" id="city" class="input" bind:value={formDataHouse.city} />

		<label for="state" class="label-input">State</label>
		<input type="text" id="state" class="input" bind:value={formDataHouse.state} />

		<div class="button-group">
			<!-- Delete button here-->
			<!-- We  color it red on hover, in a material like design-->
			<button type="submit" class="submit-button ">Submit</button>
			<button on:click={open} type="button" class="delete-house-button"> Delete House </button>
		</div>
	</form>

	<!-- Confirmation Dialog-->
	<dialog
		id="dialog"
		class="rounded-sm bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 "
	>
		<p>Quieres eliminar esta casa?</p>
		<div class="button-group justify-between">
			<button class="px-10" on:click={deleteHouse}>Si</button>
			<button class="px-10" on:click={close}>No</button>
		</div>
	</dialog>
</template>

<style>
	.label-input {
		@apply text-gray-700 font-bold mb-1 block text-base;
	}
	.input {
		@apply w-full border border-gray-300 rounded-md px-4 py-2 mb-4;
	}
	.submit-button {
		@apply bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded;
	}
	.form-tailwind-ui {
		@apply max-w-lg mx-auto bg-white shadow-lg rounded px-8 pt-6 pb-8 mb-4;
	}
	.center {
		@apply mx-auto block;
	}

	/* We center the buttons in the form */
	.button-group {
		@apply flex flex-row;
	}

    .button-group button {
        @apply flex-1;
    }
	/* Button group that has a button of type submit uses justify-center otherwise justify-between*/

	.button-group .submit-button {
		/*         We remove the right border so it "joins" the button next to it*/
		@apply rounded-r-none rounded-tr-none border-r-0;
	}
	/*All buttons except submit button*/
	.button-group button:not(.submit-button) {
		@apply rounded-l-none rounded-tl-none;
	}

	.delete-house-button {
		@apply bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded;
	}

	#dialog button {
		@apply bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4  mt-10 border-t border-b border-red-600;
	}
	#dialog {
		@apply rounded-md bg-red-500  text-white font-bold py-2 px-4;
	}
</style>
