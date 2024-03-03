// using System.Diagnostics;

// namespace MyTask.Middlewares;

// public class LogInMiddleware
// {
//     private RequestDelegate next;

//     public LogInMiddleware(RequestDelegate next)
//     {
//         this.next = next;
//     }

//     public async Task Invoke(HttpContext c)
//     {
//         await c.Response.WriteAsync("Hello from our 1st nice middleware start\n");
//         await Task.Delay(1000);
//         await next(c);
//         await c.Response.WriteAsync("Hello from our 1st nice middleware end\n");      
//     }    
// }

// public static partial class LogIn2MiddleExtensions
// {
//     public static IApplicationBuilder UseLogInMiddleware(this IApplicationBuilder builder)
//     {
//         return builder.UseMiddleware<LogInMiddleware>();
//     }
// }