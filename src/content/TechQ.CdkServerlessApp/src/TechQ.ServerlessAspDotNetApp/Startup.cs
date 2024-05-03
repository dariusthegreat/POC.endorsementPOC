namespace TechQ.ServerlessAspDotNetApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        string baseUrl = System.Environment.GetEnvironmentVariable("APP_BASE_URL") ?? "https://localhost:60263";
        string appFQDN = System.Environment.GetEnvironmentVariable("APP_FQDN") ?? "localhost";
        string appDomain = System.Environment.GetEnvironmentVariable("APP_DOMAIN") ?? "localhost";
        System.Environment.SetEnvironmentVariable("APP_BASE_URL", baseUrl);
        System.Environment.SetEnvironmentVariable("APP_FQDN", appFQDN);
        System.Environment.SetEnvironmentVariable("APP_DOMAIN", appDomain);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
            endpoints.MapDefaultControllerRoute();
        });
    }
}
