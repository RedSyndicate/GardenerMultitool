import { writable } from 'svelte/store';

export const plants = writable([]);

const fetchPlants = async () => {
	const url = 'https://localhost:44363/Plant/annuals';
	await fetch(url).then(async (res) => {
		const data = await res.json();
		console.log(data[0]);
		plants.set(data);
	});
};

fetchPlants();
