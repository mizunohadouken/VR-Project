using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSystem : MonoBehaviour {

    public GameObject reticle;

    public Color inactiveReticleColor = Color.gray;
    public Color activeReticleColor = Color.red;

    private GazeableObject currentGazeObject;
    private GazeableObject currentSelectedObject;
    private RaycastHit lastHit;

	// Use this for initialization
	void Start () {
        SetReticleColor(inactiveReticleColor);
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ProcessGaze();
        CheckForInput(lastHit);
	}

    public void ProcessGaze()
    {
        Ray raycastRay = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        Debug.DrawRay(raycastRay.origin, raycastRay.direction * 100);


        if (Physics.Raycast(raycastRay, out hitInfo))
        {
            // do something to object

            // check if interactable

            // get gameobject from the hitinfo
            GameObject hitObj = hitInfo.collider.gameObject;

            // chec if the object is a new object (first look)
            GazeableObject gazeObj = hitObj.GetComponentInParent<GazeableObject>();

            // check of GazeableObject component exists
            if (gazeObj != null)
            {
                // check if object is different
                if(gazeObj != currentGazeObject)
                {
                    clearCurrentObject();
                    currentGazeObject = gazeObj;
                    currentGazeObject.OnGazeEnter(hitInfo);
                    SetReticleColor(activeReticleColor);
                }
                else
                {
                    currentGazeObject.OnGaze(hitInfo);
                }
            }
            else
            {
                clearCurrentObject();
            }
            lastHit = hitInfo;
        }
        else
        {
            clearCurrentObject();
        }
    }

    private void SetReticleColor(Color reticleColor)
    {
        reticle.GetComponent<Renderer>().material.SetColor("_Color", reticleColor);

    }

    private void CheckForInput(RaycastHit hitInfo)
    {
        // check for down
        if (Input.GetMouseButtonDown(0) && currentGazeObject !=null)
        {
            currentSelectedObject = currentGazeObject;
            currentSelectedObject.OnPress(hitInfo);
        }

        // check for hold
        if (Input.GetMouseButton(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnHold(hitInfo);
        }
        
        // check for release
        if (Input.GetMouseButtonUp(0) && currentSelectedObject != null)
        {
            currentSelectedObject.OnRelease(hitInfo);
            currentSelectedObject = null;
        }
    }

    private void clearCurrentObject()
    {
        if (currentGazeObject != null)
        {
            currentGazeObject.OnGazeExit();
            SetReticleColor(inactiveReticleColor);
            currentGazeObject = null;
        }
    }

}
