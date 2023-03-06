namespace ArchGis
{
    public interface IEsriClient
    {
        Task<EsriTokenResponse> GetToken();
    }
}
