using Microsoft.EntityFrameworkCore;
using PrivateMSGService.Application.Services;
using PrivateMSGService.Core.Abstractions;
using PrivateMSGService.DataAccess;
using PrivateMSGService.DataAccess.Repositories;
using PrivateMSGService.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Services.AddDbContext<PrivateMSGDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString(nameof(PrivateMSGDbContext)));
});

builder.Services.AddScoped<IPrivateMessagesService, PrivateMessagesService>();
builder.Services.AddScoped<IPrivateMessageRepository, PrivateMessageRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseEndpoints(opt =>
{
    opt.MapHub<PrivateChatHub>("/privatechat");
});

app.MapControllers();

app.Run();
