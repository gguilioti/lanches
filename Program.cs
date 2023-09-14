using System.Configuration;
using Lanches.Areas.Admin.Services;
using Lanches.Context;
using Lanches.Models;
using Lanches.Repositories;
using Lanches.Repositories.Interfaces;
using Lanches.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(options => options.AccessDeniedPath = "/Home/AccessDenied");
        builder.Services.Configure<ConfigurationImagens>(builder.Configuration.GetSection("ConfigurationPastaImagens"));

        builder.Services.Configure<IdentityOptions>(options => 
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireNonAlphanumeric = false;
        });

        builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        builder.Services.AddTransient<ILancheRepository, LancheRepository>();
        builder.Services.AddTransient<IPedidoRepository, PedidoRepository>();
        builder.Services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        builder.Services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        builder.Services.AddScoped<RelatorioVendasService>();
        builder.Services.AddScoped<GraficoVendasService>();
        
        builder.Services.AddAuthorization(options => 
        {
            options.AddPolicy("Admin",
            politica => 
            {
                politica.RequireRole("Admin");
            });
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddPaging(options => {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });

        builder.Services.AddMemoryCache();
        builder.Services.AddSession();

var app = builder.Build();

    if(app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    CriaPerfisUsuario(app);

    app.UseSession();
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
        name : "areas",
        pattern : "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
        );

        endpoints.MapControllerRoute(
            name: "categoriaFiltro",
            pattern: "Lanche/{action}/{categoria?}",
            defaults: new { controller = "Lanche", action = "List"});

        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });

app.Run();

void CriaPerfisUsuario(WebApplication app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<ISeedUserRoleInitial>();
        service.SeedUsers();
        service.SeedRoles();
    }
}