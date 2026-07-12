var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Bakery_Api>("bakery-apphost-server");

builder.AddViteApp("client", "../bakery.client/");

builder.Build().Run();
