using System.Data.SqlClient;

namespace Utility.Base.ResultHandler;

// Specific Return Value for Methods using SQL Commands
public class SqlResult<T> : MethodResult<T> where T : IComparable
{
    public SqlCommand? Command { get; set; }
}