using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RemembaUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
        }
        public IViewModel ViewModel
        {
            get { return this.DataContext as IViewModel; }
        }

        void MainView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "MinimalLayout", true);
            }
            //else if (e.NewSize.Width < e.NewSize.Height)
            //{
            //    VisualStateManager.GoToState(this, "PortraitLayout", true);
            //}
            else
            {
                VisualStateManager.GoToState(this, "DefaultLayout", true);
            }
        }
        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                (this.DataContext as MainViewViewModel).EditNodeCommand.Execute(null);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 1) return;

            (this.DataContext as MainViewViewModel).SelectParent(e.AddedItems[0]);

        }

        private void SwitchMode_Click(object sender, RoutedEventArgs e)
        {
            SwitchMode();
        }

        private void SwitchMode()
        {
            contentView.InvokeScriptAsync("Switch", null);

            //if (switchModeButton.Content.ToString() == "Edit")
            //{
            //    switchModeButton.Content = "Preview";
            //    contentView.InvokeScriptAsync("Edit", null);
            //}
            //else
            //{
            //    switchModeButton.Content = "Edit";
            //    contentView.InvokeScriptAsync("Preview", null);
            //}
        }

        private void contentView_ScriptNotify(object sender, NotifyEventArgs e)
        {

        }
    }
}
