// Action Complete Script #TODO اسکریپت تکمیل عملیات
function actionCompleteFunc (args) {
	//غیرفعال کردن خاصیت اتوکامپلیت #TODO غیرفعال کردن اتوکامپلیت
	$("input").attr("autoComplete", "off");


	if (args.requestType === "add" || args.requestType === "beginEdit") {
		let statusVar = "systemUseruserFlagText";


		// برای غیرفعال کردن فعال یا غیرفعال بودن کاربر
		args.form.elements[statusVar].ej2_instances[0].enabled = false;

		// برای غیر فعال کردن فیلد عملیات استفاده میشود
//		args.form.elements["systemUserdeleteCommand_0_gridcommand11"].ej2_instances[0].enabled = false;
//		args.form.elements["systemUserreturnCommand_0_gridcommand12"].ej2_instances[0].enabled = false;
		
		$('#systemUserdeleteCommand_0_gridcommand11').addClass("e-disabled");
		$('#systemUserreturnCommand_0_gridcommand12').addClass("e-disabled");
		$('#systemUserreturnCommand_2_gridcommand11').addClass("e-disabled");
		$('#systemUserreturnCommand_2_gridcommand12').addClass("e-disabled");


		args.form.elements[statusVar].ej2_instances[0].refresh();
	}

	// در حالت ویرایش #TODO وضعیت دکمهها در حالت ویرایش
	if (args.requestType === "beginEdit") {
		// نوع وضعیت کاربر نباید در حالت ویرایش تغییر کند
		args.form.elements["systemUseruserTypeText"].ej2_instances[0].enabled = false;
		args.form.elements["systemUseruserTypeText"].ej2_instances[0].refresh();
		// کد ملی کاربر نباید در حالت ویرایش تغییر کند
		args.form.elements["systemUsermelliCode"].ej2_instances[0].enabled = false;
		args.form.elements["systemUsermelliCode"].ej2_instances[0].refresh();
	}

}