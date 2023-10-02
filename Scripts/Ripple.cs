using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{

    public RenderTexture togo;

    public ComputeShader drawer;

    [SerializeField] private Transform ya;


    private Material mat;

  
    private int kernid1, kernid2; 
    
    [SerializeField]
    private float finsih, start;
    
    [SerializeField]
    private float thresh,flat;

     public Vector2[] cent;
     private float[] timer;
     private int active= 1;
    
    
    [SerializeField]
    private int speed;
  
    private Vector3 screenpos;

    private float tim;
    private float tim1 ;

    private Vector3 bounds;
    private Vector3 globdist, nuglobdist,pos;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
        bounds = new Vector3(GetComponent<Renderer>().bounds.size.x, GetComponent<Renderer>().bounds.size.y,GetComponent<Renderer>().bounds.size.z);
        globdist = new Vector3(transform.position.x-bounds.x/2,transform.position.y - bounds.y/2, transform.position.z-bounds.z/2);
        nuglobdist = new Vector3(transform.position.x+bounds.x/2,transform.position.y + bounds.y/2, transform.position.z+bounds.z/2);

        print("low edge"+globdist);
        print("high edge"+nuglobdist);
        
        
        timer = new float[cent.Length];

        for (int i = 0; i < cent.Length; i++)
        {

            timer[i] = 0;


        }
        
        //print(cent[0]);
        //cent[0] = new Vector2(256, 256); 
        //cent[1] = new Vector2(70, 0); 
        togo = new RenderTexture(512, 512, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear);
        togo.enableRandomWrite = true;
        togo.Create();

        kernid1 = drawer.FindKernel("CSMain");
        kernid2 = drawer.FindKernel("Clear");
       
        
        // drawer.SetTexture(0,"Result",togo);
        // drawer.Dispatch(0, 512/8,512/8,1);

      mat = GetComponent<Renderer>().material ;
        //GetComponent<Renderer>().material.mainTexture = togo;
       mat.SetTexture("_Mov",togo);
    }
    
    
    
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        
        
        tim1 += Time.deltaTime;
        if (tim1 > 5f )
        {
            screenpos = ya.position;
       
         
        
            Vector3 slapdash = globdist;
            Vector3 nuslapdash = nuglobdist;
            
        
         
            Vector3 slaperdash = screenpos;
            

            //maybe remove unecessary division from steamer
            slaperdash.x = Mathf.InverseLerp(slapdash.x, nuslapdash.x, slaperdash.x);
            slaperdash.y = Mathf.InverseLerp(slapdash.z, nuslapdash.z, slaperdash.z);
         
            // print(slaperdash);
            // print(nuslapdash);
             print(slaperdash);
            
            pos = slaperdash;
            pos.x *= (512);
            pos.y *= (512);
         
        
        
            Vector2 newt = new Vector2(pos.x, pos.y);
            active= (active +1)% cent.Length ;
            cent[active] = newt;
            timer[active] = 0;
            tim1 = 0;

        }
        //print(tim1);
        
        
        drawer.SetTexture(kernid2,"Result",togo);
        drawer.Dispatch(kernid2, 512/8,512/8,1);
        
        for (int i = 0; i < cent.Length; i++)
        {
            timer[i] -= Time.deltaTime*speed;
            
            drawer.SetFloat("a", finsih);
            drawer.SetFloat("b", start);
            drawer.SetFloat("adder", timer[i]);
            drawer.SetFloat("thresh", thresh);
            drawer.SetFloat("flatner", flat);
            drawer.SetVector("cent", cent[i]);
            drawer.SetTexture(kernid1,"Result",togo);
            drawer.Dispatch(kernid1, 512/8,512/8,1);
            //8ssecs
        }

        
        
        
        
    }
}
