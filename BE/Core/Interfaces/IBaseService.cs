using Core.DTOs;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Lấy ra tất cả bản ghi
        /// </summary>
        ///<typeparam name="Y">Kiểu muốn trả về</typeparam>
        /// <returns>Trả về chi tiết kết quả</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> GetAllAsync<Y>();
        /// <summary>
        /// Lấy ra bản ghi theo id
        /// </summary>
        /// <typeparam name="Y">Kiểu muốn trả về</typeparam>
        /// <param name="id">Id</param>
        /// <returns>Bản ghi tìm thấy</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> GetAsync<Y>(Guid id);
        /// <summary>
        /// Xóa một bản ghi bất kỳ
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Trả về danh sách bản ghi</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> DeleteAsync(Guid id);
        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng sửa</param>
        /// <returns>Nếu thành công trả về true, ngược lại trả về false</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> UpdateAsync(T entity);
        /// <summary>
        /// Chèn một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng thêm</param>
        /// <returns>Trả về true - thành công và false - thất bại</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> InsertAsync(T entity);
        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <typeparam name="Y">Kiểu muốn trả về</typeparam>
        /// <param name="limit">Số bản ghi tối đa >= 1</param>
        /// <param name="number">Trang số >= 1</param>
        /// <returns>Trả về true - thành công và false - thất bại</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> LoadDataAsync<Y>(int limit, int number);
        /// <summary>
        /// Đếm số lượng bản ghi
        /// </summary>
        /// <returns>Trả về số lượng bản ghi của đối tượng</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Task<ResultDetails> CountData();
        /// <summary>
        /// Ánh xạ đối tượng
        /// </summary>
        /// <typeparam name="Y">Kiểu trả về</typeparam>
        /// <param name="entity">Đối tượng ánh xạ</param>
        /// <returns>Đối tượng ánh xạ</returns>
        /// CreatedBy: Minh Hoàng (08/05/2024)
        Y MappingObject<Y>(object entity);
    }
}
