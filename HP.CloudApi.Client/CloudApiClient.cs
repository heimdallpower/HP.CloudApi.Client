using System;
using System.Collections.Generic;
using System.Linq;
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
            var url = UrlBuilder.GetFullUrlOld("lines");
            var response = await _heimdallClient.Get<ApiResponse<List<LineDto>>>(url);
            return response == null ? new List<LineDto>() : response.Data.ToList();
        }

        /// <summary>
        /// Get aggregated measurements per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        /// <summary>
        /// Get either current og temperature for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetLatestMeasurements(LineDto line, MeasurementType measurementType, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
            if (measurementType == MeasurementType.WireTemperature)
            {
                var url = UrlBuilder.BuildLatestConductorTemperatureUrl(line, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url); 
                if (response == null) return new AggregatedFloatValueDto();
                return aggregationType == AggregationType.Max ? 
                    new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max } 
                    : new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
            }
            else{
                var url = UrlBuilder.BuildLatestCurrentsUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<CurrentDto>>(url);
                if (response == null) return new AggregatedFloatValueDto();
                return new AggregatedFloatValueDto { IntervalStartTime = response.Data.Current.Timestamp, Value = response.Data.Current.Value };
            }
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
        public async Task<List<LineAggregatedDLRForecastDto>> GetAggregatedDlrForecast(LineDto line, int hoursAhead, DLRType? dlrType = null)
        {
            var url = UrlBuilder.BuildAggregatedDlrForecastUrl(line, hoursAhead, dlrType);
            var response = await _heimdallClient.Get<ApiResponse<List<LineAggregatedDLRForecastDto>>>(url);

            return response != null ? response.Data : new();
        }
    }
}