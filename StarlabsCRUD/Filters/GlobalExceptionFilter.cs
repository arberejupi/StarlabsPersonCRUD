using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using StarlabsCRUD.Services;
namespace StarlabsCRUD.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is StarlabsCRUD.Services.ValidationException validationException)
            {
                context.Result = new BadRequestObjectResult(validationException.Errors);
                context.ExceptionHandled = true;
            }
        }
    }
}
