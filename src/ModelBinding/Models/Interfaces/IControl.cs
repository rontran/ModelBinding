using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models.Interfaces
{
    public interface IControl
    {
        Guid Id { get; set; }
        string Label { get; set; }
        string ControlType { get;  }
    }
}
