namespace ModelBinding.Models
{
    public class LabelVM:BaseVM
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
