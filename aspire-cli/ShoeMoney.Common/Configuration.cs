using System.Reflection;

using MassTransit;
using MassTransit.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace ShoeMoney;
public static class Configuration
{
  public static TBuilder EnableMonitoring<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
  {
    builder.ConfigureOpenTelemetry();

    builder.AddDefaultHealthChecks();

    return builder;
  }

  public static TBuilder EnableMessaging<TBuilder>(this TBuilder builder,
    Action<IBusRegistrationConfigurator>? callback = null)
    where TBuilder : IHostApplicationBuilder
  {

    builder.Services.AddMassTransit(cfg =>
    {
      if (callback is not null) callback(cfg);

      var connection = builder.Configuration.GetConnectionString("OrderQueue")!;

      cfg.UsingRabbitMq((ctx, config) =>
      {
        config.Host(new Uri(connection));
        config.ConfigureEndpoints(ctx);
      });
    });

    return builder;
  }

  public static TBuilder ConfigureOpenTelemetry<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
  {
    builder.Logging.AddOpenTelemetry(logging =>
    {
      logging.IncludeFormattedMessage = true;
      logging.IncludeScopes = true;
    });

    builder.Services.AddOpenTelemetry()
        .WithMetrics(metrics =>
        {
          metrics.AddAspNetCoreInstrumentation()
                  .AddHttpClientInstrumentation()
                  .AddRuntimeInstrumentation();
        })
        .WithTracing(tracing =>
        {
          tracing.AddSource(builder.Environment.ApplicationName)
                  .AddAspNetCoreInstrumentation();
        });

    builder.AddOpenTelemetryExporters();

    return builder;
  }

  private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
  {
    var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

    if (useOtlpExporter)
    {
      builder.Services.AddOpenTelemetry().UseOtlpExporter();
    }

    return builder;
  }

  public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
  {
    builder.Services.AddHealthChecks()
        // Add a default liveness check to ensure app is responsive
        .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

    return builder;
  }

}
