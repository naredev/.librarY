using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalApp
{
    internal class issues
    {
        public string Location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }  // Optional file attachment

        public issues(string location, string category, string description, string filePath)
        {
            Location = location;
            Category = category;
            Description = description;
            FilePath = filePath;
        }

        public override string ToString()
        {
            return $"Location: {Location}, Category: {Category}, Description: {Description}";
        }
    }
}
