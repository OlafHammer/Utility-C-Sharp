using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Base.ResultHandler
{

    // Discribes a Basic return Value of an generic Method
    public class MethodResult<T> 
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
    }
}
