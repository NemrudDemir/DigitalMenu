using DigitalMenu.DataAccess.Settings;
using HealthChecks.MongoDb;
using Microsoft.Extensions.Options;

namespace DigitalMenu.DataAccess.HealthChecks
{
    public class DigitalMenuDbHealthCheck : MongoDbHealthCheck
    {
        public DigitalMenuDbHealthCheck(IOptions<MongoDbSettings> options) : base(options.Value.ConnectionString, options.Value.DatabaseName)
        {
        }
    }
}
