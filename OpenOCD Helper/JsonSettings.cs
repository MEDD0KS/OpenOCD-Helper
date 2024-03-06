using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenOCD_Helper
{    
    public class JsonSettings
    {
        [JsonPropertyName("pathOpenOCD")]
        public string PathOpenOCD { get; set; }

        [JsonPropertyName("pathTarget")]
        public string PathTarget { get; set; }

        [JsonPropertyName("pathInterface")]
        public string PathInterface { get; set; }

        [JsonPropertyName("nameInterface")]
        public string NameInterface { get; set; }

        [JsonPropertyName("nameTarget")]
        public string NameTarget { get; set; }

        [JsonPropertyName("typeTransportInterface")]
        public string TypeTransportInterface { get; set; }

        [JsonPropertyName("pathFlashWriteFile")]
        public string PathFlashWriteFile { get; set; }

        [JsonPropertyName("valueFlashOffset")]
        public UInt32 ValueFlashOffset { get; set; }

        [JsonPropertyName("valueFlashStartAdr")]
        public UInt32 ValueFlashStartAdr { get; set; }

        [JsonPropertyName("pathFlashReadFile")]
        public string PathFlashReadFile { get; set; }
    }
}
