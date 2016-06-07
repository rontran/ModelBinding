using ModelBinding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    public class SurveyVM:BaseVM
    {
        public SurveyVM() {
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
