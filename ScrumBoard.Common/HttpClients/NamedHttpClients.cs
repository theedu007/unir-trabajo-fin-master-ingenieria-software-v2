using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard.Common.HttpClients
{
    public class NamedHttpClients
    {
        public HttpClientConfig? Backend { get; set; }
        public HttpClientConfig? Authorization { get; set; }
    }
}
