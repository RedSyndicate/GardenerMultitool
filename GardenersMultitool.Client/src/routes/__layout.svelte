<script lang="ts">
	import '../app.css';
	import Navigation from '../components/navigation.svelte';
</script>

<Navigation>
	<slot id="main">
		<style>
			@tailwind base;
			@tailwind components;
			@tailwind utilities;
		</style>
	</slot></Navigation
>
<slot />
