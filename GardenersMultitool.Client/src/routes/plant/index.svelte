<script lang="ts">
	import type { Plant } from '$lib/api/client';
	import PlantComponent from '$component/plant.svelte';

	export let plant: Plant;
</script>

<PlantComponent {plant} />
