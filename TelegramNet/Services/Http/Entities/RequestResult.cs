using System.Text.Json;
using TelegramNet.ExtraTypes;

namespace TelegramNet.Services.Http.Entities
{
    public class RequestResult<T>
    {
        internal RequestResult(HttpResult result)
        {
            Ok = result.Ok;

            Description = new Optional<string>(result.Description);
            ErrorCode = new Optional<int>(result.ErrorCode);
            if (Ok) Result = new Optional<T>(JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(result.Result)));
        }

        public bool Ok { get; internal init; }

        public Optional<string> Description { get; internal init; }

        public Optional<int> ErrorCode { get; internal init; }

        public Optional<T> Result { get; internal init; }

        public bool IsSuccess()
        {
            return Ok && Result.HasValue;
        }

        public T EnsureSuccess()
        {
            if (IsSuccess()) return Result.Value;

            throw new EnsureFailedException<T>(Description.Value ?? string.Empty);
        }
    }
}