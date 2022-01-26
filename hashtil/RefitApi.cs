using Refit;
using System.Threading.Tasks;

namespace SfGrid_Android
{
    public interface RefitApi
    {
        [Get("/{symbol}/")]
        Task<string> GetCompanyAsync(string symbol);
    }

    public interface GetDriversRecord
    {
        [Get("/driversloadingreportjson.php?user={username}&report_status={status}")]
        Task<string> GetDriverAsync(string username, string status);
    }
}