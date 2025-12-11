namespace DotLock.Shared.Contracts.Auth;

public record LoginFinishRequest(
    string Username, 
    Guid LoginStateId, 
    string FinishLoginRequest
);

public record LoginFinishResponse(
    string AccessToken
);