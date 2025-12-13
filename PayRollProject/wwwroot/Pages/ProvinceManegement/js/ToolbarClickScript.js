let toolbarClickFunc = (args) => {
	let grid = getProvinceGrid();
	let selectedRecords = grid.getSelectedRecords() || [];

	if (selectedRecords.length > 0) {
		updateToolbarDeleteRestore(selectedRecords[0]);
	} else {
		updateToolbarDeleteRestore(null);
		ej.popups.DialogUtility.alert({
			title: `<span class="e-badge e-badge-danger e-badge-pill" > هشدار </span>`,
			content: `لطفا یک رکورد را انتخاب نمایید.`,
			okButton: { icon: 'e-icons e-check', cssClass: 'badge bg-success okConfirm', text: "باشه" },
			showCloseIcon: true,
			closeOnEscape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});
		return;
	}

	

	let rowData = selectedRecords[0];

	// #TODO log rowData ---
	console.log("Address File => \\wwwroot\\Pages\\ProvinceManagement\\js\\ToolbarClickScript.js ");
	console.log("Method is => toolbarClickFunc()");
	console.log(rowData);

	updateToolbarDeleteRestore(rowData);

	switch (args.item.id) {
	case 'provinceList_deleteSoft':
		handleProvinceSoftDelete('delete', rowData);
		break;

	case 'provinceList_restore':
		handleProvinceSoftDelete('restore', rowData);
		break;

	}
}