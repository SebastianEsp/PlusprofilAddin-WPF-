using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlusprofilAddin.Commands;

namespace PlusprofilAddinUnitTests.Commands
{
	[TestClass]
	public class CancelCommandTest
	{
		private Window _window;
		private CancelCommand _cancelCommand;
		
		[TestInitialize]
		public void Setup()
		{
			_window = new Window();
			// TODO: Set window dictionary
			_cancelCommand = new CancelCommand();
		}

		[TestMethod]
		public void CanExecuteTest()
		{
			Assert.AreEqual(true, _cancelCommand.CanExecute(null));
		}

		[TestMethod]
		public void ExecuteTest()
		{
			_cancelCommand.Execute(_window);
		}
	}
}
