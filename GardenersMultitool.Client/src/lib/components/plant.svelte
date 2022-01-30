<script lang="ts">
	import type { Plant } from '$lib/api/client';
	import SvgIcon from '@jamescoyle/svelte-icon';
	import { mdiClose, mdiInformation, mdiInformationOutline, mdiMore } from '@mdi/js';
	import { onMount } from 'svelte';

	export let plant: Plant;
	onMount(() => {
		// console.log(plant);
	});
</script>

<div class="card card-bordered card-compact card-side mx-1 my-2 h-32">
	<figure>
		<img
			src={`https://via.placeholder.com/460x640/333333/cccccc?text=${
				plant.name.value.split('(')[0]
			}`}
			alt={plant.name.value}
			class="w-32 h-32"
		/>
	</figure>
	<div class="card-body grid auto-cols-auto">
		<h1 class="card-title text-base lg:text-md grid-flow-row">
			{plant.name.value.split('(')[0]}
		</h1>
		<div class="justify-start card-actions m-0 p-0 grid-flow-row">
			<label for="modal-more-info-{plant.id}" class="btn btn-info modal-button btn-sm">
				<SvgIcon type="mdi" path={mdiInformationOutline} />
			</label>
		</div>
	</div>
</div>
<input type="checkbox" id="modal-more-info-{plant.id}" class="modal-toggle" />
<div class="modal h-full">
	<div class="modal-box p-0">
		<div class="card">
			<div class="card-body p-0">
				<h1 class="card-title ml-3 my-1">{plant.name.value}</h1>
			</div>
			<div class="divider" />
			<div class="CONTENT" />
		</div>
		<div class="modal-action">
			<label for="modal-more-info-{plant.id}" class="btn btn-circle">
				<SvgIcon type="mdi" path={mdiClose} />
			</label>
		</div>
	</div>
</div>

<slot />
