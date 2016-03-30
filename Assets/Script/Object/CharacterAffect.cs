using UnityEngine;
using System.Collections;

public class CharacterAffect : MonoBehaviour {

	float m_distance;
	public float Distance{
		get {
			return m_distance;
		}
	}

	public float affectRange = 1.0f;
	public AnimationCurve ChangeCurve;

	float m_change;
	public float Change{
		get {
			return m_change;
		}
	}

	[SerializeField] bool isRandomInit = false;
	[SerializeField] Vector3 RandomRotation = new Vector3(0,180f,0);
	[SerializeField] Vector3 RandomScale = new Vector3( 1.2f , 1.2f , 1.2f );

	void Awake()
	{
		Init();
	}

	virtual protected void Init()
	{
		if ( isRandomInit )
		{
			Vector3 euler = transform.eulerAngles;
			euler.x += Random.Range(- RandomRotation.x , RandomRotation.x );
			euler.y += Random.Range(- RandomRotation.y , RandomRotation.y );
			euler.z += Random.Range(- RandomRotation.z , RandomRotation.z );
			transform.eulerAngles = euler;

			Vector3 temScale = transform.localScale;
			temScale.x *= Random.Range( 1 / RandomScale.x , RandomScale.x );
			temScale.y *= Random.Range( 1 / RandomScale.y , RandomScale.y );
			temScale.z *= Random.Range( 1 / RandomScale.z , RandomScale.z );
			transform.localScale = temScale;

		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		m_distance = (MainCharacter.Instance.transform.position - transform.position).magnitude;
		m_change = (m_distance < affectRange) ? ChangeCurve.Evaluate( 1f - m_distance/affectRange) : 0;
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, affectRange);
	}
}
