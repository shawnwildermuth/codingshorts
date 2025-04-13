var builder = DistributedApplication.CreateBuilder(args);

var userNameParameter = builder.AddParameter("RabbitUserName");
var passwordParameter = builder.AddParameter("RabbitPassword", true);

var queue = builder.AddRabbitMQ("OrderQueue", userNameParameter, passwordParameter)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithManagementPlugin();

var bus = builder.AddAzureServiceBus("bus")
  .RunAsEmulator();

var cache = builder.AddRedis("Cache")
    .WithLifetime(ContainerLifetime.Persistent)
  .WithRedisCommander();

var sql = builder.AddSqlServer("Database")
  .WithLifetime(ContainerLifetime.Persistent)
  .AddDatabase("ShoeMoney");

var theApi = builder.AddProject<Projects.ShoeMoney_API>("TheApi")
  .WithReference(sql)
  .WithReference(queue)
  .WithReference(cache)
  .WaitFor(sql)
  .WaitFor(queue)
  .WaitFor(cache);

builder.AddProject<Projects.ShoeMoney_OrderProcessing>("OrderProcessing")
  .WithReference(sql)
  .WithReference(queue)
  .WaitFor(sql)
  .WaitFor(queue)
  .WithExplicitStart();

builder.AddNpmApp("FrontEnd", "../shoemoney.store/", "dev")
  .WithReference(theApi)
  .WithEndpoint(targetPort: 5173, scheme: "http", env: "PORT")
  .PublishAsDockerFile();

builder.Build().Run();
