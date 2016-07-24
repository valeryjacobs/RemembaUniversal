using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService navigationService;
    
        public RelayCommand DetailsCommand { get; set; }

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            DetailsCommand = new RelayCommand(() =>
            {
                navigationService.NavigateTo("SomeObjectDetail", "some input");
            });
        }
    }
}
