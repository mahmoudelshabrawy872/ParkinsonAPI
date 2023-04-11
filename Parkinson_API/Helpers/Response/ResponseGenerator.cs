﻿using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Parkinson_API.Helpers.Response
{
    public class ResponseGenerator<T>
    {
        private readonly HttpStatusCode _statusCodes;
        private readonly bool _success;
        private readonly T _data;
        private readonly List<ResponseErrorMessage> _errorMessages;
        private readonly List<IdentityError> _identityErrors;

        public ResponseGenerator(HttpStatusCode statusCodes, bool success, T data, List<ResponseErrorMessage> errorMessages)
        {
            _success = success;
            _data = data;
            _errorMessages = errorMessages;
            _statusCodes = statusCodes;
        }
        //public ResponseGenerator(HttpStatusCode statusCodes, bool success, T data, List<IdentityError> identityErrors)
        //{
        //    _success = success;
        //    _data = data;
        //    _identityErrors = identityErrors;
        //    _statusCodes = statusCodes;
        //}


        public ResponseDto<T> Generate()
        {
            var Result = new ResponseDto<T>()
            {
                StatusCode = _statusCodes,
                Success = _success,
                Data = new ResponseData<T>()
                {
                    InnerData = _data,
                    Type = typeof(T).ToString(),
                },
                Errors = new List<ResponseError>()
                {
                    new () {Domain = "", Messages = _errorMessages }
                },
            };

            return Result;
        }




    }
}
