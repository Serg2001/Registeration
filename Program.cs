using Registeration.Components;
using Registeration.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Identity.Client;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Microsoft.Extensions.Configuration;
using Registeration.DTOs;
using Registeration.Services.SchoolServices;
using Registeration.Services.FormStateServices;
using Registeration.Services.MailServices;
using Registeration.Services.OtherCompanyServices;
using Registeration.Services.OtherLecturerServices;
using Registeration.Services.OtherPhysicalPersonServices;
using Registeration.Services.OtherStudentServices;
using Registeration.Services.OtherTeacherServices;
using Registeration.Services.OtherPupilServices;
using Registeration.Services.CrmServices;
using Registeration.Services.Selection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpClient("CRM", client =>
{
    client.BaseAddress = new Uri("https://crm.dshh.am:1400");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    };
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7266") });
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<FormStateService>();
builder.Services.AddScoped<OtherPupilService>();
builder.Services.AddScoped<OtherTeacherService>();

// Register CrmService
builder.Services.AddScoped<CrmService>(); // Add this line

// Register SchoolSelection and RegionSelection after dependencies
builder.Services.AddScoped<SchoolSelection>();
builder.Services.AddScoped<RegionSelection>();

builder.Services.AddHttpClient("ExternalRegions", client =>
{
    client.BaseAddress = new Uri("https://crm.dshh.am:1400/");
});

builder.Services.AddHttpClient("CrmApi", client =>
{
    client.BaseAddress = new Uri("https://crm.dshh.am:1400/");
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddScoped<OtherCompanyService>();
builder.Services.AddScoped<OtherPhysicalPersonService>();
builder.Services.AddScoped<FormStateForOtherPhysicalPerson>();

builder.Services.AddHttpClient("CountryStateCity", client =>
{
    client.BaseAddress = new Uri("https://api.countrystatecity.in/v1/");
    client.Timeout = TimeSpan.FromSeconds(60);
});

builder.Services.AddMudServices();

builder.Services.AddScoped<RegistrationService>();
builder.Services.AddScoped<FromStateServiceOtherCompany>();
builder.Services.AddScoped<FormStateServiceForOtherStudent>();
builder.Services.AddScoped<FormStateForOtherPhysicalPerson>();
builder.Services.AddScoped<FormStateServiceForOtherLecturer>();
builder.Services.AddScoped<OtherStudentService>();
builder.Services.AddScoped<OtherLecturerService>();
builder.Services.AddScoped<OtherPhysicalPersonService>();
builder.Services.AddScoped<CRMPupilService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API name V1");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync(); // Applies any pending migrations
}

app.Run();