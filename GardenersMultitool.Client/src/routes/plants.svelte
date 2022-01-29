<script lang="ts">
	import { each, onMount } from 'svelte/internal';
	import { plants, getAllPlants } from '$lib/plant';

	let total: number = 30;
	let page: number = 1;
	let perPage: number = 5;

	$: {
	}
	onMount(() => {
		getAllPlants(total, page, perPage);
		console.log($plants);
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<div class="mx-5 card card-compact">
	<div class="form-control card-body">
		<label for="plant-search">Search for plants</label>
		<input id="plant-search" type="search" class="input input-ghost input-bordered input-primary" />
	</div>
</div>
<hr class="my-5" />
<div class="card">
	<h1 class="m-5 card-title">Plants</h1>
	<div class="card-body">
		{#each Array(total / (total / perPage)) as _, index}
			<div class="card card-compact card-bordered">
				<h1 class="mx-3 my-1 card-title">{$plants[index].name.value}</h1>
				<div class="card-body">
					{$plants[index].notes.length > 150
						? `${$plants[index].notes.substring(0, 150)}...`
						: $plants[index].notes}
				</div>
			</div>
		{/each}
	</div>
</div>
<div class="card card-compact">
	<div class="btn-group card-actions justify-center">
		<button class="btn btn-sm">Previous</button>
		{#each Array(total / perPage) as _, index}
			{#if page == index + 1}
				<button class="btn btn-sm btn-active" />
			{:else}
				<button class="btn btn-sm">{index + 1}</button>
			{/if}
		{/each}
		<button class="btn btn-sm">Next</button>
	</div>
</div>
