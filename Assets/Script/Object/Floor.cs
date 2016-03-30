using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	[SerializeField]Mesh mesh;
	[SerializeField] MeshFilter filter;

	[SerializeField] Vector3[] vertices;
	[SerializeField] int[] triangles;

	// Use this for initialization
	void Start () {
		mesh = new Mesh();
		filter.mesh = mesh;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
}
