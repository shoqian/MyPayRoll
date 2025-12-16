import { log } from '../../shared/logger';

export function getProvinceGrid () : any {
	const el = document.getElementById("provinceList") as any;
	return el?.ej2_instances?.[0];
}

export function initProvinceGrid () : void {
	const grid = getProvinceGrid();
	if (!grid) {
		log("[Province Grid] Grid not found");
		return;
	}

	log("[Province Grid] Grid initialized");
}