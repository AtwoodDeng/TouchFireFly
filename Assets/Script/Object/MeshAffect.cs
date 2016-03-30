using UnityEngine;
using System.Collections;

public class MeshAffect : CharacterAffect {

	Mesh mesh;
	IEnumerator UpdateMesh()
	{
		if ( mesh == null )
			yield break;

		verticesSave = new Vector3[mesh.vertices.Length];
		mesh.vertices.CopyTo(verticesSave, 0);
		float[] randoms = new float[mesh.vertices.Length];
		for(int i = 0 ; i < randoms.Length ; ++ i )
			randoms[i] = Random.Range(-1f, 1f);
		while(true)
		{
			Vector3 mPos = MainCharacter.Instance.transform.position;
			
			Vector3[] tem = new Vector3[mesh.vertices.Length];

			for ( int k = 0 ; k < ThreadNum ; ++ k )
			{
				mesh.vertices.CopyTo(tem, 0);
				for( int i = mesh.vertices.Length / ThreadNum * k ; i < mesh.vertices.Length / ThreadNum * (k+1) && i < mesh.vertices.Length; ++ i )
				{
					Vector3 dis = verticesSave[i] - mPos;
					Vector3 offset = new Vector3( Mathf.Sin(dis.magnitude * changeRate + randoms[i] * 5f ) ,  Mathf.Cos(dis.magnitude * changeRate - randoms[i] * 8f )  ,  Mathf.Sin(dis.magnitude * changeRate * 2f + randoms[i] * 2f )  );
					offset.Normalize();
					offset *= changeValue * Change;
					tem[i] = verticesSave[i] + offset;
				}

				mesh.vertices = tem;
				mesh.RecalculateBounds();

				yield return null;
			}

		}
	}

	public int ThreadNum = 1;
	public float changeRate = 0.1f;
	public float changeValue = 0.1f;

	Vector3[] verticesSave;
    protected override void Init () {
        base.Init();
        if ( mesh == null )
        	mesh =  GetComponent<MeshFilter>().mesh;
        StartCoroutine(UpdateMesh());
    }
}
