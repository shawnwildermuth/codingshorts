using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;


public static class Extensions
{
  public static IHostApplicationBuilder AddTelemetry(this IHostApplicationBuilder builder)
  {
    builder.Services.AddHealthChecks();

    builder.Services.AddCors(cfg => cfg.AddDefaultPolicy(bldr =>
    {
      bldr.AllowAnyHeader();
      bldr.AllowAnyOrigin();
      bldr.AllowAnyMethod();
    }));

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
          tracing.AddAspNetCoreInstrumentation()
                  .AddHttpClientInstrumentation();
        });

    // Wire up to a listener if it is added
    var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

    if (useOtlpExporter)
    {
      builder.Services.AddOpenTelemetry().UseOtlpExporter();
    }

    return builder;
  }

  public static WebApplication MapDefaultEndpoints(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.MapHealthChecks("/health");
    }

    app.UseCors();

    return app;
  }
}
