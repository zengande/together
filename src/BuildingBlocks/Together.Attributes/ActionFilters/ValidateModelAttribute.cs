using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using Together.Attributes.ActionResults;
using Together.Attributes.FlagAttributes;

namespace Together.Attributes.ActionFilters
{
    /// <summary>
    /// 模型验证过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class ValidateModelAttribute
        : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // 判断是否跳过验证
            if (context.ActionDescriptor.IsDefined(typeof(IgnoreModelStateValidationAttribute), false))
            {
                return;
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }

    public static class ActionDescriptorExtensions
    {
        public static bool IsDefined(this ActionDescriptor actionDescriptor, Type attribute, bool inherit = true)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                return controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit)
                    .Any(a => a.GetType().Equals(attribute));
            }
            return false;
        }
    }
}
