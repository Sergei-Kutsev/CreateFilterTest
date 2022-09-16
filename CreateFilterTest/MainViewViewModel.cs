using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateFilterTest
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;
        public DelegateCommand SelectedParameter1 { get; } // delegetecommand -prism метод, который обрабатывает команды из разметки
        public MainViewViewModel(ExternalCommandData commandData) // конструктор класса
        {
            _commandData = commandData;
            SelectedParameter1 = new DelegateCommand(OnSelectCommand);
        }

        public event EventHandler HideRequest; //закрывает окно
        private void RaiseHideRequest() //метод к которому обращаемся чтобы закрыть окно, когда будем выбирать элемент
        {
            HideRequest? // если у него есть методы, то
                .Invoke(this, EventArgs.Empty); //будем запускать те методы, которые были привязаны к CloseRequest

        }

        public event EventHandler ShowRequest; //скрывает окно
        private void RaiseShowRequest() //метод к которому обращаемся чтобы закрыть окно, когда будем выбирать элемент
        {
            ShowRequest? // если у него есть методы, то
                .Invoke(this, EventArgs.Empty); //будем запускать те методы, которые были привязаны к CloseRequest

        }

        private void OnSelectCommand() // когда запускается команда выбора элемента, то закрывается окно и мы переходим к процессу выбора стержня 
        {
            RaiseHideRequest(); // запускаем это свойство т.е. закрываем окно, когда запускается команда выбора элемента

            UIApplication uiapp = _commandData.Application; // создаем переменную для доступа к интерфейсу ревита
            UIDocument uidoc = uiapp.ActiveUIDocument; // создаем переменную которая помогает выбирать элементы
            Document doc = uidoc.Document; // переменная которая поможет упаковать элемент из reference в element 

            var selectedObject = uidoc.Selection.PickObject(ObjectType.Element, new RebarSelection(), "Выберите арматурный стержень"); //запрашиваем выбор объекта

            string idRebar=selectedObject.ElementId.ToString();

            TaskDialog.Show("Id элемента", idRebar); // показываем id объекта, при этом окно снова больше не появляется, чтобы оно снова появилось

            RaiseShowRequest();
        }
    }
    internal class RebarSelection : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem.Category != null && elem is Rebar;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    } // метод который позволяет выбирать только арматурные стержни
}
