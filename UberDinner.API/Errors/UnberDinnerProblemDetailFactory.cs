using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace UberDinner.API.Errors
{
    public class UberDinnerProblemDetailFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;

        public UberDinnerProblemDetailFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext,
            int? statusCode = null,
            string title = null,
            string type = null,
            string detail = null,
            string instance = null
        )
        {
            statusCode ??= 500;

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Type = type,
                Detail = detail,
                Status = statusCode
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext,
            ModelStateDictionary modelStateDictionary,
            int? statusCode = null,
            string? title = null,
            string? type = null,
            string? detail = null,
            string? instance = null
        )
        {
            throw new NotImplementedException();
        }

        private void ApplyProblemDetailsDefaults(
            HttpContext httpContext,
            ProblemDetails problemDetail,
            int statusCode
        )
        {
            problemDetail.Status ??= statusCode;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetail.Title ??= clientErrorData.Title;
                problemDetail.Type ??= clientErrorData.Link;
                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                if (traceId != null)
                {
                    problemDetail.Extensions["traceId"] = traceId;
                }

                problemDetail.Extensions.Add("customProperty", "customValue");
            }
        }
    }
}
