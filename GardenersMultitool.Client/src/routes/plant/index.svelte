<script lang="ts">
	import type { Plant } from '$lib/api/client';
	import PlantComponent from '$lib/components/plant.svelte';

	export let plant: Plant;
</script>

<PlantComponent {plant} />
