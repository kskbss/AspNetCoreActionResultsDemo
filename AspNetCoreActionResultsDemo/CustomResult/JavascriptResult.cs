using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microsoft.AspNetCore.Mvc
{
    public class JavascriptResult : ActionResult, IStatusCodeActionResult
    {
        /// <summary>Gets or sets the HTTP status code.</summary>
        public int? StatusCode { get; set; }

        /// <summary>Gets or sets the value to be formatted.</summary>
        public object Value { get; set; }

        /// <summary>
        /// value parametresi ile gönderdiğimiz script kodları <script></script> tagları içine basılacak.
        /// </summary>
        /// <param name="value">The value to format as javascript.</param>
        public JavascriptResult(object value)
        {
            this.Value = value;
        }

        public JavascriptResult(object value, int? statusCode)
        {
            this.Value = value;
            this.StatusCode = statusCode;
        }

        /// <inheritdoc />
        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "text/html";
            response.StatusCode = StatusCode ?? 200;
            return context.HttpContext.Response.WriteAsync("<script>" + Value + "</script>");
        }
    }
}
