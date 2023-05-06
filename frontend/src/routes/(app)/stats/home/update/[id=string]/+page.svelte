<script lang="ts">
    import { Client, UpdateHouseCommand, House, IConfig } from '$lib/clients';
    import { onMount } from 'svelte';

    import type { PageData } from './$types';
    import { env } from '$env/dynamic/public';
    import { goto } from '$app/navigation';
	import { page } from '$app/stores';

    export let data = {
        content: ''
    };

    let formData : UpdateHouseCommand | null = null;
    let id = $page.params.id;
    let client : Client | null = null;

    let formDataHouse: House = new House({
        name : 'place',
        address: '',
        city: '',
        state: ''
    });

    onMount(async() => {
        client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
        let response = await client.houseGET2(id, "1");
        if(response.succeeded) {
            formData = new UpdateHouseCommand({house: response.data!});
            formDataHouse.address = formData.house!.address!;
            formDataHouse.city = formData.house!.city!;
            formDataHouse.state = formData.house!.state!;
            formDataHouse.name = formData.house!.name!;

            console.log("Success")
        } else {
            console.log("Failure");
        }
    });

    async function submit() {
        if (client && formData) {
            formData.house = formDataHouse;
            formData.id = id;
            let response = await client.housePUT(id, "1",formData);
    
            if(response.succeeded) {
                console.log("Success");
                goto("/stats");
            } else {
                console.log("Failure");
            }
        }
    }

    async function deleteHouse() {
        if (client) {
            let response = await client.houseDELETE(id, "1");
            goto("/stats");
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
        <!-- Delete button here-->
        <!-- We  color it red on hover, in a material like design-->
        <button on:click={deleteHouse} class="rounded-sm bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">
            Delete House 
        </button>
</template>
