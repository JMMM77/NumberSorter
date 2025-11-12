using NumberSorter.Shared.Constants;

var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

var db = sql.AddDatabase(NumberSorterAppConstants.AspireSqlDatabaseName);

builder.AddProject<Projects.NumberSorter_WebUI>("numbersorter-webui")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
