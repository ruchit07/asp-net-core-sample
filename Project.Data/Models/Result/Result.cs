using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Project.Data.Models
{
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Message { get; set; }
        public T Model { get; set; }

        public Result()
        {
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
            StatusMessage = nameof(HttpStatusCode.OK);
        }

        public Result(bool isSuccess, HttpStatusCode httpStatusCode)
        {
            IsSuccess = isSuccess;
            StatusCode = httpStatusCode;
            StatusMessage = httpStatusCode.ToString();
        }

        public Result(bool isSuccess, HttpStatusCode httpStatusCode, string message)
        {
            IsSuccess = isSuccess;
            StatusCode = httpStatusCode;
            StatusMessage = httpStatusCode.ToString();
            Message = message;
        }

        public Result(bool isSuccess, HttpStatusCode httpStatusCode, string message, T model)
        {
            IsSuccess = isSuccess;
            StatusCode = httpStatusCode;
            StatusMessage = httpStatusCode.ToString();
            Message = message;
            Model = model;
        }

        public Result(bool isSuccess, HttpStatusCode httpStatusCode, T model)
        {
            IsSuccess = isSuccess;
            StatusCode = httpStatusCode;
            StatusMessage = httpStatusCode.ToString();
            Model = model;
        }
    }
}
