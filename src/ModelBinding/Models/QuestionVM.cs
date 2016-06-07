using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    public class QuestionVM:BaseVM
    {
        public string Answer { get; set; }
        public override string ControlType
        {
            get
            {
                return this.GetType().FullName;
            }

        }
    }
}
