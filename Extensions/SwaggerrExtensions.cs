using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DemoService.Extensions {

    public static class SwaggerrExtensions {

        public static void UseSwaggerUI(this IApplicationBuilder builder, IDictionary<string, string> endpoints) {

            builder.UseSwagger(options => options.PreSerializeFilters.Add(ConvertUrlToLowercase()));
            builder.UseSwaggerUI(options => {
                if (endpoints != null)
                    foreach (var (key, value) in endpoints)
                        options.SwaggerEndpoint(key, value);

                options.DefaultModelsExpandDepth(-1);
            });
        }

        public static void UseSwaggerUI(this IApplicationBuilder builder, string url, string name) =>
            builder.UseSwaggerUI(new Dictionary<string, string> {{url, name}});

        public static void AddSwaggerGenUI(this IServiceCollection serviceCollection, string title, string basePath,
            Action<SwaggerGenOptions> action = null, string path = "documentation.xml") {

            serviceCollection.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = title, Version = "v1"});

                var xmlPath = Path.Combine(basePath, path);
                options.IncludeXmlComments(xmlPath);

                options.EnableAnnotations();
                options.DescribeAllParametersInCamelCase();

                action?.Invoke(options);
            });
        }

        private static Action<OpenApiDocument, HttpRequest> ConvertUrlToLowercase() {
            return (document, request) => {

                var paths = document.Paths.ToDictionary(item => item.Key.ToCamelCaseUrl(), item => item.Value);
                document.Paths.Clear();
                foreach (var (key, value) in paths)
                    document.Paths.Add(key, value);
            };
        }
    }

}