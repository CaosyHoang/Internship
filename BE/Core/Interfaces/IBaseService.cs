using Core.DTOs;

namespace Core.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> GetAllAsync();
        /// <summary>
        /// Xóa một bản ghi bất kỳ
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> DeleteAsync(Guid id);
        /// <summary>
        /// Chèn một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng</param>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> InsertAsync(T entity);
        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <param name="limit">Số bản ghi tối đa >= 1</param>
        /// <param name="number">Trang số >= 1</param>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> LoadDataAsync(int limit, int number);
        /// <summary>
        /// Đếm số lượng bản ghi
        /// </summary>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> CountData();
        /// <summary>
        /// Ánh xạ đối tượng
        /// </summary>
        /// <typeparam name="Y">Kiểu trả về</typeparam>
        /// <param name="entity">Đối tượng ánh xạ</param>
        /// <returns>Kết quả ánh xạ</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Y MappingObject<Y>(object entity);
    }
}
