using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public CloudApiClient(string clientId, string certificatePath, string certificatePassword)
        {
            _heimdallClient = new HeimdallHttpClient(clientId, certificatePath, certificatePassword);
        }

        public CloudApiClient(string clientId, X509Certificate2 certificate)
        {
            _heimdallClient = new HeimdallHttpClient(clientId, certificate);
        }

        /// <summary>
        /// Get a list of line objects. The line object consists of its id, the spans of the line, and the spanphases of each span. The ids of the lines, spans and phases can be used to query the other endpoints.
        /// </summary>
        public async Task<List<LineDto>> GetLines()
        {
            var response = await _heimdallClient.Get<ApiResponse<List<LineDto>>>("lines");
            return response == null ? new List<LineDto>() : response.Data.ToList();
        }

        private static string GetDateTimeParams(DateTime from, DateTime to)
        {
            const string dateFormat = "yyyy-MM-ddThh:mm:ss.fffZ";
            return $"fromDateTime={from.ToString(dateFormat)}&toDateTime={to.ToString(dateFormat)}";
        }

        private static string GetIdentifierParam(LineDto line, SpanDto span, SpanPhaseDto spanPhase)
        {
            if (spanPhase != null)
                return $"spanPhaseId={spanPhase.Id}";
            if (span != null)
                return $"spanId={span.Id}";
            return $"lineId={line.Id}";
        }

        private static string BuildAggregatedMeasurementsUrl(LineDto line, SpanDto span, DateTime from, DateTime to,
            string intervalDuration, MeasurementType measurementType, AggregationType aggregationType)
        {
            return "aggregated-measurements?" +
                   $"intervalDuration={intervalDuration}&" +
                   $"measurementType={measurementType}&" +
                   $"aggregationType={aggregationType}&" +
                   $"{GetDateTimeParams(from, to)}&" +
                   $"{GetIdentifierParam(line, span, null)}";
        }

        /// <summary>
        /// Get aggregated measurements per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<List<AggregatedFloatValueDto>> GetAggregatedMeasurements(LineDto line, SpanDto span,
            DateTime from, DateTime to, string intervalDuration, MeasurementType measurementType,
            AggregationType aggregationType)
        {
            var url = BuildAggregatedMeasurementsUrl(line, span, from, to, intervalDuration, measurementType,
                aggregationType);

            var response = await _heimdallClient.Get<ApiResponse<List<AggregatedFloatValueDto>>>(url);

            return response != null ? response.Data.ToList() : new();
        }

        private static string BuildIcingUrl(LineDto line, SpanDto span, DateTime from, DateTime to)
        {
            return "icing-data?" +
                   $"{GetDateTimeParams(from, to)}&" +
                   $"{GetIdentifierParam(line, span, null)}";
        }

        /// <summary>
        /// Get icing data per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<LineDto<IcingDataDto>> GetIcingData(LineDto line, SpanDto span, DateTime from, DateTime to)
        {
            var url = BuildIcingUrl(line, span, from, to);
            var response = await _heimdallClient.Get<ApiResponse<LineDto<IcingDataDto>>>(url);

            return response != null ? response.Data : new();
        }

        private static string BuildSagAndClearanceUrl(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            return "sag-and-clearances?" +
                   $"{GetDateTimeParams(from, to)}&" +
                   $"{GetIdentifierParam(line, span, spanPhase)}";
        }

        /// <summary>
        /// Get sag and clearances per spanPhase belonging to the most specific Line, Span or SpanPhase supplied (spanPhase > span > line).
        /// </summary>
        public async Task<List<LineDto<SagAndClearanceDto>>> GetSagAndClearances(LineDto line, SpanDto span, SpanPhaseDto spanPhase, DateTime from, DateTime to)
        {
            var url = BuildSagAndClearanceUrl(line, span, spanPhase, from, to);
            var response = await _heimdallClient.Get<ApiResponse<List<LineDto<SagAndClearanceDto>>>>(url);

            return response != null ? response.Data : new();
        }
    }
}