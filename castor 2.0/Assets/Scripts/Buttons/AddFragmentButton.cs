using System;
using UnityEngine;

namespace Buttons
{
    public class AddFragmentButton : AbstractButton
    {
        public GameObject FragmentsRoot;

        protected override void Awake()
        {
            base.Awake();

            if (Application.isEditor) LoadTestFragments();
        }

        private void LoadTestFragments()
        {
            IO.FragmentsImporter importer = new IO.FragmentsImporter(
                fragmentParent: FragmentsRoot,
                callBack: NotifyUser
            );
            importer.Import("/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models/RoughFracturedCube/RoughFracturedCubeXYZRotationTranslationPart1.obj", randomizeTransform: false);
            importer.Import("/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models/RoughFracturedCube/RoughFracturedCubeXYZRotationTranslationPart2.obj", randomizeTransform: false);
        }

        private void NotifyUser(Ticker.Message message)
        {
            SendMessage(
                methodName: "OnSendMessageToTicker",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            if (Application.isEditor) return IsEditorAddFragmentCombinationPressed();
            else return IsDeploymentAddFragmentCombinationPressed();
        }

        private bool IsDeploymentAddFragmentCombinationPressed()
        {
            return Input.GetButtonDown("Add Fragment") && RTEditor.InputHelper.IsAnyCtrlOrCommandKeyPressed();
        }

        private bool IsEditorAddFragmentCombinationPressed()
        {
            return IsDeploymentAddFragmentCombinationPressed() && RTEditor.InputHelper.IsAnyShiftKeyPressed();
        }

        protected override void ExecuteButtonAction()
        {
            new IO.FragmentsImporter(
                fragmentParent: FragmentsRoot,
                callBack: NotifyUser
            ).Import();
        }
    }
}