// Load Function Script
// Validation National Code
// Load Function - Validation
// تابع در هنگام لود جدول و برای اعتبارسنجی داده‌های ورودی

function loadFunc (e) {
	this.columns[1].validationRules = { required: [true, "وارد کردن نام اجباری است."] }; // نام
	this.columns[2].validationRules = { required: [true, "وارد کردن نام خانوادگی اجباری است."] }; // نام خانوادگی
	this.columns[3].validationRules = { // شماره تلفن همراه
		required: [true, "وارد کردن شماره تلفن اجباری است."],
		minLength: [11, " شماره تلفن همراه 11 رقم می باشد."],
		maxLength: [11, " شماره تلفن همراه 11 رقم می باشد."],
		custom: [validationPhone, "ورودی باید 09 و شماره تلفن باشد."]
	};
	this.columns[4].validationRules = { // کدملی
		required: [true, "وارد کردن کدملی اجباری است."],
		minLength: [10, "کد ملی باید 10 رقم باشد."],
		maxLength: [10, "کد ملی باید 10 رقم باشد."],
		custom: [validationMelliCode, "کد ملی معتبر نیست."]
	};
	this.columns[5].validationRules = { // ایمیل
		required: [true, "وارد کردن ایمیل اجباری است."],
		email: [true, "عبارت واد شده ایمیل نمی باشد. لطفا بررسی کنید."]
	}
}
function validationMelliCode (args) {
	return validationNationalCode(args.value);
}

function validationPhone (args) {
	const reg = /^09\d{9}$/;

	return reg.test(args.value);
}


// تابع اعتبارسنجی کد ملی
let validationNationalCode = (code) => {
	let reg = /^[0-9]{10}$/;
	if (!reg.test(code)) {
		return false;
	}
	let coefficients = [29, 27, 23, 21, 19, 17, 13, 11, 7, 3];
	let coefficientsNew = [10, 9, 8, 7, 6, 5, 4, 3, 2, 1];
	let sum = 0;
	for (let i = 0; i < 9; i++) {
		sum += parseInt(code[i] * coefficientsNew[i]);
	}
	let reminder = sum % 11;
	let controlDigit = parseInt(code[9]);
	if (reminder < 2 && reminder === controlDigit)
		return true;

	if (reminder >= 2 && (11 - reminder) === controlDigit)
		return true;

	return false;
}


// function loadFunc() {
// 	this.columns[1].validationRules = { required: [true, "نام وارد نشده است."] };
// 	this.columns[2].validationRules = { required: [true, "فامیلی وارد نشده است."] };
// 	this.columns[3].validationRules = {
// 		required: [true, "شماره تلفن وارد نشده است."],
// 		minLength: [11, "تلفن باید 11 رقمی باشد"],
// 		maxLength: [11, "تلفن باید 11 رقمی باشد"],
// 		regularExpression: ["^09\d{9}$", "تلفن باید با 09 شروع شده و فقط شامل اعداد باشد."]
// 	};
// 	this.columns[4].validationRules = {
// 		required: [true, "کد ملی وارد نشده است."],
// 		minLength: [10, "کد ملی باید 10 رقمی باشد"],
// 		maxLength: [10, "کد ملی باید 10 رقمی باشد"],
// 		regularExpression: ["^/d{10}$/", "کد ملی باید فقط شامل اعداد باشد."]
// 	};
// }