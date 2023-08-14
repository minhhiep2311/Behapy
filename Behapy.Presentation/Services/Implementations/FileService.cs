using Behapy.Presentation.Services.Interfaces;
using Imagekit.Models;
using Imagekit.Sdk;

namespace Behapy.Presentation.Services.Implementations;

public class FileService : IFileService
{
    private ImagekitClient ImageKit { get; }

    public FileService(IConfiguration configuration)
    {
        ImageKit = new ImagekitClient(configuration["ImageKit:PublicKey"], configuration["ImageKit:PrivateKey"], configuration["ImageKit:UrlEndpoint"]);
    }

    public AuthParamResponse GetAuthenticationParameters() => ImageKit.GetAuthenticationParameters();
}
