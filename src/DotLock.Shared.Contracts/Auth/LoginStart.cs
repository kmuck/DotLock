namespace DotLock.Shared.Contracts.Auth;

public record LoginStartRequest(
    string Username, 
    string StartLoginRequest
);

public record LoginStartResponse(
    string LoginResponse, 
    Guid LoginStateId
);