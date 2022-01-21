import Plant from '../../src/components/plant.svelte';
import { mount } from 'cypress-svelte-unit-test';
import { SvelteComponent } from 'svelte';

describe('plant component', () => {
	it('should contain the properties of a plant', () => {
		mount(Plant);
	});
});
