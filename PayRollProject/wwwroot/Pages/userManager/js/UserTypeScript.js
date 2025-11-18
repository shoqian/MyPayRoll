// <!-- generate dropdown for userType -->

let userTypeVal;
let userTypeObj;
let userTypeValue = [
	{ text: "ادمین", value: "1" },
	{ text: "کاربر", value: "2" }
];

function createUserType () {
	userTypeVal = document.createElement('input');
	return userTypeVal;
}

function readUserType () {
	return userTypeObj.value;
}

function destroyUserType () {
	userTypeObj.destroy();
}

function writeUserType (args) {
	let userTypeSelected = args.rowData.userType;
	if (userTypeSelected === undefined) {
		userTypeSelected = "1";
	}

	userTypeObj = new ej.dropdowns.DropDownList({
		dataSource: userTypeValue,
		fields: { value: 'value', text: 'text' },
		placeholder: '- نوع کاربر -',
		floatLabelType: 'Never',
		value: userTypeSelected.toString()
	});
	userTypeObj.appendTo(userTypeVal);
}