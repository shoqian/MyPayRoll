let handleProvinceSoftDelete = (action, rowData) => {
	let grid = getProvinceGrid();

	if (!rowData) {
		ej.popups.DialigUtility.alert({
			title: "انتخاب استان",
			content: "لطفا یک استان را انتخاب کنید",
			okButton: {
				test: "باشه",
				cssClass: "btn btn-outline-warning"
			},
			showCloseIcon: true,
			closeOnScape: true,
			animationSetting: {
				effect: "Zoom",
				duration: 700
			}
		});
	}
	let url = action === "delete" ? rowData = true : action === 'restore' ? rowData = false : null;

	let rowIndex = grid.getRowIndexByPrimaryKey(rowData.provinceId);
	grid.updateRow(rowIndex, rowData);

}