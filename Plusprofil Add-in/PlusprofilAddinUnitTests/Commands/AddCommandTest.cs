using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlusprofilAddin.Commands;

namespace PlusprofilAddinUnitTests.Commands
{
	[TestClass]
	public class AddCommandTest
	{
		private Window _window;
		private AddCommand _addCommand;
		
		[TestInitialize]
		public void Setup()
		{
			_window = new Window();
			// TODO: Set window dictionary
			_addCommand = new AddCommand();
		}

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
