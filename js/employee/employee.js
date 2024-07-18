window.onload = function () {
  new CustomerPage();
};

class CustomerPage {
  pageTitle = "Quản lý khách hàng";
  inputInvalid = [];
  constructor() {
    this.initEvents();
  }

  /**
   * Khởi tạo các sự kiện trong page
   * Author: Minh Hoàng (14/07/2024)
   */
  initEvents() {
    var me = this;
    try {
      // Click button "Thêm mới" hiện thị form thêm mới:
      document
        .querySelector("#btnAdd")
        .addEventListener("click", this.btnAddOnClick);

      // Ẩn form chi tiết, thông báo toast và dialog:
      let buttons = document.querySelectorAll(".modal .modal__btn--close");
      for (let button of buttons) {
        button.addEventListener("click", function () {
          this.parentElement.parentElement.parentElement.style.visibility =
            "hidden";
        });
      }
      // Click button "Đồng ý" ẩn thông báo:
      document
        .querySelector(".dialog--notice .dialog__button--confirm")
        .addEventListener("click", function () {
          this.parentElement.parentElement.parentElement.style.visibility =
            "visible";
          me.inputInvalid[0].focus();
        });
      // Lưu dữ liệu:
      document
        .querySelector("#btnSave")
        .addEventListener("click", this.btnSaveOnClick.bind(this));

      // Tìm kiếm nhân viên:
      document
        .querySelector("#btnSearch")
        .addEventListener("click", this.btnSearchOnClick);

      // Refresh dữ liệu:
      document
        .querySelector("#btnRefresh")
        .addEventListener("click", this.btnRefreshOnClick);

      // Xuất excel:
      document
        .querySelector("#btnExport")
        .addEventListener("click", this.btnExportOnClick);

      // Sửa thông tin nhân viên:
      document
        .querySelector(".btn-change")
        .addEventListener("click", this.btnChangeOnClick);

      // Copy thông tin nhân viên:
      document
        .querySelector(".btn-duplicate")
        .addEventListener("click", this.btnDuplicateOnClick);

      // Xóa nhân viên:
      document
        .querySelector(".btn-remove")
        .addEventListener("click", this.btnRemoveOnClick);

      // Xóa nhiều nhân viên:
      document
        .querySelector(".btn-remove-multi")
        .addEventListener("click", this.btnRemoveMultiOnClick);
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Load dữ liệu cần thiết lúc bắt đầu trang web
   * Author: Minh Hoàng (14/07/2024)
   */
  loadData() {
    try {
      // Gọi api lấy dữ liệu:
      fetch("")
        .then((res) => res.json())
        .then((data) => {
          console.log(data);
        });
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Click button "Thêm mới" tại trang chủ để hiển thị form thêm mới nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  btnAddOnClick() {
    try {
      // Hiển thị form thêm mới:
      // 1. Lấy ra element của form thêm mới:
      let form = document.querySelector("#formEmployeeDetail");

      // 2. Set hiển thị form:
      form.parentElement.style.visibility = "visible";
      //...
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Click button "Thêm mới" tại form thêm mới nhân viên để thêm mới nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  btnSaveOnClick() {
    try {
      // Thực hiện validate dữ liệu:
      let error = this.validateData();

      // Hiển thị thông báo nếu dữ liệu không hợp lệ:
      if (error.Errors.length > 0) {
        let dialogNotice = document.querySelector(".dialog.dialog--notice");
        // Hiển thị thông báo lên:
        dialogNotice.parentElement.style.visibility = "visible";
        // Thay đổi tiêu đề thông báo:
        dialogNotice.querySelector(".modal__header-title").innerHTML =
          "Dữ liệu không hợp lệ";
        // Duyệt từng nội dung thông báo:
        let li = "";
        for (let Msg of error.Errors) {
          li += `<li class="modal__body-description">
                    <img src="./assets/icon/error-48.png" class="modal__body-icon" alt="Error">  
                    ${Msg}
                </li>`;
        }
        // Thay đổi chi tiêt thông báo:
        dialogNotice.querySelector(
          ".modal__body"
        ).innerHTML = `<ul class="modal__body-list">${li}</ul>`;
        // Lưu input lỗi để thực hiện focus:
        this.inputInvalid = error.InputInvalid;
      } else {
        // --> Nếu dữ liệu hợp lệ thì gọi api thực hiện thêm mới:
      }
      //...
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Hàm kiểm tra hợp lệ dữ liệu form khi click button "Thêm mới" trên form
   * Author: Minh Hoàng (14/07/2024)
   */
  validateData() {
    try {
      let result = {
        InputInvalid: [],
        Errors: [],
      };
      // Kiểm tra hợp lệ các field bắt buộc:
      let validateRequire = this.checkRequireInput();
      // Lưu thông tin lỗi:
      result.InputInvalid.push(...validateRequire.InputInvalid);
      result.Errors.push(...validateRequire.Errors);
      //...

      return result;
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Hàm kiểm tra hợp lệ cho các field bắt buộc
   * Author: Minh Hoàng (14/07/2024)
   */
  checkRequireInput() {
    try {
      let result = {
        InputInvalid: [],
        Errors: [],
      };
      // Lấy ra tất cả các nút bắt buộc nhập:
      let inputs = document.querySelectorAll(
        "#formEmployeeDetail input[required]"
      );
      for (let input of inputs) {
        let value = input.value;
        // Kiểm tra tính hợp lệ của field:
        if (value === "" || value === null || value === undefined) {
          let label = input.previousElementSibling.textContent;
          // Đổi màu style input không hợp lệ:
          input.classList.add("input--invalid");
          // Tạo message thông báo lỗi:
          let error = `${label} không được phép để trống.`;
          // Bổ xung thông tin lỗi dưới input không hợp lệ:
          input.nextElementSibling.textContent = error;
          // Lưu thông tin lỗi:
          result.InputInvalid.push(input);
          result.Errors.push(error);
        } else {
          // Xóa style input không hợp lệ:
          input.classList.remove("input--invalid");
          // Xóa thông tin lỗi dưới input không hợp lệ:
          input.nextElementSibling.textContent = "";
        }
      }
      return result;
    } catch (error) {
      console.error(error);
    }
  }
}
