using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class sys1
{
    static sys1 instancef;

    static public sys1 instance {
        get
        {

            if (instancef == null)
            {
                instancef = new sys1();
            }

            return instancef;
        }

    }

    internal HashSet<obj1> cmdobj = new HashSet<obj1>();

    public void add(obj1 o)
    {
        remove(o);
        cmdobj.Add(o);

    }


    public void remove(obj1 o)
    {
        cmdobj.Remove(o);

    }




}
public class sys
{
    static sys instancef;

    static public sys instance {
        get
        {

            if (instancef == null)
            {
                instancef = new sys();
            }

            return instancef;
        }

    }

    internal HashSet<obj> cmdobj = new HashSet<obj>();

    public void add(obj o)
    {
        remove(o);
        cmdobj.Add(o);

    }


    public void remove(obj o)
    {
        cmdobj.Remove(o);

    }




}




public class Cmod : MonoBehaviour
{
    private CommandBuffer m_GlowBuffer;
   // private CommandBuffer m_GlowBuffer1;
    private Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();
   

    private void Cleanup()
    {
        foreach(var cam in m_Cameras)
        {
            if(cam.Key)
                cam.Key.RemoveCommandBuffer(CameraEvent.BeforeLighting, cam.Value);
        }
        m_Cameras.Clear();
    }
    
    public void OnDisable()
    {
        Cleanup();
    }
    
    public void OnEnable()
    {
        Cleanup();
    }


    public void OnWillRenderObject()
    {
        var render = gameObject.activeInHierarchy && enabled;
        if(!render)
        {
            Cleanup();
            return;
        }
        
        var cam = Camera.current;
        if(!cam)
            return;

        if(m_Cameras.ContainsKey(cam))
            return;

        m_GlowBuffer = new CommandBuffer();
        m_GlowBuffer.name = "hello";
        m_Cameras[cam] = m_GlowBuffer;

        var nuglow = sys1.instance;

        int tempid1 = Shader.PropertyToID("_Temp11");
        
        m_GlowBuffer.GetTemporaryRT(tempid1, -1,-1,24, FilterMode.Bilinear);
        m_GlowBuffer.SetRenderTarget(tempid1);
        m_GlowBuffer.ClearRenderTarget(true, true, Color.clear);
        
        

        foreach (obj1 var1 in nuglow.cmdobj)
        {
            Material mat = var1.mat;
            Renderer rnd = var1.GetComponent<Renderer>();
            if (mat && rnd)
            {
                m_GlowBuffer.DrawRenderer(rnd,mat);
                
                
            }

        }
        m_GlowBuffer.SetGlobalTexture("aReflec1",tempid1);
        //m_GlowBuffer.ReleaseTemporaryRT(tempid1);
       // cam.AddCommandBuffer(CameraEvent.BeforeLighting, m_GlowBuffer1);
        
      
        
        // m_GlowBuffer = new CommandBuffer();
        // m_GlowBuffer.name = "hello1";
        // m_Cameras[cam] = m_GlowBuffer;

        var glow = sys.instance;

        int tempid = Shader.PropertyToID("_Temp");
        
        m_GlowBuffer.GetTemporaryRT(tempid, -1,-1,24, FilterMode.Bilinear);
        m_GlowBuffer.SetRenderTarget(tempid);
       m_GlowBuffer.ClearRenderTarget(true, true, Color.clear);
        
        

        foreach (obj var in glow.cmdobj)
        {
            Material hi = var.mat;
            Renderer rnd = var.GetComponent<Renderer>();
            if (hi && rnd)
            {
                m_GlowBuffer.DrawRenderer(rnd,hi,0,0);
                
                
            }

        }
      
        
    
       
        
        
        m_GlowBuffer.SetGlobalTexture("_Reflec",tempid);
        cam.AddCommandBuffer(CameraEvent.BeforeLighting, m_GlowBuffer);

       

    }
}
