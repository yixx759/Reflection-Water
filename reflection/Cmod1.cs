using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


// public class sys1
// {
//     static sys1 instancef;
//
//     static public sys1 instance {
//         get
//         {
//
//             if (instancef == null)
//             {
//                 instancef = new sys1();
//             }
//
//             return instancef;
//         }
//
//     }
//
//     internal HashSet<obj1> cmdobj = new HashSet<obj1>();
//
//     public void add(obj1 o)
//     {
//         remove(o);
//         cmdobj.Add(o);
//
//     }
//
//
//     public void remove(obj1 o)
//     {
//         cmdobj.Remove(o);
//
//     }
//
//
//
//
// }




// public class Cmod1 : MonoBehaviour
// {
//     private CommandBuffer m_GlowBuffer1;
//     private Dictionary<Camera, CommandBuffer> m_Cameras = new Dictionary<Camera, CommandBuffer>();
//    
//
//     private void Cleanup()
//     {
//         foreach(var cam in m_Cameras)
//         {
//             if(cam.Key)
//                 cam.Key.RemoveCommandBuffer(CameraEvent.AfterLighting, cam.Value);
//         }
//         m_Cameras.Clear();
//     }
//
//     public void OnDisable()
//     {
//         Cleanup();
//     }
//
//     public void OnEnable()
//     {
//         Cleanup();
//     }
//
//
//     public void OnWillRenderObject()
//     {
//         var render = gameObject.activeInHierarchy && enabled;
//         if(!render)
//         {
//             Cleanup();
//             return;
//         }
//
//         var cam = Camera.current;
//         if(!cam)
//             return;
//
//         if(m_Cameras.ContainsKey(cam))
//             return;
//
//         m_GlowBuffer1 = new CommandBuffer();
//         m_GlowBuffer1.name = "hello";
//         m_Cameras[cam] = m_GlowBuffer1;
//
//         var glow = sys1.instance;
//
//         int tempid1 = Shader.PropertyToID("_Temp11");
//         
//         m_GlowBuffer1.GetTemporaryRT(tempid1, -1,-1,24, FilterMode.Bilinear);
//         m_GlowBuffer1.SetRenderTarget(tempid1);
//         m_GlowBuffer1.ClearRenderTarget(true, true, Color.black);
//         
//         
//
//         foreach (obj1 var in glow.cmdobj)
//         {
//             Material hi = var.mat;
//             Renderer rnd = var.GetComponent<Renderer>();
//             if (hi && rnd)
//             {
//                 m_GlowBuffer1.DrawRenderer(rnd,hi);
//                 
//                 
//             }
//
//         }
//         int tempid2 = Shader.PropertyToID("_Temp1");
//         
//         // m_GlowBuffer1.GetTemporaryRT(tempid1, -1,-1,24, FilterMode.Bilinear);
//         // m_GlowBuffer1.Blit(tempid, tempid1,a);
//        
//         
//         
//         m_GlowBuffer1.SetGlobalTexture("aReflec1",tempid1);
//         cam.AddCommandBuffer(CameraEvent.AfterLighting, m_GlowBuffer1);
//
//
//
//     }
// }
