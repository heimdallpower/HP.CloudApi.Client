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
        private const string Dlrs = "dlrs/latest";
        private const string DlrForecast = "weather_based_dlrs/forecast";

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

        public static string BuildAggregatedDlrUrl(LineDto line, DLRType dlrType)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam("dlrType", dlrType.ToString());
            return GetFullUrl(Dlrs, CapacityMonitoring, queryParams, line.Id.ToString());
        }

        public static string BuildAggregatedDlrForecastUrl(LineDto line, int hoursAhead, DLRType? dlrType)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam("hoursAhead", hoursAhead.ToString())
                .AddQueryParam("dlrType", dlrType.HasValue ? dlrType.ToString() : DLRType.Cigre.ToString());
            return GetFullUrl(DlrForecast, CapacityMonitoring,  queryParams, line.Id.ToString());
        }


        private static string GetFullUrl(string endpoint, string module, NameValueCollection queryParams, string lineId, string apiVersion = V1)
        {
            return $"{module}/{apiVersion}/{lineId}/{endpoint}{queryParams.ToQueryString()}";
        }

        private static string GetFullUrl(string endpoint, string module, string lineId, string apiVersion = V1)
        {
            return $"{module}/{apiVersion}/{lineId}/{endpoint}";
        }

        public static string GetFullUrl(string endpoint, string apiVersion = V1)
        {
            return $"{apiVersion}/{endpoint}";
        }
    }
}