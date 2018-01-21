﻿using UnityEngine;

namespace Buttons
{
    public class AddFragmentButton : AbstractButton
    {
        public GameObject FragmentsRoot;

        protected override void Awake()
        {
            base.Awake();

            if (Application.isEditor)
            {
                IO.FragmentsImporter importer = new IO.FragmentsImporter(
                    fragmentParent: FragmentsRoot,
                    callBack: NotifyUserOfAddedFragment
                );
                importer.Import("/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models/cone.obj");
                importer.Import("/Users/laura/Repositories/thesis-castor/castor 2.0/Assets/Models/cube.obj");
            }
        }

        private void NotifyUserOfAddedFragment(string path, GameObject fragment)
        {
            Ticker.Message.InfoMessage message = new Ticker.Message.InfoMessage(
                string.Format(
                    "Added a fragment '{0}' to the scene from the file {1}.",
                    fragment.name, path
                )
            );
            SendMessage(
                methodName: "OnSendMessageToTicker",
                value: message,
                options: SendMessageOptions.RequireReceiver
            );
        }

        protected override bool HasDetectedKeyBoardShortCut()
        {
            return Input.GetButtonDown("Add Fragment");
        }

        protected override void ExecuteButtonAction()
        {
            new IO.FragmentsImporter(
                fragmentParent: FragmentsRoot,
                callBack: NotifyUserOfAddedFragment
            ).Import();
        }
    }
}