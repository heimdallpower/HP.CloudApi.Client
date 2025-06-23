using System;
using System.Collections.Generic;
using HeimdallPower;
using HeimdallPower.Entities;
using HeimdallPower.Enums;

// Configuration setup
var clientId = "ClientId";
var clientSecret = "ClientSecret";
var useDeveloperApi = true;
var lineName = useDeveloperApi ? "Heimdall Power Line" : "Strøm Trafo - Fv";

Console.WriteLine("Initiating cloud API test client");

// Instantiate Cloud API Client
var api = new CloudApiClient(clientId, clientSecret, useDeveloperApi);

var from = DateTime.Now.AddDays(-7);
var to = DateTime.Now;
var line = new LineDto
{
    Name = "DLR Demo",
    Id = new Guid("LineId"),
    Owner = "Demos & Simulations",
    Spans = new List<SpanDto>()
};

// Fetch Aggregated Measurements data
var temperature = await api.GetAggregatedConductorTemperature(line);
Console.WriteLine(temperature.Unit);
Console.WriteLine(temperature.ConductorTemperatures.Max);
Console.WriteLine(temperature.ConductorTemperatures.Min);

var current = await api.GetAggregatedCurrent(line);
Console.WriteLine(current.Unit);
Console.WriteLine(current.Current.Timestamp);
Console.WriteLine(current.Current.Value);

// Fetch DLR data*/
var aggregatedDLR = await api.GetAggregatedDlr(line, DLRType.HeimdallDLR);
Console.WriteLine(aggregatedDLR.Dlr.MinAmpacity);
//var forecastDLR = await api.GetAggregatedDlrForecast(line, 24, DLRType.HeimdallDLR);