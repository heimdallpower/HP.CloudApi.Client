using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeimdallPower.Assets;
using HeimdallPower.CapacityMonitoring.Facilities;
using HeimdallPower.CapacityMonitoring.Lines;
using HeimdallPower.GridInsights.Lines;

namespace HeimdallPower;

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
        var response = await _heimdallClient.Get<ApiResponse<AssetsResponse>>(url);

        if (response?.Data == null) return new List<LineDto>();

        return response.Data.GridOwners
            .Where(go => go?.Facilities is { Count: > 0 })
            .SelectMany(go => go.Facilities
                .Where(facility => facility?.Line != null)
                .Select(facility => facility.Line))
            .ToList();
    }
    
    /// <summary>
    /// Get a list of all facilities.
    /// </summary>
    public async Task<List<FacilityDto>> GetFacilities()
    {
        var url = UrlBuilder.BuildAssetsUrl();
        var response = await _heimdallClient.Get<ApiResponse<AssetsResponse>>(url);
        
        if (response?.Data == null) return new List<FacilityDto>();

        return response.Data.GridOwners
            .Where(go => go?.Facilities is { Count: > 0 })
            .SelectMany(go => go.Facilities)
            .ToList();
    }
    
    /// <summary>
    /// Get latest current for a line
    /// </summary>
    public async Task<LatestCurrentResponse?> GetLatestCurrent(Guid lineId, string unitSystem = "metric")
    {
        var url = UrlBuilder.BuildLatestCurrentsUrl(lineId);
        var response = await _heimdallClient.Get<ApiResponse<LatestCurrentResponse>>(url);
        
        return response?.Data;
    }
    
    /// <summary>
    /// Get latest temperature for a line
    /// </summary>
    public async Task<ConductorTemperatureDto?> GetLatestConductorTemperature(Guid lineId, string unitSystem = "metric")
    {
        var url = UrlBuilder.BuildLatestConductorTemperatureUrl(lineId, unitSystem);
        var response = await _heimdallClient.Get<ApiResponse<ConductorTemperatureDto>>(url);
        return response?.Data;
    }

    /// <summary>
    /// Get latest Heimdall dynamic line ratings for a line
    /// </summary>
    public async Task<LatestHeimdallDlrResponse?> GetLatestHeimdallDlr(Guid lineId)
    {
        var url = UrlBuilder.BuildHeimdallDlrUrl(lineId);
        var response = await _heimdallClient.Get<ApiResponse<LatestHeimdallDlrResponse>>(url);
        
        return response?.Data;
    }
    
    /// <summary>
    /// Get latest Heimdall ambient-adjusted rating for a line
    /// </summary>
    public async Task<LatestHeimdallAarResponse?> GetLatestHeimdallAar(Guid lineId)
    {
        var url = UrlBuilder.BuildHeimdallAarUrl(lineId);
        var response = await _heimdallClient.Get<ApiResponse<LatestHeimdallAarResponse>>(url);
        
        return response?.Data;
    }

    /// <summary>
    /// Get hourly Heimdall DLR forecasts up to 72 hours ahead in time
    /// </summary>
    public async Task<HeimdallDlrForecastResponse?> GetHeimdallDlrForecast(Guid lineId)
    {
        var url = UrlBuilder.BuildDlrForecastUrl(lineId);
        var response = await _heimdallClient.Get<ApiResponse<HeimdallDlrForecastResponse>>(url);

        return response?.Data;
    }
    
    /// <summary>
    /// Get hourly Heimdall AAR forecasts up to 240 hours ahead in time
    /// </summary>
    public async Task<HeimdallAarForecastResponse?> GetHeimdallAarForecast(Guid lineId)
    {
        var url = UrlBuilder.BuildAarForecastUrl(lineId);
        var response = await _heimdallClient.Get<ApiResponse<HeimdallAarForecastResponse>>(url);

        return response?.Data;
    }
    
    /// <summary>
    /// Get the most recent circuit rating forecasts for a specified facility.
    /// The forecasted hours returned by the endpoint are set to 72 hours
    /// and are provided in 1-hour intervals.
    /// </summary>
    /// <param name="facilityId">Id of the facility for which to retrieve circuit rating forecasts.</param>

    public async Task<CircuitRatingForecastResponse?> GetCircuitRatingForecast(Guid facilityId)
    {
        var url = UrlBuilder.BuildCircuitRatingForecastUrl(facilityId);
        var response = await _heimdallClient.Get<ApiResponse<CircuitRatingForecastResponse>>(url);

        return response?.Data;
    }
    
    /// <summary>
    /// Get the most recent circuit rating forecasts for a specified facility.
    /// The forecasted hours returned by the endpoint are set to 72 hours
    /// and are provided in 1-hour intervals.
    /// </summary>
    /// <param name="facilityId">Id of the facility for which to retrieve circuit rating forecasts.</param>

    public async Task<CircuitRatingResponse?> GetLatestCircuitRating(Guid facilityId)
    {
        var url = UrlBuilder.BuildCircuitRatingUrl(facilityId);
        var response = await _heimdallClient.Get<ApiResponse<CircuitRatingResponse>>(url);
        
        return response?.Data;
    }
}