using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public interface INodeView
    {
        INode SelectedNode { get; set; }
        INode RootNode { get; set; }
    }
}
