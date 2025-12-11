using OPAQUE.Net;

namespace DotLock.Services.Auth.Services;

public class OpaqueService
{
    private readonly OpaqueServer _server;

    public OpaqueService()
    {
        _server = new OpaqueServer();
    }

    public OpaqueServer GetServer()
    {
        return _server;
    }
}