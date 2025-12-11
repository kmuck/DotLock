namespace DotLock.Shared.Contracts.Auth;

public record RegisterStartRequest(
    string Username, 
    string RegistrationRequest
);

public record RegisterStartResponse(
    string RegistrationResponse
);