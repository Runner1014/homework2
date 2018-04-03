using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class ClickGUI : MonoBehaviour {
	IUserAction action;
	CharacterController characterController;

	public void setController(CharacterController characterCtrl) {
		characterController = characterCtrl;
	}

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;
	}

	void OnMouseDown() {
		if (gameObject.name == "boat") {
			action.moveBoat ();
		}
        else {
			action.characterIsClicked (characterController);
		}
	}
}
