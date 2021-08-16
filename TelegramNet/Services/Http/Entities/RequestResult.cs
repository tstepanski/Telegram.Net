using System.Text.Json;

namespace TelegramNet.Services.Http.Entities
{
	public sealed class RequestResult<T>
	{
		internal RequestResult(HttpResult result)
		{
			Ok = result.Ok;

			Description = result.Description;
			ErrorCode = result.ErrorCode;

			if (!Ok)
			{
				return;
			}

			var resultJson = JsonSerializer.Serialize(result.Result);

			Result = JsonSerializer.Deserialize<T>(resultJson);
		}

		public bool Ok { get; internal init; }

		public string? Description { get; internal init; }

		public int? ErrorCode { get; internal init; }

		public T? Result { get; internal init; }

		public bool IsSuccess()
		{
			return Ok && Result != null;
		}

		public T EnsureSuccess()
		{
			if (IsSuccess())
			{
				return Result!;
			}

			throw new EnsureFailedException<T>(Description ?? string.Empty);
		}
	}
}