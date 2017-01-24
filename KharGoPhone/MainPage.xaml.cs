using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Popups;
using Logic.Intepreter;
using Logic.Factory_method;
using KharGo.Mobile;
using Windows.Storage;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using KharGo.MVVM;
using KharGo.Facade;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=391641

namespace KharGo
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        bool Exist { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            ActionWordFactory factory = new StartWordFactory();
            factory.Create();
            factory = new StopWordFactory();
            factory.Create();
            var word = Prepare.InitalizeProgramms().Values.ToList();

            datacontext = new MainWindowViewModel();
            datacontext.CommandList = new List<string>();
            datacontext.CommandList = word.Skip(2).Select(x => x.list.FirstOrDefault().list.FirstOrDefault()).ToList();
            DataContext = datacontext;
        }

        MainWindowViewModel datacontext;
        
        /// <summary>
        /// Вызывается перед отображением этой страницы во фрейме.
        /// </summary>
        /// <param name="e">Данные события, описывающие, каким образом была достигнута эта страница.
        /// Этот параметр обычно используется для настройки страницы.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Подготовьте здесь страницу для отображения.

            // TODO: Если приложение содержит несколько страниц, обеспечьте
            // обработку нажатия аппаратной кнопки "Назад", выполнив регистрацию на
            // событие Windows.Phone.UI.Input.HardwareButtons.BackPressed.
            // Если вы используете NavigationHelper, предоставляемый некоторыми шаблонами,
            // данное событие обрабатывается для вас.
        }

        private async void Test(string com)
        {
            await Launcher.LaunchUriAsync(new Uri(com));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MenuFacade mf = new MenuFacade();
            Test(mf.Run(listBox.SelectedItem.ToString()));
        }
    }
}
