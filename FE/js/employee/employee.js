window.onload = function () {
  new EmployeePage();
};
function test() {
  alert("dasdas");
}
class EmployeePage {
  pageTitle = "Quản lý khách hàng";
  number = 1;
  limit = 10;
  count = 0;
  page = 0;
  inputInvalid = [];
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

      // Ẩn form chi tiết, thông báo toast và dialog khi click close:
      const closeButtons = document.querySelectorAll(
        ".modal .modal__btn--close"
      );
      for (const button of closeButtons) {
        button.addEventListener("click", function () {
          this.parentElement.parentElement.parentElement.style.visibility =
            "hidden";
        });
      }
      // Ẩn form chi tiết, thông báo toast và dialog khi click hủy:
      const cancelButtons = document.querySelectorAll(".modal .button--cancel");
      for (const button of cancelButtons) {
        button.addEventListener("click", function () {
          this.parentElement.parentElement.parentElement.style.visibility =
            "hidden";
        });
      }

      // Click button "Đồng ý" ẩn thông báo:
      document
        .querySelector("#notice .dialog__button--confirm")
        .addEventListener("click", function () {
          this.parentElement.parentElement.parentElement.style.visibility =
            "hidden";
          self.inputInvalid[0]?.focus();
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

      // Chọn trang:
      const selectPage = document.querySelector("#selectionPage");
      selectPage.addEventListener("change", function () {
        self.number = this.value;
        self.loadData();
      });

      // Chuyển sang trang trước:
      document.querySelector("#previousPage").addEventListener("click", () => {
        selectPage.value = parseInt(this.number) - 1;
        const event = new Event("change");
        selectPage.dispatchEvent(event);
      });

      // Chuyển sang trang sau:
      document.querySelector("#nextPage").addEventListener("click", () => {
        selectPage.value = parseInt(this.number) + 1;
        const event = new Event("change");
        selectPage.dispatchEvent(event);
      });

      // Lưu dữ liệu:
      document
        .querySelector("#formDetail .button--save")
        .addEventListener("click", this.btnSaveOnClick.bind(this));

      // Refresh dữ liệu:
      document
        .querySelector("#btnRefresh")
        .addEventListener("click", this.btnRefresh.bind(this));

      // Tìm kiếm nhân viên khi click nút tìm kiếm:
      const searchButton = document.querySelector("#btnSearch");
      searchButton.addEventListener("click", function () {
        self.btnSearchOnClick(this);
      });

      // Tìm kiếm nhân viên khi enter:
      searchButton.previousElementSibling.addEventListener(
        "keypress",
        function (event) {
          if (event.key === "Enter") {
            event.preventDefault();
            self.btnSearchOnClick(this);
            searchButton.click();
          }
        }
      );

      // Xuất excel:
      document
        .querySelector("#btnExport")
        .addEventListener("click", this.btnExportOnClick.bind(this));

      // Nhập excel:
      // 1. Mở hộp chọn file:
      document
        .querySelector("#btnImport")
        .addEventListener("click", function () {
          this.querySelector("#fileInput").click();
        });
      // 1. Import dữ liệu:
      document
        .querySelector("#fileInput")
        .addEventListener("change", (event) => {
          this.btnImportOnClick(event.target.files[0]);
        });

      // Xóa nhiều nhân viên:
      document
        .querySelector("#btnDeleteMulti")
        .addEventListener("click", this.btnRemoveMultiOnClick.bind(this));
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
      // Gọi api lấy dữ liệu nhân viên:
      let resEmployee = await this.fetchData(
        `https://localhost:44357/api/v1/employees/page?limit=${this.limit}&number=${this.number}`
      );
      // Kiểm tra trạng thái request thông tin nhân viên:
      if (!(resEmployee.success === true)) {
        console.error(resEmployee.errors);
        return;
      }
      // Gọi api lấy số lượng nhân viên:
      let resCount = await this.fetchData(
        `https://localhost:44357/api/v1/employees/count`
      );
      // Kiểm tra trạng thái request số lượng nhân viên:
      if (!(resCount.success === true)) {
        console.error(resCount.errors);
        return;
      }
      this.count = resCount.data;
      // Xử lý bảng:
      this.insertToTable(resEmployee.data);
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Chèn dữ liệu vào bảng
   * Author: Minh Hoàng (14/07/2024)
   */
  insertToTable(employees) {
    try {
      // Số trang:
      this.page = Math.ceil(this.count / this.limit);
      let optionsHtml = "";
      // Hiển thị danh sách nhân viên lên bảng:
      const table = document.querySelector("#dataTable");
      table.querySelector("tbody").innerHTML = employees.reduce(
        (acc, cur, ind) => {
          return `${acc}
            <tr class="table__record" id="${cur.employeeId}">
                <td class="table__cell">${ind + 1}</td>
                <td class="table__cell">${cur.employeeCode}</td>
                <td class="table__cell">${cur.fullName}</td>
                <td class="table__cell">${cur.genderName}</td>
                <td class="table__cell">${this.formatDate(cur.dateOfBirth)}</td>
                <td class="table__cell">${cur.email}</td>
                <td class="table__cell">
                    ${cur.address ?? ""}
                    <div class="table__option">
                        <button class="button button--symbol button--update" title="Sửa">
                            <i class="fas fa-pen button-icon"></i>
                        </button> 
                        <button class="button button--symbol button--copy" title="Sao chép">
                            <i class="far fa-copy button-icon"></i>
                        </button>
                        <button class="button button--symbol button--delete" title="Xóa">
                            <img src="./assets/icon/close-48.png" class="button-icon" alt="Xóa">
                        </button>
                    </div>
                </td>
            </tr>`;
        },
        ""
      );
      // Thêm sự kiện xóa cho hàng:
      const deleteButtons = document.querySelectorAll(
        ".table__option .button--delete"
      );
      for (const node of deleteButtons) {
        node.addEventListener("click", () => {
          let title = "Xác nhận xóa nhân viên";
          let message = "Bạn có chắc muốn xóa nhân viên này không?";
          let callback = () => {
            this.handleDelete(node);
          };
          this.displayConfirm(message, title, callback);
        });
      }

      // Thêm sự kiện sửa cho hàng:
      const updateButtons = document.querySelectorAll(
        ".table__option .button--update"
      );
      for (const node of updateButtons) {
        node.addEventListener("click", () => {
          this.btnUpdate(node);
        });
      }

      // Thêm sự kiện copy cho hàng:
      const copyButtons = document.querySelectorAll(
        ".table__option .button--copy"
      );
      for (const node of copyButtons) {
        node.addEventListener("click", () => {
          this.handleCopy(node);
        });
      }

      // Thêm sự kiện chọn chọn nhiều dòng bằng doubleclick:
      const row = table.querySelectorAll(".table__record");
      for (const node of row) {
        node.addEventListener("dblclick", function () {
          this.classList.toggle("table__record--selected");
        });
      }

      // Hiển thị tổng số bản ghi:
      document.querySelector(
        "#countPage"
      ).textContent = `Tổng số: ${this.count}`;
      // Hiển thị các option chuyển trang:
      for (let i = 1; i <= this.page; i++) {
        let attribute = "";
        if (i === parseInt(this.number)) {
          attribute = "selected";
        }
        optionsHtml = `
          ${optionsHtml}
          <option class="main__page-number" value="${i}" ${attribute}>${i}</option>
        `;
      }
      document.querySelector("#selectionPage").innerHTML = optionsHtml;
      // Xử lý disable nút chuyển trang:
      if (this.number == 1) {
        document.querySelector("#previousPage").setAttribute("disabled", "");
      } else {
        document.querySelector("#previousPage").removeAttribute("disabled");
      }
      if (this.number == this.page) {
        document.querySelector("#nextPage").setAttribute("disabled", "");
      } else {
        document.querySelector("#nextPage").removeAttribute("disabled");
      }
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Chèn dữ liệu lên form
   * Author: Minh Hoàng (14/07/2024)
   */
  insertToForm(employee) {
    // Lấy ra các filed và chèn dữ liệu:
    document.querySelector("#txtEmployeeCode").value = employee.employeeCode;
    document.querySelector("#txtFullname").value = employee.fullName;
    document.querySelector("#dateBirthday").value = this.formatFormDate(
      employee.dateOfBirth
    );
    document.querySelectorAll("#radioGender input").forEach((radio) => {
      if (parseInt(radio.value) === employee.gender) {
        radio.checked = true;
        return;
      }
    });
    document
      .querySelector("#cbPosition")
      .setAttribute("guid", employee.positionId ?? "");
    document.querySelector("#txtIdentityNumber").value =
      employee.identityNumber;
    document.querySelector("#dateIdentityDate").value = this.formatFormDate(
      employee.identityDate
    );
    document
      .querySelector("#cbDepartment")
      .setAttribute("guid", employee.departmentId ?? "");
    document.querySelector("#txtIdentityPlace").value =
      employee.identityPlace ?? "";
    document.querySelector("#txtAddress").value = employee.address ?? "";
    document.querySelector("#txtPhoneNumber").value = employee.phoneNumber;
    document.querySelector("#txtLandlineNumber").value =
      employee.landlineNumber ?? "";
    document.querySelector("#txtEmail").value = employee.email;
    document.querySelector("#txtBankAccount").value =
      employee.bankAccount ?? "";
    document.querySelector("#txtBankName").value = employee.bankName ?? "";
    document.querySelector("#txtBranch").value = employee.branch ?? "";
  }

  /**
   * Click button "Thêm mới" tại trang chủ để hiển thị form thêm mới nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  btnAddOnClick() {
    try {
      // Lấy ra element của form thêm mới:
      const form = document.querySelector("#formDetail");
      // Xác định button cho form:
      const saveButton = form.querySelector(".button--save");
      saveButton.textContent = "Thêm mới";
      saveButton.setAttribute("type", "append");
      // Set hiển thị form:
      this.displayForm();
      // Reset form thêm mới:
      this.resetForm();
      //...
    } catch (error) {
      console.error(error);
    }
  }

  /**
   * Click button lưu thông tin nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnSaveOnClick() {
    try {
      // Thực hiện validate dữ liệu:
      let error = this.validateData();

      // Hiển thị thông báo nếu dữ liệu không hợp lệ:
      if (error.Errors.length > 0) {
        // Hiển thị thông báo lỗi:
        this.displayError(error.Errors);
        // Lưu input lỗi để thực hiện focus:
        this.inputInvalid = error.InputInvalid;
      } else {
        // Lấy kiểu form:
        const saveButton = document.querySelector("#formDetail .button--save");
        let type = saveButton.getAttribute("type");
        if (type === "append") {
          this.handleAppend();
        } else if (type === "update") {
          this.handleUpdate();
        }
      }
      //...
    } catch (error) {
      console.error(error);
    }
  }
  /**
   * Làm mới dữ liệu trong bảng
   * Author: Minh Hoàng (14/07/2024)
   */
  btnRefresh() {
    this.number = 1;
    this.loadData();
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
      let name = combobox.getAttribute("name");
      let drop = combobox.getAttribute("drop");
      let url = `https://localhost:44357/api/v1/${name}s`;

      //Đóng mở dropdown:
      if (drop === "off") {
        combobox.setAttribute("drop", "on");
        if (url !== "") {
          // Lấy dữ liệu lựa chọn cho combobox tương ứng:
          let res = await this.fetchDataCombobox(url);
          // Kiểm tra trạng thái request cho combobox:
          if (!(res.success === true)) {
            console.error(res.errors);
            return;
          }
          let items = res.data;
          dropdown.innerHTML = items.reduce((acc, cur) => {
            // Đánh dấu giá trị đã chọn trên input:
            let mark = "";
            if (cur[`${name}Name`] === input.value) {
              mark = "combobox__item--selected";
            }
            return `${acc}<li class="combobox__item ${mark}" value="${
              cur[`${name}Id`]
            }">${cur[`${name}Name`]}</li>`;
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
   * Click nút tìm kiếm để tìm kiếm nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnSearchOnClick(node) {
    // Lấy chuỗi tìm kiếm:
    let query = node.previousElementSibling?.value;
    // Kiểm tra chuỗi:
    if (query === "" || query === null || query === undefined) {
      return;
    }
    // Gọi api tìm kiếm dữ liệu nhân viên:
    let res = await this.fetchData(
      `https://localhost:44357/api/v1/employees/search?queryString=${query}`
    );
    // Thiết lập một số giá trị ban đầu:
    let employees = res.data;
    this.number = 1;
    this.count = employees.length;
    // Chèn thông tin tìm kiếm được lên bảng:
    this.insertToTable(employees);
    //...
  }
  /**
   * Click nút sửa nhân viên để hiển thị và chèn dữ liệu lên form
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnUpdate(node) {
    // Lấy ra element của form sửa:
    const form = document.querySelector("#formDetail");
    // Hiển thị form:
    this.displayForm();
    // Xác định button cho form:
    const saveButton = form.querySelector(".button--save");
    saveButton.textContent = "Sửa";
    saveButton.setAttribute("type", "update");
    // Set disabled field mã:
    form.querySelector("#txtEmployeeCode").setAttribute("disabled", "");
    // Lấy id nhân viên tại hàng muốn cập nhật:
    let id = node.parentElement.parentElement.parentElement.getAttribute("id");
    // Gọi api lấy thông tin nhân viên hàng:
    let res = await this.fetchData(
      `https://localhost:44357/api/v1/employees/${id}`
    );
    // Kiểm tra trạng thái request thông tin nhân viên:
    if (!(res.success === true)) {
      console.error(res.errors);
      return;
    }
    // Thêm dữ liệu nhân viên của hàng lên form:
    this.insertToForm(res.data);
  }

  /**
   * Xuất danh sách nhân viên thành file excel
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnExportOnClick() {
    // Gọi API để lấy danh sách mã hóa Base64:
    let res = await this.fetchData(
      "https://localhost:44357/api/v1/employees/export"
    );

    const base64String = res.data.data; // Trích xuất chuỗi Base64 từ JSON
    const byteCharacters = atob(base64String);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    const blob = new Blob([byteArray], {
      type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
    });

    // Mở hộp thoại lưu tệp để người dùng chọn thư mục và tên tệp
    const options = {
      suggestedName: res.data.fileName, // Tên tệp đề xuất
      types: [
        {
          description: "Excel Files",
          accept: {
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
              [".xlsx"],
          },
        },
      ],
    };

    const handle = await window.showSaveFilePicker(options);
    const writable = await handle.createWritable();
    await writable.write(blob);
    await writable.close();

    // Hiển thị thông báo export:
    let title = "Xuất thông tin nhân viên";
    let message = "Xuất thành công vui lòng kiểm tra file trong thư mục.";
    let callback = () => this.btnRefresh();
    this.displayNotice(res, title, message, callback);
  }

  /**
   * Nhập danh sách nhân viên bằng file excel
   * Author: Minh Hoàng (14/07/2024)
   */
  async btnImportOnClick(file) {
    // Tạo tham số đầu vào cho request:
    const formData = new FormData();
    formData.append("file", file);
    // Cấu hình option cho phương thức Post:
    let option = {
      method: "POST",
      body: formData,
    };
    // Gọi api thực hiện thêm mới:
    let res = await this.fetchData(
      "https://localhost:44357/api/v1/employees/import",
      option
    );
    // Hiển thị thông báo thêm:
    let title = "Nhập thông tin nhân viên";
    let message = `Nhập thành công ${res.data} nhân viên.`;
    let callback = () => this.btnRefresh();
    this.displayNotice(res, title, message, callback);
  }
  /**
   * Xóa các bản ghi được chọn trên bảng
   * Author: Minh Hoàng (14/07/2024)
   */
  btnRemoveMultiOnClick() {
    // Lấy ra nhưng row được chọn:
    const row = document.querySelectorAll(
      "#dataTable .table__record.table__record--selected"
    );
    // Duyệt lấy danh sách id của nhân viên trên row:
    let ids = [];
    for (const node of row) {
      // Lấy id:
      let id = node.getAttribute("id");
      ids.push(id);
    }
    // Thông báo khi chưa có row nào được chọn:
    if (ids.length <= 0) {
      // Hiển thị thông báo lỗi chưa chọn:
      let title = "Xóa thông tin nhân viên";
      let errors = ["Vui lòng chọn nhân viên trước khi xóa!"];
      this.displayError(errors, title);
      return;
    }
    // Thông báo xác nhận xóa không:
    let title = "Xác nhận xóa nhân viên";
    let message = "Bạn có chắc muốn xóa không?";
    let callback = () => {
      this.handleRemoveMulti(ids);
    };
    this.displayConfirm(message, title, callback);
  }
  /**
   * Thực hiện thao tác xóa theo danh sách ids
   * Author: Minh Hoàng (14/07/2024)
   */
  async handleRemoveMulti(ids) {
    // Cấu hình option cho phương thức Delete:
    let option = {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(ids),
    };
    // Gọi api thực hiện xóa nhiều:
    let res = await this.fetchData(
      "https://localhost:44357/api/v1/employees/multi",
      option
    );
    // Hiển thị thông báo thêm:
    let title = "Xóa thông tin nhân viên";
    let message = `Thành công xóa ${res.data} nhân viên.`;
    let callback = () => this.btnRefresh();
    this.displayNotice(res, title, message, callback);
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
        input.setAttribute("guid", node.getAttribute("value"));
      });
    }
  }

  /**
   * Xử lý "Thêm mới" tại form thêm mới nhân viên để thêm mới nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async handleAppend() {
    // Tạo tham số đầu vào cho request:
    const data = this.mockData();
    // Cấu hình option cho phương thức Post:
    let option = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    };
    // Gọi api thực hiện thêm mới:
    let res = await this.fetchData(
      "https://localhost:44357/api/v1/employees",
      option
    );
    // Hiển thị thông báo thêm:
    let title = "Thêm nhân viên";
    let message = "Thêm nhân viên thành công.";
    let callback = () => this.btnRefresh();
    this.displayNotice(res, title, message, callback);
  }

  /**
   * Xử lý xóa một nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async handleDelete(node) {
    // Lấy id nhân viên tại hàng muốn xóa:
    let id = node.parentElement.parentElement.parentElement.getAttribute("id");
    // Cấu hình option cho phương thức Delete:
    let option = {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
      },
    };
    // Gọi api xóa dữ liệu nhân viên:
    let res = await this.fetchData(
      `https://localhost:44357/api/v1/employees?id=${id}`,
      option
    );
    // Kiểm tra trạng thái request xóa nhân viên:
    if (!(res.success === true)) {
      // Hiển thị thông báo lỗi:
      this.displayError(res.errors);
      console.error(res.errors);
      return;
    }
    // Hiển thị thông báo thêm:
    let title = "Xóa nhân viên";
    let message = "Xóa nhân viên thành công.";
    let callback = () => this.loadData();
    this.displayNotice(res, title, message, callback);
  }

  /**
   * Xử lý sửa một nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async handleUpdate() {
    // Tạo tham số đầu vào cho request:
    const data = this.mockData();
    // Cấu hình option cho phương thức Put:
    let option = {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    };
    // Gọi api cập nhật thông tin nhân viên hàng:
    let res = await this.fetchData(
      `https://localhost:44357/api/v1/employees`,
      option
    );
    // Hiển thị thông báo cập nhật:
    let title = "Cập nhật nhân viên";
    let message = "Cập nhật nhân viên thành công.";
    let callback = () => this.btnRefresh();
    this.displayNotice(res, title, message, callback);
  }

  /**
   * Xử lý sao chép thông tin nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  async handleCopy(node) {
    // Lấy id nhân viên tại hàng muốn cập nhật:
    let id = node.parentElement.parentElement.parentElement.getAttribute("id");
    // Gọi api lấy thông tin nhân viên hàng:
    let res = await this.fetchData(
      `https://localhost:44357/api/v1/employees/${id}`
    );
    // Kiểm tra trạng thái request thông tin nhân viên:
    if (!(res.success === true)) {
      console.error(res.errors);
      return;
    }
    var employee = res.data;
    // Tạo chuỗi text:
    var textToCopy = `
      Mã nhân viên: ${employee.employeeCode}
      Tên nhân viên: ${employee.fullName}
      Giới tính: ${employee.genderName}
      Ngày sinh: ${this.formatDate(employee.dateOfBirth)}
      Địa chỉ Email: ${employee.email}
      Địa chỉ: ${employee.address}
    `;
    navigator.clipboard
      .writeText(textToCopy)
      .then(() => {
        alert(textToCopy);
      })
      .catch((err) => {
        console.error("Lỗi khi sao chép: ", err);
      });
  }

  /**
   * Hiển thị form
   * Author: Minh Hoàng (14/07/2024)
   */
  displayForm() {
    // Lấy ra element của form thêm mới:
    const form = document.querySelector("#formDetail");
    // Set hiển thị form:
    form.parentElement.style.visibility = "visible";
    // focus vào input đầu tiên:
    form.querySelector("input.input__data:not([disabled])").focus();
    //...
  }

  /**
   * Hiển thị thông báo sau khi gọi api
   * Author: Minh Hoàng (14/07/2024)
   */
  displayNotice(res, title, message, callback) {
    // Hiển thị thông báo:
    const dialogNotice = document.querySelector("#notice");
    dialogNotice.parentElement.style.visibility = "visible";
    // Thay đổi tiêu đề thông báo:
    dialogNotice.querySelector(".modal__header-title").innerHTML = title;
    // Kiểm tra trạng thái request:
    if (res.success === true) {
      // Thay đổi chi tiêt thông báo:
      dialogNotice.querySelector(".modal__body").innerHTML = message;
      // Lấy ra element của form:
      const form = document.querySelector("#formDetail");
      // Set ẩn form:
      form.parentElement.style.visibility = "hidden";
      // Chạy hành động thêm:
      callback();
    } else {
      // Duyệt từng nội dung lỗi:
      if (Array.isArray(res.errors)) {
        let li = res.errors.reduce((acc, cur) => {
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
      } else {
        dialogNotice.querySelector(".modal__body").innerHTML =
          "Lỗi không xác định!";
      }
      console.error(res.errors);
    }
  }
  /**
   * Hiển thị thông báo xác nhận trước khi gọi api
   * Author: Minh Hoàng (14/07/2024)
   */
  displayConfirm(message, title, callback) {
    // Hiển thị thông báo:
    const dialogNotice = document.querySelector("#confirm");
    dialogNotice.parentElement.style.visibility = "visible";
    // Thay đổi tiêu đề thông báo:
    dialogNotice.querySelector(".modal__header-title").innerHTML = title;
    // Thay đổi chi tiêt thông báo:
    dialogNotice.querySelector(".modal__body").innerHTML = message;
    // Sự kiện đồng ý:
    dialogNotice
      .querySelector(".button--approve")
      .addEventListener("click", () => {
        callback();
        dialogNotice.parentElement.style.visibility = "hidden";
      });
  }
  /**
   * Hiển thị thông báo các lỗi
   * Author: Minh Hoàng (14/07/2024)
   */
  displayError(errors, title) {
    // Hiển thị thông báo lên:
    const dialogNotice = document.querySelector("#notice");
    dialogNotice.parentElement.style.visibility = "visible";
    // Thay đổi tiêu đề thông báo:
    dialogNotice.querySelector(".modal__header-title").innerHTML = title;
    // Duyệt từng nội dung thông báo:
    let li = errors.reduce((acc, cur) => {
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
      const inputEmail = document.querySelector("#formDetail #txtEmail");
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
   * Mock dữ liệu từ form nhân viên
   * Author: Minh Hoàng (14/07/2024)
   */
  mockData() {
    // Lấy dữ liệu từ form thêm nhân viên:
    let employeeCode = document.querySelector("#txtEmployeeCode").value;
    let fullname = document.querySelector("#txtFullname").value;
    let birthday = document.querySelector("#dateBirthday").value;
    let gender = document.querySelector("#radioGender input[checked]").value;
    let position = document.querySelector("#cbPosition").getAttribute("guid");
    let identityNumber = document.querySelector("#txtIdentityNumber").value;
    let identityDate = document.querySelector("#dateIdentityDate").value;
    let department = document
      .querySelector("#cbDepartment")
      .getAttribute("guid");
    let identityPlace = document.querySelector("#txtIdentityPlace").value;
    let address = document.querySelector("#txtAddress").value;
    let phoneNumber = document.querySelector("#txtPhoneNumber").value;
    let landlineNumber = document.querySelector("#txtLandlineNumber").value;
    let email = document.querySelector("#txtEmail").value;
    let bankAccount = document.querySelector("#txtBankAccount").value;
    let bankName = document.querySelector("#txtBankName").value;
    let branch = document.querySelector("#txtBranch").value;

    // Tạo tham số đầu vào cho request:
    return {
      departmentId: this.convertEmpty(department),
      positionId: this.convertEmpty(position),
      employeeCode: this.convertEmpty(employeeCode),
      fullName: this.convertEmpty(fullname),
      dateOfBirth: this.convertDate(birthday),
      gender: parseInt(gender),
      identityNumber: this.convertEmpty(identityNumber),
      identityDate: this.convertDate(identityDate),
      identityPlace: this.convertEmpty(identityPlace),
      address: this.convertEmpty(address),
      phoneNumber: this.convertEmpty(phoneNumber),
      landlineNumber: this.convertEmpty(landlineNumber),
      email: this.convertEmpty(email),
      bankAccount: this.convertEmpty(bankAccount),
      bankName: this.convertEmpty(bankName),
      branch: this.convertEmpty(branch),
    };
  }
  /**
   * Reset form điền về mặc định
   * Author: Minh Hoàng (14/07/2024)
   */
  resetForm() {
    const form = document.querySelector("#formDetail");
    const inputs = form.querySelectorAll('input:not([type="radio"])');
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
    form.querySelector('input[type="radio"]').checked = true;
    // Xóa disable cho field mã:
    form.querySelector("#txtEmployeeCode").removeAttribute("disabled");
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
      const inputs = document.querySelectorAll("#formDetail input[required]");
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
  async fetchData(url, option) {
    // Hiện loading trước khi gọi api:
    this.spinner(true);
    try {
      let response = await fetch(url, option);
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
  /**
   * Dịnh dạng kiểu Date => (dd/MM/yyyy)
   * Author: Minh Hoàng (14/07/2024)
   */
  formatDate(value) {
    if (value === null) {
      return "";
    }
    let date = new Date(value);
    let day = date.getDate().toString().padStart(2, "0");
    let month = (date.getMonth() + 1).toString().padStart(2, "0");
    let year = date.getFullYear();
    return `${day}/${month}/${year}`;
  }
  /**
   * Dịnh dạng kiểu Date => (yyyy-MM-dd)
   * Author: Minh Hoàng (14/07/2024)
   */
  formatFormDate(value) {
    if (value === null) {
      return "";
    }
    let date = new Date(value);
    let day = date.getDate().toString().padStart(2, "0");
    let month = (date.getMonth() + 1).toString().padStart(2, "0");
    let year = date.getFullYear();
    return `${year}-${month}-${day}`;
  }
  /**
   * Truyển đổi giá trị từ input date thành kiểu Date
   * Author: Minh Hoàng (14/07/2024)
   */
  convertDate(value) {
    const date = new Date(value);
    // Kiểm tra xem giá trị của date có hợp lệ hay không
    if (isNaN(date.getTime())) {
      return null; // Nếu không hợp lệ, trả về null
    }
    return date; // Nếu hợp lệ, trả về đối tượng Date
  }
  /**
   * Chuyển giá trị trống nếu có thành null
   * Author: Minh Hoàng (14/07/2024)
   */
  convertEmpty(value) {
    if (value === "" || value === null || value === undefined) {
      return null;
    }
    return value;
  }
}
