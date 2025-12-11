namespace DotLock.Shared.Contracts.Auth;

public record RegisterFinishRequest(
    string Username, 
    string RegistrationRecord
);