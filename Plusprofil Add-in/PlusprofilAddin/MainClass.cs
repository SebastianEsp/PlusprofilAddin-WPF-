using System;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels;
using PlusprofilAddin.Views;

namespace PlusprofilAddin
{
	/// <summary>
	/// 
	/// </summary>
	public class MainClass
	{
		public void EA_Connect(Repository repository)
		{
			//Create a new hotkeyForm to allow the add-in window to be opened using hotkeys
			_hotkeyForm = new InvisibleHotkeyForm(this, repository, DanishMenuOption, EnglishMenuOption);
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
			ObjectType itemType = repository.GetContextItemType();

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

				// Check for ConnectorEnd stereotypes. If it is unknown, hide that side of the interface
			}

			//Create new ResourceDictionary and set source for language matching the selected menu option
			ResourceDictionary dict = new ResourceDictionary();
			switch (itemName)
			{
				case DanishMenuOption:
					dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.da-DK.xaml",
						UriKind.Absolute);
					break;
				case EnglishMenuOption:
					dict.Source = new Uri("pack://application:,,,/PlusprofilAddin;component/Resources/StringResources.en-US.xaml",
						UriKind.Absolute);
					break;
			}
			_window.Resources.MergedDictionaries.Add(dict);
			_viewModel.ResourceDictionary = dict;
			_viewModel.Initialize();
			_window.DataContext = _viewModel;

			//Set window size
			_window.MinHeight = _window.Height = 520;
			_window.MinWidth = _window.Width = 540;

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
		private InvisibleHotkeyForm _hotkeyForm;

		#endregion
	}
}