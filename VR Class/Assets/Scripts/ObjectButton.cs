using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectButton : GazeableButton {

    public Object obj_prefab;

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // set player mode to place furniture
        Player.instance.activeMode = InputMode.OBJ_PLACEMENT;

        // set active furniture prefab to this prefab
        Player.instance.activeObjPrefab = obj_prefab;


    }
}
