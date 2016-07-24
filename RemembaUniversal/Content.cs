using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public class Content : IContent
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Data { get; set; }

        [JsonProperty(PropertyName = "t")]
        public string Touched { get; set; }
    }
}
