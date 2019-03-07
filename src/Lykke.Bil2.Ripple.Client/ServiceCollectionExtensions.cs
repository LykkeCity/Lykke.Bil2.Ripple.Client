using System;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Lykke.Common.Log;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Lykke.Bil2.Ripple.Client
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the JSON RPC client of the Ripple blockchain to the app services as <see cref="IRippleApi"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="url">Base URL of node.</param>
        /// <param name="username">Username, if authentication required for JSON RPC, or null</param>
        /// <param name="password">Password, if authentication required for JSON RPC, or null</param>
        /// <returns></returns>
        public static IServiceCollection AddRippleClient(this IServiceCollection services,
            string url,
            string username = null,
            string password = null,
            bool logRequestErrors = true)
        {
            if (string.IsNullOrWhiteSpace(url) || !Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out _))
            {
                throw new ArgumentException("Invalid node URL", nameof(url));
            }

            services.AddSingleton(serviceProvider =>
            {
                var builder = HttpClientGenerator.HttpClientGenerator
                    .BuildForUrl(url)
                    .WithoutRetries()
                    .WithoutCaching();

                if (logRequestErrors)
                {
                    builder = builder.WithRequestErrorLogging(serviceProvider.GetRequiredService<ILogFactory>());
                }

                if (!string.IsNullOrWhiteSpace(username))
                {
                    builder = builder.WithAdditionalDelegatingHandler(
                        new AuthenticationHandler("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"))));
                }

                return builder
                    .Create()
                    .Generate<IRippleApi>();
            });

            return services;
        }
    }
}