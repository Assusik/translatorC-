using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace translatorc_
{
    public class Translations
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("detectedLanguageCode")]
        public string DetectedLanguageCode { get; set; }
        

    }
    public class TranslationsResponce()
    {
        [JsonProperty("translations")]
        public List<Translations> Translations { get; set; }
    }
}
