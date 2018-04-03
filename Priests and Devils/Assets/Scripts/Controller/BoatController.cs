using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mygame;

public class BoatController
{
    readonly GameObject boat;
    readonly Moveable moveableScript;
    readonly Vector3 startPosition = new Vector3(5, 1, 0);
    readonly Vector3 endPosition = new Vector3(-5, 1, 0);
    readonly Vector3[] start_positions;
    readonly Vector3[] end_positions;

    int end_or_start;
    CharacterController[] passenger = new CharacterController[2];

    public BoatController()
    {
        end_or_start = 1;

        start_positions = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        end_positions = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };

        boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)), startPosition, Quaternion.identity, null) as GameObject;
        boat.name = "boat";

        moveableScript = boat.AddComponent(typeof(Moveable)) as Moveable;
        boat.AddComponent(typeof(ClickGUI));
    }


    public void Move()
    {
        if (end_or_start == -1)
        {
            moveableScript.setDestination(startPosition);
            end_or_start = 1;
        }
        else
        {
            moveableScript.setDestination(endPosition);
            end_or_start = -1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool isEmpty()
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = getEmptyIndex();
        if (end_or_start == -1)
        {
            pos = end_positions[emptyIndex];
        }
        else
        {
            pos = start_positions[emptyIndex];
        }
        return pos;
    }

    public void GetOnBoat(CharacterController characterCtrl)
    {
        int index = getEmptyIndex();
        passenger[index] = characterCtrl;
    }

    public CharacterController GetOffBoat(string passenger_name)
    {
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] != null && passenger[i].getName() == passenger_name)
            {
                CharacterController charactorCtrl = passenger[i];
                passenger[i] = null;
                return charactorCtrl;
            }
        }
        Debug.Log("Cant find passenger in boat: " + passenger_name);
        return null;
    }

    public GameObject getGameobj()
    {
        return boat;
    }

    public int get_end_or_start()
    {
        return end_or_start;
    }

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < passenger.Length; i++)
        {
            if (passenger[i] == null)
                continue;
            if (passenger[i].getType() == 0) // 0->priest, 1->devil
            {
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }

    public void reset()
    {
        moveableScript.reset();
        if (end_or_start == -1)
        {
            Move();
        }
        passenger = new CharacterController[2];
    }
}