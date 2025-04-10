var builder = DistributedApplication.CreateBuilder(args);

var queue = builder.AddRabbitMQ("order-queue")
    .WithManagementPlugin()
    .WithLifetime(ContainerLifetime.Persistent);

var cache = builder.AddRedis("cache")
  .WithRedisCommander()
  .WithLifetime(ContainerLifetime.Persistent);

var sql = builder.AddSqlServer("db")
  .WithLifetime(ContainerLifetime.Persistent)
  .AddDatabase("shoe-money");

var theApi = builder.AddProject<Projects.ShoeMoney_API>("theapi")
  .WithReference(sql)
  .WithReference(queue)
  .WithReference(cache)
  .WaitFor(queue)
  .WaitFor(cache)
  .WaitFor(sql);

builder.AddProject<Projects.ShoeMoney_OrderProcessing>("order-processor")
  .WithReference(sql)
  .WithReference(queue)
  .WaitFor(queue)
  .WaitFor(sql);

builder.AddNpmApp("the-store", "../shoemoney.store/", "dev")
  .WithReference(theApi)
  .WithEndpoint(targetPort: 5173, scheme: "http", env: "PORT")
  .PublishAsDockerFile();

builder.Build().Run();
