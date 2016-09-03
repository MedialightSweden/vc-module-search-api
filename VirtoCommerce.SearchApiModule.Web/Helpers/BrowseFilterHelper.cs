﻿using System;
using System.Linq;
using Omu.ValueInjecter;
using VirtoCommerce.SearchModule.Data.Services;
using VirtoCommerce.SearchModule.Data.Model.Filters;
using System.Collections.Generic;

namespace VirtoCommerce.SearchApiModule.Web.Helpers
{
    public static class BrowseFilterHelper
    {
        #region Public Methods and Operators
        public static ISearchFilter Convert(ISearchFilter filter, string[] keys)
        {
            if (filter != null && keys != null)
            {
                // get values that we have filters set for
                var values = from v in filter.GetValues() where keys.Contains(v.Id, StringComparer.OrdinalIgnoreCase) select v;

                var attributeFilter = filter as AttributeFilter;
                if (attributeFilter != null)
                {
                    var newFilter = new AttributeFilter();
                    newFilter.InjectFrom(filter);
                    newFilter.Values = values.OfType<AttributeFilterValue>().ToArray();
                    return newFilter;
                }

                var rangeFilter = filter as RangeFilter;
                if (rangeFilter != null)
                {
                    var newFilter = new RangeFilter();
                    newFilter.InjectFrom(filter);

                    newFilter.Values = values.OfType<RangeFilterValue>().ToArray();
                    return newFilter;
                }

                var priceRangeFilter = filter as PriceRangeFilter;
                if (priceRangeFilter != null)
                {
                    var newFilter = new PriceRangeFilter();
                    newFilter.InjectFrom(filter);

                    newFilter.Values = values.OfType<RangeFilterValue>().ToArray();
                    return newFilter;
                }

                var categoryFilter = filter as CategoryFilter;
                if (categoryFilter != null)
                {
                    var newFilter = new CategoryFilter();
                    newFilter.InjectFrom(filter);
                    newFilter.Values = values.OfType<CategoryFilterValue>().ToArray();
                    return newFilter;
                }
            }

            return null;
        }

        public static AttributeFilterValue[] CreateAttributeFilterValues(IEnumerable<string> values)
        {
            return values.Select(v => new AttributeFilterValue
            {
                Id = v,
                Value = v
            })
            .ToArray();
        }

        #endregion
    }
}
