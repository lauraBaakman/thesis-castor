namespace Buttons
{
    public abstract class AbstractRegistrationButton : AbstractButton
    {
        public Registration.ICPRegisterer Registerer
        {
            set { registerer = value; }
        }
        protected Registration.ICPRegisterer registerer;
    }
}