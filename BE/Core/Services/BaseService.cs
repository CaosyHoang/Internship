using AutoMapper;
using Core.DTOs;
using Core.Exceptions;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        #region Declaration

        protected IBaseRepository<T> _repository;
        protected readonly IMapper _mapper;

        #endregion

        #region Property
        #endregion

        #region Constructor

        public BaseService(IBaseRepository<T> repositoty, IMapper mapper)
        {
            _repository = repositoty;
            _mapper = mapper;
        }

        #endregion

        #region Method

        public async Task<ResultDetails> CountData()
        {
            var res = await _repository.CountRecordAsync();
            return new ResultDetails
            {
                Success = true,
                Data = res,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async Task<ResultDetails> InsertAsync(T entity)
        {
            // Xử lý nghiệp vụ trước khi thêm mới dữ liệu:
            await ValidateObject(entity);
            // Thêm mới dữ liệu:
            var res = await _repository.InsertAsync(entity);
            if (res == false)
            {
                throw new ValidateException(Resource.ExceptionsResource.Insert_Error_Exception);
            }
            return new ResultDetails
            {
                Success = true,
                Data = res,
                StatusCode = (int)HttpStatusCode.Created,
            };
        }

        public async Task<ResultDetails> LoadDataAsync<Y>(int limit, int number)
        {
            // Kiểm tra hợp lệ:
            var count = await _repository.CountRecordAsync();
            var page = Math.Ceiling(count / (double)limit);
            if (limit <= 0)
            {
                throw new ValidateException(Resource.ExceptionsResource.LimitPage_Exception);
            }
            else if (number <= 0 || number > page)
            {
                throw new ValidateException(Resource.ExceptionsResource.NumPage_Exception);
            }
            // Lấy dữ liệu:
            var res = await _repository.GetAsync(limit, number);
            return new ResultDetails
            {
                Success = true,
                Data = _mapper.Map<List<Y>>(res),
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        protected virtual Task ValidateObject(T entity)
        {
            return Task.CompletedTask;
        }

        public Y MappingObject<Y>(object entity)
        {
            var res = _mapper.Map<Y>(entity);
            Console.WriteLine(res);
            return res;
        }

        public async Task<ResultDetails> GetAllAsync<Y>()
        {
            var res = await _repository.GetAsync();
            return new ResultDetails
            {
                Success = true,
                Data = _mapper.Map<List<Y>>(res),
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async Task<ResultDetails> DeleteAsync(Guid id)
        {
            var res = await _repository.DeleteAsync(id);
            if (res == false)
            {
                throw new ValidateException(Resource.ExceptionsResource.Delete_Error_Exception);
            }
            return new ResultDetails
            {
                Success = true,
                Data = res,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async Task<ResultDetails> GetAsync<Y>(Guid id)
        {
            var res = await _repository.GetAsync(id);
            return new ResultDetails
            {
                Success = true,
                Data = _mapper.Map<Y>(res),
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async  Task<ResultDetails> UpdateAsync(T entity)
        {
            // Sửa dữ liệu:
            var res = await _repository.UpdateAsync(entity);
            if (res == false)
            {
                throw new ValidateException(Resource.ExceptionsResource.Update_Error_Exception);
            }
            return new ResultDetails
            {
                Success = true,
                Data = res,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        public async Task<ResultDetails> DeleteMultiAsync(List<Guid> ids)
        {
            var res = await _repository.DeleteMultiAsync(ids);
            if (res == false)
            {
                throw new ValidateException(Resource.ExceptionsResource.Delete_Error_Exception);
            }
            return new ResultDetails
            {
                Success = true,
                Data = ids.Count,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }

        #endregion
    }
}
