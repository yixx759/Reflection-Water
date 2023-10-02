using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj : MonoBehaviour
{
   public Material mat;


   public void OnEnable()
   {
      sys.instance.add(this);
   }

   public void Start()
   {
      sys.instance.add(this);
   }

   public void OnDisable()
   {
      sys.instance.remove(this);
   }
}
