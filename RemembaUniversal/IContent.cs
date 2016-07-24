using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public interface IContent
    {
        string Data { get; set; }
        string Id { get; set; }
        string Touched { get; set; }
    }
}
