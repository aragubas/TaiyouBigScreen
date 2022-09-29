using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ceira.Classes
{
    public class Settings
    {
        [JsonProperty("fullscreen")]
        public bool Fullscreen { get; set; }

        [JsonIgnore]
        public bool FirstTime { get; set; }


        public void LoadDefaultSettings()
        {
            Fullscreen = true;
        }
    }
}
