using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MultiCollectionViewTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckboxView : ContentView
    {
        public CheckboxView()
        {
            InitializeComponent();
        }

        public string CustomImageSource
        {
            set => SetValue(CustomImageSourceProperty, value);
            get => (string)GetValue(CustomImageSourceProperty);
        }
        public readonly static BindableProperty CustomImageSourceProperty = BindableProperty.Create(nameof(CustomImageSource),
                                                                                                typeof(string),
                                                                                                typeof(CheckboxView),
                                                                                                defaultValue: string.Empty,
                                                                                                propertyChanged: (bindableObject, oldValue, newValue) =>
                                                                                                {
                                                                                                    ((CheckboxView)bindableObject).MyImage.Source = ImageSource.FromFile((string)newValue);
                                                                                                });
    }
}