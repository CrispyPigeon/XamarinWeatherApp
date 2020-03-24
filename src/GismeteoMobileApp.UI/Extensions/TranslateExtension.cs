using System;
using System.Collections.Generic;
using System.Text;
using GismeteoMobileApp.Core.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GismeteoMobileApp.UI.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }


        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text is null)
            {
                return null;
            }

            return ResourceController.GetString(Text);
        }
    }
}
