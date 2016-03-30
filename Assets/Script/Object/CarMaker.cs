using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CarMaker : MonoBehaviour {

	[SerializeField] float makeCarTime;
	[SerializeField] GameObject carPrefab;
	[SerializeField] float speed;
	[SerializeField] bool needChangeColor = false;
	[SerializeField] Vector3 positionOffset;

	List<GameObject> carList = new List<GameObject>();
	float timer;
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if ( timer > makeCarTime )
		{
			MakeCar();
			timer = 0;
		}

		for( int i = carList.Count -1 ; i >= 0 ; -- i )
		{
			if ( carList[i].transform.position.magnitude > LogicManager.Instance.moveRange.magnitude * 1.5f ) 
			{
				carList[i].transform.DOKill();
				carList.RemoveAt(i);
			}
		}
	}

	Vector3 getRandomOffset()
	{
		return new Vector3( Random.Range( - positionOffset.x , positionOffset.x ) 
			, Random.Range( - positionOffset.y , positionOffset.y )
			, Random.Range( - positionOffset.z , positionOffset.z )); 
	}

	void MakeCar()
	{
		GameObject car  = Instantiate( carPrefab , transform.position + getRandomOffset() , transform.rotation ) as GameObject;
		Vector3 toward = transform.forward;
		car.transform.DOMove( toward.normalized * 120f , 120f / speed ).SetRelative(true);

		if ( needChangeColor )
		{
			MeshRenderer render = car.GetComponentInChildren<MeshRenderer>();
			Material myCarMaterial = new Material(render.material.shader);
			myCarMaterial.SetColor("_Color", Global.getRandomColor() );
			render.material = myCarMaterial;
		}

		carList.Add( car );
	}



	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine( transform.position , transform.position + transform.forward * speed );
	}

	
}
