using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EA;
using System.IO;

namespace EACustomWindow
{
    public partial class EAUserControl : UserControl
	{
		public EAUserControl()
		{
			InitializeComponent();
			//Dock = DockStyle.Fill;
            AutoSize = true;
			uriLabel.Dock = DockStyle.Fill;
			uriTextBox.Dock = DockStyle.Fill;
			topTableLayoutPanel.Dock = DockStyle.Fill;
            topTableLayoutPanel.AutoSize = true;
			masterTableLayoutPanel.Dock = DockStyle.Fill;
			langTabControl.Dock = DockStyle.Fill;
			danishTableLayoutPanel.Dock = DockStyle.Fill;
			danishTableLayoutPanel.AutoSize = true;
			danishTableLayoutPanel.Padding = new Padding(5);
			englishTableLayoutPanel.Dock = DockStyle.Fill;
			englishTableLayoutPanel.AutoSize = true;
			englishTableLayoutPanel.Padding = new Padding(5);
			stereotypeTableLayoutPanel.Dock = DockStyle.Fill;
			stereotypeTableLayoutPanel.AutoSize = true;
            stereotypeTableLayoutPanel.Padding = new Padding(5);
		}

		/*
		 * Function for filling the user control with package-related UI elements.
		 * Hides the top panel (as packages have no "URI"-tag), then adds a header (label with Tagged Value name and "+"-button) and textboxes for relevant Tagged Values
		 */
		public void populateUserControlPackage(Repository repository) {
            //TODO: Remove relevant elements from topTableLayoutPanel
            this.topTableLayoutPanel.Hide();

			Package selectedObject = repository.GetContextObject();
			Collection taggedValues = selectedObject.Element.TaggedValues;
			string stereotype = selectedObject.Element.Stereotype;

			addTaggedValue("label (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("comment (da)", taggedValues, danishTableLayoutPanel);

			addTaggedValue("label (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("comment (en)", taggedValues, englishTableLayoutPanel);

			addTaggedValue("approvalStatus", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("modelStatus", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("modified", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("namespace", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("namespacePrefix", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("publisher", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("source", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("theme", taggedValues, stereotypeTableLayoutPanel);
			addTaggedValue("versionInfo", taggedValues, stereotypeTableLayoutPanel);

			if (stereotype == "Vocabulary")
			{
				addTaggedValue("wasDerivedFrom", taggedValues, stereotypeTableLayoutPanel);
			}
		}

		//Function for filling the user control with element-related UI elements.
		public void populateUserControlElement(EA.Repository repository)
		{
			Element selectedObject = repository.GetContextObject();
            Collection taggedValues = selectedObject.TaggedValues;
            string stereotype = selectedObject.Stereotype;

            //Set topTableLayoutPanel TextBox values
            dynamic uriTaggedValue = retrieveTaggedValue("URI", taggedValues).First();
            uriTextBox.Text = uriTaggedValue.Value;
            umlTextBox.Text = selectedObject.Name;
            aliasTextBox.Text = selectedObject.Alias;
            uriTextBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, uriTaggedValue, false);
            umlTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, selectedObject, "Name");
            aliasTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, selectedObject, "Alias");

            addTaggedValue("prefLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("altLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("definition (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("comment (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("example (da)", taggedValues, danishTableLayoutPanel);

			addTaggedValue("prefLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("altLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("definition (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("comment (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("example (en)", taggedValues, englishTableLayoutPanel);

			if (stereotype == "OwlClass" || stereotype == "RdfsClass")
			{
				addTaggedValue("equivalentClass", taggedValues, stereotypeTableLayoutPanel);
				addTaggedValue("subClassOf", taggedValues, stereotypeTableLayoutPanel);
			}

			if (stereotype == "Individual")
			{
				addTaggedValue("sameAs", taggedValues, stereotypeTableLayoutPanel);
				addTaggedValue("type", taggedValues, stereotypeTableLayoutPanel);
			}
		}

		/*
		 * Function for filling the user control with attribute-related UI elements.
		 * Be aware that Connectors do not use class TaggedValue, but class AttributeTag.
		 * The two are identical except for the name of ID properties, and thus using a dynamic type allows a function to use either type
		 */
		public void populateUserControlAttribute(EA.Repository repository)
		{
			EA.Attribute selectedObject = repository.GetContextObject();
			Collection taggedValues = selectedObject.TaggedValues;
			string stereotype = selectedObject.Stereotype;

            //Set topTableLayoutPanel TextBox values
            dynamic uriTaggedValue = retrieveTaggedValue("URI", taggedValues).First();
            uriTextBox.Text = uriTaggedValue.Value;
            umlTextBox.Text = selectedObject.Name;
            aliasTextBox.Text = selectedObject.Alias;
            uriTextBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, uriTaggedValue, false);
            umlTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, selectedObject, "Name");
            aliasTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, selectedObject, "Alias");

            //Add datatype field
            topTableLayoutPanel.RowCount = topTableLayoutPanel.RowCount + 1;
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            TextBox datatypeTextBox = new TextBox();
            Label datatypeLabel = new Label();

            datatypeLabel.Text = "Type";
            datatypeLabel.Dock = DockStyle.Fill;

            datatypeTextBox.Text = selectedObject.Type;
            datatypeTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, selectedObject, "Type");
            datatypeTextBox.Dock = DockStyle.Fill;

            topTableLayoutPanel.Controls.Add(datatypeLabel);
            topTableLayoutPanel.Controls.Add(datatypeTextBox);

            addTaggedValue("prefLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("altLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("definition (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("comment (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("example (da)", taggedValues, danishTableLayoutPanel);

			addTaggedValue("prefLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("altLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("definition (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("comment (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("example (en)", taggedValues, englishTableLayoutPanel);

			if (stereotype == "RdfsProperty" || stereotype == "DatatypeProperty")
			{
				addTaggedValue("range", taggedValues, stereotypeTableLayoutPanel);
				addTaggedValue("domain", taggedValues, stereotypeTableLayoutPanel);
				addTaggedValue("subPropertyOf", taggedValues, stereotypeTableLayoutPanel);
				addTaggedValue("equivalentProperty", taggedValues, stereotypeTableLayoutPanel);
			}
			if(stereotype == "DatatypeProperty") {
				addTaggedValue("functionalProperty", taggedValues, stereotypeTableLayoutPanel);
			}
			
		}

        //Function for filling the user control with connector end-related UI elements.
        //Not implemented. Be aware that ConnectorEnds do not use class TaggedValue, but class RoleTag
        //RoleTags do not have a Notes property, and their name are stored in property "Tag" rather than "Name"
        public void populateUserControlConnectorEnd(EA.Repository repository, bool isClientEnd)
		{
			Connector selectedObject = repository.GetContextObject();
			ConnectorEnd connectorEnd = (isClientEnd) ? selectedObject.ClientEnd : selectedObject.SupplierEnd;
            //If no ConnectorEnd exists, hide associated control
            //TODO: Allow adding of new ConnectorEnd from this user control
            if (connectorEnd == null)
            {
                Hide();
                return;
            }

			Collection taggedValues = connectorEnd.TaggedValues;
			string stereotype = connectorEnd.Stereotype;

            //Add ConnectorEnd role label (Source / Target)
            Label roleLabel = new Label();
            roleLabel.Dock = DockStyle.Fill;
            if (isClientEnd) roleLabel.Text = "TARGET: " + repository.GetElementByID(selectedObject.ClientID).Name;
            else roleLabel.Text = "SOURCE: " + repository.GetElementByID(selectedObject.SupplierID).Name;
            roleLabel.TextAlign = ContentAlignment.MiddleLeft;
            roleLabel.Margin = new Padding(0, 0, 0, 15);

            topTableLayoutPanel.RowCount++;
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            topTableLayoutPanel.Controls.Add(roleLabel);
            topTableLayoutPanel.SetColumnSpan(roleLabel, 2);
            topTableLayoutPanel.Controls.SetChildIndex(roleLabel, 0);

            //Set topTableLayoutPanel TextBox values
            //dynamic uriTaggedValue = retrieveTaggedValue("URI", taggedValues).First();
            //uriTextBox.Text = uriTaggedValue.Value;
            umlTextBox.Text = connectorEnd.Role;
            aliasTextBox.Text = connectorEnd.Alias;
            //uriTextBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, uriTaggedValue, false);
            umlTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, connectorEnd, "Role");
            aliasTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, connectorEnd, "Alias");

            //Add multiplicity field
            topTableLayoutPanel.RowCount = topTableLayoutPanel.RowCount + 1;
            topTableLayoutPanel.RowStyles.Add(new RowStyle());
            TextBox multiplicityTextBox = new TextBox();
            Label multiplicityLabel = new Label();

            multiplicityLabel.Text = "Multiplicity";
            multiplicityLabel.Dock = DockStyle.Fill;

            multiplicityTextBox.Text = connectorEnd.Cardinality;
            multiplicityTextBox.TextChanged += (sender, e) => StringTextBox_TextChanged(sender, e, connectorEnd, "Cardinality");
            multiplicityTextBox.Dock = DockStyle.Fill;

            topTableLayoutPanel.Controls.Add(multiplicityLabel);
            topTableLayoutPanel.Controls.Add(multiplicityTextBox);


            addTaggedValue("prefLabel (da)", taggedValues, danishTableLayoutPanel);
            addTaggedValue("altLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("definition (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("comment (da)", taggedValues, danishTableLayoutPanel);
			addTaggedValue("example (da)", taggedValues, danishTableLayoutPanel);

			addTaggedValue("prefLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("altLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("deprecatedLabel (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("definition (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("comment (en)", taggedValues, englishTableLayoutPanel);
			addTaggedValue("example (en)", taggedValues, englishTableLayoutPanel);

            if(stereotype == "RdfsProperty" || stereotype == "ObjectProperty")
            {
                addTaggedValue("range", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("domain", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("subPropertyOf", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("equivalentProperty", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("applicationNote (da)", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("applicationNote (en)", taggedValues, stereotypeTableLayoutPanel);
            }

            if(stereotype == "ObjectProperty")
            {
                addTaggedValue("inverseOf", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("functionalProperty", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("inverseFunctionalProperty", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("transitiveProperty", taggedValues, stereotypeTableLayoutPanel);
                addTaggedValue("symmetricProperty", taggedValues, stereotypeTableLayoutPanel);
            }
            
		}
		/*
		 * Function for adding UI elements for a Tagged Value
		 * Finds every Tagged Value in a Collection, then adds a header with Tagged Value name
		 * and a textbox for each existing Tagged Value to a TableLayoutPanel
		 */
		private void addTaggedValue(string taggedValueName, Collection taggedValues, TableLayoutPanel tableLayoutPanel)
		{
			//Find all Tagged Values with passed string Tagged Value name
			//If none exists, catch exception and display error
			List<dynamic> toAddTaggedValues = null; ;
			try
			{
				toAddTaggedValues = retrieveTaggedValue(taggedValueName, taggedValues);
			}
			catch (ValueNotFoundException)
			{
				//MessageBox.Show("TaggedValue \"" + taggedValueName + "\" could not be found");
				return;
			}

			//Add another row to hold Tagged Value label and button
			tableLayoutPanel.RowCount++;
			tableLayoutPanel.RowStyles.Add(new RowStyle());

			//Label for displaying Tagged Value name
			Label label = new Label();
			label.Text = taggedValueName;
			label.AutoSize = false;
			label.TextAlign = ContentAlignment.MiddleLeft;
			label.Dock = DockStyle.Fill;
			label.Margin = new Padding(0);

			//Button for adding another Tagged Value
			Button newButton = new Button();
			newButton.Text = "+";
			newButton.TextAlign = ContentAlignment.MiddleCenter;
			newButton.Anchor = AnchorStyles.Right;
			newButton.Size = new Size(24, 24);
			newButton.Click += (sender, e) => NewButton_Click(sender, e, taggedValueName, taggedValues, tableLayoutPanel);
			newButton.Margin = new Padding(5,0,0,0);
            newButton.Dock = DockStyle.Fill;

			//Panel for holding label and button
			TableLayoutPanel labelButtonPanel = new TableLayoutPanel();
			labelButtonPanel.ColumnCount = 2;
			labelButtonPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			labelButtonPanel.ColumnStyles.Add(new ColumnStyle());
			labelButtonPanel.RowCount = 1;
			labelButtonPanel.RowStyles.Add(new RowStyle());
			labelButtonPanel.Dock = DockStyle.Fill;
			labelButtonPanel.Height = 25;
			labelButtonPanel.Margin = new Padding(0);
			labelButtonPanel.Padding = new Padding(0);

			labelButtonPanel.Controls.Add(label);
			labelButtonPanel.Controls.Add(newButton);
			tableLayoutPanel.Controls.Add(labelButtonPanel);

			//Add a panel with a textbox and remove button for each found Tagged Value with passed string taggedValueName
			foreach (dynamic taggedValue in toAddTaggedValues)
			{
				tableLayoutPanel.RowCount++;
				tableLayoutPanel.RowStyles.Add(new RowStyle());

				TableLayoutPanel newTableLayoutPanel = new TableLayoutPanel();
				newTableLayoutPanel.Dock = DockStyle.Fill;
				newTableLayoutPanel.AutoSize = true;
				newTableLayoutPanel.ColumnCount = 2;
				newTableLayoutPanel.RowCount = 1;
				newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
				newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
				newTableLayoutPanel.RowStyles.Add(new RowStyle());
				newTableLayoutPanel.Height = 25;
				newTableLayoutPanel.Margin = new Padding(0);
				newTableLayoutPanel.Padding= new Padding(0);

				TextBox textBox = new TextBox();
				if (taggedValueName == "comment (da)" || taggedValueName == "comment (en)" || taggedValueName == "definition (da)" || taggedValueName == "definition (en)")
				{
                    if (taggedValue.ObjectType == (int) ObjectType.otRoleTag) textBox.Text = taggedValue.Value;
                    else {
                        textBox.Text = taggedValue.Notes;
                        textBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, taggedValue, true);
                    }
				}
				else {
					textBox.Text = taggedValue.Value;
					textBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, taggedValue, false);
				}

				textBox.Dock = DockStyle.Fill;
				textBox.Margin = new Padding(0);
				textBox.DoubleClick += (sender, e) => TextBox_DoubleClick(sender, e, taggedValue, false);

				Button removeButton = new Button();
                removeButton.Image = Properties.Resources.rubbish_bin16;
                removeButton.ImageAlign = ContentAlignment.MiddleCenter;
				removeButton.Anchor = AnchorStyles.Right;
				removeButton.Size = new Size(24, 24);
				removeButton.Click += (sender, e) => RemoveButton_Click(sender, e, newTableLayoutPanel, taggedValues, taggedValue);
				removeButton.Margin = new Padding(5,0,0,0);
                removeButton.Dock = DockStyle.Fill;

				newTableLayoutPanel.Controls.Add(textBox, 0, 0);
				newTableLayoutPanel.Controls.Add(removeButton, 1, 0);
				tableLayoutPanel.Controls.Add(newTableLayoutPanel);
			}
		}
		/* 
		 * Function for retrieving a tagged value with specific name from a collection.
		 * Iterates over the collection, checking if each Tagged Value has the same name as
		 * passed string taggedValueName. If so, add it to the list of results, which is returned.
		 */
		private List<dynamic> retrieveTaggedValue(string argTaggedValueName, Collection taggedValues)
		{
			List<dynamic> resultTaggedValues = new List<dynamic>();
			dynamic taggedValue = null;
            string taggedValueName;

			for (int i = 0; i < taggedValues.Count; i++)
			{
				taggedValue = taggedValues.GetAt((short)i);
                //Check for RoleTag type
                if (taggedValue.ObjectType == (int) EA.ObjectType.otRoleTag) taggedValueName = taggedValue.Tag;
                else taggedValueName = taggedValue.name;

                if (taggedValueName == argTaggedValueName) resultTaggedValues.Add(taggedValue);
			}
			//If no results are found, throw ValueNotFoundException
			if (resultTaggedValues.Count == 0) throw new ValueNotFoundException();

            return resultTaggedValues;			
		}
		/*
		 * Event handler for double clicking textbox.
		 * Creates a new, resizable form with a RichTextBox, allowing better view of contents.
		 * TextBox is updated when form is closed.
		 * Still in development. May need buttons for saving and cancelling, and changing of properties of RichTextBox.
		 */
		private void TextBox_DoubleClick(object senderArg, EventArgs eArg, dynamic taggedValue, bool setNotes)
		{
			Form form = new Form();
			form.Size = new Size(600, 400);
			form.Padding = new Padding(10);
			form.Text = "Edit TaggedValue";
			TextBox textBox = senderArg as TextBox;
			RichTextBox richTextBox = new RichTextBox();
			richTextBox.Text = textBox.Text;
			richTextBox.Dock = DockStyle.Fill;
			richTextBox.TextChanged += (sender, e) => TaggedValueRichTextBox_TextChanged(sender, e, taggedValue, setNotes);
			form.Controls.Add(richTextBox);
			form.FormClosing += (sender, e) => Form_FormClosing(sender, e, richTextBox, textBox);
			form.Show();
		}
		//Event handler that updates contents of TextBox with contents of RichTextBox
		private void Form_FormClosing(object sender, FormClosingEventArgs e, RichTextBox richTextBox, TextBox textBox)
		{
			textBox.Text = richTextBox.Text;
		}

		//Event handlers for updating TaggedValue.Value or TaggedValue.Notes to match contents of TextBox / 

		private void TaggedValueTextBox_TextChanged(object sender, EventArgs e, dynamic taggedValue, bool setNotes)
		{
			TextBox textBox = sender as TextBox;
			if (setNotes)
			{
                if (taggedValue.ObjectType == (int)ObjectType.otRoleTag) taggedValue.Value = textBox.Text;
                else {
                    taggedValue.Notes = textBox.Text;
                    taggedValue.Update();
                }
			}
			else {
				taggedValue.Value = textBox.Text;
				taggedValue.Update();
			}
		}

		private void TaggedValueRichTextBox_TextChanged(object sender, EventArgs e, dynamic taggedValue, bool setNotes)
		{
			RichTextBox textBox = sender as RichTextBox;

            if (setNotes)
            {
                if (taggedValue.ObjectType == (int)ObjectType.otRoleTag) taggedValue.Value = textBox.Text;
                else
                {
                    taggedValue.Notes = textBox.Text;
                    taggedValue.Update();
                }
            }
            else {
				taggedValue.Value = textBox.Text;
				taggedValue.Update();
			}
		}

        private void StringTextBox_TextChanged(object sender, EventArgs e, dynamic selectedObject, string changeValue)
        {
            TextBox textBox = sender as TextBox;
            if (changeValue == "Name")
            {
                selectedObject.Name = textBox.Text;
                //Change Form text
                dynamic parent = textBox.Parent;
                while (parent.Parent != null)
                {
                    parent = parent.Parent;
                }
                parent.Text = textBox.Text;
            }
            else if (changeValue == "Role")
            {
                selectedObject.Role = textBox.Text;
            }
            else if (changeValue == "Alias") selectedObject.Alias = textBox.Text;
            else if (changeValue == "Cardinality") selectedObject.Cardinality = textBox.Text;
            else if (changeValue == "Type") selectedObject.Type = textBox.Text;
            selectedObject.Update();
        }

        //Event handler for "+"-button click, which adds new TextBox for relevant TaggedValue.
        private void NewButton_Click(object buttonArg, EventArgs eventArgs, string taggedValueName, Collection taggedValues, TableLayoutPanel tableLayoutPanel)
		{
			Button button = buttonArg as Button;
			//Add new TaggedValue / AttributeTag to Collection of TaggedValue/AttributeTag
			dynamic taggedValue = taggedValues.AddNew(taggedValueName, "");
			taggedValue.Update();
			taggedValues.Refresh();

			//Add UI element
			tableLayoutPanel.RowCount++;
			tableLayoutPanel.RowStyles.Add(new RowStyle());

			TableLayoutPanel newTableLayoutPanel = new TableLayoutPanel();
			newTableLayoutPanel.Dock = DockStyle.Fill;
			newTableLayoutPanel.AutoSize = true;
			newTableLayoutPanel.ColumnCount = 2;
			newTableLayoutPanel.RowCount = 1;
			newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			newTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			newTableLayoutPanel.RowStyles.Add(new RowStyle());
			newTableLayoutPanel.Height = 25;
			newTableLayoutPanel.Margin = new Padding(0);
			newTableLayoutPanel.Padding = new Padding(0);

			TextBox textBox = new TextBox();
			textBox.Margin = new Padding(0);
			if (taggedValueName == "comment (da)" || taggedValueName == "comment (en)" || taggedValueName == "definition (da)" || taggedValueName == "definition (en)")
			{
                if (taggedValue.ObjectType == (int)ObjectType.otRoleTag) textBox.Text = taggedValue.Value;
                else
                {
                    textBox.Text = taggedValue.Notes;
                    textBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, taggedValue, true);
                }			}
			else {
				textBox.Text = taggedValue.Value;
				textBox.TextChanged += (sender, e) => TaggedValueTextBox_TextChanged(sender, e, taggedValue, false);
			}

			textBox.Dock = DockStyle.Fill;
			textBox.DoubleClick += (sender, e) => TextBox_DoubleClick(sender, e, taggedValue, false);

			Button removeButton = new Button();
            removeButton.Image = Properties.Resources.rubbish_bin16;
            removeButton.ImageAlign = ContentAlignment.MiddleCenter;
            removeButton.Anchor = AnchorStyles.Right;
			removeButton.Size = new Size(24, 24);
			removeButton.Click += (sender, e) => RemoveButton_Click(sender, e, newTableLayoutPanel, taggedValues, taggedValue);
			removeButton.Margin = new Padding(5,0,0,0);
            removeButton.Dock = DockStyle.Fill;

			newTableLayoutPanel.Controls.Add(textBox, 0, 0);
			newTableLayoutPanel.Controls.Add(removeButton, 1, 0);

			tableLayoutPanel.Controls.Add(newTableLayoutPanel);

            //Sets index of controls to first index after parent of NewButton
            //TODO: Set index to last of that type of TaggedValue
			int index = tableLayoutPanel.Controls.GetChildIndex(button.Parent);
			tableLayoutPanel.Controls.SetChildIndex(newTableLayoutPanel, index + 1);
		}

		private void RemoveButton_Click(object sender, EventArgs e, TableLayoutPanel tableLayoutPanel, Collection taggedValues, dynamic taggedValue)
		{
            string taggedValueName;
            if (taggedValue.ObjectType == (int) ObjectType.otRoleTag) taggedValueName = taggedValue.Tag;
            else taggedValueName = taggedValue.Name;
            DialogResult result = MessageBox.Show("Delete Tagged Value " + taggedValueName + "?", "Delete Tagged Value", MessageBoxButtons.OKCancel);
			if (result == DialogResult.Cancel) return;

			tableLayoutPanel.Parent.Controls.Remove(tableLayoutPanel);
			if (taggedValue.ObjectType == (int) ObjectType.otAttributeTag) {
				for (int i = 0; i < taggedValues.Count; i++) {
					if (taggedValue.TagGUID == taggedValues.GetAt((short)i).TagGUID) {
						taggedValues.Delete((short)i);
						break;
					}
				}
			}
			else {
				for (int i = 0; i < taggedValues.Count; i++) {
					if (taggedValue.PropertyGUID == taggedValues.GetAt((short)i).PropertyGUID) {
						taggedValues.Delete((short)i);
						break;
					}
				}
			}
		}
    }
}
