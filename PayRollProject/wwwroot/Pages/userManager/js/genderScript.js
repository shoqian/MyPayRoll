
// generate dropdown for gender

let genderType;
let genderTypeObj;
let genderTypeValue = [
	{ text: "آقا", value: "1" },
	{ text: "خانم", value: "2" }
];

function createGender () {
	genderType = document.createElement('input');
	return genderType;
}

function readGender () {
	return genderTypeObj.value;
}

function destroyGender () {
	genderTypeObj.destroy();
}

function writeGender (args) {
	let genderSelected = args.rowData.gender;
	if (genderSelected === undefined) {
		genderSelected = "1";
	}

	genderTypeObj = new ej.dropdowns.DropDownList({
		dataSource: genderTypeValue,
		fields: { value: 'value', text: 'text' },
		placeholder: '- جنسیت -',
		floatLabelType: 'Never',
		value: genderSelected.toString()
	});
	genderTypeObj.appendTo(genderType);
}