using Microsoft.Extensions.Options;

namespace ASPA006_1
{
    public class CelebritiesConfig
    {
        public string PhotosFolder { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }

}
