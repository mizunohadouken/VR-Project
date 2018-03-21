using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : GazeableButton
{
    private VRCanvas parentPanel;

    private Transform originalParent;

    private InputMode savedInputMode = InputMode.NONE;

    void Start ()
    {
        parentPanel = GetComponentInParent<VRCanvas>();

    }

    void Update()
    {

    }

    public override void OnPress(RaycastHit hitInfo)
    {
        base.OnPress(hitInfo);

        // Make the entire canvas a child of the camera to move with it
        originalParent = parentPanel.transform.parent;
        parentPanel.transform.parent = Camera.main.transform;

        savedInputMode = Player.instance.activeMode;
        Player.instance.activeMode = InputMode.DRAG;
    }

    public override void OnRelease(RaycastHit hitInfo)
    {
        base.OnRelease(hitInfo);

        parentPanel.transform.parent = originalParent;
        Player.instance.activeMode = savedInputMode;
    }
}
