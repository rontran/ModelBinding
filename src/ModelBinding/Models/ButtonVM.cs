using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    public class ButtonVM:BaseVM
    {
        public override string ControlType
        {
            get
            {
                return this.GetType().FullName;
            }

        }
    }
}
