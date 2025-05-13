using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translatorc_
{
    public static class JsonParser
    {

        public static string Convert(string json)
        {
            
            var result = JsonConvert.DeserializeObject<TranslationsResponce>(json);

            if (result?.Translations?.FirstOrDefault() != null)
            {
                return result.Translations[0].Text;
            }

            return String.Empty;
        }

    }
}
