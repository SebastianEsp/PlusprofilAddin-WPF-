using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlusprofilAddin
{
	/// <summary>
	/// Class used for registering hotkeys with Windows.<para/>
	/// Original implementation by Christian Liensberger, shared by AaronLS on StackOverflow - https://stackoverflow.com/a/27309185/9105071
	/// </summary>
	public sealed class KeyboardHook : IDisposable
	{
		// Registers a hot key with Windows.
		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
		// Unregisters the hot key with Windows.
		[DllImport("user32.dll")]
		private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

		/// <summary>
		/// Represents the window that is used internally to get the messages.
		/// </summary>
		private sealed class Window : NativeWindow, IDisposable
		{
			private const int WmHotkey = 0x0312;

			public Window()
			{
				// Create the handle for the window.
				CreateHandle(new CreateParams());
			}

			/// <summary>
			/// Overridden to get the notifications.
			/// </summary>
			/// <param name="m"></param>
			protected override void WndProc(ref Message m)
			{
				base.WndProc(ref m);

				// Check if we got a hot key pressed.
				if (m.Msg != WmHotkey) return;
				// Get the keys.
				var key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
				var modifier = (ModifierKeys)((int)m.LParam & 0xFFFF);

				// Invoke the event to notify the parent.
				KeyPressed?.Invoke(this, new KeyPressedEventArgs(modifier, key));
			}

			public event EventHandler<KeyPressedEventArgs> KeyPressed;

			#region IDisposable Members

			public void Dispose()
			{
				DestroyHandle();
			}

			#endregion
		}

		private readonly Window _window = new Window();
		private int _currentId;

		/// <inheritdoc />
		public KeyboardHook()
		{
			// Register the event of the inner native window.
			_window.KeyPressed += delegate (object sender, KeyPressedEventArgs args)
			{
				KeyPressed?.Invoke(this, args);
			};
		}

		/// <summary>
		/// Registers a hot key in the system.
		/// </summary>
		/// <param name="modifier">The modifiers that are associated with the hot key.</param>
		/// <param name="key">The key itself that is associated with the hot key.</param>
		public void RegisterHotKey(ModifierKeys modifier, Keys key)
		{
			// Increment the counter.
			_currentId = _currentId + 1;

			// Register the hot key.
			if (!RegisterHotKey(_window.Handle, _currentId, (uint)modifier, (uint)key))
				throw new InvalidOperationException("Couldn’t register the hot key.");
		}

		/// <summary>
		/// A hot key has been pressed.
		/// </summary>
		public event EventHandler<KeyPressedEventArgs> KeyPressed;

		#region IDisposable Members

		/// <inheritdoc />
		public void Dispose()
		{
			// Unregister all the registered hot keys.
			for (var i = _currentId; i > 0; i--)
			{
				UnregisterHotKey(_window.Handle, i);
			}

			// Dispose the inner native window.
			_window.Dispose();
		}

		#endregion
	}

	/// <summary>
	/// Event Args for the event that is fired after the hot key has been pressed.
	/// </summary>
	public class KeyPressedEventArgs : EventArgs
	{
		internal KeyPressedEventArgs(ModifierKeys modifier, Keys key)
		{
			Modifier = modifier;
			Key = key;
		}

		/// <summary>
		/// 
		/// </summary>
		public ModifierKeys Modifier { get; }

		/// <summary>
		/// 
		/// </summary>
		public Keys Key { get; }
	}

	/// <summary>
	/// The enumeration of possible modifiers.
	/// </summary>
	[Flags]
	public enum ModifierKeys : uint
	{
		/// <summary>
		/// Alt-key
		/// </summary>
		Alt = 1,
		/// <summary>
		/// Control-key
		/// </summary>
		Control = 2,
		/// <summary>
		/// Shift-key
		/// </summary>
		Shift = 4,
		/// <summary>
		/// Windows-key
		/// </summary>
		Win = 8
	}
}