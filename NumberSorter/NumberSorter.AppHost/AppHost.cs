using NumberSorter.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase(AspireResourceNameConstants.SqlDatabaseName);

var cache = builder.AddRedis(AspireResourceNameConstants.CacheName)
                   .WithRedisInsight();

var ollama = builder.AddOllama(AspireResourceNameConstants.OllamaServiceName)
                    .WithContainerRuntimeArgs("--gpus=all")
                    .WithDataVolume()
                    .WithOpenWebUI();

var llm = ollama.AddModel(AspireResourceNameConstants.OllamaLlmConnectionName, AspireResourceNameConstants.OllamaLlmName);

var mcpServer = builder.AddProject<Projects.NumberSorter_Mcp>(AspireResourceNameConstants.McpProjectName);

// Currently does not trust dev certificate, so you have to connect to the HTTP (Not HTTPS) version of the mcp version
builder.AddMcpInspector("inspector")
    .WithMcpServer(mcpServer);

var webApi = builder.AddProject<Projects.NumberSorter_WebApis>(AspireResourceNameConstants.WebApiProjectName)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(mcpServer)
    .WaitFor(mcpServer)
    .WithReference(llm);

builder.AddProject<Projects.NumberSorter_WebUI>(AspireResourceNameConstants.WebUiProjectName)
    .WithReference(webApi)
    .WaitFor(webApi);

builder.Build().Run();
