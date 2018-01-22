using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlusprofilAddin.Commands;

namespace PlusprofilAddinUnitTests.Commands
{
	[TestClass]
	public class SaveCommandTest
	{
		private Window _window;
		private SaveCommand _saveCommand;
		
		[TestInitialize]
		public void Setup()
		{
			_window = new Window();
			// TODO: Set window dictionary
			_saveCommand = new SaveCommand();
		}

		[TestMethod]
		public void CanExecuteTest()
		{
			Assert.AreEqual(true, _saveCommand.CanExecute(null));
		}

		[TestMethod]
		public void ExecuteTest()
		{
			_saveCommand.Execute(_window);
		}
	}
}
