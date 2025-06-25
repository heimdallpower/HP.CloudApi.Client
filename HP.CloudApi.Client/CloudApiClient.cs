using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeimdallPower.Entities;
using HeimdallPower.Enums;

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
        /// Get either current og temperature for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetAggregatedMeasurements(LineDto line, MeasurementType measurementType, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
            var url = "";
            if (measurementType == MeasurementType.WireTemperature)
            {
                url = UrlBuilder.BuildLatestConductorTemperatureUrl(line, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url); 
                if (response == null) return new AggregatedFloatValueDto();
                return aggregationType == AggregationType.Max ? 
                    new AggregatedFloatValueDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max } 
                    : new AggregatedFloatValueDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
            }
            else{
                url = UrlBuilder.BuildLatestCurrentsUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<CurrentDto>>(url);
                if (response == null) return new AggregatedFloatValueDto();
                return new AggregatedFloatValueDto { Timestamp = response.Data.Current.Timestamp, Value = response.Data.Current.Value };
            }
        }

        /// <summary>
        /// Get aggregated dynamic line ratings for a line
        /// </summary>
        public async Task<DLRDto> GetAggregatedDlr(LineDto line, DLRType dlrType)
        {
            if (dlrType == DLRType.HeimdallDLR)
            {
                var url = UrlBuilder.BuildHeimdallDlrUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<HeimdallDlrDto>>(url);
                if (response == null) return new DLRDto();
                return new DLRDto() { Timestamp = response.Data.Dlr.Timestamp, Ampacity = response.Data.Dlr.Value };
            }
            else
            {
                var url = UrlBuilder.BuildHeimdallAarUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<HeimdallAarDto>>(url);
                if (response == null) return new DLRDto();
                return new DLRDto() { Timestamp = response.Data.Aar.Timestamp, Ampacity = response.Data.Aar.Value };
            }
        }

        /// <summary>
        /// Get hourly DLR forecasts up to 240 hours ahead in time
        /// </summary>
        public async Task<List<LineAggregatedDLRForecastDto>> GetAggregatedDlrForecast(LineDto line, int hoursAhead)
        {
            var url = UrlBuilder.BuildAggregatedDlrForecastUrl(line, hoursAhead);
            var response = await _heimdallClient.Get<ApiResponse<List<LineAggregatedDLRForecastDto>>>(url);

            return response != null ? response.Data : new();
        }
    }
}