import { getProvinceGrid } from "./grid";
import { Province, Nullable } from "./types";
import { log } from "../../shared/logger";


const DELETE_ID = "provinceList_deleteSoft";
const RESTORE_ID = "provinceList_restore";

export function updateToolbarDeleteRestore (rowData : Nullable<Province>) {
	const grid = getProvinceGrid();
	if (!grid || !grid.toolbarModule) return;

	const ids = [DELETE_ID, RESTORE_ID];
	grid.toolbarModule.enableItems(ids, false);

	if (!rowData) return;

	if (rowData.IsDelete) {
		grid.toolbarModule.enableItems([RESTORE_ID], true);
	} else {
		grid.toolbarModule.enableItems([DELETE_ID], true);
	}
}

export function initProvinceToolbar () : void {
	updateToolbarDeleteRestore(null);

	log("[Province Toolbar] Toolbar initialized");
}