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

	import LandPlotsCircle from 'svelte-material-icons/LandPlotsCircle.svelte';
	import Plus from 'svelte-material-icons/Plus.svelte';

	export let data: PageData;

	const dataTableStore = createDataTableStore(
		// Pass your source data here:
		data.locations,
		// Provide optional settings:
		{
			// The current search term.
			search: '',
			// The current sort key.
			sort: '',
			// Paginator component settings.
			pagination: { offset: 0, limit: 5, size: 0, amounts: [5, 10, 15] }
		}
	);

	// This automatically handles search, sort, etc when the model updates.
	dataTableStore.subscribe((model) => dataTableHandler(model));

	$: dataTableStore.updateSource(data.locations);
</script>

<svelte:head>
	<title>Gardener Multitool | Locations</title>
</svelte:head>

<div
	class="card rounded-none 
		m-0 p-5
		overflow-x-scroll overflow-y-scroll 
		space-y-1
		"
>
	<div class="table-container space-y-1">
		<h1>Locations</h1>
		<div class="w-64 {$dataTableStore.filtered.length ? '' : 'hidden'}">
			<input bind:value={$dataTableStore.search} type="search" placeholder="Search..." />
		</div>
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
					<th>Id</th>
					<th>Name</th>
					<th>HardinessZone</th>
					<th>pH</th>
					<th>Area</th>
					<th>Compaction</th>
					<!-- <th data-sort="Notes">Notes</th> -->
				</tr>
			</thead>
			<tbody>
				{#if !$dataTableStore.filtered.length}
					<tr class="text-center">
						<td colspan="7">
							<div class="mx-auto w-fit flex items-center">
								<LandPlotsCircle size={48} />
								<Plus size={24} />
							</div>
							<h3 class="mt-2 text-sm font-medium text-gray-900">No locations</h3>
							<p class="mt-1 text-sm text-gray-500">Get started by creating a new location.</p>
							<div class="mt-6">
								<button
									type="button"
									class="inline-flex items-center rounded-md border border-transparent bg-indigo-600 px-4 py-2 text-sm font-medium text-white shadow-sm hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2"
								>
									<Plus />
									New Location
								</button>
							</div>
						</td>
					</tr>
				{/if}

				{#each $dataTableStore.filtered as row, rowIndex}
					<tr class:table-row-checked={row.dataTableChecked} aria-rowindex={rowIndex + 1}>
						<td role="gridcell" aria-colindex={1} tabindex="0">
							<input type="checkbox" bind:checked={row.dataTableChecked} />
						</td>
						<td role="gridcell" aria-colindex={2} tabindex="0">{row.Id}</td>
						<td role="gridcell" aria-colindex={3} tabindex="0">{row.Name}</td>
						<td role="gridcell" aria-colindex={4} tabindex="0">{row.HardinessZone}</td>
						<td role="gridcell" aria-colindex={5} tabindex="0">{row.pH}</td>
						<td role="gridcell" aria-colindex={6} tabindex="0">{row.Area}</td>
						<td role="gridcell" aria-colindex={7} tabindex="0">{row.Compaction}</td>
					</tr>
				{/each}
			</tbody>
		</table>
		{#if $dataTableStore.pagination && $dataTableStore.filtered.length}
			<div class="">
				<Paginator
					bind:settings={$dataTableStore.pagination}
					amountText="Locations"
					buttonClasses="btn-icon variant-filled-primary"
				/>
			</div>
		{/if}
	</div>
</div>
