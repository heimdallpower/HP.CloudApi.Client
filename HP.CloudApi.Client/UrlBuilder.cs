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
        private const string AggregatedDLR = "dlr/aggregated-dlr";
        private const string AggregatedDLRForecast = "dlr/aggregated-dlr-forecast";

        private const string GridInsight = "grid_insights";
        private const string V1 = "v1/lines";
        private const string Beta = "api/beta";

        private const string DateFormat = "o";

        private static NameValueCollection GetDateTimeParams(DateTime from, DateTime to)
        {
            return new NameValueCollection()
                .AddQueryParam("fromDateTime", from.ToString(DateFormat))
                .AddQueryParam("toDateTime", to.ToString(DateFormat));
        }

        private static NameValueCollection GetIdentifierParam(LineDto line, SpanDto span, SpanPhaseDto spanPhase)
        {
            var identifierParam = new NameValueCollection();
            if (spanPhase != null)
                identifierParam.AddQueryParam("spanPhaseId", spanPhase.Id.ToString());
            else if (span != null)
                identifierParam.AddQueryParam("spanId", span.Id.ToString());
            else
                identifierParam["lineId"] = line.Id.ToString();

            return identifierParam;
        }

        private static NameValueCollection GetIntervalDurationParam(string intervalDuration)
        {
            var durationParam = new NameValueCollection();
            durationParam["intervalDuration"] = intervalDuration;
            return durationParam;
        }

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

        public static string BuildAggregatedDlrUrl(LineDto line, DateTime from, DateTime to, DLRType dlrType, string intervalDuration)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam(GetDateTimeParams(from, to))
                .AddQueryParam(GetIdentifierParam(line, null, null))
                .AddQueryParam(GetIntervalDurationParam(intervalDuration))
                .AddQueryParam("dlrType", dlrType.ToString());
            return GetFullUrlOld(AggregatedDLR, queryParams);
        }

        public static string BuildAggregatedDlrForecastUrl(LineDto line, int hoursAhead, DLRType? dlrType)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam(GetIdentifierParam(line, null, null))
                .AddQueryParam("hoursAhead", hoursAhead.ToString())
                .AddQueryParam("dlrType", dlrType.HasValue ? dlrType.ToString() : DLRType.Cigre.ToString());
            return GetFullUrlOld(AggregatedDLRForecast, queryParams);
        }
        
        public static string BuildAssetsUrl()
        {
            return "assets/v1/assets";
        }


        private static string GetFullUrlOld(string endpoint, NameValueCollection queryParams, string apiVersion = V1)
        {
            return $"{apiVersion}/{endpoint}{queryParams.ToQueryString()}";
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