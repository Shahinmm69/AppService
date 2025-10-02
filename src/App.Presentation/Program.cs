using App.Application.UseCases.Subscription.Queries.GetSubscriptionByUserId;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IPackagingHttpClient, PackagingHttpClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Packaging:BaseUrl"]!);
});

builder.Services.AddSingleton<IRedisCache>(sp =>
    new RedisCache(builder.Configuration["Redis:ConnectionString"]!)
);

builder.Services.AddSingleton<IRedisHealthMonitor, RedisHealthMonitor>();
builder.Services.AddHostedService(sp => (RedisHealthMonitor)sp.GetRequiredService<IRedisHealthMonitor>());

builder.Services.AddScoped<ISubscriptionProvider, SubscriptionProvider>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(typeof(GetSubscriptionByUserIdQuery).Assembly)
);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
