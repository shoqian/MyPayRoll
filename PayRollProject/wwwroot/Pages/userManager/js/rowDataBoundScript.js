
// Region Row Data Bound Event #TODO بهینه‌سازی کد رویداد ردیف داده شده
function rowDataBoundFunc(argsDataBound) {

	// #TODO مشخض کردن ردیف‌های کاربر غیرفعال
	if (argsDataBound && argsDataBound.row) {
		if (argsDataBound.data && argsDataBound.data.userFlag === 2) {
			// Add the deactivate class when userFlag === 2
			argsDataBound.row.classList.add('deactivate');
		} else {
			// Ensure class is removed for active rows (important when paging or re-rendering)
			argsDataBound.row.classList.remove('deactivate');
		}

		if (argsDataBound.data && argsDataBound.data.id === window.currentUserId) {
			argsDataBound.row.classList.add("currentUser");
			argsDataBound.row.classList.add('e-disabled');

			argsDataBound.row.setAttribute('title', "هر کاربری که فعال باشه نمی تونه خودش رو غیر فعال کنه");
			argsDataBound.row.dataset.userId = window.currentUserId;
		} 

	}

	indexRow(argsDataBound);

}

// برای تعریف ردیف جدول
let indexNumber = (args) => {
	let grid = document.getElementById('systemUser').ej2_instances[0];
	if (args.row) {
		let rowIndex = parseInt(args.row.getAttribute('aria-rowIndex'));
		let currentPageNumber = grid.pageSettings.currentPage;
		let pageSize = grid.pageSettings.pageSize;
		let startIndex = (currentPageNumber - 1) * pageSize;
		args.row.querySelector('.e-rowcell').innerHTML = (startIndex + rowIndex).toString();
	}
}

// برای تعریف ردیف جدول #TODO بهینه‌سازی کد تعریف ردیف جدول
function indexRow(e) {
	let grid = document.getElementById('systemUser').ej2_instances[0];
	//			if (grid.columns[0].headerText === "ردیف") {
	if (e.row) {
		let rowIndex = parseInt(e.row.getAttribute('aria-rowIndex'));
		let currentPageNumber = grid.pageSettings.currentPage;
		let pageSize = grid.pageSettings.pageSize;
		let startIndex = (currentPageNumber - 1) * pageSize;
		e.row.cells[0].innerHTML = (startIndex + rowIndex);
	}
}
