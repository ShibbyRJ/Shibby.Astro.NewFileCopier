using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewFileCopier
{
    internal class NewFileCopierConfig
    {
        public List<Mapping> Mappings { get; set; }
    }


    internal class Mapping
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public int DelayBeforeCopy { get; set; } = 20;
    }
}
