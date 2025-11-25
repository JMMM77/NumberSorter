using NumberSorter.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase(AspireResourceNameConstants.SqlDatabaseName);

var cache = builder.AddRedis(AspireResourceNameConstants.CacheName)
                   .WithRedisInsight();

var webApi = builder.AddProject<Projects.NumberSorter_WebApis>(AspireResourceNameConstants.WebApiProjectName)
    .WithReference(db)
    .WaitFor(db)
    .WithReference(cache)
    .WaitFor(cache);

builder.AddProject<Projects.NumberSorter_WebUI>(AspireResourceNameConstants.WebUiProjectName)
    .WithReference(webApi)
    .WaitFor(webApi);

builder.Build().Run();
