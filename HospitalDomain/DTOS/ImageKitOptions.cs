using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDomain.DTOS
{
    public class ImageKitOptions
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string UrlEndpoint { get; set; }
    }
}