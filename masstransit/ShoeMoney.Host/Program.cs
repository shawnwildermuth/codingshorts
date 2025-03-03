using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var connectionString = builder.AddConnectionString("ShoeMoneyDb");
var queue = builder.AddRabbitMQ("orderQueue")
  .WithManagementPlugin()
  .WithLifetime(ContainerLifetime.Persistent);

var theApi = builder.AddProject<Projects.ShoeMoney_API>("theApi")
  .WithReference(queue)
  .WithReference(connectionString)
  .WaitFor(queue);

builder.AddProject<Projects.ShoeMoney_OrderProcessing>("orderProcessing")
  .WithReference(queue)
  .WithReference(connectionString)
  .WaitFor(queue);


builder.AddNpmApp("store", "../shoemoney.store/", "dev")
  .WithReference(theApi)
  .WithEndpoint(targetPort: 5173, scheme: "http", env: "PORT")
  .PublishAsDockerFile();

builder.Build().Run();
