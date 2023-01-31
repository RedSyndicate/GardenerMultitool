<script lang="ts">
	import '@skeletonlabs/skeleton/themes/theme-seafoam.css';
	import '@skeletonlabs/skeleton/styles/all.css';
	import '../app.postcss';

	import { AppShell, AppRail, AppRailTile, AppBar } from '@skeletonlabs/skeleton';

	import Navigation from '$components/navbar/navigation.svelte';
</script>

<AppShell slotPageContent="overflow-x-scroll overflow-y-scroll" slotHeader="">
	<svelte:fragment slot="header">
		<AppBar class="justify-between">
			<svelte:fragment slot="lead">
				<img src="$assets/favicon.png" />
			</svelte:fragment>
			(center)
			<svelte:fragment slot="trail">(trail)</svelte:fragment>
		</AppBar>
	</svelte:fragment>

	<svelte:fragment slot="sidebarLeft">
		<AppRail>
			<svelte:fragment slot="lead">
				<AppRailTile>(icon)</AppRailTile>
			</svelte:fragment>
			<svelte:fragment slot="trail">
				<AppRailTile>(icon)</AppRailTile>
			</svelte:fragment>
		</AppRail>
	</svelte:fragment>

	<slot />

	<svelte:fragment slot="footer">
		<footer />
	</svelte:fragment>
</AppShell>
