
// Region Row Data Bound Event

function rowDataBoundFunc(argsDataBound) {
//	console.log(`This Event For RowDataBoundFunc Start `);
//	console.log(argsDataBound);
//	console.log(`This Event For RowDataBoundFunc End `);


//	console.log(JSON.stringify(argsDataBound));

	// مشخض کردن ردیف‌های غیرفعال
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

			argsDataBound.row.setAttribute('title', "هرکاربری که فعال باشه نمی تونه خودش رو غیر فعال کنه");
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

// برای تعریف ردیف جدول
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

//function rowDataBoundFunc(args) {
//	console.log(args);
//	if (args.data.userFlag === "2") {
//		args.row.bgColor = '#efcdc3';
//	}
//}