//	Command Click Function
// okButton: { text: "خوبه", cssClass: "badge bg-success okConfirm" },
function commandClickFunc (args) {
//	console.log(`این مقادر داخلی Command Click Function هستش ${JSON.stringify(args)} ....`);



	if (args.commandColumn.type === "deleteCommand") {
		DialogObj = ej.popups.DialogUtility.confirm({
			title: "غیرفعال کردن کاربر",
			content: "آیا از غیر فعال کردن اکانت <span style='color:red'>" + args.rowData.firstName + " " + args.rowData.family + "</span> مطمئن هستید ؟",
			okButton: { icon: '', cssClass: 'badge bg-success okConfirm', click: function () { okClick(args); }, text: "بله" },
			cancelButton: {
				icon: '',
				cssClass: 'badge bg-danger cancelConfirm',
				text: "فعلا نه",
				click: cancelClick
			},
			showCloseIcon: true,
			closeOnEscape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});
	}

//	url: "/AdminArea/UserManager/DeactivateUser",

	function okClick (e) {
		let ajax = new ej.base.Ajax({
			url: deactivateUrl,
			type: "POST",
			contentType: "application/json",
			data: JSON.stringify({ value: e.rowData })
		});
		ajax.send();
		ajax.onSuccess = function (data) {
			DialogObj.hide();
			let grid = document.querySelector('#systemUser').ej2_instances[0];
			grid.refresh();
		};
	}


	//////

	if (args.commandColumn.type === "returnCommand") {
		DialogObj = ej.popups.DialogUtility.confirm({
			title: "فعال کردن کاربر",
			content: "آیا از فعال کردن اکانت <span style='color:red'>" + args.rowData.firstName + " " + args.rowData.family + "</span> مطمئن هستید ؟",
			okButton: { icon: '', cssClass: 'badge bg-success okConfirm', click: function () { okActiveClick(args) }, text: "بله" },
			cancelButton: {
				icon: '',
				cssClass: 'badge bg-danger cancelConfirm',
				text: "فعلا نه",
				click: cancelClick
			},
			showCloseIcon: true,
			closeOnEscape: true,
			animationSettings: { effect: "Zoom", duration: 700 }
		});
	}

//	url: "/AdminArea/UserManager/ActiveUser",

	function okActiveClick (e) {
		let ajax = new ej.base.Ajax({
			url: activeUrl,
			type: "POST",
			contentType: "application/json",
			data: JSON.stringify({ value: e.rowData })
		});
		ajax.send();
		ajax.onSuccess = function (data) {
			DialogObj.hide();
			let grid = document.querySelector('#systemUser').ej2_instances[0];
			grid.refresh();
		};
	}

	function cancelClick () {
		DialogObj.hide();
	}
}