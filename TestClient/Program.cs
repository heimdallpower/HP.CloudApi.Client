﻿using System;
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

// Fetch Facilities data
var facilities = await api.GetFacilities();
var facility = facilities.FirstOrDefault(f => f.Line.Name.Equals(lineName));

// Fetch Aggregated Measurements data
var measurementsLine = await api.GetLatestCurrent(line.Id);
var measurementsSpan = await api.GetLatestConductorTemperature(line.Id);

// Fetch DLR data
var latestDlr = await api.GetLatestHeimdallDlr(line.Id);
var latestAar = await api.GetLatestHeimdallAar(line.Id);
var forecastDlr = await api.GetHeimdallDlrForecast(line.Id);
var forecastAar = await api.GetHeimdallAarForecast(line.Id);

// Fetch Circuit Rating data
var circuitRatingForecast = await api.GetCircuitRatingForecast(facility.Id);
var circuitRating = await api.GetLatestCircuitRating(facility.Id);

Console.WriteLine(forecastAar.First().Timestamp);