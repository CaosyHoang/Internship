using Core.Exceptions;

namespace Core.Helpers
{
    public static class Transfer
    {
        /// <summary>
        /// Xử lý định dạng Datetime
        /// </summary>
        /// <param name="date">Chuỗi date (dd/MM/yyyy)</param>
        /// <returns>Kết quả kiểu Datetime</returns>
        public static DateTime ProcessDateTime(string date)
        {
            // Tách chuỗi ngày tháng thành từng phần để thực hiện định dạng:
            string[] parts = date.Split('/');
            // Thêm số 0 nếu cần:
            string day = parts[0].PadLeft(2, '0');
            string month = parts[1].PadLeft(2, '0');
            string year = parts[2];
            // Kết quả định dạng(dd/MM/yyyy):
            string formattedDate = $"{day}/{month}/{year}";
            DateTime res;
            string format = "dd/MM/yyyy";
            if (DateTime.TryParseExact(
                formattedDate,
                format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out res))
            {
                return res;
            }
            else
            {
                throw new ValidateException(Resource.ExceptionsResource.Date_Error_Exception);
            }
        }
        /// <summary>
        /// Chuyển đổi excelDate về datetime
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Kết quả kiểu Datetime</returns>
        public static DateTime? ProcessExcelDateToDateTime(string value)
        {
            int excelDate;
            if (Int32.TryParse(value, out excelDate))
            {
                // Excel sử dụng số ngày kể từ ngày gốc 1/1/1900
                // Kiểm tra xem giá trị có phải là ngày tháng hay không
                if (excelDate >= 1)
                {
                    DateTime baseDate = new DateTime(1900, 1, 1);
                    DateTime res = baseDate.AddDays(excelDate - 2); // -2 vì Excel coi 1900 là năm nhuận
                    return res;
                }
            }
            return null;
        }
    }
}
