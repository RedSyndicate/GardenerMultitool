<script lang="ts">
	import { each, onMount } from 'svelte/internal';
	import { locations, getLocations } from '../lib/location';
	import Location from '../components/location.svelte';
	onMount(() => {
		getLocations();
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Locations</h1>

<div class="py-4 grid gap-4 md:grid-cols-6 grid-cols-1">
	{#each $locations as location}
		<Location {location} />
	{/each}
	<button>+</button>
</div>
