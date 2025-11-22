function createFunc(args) {
	ej.base.enableRtl(true);

	let grid = document.getElementById('provinceList').ej2_instances[0];

	class CustomAdaptor extends ej.data.UrlAdaptor {
		processResponse(data, dt, query, xhr, request, changes) {
			console.log("Data is :", data);
			console.log("Data Table :", dt);
			console.log("Query is :", query);
			console.log("XHR is :", xhr);
			console.log("Request is :", request);
			console.log("Changes are :", changes);
			if (!ej.base.isNullOrUndefined(data.action)) {
				if (data.action==="fetchGridProvince") {
					$('#spnRowProvince').text(data.count);
				}
				switch (data.action) {
					case "insert":
						ej.popups.DialogUtility.alert({
							title: "استان جدید",
							content: `استان <span class="text-green text-bold">${data.province}</span> با موفقیت ثبت شد.`,
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
							content: `استان <span class="text-yellow text-bold">${data.province
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
						ej.popups.DialogUtility.alrte({
							title: "حذف استان",
							content: `استان <span class="text-red text-bold">${data.province
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
						break;

					case "repeat":
						ej.popups.DialogUtility.alert({
							title: "خطا در ثبت اطلاعات",
							content: `استان ${data.province} تکراری میباشد.`,
							okButton: { text: "متوجه شدم", cssClass: "btn btn-outline-danger black rounded" },
							showCloseIcon: true,
							closeOnEscape: true,
							animationSettings: { effect: 'Zoom' , }
						});
						break;

					case "error":
						ej.popups.DialogUtility.alert({
							title: "خطا در ثبت اطلاعات",
							content: "در ثبت اطلاعات خطایی رح داده لطفا بررسی کنید." + data.ErrMsg.toString(),
							okButton: { text: "متوجه شدم", cssClass: "btn btn-outline-danger black rounded" },
							showCloseIcon: true,
							closeOnEscape: true,
							animationSettings: { effect: 'Zoom' }
						});
						break;
				}
				if (!ej.base.isNullOrUndefuned(data.data)) {
					return data.data;
				} else {
					return data;
				}
			}
		}
		grid.dataSource = new ej.data.DataManager({
			url: window.baseUrlProvince,
			insertUrl: window.insertUrlProvince,
			updateUrl: window.updateUrlProvince,
			deleteUrl: window.deleteUrlProvince,
			adaptor: new CustomAdaptor()
		});
	}
}