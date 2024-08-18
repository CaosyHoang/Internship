using Microsoft.AspNetCore.Http;

namespace Core.Exceptions
{
    public static class Filter
    {
        /// <summary>
        /// Kiểm tra file excel có hợp lệ không
        /// </summary>
        /// <param name="file">File excel dữ liệu nhập khẩu</param>
        /// <returns>Trả về true nếu file excel hợp lệ và ngược lại</returns>
        public static void CheckFileImport(IFormFile file)
        {
            // Kiểm tra tệp có được gửi lên hay không:
            if (file == null || file.Length == 0)
            {
                throw new ValidateException(Resource.ExceptionsResource.Null_File_Exception);
            }
            // Kiểm tra loại tệp (phần mở rộng và MIME type):
            var fileExtension = Path.GetExtension(file.Name).ToLower();
            if (fileExtension != ".xlsx" && fileExtension != ".xls")
            {
                throw new ValidateException(Resource.ExceptionsResource.Excel_File_Invalid_Exception);
            }
            // Kiểm tra kích thước tệp (giới hạn 10MB):
            if (file.Length > 10 * 1024 * 1024)
            {
                throw new ValidateException(Resource.ExceptionsResource.Limit_File_Exception);
            }
        }
    }
}
