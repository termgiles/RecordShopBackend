using Microsoft.Extensions.Diagnostics.HealthChecks;
using RecordShopBackend.Service;

namespace RecordShopBackend.Controllers
{
    public class RecordShopHealthCheck : IHealthCheck
    {
        private readonly IRecordShopService _service;
        public RecordShopHealthCheck(IRecordShopService service)
        {
            _service = service;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var AlbumList = _service.ReturnAllAlbums();
            string resultType = AlbumList.GetType().GetGenericArguments().Single().ToString();
            if (resultType == "RecordShopBackend.Album")
            {
                return HealthCheckResult.Healthy($"There are {AlbumList.Count} albums available");
            }
            return HealthCheckResult.Unhealthy("Database did not respond as expected");
        }
    }
}
