using System;
using System.Collections.Specialized;
using System.Data;
using System.Net;
using HeimdallPower.Entities;
using HeimdallPower.Enums;
using HeimdallPower.ExtensionMethods;

namespace HeimdallPower
{
    internal static class UrlBuilder
    {
        private const string ConductorTemperatures = "conductor_temperatures/latest";
        private const string Currents = "currents/latest";
        private const string HeimdallDlr = "heimdall_dlrs/latest";
        private const string HeimdallAar = "heimdall_aars/latest";
        private const string HeimdallAarForecast = "heimdall_aars/forecast";

        private const string CapacityMonitoring = "capacity_monitoring";
        private const string GridInsight = "grid_insights";
        private const string V1 = "v1/lines";

        private const string DateFormat = "o";

        public static string BuildLatestConductorTemperatureUrl(LineDto line, string unitSystem)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam("unit_system", unitSystem);
            return GetFullUrl(ConductorTemperatures, GridInsight, queryParams, line.Id.ToString());
        }

        public static string BuildLatestCurrentsUrl(LineDto line)
        {
            return GetFullUrl(Currents, GridInsight, line.Id.ToString());
        }

        public static string BuildHeimdallDlrUrl(LineDto line)
        {
            return GetFullUrl(HeimdallDlr, CapacityMonitoring, line.Id.ToString());
        }
        
        public static string BuildHeimdallAarUrl(LineDto line)
        {
            return GetFullUrl(HeimdallAar, CapacityMonitoring, line.Id.ToString());
        }

        public static string BuildDlrForecastUrl(LineDto line, int hoursAhead)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam("hoursAhead", hoursAhead.ToString());
            return GetFullUrl(HeimdallAarForecast, CapacityMonitoring,  queryParams, line.Id.ToString());
        }

        public static string BuildAssetsUrl()
        {
            return "assets/v1/assets";
        }

        private static string GetFullUrl(string endpoint, string module, NameValueCollection queryParams, string lineId, string apiVersion = V1)
        {
            return $"{module}/{apiVersion}/{lineId}/{endpoint}{queryParams.ToQueryString()}";
        }

        private static string GetFullUrl(string endpoint, string module, string lineId, string apiVersion = V1)
        {
            return $"{module}/{apiVersion}/{lineId}/{endpoint}";
        }
    }
}