using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj1 : MonoBehaviour
{
   public Material mat;


   public void OnEnable()
   {
      sys1.instance.add(this);
   }

   public void Start()
   {
      sys1.instance.add(this);
   }

   public void OnDisable()
   {
      sys1.instance.remove(this);
   }
}
