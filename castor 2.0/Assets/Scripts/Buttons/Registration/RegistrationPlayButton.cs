using UnityEngine;
using System.Collections.Generic;
namespace Buttons
{
	namespace RegistrationButtons
	{
		public class RegistrationPlayButton : AbstractRegistrationButton
		{
			protected override void Awake()
			{
				base.Awake();

				Button.interactable = false;
			}

			protected override void ExecuteButtonAction()
			{
				if (registerer == null) return;
				StartCoroutine(RegistrationCoroutine());
			}

			private IEnumerator<object> RegistrationCoroutine()
			{
				while (!registerer.HasTerminated)
				{
					registerer.PrepareStep();
					yield return null;

					registerer.Step();
					yield return null;
				}
			}

			protected override bool HasDetectedKeyBoardShortCut()
			{
				return Input.GetKeyDown(KeyCode.Return);
			}
		}
	}

}

