using NumberSorter.WebApis.Extensions;

WebApplication
    .CreateBuilder(args)
    .ConfigureBuilder()
    .Build()
    .ConfigureWebApis()
    .Run();
