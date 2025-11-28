let DialogObj;

let commandsClickFunc =(args) => {
	if (args.commandColumn.type === 'deleteCmd') {
		DialogObj = ej.popups.DialogUtility.confirm({
			title: `<span class="e-badge e-badge-danger e-badge-pill" > غیرفعال کردن استان </span>`,
			content: `آیا از غیرفعال کردن استان <span style="color:red;font-weight:bold;" >${args.rowData.province
				}</span> مطمئن هستید ؟`,
			okButton: {
				icon: 'e-icons e',
				cssClass: 'badge bg-success okConfirm',
				click: function() { okDeleteClick(args); },
				text: "بله"
			},
			cancelButton: {
				icon: 'e- icons e-close',
				cssClass: 'badge bg-danger cancelConfirm',
				text: 'فعلا نه',
				click: cancelClick
			},
			showCloseIcon: true,
			closeOnScape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});
	}
	if (args.commandColumn.type === 'restoreCmd') {
		DialogObj = ej.popups.DialogUtility.confirm({
			title: `<span class="e-badge e-badge-success e-badge-pill" > فعال کردن استان </span>`,
			content: `آیا از فعال کردن استان <span style="color:green;font-weight:bold;" >${args.rowData.province
				} اطمینان دارید؟ `,
			okButton: {
				icon: 'e-icons e-check',
				cssClass: 'badge bg-success okConfirm',
				click: function() { okRestoreClick(args); },
				text: "بله"
			},
			cancelButton: {
				icon: 'e-icons e-close',
				cssClass: 'badge bg-danger cancelConfirm',
				text: 'فعلا نه',
				click: cancelClick
			},
			showCloseIcon: true,
			closeOnScape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});
	}

	function okDeleteClick(e) {
		let ajax = new ej.base.Ajax({
			url: window.deleteUrlProvince,
			type: "POST",
			contentType: "application/json",
			data: JSON.stringify({ value: e.rowData })
		});
		ajax.send();
		ajax.onSuccess = function(data) {
			DialogObj.hide();
			let grid = getProvinceGrid();
			grid.refresh();
		}
	}

	function okRestoreClick(e) {
		let ajax = new ej.base.Ajax({
			url: window.restoreUrlProvince,
			type: "POST",
			contentType: "application/json",
			data: JSON.stringify({ value: e.rowData })
		});
		ajax.send();
		ajax.onSuccess = (data) => {
			DialogObj.hide();
			let grid = getProvinceGrid();
			grid.refresh();
		}
	}

	function cancelClick(eArgs) {
		DialogObj.hide();
	}

}