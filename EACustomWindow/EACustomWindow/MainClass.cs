using System;
using System.Windows.Forms;
using EA;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EACustomWindow
{
	public class MainClass
	{
		const string menuHeader = "-&EACustomWindow";
		const string menuWindow = "&Open Window";

		Form mainForm;
		HotkeyForm hotkeyForm;
		EAUserControl userControl;

		public MainClass() {
			
		}

		public void EA_Connect(EA.Repository Repository) { 
			//Save repository reference for later use
			this.hotkeyForm = new HotkeyForm(this, Repository, menuWindow);
		}

		public object EA_GetMenuItems(EA.Repository Repository, string Location, string MenuName)
		{
			switch (MenuName)
			{
				case "":
					return menuHeader;
				case menuHeader:
					string[] subMenus = { menuWindow };
					return subMenus;
			}
			return "";
		}

		public void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
		{
			switch (ItemName)
			{
				case menuWindow:
					mainForm = new Form();
					mainForm.Size = new System.Drawing.Size(625, 800);
					mainForm.MinimumSize = new System.Drawing.Size(625, 800);
                    mainForm.AutoSize = true;

					dynamic selectedObject = Repository.GetContextObject();
					ObjectType selectedObjectType = (ObjectType) selectedObject.objecttype;

					switch (selectedObjectType)
					{
						#region Switch cases
						case ObjectType.otPackage:
							{
								mainForm.Text = selectedObject.Element.Name;
								userControl = new EAUserControl();
								mainForm.Controls.Add(userControl);
								userControl.populateUserControlPackage(Repository);
								break;
							}

						case ObjectType.otElement:
							{
								mainForm.Text = selectedObject.Name;
								userControl = new EAUserControl();
								mainForm.Controls.Add(userControl);
								userControl.populateUserControlElement(Repository);
								break;
							}

						case ObjectType.otAttribute:
							{
								mainForm.Text = selectedObject.Name;
								userControl = new EAUserControl();
								mainForm.Controls.Add(userControl);
								userControl.populateUserControlAttribute(Repository);
								break;
							}

						case ObjectType.otConnector:
							{
								Connector selectedConnector = Repository.GetContextObject();
								mainForm.Text = "Connector Properties";
								mainForm.Size = new System.Drawing.Size(1250, 800);
								mainForm.MinimumSize = new System.Drawing.Size(1250, 800);

								ConnectorUserControl connectorUserControl = new ConnectorUserControl();
								mainForm.Controls.Add(connectorUserControl);
                                connectorUserControl.populateUserControlConnector(Repository);
								break;
							}

						default:
							{
								//Invalid element type; do nothing
								MessageBox.Show("Invalid object type.\nAdd-in is only defined for packages, elements, attributes and connector ends");
                                mainForm = null;
                                break;
							}
						#endregion
					}
					if(mainForm != null) mainForm.Show();
					break;
			}
		}

		public void EA_Disconnect()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
	}
}