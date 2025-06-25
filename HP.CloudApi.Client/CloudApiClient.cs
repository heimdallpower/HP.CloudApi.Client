using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
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
        /// Get either current og temeperature for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetAggregatedMeasurements(LineDto line, MeasurementType measurementType, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
            var url = "";
            if (measurementType == MeasurementType.WireTemperature)
            {
                url = UrlBuilder.BuildLatestConductorTemperatureUrl(line, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url);
                if(aggregationType == AggregationType.Max) return new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max };
                else return new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
            }
            else{
                url = UrlBuilder.BuildLatestCurrentsUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<CurrentDto>>(url);
                return new AggregatedFloatValueDto { IntervalStartTime = response.Data.Current.Timestamp, Value = response.Data.Current.Value };
            }
        }

        /// <summary>
        /// Get aggregated dynamic line ratings for a line
        /// </summary>
        public async Task<DLRDto> GetAggregatedDlr(LineDto line, DLRType dlrType)
        {
            var url = UrlBuilder.BuildAggregatedDlrUrl(line, dlrType);
            var response = await _heimdallClient.Get<ApiResponse<DLRDto>>(url);

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