<script lang="ts">
	import '@skeletonlabs/skeleton/themes/theme-seafoam.css';
	import '@skeletonlabs/skeleton/styles/all.css';
	import '../app.postcss';

	import { writable, type Writable } from 'svelte/store';
	import { AppShell, AppRail, AppRailTile, AppBar } from '@skeletonlabs/skeleton';
	import { page } from '$app/stores';

	import Navigation from '$components/navbar/navigation.svelte';
	import Github from 'svelte-material-icons/Github.svelte';
	import Tree from 'svelte-material-icons/Tree.svelte';
	import ViewDashboard from 'svelte-material-icons/ViewDashboard.svelte';
	import LandPlots from 'svelte-material-icons/LandPlotsCircle.svelte';
	import Menu from 'svelte-material-icons/Menu.svelte';
	import Close from 'svelte-material-icons/Close.svelte';
	import { slide } from 'svelte/transition';

	const storeValue: Writable<string> = writable('/');

	let isMenuActive = false;

	$: $storeValue, $page.route.id;
</script>

<AppShell slotPageContent="bg-surface-50" slotSidebarLeft="bg-primary-50">
	<svelte:fragment slot="header">
		<AppBar class="bg-surface-100">
			<svelte:fragment slot="lead">
				<div class="block sm:hidden">
					<button
						class="btn-icon variant-soft-surface w-full p-3"
						on:click={() => {
							isMenuActive = !isMenuActive;
						}}
					>
						{#if isMenuActive}
							<Close size={24} />
						{:else}
							<Menu size={24} />
						{/if}
					</button>
				</div>
				<div class="hidden sm:block">
					<img src="favicon.png" alt="icon" class="rounded-full p-1 variant-filled-primary" />
				</div>
			</svelte:fragment>

			<div class="w-fit mx-auto hidden sm:block">
				<div class="space-x-1">
					<a class="btn variant-filled-primary" href="/">Home</a>
					<a class="btn variant-filled-primary" href="/about"> About </a>
					<a class="btn variant-filled-primary" href="/community"> Community </a>
				</div>
			</div>

			<svelte:fragment slot="trail">
				<div class="space-x-1">
					<a class="btn variant-filled-primary" href="/">Login</a>
					<a class="btn variant-filled-primary" href="/about">Signup</a>
				</div>
			</svelte:fragment>
		</AppBar>
		{#if isMenuActive}
			<div transition:slide>
				<div class="menu-tl space-y-0 mt-0 mb-1">
					<a class="btn variant-filled-primary w-full rounded-none" href="/">Home</a>
					<a class="btn variant-filled-primary w-full rounded-none" href="/about"> About </a>
					<a class="btn variant-filled-primary w-full rounded-none" href="/community">
						Community
					</a>
				</div>
			</div>
		{/if}
	</svelte:fragment>

	<svelte:fragment slot="sidebarLeft">
		<AppRail selected={storeValue} class="">
			<svelte:fragment slot="lead">
				<AppRailTile
					class={$page.route.id === '/' ? 'bg-primary-300' : ''}
					tag="a"
					href="/"
					label="Dashboard"
					regionIcon="w-fit mx-auto"
					value="/"
				>
					<ViewDashboard size={36} />
				</AppRailTile>
			</svelte:fragment>
			<AppRailTile
				class={$page.route.id === '/plants' ? 'bg-primary-300' : ''}
				tag="a"
				href="/plants"
				value="/plants"
				label="Plants"
				regionIcon="w-fit mx-auto"
			>
				<Tree size={36} />
			</AppRailTile>
			<AppRailTile
				class={$page.route.id === '/locations' ? 'bg-primary-300' : ''}
				tag="a"
				href="/locations"
				value="/locations"
				label="Locations"
				regionIcon="w-fit mx-auto"
			>
				<LandPlots size={36} />
			</AppRailTile>
			<svelte:fragment slot="trail">
				<AppRailTile tag="a" href="https://github.com" label="src" regionIcon="w-fit mx-auto">
					<Github size={36} />
				</AppRailTile>
			</svelte:fragment>
		</AppRail>
	</svelte:fragment>

	<slot />

	<svelte:fragment slot="footer">
		<footer />
	</svelte:fragment>
</AppShell>
