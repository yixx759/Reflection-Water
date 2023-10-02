using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Post : MonoBehaviour
{
    [FormerlySerializedAs("lol")] [SerializeField] private Material reflectionMaterial;
    
    
    private Vector4 wpos;
    
    private Camera cam;
    private Vector3 startbounds;
  
    [SerializeField]
    private Transform trans1;

    
    private Vector3 boundya;

    private void Start()
    {
         boundya = new Vector3(trans1.GetComponent<Renderer>().bounds.size.x, 
             trans1.GetComponent<Renderer>().bounds.size.y, trans1.GetComponent<Renderer>().bounds.size.z); 
       
       cam =  GetComponent<Camera>();
       wpos = trans1.position;
     
    }

    private Vector3 toviewscreenspace(Vector3 pos)
    {
        
        Matrix4x4 screenspace = cam.projectionMatrix*cam.transform.worldToLocalMatrix;
        Vector4 v = screenspace * new Vector4(pos.x, pos.y, pos.z, 1);
        Vector3 viewport = v / -v.w;
        viewport = Vector3.Scale(viewport , new Vector3(0.5f,0.5f,0.5f)) + new Vector3(0.5f,0.5f,0.5f);
        return viewport;

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        wpos = trans1.position;
        
        startbounds = new Vector3(wpos.x,wpos.y,wpos.z) - new Vector3(boundya.x,boundya.y,boundya.z) / 2;
 
        Vector3 startviewport =  cam.WorldToScreenPoint(startbounds);

        startviewport =new Vector3(startviewport.x/Screen.width, startviewport.y/Screen.height,1);
        
        reflectionMaterial.SetVector("startviewport",startviewport);
        
       
        Graphics.Blit(source, destination, reflectionMaterial);
    }
}
