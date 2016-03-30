using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PeopleMaker : MonoBehaviour {

	[SerializeField] MaxMin makeTime;
	[SerializeField] GameObject objPrefab;
	[SerializeField] float speed;
	[SerializeField] bool needChangeColor = false;
	[SerializeField] Vector3 positionOffset;

	float timer;
	float nextTime = 0;
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime ;
		if ( timer > nextTime )
		{
			Make();
			timer = 0;
			nextTime = Random.Range( makeTime.min , makeTime.max );
		}
	}

	Vector3 getRandomOffset()
	{
		return new Vector3( Random.Range( - positionOffset.x , positionOffset.x ) 
			, Random.Range( - positionOffset.y , positionOffset.y )
			, Random.Range( - positionOffset.z , positionOffset.z )); 
	}

	void Make()
	{
		GameObject obj  = Instantiate( objPrefab , transform.position + getRandomOffset() , transform.rotation ) as GameObject;
		Vector3 toward = transform.forward;
		obj.transform.DOMoveY( 10f , 1f ).SetRelative(true).From();

		if ( needChangeColor )
		{
			MeshRenderer render = obj.GetComponentInChildren<MeshRenderer>();
			Material myCarMaterial = new Material(render.material.shader);
			myCarMaterial.SetColor("_Color", Global.getRandomColor() );
			render.material = myCarMaterial;
		}

	}



	void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireCube( transform.position , positionOffset * 2 );
	}

}
