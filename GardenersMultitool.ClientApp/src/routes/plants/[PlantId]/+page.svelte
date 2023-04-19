<script lang="ts">
	import Plant from '$components/plant.svelte';
	import type { PageData } from './$types';

	export let data: PageData;
</script>

<svelte:head>
	<title>Gardener Multitool</title>
</svelte:head>

<Plant plant={data.plant} />
