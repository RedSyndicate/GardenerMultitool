<script lang="ts">
	import '../app.css';
	import Navigation from '$components/navbar/navigation.svelte';
</script>

<Navigation>
	<slot id="main" />
</Navigation>

<slot />
