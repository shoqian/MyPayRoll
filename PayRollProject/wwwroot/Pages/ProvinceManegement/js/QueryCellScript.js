function QueryCellFunc(args) {
	console.log(args);

	if (args.column["headerText"] === "عملیات" && args.data["isDelete"] === true) {
		$(args.cell).find('.btnDelete')[0].classList.add('e-hide');
		$(args.cell).find('.btnRestore')[0].classList.remove('e-hide');
	}
	if (args.column["headerText"] === "عملیات" && args.data["isDelete"] === false) {
		$(args.cell).find('.btnRestore')[0].classList.add('e-hide');
		$(args.cell).find('.btnDelete')[0].classList.remove('e-hide');
	}
}