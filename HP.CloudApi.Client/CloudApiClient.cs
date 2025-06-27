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
        /// Get all available assets.
        /// </summary>
        public async Task<List<LineDto>> GetLines()
        {
            var url = UrlBuilder.BuildAssetsUrl();
            var response = await _heimdallClient.Get<AssetsResponseObject>(url);
            
            var lines = new List<LineDto>();

            foreach (var gridOwner in response.Data.GridOwners)
            {
                foreach (var facility in gridOwner.Facilities)
                {
                    if(facility?.Line != null) lines.Add(facility.Line);
                }
            }

            return lines;
        }

        /// <summary>
        /// Get either current og temperature for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetAggregatedMeasurements(LineDto line, MeasurementType measurementType, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
            if (measurementType == MeasurementType.WireTemperature)
            {
                var url = UrlBuilder.BuildLatestConductorTemperatureUrl(line, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url); 
                if (response == null) return new AggregatedFloatValueDto();
                return aggregationType == AggregationType.Max ? 
                    new AggregatedFloatValueDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max } 
                    : new AggregatedFloatValueDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
            }
            else{
                var url = UrlBuilder.BuildLatestCurrentsUrl(line);
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
        public async Task<List<ForecastDto>> GetAggregatedDlrForecast(LineDto line, int hoursAhead)
        {
            var url = UrlBuilder.BuildDlrForecastUrl(line, hoursAhead);
            var response = await _heimdallClient.Get<ApiResponse<AarForecastDto>>(url);

            return response != null ? response.Data.AarForecast.Forecasts : new();
        }
    }
}