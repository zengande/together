using System;

namespace Together.Attributes.FlagAttributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class IgnoreModelStateValidationAttribute
        : Attribute
    {
    }
}
