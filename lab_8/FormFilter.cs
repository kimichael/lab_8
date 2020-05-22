using System;
using System.Windows.Forms;

namespace lab_8
{
    public partial class FormFilter : Form
    {
        public FormFilter()
        {
            InitializeComponent();
            okButton.DialogResult = DialogResult.OK;
            cancelButton.DialogResult = DialogResult.Cancel;
        }

        public Filter GetFilter()
        {
            var filter = new Filter();
            AddPredicateBasedOnTextBox(filter, codeTextBox, Filter.ContainsPredicate, item => item.Code, val => val);
            AddPredicateBasedOnTextBox(filter, manufacturerTextBox, Filter.ContainsPredicate, item => item.Manufacturer, val => val);
            AddPredicateBasedOnTextBox(filter, procTextBox, Filter.ContainsPredicate, item => item.Proc, val => val);
            AddPredicateBasedOnTextBox(filter, freqMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.Freq, double.Parse);
            AddPredicateBasedOnTextBox(filter, freqMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.Freq, double.Parse);
            AddPredicateBasedOnTextBox(filter, memMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.Mem, double.Parse);
            AddPredicateBasedOnTextBox(filter, memMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.Mem, double.Parse);
            AddPredicateBasedOnTextBox(filter, hddMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.HDD, double.Parse);
            AddPredicateBasedOnTextBox(filter, hddMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.HDD, double.Parse);
            AddPredicateBasedOnTextBox(filter, videoMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.Video, double.Parse);
            AddPredicateBasedOnTextBox(filter, videoMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.Video, double.Parse);
            AddPredicateBasedOnTextBox(filter, priceMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.Price, int.Parse);
            AddPredicateBasedOnTextBox(filter, priceMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.Price, int.Parse);
            AddPredicateBasedOnTextBox(filter, countMinTextBox, Filter.GreaterThanOrEqualToPredicate, item => item.Count, int.Parse);
            AddPredicateBasedOnTextBox(filter, countMaxTextBox, Filter.LessThanOrEqualToPredicate, item => item.Count, int.Parse);
            return filter;
        }

        private void AddPredicateBasedOnTextBox<T>(Filter filter, TextBox targetTextBox, 
            Func<Func<PCStruct, T>, T, Func<PCStruct, bool>> predicateFactory, Func<PCStruct, T> pcValueGetter, 
            Func<string, T> valueConverter) 
        {
            if (targetTextBox.Text.Length == 0) return;
            filter.AddPredicate(predicateFactory, pcValueGetter, valueConverter(targetTextBox.Text));
        }
    }
}
