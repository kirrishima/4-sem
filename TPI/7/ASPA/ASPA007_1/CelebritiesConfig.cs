using Microsoft.Extensions.Options;

namespace ASPA007_1
{
    public class CelebritiesConfig
    {
        public string PhotosRequestPath { get; set; } = string.Empty;
        public string PhotosFolder { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }

}
