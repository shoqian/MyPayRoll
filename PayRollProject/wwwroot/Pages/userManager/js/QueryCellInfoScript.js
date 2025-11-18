//	Query Cell Info
function cellInfoFunc(args) {

	var isCurrentUser = window.currentUserId && args.data.id === window.currentUserId;

//	if (isCurrentUser) {
//		$(args.row).classList.add("currentUser");
//		$(args.row).classList.add("e-disabled");
//
//		$(args.row).attr("title", "کاربر جاری سیستم - امکان ویرایش خودش را ندارد");
//
////		$(args.row).prop("title", "کاربر جاری سیستم - امکان ویرایش خودش را ندارد");
//	}

	if (args.column["headerText"] === "عملیات") {
		if (isCurrentUser) {

			let deleteBtn = $(args.cell).find('.btnDelete')[0];
			let returnBtn = $(args.cell).find('.btnReturn')[0];

			if (deleteBtn) {
				deleteBtn.disabled = true;
				deleteBtn.style.opacity = "0.4";
				deleteBtn.title = "غیره فعال کردن برای کاربر جاری";
			}

			if (returnBtn) {
				returnBtn.disabled = true;
				returnBtn.style.opacity = "0.4";
				returnBtn.title = "غیره فعال کردن برای کاربر جاری";
			}

			
//			$(args.row).addClass("currentUser e-disabled");
		} else {
			if (args.data["userFlag"] === 2) {
				$(args.cell).find('.btnDelete')[0].hidden = true;
				$(args.cell).find('.btnReturn')[0].title = "فعال کردن";

			} else if (args.data["userFlag"] === 1) {

				$(args.cell).find('.btnReturn')[0].hidden = true;
				$(args.cell).find('.btnDelete')[0].title = "غیرفعال کردن";
			}
		}
	}

}