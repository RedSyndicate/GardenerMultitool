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

	onMount(() => {
		console.log($plants);
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<div class="card">
	<div class="card-body">
		<div class="w-full shadow stats">
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Total</div>
				<div class="stat-value">{$plants.length}</div>
			</div>
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Annuals</div>
				<div class="stat-value">
					{$plants.flatMap((p) => p.plantType).length}
				</div>
			</div>
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Vines</div>
				<div class="stat-value">
					{$plants.filter((p) => p.plantType.label.toLowerCase() === 'vine').length}
				</div>
			</div>
		</div>
	</div>
</div>
<div class="card">
	<div class="card-body">
		<div class="w-full shadow stats">
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Total</div>
				<div class="stat-value">{$plants.length}</div>
			</div>
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Annuals</div>
				<div class="stat-value">
					{$plants.filter((p) => p.plantType.label.toLowerCase() === 'annual').length}
				</div>
			</div>
			<div class="stat place-items-center place-content-center">
				<div class="stat-title">Vines</div>
				<div class="stat-value">
					{$plants.filter((p) => p.plantType.label.toLowerCase() === 'vine').length}
				</div>
			</div>
		</div>
	</div>
</div>
