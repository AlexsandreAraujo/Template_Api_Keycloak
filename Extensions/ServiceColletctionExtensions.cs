﻿using Microsoft.OpenApi.Models;

namespace TemplateApi.Extensions
{
    internal static class ServiceColletctionExtensions
    {
        internal static IServiceCollection AddSwaggerGenWithAuth(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(
                o =>
                {
                    o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));

                    o.AddSecurityDefinition("keycloak", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,
                        Flows = new OpenApiOAuthFlows
                        {
                            Implicit = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri(configuration["keycloak:AuthorizationUrl"]!),
                                Scopes = new Dictionary<string, string>
                                {
                                    {"openid", "openid" },
                                    {"profile", "profile" }
                                }
                            }
                        }
                    });

                    var securityRequirement = new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "keycloak",
                                    Type = ReferenceType.SecurityScheme
                                },
                                In = ParameterLocation.Header,
                                Name = "Bearer",
                                Scheme = "Bearer"
                            },
                            []
                        }
                    };

                    o.AddSecurityRequirement(securityRequirement);
                }
            );

            return services;
        }
    }
}
