using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Together.Attributes.ActionFilters;
using Xunit;

namespace Together.Attributes.Tests
{
    public class ValidateModelAttributeTest
    {
        private readonly Mock<ActionExecutingContext> _actionExecutingContextMock;
        public ValidateModelAttributeTest()
        {
            _actionExecutingContextMock = new Mock<ActionExecutingContext>();
        }
        [Fact]
        public void Model_value_invalid_return_errors()
        {
            // Arrange
            _actionExecutingContextMock.Object.ModelState.AddModelError("Test", "This Field invalid");
            // Act
            var attribute = new ValidateModelAttribute();
            attribute.OnActionExecuting(_actionExecutingContextMock.Object);
            // Assert
            var result = _actionExecutingContextMock.Object.Result;
        }

        private ModelStateDictionary FakeModelState()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Test", "This Field invalid");

            return modelState;
        }
    }
}
