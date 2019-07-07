
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Together.UI.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Icon : Label
    {
        public Icon()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(string), typeof(Icon), "\u0030");
        public string Type
        {
            get => (string)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }
    }
}