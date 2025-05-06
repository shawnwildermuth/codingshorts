

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache")
  .WithRedisCommander()
  .WithLifetime(ContainerLifetime.Persistent);

var db = builder.AddSqlServer("db")
  .WithLifetime(ContainerLifetime.Persistent);

var sql = db.AddDatabase("shoe-money");
var queue = db.AddDatabase("order-queue");

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
  .WithHttpEndpoint(env: "PORT")
  .WithExternalHttpEndpoints()
  .PublishAsDockerFile();

builder.Build().Run();
