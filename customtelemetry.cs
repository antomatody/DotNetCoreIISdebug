using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.InteropServices;

namespace dotnetcoreweb1
{
    public class customtelemetry: ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Operation.Id = "Opetion ID";
            telemetry.Context.User.Id = "user ID";
            telemetry.Context.Session.Id = "Session ID";
            telemetry.Context.Operation.ParentId = "Operation ParentId";
            telemetry.Context.Cloud.RoleName = ".NET Core 5";
            telemetry.Context.Cloud.RoleInstance = ".NET Core 5";
            telemetry.Context.Operation.Name = "Test OperationName";

            var customrequest = telemetry as RequestTelemetry;
            if (customrequest == null) return;
            customrequest.Properties.Remove("AspNetCoreEnvironment");
            customrequest.Properties.Remove("DeveloperMode");
            customrequest.Properties.Add("Custom Propertry", "test1");
            customrequest.Properties.Add("Custom Propertry 2", "test2");

            TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
            configuration.ConnectionString = "InstrumentationKey=1fb6f357-f6d0-426e-b64e-f148e03e7a2b;IngestionEndpoint=https://southeastasia-0.in.applicationinsights.azure.com/";

            var customtrace = new TelemetryClient(configuration);
            {
                var myJsonString = File.ReadAllText("sample.json");
                var myJObject = JObject.Parse(myJsonString);
                customtrace.TrackTrace(myJObject.ToString());
                customtrace.TrackTrace("Slow response - andyDB");
            }
            var customevent = new TelemetryClient(configuration);
            {
                customevent.TrackEvent("custom event");
            }
            var custommertic = new TelemetryClient(configuration);
            {
                var demo1 = new MetricTelemetry();
                demo1.Name = "my metric";
                demo1.Value = 13.5;
                custommertic.TrackMetric(demo1);
            }
            var custompageview = new TelemetryClient(configuration);
            {
                custompageview.TrackPageView("custom pageview");
            }
            var customDependency = new TelemetryClient(configuration);
            {
                var success = false;
                var startTime = DateTime.UtcNow;
                var timer = System.Diagnostics.Stopwatch.StartNew();
                customDependency.TrackDependency("DependencyType", "myDependency", "one Microsoft！", startTime, timer.Elapsed, success);
            }
        }
    }
}