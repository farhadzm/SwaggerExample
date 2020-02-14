using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_Example.Web.SwaggerConfig
{
    public class AuthenticationDocumentFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationDocumentFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            if (!httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                swaggerDoc.Definitions = new Dictionary<string, Schema>();
                swaggerDoc.Paths = new Dictionary<string, PathItem>();
            }
        }
    }
}
