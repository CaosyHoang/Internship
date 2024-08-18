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
            DateTime res;
            string format = "dd/MM/yyyy";
            if (DateTime.TryParseExact(
                date,
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
    }
}
