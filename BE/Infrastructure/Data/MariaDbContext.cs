using Core.Exceptions;
using Dapper;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using static Dapper.SqlMapper;

namespace Infrastructure.Data
{
    public class MariaDbContext : IDapperContext
    {
        #region Property

        public IDbConnection Connection { get; }
        public IDbTransaction? Transaction { get; set; }

        #endregion

        #region Constructor
        public MariaDbContext(IConfiguration config)
        {
            Connection = new MySqlConnection(config.GetConnectionString("mySqlConnection"));
        }
        #endregion

        #region Method
        public async Task<IEnumerable<T>> GetAsync<T>()
        {
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Select:
            var sql = $"SELECT * FROM {className}";
            // Thực thi:
            var res = await Connection.QueryAsync<T>(sql);
            return res;
        }

        public async Task<T?> GetAsync<T>(Guid id)
        {
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Select:
            var sql = $"SELECT * FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            // Thực thi:
            var res = await Connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            return res;
        }

        public async Task<int> InsertAsync<T>(T entity)
        {
            var propsKey = new List<string>();
            var ParamsValue = new List<string>();

            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Lấy ra các props của entity:
            var props = entity!.GetType().GetProperties();
            // Tạo đối tượng chứa tham số để chèn vào truy vấn:
            var parameters = new DynamicParameters();
            // Duyệt từng prop:
            foreach (var prop in props)
            {
                var propKey = prop.Name;
                propsKey.Add(propKey);
                ParamsValue.Add($"@{propKey}");
                parameters.Add($"@{propKey}", prop.GetValue(entity));
            }
            // Build câu lệnh Insert:
            var sql =
                $"INSERT INTO {className} ({String.Join(", ", propsKey)}) " +
                $"VALUES ({String.Join(", ", ParamsValue)})";
            // Thực thi:
            var res = await Connection.ExecuteAsync(sql, parameters, transaction: Transaction);
            return res;
        }

        public async Task<int> UpdateAsync<T>(T entity)
        {
            var modifies = new List<string>();

            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Lấy ra các props của entity:
            var props = entity!.GetType().GetProperties();
            // Lấy ra Id của entity:
            var id = entity!.GetType().GetProperty($"{className}Id");
            // Lấy ra code của entity:
            var code = entity!.GetType().GetProperty($"{className}Code");
            // Build câu lệnh Update:
            var parameters = new DynamicParameters();
            parameters.Add("@Code", code!.GetValue(entity));
            // Duyệt từng prop:
            foreach (var prop in props)
            {
                var value = prop.GetValue(entity);
                var name = prop.Name;
                if (prop == code || prop == id || value == null)
                {
                    continue;
                }
                modifies.Add($"{name} = @{name}");
                parameters.Add($"@{name}", value);
            }
            var sql = $"UPDATE {className} SET {String.Join(", ", modifies)} WHERE {className}Code = @Code";
            // Thực thi:
            var res = await Connection.ExecuteAsync(sql, parameters, transaction: Transaction);
            return res;
        }

        public async Task<int> DeleteAsync<T>(Guid id)
        {
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Delete:
            var sql = $"DELETE FROM {className} WHERE {className}Id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            // thực thi:
            var res = await Connection.ExecuteAsync(sql, parameters, transaction: Transaction);
            return res;
        }

        public async Task<bool> CheckExistAsync<T>(string code)
        {
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Select:
            var sql = $"SELECT EXISTS (SELECT 1 FROM {className} WHERE {className}Code = @code)";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);
            // Thực thi:
            var res = await Connection.QueryFirstOrDefaultAsync<bool>(sql, parameters);
            return res;
        }

        public async Task<int> CountRecordAsync<T>()
        {
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Select:
            var sql = $"SELECT COUNT(*) FROM {className}";
            // Thực thi:
            var res = await Connection.QueryFirstOrDefaultAsync<int>(sql);
            return res;
        }

        public async Task<IEnumerable<T>> GetAsync<T>(int limit, int? number = 1)
        {
            var offset = limit * (number - 1);
            // Lấy tên lớp đối tượng:
            var className = typeof(T).Name;
            // Build câu lệnh Select:
            var sql = $"SELECT * FROM {className} LIMIT {limit} OFFSET {offset}";
            // Thực thi:
            var res = await Connection.QueryAsync<T>(sql);
            return res;
        }

        #endregion
    }
}
