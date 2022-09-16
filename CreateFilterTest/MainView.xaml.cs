using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CreateFilterTest
{
    public partial class MainView : Window
    {
        public MainView(ExternalCommandData commandData)
        {
            InitializeComponent();
            MainViewViewModel vm = new MainViewViewModel(commandData);
            vm.HideRequest += (s, e) => Hide(); //отвечает за то, чтобы скрыть активное окно
            vm.ShowRequest += (s, e) => Show(); //отвечает за то, чтобы показать активное окно

            DataContext = vm; //св-во датаконтекст является тем св-вом, по которому можно найти обработчики команд в окне разметки приложения
        }
    }
}
