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
        /// Get a list of all line objects.
        /// </summary>
        public async Task<List<LineDto>> GetLines()
        {
            var url = UrlBuilder.BuildAssetsUrl();
            var response = await _heimdallClient.Get<AssetsResponseObject>(url);

            if (response?.Data == null) return new List<LineDto>();

            return response.Data.GridOwners
                .Where(go => go.Facilities != null)
                .SelectMany(go => go.Facilities
                    .Where(facility => facility.Line != null)
                    .Select(facility =>
                    {
                        facility.Line.Owner = go.Name;
                        return facility.Line;
                    }))
                .ToList();
        }
        
        /// <summary>
        /// Get a list of all facilities.
        /// </summary>
        public async Task<List<FacilityDto>> GetFacilities()
        {
            
            var url = UrlBuilder.BuildAssetsUrl();
            var response = await _heimdallClient.Get<AssetsResponseObject>(url);

            if (response?.Data == null) return new List<FacilityDto>();

            return response.Data.GridOwners
                .Where(go => go.Facilities != null)
                .SelectMany(go => go.Facilities
                    .Where(facility => facility.Line != null)
                .Select(facility =>
                    {
                        facility.Line.Owner = go.Name;
                        return facility;
                    }))
                .ToList();
        }
        
        /// <summary>
        /// Get latest current for a line
        /// </summary>
        public async Task<MeasurementDto> GetLatestCurrent(LineDto line, string unitSystem = "metric")
        {
            var url = UrlBuilder.BuildLatestCurrentsUrl(line.Id);
            var response = await _heimdallClient.Get<ApiResponse<CurrentDto>>(url);
            if (response == null) return new MeasurementDto();
            return new MeasurementDto
                { Timestamp = response.Data.Current.Timestamp, Value = response.Data.Current.Value };
        }
        
        /// <summary>
        /// Get latest temperature for a line
        /// </summary>
        public async Task<MeasurementDto> GetLatestConductorTemperature(LineDto line, AggregationType aggregationType = AggregationType.Max, string unitSystem = "metric")
        {
                var url = UrlBuilder.BuildLatestConductorTemperatureUrl(line.Id, unitSystem);
                var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url); 
                if (response == null) return new MeasurementDto();
                return aggregationType == AggregationType.Max ? 
                    new MeasurementDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Max } 
                    : new MeasurementDto { Timestamp = response.Data.ConductorTemperatures.Timestamp, Value = response.Data.ConductorTemperatures.Min };
        }

        /// <summary>
        /// Get latest Heimdall dynamic line ratings for a line
        /// </summary>
        public async Task<DLRDto> GetLatestHeimdallDlr(LineDto line)
        {
            var url = UrlBuilder.BuildHeimdallDlrUrl(line.Id);
            var response = await _heimdallClient.Get<ApiResponse<HeimdallDlrDto>>(url);
            if (response?.Data == null) return new DLRDto();
            return new DLRDto() { Timestamp = response.Data.Dlr.Timestamp, Ampacity = response.Data.Dlr.Value };
        }
        
        /// <summary>
        /// Get latest Heimdall ambient-adjusted rating for a line
        /// </summary>
        public async Task<DLRDto> GetLatestHeimdallAar(LineDto line)
        {
            var url = UrlBuilder.BuildHeimdallAarUrl(line.Id);
            var response = await _heimdallClient.Get<ApiResponse<HeimdallAarDto>>(url);
            if (response?.Data == null) return new DLRDto();
            return new DLRDto() { Timestamp = response.Data.Aar.Timestamp, Ampacity = response.Data.Aar.Value };
        }

        /// <summary>
        /// Get hourly Heimdall DLR forecasts up to 72 hours ahead in time
        /// </summary>
        public async Task<List<ForecastDto>> GetHeimdallDlrForecast(LineDto line)
        {
            var url = UrlBuilder.BuildDlrForecastUrl(line.Id);
            var response = await _heimdallClient.Get<ApiResponse<DlrForecastDto>>(url);

            return response?.Data != null ? response.Data.DlrForecasts : new List<ForecastDto>();
        }
        
        /// <summary>
        /// Get hourly Heimdall AAR forecasts up to 240 hours ahead in time
        /// </summary>
        public async Task<List<ForecastDto>> GetHeimdallAarForecast(LineDto line)
        {
            var url = UrlBuilder.BuildAarForecastUrl(line.Id);
            var response = await _heimdallClient.Get<ApiResponse<AarForecastDto>>(url);

            return response?.Data != null ? response.Data.AarForecasts : new List<ForecastDto>();
        }
        
        /// <summary>
        /// Get the most recent circuit rating forecasts for a specified facility.
        /// The forecasted hours returned by the endpoint are set to 72 hours
        /// and are provided in 1-hour intervals.
        /// </summary>
        /// <param name="facility">The facility for which to retrieve circuit rating forecasts.</param>

        public async Task<IReadOnlyList<ForecastDto>> GetCircuitRatingForecast(FacilityDto facility)
        {
            var url = UrlBuilder.BuildCircuitRatingForeceastUrl(facility.Id);
            var response = await _heimdallClient.Get<ApiResponse<CircuitRatingForecastDto>>(url);

            return response?.Data != null ? response.Data.CircuitRatingForecasts : new List<ForecastDto>();
        }
    }
}