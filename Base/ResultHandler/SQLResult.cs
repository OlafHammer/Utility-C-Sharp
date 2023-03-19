﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Base.ResultHandler
{
    public class SQLResult<T> : MethodResult<T>
    {
        public string Statement { get; set; } = "";
        public SqlCommand? Command { get; set; }

        public SQLResult()
        {
            
        }
    }
}