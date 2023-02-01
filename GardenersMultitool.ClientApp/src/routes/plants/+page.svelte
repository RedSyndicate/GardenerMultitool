<script lang="ts">
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

	import Plant from '$components/plant.svelte';
	import ArrowBottomLeft from 'svelte-material-icons/ArrowBottomLeft.svelte';

	export let data: PageData;

	const dataTableStore = createDataTableStore(
		// Pass your source data here:
		data.plants,
		// Provide optional settings:
		{
			// The current search term.
			search: '',
			// The current sort key.
			sort: 'Name',
			// Paginator component settings.
			pagination: { offset: 0, limit: 5, size: 0, amounts: [5, 10, 15] }
		}
	);

	// This automatically handles search, sort, etc when the model updates.
	dataTableStore.subscribe((model) => dataTableHandler(model));

	$: dataTableStore.updateSource(data.plants);
	$: console.log(data.plants);
</script>

<svelte:head>
	<title>Gardener Multitool | Plants</title>
</svelte:head>

<div
	class="card rounded-none
			m-0 p-5 
			overflow-x-scroll overflow-y-scroll 
			space-y-1
			"
>
	<h1>Plants</h1>
	<div class="w-64">
		<input bind:value={$dataTableStore.search} type="search" placeholder="Search..." />
	</div>
	<div class="table-container space-y-1">
		<table
			class="table table-hover table-compact table-fixed"
			role="grid"
			use:tableA11y
			use:tableInteraction
		>
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
					<th data-sort="PlantId">Id</th>
					<th class="truncate" data-sort="Name" colspan="2">Name</th>
					<th class="truncate" data-sort="ScientificName" colspan="2">Scientific Name</th>
					<th class="truncate">Type</th>
					<th class="truncate">Binomial</th>
					<th class="truncate" data-sort="MinimumHardinessZone">Hardiness Zone (min)</th>
					<th class="truncate" data-sort="MaximumHardinessZone">Hardiness Zone (max)</th>
					<th class="truncate" data-sort="MinimumSoilpH">Soil pH (min)</th>
					<th class="truncate" data-sort="MaximumSoilpH">Soil pH (max)</th>
					<th class="truncate" data-sort="Height">Height</th>
					<!-- <th data-sort="Notes">Notes</th> -->
				</tr>
			</thead>
			<tbody>
				{#each $dataTableStore.filtered as row, rowIndex}
					<tr class:table-row-checked={row.dataTableChecked} aria-rowindex={rowIndex + 1}>
						<td role="gridcell" aria-colindex={1} tabindex="0">
							<input type="checkbox" bind:checked={row.dataTableChecked} />
						</td>
						<td class="truncate" role="gridcell" aria-colindex={2} tabindex="0">{row.PlantId}</td>
						<td class="truncate" role="gridcell" aria-colindex={3} tabindex="0" colspan="2"
							>{row.Name}</td
						>
						<td class="truncate " role="gridcell" aria-colindex={4} tabindex="0" colspan="2"
							>{row.ScientificName}</td
						>
						<td class="truncate" role="gridcell" aria-colindex={5} tabindex="0">{row.PlantType}</td>
						<td class="truncate" role="gridcell" aria-colindex={6} tabindex="0">{row.Binomial}</td>
						<td class="truncate " role="gridcell" aria-colindex={7} tabindex="0">
							{row.MinimumHardinessZone}
						</td>
						<td class="truncate " role="gridcell" aria-colindex={8} tabindex="0">
							{row.MaximumHardinessZone}
						</td>
						<td class="truncate " role="gridcell" aria-colindex={9} tabindex="0">
							{row.MinimumSoilpH}
						</td>
						<td class="truncate " role="gridcell" aria-colindex={10} tabindex="0">
							{row.MaximumSoilpH}
						</td>
						<td class="truncate " role="gridcell" aria-colindex={11} tabindex="0">
							{row.Height}
						</td>

						<!-- <td role="gridcell" aria-colindex={4} tabindex="0">{row.Notes}</td> -->
					</tr>
				{/each}
			</tbody>
		</table>
	</div>
	{#if $dataTableStore.pagination}
		<div>
			<Paginator
				bind:settings={$dataTableStore.pagination}
				amountText="Plants"
				buttonClasses="btn-icon variant-filled-primary"
			/>
		</div>
	{/if}
</div>
