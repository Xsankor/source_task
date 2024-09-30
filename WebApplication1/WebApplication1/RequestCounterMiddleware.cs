namespace WebApplication1
{
    public class RequestCounterMiddleware
    {
        private readonly RequestDelegate next;

        private readonly int parallelLimit;
        private static int currentCountRequest = 0;

        public RequestCounterMiddleware(RequestDelegate next, IConfiguration configuration) 
        {
            this.next = next;
            parallelLimit = configuration.GetValue<int>("Setting:ParallelLimit");
        }

        public async Task Invoke(HttpContext context) 
        {
            int currentCount = Interlocked.Increment(ref currentCountRequest);

            try
            {
                if (currentCount >= parallelLimit)
                {
                    context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    await context.Response.WriteAsync("Service Unavailable"); //
                    return;
                }

                await next(context);
            }
            finally 
            {
                Interlocked.Decrement(ref currentCountRequest);
            }
        }
    }
}
