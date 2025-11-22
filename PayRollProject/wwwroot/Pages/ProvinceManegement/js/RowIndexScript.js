let indexNumber = (args) => {
	let grid = document.getElementById('provinceList').ej2_instances[0];
	if (args.row) {
		let rowIndex = parseInt(args.row.getAttribute('area-rowIndex'));
		let currentPageNumber = grid.pageSettings.currentPage;
		let pageSize = grid.pageSettings.pageSize;
		let startIndex = (currentPageNumber - 1) * pageSize;
		args.row.querySelector('.e-rowcell').innerHTML = (startIndex + rowIndex).toString();
	}
}