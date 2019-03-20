using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DecalRender : MonoBehaviour
{
    // Start is called before the first frame update
    
    public DepthTextureMode mode = DepthTextureMode.Depth;

    public void Start()
    {

    }

    // before a camera renders this 
    public void OnWillRenderObject()
    {

        if (!enabled)
            return;

        Camera cam = Camera.current;
        if (!cam)
            return;

        cam.depthTextureMode = mode;

    }
}