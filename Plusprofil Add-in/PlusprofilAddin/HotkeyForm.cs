using System;
using System.Diagnostics;
using System.Windows.Forms;
using EA;

namespace PlusprofilAddin
{
	///<summary>
	/// <c>Form</c> used to register hotkeys.<para/>
	/// Original implementation by Christian Liensberger, shared by AaronLS on StackOverflow - https://stackoverflow.com/a/27309185/9105071
	/// </summary>
	/// <inheritdoc/>
	public partial class HotkeyForm : Form
	{
		private readonly MainClass _mainClass;
        private readonly string _danishMenuOption;
        private readonly string _englishMenuOption;
		private readonly Repository _repository;

		/// <param name="mainClass">The <c>MainClass</c> registering the hotkey. Used to allow <c>EA_MenuClick()</c> to be called from the <c>HotkeyForm</c>.</param>
		/// <param name="repository">The <c>EA.Repository</c> passed from the Enterprise Architect Automation Interface to the add-in. Used as a parameter in <c>MainClass</c> functions.</param>
		/// <param name="danishMenuOption">String used to indicate a Window with Danish strings should be shown when calling <c>EA_MenuClick()</c>.</param>
		/// <param name="englishMenuOption">String used to indicate a Window with English strings should be shown when calling <c>EA_MenuClick()</c>.</param>
		/// <inheritdoc/>
		/// TODO: Consider refactoring the menuOptions to a more scalable approach, e.g. using params string[] menuOptions.
		public HotkeyForm(MainClass mainClass, Repository repository, string danishMenuOption, string englishMenuOption)
		{
			var keyboardHook = new KeyboardHook();
			
			_mainClass = mainClass;
			_repository = repository;
            _danishMenuOption = danishMenuOption;
            _englishMenuOption = englishMenuOption;

            InitializeComponent();

			// Add event handler for keypress
			keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
			try
			{
				// Register CTRL + Q and CTRL + R as hotkeys
				keyboardHook.RegisterHotKey(PlusprofilAddin.ModifierKeys.Control, Keys.Q);
				keyboardHook.RegisterHotKey(PlusprofilAddin.ModifierKeys.Control, Keys.R);
            }
			catch (InvalidOperationException)
			{
				// Error occured when attempting to register hotkeys. Do not register hotkeys, but warn the user
				// If exception is not caught, Enterprise Architect simply does not load the add-in without warning the user.
				MessageBox.Show("Exception occurred while trying to register hotkeys for Plusprofil Add-in.\nEnsure that no other instances of Sparx Enterprise Architect are running.", "Plusprofil Add-in");
			}
		}

		/// <summary>
		/// Event handler used when a hotkey keypress is registered by the <c>KeyboardHook</c>.<para/>
		/// Used to register functionality to the combinations of keys and modifier keys.
		/// </summary>
		/// <param name="sender">The source of the event.<para/>Not used.</param>
		/// <param name="e"><c>KeyPressedEventArgs</c> containing the event data (keys pressed).</param>
		private void KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
		{
			switch (e.Key)
			{
                case Keys.Q when e.Modifier == PlusprofilAddin.ModifierKeys.Control:
					_mainClass.EA_MenuClick(_repository, "", "", _englishMenuOption);
					break;
				case Keys.R when e.Modifier == PlusprofilAddin.ModifierKeys.Control:
					_mainClass.EA_MenuClick(_repository, "", "", _danishMenuOption);
					break;
			}
		}
    }
}
