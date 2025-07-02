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

// Fetch Aggregated Measurements data
var measurementsLine = await api.GetLatestCurrent(line);
var measurementsSpan = await api.GetLatestConductorTemperature(line);

// Fetch DLR data
var latestAar = await api.GetLatestHeimdallDlr(line);
var latestDlr = await api.GetLatestHeimdallAar(line);
var forecastAar = await api.GetHeimdallDlrForecast(line);
var forecastDlr = await api.GetHeimdallAarForecast(line);
Console.WriteLine(forecastAar.First().Timestamp);