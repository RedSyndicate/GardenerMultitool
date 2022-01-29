<script lang="ts">
	export let plant;
</script>

<div class="card card-bordered mx-5 card-compact">
	<figure>
		<img
			class="h-fit"
			src={`https://via.placeholder.com/460x640/333333/cccccc?text=${
				plant.name.value.split('(')[0]
			}`}
			alt={plant.name.value}
		/>
	</figure>
	<div class="card-body">
		<h2 class="card-title mx-auto">{plant.name.value}</h2>
	</div>
	<p class="justify-center text-center">
		{plant.notes.length > 150 ? `${plant.notes.substr(0, 150 - 1)} ...` : plant.notes}
	</p>
	<div class="justify-end card-actions">
		<button class="btn btn-secondary">More info</button>
	</div>
</div>

<slot />
