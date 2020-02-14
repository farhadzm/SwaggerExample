using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swagger_Example.Web.Extensions
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
    public static class SwaggerExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                options.DocumentFilter<AuthenticationDocumentFilter>();
                options.SwaggerDoc("v1", new Info { Version = "v1", Title = "Swagger Example" });
            });
        }
        public static void UseSwaggerAndUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocExpansion(DocExpansion.None);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Example Docs");
            });
        }
    }
}
