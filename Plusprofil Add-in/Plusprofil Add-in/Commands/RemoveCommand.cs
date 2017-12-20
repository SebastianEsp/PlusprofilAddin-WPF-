using System;
using System.Windows;
using System.Windows.Input;

namespace PlusprofilAddin.Commands
{
    class RemoveCommand : ICommand
	{
        public RemoveCommand()
        {

        }

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
            MessageBox.Show("In RemoveCommand.Execute(object parameter)");
            MessageBox.Show(parameter.ToString());
        }
	}
}
