﻿using System;
using Microsoft.Azure.Search;

namespace Application.Search.Model
{
    public class SearchableFinishedSchool
    {
        [IsFacetable, IsSearchable, IsFilterable]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
