using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeVolume : MonoBehaviour {

    MeshFilter _mesh; //C# private C# feild
    
   // Mesh 


    public MeshFilter mesh {//C#  property

        get {
            if (_mesh == null) _mesh = GetComponent<MeshFilter>();// "lazy" initailisation
            return _mesh;
        }
    }

   

    void OnDrawGizmos() {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawWireCube(transform.position, mesh.bounds.size);
        Gizmos.DrawWireMesh(mesh.mesh, transform.position,transform.rotation,transform.localScale);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
