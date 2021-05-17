using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Models
{

    public class Result
    {

    }
    public class Result<T> : Result
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string[] SuccessMessages { get; set; }

        internal Result(bool succeeded, IEnumerable<string> errors, T data, IEnumerable<string> successMsg)
        {
            IsSuccess = succeeded;
            Errors = errors.ToArray();
            Data = data;
            SuccessMessages = successMsg.ToArray();
        }
        public static Result<T> Success(T Data)
        {
            return new Result<T>(true, new string[] { }, Data, new string[] { });
        }

        public static Result<T> Success(T Data, IEnumerable<string> successMsg)
        {
            return new Result<T>(true, new string[] { }, Data, successMsg);
        }

        public static Result<T> Success(IEnumerable<string> successMsg)
        {
            return new Result<T>(true, new string[] { }, default(T), successMsg);
        }

        public static Result<T> Success(string successMsg)
        {
            return new Result<T>(true, new string[] { }, default(T), new string[] { successMsg });
        }

        public static Result<T> Success()
        {
            return new Result<T>(true, new string[] { }, default(T), new string[] { });
        }

        internal static Result<Unit> Success(object localizationKeys)
        {
            throw new NotImplementedException();
        }

        public static Result<T> Failure(IEnumerable<string> errors)
        {
            return new Result<T>(false, errors, default(T), new string[] { });
        }
        public static Result<T> Failure(string errors)
        {
            return new Result<T>(false, new[] { errors }, default(T), new string[] { });
        }
    }
}
