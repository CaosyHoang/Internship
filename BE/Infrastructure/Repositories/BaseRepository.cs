using Core.Exceptions;
using Core.Interfaces;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        #region Declaration

        protected IDapperContext _context;

        #endregion

        #region Constructor

        protected BaseRepository(IDapperContext context) =>
            _context = context;

        #endregion

        #region Method

        public async Task<bool> DeleteAsync(Guid id)
        {
            var res = await _context.DeleteAsync<T>(id);
            return res != 0;
        }

        public async Task<bool> DeleteMultiAsync(List<Guid> ids)
        {
            // Bắt đầu một phiên giao dịch:
            _context.Connection.Open();
            _context.Transaction = _context.Connection.BeginTransaction();
            // Duyệt từng id:
            foreach (var id in ids)
            {
                if (await _context.DeleteAsync<T>(id) != 0)
                {
                    // Khôi phục lại trạng thái ban đầu:
                    _context.Transaction.Rollback();
                    return false;
                    throw new ValidateException(Core.Resource.ExceptionsResource.Delete_Error_Exception);
                }
            }
            // Lưu thay đổi:
            _context.Transaction.Commit();
            return true;
        }

        public void Dispose()
        {
            _context.Connection.Close();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            var res = await _context.GetAsync<T>();
            return res;
        }

        public async Task<T?> GetAsync(Guid id)
        {
            var res = await _context.GetAsync<T>(id);
            return res;
        }

        public async Task<bool> CheckExistAsync(string code)
        {
            var res = await _context.CheckExistAsync<T>(code);
            return res;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            var res = await _context.InsertAsync<T>(entity);
            return res != 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var res = await _context.UpdateAsync<T>(entity);
            return res != 0;
        }

        public async Task<int> CountRecordAsync()
        {
            var res = await _context.CountRecordAsync<T>();
            return res;
        }

        public async Task<IEnumerable<T>> GetAsync(int limit, int number)
        {
            var res = await _context.GetAsync<T>(limit, number);
            return res;
        }

        public async Task<bool> InsertMultiAsync(List<T> entities)
        {
            // Bắt đầu một phiên giao dịch:
            _context.Connection.Open();
            _context.Transaction = _context.Connection.BeginTransaction();
            // Duyệt từng entity:
            foreach (var entity in entities)
            {
                if (await _context.InsertAsync(entity) != 0)
                {
                    // Khôi phục lại trạng thái ban đầu:
                    _context.Transaction.Rollback();
                    return false;
                    throw new ValidateException(Core.Resource.ExceptionsResource.Insert_Error_Exception);
                }
            }
            // Lưu thay đổi:
            _context.Transaction.Commit();
            return true;
        }

        #endregion
    }
}
