<script lang="ts">
	import { Client, CreateHouseCommand, House, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import type { PageData } from './$types';
	import { env } from '$env/dynamic/public';
	import { goto } from '$app/navigation';
	import { houseStore, setInvalidate } from '$lib/houseStore';

    export let data = {
        content: ''
    };

    
    let formData = new CreateHouseCommand({house: new House({
name : '',
address: '',
city: '',
state: ''})});

let formDataHouse: House = formData.house!;

let client : Client | null = null;
let store_house = houseStore;
onMount(async() => {
    client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);

})

async function submit() {
    if (client) {
        formData.house = formDataHouse;
        let response = await client.housePOST("1",formData);


        if(response.succeeded) {
            console.log("Success");
            setInvalidate();
            $houseStore.at(0);

            goto("/stats");
        } else {
            console.log("Failure");
            setInvalidate();
            $houseStore.at(0);

        }
    }
}



</script>

<template>
        <form class="form-tailwind-ui" on:submit|preventDefault={submit}>
            <label for="name" class="label-input">Name</label>
            <input type="text" id="name" class="input" bind:value={formDataHouse.name}>

            <label for="address" class="label-input">Address</label>
            <input type="text" id="address" class="input" bind:value={formDataHouse.address}>

            <label for="city" class="label-input">City</label>
            <input type="text" id="city" class="input" bind:value={formDataHouse.city}>

            <label for="state" class="label-input">State</label>
            <input type="text" id="state" class="input" bind:value={formDataHouse.state}>

            <button type="submit" class="submit-button center">Submit</button>
        </form>
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
</style>
