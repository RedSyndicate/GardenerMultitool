<script lang="ts">
	import type { Plant } from '@prisma/client';

	export let plant: Plant;
</script>

<div class="card p-5">
	<img
		class="h-40"
		src={`https://via.placeholder.com/460x640/333333/cccccc?text=${plant.Name}`}
		alt={plant.Name}
	/>
	{plant.Name}
</div>

<slot />
