using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAiAssistant;
using OpenAiProxy;
//  public List<ChatCompletionRequestMessage> Messages { get; set; }
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var section = builder.Configuration.GetSection(nameof(OpenAiSettings));
var openAiSettings = section.Get<OpenAiSettings>();
if (openAiSettings == null)
{
    throw new Exception("openAiSettings == null");
}
builder.Services.AddOptions<OpenAiSettings>().Bind(section).ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddHttpClient<IOpenAiClient, OpenAiClient>().ConfigureHttpClient( c=>
{
    c.BaseAddress = new Uri(openAiSettings.BaseUrl);
    c.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", openAiSettings.ApiKey);
    c.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v1");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
