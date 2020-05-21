using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_8
{
    public class Filter
    {
        private List<Func<PCStruct, bool>> _predicates;

        public Filter(IEnumerable<Func<PCStruct, bool>> predicates)
        {
            _predicates = new List<Func<PCStruct, bool>>(predicates);
        }

        public List<PCStruct> FilterItems(List<PCStruct> items)
        {
            return items.Where(DoesItemApplyToPredicates).ToList();
        }

        private bool DoesItemApplyToPredicates(PCStruct item)
        {
            return _predicates.Aggregate(true, (acc, predicate) => acc && predicate(item));
        }
    }
}
