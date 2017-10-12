using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Facade.DataStruct
{
    public struct AccessToken
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }

    public struct JsapiTicket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
    }

    public class TicketCache
    {
        public string access_token { get; set; }
        public string ticket { get; set; }
    }
}
