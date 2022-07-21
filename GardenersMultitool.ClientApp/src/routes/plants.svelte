<script lang="ts">
	import { each } from 'svelte/internal';
	import { annuals } from '$lib/plant';
	import Plant from '$components/plant.svelte';
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Plants</h1>

<div class="py-4 grid gap-4 md:grid-cols-6 grid-cols-1">
	{#each $annuals as plant}
		<Plant {plant} />
	{/each}
</div>
