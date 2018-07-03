using System;
using System.Collections.Generic;

namespace smprojectMVC3.Models
{
    public partial class LibaryandSaveViewModel
    {
        public int LegoID { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectContent { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }

        public IEnumerable<LegoLibrary> LegoLibrary { get; set; }
        public IEnumerable<SaveTable> SaveTables { get; set; }
    }
}