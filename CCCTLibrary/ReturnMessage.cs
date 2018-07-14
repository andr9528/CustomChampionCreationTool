using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Data;

namespace CCCTLibrary
{
    public class ReturnMessage
    {
        public string Message { get; set; }
        public bool WasSuccesful { get; set; }
        public Exception Exception { get; set; }
        public ReturnMessage ChainMessage { get; set; }
        public string Where { get; set; }
        public DataTable DBScheme { get; set; }
        public ConnectionState DBState { get; set; }

    }
}