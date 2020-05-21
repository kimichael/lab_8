using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace lab_8
{
    public partial class FormPC : Form
    {
        private List<PCStruct> _originalList = new List<PCStruct>();
        private Filter _filter = new Filter(new List<Func<PCStruct, bool>>());

        public FormPC()
        {
            InitializeComponent();
            UpdateListView();
            UpdateRemoveButtonEnabledStatus();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var newPCDialog = new FormNewPC();

            var dialogResult = newPCDialog.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;

            PCStruct newPC;
            try
            {
                newPC = newPCDialog.GetPCStruct();
            }
            catch
            {
                MessageBox.Show("Unable to create new PC entry.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _originalList.Add(newPC);
            UpdateListView();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            foreach (int index in listView.SelectedIndices)
            {
                _originalList.RemoveAt(index);
            }

            UpdateListView();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            var filterDialog = new FormFilter();

            var dialogResult = filterDialog.ShowDialog();
            if (dialogResult != DialogResult.OK) return;

            try
            {
                _filter = filterDialog.GetFilter();
            }
            catch
            {
                MessageBox.Show("Unable to create new filter.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateListView();
        }

        private void openMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            var filename = openFileDialog.FileName;
            try
            {
                var file = XElement.Load(filename);
                _originalList = file.Descendants("PC").Select(item => PCStruct.FromXElement(item)).ToList();
            } 
            catch
            {
                MessageBox.Show("Unable to open the file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            UpdateListView();
        }

        private void saveMenuItem_Click(object sender, EventArgs e)
        {
            var result = saveFileDialog.ShowDialog();
            if (result != DialogResult.OK) return;

            var filename = saveFileDialog.FileName;

            var file = new XElement("PCs");
            _originalList.ForEach(item => file.Add(item.GetXElement()));

            try
            {
                file.Save(filename);
            }
            catch
            {
                MessageBox.Show("Unable to save the file.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void UpdateListView()
        {
            listView.Items.Clear();
            _filter.FilterItems(_originalList).ForEach(item => listView.Items.Add(item.GetListViewItem()));
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            UpdateRemoveButtonEnabledStatus();
        }

        private void UpdateRemoveButtonEnabledStatus()
        {
            removeButton.Enabled = listView.SelectedItems.Count > 0;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            _filter = new Filter(new List<Func<PCStruct, bool>>());
            UpdateListView();
        }
    }
}
