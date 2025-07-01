using System;
using System.Collections.Specialized;
using System.Data;
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
        private const string HeimdallDlrForecast = "heimdall_dlrs/forecasts";
        private const string HeimdallAarForecast = "heimdall_aars/forecasts";

        private const string CapacityMonitoring = "capacity_monitoring";
        private const string GridInsight = "grid_insights";
        private const string V1 = "v1/lines";

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

        public static string BuildDlrForecastUrl(LineDto line)
        {
            return GetFullUrl(HeimdallDlrForecast, CapacityMonitoring, line.Id.ToString());
        }
        
        public static string BuildAarForecastUrl(LineDto line)
        {
            return GetFullUrl(HeimdallAarForecast, CapacityMonitoring, line.Id.ToString());
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