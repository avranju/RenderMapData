using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenderMapData.Models
{
    public class Polygon
    {
        [JsonProperty("points")]
        public Point[] Points { get; set; }
    }
}
