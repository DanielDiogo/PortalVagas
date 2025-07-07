using PortalVagas.Models;
using PortalVagas.Repositories;
using PortalVagas.Services;     

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IVagaRepositorio, VagaRepositorio>();
builder.Services.AddSingleton<IPessoaRepositorio, PessoaRepositorio>();
builder.Services.AddSingleton<ICandidaturaRepositorio, CandidaturaRepositorio>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IVagaServico, VagaServico>();
builder.Services.AddTransient<IPessoaServico, PessoaServico>();
builder.Services.AddTransient<ICandidaturaServico, CandidaturaServico>();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vagas}/{action=Index}/{id?}");

app.Run();