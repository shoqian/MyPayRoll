let toolbarClickFunc = (args) => {
	let grid = getProvinceGrid();
	let selectedRecords = grid.getSelectedRecords();

	if (!selectedRecords) {
		ej.popups.DialogUtility.alert({
			title: `<span class="e-badge e-badge-danger e-badge-pill" > هشدار </span>`,
			content: `لطفا یک رکورد را انتخاب نمایید.`,
			okButton: { icon: 'e-icons e-check', cssClass: 'badge bg-success okConfirm', text: "باشه" },
			showCloseIcon: true,
			closeOnEscape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});

		updateToolbarDeleteRestor(null);

		return;
	}

		// console.log("Selected record :",selectedRecords);

		let rowData = selectedRecords[0];

		updateToolbarDeleteRestor(rowData);

		switch (args.item.id) {
			case 'provinceList_deleteSoft':
				handleProvinceSoftDelete('delete', rowData);
				break;

			case 'provinceList_restore':
				handleProvinceSoftDelete('restore', rowData);
				break;

		}
}