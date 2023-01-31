<script lang="ts">
	import Plant from '$components/plant.svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<h1>Plants</h1>

<ul role="list" class="grid grid-cols-3 gap-6">
	{#each data.plants as plant}
		<li><Plant {plant} /></li>
	{/each}
</ul>
