//  Created Function Script For User Manager Page
// غیرفعال کردن خاصیت اتوکامپلیت

function CreatedFunc (args) {

	console.log(args);

	let grid = document.querySelector('#systemUser').ej2_instances[0];

	//برای کنترل مقادیر
	class customAdaptor extends ej.data.UrlAdaptor {
		processResponse (data, ds, query, xhr, request, changes) {

			// لاگ برای تشخیص دقیق شکل پاسخ
			console.log('CustomAdaptor.processResponse:', data, { ds, query, xhr, request, changes });


			if (!ej.base.isNullOrUndefined(data.action)) {
				if (data.action === "fetchGrid") {
					$("#spanAllUser").text(data.countAll);
					$("#spanActiveUser").text(data.countActive);
					$("#spanDeactivateUser").text(data.countDeactivate);
				}
				if (data.action === "repeatUser") {
					//alert("شماره ملی کاربر تکراری می باشد.");
					ej.popups.DialogUtility.alert({
						title: "خطا در اطلاعات",
						content: ` شماره ملی <span style='color:red'>  ${data.user} </span> تکراری می باشد.`,
						okButton: { text: "متوجه شدم.", cssClass: "badge bg-danger okConfirm" },
						showCloseIcon: true,
						closeOnEscape: true,
						animationSettings: { effect: 'Zoom' }
					});
				}
				if (data.action === "insert") {
					//alert("کاربر جدید با موفقیت ثبت شد.");
					ej.popups.DialogUtility.alert({
						title: "ثبت کاربر جدید",
						content: `کاربر <span style='color:green'> ${data.user} </span> با موفقیت ثبت شد`,
						okButton: { text: "خوبه", cssClass: "badge bg-success okConfirm" },
						showCloseIcon: true,
						closeOnEscape: true,
						animationSettings: { effect: 'Zoom' }
					});
				}
				if (data.action === "error") {
					//alert("در ارتباط با دیتابیس مشکلی وجود دارد.");
					ej.popups.DialogUtility.alert({
						title: "خطا در سرور",
						content: `ظاهراٌ در ارتباط مشکلی وجود دارد. مانند ${data.ErrMsg} در قسمت ${data.text}`,
						okButton: { text: "متوجه شدم.", cssClass: "badge bg-danger okConfirm" },
						showCloseIcon: true,
						closeOnEscape: true,
						animationSettings: { effect: 'Zoom' }
					});
				}
				if (data.action === "update") {
					ej.popups.DialogUtility.alert({
						title: "ویرایش اطلاعات کاربر ",
						content: `اطلاعات <span style='color:green'> ${data.user} </span> با موفقیت ویرایش شد.`,
						okButton: { text: "خوبه", cssClass: "badge bg-success okConfirm" },
						showCloseIcon: true,
						closeOnEscape: true,
						animationSettings: { effect: 'Zoom' }
					});
					grid.refresh();
				}
				if (!ej.base.isNullOrUndefined(data.data))
					return data.data;
				else
					return data;
			}
		}
	}


//	url: "/AdminArea/UserManager/FetchUserList",
//		insertUrl: "/AdminArea/UserManager/Insert",
//		updateUrl: "/AdminArea/UserManager/Update",

	//دریافت اطلاعات از دیتابیس به روش api

	grid.dataSource = new ej.data.DataManager({
		url: baseUrl,
		insertUrl: insertUrl,
		updateUrl: updateUrl,
		adaptor: new customAdaptor()
	});
}