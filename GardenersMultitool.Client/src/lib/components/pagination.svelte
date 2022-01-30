<script lang="ts">
	export let total: number;
	export let perPage: number;
	export let page: number;
</script>

<div class="my-1 btn-group justify-center">
	<button class="btn btn-sm">Previous</button>
	{#each Array(total / perPage) as _, index}
		{#if page == index + 1}
			<button class="btn btn-sm btn-active" />
		{:else if index < 5}
			<button class="btn btn-sm">{index + 1}</button>
		{/if}
	{/each}
	{#if Array(total / perPage).length > 5}
		<button class="btn btn-sm btn-disabled" />
	{/if}
	<button class="btn btn-sm">Next</button>
</div>
