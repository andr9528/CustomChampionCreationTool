using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace CCCTLibrary
{
    public class ReturnMessage
    {
        public string Message { get; set; }
        public bool WasSuccesful { get; set; }
        public string Exception { get; set; }

    }
}