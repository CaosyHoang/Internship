namespace Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Lấy ra toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi hoặc list rỗng</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<IEnumerable<T>> GetAsync();
        /// <summary>
        /// Lấy ra bản ghi theo id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Bản ghi hoặc null</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<T?> GetAsync(Guid id);
        /// <summary>
        /// Chèn một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Nếu thành công trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> InsertAsync(T entity);
        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Nếu thành công trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> UpdateAsync(T entity);
        /// <summary>
        /// Xóa một bản ghi theo id
        /// </summary>
        /// <param name="id">Đối tượng</param>
        /// <returns>Nếu thành công trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> DeleteAsync(Guid id);
        /// <summary>
        /// Xóa nhiều bản ghi theo danh sach id (sử dụng transaction)
        /// </summary>
        /// <param name="ids">Danh sách Id</param>
        /// <returns>Nếu thành công trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> DeleteMultiAsync(List<Guid> ids);
        /// <summary>
        /// Kiểm tra bản ghi tồn tại
        /// </summary>
        /// <param name="code">Mã</param>
        /// <returns>Nếu tồn tại trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<bool> CheckExistAsync(string code);
        /// <summary>
        /// Đếm số bản ghi
        /// </summary>
        /// <returns>Tổng số bản ghi</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<int> CountRecordAsync();
        /// <summary>
        /// Lấy ra các bản ghi
        /// </summary>
        /// <param name="limit">Số bản ghi tối đa >= 1</param>
        /// <param name="number">Trang số >= 1 mặc định là 1</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<IEnumerable<T>> GetAsync(int limit, int number);
    }
}
