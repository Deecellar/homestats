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
	import { Client, HouseAggregator, IConfig, Sensor } from '$lib/clients';
	import { onMount } from 'svelte';
    import { env } from '$env/dynamic/public';
    let id = $page.params.id;
    let homes = new HouseAggregator();
    var temp: Sensor[] = [];
    var humidity:Sensor[] = [];
    var sun: Sensor[] = [];

        onMount(async() => {
            let client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
            try {
            let homie = await client.sensorGET2(id, "1");
            temp = homie.filter( (x) => x.type == 0);
            humidity = homie.filter( (x) => x.type == 1);
            sun = homie.filter( (x) => x.type == 2);

            }
            catch (e) {
                console.log(e);
            }

        })

	ChartJS.register(Title, Tooltip, Legend, LineElement, LinearScale, PointElement, CategoryScale);
	export let data: PageData;

    var temp_data = {
        labels: temp.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Temperature',
                data: temp.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var humidity_data = {
        labels: humidity.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Humidity',
                data: humidity.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var sun_data = {
        labels: sun.flatMap( (x) => x.recordedAt),
        datasets: [
            {
                label: 'Sun',
                data: sun.flatMap( (x) => x.value),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
	$: {
        temp.sort( (x, y)=> x.recordedAt?.getTime() - y.recordedAt?.getTime());
        humidity.sort( (x, y)=> x.recordedAt?.getTime() - y.recordedAt?.getTime());
        sun.sort( (x, y)=> x.recordedAt?.getTime() - y!.recordedAt?.getTime());

        temp_data = {
            labels: temp.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Temperature',
                    data: temp.flatMap( (x) => x.value),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        humidity_data = {
            labels: humidity.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Humidity',
                    data: humidity.flatMap( (x) => x.value),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        sun_data = {
            labels: sun.flatMap( (x) => x.recordedAt),
            datasets: [
                {
                    label: 'Sun',
                    data: sun.flatMap( (x) => x.value),
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
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{temp.at(0)?.value}</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(temp.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded Humidity Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{humidity.at(0)?.value}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(humidity.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded  Sun Exposure Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{sun.at(0)?.value}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(sun.at(0)?.recordedAt)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
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
