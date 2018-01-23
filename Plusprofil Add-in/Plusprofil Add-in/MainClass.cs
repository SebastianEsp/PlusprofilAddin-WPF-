using System;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels;
using PlusprofilAddin.Views;

namespace PlusprofilAddin
{
	public class MainClass
	{
		public void EA_Connect(Repository repository)
		{
		}

		public object EA_GetMenuItems(Repository repository, string location, string menuName)
		{
			switch (menuName)
			{
				case "":
					return MenuHeader;
				case MenuHeader:
					string[] subMenus = {DanishMenuOption, EnglishMenuOption};
					return subMenus;
			}

			return "";
		}

		public void EA_MenuClick(Repository repository, string location, string menuName, string itemName)
		{
			dynamic itemType = repository.GetContextItemType();

			if (itemType == ObjectType.otElement)
			{
				_window = new ElementDialog();
				_viewModel = new ElementDialogViewModel
				{
					Repository = repository
				};
			}
			else if (itemType == ObjectType.otPackage)
			{
				_window = new PackageDialog();
				_viewModel = new PackageDialogViewModel
				{
					Repository = repository
				};
			}
			else if (itemType == ObjectType.otAttribute)
			{
				_window = new AttributeDialog();
				_viewModel = new AttributeDialogViewModel
				{
					Repository = repository
				};
			}
			else if (itemType == ObjectType.otConnector)
			{
				_window = new ConnectorDialog();
				_viewModel = new ConnectorDialogViewModel
				{
					Repository = repository
				};
			}

			//Create new Application, ResourceDictionary and set source for language matching the selected menu option
			ResourceDictionary dict = new ResourceDictionary();
			switch (itemName)
			{
				case DanishMenuOption:
					dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.da-DK.xaml",
						UriKind.Absolute);
					break;
				case EnglishMenuOption:
					dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.xaml",
						UriKind.Absolute);
					break;
			}
			_window.Resources.MergedDictionaries.Add(dict);

			_viewModel.Initialize();
			_window.DataContext = _viewModel;
			_window.ShowDialog();
		}

		public void EA_Disconnect()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		#region Fields

		private const string MenuHeader = "-&Plusprofil Editing Window (WPF)";
		private const string EnglishMenuOption = "&Open English Editing Window";
		private const string DanishMenuOption = "&Open Danish Editing Window";
		private Window _window;
		private DialogViewModel _viewModel;

		#endregion
	}
}