using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Together.Mvc.Core.Models;

namespace Together.Attributes.Models
{
    public class ValidationResultModel : JsonResultModel<bool>
    {
        public List<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState, string message)
        {
            Success = false;
            ErrorMessage = message;
            ErrorCode = ErrorCodes.ModelStateValidationFailed;
            Errors = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                    .ToList();
        }
        public ValidationResultModel(ModelStateDictionary modelState)
            : this(modelState, "Validation Failed")
        {
        }
    }

    public class ValidationError
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
