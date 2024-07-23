window.onload = function () {
  new EmployeePage();
};

class EmployeePage {
  pageTitle = "Quản lý khách hàng";
  inputInvalid = [];
  employees = [];
  constructor() {
    this.loadData();
    this.initEvents();
  }

  /**
   * Khởi tạo các sự kiện trong page
   * Author: Minh Hoàng (14/07/2024)
   */
  initEvents() {
    const self = this;
    try {
      // Click button "Thêm mới" hiện thị form thêm mới:
      document
        .querySelector("#btnAdd")
        .addEventListener("click", this.btnAddOnClick.bind(this));

      // Ẩn form chi tiết, thông báo toast và dialog:
      const closeButtons = document.querySelectorAll(
        ".modal .modal__btn--close"
      );
      for (const button of closeButtons) {
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
            "hidden";
          self.inputInvalid[0].focus();
        });

      // Click thu gọn/mở rộng sidebar
      document
        .querySelector("#aside .aside__collapse")
        .addEventListener("click", function () {
          const aside = this.parentElement;
          const container = document.querySelector(".container");
          // Lấy ra giá trị attribute collapse là on/off:
          let collapse = aside.getAttribute("collapse");
          if (collapse === "off") {
            // Thu nhỏ aside:
            aside.setAttribute("collapse", "on");
            container.classList.add("container--collapse");
          } else {
            // Mở rộng aside:
            aside.setAttribute("collapse", "off");
            container.classList.remove("container--collapse");
          }
        });

      // Click button để hiển thị dropdown trên combobox:
      const drpButtons = document.querySelectorAll(
        ".combobox .combobox__button"
      );
      for (const button of drpButtons) {
        button.addEventListener("click", function () {
          // Ẩn hoặc hiện dropdown và lấy dữ liệu danh sách:
          self.btnDropdown(this);
        });
        // Ẩn dropdown khi click bên ngoài combobox:
        document.addEventListener("click", (event) => {
          if (!button.contains(event.target)) {
            button.parentElement.parentElement.setAttribute("drop", "off");
          }
        });
      }

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
   * Load danh sách thông tin nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async loadData() {
    try {
      // Gọi api lấy dữ liệu:
      this.employees = await this.fetchData(
        "https://cukcuk.manhnv.net/api/v1/Customers"
      );
      // Chèn dữ liệu vào bảng:
      document.querySelector("#dataTable tbody").innerHTML =
        this.employees.reduce((acc, cur, ind) => {
          return `${acc}
            <tr class="table__record">
                <td class="table__cell">${ind + 1}</td>
                <td class="table__cell">${cur.CustomerCode}</td>
                <td class="table__cell">${cur.FullName}</td>
                <td class="table__cell">${cur.Gender === 1 ? "Nam" : "Nữ"}</td>
                <td class="table__cell">${this.formatDate(cur.DateOfBirth)}</td>
                <td class="table__cell">${cur.Email}</td>
                <td class="table__cell">
                    ${cur.Address}
                    <div class="table__option">
                        <button class="button button--symbol">
                            <i class="fas fa-pen button-icon"></i>
                        </button>
                        <button class="button button--symbol">
                            <i class="far fa-copy button-icon"></i>
                        </button>
                        <button class="button button--symbol">
                            <img src="./assets/icon/close-48.png" class="button-icon" alt="Xóa">
                        </button>
                    </div>
                </td>
            </tr>`;
        }, "");
      document.querySelector(
        "#countEmployee"
      ).textContent = `Tổng số: ${this.employees.length}`;
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
      // Lấy ra element của form thêm mới:
      const form = document.querySelector("#formEmployeeDetail");
      // Set hiển thị form:
      form.parentElement.style.visibility = "visible";
      // Reset form thêm mới:
      this.resetForm();
      // focus vào input đầu tiên:
      form.querySelector("input.input__data").focus();
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
        const dialogNotice = document.querySelector(".dialog.dialog--notice");
        // Hiển thị thông báo lên:
        dialogNotice.parentElement.style.visibility = "visible";
        // Thay đổi tiêu đề thông báo:
        dialogNotice.querySelector(".modal__header-title").innerHTML =
          "Dữ liệu không hợp lệ";
        // Duyệt từng nội dung thông báo:
        let li = error.Errors.reduce((acc, cur) => {
          return `${acc}
            <li class="modal__body-description">
              <img src="./assets/icon/error-48.png" class="modal__body-icon" alt="Error">  
              ${cur}
            </li>`;
        }, "");
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
   * Click hiển thị drowdown và gọi api lấy dữ liệu danh sách
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnDropdown(element) {
    try {
      const combobox = element.parentElement.parentElement;
      const dropdown = element.nextElementSibling;
      const input = element.previousElementSibling;
      let drop = combobox.getAttribute("drop");
      let url = combobox.getAttribute("src");

      //Đóng mở dropdown:
      if (drop === "off") {
        combobox.setAttribute("drop", "on");
        if (url !== "") {
          // Lấy dữ liệu lựa chọn cho combobox tương ứng:
          let items = await this.fetchDataCombobox(url);
          dropdown.innerHTML = items.reduce((acc, cur) => {
            return `${acc}<li class="combobox__item">${cur.DepartmentName}</li>`;
          }, "");
          // Chọn combobox:
          this.handleSelectCombobox(input, dropdown);
        }
      } else {
        combobox.setAttribute("drop", "off");
      }
      //...
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Xử lý sự kiện chọn combobox
   * Author: Minh Hoàng (14/07/2024)
   */
  handleSelectCombobox(input, dropdown) {
    // Chọn option trên dropdown:
    let nodeItems = dropdown.querySelectorAll(".combobox__item");
    for (const node of nodeItems) {
      node.addEventListener("click", () => {
        // Chèn giá trị vào input:
        input.value = node.textContent;
        // Đánh dấu giá trị đã chọn trên input:
        for(const option of nodeItems){
          // option.classList.remove("combobox__item--selected");
          if(option.textContent === input.value){
            option.classList.add("combobox__item--selected");
          }
        }
      });
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
      // Lưu lỗi require:
      result.InputInvalid.push(...validateRequire.InputInvalid);
      result.Errors.push(...validateRequire.Errors);
      // Kiểm tra format email
      const inputEmail = document.querySelector(
        "#formEmployeeDetail #txtEmail"
      );
      let email = inputEmail.value;
      if (email !== "") {
        if (!this.validateEmail(email)) {
          result.InputInvalid.push(inputEmail);
          result.Errors.push("Email không hợp lệ.");
          inputEmail.nextElementSibling.textContent = "Email không hợp lệ.";
        }
      }
      //...

      return result;
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Reset form điền về mặc định
   * Author: Minh Hoàng (14/07/2024)
   */
  resetForm() {
    const inputs = document.querySelectorAll(
      '#formEmployeeDetail input:not([type="radio"])'
    );
    // Set trống cho các trường text:
    for (const input of inputs) {
      input.value = "";
      if (input.hasAttribute("required")) {
        // Xóa style input không hợp lệ nếu có:
        input.classList.remove("input--invalid");
        // Xóa thông tin lỗi dưới input không hợp lệ nếu có:
        input.nextElementSibling.textContent = "";
      }
    }
    // Set radio mặc định:
    document.querySelector(
      '#formEmployeeDetail input[type="radio"]'
    ).checked = true;
  }

  /**
   * Kiểm tra email hợp lệ
   * Author: Minh Hoàng (14/07/2024)
   */
  validateEmail(email) {
    try {
      let regex = /^[\w]+@gmail\.com$/;
      return regex.test(email);
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
      const inputs = document.querySelectorAll(
        "#formEmployeeDetail input[required]"
      );
      for (const input of inputs) {
        let value = input.value.trim();
        // Kiểm tra tính hợp lệ của field:
        if (value === "" || value === null || value === undefined) {
          const label = input.previousElementSibling.textContent;
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
  /**
   * Dịnh dạng kiểu Date => (dd/MM/yyyy)
   * Author: Minh Hoàng (14/07/2024)
   */
  formatDate(dateString) {
    let date = new Date(dateString);
    let day = date.getDay().toString().padStart(2, "0");
    let month = (date.getMonth() + 1).toString().padStart(2, "0");
    let year = date.getFullYear();
    return `${day}/${month}/${year}`;
  }
  /**
   * Hàm ẩn hiện loading
   * Author: Minh Hoàng (14/07/2024)
   */
  spinner(isShow) {
    try {
      const spin = document.querySelector("#spinner");
      if (isShow === true) {
        spin.style.visibility = "visible";
      } else {
        spin.style.visibility = "hidden";
      }
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Hàm thực hiện fetch dữ liệu
   * Author: Minh Hoàng (14/07/2024)
   */
  async fetchData(url) {
    // Hiện loading trước khi gọi api:
    this.spinner(true);
    try {
      let response = await fetch(url);
      return await response.json();
    } catch (error) {
      console.error(error);
    } finally {
      // Ẩn loading khi kết thúc:
      this.spinner(false);
    }
  }

  /**
   * Hàm thực hiện fetch dữ liệu cho combobox
   * Author: Minh Hoàng (14/07/2024)
   */
  async fetchDataCombobox(url) {
    try {
      let response = await fetch(url);
      return await response.json();
    } catch (error) {
      console.error(error);
    }
  }
}
