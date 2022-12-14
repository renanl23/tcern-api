// Renan Lima Gomes

// http://localhost:5000/Dividir?Dividendo=4&divisor=2

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:4200");
                      });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/Dividir", (HttpRequest request) =>
{   
    var Dividendo = int.Parse(request.Query["Dividendo"]);
    var divisor = int.Parse(request.Query["divisor"]);    
    
    if (divisor == 0) {
        var resultadoDivisao = new ResultadoDivisao
        {
        resultado = "",
        erro = "Não é possível dividir por 0",
        };
        return resultadoDivisao;
    } else {
        var resultadoCalculo = Dividendo / divisor;
        var resultadoDivisao = new ResultadoDivisao
        {
        resultado = resultadoCalculo.ToString(),
        erro = "",
        };
        return resultadoDivisao;
    }
})
.WithName("GetResultadoDividir");

app.Run("http://localhost:5000");


public record ResultadoDivisao
{
  public string resultado { get; init; }
  public string erro { get; init; }
}
