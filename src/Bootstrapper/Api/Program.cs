var builder = WebApplication.CreateBuilder(args);

//Add Service to container 
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
var orderingAssembly = typeof(OrderingModule).Assembly;

builder.Services
    .AddCarterWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly);

//builder.Services
//    .AddMediatRWithAssemblies(catalogAssembly, basketAssembly, orderingAssembly);




builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

var app = builder.Build();

//configure the http request pipline

app.MapCarter();
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();
app.Run();
