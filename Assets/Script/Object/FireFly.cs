using UnityEngine;
using System.Collections;
using DG.Tweening;

public class FireFly : MonoBehaviour {
	[SerializeField] MaxMin flyTime;
	[SerializeField] MaxMin flyRange;
	[SerializeField] float flySpeed;

	[SerializeField] GameObject wingLeft;
	[SerializeField] GameObject wingRight;
	[SerializeField] float wingFrequency;
	[SerializeField] Light light;

	// Use this for initialization
	void Start () {
		wingLeft.transform.DOLocalRotate( new Vector3( -90f , 0 , 0 ) , 1f / wingFrequency ).SetRelative(true).SetLoops(999999,LoopType.Yoyo);
		wingRight.transform.DOLocalRotate( new Vector3( -90f , 0 , 0 ) , 1f / wingFrequency ).SetRelative(true).SetLoops(999999,LoopType.Yoyo);
	}


	float timer;
	float nextFlyTime;
	// Update is called once per frame
	Vector3 posLast;
	void Update () {
	
		timer += Time.deltaTime;
		if ( timer > nextFlyTime  )
		{
			nextFlyTime = Random.Range( flyTime.min , flyTime.max );
			FlyToNext();
			timer = 0;
		}

		Vector3 forward = transform.position - posLast;
		if ( forward.magnitude > 0 )
		{
			transform.rotation = Quaternion.Lerp( transform.rotation , Quaternion.LookRotation( forward ) , 0.95f);
		}
		posLast = transform.position;
	}

	float lastTime;
	void FlyToNext()
	{
		float a = Random.Range( 0 , 2f * Mathf.PI );
		Vector3 dir = new Vector3( Mathf.Cos( a ) , 0 ,  Mathf.Sin( a )  );
		FlyTo( dir );
	}
	void FlyTo(Vector3 dir)
	{
		Vector2 range = LogicManager.Instance.moveRange ;
		Vector3 next = transform.position;

		next = dir.normalized * Random.Range( flyRange.min , flyRange.max ); 
		next.y = Random.Range( -0.2f , 0.2f );

		if ( transform.position.x + next.x > range.x * 0.8f )
			next.x = -flyRange.min;
		if ( transform.position.x + next.x < - range.x * 0.8f )
			next.x = flyRange.min;
		if ( transform.position.z + next.z > range.y * 0.8f )
			next.z = -flyRange.min;
		if ( transform.position.z + next.z < - range.y * 0.8f )
			next.z = flyRange.min;

		next.y = Random.Range(1f,2f) - transform.position.y;

		transform.DOKill();
		transform.DOMove( next , next.magnitude / flySpeed ).SetRelative(true).SetEase(Ease.OutQuad );
	}



	void OnTriggerEnter( Collider col)
	{
		if ( col.GetComponent<MainCharacter>() != null )
		{
			MeetMC(col.GetComponent<MainCharacter>());
		}
	}



	void MeetMC( MainCharacter mc)
	{
		FlyTo( transform.position - mc.transform.position );
	}



}
