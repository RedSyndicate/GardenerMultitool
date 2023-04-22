<script lang="ts">
	import type { PageData } from './$types';
	import { plants } from '$lib/stores/plant';
	import { DataHandler, Datatable, Th, ThFilter } from '@vincjo/datatables';
	export let data: PageData;
	plants.set(data.plants);

	const handler = new DataHandler($plants, { rowsPerPage: 10 });
	const rows = handler.getRows();
	handler.sortAsc("Id")
	const selected = handler.getSelected();
	const isAllSelected = handler.isAllSelected();

	$: $plants, handler.setRows($plants);
</script>

<svelte:head>
	<title>Gardener Multitool | Plants</title>
</svelte:head>

<div
	class="card rounded-none
			m-0 p-5
			space-y-1
			"
>
	<h1>Plants</h1>
	<div class="table-container space-y-1">
		<Datatable {handler}>
			<table class="table table-hover table-compact table-fixed" role="grid">
				<thead>
					<tr>
						<th class="w-1">
							<input
								type="checkbox"
								on:click={() => handler.selectAll({ selectBy: 'Id' })}
								checked={$isAllSelected}
							/>
						</th>
						<Th {handler} orderBy="PlantId">Id</Th>
						<Th {handler} orderBy="Name">Name</Th>
						<Th {handler} orderBy="ScientificName">Scientific Name</Th>
						<Th {handler}>Type</Th>
						<Th {handler}>Binomial</Th>
						<Th {handler}>Hardiness Zone (min)</Th>
						<Th {handler}>Hardiness Zone (max)</Th>
						<Th {handler}>Soil pH (min)</Th>
						<Th {handler}>Soil pH (max)</Th>
						<Th {handler}>Height</Th>
						<!-- <th data-sort="Notes">Notes</th> -->
					</tr>
					<tr>
						<th />
						<ThFilter {handler} filterBy="PlantId" />
						<ThFilter {handler} filterBy="Name" />
						<ThFilter {handler} filterBy="ScientificName" />
						<ThFilter {handler} filterBy="PlantType" />
						<ThFilter {handler} filterBy="Binomial" />
						<ThFilter {handler} filterBy="MinimumHardinessZone" />
						<ThFilter {handler} filterBy="MaximumHardinessZone" />
						<ThFilter {handler} filterBy="MinimumSoilpH" />
						<ThFilter {handler} filterBy="MaximumSoilpH" />
						<ThFilter {handler} filterBy="Height" />
					</tr>
				</thead>
				<tbody>
					{#each $rows as row, rowIndex}
						<tr class:active={$selected.includes(row.Id)} aria-rowindex={rowIndex + 1}>
							<td class="selection">
								<input
									type="checkbox"
									on:click={() => handler.select(row.Id)}
									checked={$selected.includes(row.Id)}
								/>
							</td>
							<td class="truncate" role="gridcell" aria-colindex={2} tabindex="0">
								{row.PlantId}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={3} tabindex="0">
								{row.Name}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={4} tabindex="0">
								{row.ScientificName}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={5} tabindex="0">
								{row.PlantType}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={6} tabindex="0">
								{row.Binomial}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={7} tabindex="0">
								{row.MinimumHardinessZone}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={8} tabindex="0">
								{row.MaximumHardinessZone}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={9} tabindex="0">
								{row.MinimumSoilpH}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={10} tabindex="0">
								{row.MaximumSoilpH}
							</td>
							<td class="truncate" role="gridcell" aria-colindex={11} tabindex="0">
								{row.Height}
							</td>
						</tr>
					{/each}
				</tbody>
			</table>
		</Datatable>
	</div>
</div>
