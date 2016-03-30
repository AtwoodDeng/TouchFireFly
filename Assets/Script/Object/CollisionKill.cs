using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CollisionKill : MonoBehaviour {

//	void OnCollision(Collision col )
//	{
//		Debug.Log("On Collision");
//			Killable ka = col.collider.GetComponent<Killable>();
//			if ( ka != null )
//			{
//				ka.Kill();
//			}
//	}

	void OnTriggerEnter(Collider col )
	{
		Killable ka = col.GetComponent<Killable>();
		if ( ka != null )
		{
			ka.Kill();
		}
	}

	[SerializeField] float moveTime = 1f;
	[SerializeField] Vector3 showMove;
	[SerializeField] Vector3 showScale;
	[SerializeField] Ease easeType;
	public void Show()
	{
		if ( showMove != Vector3.zero )
			transform.DOLocalMove( showMove , moveTime ).From().SetRelative(true).SetEase(easeType);
		if ( showScale != Vector3.one )
			transform.DOScale( showScale , moveTime ).From().SetEase(easeType);
	}
}