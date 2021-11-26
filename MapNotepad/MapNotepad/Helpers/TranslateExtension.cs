using System;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MapNotepad.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public string Content { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            string result = string.Empty;

            if (Text is not null)
            {
                ResourceManager resmgr = new ResourceManager("MapNotepad.Resource", typeof(TranslateExtension).GetTypeInfo().Assembly);

                result = resmgr.GetString(Text, Resource.Culture);

                if (result == null)
                {
                    result = Text;
                }
            }
            
            return result;
        }
    }
}
