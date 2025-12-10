function getProvinceGrid() {
	return document.getElementById('provinceList').ej2_instances[0];
}

function styleBuiltInButtons() {
	setTimeout(function() {
			const toolbar = document.querySelector('.e-toolbar');

			if (toolbar) {
				const addBtn = toolbar.querySelector('[aria-label="اضافه کردن"]') ||
					toolbar.querySelector('#provinceList_add');

				let overBtn = toolbar.querySelector('[title="اضافه کردن"]');

				if (addBtn) {
					overBtn.classList.add('btn', 'btn-outline-success', 'e-success');
					addBtn.classList.add('btn', 'btn-outline-success', 'e-success');

					addBtn.setAttribute('title', "افزودن استان جدید");
				}

				const editBtn = toolbar.querySelector('#provinceList_edit');
				overBtn = toolbar.querySelector('[title="ویرایش کنید"]');
				if (editBtn) {
					overBtn.classList.add('btn', 'btn-outline-primary', 'e-primary');
					editBtn.classList.add('btn', 'btn-outline-primary', 'e-primary');

					editBtn.setAttribute('title', "ویرایش استان مورد نظر");
				}

				const updateBtn = toolbar.querySelector('#provinceList_update');
				overBtn = toolbar.querySelector('[title="به روز رسانی"]');
				if (updateBtn) {
					updateBtn.classList.add('btn', 'btn-outline-warning', 'e-warning');
					overBtn.classList.add('btn', 'btn-outline-warning', 'e-warning');

					updateBtn.setAttribute('title', "جهت بروزرسانی استان مورد نظر می باشد");
				}

				const cancelBtn = toolbar.querySelector('#provinceList_cancel');
				overBtn = toolbar.querySelector('[title="لغو"]');
				if (cancelBtn) {
					cancelBtn.classList.add('btn', 'btn-outline-secondary', 'e-secondary');
					overBtn.classList.add('btn', 'btn-outline-secondary', 'e-secondary');

					cancelBtn.setAttribute('title', "جهت لغو درخواست جاری استفاده میشه");
				}

				const centerToolbar = toolbar.querySelector('.e-toolbar-center');
				if (centerToolbar) {

					centerToolbar.innerHTML =
						`<span class='e-badage e-badge-primary' style="font-size:20px;font-weight:bolder;">جدول استان</span>`;
				}

			}
		},
		100);
}

function handleProvinceSoftDelete(action, rowData) {
	let grid = getProvinceGrid();

	if (!rowData) {
		ej.popups.DialogUtility.alert({
			title: `<span class="e-badge e-badge-warning"> هشدار </span> در حذف اطلاعات`,
			content: `لطفا یک استان را برای حذف انتخاب کنید.`,
			okButton: {
				text: "متوجه شدم",
				cssClass: "btn btn-outline-danger black rounded",
				icon: 'e-icons e-circle-info'
			},
			showCloseIcon: true,
			closeOnEscape: true,
			animationSettings: { effect: 'Zoom' }
		});
		return;
	}

	if (action === 'delete') {
		rowData.isDelete = true;
	} else if (action === 'restore') {
		rowData.isDelete = false;
	}

	let rowIndex = grid.getRowIndexByPrimaryKey(rowData.ProvinceId);

	grid.updateRow(rowIndex, rowData);

}

class ProvinceCustomAdaptor extends ej.data.UrlAdaptor {
	processResponse (data, dt, query, xhr, request, changes) {

		if (!ej.base.isNullOrUndefined(data.action)) {
			const grid = getProvinceGrid();
			if (data.action === "fetchGridProvince") {
				$('#spnRowProvince').text(data.count);
				$('#spnRowDelProvince').text(data.countDelete);
				$('#spnRowAllProvince').text(data.countAll);
			}
			switch (data.action) {
			case "insert":
				ej.popups.DialogUtility.alert({
					title: "استان جدید",
					content: `استان <span style="color:greenyellow;font-weight:bold;">${data.province
						}</span> با موفقیت ثبت شد.`,
					okButton: { text: 'عالیه', cssClass: "badge btn btn-outline-success green rounded okConfirm" },
					showCloseIcon: true,
					width: '400px',
					height: '200px',
					isModal: true,
					closeOnEscape: true,
					position: { X: 'center', Y: 'center' },
					animationSettings: { effect: 'Zoom', deration: 700 }
				});
				break;

			case "update":
				ej.popups.DialogUtility.alert({
					title: "ویرایش استان",
					content: `استان <span style=\"color:yellow;font-weight:bold;\" >${data.province
						}</span> با موفقیت ویرایش شد.`,
					okButton: { text: 'عالیه', cssClass: "badge btn btn-outline-warning yellow rounded okConfirm" },
					showCloseIcon: true,
					width: '400px',
					height: '200px',
					isModal: true,
					closeOnEscape: true,
					position: { X: 'center', Y: 'center' },
					animationSettings: { effect: 'Zoom', deration: 700 }
				});
				grid.refresh();
				break;

			case "delete":
				ej.popups.DialogUtility.alert({
					title: "حذف استان",
					content: `استان <span style=\"color:#EF5A26;font-weight:bold;\">${data.province
						}</span> با موفقیت حذف شد.`,
					okButton: { text: "عجب", cssClass: "badge btn btn-outline-danger red rounded okConfirm" },
					showCloseIcon: true,
					width: '400px',
					height: '200px',
					isModal: true,
					closeOnEscape: true,
					position: { X: 'center', Y: 'center' },
					animationSettings: { effect: 'Zoom', deration: 700 }
				});
				grid.refresh();
				break;

			case "repeat":
				ej.popups.DialogUtility.alert({
					title: `<span class=\"e-badge e-badge-warning\"> خطا </span> در ثبت اطلاعات`,
					content: `استان <span class=\"e-badge e-badge-primary\" style=\"font-weight:bold;\"> ${data.province
						} </span> تکراری میباشد.`,
					okButton: {
						text: "متوجه شدم",
						cssClass: "btn btn-outline-danger black rounded",
						icon: 'e-icons e-circle-info'
					},
					showCloseIcon: true,
					width: '400px',
					height: '200px',
					isModal: true,
					closeOnEscape: true,
					position: { X: 'center', Y: 'center' },
					animationSettings: { effect: 'Zoom', deration: 1500 }
				});
				grid.refresh();
				break;

			case "error":
				ej.popups.DialogUtility.alert({
					title: `<span style=\"color:white;background-color:red;\" > خطا </span> در ثبت اطلاعات`,
					content:
						`در ثبت اطلاعات خطایی رح داده لطفا بررسی کنید. <span style=\"color:red;font-weight:bold;\">${
							data.ErrMsg.toString()}</span>`,
					okButton: { text: "متوجه شدم", cssClass: "btn btn-outline-danger black rounded" },
					showCloseIcon: true,
					closeOnEscape: true,
					animationSettings: { effect: 'Zoom' }
				});
				grid.refresh();
				break;
			}
			if (!ej.base.isNullOrUndefined(data.data)) {
				return data.data;
			} else {
				return data;
			}
		}
	}
}

function getProvinceUrl(mode) {
	if (mode) {
		return `${window.baseUrlProvince}?mode=${mode}`;
	}
	return window.baseUrlProvince;
}

function createProvinceDataManager(mode) {
	return new ej.data.DataManager({
		url: getProvinceUrl(mode),
		insertUrl: window.insertUrlProvince,
		updateUrl: window.updateUrlProvince,
		removeUrl: window.deleteUrlProvince,
		adaptor: new ProvinceCustomAdaptor()
	});
}

function updateProvinceList(mode) {
	let grid = getProvinceGrid();
	grid.dataSource = createProvinceDataManager(mode);
	grid.refresh();
}

function createdFunc(args) {
	ej.base.enableRtl(true);

	updateProvinceList('');

	styleBuiltInButtons();

	updateToolbarDeleteRestor(null);
}

function updateToolbarDeleteRestor(rowData) {
	let grid = getProvinceGrid();

	if (!grid || !grid.toolbarModule) return;

	let ids = ['provinceList_deleteSoft', 'provinceList_restore'];

	// پیش فرض غیر فعال کردن هر دو دکمه
	grid.toolbarModule.enableItems(ids, false);

	if (!rowData) return;

	let isDelete = rowData.IsDelete;
	if (typeof isDelete === 'undefined') {
		isDelete = rowData.isDelete;
	}

	//console.log(select, isDelete);


	if (isDelete) {
		grid.toolbarModule.enableItems(['provinceList_restore'], true);
	} else {
		grid.toolbarModule.enableItems(['provinceList_deleteSoft'], true);
	}

}