<script lang="ts">
	import { each, onMount } from 'svelte/internal';
	import { locations, getLocations } from '../lib/location';
	onMount(() => {
		getLocations();
	});
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Dashboard</h1>
