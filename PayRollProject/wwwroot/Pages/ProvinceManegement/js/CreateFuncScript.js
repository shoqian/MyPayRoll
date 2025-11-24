function getProvinceGrid () {
        return document.getElementById('provinceList').ej2_instances[0];
}

class ProvinceCustomAdaptor extends ej.data.UrlAdaptor {
        processResponse (data, dt, query, xhr, request, changes) {
//              console.log("Data is :", data);
//              console.log("Data Table :", dt);
//              console.log("Query is :", query);
//              console.log("XHR is :", xhr);
//              console.log("Request is :", request);
//              console.log("Changes are :", changes);
                if (!ej.base.isNullOrUndefined(data.action)) {
                        const grid = getProvinceGrid();
                        if (data.action === "fetchGridProvince") {
                                $('#spnRowProvince').text(data.count);
                                $('#spnRowDelProvince').text(data.countDelete);
                                $('#spnRowAllProvince').text(data.countAll);
                        }
                        switch (data.action) {
                                case "insert":
                                        ej.popups.DialogUtility.alert({
                                                title: "استان جدید",
                                                content: `استان <span style="color:greenyellow;font-weight:bold;">${data.province}</span> با موفقیت ثبت شد.`,
                                                okButton: { text: 'عالیه', cssClass: "badge btn btn-outline-success green rounded okConfirm" },
                                                showCloseIcon: true,
                                                width: '400px',
                                                height: '200px',
                                                isModal: true,
                                                closeOnEscape: true,
                                                position: { X: 'center', Y: 'center' },
                                                animationSettings: { effect: 'Zoom', deration: 700 }
                                        });
                                        break;

                                case "update":
                                        ej.popups.DialogUtility.alert({
                                                title: "ویرایش استان",
                                                content: `استان <span style=\"color:yellow;font-weight:bold;\" >${data.province
                                                        }</span> با موفقیت ویرایش شد.`,
                                                okButton: { text: 'عالیه', cssClass: "badge btn btn-outline-warning yellow rounded okConfirm" },
                                                showCloseIcon: true,
                                                width: '400px',
                                                height: '200px',
                                                isModal: true,
                                                closeOnEscape: true,
                                                position: { X: 'center', Y: 'center' },
                                                animationSettings: { effect: 'Zoom', deration: 700 }
                                        });
                                        grid.refresh();
                                        break;

                                case "delete":
                                        ej.popups.DialogUtility.alert({
                                                title: "حذف استان",
                                                content: `استان <span style=\"color:#EF5A26;font-weight:bold;\">${data.province
                                                        }</span> با موفقیت حذف شد.`,
                                                okButton: { text: "عجب", cssClass: "badge btn btn-outline-danger red rounded okConfirm" },
                                                showCloseIcon: true,
                                                width: '400px',
                                                height: '200px',
                                                isModal: true,
                                                closeOnEscape: true,
                                                position: { X: 'center', Y: 'center' },
                                                animationSettings: { effect: 'Zoom', deration: 700 }
                                        });
                                        grid.refresh();
                                        break;

                                case "repeat":
                                        ej.popups.DialogUtility.alert({
                                                title: `<span class=\"e-badge e-badge-warning\"> خطا </span> در ثبت اطلاعات`,
                                                content: `استان <span class=\"e-badge e-badge-primary\" style=\"font-weight:bold;\"> ${data.province} </span> تکراری میباشد.`,
                                                okButton: { text: "متوجه شدم", cssClass: "btn btn-outline-danger black rounded", icon: 'e-icons e-circle-info' },
                                                showCloseIcon: true,
                                                width: '400px',
                                                height: '200px',
                                                isModal: true,
                                                closeOnEscape: true,
                                                position: { X: 'center', Y: 'center' },
                                                animationSettings: { effect: 'Zoom' ,deration:1500 }
                                        });
                                        grid.refresh();
                                        break;

                                case "error":
                                        ej.popups.DialogUtility.alert({
                                                title: `<span style=\"color:white;background-color:red;\" > خطا </span> در ثبت اطلاعات`,
                                                content: `در ثبت اطلاعات خطایی رح داده لطفا بررسی کنید. <span style=\"color:red;font-weight:bold;\">${data.ErrMsg.toString() }</span>`,
                                                okButton: { text: "متوجه شدم", cssClass: "btn btn-outline-danger black rounded" },
                                                showCloseIcon: true,
                                                closeOnEscape: true,
                                                animationSettings: { effect: 'Zoom' }
                                        });
                                        grid.refresh();
                                        break;
                        }

                        if (!ej.base.isNullOrUndefined(data.data)) {
                                return data.data;
                        } else {
                                return data;
                        }
                }
        }
}

function getProvinceUrl (mode) {
        if (mode) {
                return `${window.baseUrlProvince}?mode=${mode}`;
        }
        return window.baseUrlProvince;
}

function createProvinceDataManager (mode) {
        return new ej.data.DataManager({
                url: getProvinceUrl(mode),
                insertUrl: window.insertUrlProvince,
                updateUrl: window.updateUrlProvince,
                removeUrl: window.deleteUrlProvince,
                adaptor: new ProvinceCustomAdaptor()
        });
}

function updateProvinceList (mode) {
        let grid = getProvinceGrid();
        grid.dataSource = createProvinceDataManager(mode);
        grid.refresh();
}

function createdFunc (args) {
        ej.base.enableRtl(true);

        updateProvinceList('');
}
