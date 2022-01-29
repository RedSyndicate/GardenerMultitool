<script lang="ts">
	import { each, onMount } from 'svelte/internal';
	import { annuals, fetchAnnuals } from '$lib/plant';
	import Plant from '$components/plant.svelte';

	onMount(() => {
		fetchAnnuals();
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Plants</h1>

<div class="m-1 grid gap-4 sm:grid-cols-3 md:grid-cols-6 lg:grid-cols-1">
	{#each $annuals as plant}
		<Plant {plant} />
	{/each}
</div>
