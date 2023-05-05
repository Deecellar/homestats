export { matchers } from './matchers.js';

export const nodes = [() => import('./nodes/0'),
	() => import('./nodes/1'),
	() => import('./nodes/2'),
	() => import('./nodes/3'),
	() => import('./nodes/4'),
	() => import('./nodes/5'),
	() => import('./nodes/6'),
	() => import('./nodes/7'),
	() => import('./nodes/8'),
	() => import('./nodes/9'),
	() => import('./nodes/10'),
	() => import('./nodes/11')];

export const server_loads = [];

export const dictionary = {
	"/(marketing)": [9,[4]],
	"/(marketing)/company": [~10,[4]],
	"/(marketing)/contact": [11,[4]],
	"/(admin)/login": [5],
	"/(admin)/register": [6],
	"/(app)/stats": [7,[,3],[2]],
	"/(app)/stats/home/[id=string]": [8,[,3],[2]]
};

export const hooks = {
	handleError: (({ error }) => { console.error(error) }),
};

export { default as root } from '../root.svelte';