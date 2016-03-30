using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TouchMC : MonoBehaviour {

	[SerializeField] GameObject touchPrefab;
	[SerializeField] AudioSource sound ;

	void OnTriggerEnter(Collider col )
	{
		if ( col.GetComponent<MainCharacter>() != null )
		{
			Touch( col.GetComponent<MainCharacter>() );
		}
	}

	float lastTouch = 0;

	void Touch(MainCharacter mc )
	{
		if ( Time.time - lastTouch < 2f )
			return; 
		
		GameObject effect = Instantiate( touchPrefab ) as GameObject;
		effect.transform.position = transform.position;
		LogicManager.Instance.Touch();

		sound.Play();
		MainCharacter.Instance.Touch(transform.position);


		lastTouch = Time.time;
	}
}
