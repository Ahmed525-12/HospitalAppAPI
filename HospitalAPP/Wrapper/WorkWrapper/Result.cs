using HospitalAPP.Wrapper.Interface;

namespace HospitalAPP.Wrapper.WorkWrapper
{
    public class ResultResponse : IResult
    {
        public ResultResponse()
        {
        }

        public List<string> Messages { get; set; } = new List<string>();

        public bool Succeeded { get; set; }

        public static IResult Fail()
        {
            return new ResultResponse { Succeeded = false };
        }

        public static IResult Fail(string message)
        {
            return new ResultResponse { Succeeded = false, Messages = new List<string> { message } };
        }

        public static IResult Fail(List<string> messages)
        {
            return new ResultResponse { Succeeded = false, Messages = messages };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static IResult Success()
        {
            return new ResultResponse { Succeeded = true };
        }

        public static IResult Success(string message)
        {
            return new ResultResponse { Succeeded = true, Messages = new List<string> { message } };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class ResultResponse<T> : ResultResponse, IResult<T>
    {
        public ResultResponse()
        {
        }

        public T Data { get; set; }

        public new static ResultResponse<T> Fail()
        {
            return new ResultResponse<T> { Succeeded = false };
        }

        public new static ResultResponse<T> Fail(string message)
        {
            return new ResultResponse<T> { Succeeded = false, Messages = new List<string> { message } };
        }

        public new static ResultResponse<T> Fail(List<string> messages)
        {
            return new ResultResponse<T> { Succeeded = false, Messages = messages };
        }

        public new static Task<ResultResponse<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<ResultResponse<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public new static Task<ResultResponse<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public new static ResultResponse<T> Success()
        {
            return new ResultResponse<T> { Succeeded = true };
        }

        public new static ResultResponse<T> Success(string message)
        {
            return new ResultResponse<T> { Succeeded = true, Messages = new List<string> { message } };
        }

        public static ResultResponse<T> Success(T data)
        {
            return new ResultResponse<T> { Succeeded = true, Data = data };
        }

        public static ResultResponse<T> Success(T data, string message)
        {
            return new ResultResponse<T> { Succeeded = true, Data = data, Messages = new List<string> { message } };
        }

        public static ResultResponse<T> Success(T data, List<string> messages)
        {
            return new ResultResponse<T> { Succeeded = true, Data = data, Messages = messages };
        }

        public new static Task<ResultResponse<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<ResultResponse<T>> SuccessAsync(T data)
        {
            return Task.FromResult(Success(data));
        }

        public static Task<ResultResponse<T>> SuccessAsync(T data, string message)
        {
            return Task.FromResult(Success(data, message));
        }
    }
}