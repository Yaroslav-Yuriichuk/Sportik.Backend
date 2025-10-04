namespace Sportik.Backend.Domain.Common;

public sealed class OperationResult<T>
{
    public bool Succeeded { get; private set; }
    public T? Value { get; private set; }
    public List<string> Errors { get; private set; } = new();

    public static OperationResult<T> Success(T value)
    {
        return new OperationResult<T> { Succeeded = true, Value = value };
    }

    public static OperationResult<T> Failure(IEnumerable<string> errors)
    {
        return new OperationResult<T> { Succeeded = false, Errors = errors.ToList() };
    }
}
