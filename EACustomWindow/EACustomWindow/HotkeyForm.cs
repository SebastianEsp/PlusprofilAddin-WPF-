using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;
using EACustomWindow;

namespace EACustomWindow
{
	public partial class HotkeyForm : Form
	{
		KeyboardHook hook;
		private MainClass mainClass;
		private string menuWindow;
		private Repository repository;

		public HotkeyForm(MainClass mainClass, Repository repository, string menuWindow)
		{
			/* Creates a new form with a KeyboardHook for capturing keypresses.
			 * To add new shortcuts, register another hotkey using the function hook.RegisterHotKey(global::ModifierKeys modifier, Keys key).
			 * Then, add a conditional statement to the hook_KeyPressed event, e.g.
			 * if(e.Keys == Keys.W && e.Modifier == global::ModifierKeys.Control | global::ModifierKeys.Shift) { foo(); }
			 * to call function foo(); when CTRL + SHIFT + W is pressed
			 */ 
			hook = new KeyboardHook();
			this.mainClass = mainClass;
			this.repository = repository;
			this.menuWindow = menuWindow;

			InitializeComponent();

			//Add event handler for keypress
			hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
			//Register CTRL + SHIFT + Q as hotkey 
			hook.RegisterHotKey(global::ModifierKeys.Control | global::ModifierKeys.Shift, Keys.Q);
		}

		void hook_KeyPressed(object sender, KeyPressedEventArgs e)
		{
			mainClass.EA_MenuClick(repository, "", "", menuWindow);
		}
	}
}
