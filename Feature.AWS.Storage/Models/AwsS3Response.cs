using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Feature.AWS.Storage.Models
{
    public class AwsS3Response
    {
        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }
    }
}
