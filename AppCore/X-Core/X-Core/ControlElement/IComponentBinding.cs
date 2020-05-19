using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Core.ControlElement
{
    public interface IComponentBinding<T>
    {
        T Bind { get; set; }
    }
}
