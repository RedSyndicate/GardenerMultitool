<script>
    import { page } from '$app/stores';
	import { dev } from '$app/environment';
</script>

<svelte:head>
	<title>{$page.status} | {$page.error?.message}</title>
</svelte:head>

<div class="col-md-9">
	<h1>{$page.status}</h1>

	{#if dev && $page.error?.message}
		<pre>{$page.error.message}</pre>
	{/if}
</div>

<style scoped>
	h1,
	p {
		margin: 0 auto;
	}
	h1 {
		font-size: 2.8em;
		font-weight: 700;
		margin: 0 0 0.5em 0;
	}
	p {
		margin: 1em auto;
	}
	@media (min-width: 480px) {
		h1 {
			font-size: 4em;
		}
	}
</style>
