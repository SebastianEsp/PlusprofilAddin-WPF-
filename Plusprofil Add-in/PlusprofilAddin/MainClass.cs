using System;
using System.Windows;
using EA;
using PlusprofilAddin.ViewModels;
using PlusprofilAddin.Views.Dialogs;

namespace PlusprofilAddin
{
	//This s a test comment
	/// <summary>
	/// The entry point for the add-in. Upon launch, Sparx Enterprise Architect creates a new instance of MainClass and calls the <c>EA_Connect</c> event function.
	/// Event handlers defined in this class serve as communication between Enterprise Architect and the add-in, particularly <c>EA_Menuclick</c> which creates the dialog window.
	/// </summary>
	public class MainClass
	{
		/// <summary>
		/// EA_Connect events enable Add-Ins to identify their type and to respond to Enterprise Architect start up.
		/// This event occurs when Enterprise Architect first loads your Add-In. Enterprise Architect itself is loading at this time so that while a Repository object is supplied, there is limited information that you can extract from it.
		/// The chief uses for <c>EA_Connect</c> are in initializing global Add-In data and for identifying the Add-In as an MDG Add-In.
		/// </summary>
		/// <param name="repository">An EA.Repository object representing the currently open Enterprise Architect model. Poll its members to retrieve model data and user interface status information.</param>
		public void EA_Connect(Repository repository)
		{
			// Create a new hotkeyForm to allow the add-in window to be opened using hotkeys
			var hotkeyForm = new HotkeyForm(this, repository, DanishMenuOption, EnglishMenuOption);
		}

		/// <summary>
		/// The <c>EA_GetMenuItems</c> event enables the Add-In to provide the Enterprise Architect user interface with additional Add-In menu options in various context and main menus. When a user selects an Add-In menu option, an event is raised and passed back to the Add-In that originally defined that menu option.
		/// This event is raised just before Enterprise Architect has to show particular menu options to the user, and its use is described in the Define Menu Items topic.
		/// </summary>
		/// <param name="repository">/// An <c>EA.Repository</c> object representing the currently open Enterprise Architect model. Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="location">A string representing the part of the user interface that brought up the menu. This can be TreeView, MainMenu or Diagram.</param>
		/// <param name="menuName">The name of the parent menu for which sub-items are to be defined. In the case of the top-level menu this is an empty string.</param>
		/// <returns>Returns the menu items to display in Enterprise Architect.</returns>
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

		/// <summary>
		/// <c>EA_MenuClick</c> events are received by an Add-In in response to user selection of a menu option.
		/// The event is raised when the user clicks on a particular menu option. When a user clicks on one of your non-parent menu options, your Add-In receives a <c>MenuClick</c> event.
		/// Notice that your code can directly access Enterprise Architect data and UI elements using Repository methods.
		/// </summary>
		/// <param name="repository"><c>An EA.Repository</c> object representing the currently open Enterprise Architect model. Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="location">Not used</param>
		/// <param name="menuName">The name of the parent menu for which sub-items are to be defined.
		/// In the case of the top-level menu this is an empty string.</param>
		/// <param name="itemName">The name of the option actually clicked.</param>
		public void EA_MenuClick(Repository repository, string location, string menuName, string itemName)
		{
			var itemType = repository.GetContextItemType();

			switch (itemType)
			{
				case ObjectType.otElement:
					_window = new ElementDialog();
					_viewModel = new ElementDialogViewModel
					{
						Repository = repository
					};
					break;
				case ObjectType.otPackage:
					_window = new PackageDialog();
					_viewModel = new PackageDialogViewModel
					{
						Repository = repository
					};
					break;
				case ObjectType.otAttribute:
					_window = new AttributeDialog();
					_viewModel = new AttributeDialogViewModel
					{
						Repository = repository
					};
					break;
				case ObjectType.otConnector:
					_window = new ConnectorDialog();
					_viewModel = new ConnectorDialogViewModel
					{
						Repository = repository
					};
					break;
			}

			//Create new ResourceDictionary and set source for language matching the selected menu option
			var dict = new ResourceDictionary();
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
				default:
					throw new ArgumentException("Invalid Menu Option selected");
			}
			_window.Resources.MergedDictionaries.Add(dict);
			_viewModel.ResourceDictionary = dict;
			_viewModel.Initialize();
			_window.DataContext = _viewModel;

			// Set window size
			_window.MinHeight = _window.Height = 512;
			_window.MinWidth = _window.Width = 576;

			// Increase size for Connector dialogs
			if (_window is ConnectorDialog)
			{
				_window.MinHeight = _window.Height = 512;
				_window.MinWidth = _window.Width = 768;
			}

            _window.Closing += _viewModel.OnWindowClosing;

            _window.ShowDialog();
		}

		/// <summary>
		/// The EA_Disconnect event enables the Add-In to respond to user requests to disconnect the model branch from an external project.
		/// This function is called when the Enterprise Architect closes. If you have stored references to Enterprise Architect objects (not particularly recommended anyway), you must release them here.
		/// In addition, .NET users must call the memory management functions <c>GC.Collect()</c> and <c>GC.WaitForPendingFinalizers()</c>.
		/// </summary>
		public void EA_Disconnect()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

        private const string MenuHeader = "-&Plusprofil Editing Window (WPF)";
		private const string EnglishMenuOption = "&Open English Editing Window";
		private const string DanishMenuOption = "&Open Danish Editing Window";
		private Window _window;
		private DialogViewModel _viewModel;
	}
}