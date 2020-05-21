using System;
using System.Collections.Generic;
using System.Net.Sockets;
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
            var predicates = new List<Func<PCStruct, bool>>();
            AddPredicateBasedOnTextBox(predicates, codeTextBox, ContainsPredicate, item => item.Code, val => val);
            AddPredicateBasedOnTextBox(predicates, manufacturerTextBox, ContainsPredicate, item => item.Manufacturer, val => val);
            AddPredicateBasedOnTextBox(predicates, procTextBox, ContainsPredicate, item => item.Proc, val => val);
            AddPredicateBasedOnTextBox(predicates, freqMinTextBox, GreaterThanOrEqualToPredicate, item => item.Freq, double.Parse);
            AddPredicateBasedOnTextBox(predicates, freqMaxTextBox, LessThanOrEqualToPredicate, item => item.Freq, double.Parse);
            AddPredicateBasedOnTextBox(predicates, memMinTextBox, GreaterThanOrEqualToPredicate, item => item.Mem, double.Parse);
            AddPredicateBasedOnTextBox(predicates, memMaxTextBox, LessThanOrEqualToPredicate, item => item.Mem, double.Parse);
            AddPredicateBasedOnTextBox(predicates, hddMinTextBox, GreaterThanOrEqualToPredicate, item => item.HDD, double.Parse);
            AddPredicateBasedOnTextBox(predicates, hddMaxTextBox, LessThanOrEqualToPredicate, item => item.HDD, double.Parse);
            AddPredicateBasedOnTextBox(predicates, videoMinTextBox, GreaterThanOrEqualToPredicate, item => item.Video, double.Parse);
            AddPredicateBasedOnTextBox(predicates, videoMaxTextBox, LessThanOrEqualToPredicate, item => item.Video, double.Parse);
            AddPredicateBasedOnTextBox(predicates, priceMinTextBox, GreaterThanOrEqualToPredicate, item => item.Price, int.Parse);
            AddPredicateBasedOnTextBox(predicates, priceMaxTextBox, LessThanOrEqualToPredicate, item => item.Price, int.Parse);
            AddPredicateBasedOnTextBox(predicates, countMinTextBox, GreaterThanOrEqualToPredicate, item => item.Count, int.Parse);
            AddPredicateBasedOnTextBox(predicates, countMaxTextBox, LessThanOrEqualToPredicate, item => item.Count, int.Parse);
            return new Filter(predicates);
        }

        private Func<PCStruct, bool> ContainsPredicate(Func<PCStruct, string> valueGetter, string targetValue)
        {
            return item => valueGetter(item).Contains(targetValue);
        }

        private Func<PCStruct, bool> GreaterThanOrEqualToPredicate<T>(Func<PCStruct, T> valueGetter, 
            T targetValue) where T: IComparable 
        {
            return item => valueGetter(item).CompareTo(targetValue) != -1;
        }

        private Func<PCStruct, bool> LessThanOrEqualToPredicate<T>(Func<PCStruct, T> valueGetter, 
            T targetValue) where T: IComparable
        {
            return item => valueGetter(item).CompareTo(targetValue) != 1;
        }

        private void AddPredicateBasedOnTextBox<T>(List<Func<PCStruct, bool>> predicates, TextBox targetTextBox, 
            Func<Func<PCStruct, T>, T, Func<PCStruct, bool>> predicateFactory, Func<PCStruct, T> valueGetter, 
            Func<string, T> valueConverter) 
        {
            if (targetTextBox.Text.Length == 0) return;
            var predicate = predicateFactory(valueGetter, valueConverter(targetTextBox.Text));
            predicates.Add(predicate);
        }
    }
}
