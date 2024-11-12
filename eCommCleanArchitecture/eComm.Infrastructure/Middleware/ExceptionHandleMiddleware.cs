using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace eComm.Infrastructure.Middleware
{
    public class ExceptionHandleMiddleware(RequestDelegate request)
    {
        public async Task InvokeAsync(HttpContext context )
        {
            try
            {
                await request(context);
            }
            catch (DbUpdateException ex)
            {
                context.Response.ContentType = "application/json";
                if (ex.InnerException is SqlException exception)
                {
                    switch (exception.Number) 
                    {
                        case 2627:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Unique constraint violation");
                            break;
                        case 515:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync("Not insert");
                            break;
                        case 547:
                            context.Response.StatusCode = StatusCodes.Status409Conflict;
                            await context.Response.WriteAsync("Foreign key constrain");
                            break;
                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync("Error while processing");
                            break;
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Error while saving entity change");
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Error" + ex.Message);
            }
         }
    }
}
