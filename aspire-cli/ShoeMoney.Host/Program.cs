var builder = DistributedApplication.CreateBuilder(args);

var userNameParameter = builder.AddParameter("RabbitUserName");
var passwordParameter = builder.AddParameter("RabbitPassword", true);

var queue = builder.AddRabbitMQ("OrderQueue", userNameParameter, passwordParameter)
    .WithManagementPlugin();

var cache = builder.AddRedis("Cache")
  .WithRedisCommander();

var sql = builder.AddSqlServer("Database")
  .AddDatabase("ShoeMoney");

var theApi = builder.AddProject<Projects.ShoeMoney_API>("TheApi")
  .WithReference(sql)
  .WithReference(queue)
  .WithReference(cache);

builder.AddProject<Projects.ShoeMoney_OrderProcessing>("OrderProcessing")
  .WithReference(sql)
  .WithReference(queue);

builder.AddNpmApp("FrontEnd", "../shoemoney.store/", "dev")
  .WithReference(theApi)
  .WithEndpoint(targetPort: 5173, scheme: "http", env: "PORT")
  .PublishAsDockerFile();

builder.Build().Run();
