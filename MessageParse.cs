using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextCopy;


namespace translatorc_
{
    public static class MessageParse
    {
        static public void Copy(string text)
        {
           ClipboardService.SetText($"{text}");
        }
        static  public string Paste()
        {
            return ClipboardService.GetText();
        }


    }
}
