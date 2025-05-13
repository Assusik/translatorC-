using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TextCopy;
using translatorc_;
using WindowsInput;
using WindowsInput.Native;


class Program
{
        



    private const string APIKEY = "AQVN3g6-anTBkvd4qSzfc02Dbzrr_CxS48OivNsT";
    private static bool _isChanged = false;

    [STAThread]
    static async Task Main(string[] args)
    {
        var simulator = new InputSimulator();


        while (true)
        {
            var lang = ChangeLanguage(_isChanged);

            if (simulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.TAB) &&
                simulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_C))
            {
                
                _isChanged = !_isChanged;
                lang = ChangeLanguage(_isChanged);

                
                Console.WriteLine($"Язык сменен,Текущий язык:{lang}");
                await Task.Delay(400);
            }

            if (simulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.CONTROL) &&
                simulator.InputDeviceState.IsHardwareKeyDown(VirtualKeyCode.VK_T))  
            {
                Console.WriteLine("Переводим...");

                simulator.Keyboard
                    .ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_C);

                await Task.Delay(400);

                var stringForTranslate = ClipboardService.GetText();
                Translate(stringForTranslate, lang);
                await Task.Delay(3000);
               
                simulator.Keyboard
                    .ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
                //Console.WriteLine(ClipboardService.GetText());
              


                await Task.Delay(400);

            }

            


        }
        static async void Translate(string textToTranslate,string targetLanguage)
        {
            
            

            string apiUrl = "https://translate.api.cloud.yandex.net/translate/v2/translate";
            using (var client = new HttpClient()) { 
            
                client.DefaultRequestHeaders.Add("Authorization", $"Api-Key {APIKEY}");

                var requestBody = new
                {
                    targetLanguageCode = targetLanguage,
                    texts = new[] { textToTranslate }
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    ClipboardService.SetText( JsonParser.Convert(responseJson));
                }
                else
                {
                    Console.WriteLine($"Ошибка: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
        }
    }
    private static string ChangeLanguage(bool isChainged)
    {
        if (isChainged)
        {
            return "ru";

        }
        else
        {
            return "en";
        }
        
    }
}