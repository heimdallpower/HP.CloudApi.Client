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
var responseTemp = await api.GetAggregatedMeasurements(line, MeasurementType.WireTemperature);
Console.WriteLine(responseTemp.Value);
Console.WriteLine(responseTemp.IntervalStartTime);

var responseCurrent = await api.GetAggregatedMeasurements(line, MeasurementType.Current);
Console.WriteLine(responseCurrent.Value);
Console.WriteLine(responseCurrent.IntervalStartTime);

// Fetch DLR data*/
var aggregatedDLR = await api.GetAggregatedDlr(line, DLRType.HeimdallDLR);
Console.WriteLine(aggregatedDLR.Dlr.MinAmpacity);
//var forecastDLR = await api.GetAggregatedDlrForecast(line, 24, DLRType.HeimdallDLR);