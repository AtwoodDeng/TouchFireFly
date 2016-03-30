using UnityEngine;
using System.Collections;

public class MyCameraFollow : MonoBehaviour {

	[SerializeField] GameObject target;
	[SerializeField] Vector3 relativePositon;
	[SerializeField] float followRate = 0.02f;

	// Use this for initialization
	void Start () {
		if ( target == null )
			target = MainCharacter.Instance.gameObject;
		if ( relativePositon == Vector3.zero && target != null )
			relativePositon = transform.position;
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if ( target != null && relativePositon != null )
			transform.position = Vector3.Lerp( transform.position , target.transform.position + relativePositon , followRate );
	}
}
