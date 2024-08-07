using System.Text.Json;

namespace Core.DTOs
{
    public class ResultDetails
    {
        /// <summary>
        /// Trạng thái thành công
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object? Data { get; set; }
        /// <summary>
        /// Trạng thái HTTP
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// Danh sách lỗi
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
