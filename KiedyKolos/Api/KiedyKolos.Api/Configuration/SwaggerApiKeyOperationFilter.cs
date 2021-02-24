using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace KiedyKolos.Api.Configuration
{
    public class SwaggerApiKeyOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Api-Key",
                    In = ParameterLocation.Header,
                    Description = "Api key for authorization",
                    Required = false
                });
            }
        }
    }
}