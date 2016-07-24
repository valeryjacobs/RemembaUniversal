using RemembaUniversal.Extensions;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace RemembaUniversal.Converters
{
    public class WebContentViewConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (ControlLocator.ContentView == null) ControlLocator.ContentView = (WebView)FindDescendantByName((FrameworkElement)Window.Current.Content, parameter.ToString());

            ControlLocator.ContentView.Height = ((Grid)ControlLocator.ContentView.Parent).Height;

            if (value == null) value = "";

            if (ControlLocator.ContentViewReady && ControlLocator.ContentViewSmallReady && ControlLocator.ContentView.Source.LocalPath == "/ContentEditor/ContentTemplate.html")
            {
                ControlLocator.ContentView.InvokeScriptAsync("SetContent", new string[] { value.ToString() });
            }
            else
            {
                Uri url = ControlLocator.ContentView.BuildLocalStreamUri("MyTag", "/ContentEditor/ContentTemplate.html");
                ControlLocator.ContentView.NavigateToLocalStreamUri(url, ControlLocator.StreamResolver);

                ControlLocator.ContentView.DOMContentLoaded += (x, y) =>
                {
                    ControlLocator.ContentView.InvokeScriptAsync("SetContent", new string[] { value.ToString() });
                };

                ControlLocator.ContentViewReady = true;
                ControlLocator.ContentViewSmallReady = true;
            }


            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }


        public static FrameworkElement FindDescendantByName(FrameworkElement element, string name)
        {
            if (element == null || string.IsNullOrWhiteSpace(name)) { return null; }

            if (name.Equals(element.Name, StringComparison.OrdinalIgnoreCase))
            {
                return element;
            }
            var childCount = VisualTreeHelper.GetChildrenCount(element);
            for (int i = 0; i < childCount; i++)
            {
                var result = (VisualTreeHelper.GetChild(element, i) as FrameworkElement).FindDescendantByName(name);
                if (result != null) { return result; }
            }
            return null;
        }

    }

}
