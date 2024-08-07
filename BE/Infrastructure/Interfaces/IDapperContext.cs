using System.Data;

namespace Infrastructure.Interfaces
{
    public interface IDapperContext
    {
        /// <summary>
        /// Một kết nối mở đến cơ sở dữ liệu
        /// </summary>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        IDbConnection Connection { get; }
        /// <summary>
        /// Một phiên giao dịch được thực hiện tại một luồng dữ liệu
        /// </summary>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        IDbTransaction? Transaction { get; set; }
        /// <summary>
        /// Lấy ra toàn bộ bản ghi
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <returns>Danh sách bản ghi hoặc list rỗng</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<IEnumerable<T>> GetAsync<T>();
        /// <summary>
        /// Lấy ra bản ghi theo id
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <param name="id">Id</param>
        /// <returns>Bản ghi hoặc null</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<T?> GetAsync<T>(Guid id);
        /// <summary>
        /// Chèn một bản ghi
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Số bản ghi thêm</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<int> InsertAsync<T>(T entity);
        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <typeparam name="T">Kiểu đôi tượng</typeparam>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Số bản ghi sửa</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<int> UpdateAsync<T>(T entity);
        /// <summary>
        /// Xóa một bản ghi theo id
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <param name="id">Đối tượng</param>
        /// <returns>Số bản ghi xóa</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<int> DeleteAsync<T>(Guid id);
        /// <summary>
        /// Kiểm tra bản ghi tồn tại
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <param name="code">Mã</param>
        /// <returns>Nếu tồn tại trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> CheckExistAsync<T>(string code);
        /// <summary>
        /// Đếm số bản ghi
        /// </summary>
        /// <typeparam name="T">Kiểu generic</typeparam>
        /// <returns>Tổng số bản ghi</returns>
        Task<int> CountRecordAsync<T>();
        /// <summary>
        /// Lấy ra các bản ghi
        /// </summary>
        /// <typeparam name="T">Kiểu gereric</typeparam>
        /// <param name="limit">Số bản ghi tối đa >= 1</param>
        /// <param name="number">Trang số >= 1 mặc định là 1</param>
        /// <returns>Danh sách bản ghi</returns>
        Task<IEnumerable<T>> GetAsync<T>(int limit, int? number);
    }
}
