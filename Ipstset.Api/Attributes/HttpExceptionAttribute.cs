using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Ipstset.Api.Models;
using Ipstset.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ipstset.Api.Attributes
{
    public class HttpExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.Result = new ObjectResult(new ValidationErrorModel
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Code = 1001,
                    Message = "Validation failed",
                    Errors = ((ValidationException)context.Exception).ValidationErrors

                })
                { StatusCode = (int)HttpStatusCode.BadRequest };
                return;
            }

            if (context.Exception is NotAuthorizedException)
            {
                context.Result = new ObjectResult(new ErrorModel
                {
                    Status = (int)HttpStatusCode.Forbidden,
                    Code = 1002,
                    Message = "The user does not have the required permissions to access the resource."
                })
                { StatusCode = (int)HttpStatusCode.Forbidden };
                return;
            }


            if (context.Exception is NotFoundException)
            {
                context.Result = new ObjectResult(new ErrorModel
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Code = 1003,
                    Message = context.Exception.Message
                })
                { StatusCode = (int)HttpStatusCode.NotFound };
                return;
            }

            if (context.Exception is BadRequestException)
            {
                context.Result = new ObjectResult(new ErrorModel
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Code = 1004,
                        Message = "The request contains malformed syntax or is missing required parameters."

                })
                    { StatusCode = (int)HttpStatusCode.BadRequest };
                return;
            }

            //if (context.Exception is InternalServerErrorException)
            //{
            //    context.Result = new ObjectResult(new ErrorModel
            //    {
            //        Status = (int)HttpStatusCode.InternalServerError,
            //        Code = 1003,
            //        Message = context.Exception.Message
            //    })
            //    { StatusCode = (int)HttpStatusCode.InternalServerError };
            //}

            //Unhandled
            context.Result = new ObjectResult(new ErrorModel
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Code = 1000,
                //Message = context.Exception.Message
            })
            { StatusCode = (int)HttpStatusCode.InternalServerError };
        }


    }
}
