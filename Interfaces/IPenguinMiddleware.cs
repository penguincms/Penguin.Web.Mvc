using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Penguin.Web.Mvc.Interfaces
{
    public interface IPenguinMiddleware
    {
        Task Invoke(HttpContext context);
    }
}
