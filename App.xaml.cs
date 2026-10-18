using Microsoft.Extensions.DependencyInjection;

namespace PM2E130148
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new CalculadoraEnvios());
        }
    }
}