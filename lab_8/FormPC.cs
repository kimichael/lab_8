using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace lab_8
{
    public partial class FormPC : Form
    {
        private XmlFileManager _fileManager = new XmlFileManager();
        private List<PCStruct> _originalList = new List<PCStruct>();
        private List<PCStruct> _filteredList = new List<PCStruct>();
        private Filter _filter = new Filter();

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
            var sortedIndices = listView.SelectedIndices.Cast<int>().ToList();
            sortedIndices.Sort();

            for (var i = sortedIndices.Count - 1; i >= 0; i--)
            {
                _originalList.Remove(_filteredList[sortedIndices[i]]);
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
                _originalList = _fileManager.Load(filename).ToList();
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

            try
            {
                _fileManager.Save(_originalList, filename);
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
            _filteredList = _filter.FilterItems(_originalList).ToList();
            _filteredList.ForEach(item => listView.Items.Add(item.GetListViewItem()));
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
            _filter = new Filter();
            UpdateListView();
        }
    }
}
