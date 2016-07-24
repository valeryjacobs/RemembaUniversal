using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = this.CreateNavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IMainViewViewModel, MainViewViewModel>();
            SimpleIoc.Default.Register<IMindMapDataService, MindMapDataService>();
            SimpleIoc.Default.Register<IContentDataService, ContentDataService>();
        }


        public static void SetAndReg()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            var navigationService = new NavigationService();
            navigationService.Configure(PageNames.MainView, typeof(MainView));
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            SimpleIoc.Default.Register<IDialogService, DialogService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IMainViewViewModel, MainViewViewModel>();
            SimpleIoc.Default.Register<IMindMapDataService, MindMapDataService>();
            SimpleIoc.Default.Register<IContentDataService, ContentDataService>();
        }

        private INavigationService CreateNavigationService()
        {
            var navigationService = new NavigationService();
            navigationService.Configure(PageNames.MainView, typeof(MainView));

            return navigationService;
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }


        public IMainViewViewModel MainViewViewModel
        {
            get
            {
                return SimpleIoc.Default.GetInstance<IMainViewViewModel>();
            }
        }
    }
}
