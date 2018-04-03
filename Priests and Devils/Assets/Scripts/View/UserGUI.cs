using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class UserGUI : MonoBehaviour {
	private IUserAction action;
	public int status = 0; //0->continue, 1->lose, 2->win

    Rect labelContainer = new Rect(Screen.width / 2 - 60, Screen.height / 2 - 140 , 120, 50);
    Rect buttonContainer = new Rect(Screen.width / 2 - 70, Screen.height / 2 - 50, 140, 70);

    GUIStyle labelStyle;
	GUIStyle buttonStyle;

	void Start() {
		action = SSDirector.getInstance ().currentSceneController as IUserAction;

        labelStyle = new GUIStyle();
        labelStyle.fontSize = 60;
        labelStyle.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 35;
	}

	void OnGUI() {
		if (status == 1) {
			GUI.Label(labelContainer, "Gameover!", labelStyle);
			if (GUI.Button(buttonContainer, "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		}
        else if(status == 2) {
			GUI.Label(labelContainer, "You win!", labelStyle);
			if (GUI.Button(buttonContainer, "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		}
	}
}
