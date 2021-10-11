using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using HeimdallPower.Entities;
using HeimdallPower.Enums;
using HeimdallPower.ExtensionMethods;

namespace HeimdallPower
{
    internal static class UrlBuilder
    {
        private const string AggregatedMeasurements = "aggregated-measurements";
        private const string IcingData = "icing-data";
        private const string SagAndClearances = "sag-and-clearances";
        private const string V1 = "api/v1";
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

        public static string BuildAggregatedMeasurementsUrl(LineDto line, SpanDto span, DateTime from, DateTime to,
            string intervalDuration, MeasurementType measurementType, AggregationType aggregationType)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam(GetDateTimeParams(from, to))
                .AddQueryParam(GetIdentifierParam(line, span, null))
                .AddQueryParam("intervalDuration", intervalDuration)
                .AddQueryParam("measurementType", measurementType.ToString())
                .AddQueryParam("aggregationType", aggregationType.ToString());

            return GetFullUrl(AggregatedMeasurements, queryParams);
        }

        public static string BuildIcingUrl(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam(GetDateTimeParams(from, to))
                .AddQueryParam(GetIdentifierParam(line, span, spanPhase));
            return GetFullUrl( IcingData, queryParams);
        }

        public static string BuildSagAndClearanceUrl(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            var queryParams = new NameValueCollection()
                .AddQueryParam(GetDateTimeParams(from, to))
                .AddQueryParam(GetIdentifierParam(line, span, spanPhase));
            return GetFullUrl(SagAndClearances, queryParams);
        }

        private static string GetFullUrl(string endpoint, NameValueCollection queryParams, string apiVersion = V1)
        {
            return $"{apiVersion}/{endpoint}{queryParams.ToQueryString()}";
        }

        public static string GetFullUrl(string endpoint, string apiVersion = V1)
        {
            return $"{apiVersion}/{endpoint}";
        }
    }
}