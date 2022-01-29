<script lang="ts">
	import { each, object_without_properties, onMount } from 'svelte/internal';
	import { plants, getAllPlants } from '$lib/plant';

	let total;
	let totalPerPage;

	onMount(() => {
		getAllPlants(10);
		total = $plants.length;
		totalPerPage = 10;

		console.log(Object.keys($plants));
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<div class="mx-5 card card-compact">
	<h1 class="card-title">Plants</h1>
	<div class="form-control card-body">
		<label for="plant-search">Search for plants</label>
		<input id="plant-search" type="search" class="input input-ghost input-bordered input-primary" />
	</div>
</div>
<hr class="my-5" />
<div class="card">
	<div class="btn-group card-actions">
		<button class="btn">Previous</button>
		{#each $plants as plant, index}
			<button class="btn">{index}</button>
		{/each}
		<button class="btn">Next</button>
	</div>
</div>
