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
