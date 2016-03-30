using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Killable : MonoBehaviour {

	[SerializeField] float moveTime = 1f;
	[SerializeField] Vector3 killMove;
	[SerializeField] float killScale;
	[SerializeField] Ease easeType;
	[SerializeField] bool ifShake = false;
	public void Kill()
	{
		transform.DOLocalMove( killMove , moveTime ).SetRelative(true).SetEase(easeType).OnComplete(SelfDestory).SetDelay(Random.Range(0,0.33f));
		transform.DOScale( killScale  * transform.localScale , moveTime ).SetEase(easeType);
		AudioSource sound = GetComponent<AudioSource>();
		if ( sound != null )
			sound.Play();
		if ( ifShake )
			transform.DOShakeRotation( moveTime , 60f );
	}
	public void SelfDestory()
	{
		gameObject.SetActive(false);
	}

}