using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddOpenApi("v1", options => {
    // Cambiar versión del OpenAPI Spec si es necesario (3.1 es el default en .NET 10)
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;

    // Transformador de Documento: Información general de la API
    options.AddDocumentTransformer((document, context, cancellationToken) => {
        document.Info.Title = "Mi API Practica .NET 10";
        document.Info.Version = "v1.0";
        document.Info.Description = "Servicio Web API configurado con Microsoft.AspNetCore.OpenApi | Test Pipeline Jenkins";
        return Task.CompletedTask;
    });

    // Transformador de Esquemas: Ejemplo para mapear tipos decimales
    options.AddSchemaTransformer((schema, context, cancellationToken) => {
        if(context.JsonTypeInfo.Type == typeof(decimal))
        {
            schema.Format = "decimal";
        }
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Expone la interfaz gráfica en `/scalar/v1`
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
