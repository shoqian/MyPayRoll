import { getProvinceGrid } from "./grid";
import { Province } from "./types";
import { log } from "../../shared/logger";
import { updateToolbarDeleteRestore } from "./toolbar";

export function wireProvinceEvents () : void {
	const grid = getProvinceGrid();
	if (!grid) {
		log("[Province Events] Grid not found");
		return;
	}

	grid.rowSelected = (args : any) : void => {
		const selected : Province[] = grid.getSelectedRecords() || [];
		if (selected.length > 0) {
			updateToolbarDeleteRestore(selected[0]);
		} else {
			updateToolbarDeleteRestore(null);
		}
	};

	grid.rowDeselected = (args : any) : void => {
		const selected : Province[] = grid.getDeselectedRecords() || [];
		if (selected.length > 0) {
			updateToolbarDeleteRestore(selected[0]);
		} else {
			updateToolbarDeleteRestore(null);
		}
	};
	log("[Province Events] Events wired");
}
