declare const ej: any;
declare const $: any;

type ProvinceLookup = { ProvinceId: number; ProvinceName: string };

type CityRecord = {
  CityId: number;
  CityName: string;
  Description?: string;
  ProvinceID: number;
  Province?: ProvinceLookup;
  IsDelete?: boolean;
  isDelete?: boolean;
};

declare global {
  interface Window {
    baseUrlCity: string;
    insertUrlCity: string;
    updateUrlCity: string;
    deleteUrlCity: string;
    restoreUrlCity: string;
    cityProvinces: ProvinceLookup[];
    // grid handlers used by Syncfusion tag helpers
    cityCreated?: (args?: unknown) => void;
    cityRowDataBound?: (args: any) => void;
    cityToolbarClick?: (args: any) => void;
    cityCommandClick?: (args: any) => void;
    cityQueryCell?: (args: any) => void;
    updateCityList?: (mode?: string) => void;
  }
}

const getCityGrid = () => document.getElementById('cityList')?.ej2_instances[0];

const indexNumber = (args: any) => {
  const grid = getCityGrid();
  if (args.row && grid) {
    const rowIndex = parseInt(args.row.getAttribute('aria-rowIndex'), 10);
    const currentPageNumber = grid.pageSettings.currentPage;
    const pageSize = grid.pageSettings.pageSize;
    const startIndex = (currentPageNumber - 1) * pageSize;
    args.row.cells[0].innerHTML = (startIndex + rowIndex).toString();
  }
};

const toggleDeleteRowClass = (args: any) => {
  const isDeleted = args?.data?.isDelete ?? args?.data?.IsDelete;
  if (isDeleted === true) {
    args.row?.classList.add('deactivate');
  } else {
    args.row?.classList.remove('deactivate');
  }
};

const styleBuiltInButtons = () => {
  setTimeout(() => {
    const toolbar = document.querySelector('.e-toolbar');

    if (!toolbar) return;

    const updateButtonClasses = (
      selector: string,
      additionalTitle: string,
      css: string[]
    ) => {
      const target = toolbar.querySelector(selector) as HTMLElement | null;
      const hoverTarget = toolbar.querySelector(`[title]${selector ? '' : ''}`) as HTMLElement | null;
      if (target) {
        target.classList.add('btn', ...css);
        target.setAttribute('title', additionalTitle);
      }
      if (hoverTarget) hoverTarget.classList.add('btn', ...css);
    };

    updateButtonClasses('[aria-label="اضافه کردن"]', 'افزودن شهر جدید', ['btn-outline-success', 'e-success']);
    updateButtonClasses('#cityList_edit', 'ویرایش شهر', ['btn-outline-primary', 'e-primary']);
    updateButtonClasses('#cityList_update', 'بروزرسانی شهر', ['btn-outline-warning', 'e-warning']);
    updateButtonClasses('#cityList_cancel', 'لغو', ['btn-outline-secondary', 'e-secondary']);

    const centerToolbar = toolbar.querySelector('.e-toolbar-center');
    if (centerToolbar) {
      centerToolbar.innerHTML = `<span class='e-badge e-badge-primary' style="font-size:20px;font-weight:bolder;">جدول شهر</span>`;
    }
  }, 100);
};

class CityCustomAdaptor extends ej.data.UrlAdaptor {
  processResponse(data: any, dt: any, query: any, xhr: any, request: any, changes: any) {
    const grid = getCityGrid();
    if (!ej.base.isNullOrUndefined(data.action)) {
      if (data.action === 'fetchGridCity') {
        $('#spnRowCity').text(data.count);
        $('#spnRowDelCity').text(data.countDelete);
        $('#spnRowAllCity').text(data.countAll);
      }

      const alertConfig = (title: string, content: string, cssClass: string) => ({
        title,
        content,
        okButton: { text: 'باشه', cssClass },
        showCloseIcon: true,
        width: '400px',
        height: '200px',
        isModal: true,
        closeOnEscape: true,
        position: { X: 'center', Y: 'center' },
        animationSettings: { effect: 'Zoom', duration: 700 }
      });

      switch (data.action) {
        case 'insert':
          ej.popups.DialogUtility.alert(
            alertConfig(
              'ثبت شهر جدید',
              `شهر <span style="color:greenyellow;font-weight:bold;">${data.city}</span> با موفقیت ثبت شد.`,
              'badge btn btn-outline-success green rounded'
            )
          );
          break;
        case 'update':
          ej.popups.DialogUtility.alert(
            alertConfig(
              'ویرایش شهر',
              `شهر <span style="color:yellow;font-weight:bold;">${data.city}</span> با موفقیت ویرایش شد.`,
              'badge btn btn-outline-warning yellow rounded'
            )
          );
          grid?.refresh();
          break;
        case 'delete':
          ej.popups.DialogUtility.alert(
            alertConfig(
              'حذف شهر',
              `شهر <span style="color:#EF5A26;font-weight:bold;">${data.city}</span> با موفقیت حذف شد.`,
              'badge btn btn-outline-danger red rounded'
            )
          );
          grid?.refresh();
          break;
        case 'restore':
          ej.popups.DialogUtility.alert(
            alertConfig(
              'بازگردانی شهر',
              `شهر <span style="color:lightgreen;font-weight:bold;">${data.city}</span> با موفقیت بازگردانده شد.`,
              'badge btn btn-outline-info blue rounded'
            )
          );
          grid?.refresh();
          break;
        case 'repeat':
          ej.popups.DialogUtility.alert(
            alertConfig(
              `<span class="e-badge e-badge-warning"> خطا </span> در ثبت اطلاعات`,
              `شهر <span class="e-badge e-badge-primary" style="font-weight:bold;"> ${data.city} </span> تکراری میباشد.`,
              'btn btn-outline-danger black rounded'
            )
          );
          grid?.refresh();
          break;
        case 'error':
          ej.popups.DialogUtility.alert(
            alertConfig(
              `<span style="color:white;background-color:red;" > خطا </span> در ثبت اطلاعات`,
              `در ثبت اطلاعات خطایی رخ داده لطفا بررسی کنید. <span style="color:red;font-weight:bold;">${data.ErrMsg}</span>`,
              'btn btn-outline-danger black rounded'
            )
          );
          grid?.refresh();
          break;
      }

      if (!ej.base.isNullOrUndefined(data.data)) {
        return data.data;
      }
    }
    return data;
  }
}

const getCityUrl = (mode?: string) => (mode ? `${window.baseUrlCity}?mode=${mode}` : window.baseUrlCity);

const createCityDataManager = (mode?: string) =>
  new ej.data.DataManager({
    url: getCityUrl(mode),
    insertUrl: window.insertUrlCity,
    updateUrl: window.updateUrlCity,
    removeUrl: window.deleteUrlCity,
    adaptor: new CityCustomAdaptor()
  });

const updateCityList = (mode?: string) => {
  const grid = getCityGrid();
  if (grid) {
    grid.dataSource = createCityDataManager(mode);
    grid.refresh();
  }
};

const cityCreated = () => {
  ej.base.enableRtl(true);
  updateCityList('');
  styleBuiltInButtons();
  updateToolbarDeleteRestore(null as any);
};

const updateToolbarDeleteRestore = (rowData: CityRecord | null) => {
  const grid = getCityGrid();
  if (!grid || !grid.toolbarModule) return;

  const deleteBtn = document.getElementById('cityList_deleteSoft');
  const restoreBtn = document.getElementById('cityList_restore');
  const isDeleted = rowData?.isDelete ?? rowData?.IsDelete;

  if (rowData == null) {
    deleteBtn?.setAttribute('disabled', 'true');
    restoreBtn?.setAttribute('disabled', 'true');
    return;
  }

  if (isDeleted) {
    deleteBtn?.setAttribute('disabled', 'true');
    restoreBtn?.removeAttribute('disabled');
  } else {
    deleteBtn?.removeAttribute('disabled');
    restoreBtn?.setAttribute('disabled', 'true');
  }
};

const handleCitySoftDelete = (action: 'delete' | 'restore', rowData?: CityRecord) => {
  const grid = getCityGrid();

  if (!grid || !rowData) {
    ej.popups.DialogUtility.alert({
      title: `<span class="e-badge e-badge-warning"> هشدار </span> در حذف اطلاعات`,
      content: 'لطفا یک شهر را برای حذف یا بازگشت انتخاب کنید.',
      okButton: { text: 'متوجه شدم', cssClass: 'btn btn-outline-danger black rounded', icon: 'e-icons e-circle-info' },
      showCloseIcon: true,
      closeOnEscape: true,
      animationSettings: { effect: 'Zoom' }
    });
    return;
  }

  const isDeleting = action === 'delete';
  const updatedRow = {
    ...rowData,
    isDelete: isDeleting,
    IsDelete: isDeleting
  } as CityRecord;

  const rowIndex = grid.getRowIndexByPrimaryKey(rowData.CityId);
  grid.updateRow(rowIndex, updatedRow);
};

const cityCommandClick = (args: any) => {
  const rowData = args.rowData as CityRecord;
  switch (args.commandColumn.type) {
    case 'deleteCmd':
      handleCitySoftDelete('delete', rowData);
      break;
    case 'restoreCmd':
      handleCitySoftDelete('restore', rowData);
      break;
  }
};

const cityToolbarClick = (args: any) => {
  const grid = getCityGrid();
  const selectedRecords: CityRecord[] = grid?.getSelectedRecords?.() ?? [];

  if (selectedRecords.length === 0) {
    updateToolbarDeleteRestore(null);
    ej.popups.DialogUtility.alert({
      title: `<span class="e-badge e-badge-danger e-badge-pill" > هشدار </span>`,
      content: 'لطفا یک رکورد را انتخاب نمایید.',
      okButton: { icon: 'e-icons e-check', cssClass: 'badge bg-success okConfirm', text: 'باشه' },
      showCloseIcon: true,
      closeOnEscape: true,
      animationSettings: { effect: 'Zoom', duration: 700 }
    });
    return;
  }

  const rowData = selectedRecords[0];
  updateToolbarDeleteRestore(rowData);

  switch (args.item.id) {
    case 'cityList_deleteSoft':
      handleCitySoftDelete('delete', rowData);
      break;
    case 'cityList_restore':
      handleCitySoftDelete('restore', rowData);
      break;
  }
};

const cityRowDataBound = (e: any) => {
  indexNumber(e);
  toggleDeleteRowClass(e);
};

const cityQueryCell = (args: any) => {
  if (args.column['headerText'] === 'عملیات') {
    const isDeleted = args.data['isDelete'] ?? args.data['IsDelete'];
    if (isDeleted === true) {
      $(args.cell).find('.btnDelete')[0].classList.add('e-hide');
      $(args.cell).find('.btnRestore')[0].classList.remove('e-hide');
    } else {
      $(args.cell).find('.btnRestore')[0].classList.add('e-hide');
      $(args.cell).find('.btnDelete')[0].classList.remove('e-hide');
    }
  }
};

// expose handlers to window for Syncfusion tag helpers
window.cityCreated = cityCreated;
window.cityRowDataBound = cityRowDataBound;
window.cityToolbarClick = cityToolbarClick;
window.cityCommandClick = cityCommandClick;
window.cityQueryCell = cityQueryCell;
window.updateCityList = updateCityList;

export {};
