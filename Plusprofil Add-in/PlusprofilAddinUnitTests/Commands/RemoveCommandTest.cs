using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlusprofilAddin.Commands;

namespace PlusprofilAddinUnitTests.Commands
{
	[TestClass]
	public class RemoveCommandTest
	{
		private Window _window;
		private RemoveCommand _removeCommand;
		
		[TestInitialize]
		public void Setup()
		{
			_window = new Window();
			// TODO: Set window dictionary
			_removeCommand = new RemoveCommand();
		}

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
