using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace RemembaUniversal.Extensions
{
    public sealed class ControlLocator
    {
        private static volatile ControlLocator instance;
        private static object syncRoot = new Object();

        private ControlLocator() { }

        public static ControlLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ControlLocator();
                    }
                }

                return instance;
            }
        }

        public static bool ContentViewReady { get; set; }
        public static bool ContentViewSmallReady { get; set; }
        public static bool ContentEditorReady { get; set; }

        public static bool ContentEditorSmallReady { get; set; }
        private static WebView _contentEditor;


        public static WebView ContentEditor
        {
            get { return _contentEditor; }
            set { _contentEditor = value; }
        }

        private static WebView _contentView;

        public static WebView ContentView
        {
            get { return _contentView; }
            set { _contentView = value; }
        }


        private static StreamUriWinRTResolver _streamResolver;
        public static StreamUriWinRTResolver StreamResolver
        {
            get { return _streamResolver; }
            set { _streamResolver = value; }
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
