using Xamarin.Forms;
using dotNET_Chat_Xamarin_Forms_Client.Services;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace dotNET_Chat_Xamarin_Forms_Client
{
    public partial class App : Xamarin.Forms.Application
    {

        public App()
        {
            Xamarin.Forms.Application.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            InitializeComponent();

            DependencyService.Register<IDialogService, DialogService>();
            DependencyService.Register<IAuthenticationService, AuthenticationService>();
            DependencyService.Register<IPropertiesService, PropertiesService>();
            DependencyService.Register<IChatsService, ChatsService>();
            DependencyService.Register<IApplicationUsersService, ApplicationUsersService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
