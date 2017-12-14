using System;
using System.Windows;
using System.Windows.Resources;

using EA;
using PlusprofilAddin.ViewModels;

namespace PlusprofilAddin
{
    public class MainClass
    {
        #region Fields
        const string menuHeader = "-&Plusprofil Editing Window (WPF)";
        const string englishMenuOption = "&Open English Editing Window";
        const string danishMenuOption = "&Open Danish Editing Window";
        Window Window;
        DialogViewModel ViewModel;
        #endregion

        public void EA_Connect(Repository Repository)
        {

        }
        public object EA_GetMenuItems(Repository Repository, string Location, string MenuName)
        {
            switch (MenuName)
            {
                case "":
                    return menuHeader;
                case menuHeader:
                    string[] subMenus = { danishMenuOption, englishMenuOption };
                    return subMenus;
            }
            return "";
        }

        public void EA_MenuClick(Repository Repository, string Location, string MenuName, string ItemName)
        {
            dynamic itemType = Repository.GetContextItemType();
            
            if(itemType == ObjectType.otElement)
            {
                Window = new ElementDialog();
                ViewModel = new ElementDialogViewModel
                {
                    Repository = Repository
                };
            }
            else if (itemType == ObjectType.otPackage)
            {
                Window = new PackageDialog();
                ViewModel = new PackageDialogViewModel
                {
                    Repository = Repository
                };
            }
            else if (itemType == ObjectType.otAttribute)
            {
                Window = new AttributeDialog();
                ViewModel = new AttributeDialogViewModel
                {
                    Repository = Repository
                };
            }
            else if (itemType == ObjectType.otConnector){
                Window = new ConnectorDialog();
                ViewModel = new ConnectorDialogViewModel
                {
                    Repository = Repository
                };
            }

            //Create new ResourceDictionary and set source for language matching the selected menu option
            ResourceDictionary dict = new ResourceDictionary();
            if (ItemName == danishMenuOption) dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.da-DK.xaml", UriKind.Absolute);
            else if (ItemName == englishMenuOption) dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.xaml", UriKind.Absolute);
            Window.Resources.MergedDictionaries.Add(dict);

            ViewModel.Initialize();
            Window.DataContext = ViewModel;
            Window.Show();
        }

        public void EA_Disconnect()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}