using OPAQUE.Net;
using OPAQUE.Net.Types.Results;

namespace DotLock.Services.Auth.Services;

public sealed class OpaqueService
{
    private readonly string _serverSetup;
    private readonly OpaqueServer _server;
    
    public OpaqueService(IConfiguration config)
    {
        _serverSetup = config["Opaque:ServerSetup"]
            ?? throw new InvalidOperationException("Missing configuration: Opaque:ServerSetup");

        _server = new OpaqueServer();
    }

    public string CreateRegistrationResponse(string userIdentifier, string registrationRequest)
    {
        if (!_server.CreateRegistrationResponse( _serverSetup, 
                                                userIdentifier, 
                                                registrationRequest, 
                                                out string? registrationResponse ))

            throw new InvalidOperationException("OPAQUE CreateRegistrationResponse failed.");

        return registrationResponse!;
    }

    public StartServerLoginResult StartLogin(string userIdentifier, string startLoginRequest, string registrationRecord)
    {
        if (!_server.StartLogin( _serverSetup,
                                 startLoginRequest,
                                 userIdentifier,
                                 registrationRecord,
                                 out StartServerLoginResult? result,
                                 out Exception? exception ))

            throw new InvalidOperationException("OPAQUE StartLogin failed.", exception);
        
        return result!;
    }

    public string FinishLogin(string serverLoginState, string finishLoginRequest)
    {
        if (!_server.FinishLogin( serverLoginState,
                                  finishLoginRequest,
                                  out string? sessionKey ))

            throw new InvalidOperationException("OPAQUE FinishLogin failed.");

        return sessionKey!;   
    }
}
