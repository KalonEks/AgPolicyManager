namespace AgPolicy.Api.Services;

public enum ServiceResultStatus
{
    Success,
    Invalid,
    NotFound,
    Conflict
}

public class ServiceResult<T>
{
    private ServiceResult(ServiceResultStatus status, T? value, string? error)
    {
        Status = status;
        Value = value;
        Error = error;
    }

    public ServiceResultStatus Status { get; }
    public T? Value { get; }
    public string? Error { get; }

    public static ServiceResult<T> Success(T value) => new(ServiceResultStatus.Success, value, null);
    public static ServiceResult<T> Invalid(string error) => new(ServiceResultStatus.Invalid, default, error);
    public static ServiceResult<T> NotFound(string error) => new(ServiceResultStatus.NotFound, default, error);
    public static ServiceResult<T> Conflict(string error) => new(ServiceResultStatus.Conflict, default, error);
}

public class ServiceResult
{
    private ServiceResult(ServiceResultStatus status, string? error)
    {
        Status = status;
        Error = error;
    }

    public ServiceResultStatus Status { get; }
    public string? Error { get; }

    public static ServiceResult Success() => new(ServiceResultStatus.Success, null);
    public static ServiceResult Invalid(string error) => new(ServiceResultStatus.Invalid, error);
    public static ServiceResult NotFound(string error) => new(ServiceResultStatus.NotFound, error);
    public static ServiceResult Conflict(string error) => new(ServiceResultStatus.Conflict, error);
}
