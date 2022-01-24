<script lang="ts">
	import SvgIcon from '@jamescoyle/svelte-icon';
	import { mdiMenu, mdiAccount, mdiFolder } from '@mdi/js';
	import { onMount } from 'svelte';
	import { fly } from 'svelte/transition';
	import { backInOut } from 'svelte/easing';

	let visible;

	onMount(() => {
		let drawer: any = document.getElementById('nav-drawer');
		let menu: any = document.getElementById('nav-menu');

		drawer.addEventListener('change', () => {
			!drawer.checked ? menu.classList.add('hidden') : menu.classList.remove('hidden');
		});

		menu.addEventListener('click', () => {
			drawer.click();
		});
	});
</script>

<div class="shadow-lg drawer mb-2 p-0 bg-base-200">
	<input id="nav-drawer" type="checkbox" class="drawer-toggle" bind:checked={visible} />
	<div class="flex flex-col drawer-content">
		<div class="w-full navbar">
			<div class="flex-none">
				<label for="nav-drawer" class="btn btn-square btn-ghost">
					<SvgIcon type="mdi" path={mdiMenu} />
				</label>
			</div>
		</div>
	</div>
	{#if visible}
		<div
			id="nav-menu"
			transition:fly={{ x: -200, duration: 400, easing: backInOut }}
			class="drawer-side hidden"
		>
			<label for="nav-drawer" class="drawer-overlay" />
			<ul class="m-0 p-4 menu w-80 bg-base-100 h-screen">
				<li>
					<a href="/">Dashboard</a>
				</li>
				<li>
					<a href="/plants">Plants</a>
				</li>
				<li>
					<a href="/locations">Locations</a>
				</li>
			</ul>
		</div>
	{/if}
</div>
