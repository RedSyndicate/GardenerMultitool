<script lang="ts">
	import Plant from '$components/plant.svelte';
	import type { PageData } from './$types';

	import {
		// Utilities
		createDataTableStore,
		dataTableHandler,
		// Svelte Actions
		tableInteraction,
		tableA11y,
		Paginator
	} from '@skeletonlabs/skeleton';

	export let data: PageData;

	const dataTableStore = createDataTableStore(
		// Pass your source data here:
		data.plants,
		// Provide optional settings:
		{
			// The current search term.
			search: '',
			// The current sort key.
			sort: '',
			// Paginator component settings.
			pagination: { offset: 0, limit: 10, size: 0, amounts: [10, 25, 50, 100] }
		}
	);

	// This automatically handles search, sort, etc when the model updates.
	dataTableStore.subscribe((model) => dataTableHandler(model));

	$: dataTableStore.updateSource(data.plants);
</script>

<svelte:head>
	<title>Gardener Multitool | Plants</title>
</svelte:head>

<h1>Plants</h1>

<div class="table-container">
	<div class="w-64">
		<input bind:value={$dataTableStore.search} type="search" placeholder="Search..." />
	</div>
	<table class="table table-hover table-compact" role="grid" use:tableA11y use:tableInteraction>
		<thead
			on:click={(e) => {
				dataTableStore.sort(e);
			}}
			on:keypress
		>
			<tr>
				<th>
					<input
						type="checkbox"
						on:click={(e) => {
							dataTableStore.selectAll(e.currentTarget.checked);
						}}
					/>
				</th>
				<th>Id</th>
				<th data-sort="Name">Name</th>
				<th data-sort="ScientificName">Scientific Name</th>
				<th>Plant Type</th>
				<th>Binomial</th>
				<th data-sort="MinimumHardinessZone">Hardiness Zone (min)</th>
				<th data-sort="MaximumHardinessZone">Hardiness Zone (max)</th>
				<!-- <th data-sort="Notes">Notes</th> -->
			</tr>
		</thead>
		<tbody>
			{#each $dataTableStore.filtered as row, rowIndex}
				<tr class:table-row-checked={row.dataTableChecked} aria-rowindex={rowIndex + 1}>
					<td role="gridcell" aria-colindex={1} tabindex="0">
						<input type="checkbox" bind:checked={row.dataTableChecked} />
					</td>
					<td role="gridcell" aria-colindex={2} tabindex="0">{row.PlantId}</td>
					<td role="gridcell" aria-colindex={3} tabindex="0">{row.Name}</td>
					<td role="gridcell" aria-colindex={4} tabindex="0">{row.ScientificName}</td>
					<td role="gridcell" aria-colindex={5} tabindex="0">{row.PlantType}</td>
					<td role="gridcell" aria-colindex={6} tabindex="0">{row.Binomial}</td>
					<td role="gridcell" aria-colindex={7} tabindex="0">
						{row.MinimumHardinessZone}
					</td>
					<td role="gridcell" aria-colindex={8} tabindex="0">
						{row.MaximumHardinessZone}
					</td>
					<!-- <td role="gridcell" aria-colindex={4} tabindex="0">{row.Notes}</td> -->
				</tr>
			{/each}
		</tbody>
	</table>
	{#if $dataTableStore.pagination}
		<Paginator
			bind:settings={$dataTableStore.pagination}
			justify="between"
			amountText="Plants"
			buttonClasses="btn-icon variant-filled-primary"
		/>
	{/if}
</div>
