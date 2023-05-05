<script lang="ts">
	import { page } from '$app/stores';
	import type { PageData } from '../[id=string]/$types';
	import { Line, Bar } from 'svelte-chartjs';
	import {
		Chart as ChartJS,
		Title,
		Tooltip,
		Legend,
		LineElement,
		LinearScale,
		PointElement,
		CategoryScale
	} from 'chart.js';
	import { Client, HouseAggregator, IConfig } from '$lib/clients';
	import { onMount } from 'svelte';
    import { env } from '$env/dynamic/public';
    let id = $page.params.id;
    let homes = new HouseAggregator();
        onMount(async() => {
            let client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
            let homie = client.sensorGET2(id, "1");
            homes = (await homie).data!;
        })

	ChartJS.register(Title, Tooltip, Legend, LineElement, LinearScale, PointElement, CategoryScale);
	export let data: PageData;

    var temp_data = {
        labels: homes.temperatures?.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Temperature',
                data: homes.temperatures?.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var humidity_data = {
        labels: homes.humidities?.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Humidity',
                data: homes.humidities?.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var sun_data = {
        labels: homes.sunExposures?.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Sun',
                data: homes.sunExposures?.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
	$: {
        homes.temperatures?.sort( (x, y)=> x.recordedAt?.getTime() - y.recordedAt?.getTime());
        homes.humidities?.sort( (x, y)=> x.recordedAt?.getTime() - y.recordedAt?.getTime());
        homes.sunExposures?.sort( (x, y)=> x.recordedAt?.getTime() - y!.recordedAt?.getTime());

        temp_data = {
            labels: homes.temperatures?.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Temperature',
                    data: homes.temperatures?.flatMap( (x) => x.value),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        humidity_data = {
            labels: homes.humidities?.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Humidity',
                    data: homes.humidities?.flatMap( (x) => x.value),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        sun_data = {
            labels: homes.sunExposures?.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Sun',
                    data: homes.sunExposures?.flatMap( (x) => x.value),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };
    }
</script>

<svelte:head>
	<title>{homes.house?.name}</title>
</svelte:head>

<template >
    <div>
        <h3 class="text-base font-semibold leading-6 text-gray-900">Latest Datapoints</h3>
        <dl class="mt-5 grid grid-cols-1 gap-5 sm:grid-cols-3">
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Last Temperature recorded in Cº </dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{homes.temperatures?.at(0)?.value}</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(homes.temperatures?.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded Humidity Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{homes.humidities?.at(0)?.value}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(homes.humidities?.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded  Sun Exposure Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{homes.sunExposures?.at(0)?.value}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(homes.sunExposures?.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
        </dl>
      </div>

      <div>
        <h3 class="text-base font-semibold leading-6 text-gray-900">Historic</h3>
        <dl class="mt-5 grid grid-cols-1 gap-5 sm:grid-cols-3">
            <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <Line data={temp_data} options={{ responsive: true }} ></Line>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <Line data={humidity_data} options={{ responsive: true }} ></Line>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <Line data={sun_data} options={{ responsive: true }} ></Line>
          </div>
        </dl>
      </div>
      
</template>
