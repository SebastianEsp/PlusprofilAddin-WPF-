using System;
using System.Windows.Forms;
using EA;

namespace PlusprofilAddin
{
	public partial class InvisibleHotkeyForm : Form
	{
		private readonly MainClass _mainClass;
        private readonly string _danishMenuOption;
        private readonly string _englishMenuOption;
		private readonly Repository _repository;

		public InvisibleHotkeyForm(MainClass mainClass, Repository repository, string danishMenuOption, string englishMenuOption)
		{
			/* Creates a new form with a KeyboardHook for capturing keypresses.
			 * To add new shortcuts, register another hotkey using the function hook.RegisterHotKey(global::ModifierKeys modifier, Keys key).
			 * Then, add a conditional statement to the hook_KeyPressed event, e.g.
			 * if(e.Keys == Keys.W && e.Modifier == global::ModifierKeys.Control | global::ModifierKeys.Shift) { foo(); }
			 * to call function foo(); when CTRL + SHIFT + W is pressed
			 */
			KeyboardHook keyboardHook = new KeyboardHook();

			_mainClass = mainClass;
			_repository = repository;
            _danishMenuOption = danishMenuOption;
            _englishMenuOption = englishMenuOption;

            InitializeComponent();

			//Add event handler for keypress
			keyboardHook.KeyPressed += KeyboardHook_KeyPressed;
			try
			{
				//Register CTRL + Q and CTRL + Ras hotkeys
				keyboardHook.RegisterHotKey(PlusprofilAddin.ModifierKeys.Control, Keys.Q);
				keyboardHook.RegisterHotKey(PlusprofilAddin.ModifierKeys.Control, Keys.R);
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Exception occurred while trying to register hotkeys for Plusprofil Add-in.\nEnsure that no other instances of Sparx Enterprise Architect are running.", "Plusprofil Add-in");
			}
		}

		private void KeyboardHook_KeyPressed(object sender, KeyPressedEventArgs e)
		{
			switch (e.Key)
			{
				case Keys.Q:
					_mainClass.EA_MenuClick(_repository, "", "", _englishMenuOption);
					break;
				case Keys.R:
					_mainClass.EA_MenuClick(_repository, "", "", _danishMenuOption);
					break;
			}
		}
    }
}
