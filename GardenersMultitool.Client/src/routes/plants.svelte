<script context="module">
	/** @type {import('@sveltejs/kit').Load} */
	export async function load({ params, fetch, session, stuff }) {
		return {
			props: {
				plants: await getPlantsAll()
			}
		};
	}
</script>

<script lang="ts">
	import { each, onMount } from 'svelte/internal';
	import { plants, getPlantsAll } from '$lib/plant';
	import Plant from '$components/plant.svelte';
	import Pagination from '$components/pagination.svelte';

	let total: number = 100;
	let page: number = 1;
	let perPage: number = 10;
	let orderBy: string = 'name';

	onMount(async () => {
		console.log(
			$plants
				.sort((a, b) => {
					if (a.name.value < b.name.value) return -1;
					else return 0;
				})

				.slice(0, perPage)
		);
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<div class="mx-1 card card-compact justify-start">
	<div class="form-control card-body">
		<label for="plant-search">Search for plants</label>
		<input id="plant-search" type="search" class="input input-ghost input-bordered input-primary" />
	</div>
</div>
<div class="divider" />
<Pagination {total} {page} {perPage} />
<div class="card">
	<div class="m-1 p-0 card-body">
		{#each $plants.slice(0, 10) as plant}
			<Plant {plant} />
		{/each}
	</div>
</div>
<Pagination {total} {page} {perPage} />
