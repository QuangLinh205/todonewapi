using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoNewApi.Modes;
using Microsoft.EntityFrameworkCore.InMemory;
namespace ToDoNewApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ToDoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc(option => option.EnableEndpointRouting = false);

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
