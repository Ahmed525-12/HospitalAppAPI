using HospitalDomain.DTOS;
using Imagekit.Sdk;
using Microsoft.Extensions.Options;

namespace HospitalAppAPI.UploadImage
{
    public class ImageKitService
    {
        private readonly ImagekitClient _imagekitClient;

        public ImageKitService(IConfiguration configuration)
        {
            _imagekitClient = new ImagekitClient(configuration["ImageKit:PublicKey"], configuration["ImageKit:PrivateKey"], configuration["ImageKit:UrlEndpoint"]);
        }

        public async Task<string> UploadImageAsync(byte[] imageBytes, string fileName)
        {
            var uploadRequest = new FileCreateRequest
            {
                file = imageBytes,
                fileName = fileName
            };

            var response = await _imagekitClient.UploadAsync(uploadRequest);
            return response.url;
        }
    }
}