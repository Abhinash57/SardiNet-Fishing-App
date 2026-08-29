namespace PVT15_8.Media.Endpoints;

public static class ImageEndpointsExtensions
{
    public static IEndpointRouteBuilder MapAllImageEndpoints(this IEndpointRouteBuilder app, IConfiguration config)
    {
        var imageGroup = app.MapGroup("/images").WithTags("Images").RequireAuthorization();

        imageGroup.MapProfilePictureEndpoints(config);
        imageGroup.MapReportCatchEndpoints(config);
        imageGroup.MapFishSpeciesEndpoints(config);
        imageGroup.MapFishingLureEndpoints(config);

        return app;
    }
}
