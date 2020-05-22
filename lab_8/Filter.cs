using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_8
{
    public class Filter
    {
        private List<Func<PCStruct, bool>> _predicates = new List<Func<PCStruct, bool>>();

        public List<PCStruct> FilterItems(List<PCStruct> items)
        {
            return items.Where(DoesItemApplyToPredicates).ToList();
        }

        public static Func<PCStruct, bool> ContainsPredicate(Func<PCStruct, string> valueGetter, string targetValue)
        {
            return item => valueGetter(item).ToLower().Contains(targetValue.ToLower());
        }

        public static Func<PCStruct, bool> GreaterThanOrEqualToPredicate<T>(Func<PCStruct, T> valueGetter,
            T targetValue) where T : IComparable
        {
            return item => valueGetter(item).CompareTo(targetValue) != -1;
        }

        public static Func<PCStruct, bool> LessThanOrEqualToPredicate<T>(Func<PCStruct, T> valueGetter,
            T targetValue) where T : IComparable
        {
            return item => valueGetter(item).CompareTo(targetValue) != 1;
        }

        public void AddPredicate<T>(Func<Func<PCStruct, T>, T, Func<PCStruct, bool>> predicateFactory, 
            Func<PCStruct, T> pcValueGetter, T val)
        {
            _predicates.Add(predicateFactory(pcValueGetter, val));
        }

        private bool DoesItemApplyToPredicates(PCStruct item)
        {
            return _predicates.Aggregate(true, (acc, predicate) => acc && predicate(item));
        }
    }
}
