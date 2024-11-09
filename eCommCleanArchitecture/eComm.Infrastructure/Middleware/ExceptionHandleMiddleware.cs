using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

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

            }
        }
    }
}
