using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : GazeableObject {

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        if (Player.instance.activeMode == InputMode.TELEPORT)
        {
            Vector3 destLocation = hitInfo.point;

            destLocation.y = Player.instance.transform.position.y;

            Player.instance.transform.position = destLocation;
        }

        else if (Player.instance.activeMode == InputMode.OBJ_PLACEMENT)
        {
            // create the piece of furniture
            GameObject placed_obj = GameObject.Instantiate(Player.instance.activeObjPrefab) as GameObject;

            // set the position of the furniture
            placed_obj.transform.position = hitInfo.point;
        }
    }

}
