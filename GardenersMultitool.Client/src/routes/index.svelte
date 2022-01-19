<script lang="ts">
	import { each } from 'svelte/internal';
	import { plants } from '../stores/plantstore';
	import Plant from '../components/plant.svelte';
	console.log(plants);
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Plants</h1>

<div class="py-4 grid gap-4 md:grid-cols-6 grid-cols-1">
	{#each $plants as plant}
		<Plant {plant} />
	{/each}
</div>
