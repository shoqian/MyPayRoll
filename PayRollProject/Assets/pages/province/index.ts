import { initProvinceGrid } from "./grid";
import { initProvinceToolbar } from "./toolbar";
import { wireProvinceEvents } from "./events";


export function initProvincePage () : void {
	initProvinceGrid();
	initProvinceToolbar();
	wireProvinceEvents();
}

initProvincePage();