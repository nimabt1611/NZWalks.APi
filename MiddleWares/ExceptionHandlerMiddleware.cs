using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Net;

namespace NZWalks.API.MiddleWares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> logger;
		private readonly RequestDelegate next;


		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
			RequestDelegate next)
        {
			this.logger = logger;
			this.next = next;
		
		}
		public async Task InvokeAsync(HttpContext httpContext) 
		{
			try
			{
				await next(httpContext);
			}
			catch(Exception ex)
			{
				var errorId = Guid.NewGuid();

				logger.LogError(ex, $"{errorId} : { ex.Message}");

				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/Json";

				var error = new
				{
					Id = errorId,
					ErorrMessage = "Something went wrong!"
				};

				await httpContext.Response.WriteAsJsonAsync(error);

			}
		}
    }
}
