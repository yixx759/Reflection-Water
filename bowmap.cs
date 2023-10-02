using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class bowmap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 BoxLength = this.transform.localScale;
        Vector3 Boxloc = this.transform.position;

        Vector3 Bmin = Boxloc - BoxLength / 2;
        Vector3 Bmax = Boxloc + BoxLength / 2;

        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("BBoxmin", Bmin);
        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("BBoxmax", Bmax);
        this.gameObject.GetComponent<Renderer>().sharedMaterial.SetVector("BBoxcent", Boxloc);

    }

}
