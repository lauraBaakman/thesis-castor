namespace Buttons
{
	namespace RegistrationButtons
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

}