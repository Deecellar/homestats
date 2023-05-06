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
	() => import('./nodes/11'),
	() => import('./nodes/12'),
	() => import('./nodes/13')];

export const server_loads = [];

export const dictionary = {
	"/(marketing)": [11,[4]],
	"/(marketing)/company": [~12,[4]],
	"/(marketing)/contact": [13,[4]],
	"/(admin)/login": [5],
	"/(admin)/register": [6],
	"/(app)/stats": [7,[,3],[2]],
	"/(app)/stats/home/create": [8,[,3],[2]],
	"/(app)/stats/home/update/[id=string]": [9,[,3],[2]],
	"/(app)/stats/home/[id=string]": [10,[,3],[2]]
};

export const hooks = {
	handleError: (({ error }) => { console.error(error) }),
};

export { default as root } from '../root.svelte';