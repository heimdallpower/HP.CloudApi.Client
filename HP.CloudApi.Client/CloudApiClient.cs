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
        /// Get a list of all line objects.
        /// </summary>
        public async Task<List<LineDto>> GetLines()
        {
            var url = UrlBuilder.BuildAssetsUrl();
            var response = await _heimdallClient.Get<AssetsResponseObject>(url);
            
            var lines = new List<LineDto>();
            
            if (response?.Data == null) return lines;

            foreach (var gridOwner in response.Data.GridOwners)
            {
                if (gridOwner.Facilities == null) continue;
                foreach (var facility in gridOwner.Facilities)
                {
                    if (facility.Line == null) 
                        continue;
                        
                    var facilityLine = facility.Line;
                    facilityLine.Owner = gridOwner.Name;
                    lines.Add(facilityLine);
                }
            }

            return lines;
        }
        
        /// <summary>
        /// Get latest current for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetLatestCurrent(LineDto line, string unitSystem = "metric")
        {
            var url = UrlBuilder.BuildLatestCurrentsUrl(line);
            var response = await _heimdallClient.Get<ApiResponse<CurrentDto>>(url);
            if (response == null) return new AggregatedFloatValueDto();
            return new AggregatedFloatValueDto
                { IntervalStartTime = response.Data.Current.Timestamp, Value = response.Data.Current.Value };
        }
        
        /// <summary>
        /// Get latest temperature for a line
        /// </summary>
        public async Task<AggregatedFloatValueDto> GetLatestConductorTemperature(LineDto line, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
                var url = UrlBuilder.BuildLatestConductorTemperatureUrl(line, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url); 
                if (response == null) return new AggregatedFloatValueDto();
                return aggregationType == AggregationType.Max ? 
                    new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max } 
                    : new AggregatedFloatValueDto { IntervalStartTime = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
        }

        /// <summary>
        /// Get latest dynamic line ratings for a line
        /// </summary>
        public async Task<DLRDto> GetLatestDlr(LineDto line, DLRType dlrType)
        {
            if (dlrType == DLRType.HeimdallDLR)
            {
                var url = UrlBuilder.BuildHeimdallDlrUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<HeimdallDlrDto>>(url);
                if (response == null) return new DLRDto();
                return new DLRDto() { IntervalStartTime = response.Data.Dlr.Timestamp, Ampacity = response.Data.Dlr.Value };
            }
            else
            {
                var url = UrlBuilder.BuildHeimdallAarUrl(line);
                var response = await _heimdallClient.Get<ApiResponse<HeimdallAarDto>>(url);
                if (response == null) return new DLRDto();
                return new DLRDto() { IntervalStartTime = response.Data.Aar.Timestamp, Ampacity = response.Data.Aar.Value };
            }
        }

        /// <summary>
        /// Get hourly DLR forecasts up to 240 hours ahead in time
        /// </summary>
        public async Task<List<AarForecast>> GetDlrForecast(LineDto line, DLRType dlrType)
        {
            var url = UrlBuilder.BuildDlrForecastUrl(line);
            var response = await _heimdallClient.Get<ApiResponse<AarForecastDto>>(url);

            return response != null ? response.Data.AarForecast : new();
        }
    }
}