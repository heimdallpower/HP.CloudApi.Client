using System;
using System.Linq;
using System.Threading.Tasks;
using HeimdallPower;
using HeimdallPower.Enums;

// Configuration setup
var clientId = "insert-your-client-id-here";
var clientSecret = "insert-your-client-secret-here";
var useDeveloperApi = true;
var lineName = useDeveloperApi ? "Heimdall Power Line" : "Strøm Trafo - Fv";

Console.WriteLine("Initiating cloud API test client");

// Instantiate Cloud API Client
var api = new CloudApiClient(clientId, clientSecret, useDeveloperApi);

// Fetch Lines data
var lines = await api.GetLines();
var line = lines.FirstOrDefault(line => line.Name.Equals(lineName));
var span = line.Spans.First();
var spanPhase = span.SpanPhases.ToList().First();
var from = DateTime.Now.AddDays(-7);
var to = DateTime.Now;

// Fetch Aggregated Measurements data
var measurementsLine = await api.GetAggregatedMeasurements(line, null, from, to, IntervalDuration.EveryHour,
    MeasurementType.Current, AggregationType.Average);
var measurementsSpan = await api.GetAggregatedMeasurements(line, span, from, to, IntervalDuration.EveryHour,
    MeasurementType.Current, AggregationType.Average);

// Fetch Icing data
var icingLine = await api.GetIcingData(line, null, null, from, to);
var icingSpan = await api.GetIcingData(null, span, null, from, to);
var icingSpanPhase = await api.GetIcingData(null, span, spanPhase, from, to);


// Fetch Sag and Clearances data
var sagAndClearancesLine = await api.GetSagAndClearances(line, null, null, from, to);
var sagAndClearancesSpan = await api.GetSagAndClearances(line, span, null, from, to);
var sagAndClearancesSpanPhase = await api.GetSagAndClearances(line, null, spanPhase, from, to);

// Fetch DLR data
var aggregatedDLR = await api.GetAggregatedDlr(line, from, to, DLRType.HP, "P1D");
var forecastDLR = await api.GetAggregatedDlrForecast(line, 24, DLRType.HeimdallDLR);