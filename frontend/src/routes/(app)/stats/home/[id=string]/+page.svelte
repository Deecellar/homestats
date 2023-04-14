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
    let homes = new HouseAggregator();
        onMount(async() => {
            let client = new Client(new IConfig(localStorage), env.PUBLIC_API_URL);
            let homie = client.sensorGET2("", "1");
            homes = (await homie).data!;
        })

	ChartJS.register(Title, Tooltip, Legend, LineElement, LinearScale, PointElement, CategoryScale);
	export let data: PageData;

	var index: number = Number.parseInt($page.params.id) - 1;
    var home = data.homes[index];
    var temp_data = {
        labels: home.temperatures?.flatMap( (x) => x.date),
        datasets: [
            {
                label: 'Temperature',
                data: home.temperatures?.flatMap( (x) => x.temperatureC),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var humidity_data = {
        labels: home.humidity?.flatMap( (x) => x.date),
        datasets: [
            {
                label: 'Humidity',
                data: home.humidity?.flatMap( (x) => x.humidity),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
    var sun_data = {
        labels: home.sun?.flatMap( (x) => x.date),
        datasets: [
            {
                label: 'Sun',
                data: home.sun?.flatMap( (x) => x.sun),
                backgroundColor: 'rgba(255, 99, 132, 0.2)',
                borderColor: 'rgba(255, 99, 132, 1)',
                borderWidth: 1,
            },
        ],
    };
	$: index = Number.parseInt($page.params.id) - 1;
	$: {
        home = data.homes[index];
        
        home.temperatures?.sort( (x, y)=> x.id - y.id);
        home.humidity?.sort( (x, y)=> x.id - y.id);
        home.sun?.sort( (x, y)=> x.id - y.id);

        temp_data = {
            labels: home.temperatures?.flatMap( (x) => x.date),
            datasets: [
                {
                    label: 'Temperature',
                    data: home.temperatures?.flatMap( (x) => x.temperatureC),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        humidity_data = {
            labels: home.humidity?.flatMap( (x) => x.date),
            datasets: [
                {
                    label: 'Humidity',
                    data: home.humidity?.flatMap( (x) => x.humidity),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };

        sun_data = {
            labels: home.sun?.flatMap( (x) => x.date),
            datasets: [
                {
                    label: 'Sun',
                    data: home.sun?.flatMap( (x) => x.sun),
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                },
            ],
        };
    }
</script>

<svelte:head>
	<title>{home.name}</title>
</svelte:head>

<template >
    <div>
        <h3 class="text-base font-semibold leading-6 text-gray-900">Latest Datapoints</h3>
        <dl class="mt-5 grid grid-cols-1 gap-5 sm:grid-cols-3">
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Last Temperature recorded in Cº </dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{home.temperatures?.at(0)?.temperatureC}</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(home.temperatures?.at(0)?.date)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded Humidity Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{home.humidity?.at(0)?.humidity}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(home.humidity?.at(0)?.date)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
          </div>
      
          <div class="overflow-hidden rounded-lg bg-white px-4 py-5 shadow sm:p-6">
            <dt class="truncate text-sm font-medium text-gray-500">Latest Recorded  Sun Exposure Percentage</dt>
            <dd class="mt-1 text-3xl font-semibold tracking-tight text-gray-900">{home.sun?.at(0)?.sun}%</dd>
            <dt class="truncate text-sm font-medium text-gray-500">Recorded at: {new Date(Date.parse(home.sun?.at(0)?.date)).toLocaleString("en-US", {weekday: "long", year: "numeric", month: "long", day: "numeric"})}</dt>
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
