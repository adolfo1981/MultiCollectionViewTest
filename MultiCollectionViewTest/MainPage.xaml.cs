using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MultiCollectionViewTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new ViewModel();
            Vm.LoadData();
        }

        public ViewModel Vm { get { return this.BindingContext as ViewModel; } }
    }
}
