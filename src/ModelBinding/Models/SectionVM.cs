using System.Collections.Generic;
using ModelBinding.Models.Interfaces;

namespace ModelBinding.Models
{
    public class SectionVM:BaseVM
    {
        public SectionVM() {
            Controls = new List<IControl>();
        }

        public IList<IControl> Controls { get; set; }

        public override string ControlType
        {
            get
            {
                return this.GetType().FullName;
            }
        }
    }
}
