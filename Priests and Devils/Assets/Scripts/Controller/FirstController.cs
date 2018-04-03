using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{

	readonly Vector3 water_pos = new Vector3(0,0.5F,0);


	UserGUI userGUI;

	public CoastController startCoast;
	public CoastController endCoast;
	public BoatController boat;
	private CharacterController[] characters;

	void Awake() {
		SSDirector director = SSDirector.getInstance ();
		director.currentSceneController = this;
		userGUI = gameObject.AddComponent <UserGUI>() as UserGUI;
		characters = new CharacterController[6];
		loadResources ();
	}

	public void loadResources() {
		GameObject water = Instantiate (Resources.Load ("Perfabs/Water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
		water.name = "water";

		startCoast = new CoastController ("start");
		endCoast = new CoastController ("end");
		boat = new BoatController ();

		loadCharacter ();
	}

	private void loadCharacter() {
		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("priest");
			cha.setName("priest" + i);
			cha.setPosition (startCoast.getEmptyPosition ());
			cha.getOnCoast (startCoast);
			startCoast.getOnCoast (cha);

			characters [i] = cha;
		}

		for (int i = 0; i < 3; i++) {
			CharacterController cha = new CharacterController ("devil");
			cha.setName("devil" + i);
			cha.setPosition (startCoast.getEmptyPosition ());
			cha.getOnCoast (startCoast);
			startCoast.getOnCoast (cha);

			characters [i+3] = cha;
		}
	}


	public void moveBoat() {
		if (boat.isEmpty ())
			return;
        if(check_game_over() == 0)
		    boat.Move ();
		userGUI.status = check_game_over ();
	}

	public void characterIsClicked(CharacterController characterCtrl) {
        if (check_game_over() == 0)
        {
            if (characterCtrl.isOnBoat())
            {
                CoastController whichCoast;
                if (boat.get_end_or_start() == -1)
                { // end->-1; start->1
                    whichCoast = endCoast;
                }
                else
                {
                    whichCoast = startCoast;
                }

                boat.GetOffBoat(characterCtrl.getName());
                characterCtrl.moveToPosition(whichCoast.getEmptyPosition());
                characterCtrl.getOnCoast(whichCoast);
                whichCoast.getOnCoast(characterCtrl);

            }
            else // character on coast
            {
                CoastController whichCoast = characterCtrl.getCoastController();

                if (boat.getEmptyIndex() == -1)
                { // full
                    return;
                }

                if (whichCoast.get_end_or_start() != boat.get_end_or_start())
                    return;

                whichCoast.getOffCoast(characterCtrl.getName());
                characterCtrl.moveToPosition(boat.getEmptyPosition());
                characterCtrl.getOnBoat(boat);
                boat.GetOnBoat(characterCtrl);
            }
        }
		userGUI.status = check_game_over ();
	}

	int check_game_over() {	// 0->continue, 1->lose, 2->win
		int start_priest = 0;
		int start_devil = 0;
		int end_priest = 0;
		int end_devil = 0;

		int[] startCount = startCoast.getCharacterNum ();
		start_priest += startCount[0];
		start_devil += startCount[1];

		int[] endCount = endCoast.getCharacterNum ();
		end_priest += endCount[0];
		end_devil += endCount[1];

		if (end_priest + end_devil == 6)		// win
			return 2;

		int[] boatCount = boat.getCharacterNum ();
		if (boat.get_end_or_start () == -1) {	// boat at endCoast
			end_priest += boatCount[0];
			end_devil += boatCount[1];
		} else {	// boat at startCoast
			start_priest += boatCount[0];
			start_devil += boatCount[1];
		}
		if (start_priest < start_devil && start_priest > 0) {	// lose
			return 1;
		}
		if (end_priest < end_devil && end_priest > 0) {
			return 1;
		}
		return 0;	// continue
	}

	public void restart() {
		boat.reset ();
		startCoast.reset ();
		endCoast.reset ();
		for (int i = 0; i < characters.Length; i++) {
			characters [i].reset ();
		}
	}
}
