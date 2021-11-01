using System;
using System.Linq;
using System.Threading.Tasks;
using HeimdallPower;
using HeimdallPower.Enums;

namespace TestClient
{
    class Program
    {
        private const string ClientId = "3d7bc9e8-0602-4285-b53d-c5e3db18785b";

        private const string PfxCertificatePath =
            @"C:\Users\even.skari\Code\Heimdall\HP.Cloud.API-clients\DotNetClient\DotNetClient\c.pfx";

        private const string CertificatePassword = "";

        private static async Task Main()
        {
            Console.WriteLine("Hello World!");
            var useDeveloperApi = false;
            var nameOfLineWithData = useDeveloperApi ? "Heimdall Power Line" : "Strøm Trafo - Fv";
            var api = new CloudApiClient(ClientId, useDeveloperApi, PfxCertificatePath, CertificatePassword);
            var lines = await api.GetLines();
            var line = lines.FirstOrDefault(line => line.Name.Equals(nameOfLineWithData));
            var span = line.Spans.First();
            var spanPhase = span.SpanPhases.ToList().First();
            var from = DateTime.Now.AddDays(-7);
            var to = DateTime.Now;

            var measurementsLine = await api.GetAggregatedMeasurements(line, null, from, to, IntervalDuration.EveryHour,
                MeasurementType.Current, AggregationType.Average);
            var measurementsSpan = await api.GetAggregatedMeasurements(line, span, from, to, IntervalDuration.EveryHour,
                MeasurementType.Current, AggregationType.Average);

            var icingLine = await api.GetIcingData(line, null, null, from, to);
            var icingSpan = await api.GetIcingData(null, span, null, from, to);
            var icingSpanPhase = await api.GetIcingData(null, span, spanPhase, from, to);

            var sagAndClearancesLine = await api.GetSagAndClearances(line, null, null, from, to);
            var sagAndClearancesSpan = await api.GetSagAndClearances(line, span, null, from, to);
            var sagAndClearancesSpanPhase = await api.GetSagAndClearances(line, null, spanPhase, from, to);

            var aggregatedDLR = await api.GetAggregatedDlr(line, from, to, DLRType.HP, "P1D");
            var forecastDLR = await api.GetAggregatedDlrForecast(line, 24);
        }
    }
}