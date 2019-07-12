using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Together.Attributes.Models;

namespace Together.Attributes.ActionResults
{
    public class ValidationFailedResult
        : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
           : base(new ValidationResultModel(modelState))
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity;
        }
    }
}
