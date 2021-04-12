using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMech : MonoBehaviour
{
    private Camera cam;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void render_walk(int player)
    {
        if (player == 1) {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Treading"));
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Idle");
        }
        else {
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Treading"));
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Idle");
        }
        cam.cullingMask |= 1 << LayerMask.NameToLayer("WaterTop");
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("WaterBottom"));
        cam.renderingPath = RenderingPath.DeferredShading;
    }

    public void render_swim(int player)
    {
        if (player == 1) {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Model1-Treading");
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model1-Idle"));
        }
        else {
            cam.cullingMask |= 1 << LayerMask.NameToLayer("Model-Treading");
            cam.cullingMask &= ~(1 << LayerMask.NameToLayer("Model-Idle"));
        }
        cam.cullingMask &= ~(1 << LayerMask.NameToLayer("WaterTop"));
        cam.cullingMask |= 1 << LayerMask.NameToLayer("WaterBottom");
        cam.renderingPath = RenderingPath.UsePlayerSettings;
    }
}
