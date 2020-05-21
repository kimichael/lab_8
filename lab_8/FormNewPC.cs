using System.Windows.Forms;

namespace lab_8
{
    public partial class FormNewPC : Form
    {
        public FormNewPC()
        {
            InitializeComponent();
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
        }

        public PCStruct GetPCStruct()
        {
            return new PCStruct()
            {
                Code = codeTextBox.Text,
                Manufacturer = manufacturerTextBox.Text,
                Proc = procTextBox.Text,
                Freq = double.Parse(freqTextBox.Text),
                Mem = double.Parse(memTextBox.Text),
                HDD = double.Parse(hddTextBox.Text),
                Video = double.Parse(videoTextBox.Text),
                Price = int.Parse(priceTextBox.Text),
                Count = int.Parse(countTextBox.Text)
            };
        }
    }
}
