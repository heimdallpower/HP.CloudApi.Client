using System;
using System.Collections.Generic;
using System.Linq;
using HeimdallPower;
using HeimdallPower.Entities;
using HeimdallPower.Enums;

// Configuration setup
var clientId = "clientId";
var clientSecret = "clientSecret";
var useDeveloperApi = true;

Console.WriteLine("Initiating cloud API test client");

// Instantiate Cloud API Client
var api = new CloudApiClient(clientId, clientSecret, useDeveloperApi);

var lines = await api.GetLines();
Console.WriteLine(lines.First().Name);
Console.WriteLine(lines.Count);

var line = new LineDto
{
    Name = "DLR Demo",
    Id = new Guid("LineGuid"),
    Spans = new List<SpanDto>()
};

// Fetch Aggregated Measurements data
var responseTemp = await api.GetAggregatedMeasurements(line, MeasurementType.WireTemperature);
Console.WriteLine(responseTemp.Value);
Console.WriteLine(responseTemp.Timestamp);

var responseCurrent = await api.GetAggregatedMeasurements(line, MeasurementType.Current);
Console.WriteLine(responseCurrent.Value);
Console.WriteLine(responseCurrent.Timestamp);

// Fetch DLR data
var responseDlr = await api.GetAggregatedDlr(line, DLRType.HeimdallDLR);
Console.WriteLine(responseDlr.Ampacity);
Console.WriteLine(responseDlr.Timestamp);

// Fetch DLR Forecast
var responseDlrForecast = await api.GetAggregatedDlrForecast(line, 72);
Console.WriteLine(responseDlrForecast.First().Prediction.Ampacity);
