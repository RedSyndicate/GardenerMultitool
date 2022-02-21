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
	import { plantStore as plants, getPlantsAll } from '$lib/api/plant';
	onMount(() => {
		console.log($plants);
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<div class="card">
	<div class="card-body mx-1 my-5 p-0">
		<div class="w-full shadow-lg stats">
			<div class="stat">
				<div class="stat-title">All Plants</div>
				<div class="stat-value">{$plants.length}</div>
				<div class="stat-desc">Total plants</div>
			</div>
		</div>
	</div>
</div>
<div class="card">
	<div class="card-body mx-1 my-5 p-0">
		<div class="w-full shadow-lg stats">
			<div class="stat">
				<div class="stat-title">Annuals</div>
				<div class="stat-value">
					{$plants.filter((p) => p.plantType.label.toLowerCase() === 'annual').length}
				</div>
				<div class="stat-desc">Total plants</div>
			</div>
			<div class="stat">
				<div class="stat-title">Vines</div>
				<div class="stat-value">
					{$plants.filter((p) => p.plantType.label.toLowerCase() === 'vine').length}
				</div>
				<div class="stat-desc">Total plants</div>
			</div>
		</div>
	</div>
</div>
