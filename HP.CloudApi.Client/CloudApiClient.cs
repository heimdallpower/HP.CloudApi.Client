using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HeimdallPower.Entities;
using HeimdallPower.Enums;
using HeimdallPower.ExtensionMethods;

namespace HeimdallPower
{
    /// <summary>
    /// A client that lets you consume the Heimdall Cloud API
    /// </summary>
    public class CloudApiClient
    {
        private readonly HeimdallHttpClient _heimdallClient;

        public CloudApiClient(string clientId, string clientSecret, bool useDeveloperApi)
        {
            _heimdallClient = new HeimdallHttpClient(clientId, clientSecret, useDeveloperApi);
        }

        /// <summary>
        /// Get a list of line objects. The line object consists of its id, the spans of the line, and the spanphases of each span. The ids of the lines, spans and phases can be used to query the other endpoints.
        /// </summary>
        public async Task<List<LineDto>> GetLines()
        {
            var url = UrlBuilder.GetFullUrl("lines");
            var response = await _heimdallClient.Get<ApiResponse<List<LineDto>>>(url);
            return response == null ? new List<LineDto>() : response.Data.ToList();
        }

        /// <summary>
        /// Get aggregated measurements per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<List<AggregatedFloatValueDto>> GetAggregatedMeasurements(LineDto line, SpanDto span,
            DateTime from, DateTime to, string intervalDuration, MeasurementType measurementType,
            AggregationType aggregationType)
        {
            if (!intervalDuration.IsValidIso8601Duration())
            {
                throw new ArgumentException($"Interval duration '{intervalDuration}' is not a valid ISO 8601 string");
            }
            var url = UrlBuilder.BuildAggregatedMeasurementsUrl(line, span, from, to, intervalDuration,
                measurementType,
                aggregationType);
            var response = await _heimdallClient.Get<ApiResponse<List<AggregatedFloatValueDto>>>(url);

            return response != null ? response.Data.ToList() : new();
        }

        /// <summary>
        /// Get icing data per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<LineDto<IcingDataDto>> GetIcingData(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            var url = UrlBuilder.BuildIcingUrl(line, span, spanPhase, from, to);
            var response = await _heimdallClient.Get<ApiResponse<LineDto<IcingDataDto>>>(url);

            return response != null ? response.Data : new();
        }

        /// <summary>
        /// Get sag and clearances per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<List<LineDto<SagAndClearanceDto>>> GetSagAndClearances(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            var url = UrlBuilder.BuildSagAndClearanceUrl(line, span, spanPhase, from, to);
            var response = await _heimdallClient.Get<ApiResponse<List<LineDto<SagAndClearanceDto>>>>(url);

            return response != null ? response.Data : new();
        }

        /// <summary>
        /// Get aggregated dynamic line ratings for a line
        /// </summary>
        public async Task<List<DLRDto>> GetAggregatedDlr(LineDto line, DateTime from, DateTime to, DLRType dlrType, string intervalDuration)
        {
            if (!intervalDuration.IsValidIso8601Duration())
            {
                throw new ArgumentException($"Interval duration '{intervalDuration}' is not a valid ISO 8601 string");
            }
            var url = UrlBuilder.BuildAggregatedDlrUrl(line, from, to, dlrType, intervalDuration);
            var response = await _heimdallClient.Get<ApiResponse<List<DLRDto>>>(url);

            return response != null ? response.Data : new();
        }

        /// <summary>
        /// Get hourly DLR forecasts (Cigre) up to 48 hours ahead in time
        /// </summary>
        public async Task<List<LineAggregatedDLRForecastDto>> GetAggregatedDlrForecast(LineDto line, int hoursAhead)
        {
            var url = UrlBuilder.BuildAggregatedDlrForecastUrl(line, hoursAhead);
            var response = await _heimdallClient.Get<ApiResponse<List<LineAggregatedDLRForecastDto>>>(url);

            return response != null ? response.Data : new();
        }
    }
}