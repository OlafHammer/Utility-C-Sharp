using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Base.ResultHandler
{

    // Discribes a Basic return Value of an generic Method
    public class MethodResult<T> : IComparable<MethodResult<T>> where T : IComparable
    {
        private bool _Success;
        public bool Success { get { return _Success; } }

        private Exception? _error;
        public Exception? Error 
        {
            get { return _error; }
            set { _error = value; _Success = false; }
        }

        private T? _Output;
        public T? Output 
        {
            get { return _Output; }
            set 
            {
                _Output = value;  
                if(Error != null)
                    _Success = true; 
            }
        }

        // Compares two Elements and returns equal if
        // Success is for both false
        // Success if for both true and Output has the same value 
        public int CompareTo(MethodResult<T>? other)
        {
            if(other == null) return 1;
            if (Success != other.Success) return 1;
            if (Success)
            {
                if (Output == null && other.Output == null) return 0;
                if (Output == null || other.Output == null) return -1;
                else if (Output.CompareTo(other.Output) == 0) return 0;
                return 1;
            } else
            {
                return 0;
            }
        }
    }
}
