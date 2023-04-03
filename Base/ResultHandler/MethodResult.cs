namespace Utility.Base.ResultHandler;

// Describes a Basic return Value of an generic Method
public class MethodResult<T> : IComparable<MethodResult<T>> where T : IComparable
{
    private Exception? _error;

    private T? _output;
    public bool Success { get; private set; }

    public Exception? Error
    {
        get => _error;
        set
        {
            _error = value;
            Success = false;
        }
    }

    public T? Output
    {
        get => _output;
        set
        {
            _output = value;
            if (Error != null)
                Success = true;
        }
    }

    // Compares two Elements and returns equal if
    // Success is for both false
    // Success if for both true and Output has the same value 
    public int CompareTo(MethodResult<T>? other)
    {
        if (other == null) return 1;
        if (Success != other.Success) return 1;
        if (!Success) return 0;
        if (Output == null && other.Output == null) return 0;
        if (Output == null || other.Output == null) return -1;
        return Output.CompareTo(other.Output) == 0 ? 0 : 1;
    }
}