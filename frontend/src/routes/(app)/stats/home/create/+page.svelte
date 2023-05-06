<script lang="ts">
	import { Client, CreateHouseCommand, House, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import type { PageData } from './$types';
	import { env } from '$env/dynamic/public';
	import { goto } from '$app/navigation';

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

onMount(async() => {
    client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
})

async function submit() {
    if (client) {
        formData.house = formDataHouse;
        let response = await client.housePOST("1",formData);
        if(response.succeeded) {
            console.log("Success");
            goto("/stats");
        } else {
            console.log("Failure");
        }
    }
}



</script>

<template>
        <form on:submit|preventDefault={submit}>
            <label for="name">Name</label>
            <input type="text" id="name" bind:value={formDataHouse.name}>

            <label for="address">Address</label>
            <input type="text" id="address" bind:value={formDataHouse.address}>

            <label for="city">City</label>
            <input type="text" id="city" bind:value={formDataHouse.city}>

            <label for="state">State</label>
            <input type="text" id="state" bind:value={formDataHouse.state}>

            <button type="submit">Submit</button>
        </form>
</template>
