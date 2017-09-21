using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EA;

namespace EACustomWindow
{
	public partial class ConnectorUserControl : UserControl
	{
		EAUserControl clientControl;
		EAUserControl supplierControl;
		public ConnectorUserControl()
		{
			/*
			 * Creates a new ConnectorUserControl, a SplitContainer with an EAUserControl on each side for each ConnectorEnd
			 * Functionality for connectors should be added to EAUserControl
			 * Be aware that Connectors do not use type TaggedValue but RoleTag, which do not have property Notes or Name.
			 * Functionality for connectors will either require new functions and event handlers for specifically Connectors,
			 * or a number of conditional statements to check if object is a Connector, e.g.
			 * if(taggedValue.ObjectType == EA.ObjectType.otConnector) { foo(); }
			 * "<memo>" type is still supported for Connectors, but as no RoleTag.Notes property exists, information is stored in RoleTag.Value instead.
			 */
			clientControl = new EAUserControl();
			supplierControl = new EAUserControl();
			clientControl.Dock = DockStyle.Fill;
			supplierControl.Dock = DockStyle.Fill;

			InitializeComponent();
			splitContainer.Panel1.Controls.Add(clientControl);
			splitContainer.Panel2.Controls.Add(supplierControl);
		}
		public void populateUserControlConnector(EA.Repository repository) {
			clientControl.populateUserControlConnectorEnd(repository, true);
			supplierControl.populateUserControlConnectorEnd(repository, false);
		}
	}
}
