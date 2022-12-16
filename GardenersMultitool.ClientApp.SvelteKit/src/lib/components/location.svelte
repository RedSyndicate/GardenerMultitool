<script lang="ts">
	export let location;
</script>

<div
	class="
	flex p-6 bg-gray-100 text-gray-800 
	text-center rounded-md 
	shadow-sm hover:shadow-md flex-col items-center"
>
	<img
		class="h-40"
		src={`https://via.placeholder.com/460x640/333333/cccccc?text=${location.name.value}`}
		alt={location.name.value}
	/>
	{location.name.value}
</div>

<slot />
