using RabbitMQ.Core.Data;
using RabbitMQ.Core.Entities;
using RabbitMQ.Core.Services.Abstract;
using RabbitMQ.Core.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddScoped<IRabbitMQConfiguration, RabbitMQConfiguration>();
builder.Services.AddScoped<IObjectConvertFormat, ObjectConvertFormat>();
builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IDataModel<User>, UsersDataModel>();
builder.Services.AddScoped<ISmtpConfiguration, SmtpConfiguration>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IConsumerService, ConsumerService>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
