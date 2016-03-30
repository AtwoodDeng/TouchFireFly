using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainCharacter : MonoBehaviour {

	public MainCharacter() { s_Instance = this; }
	public static MainCharacter Instance { get { return s_Instance; } }
	private static MainCharacter s_Instance;
	
	enum State
	{
		Normal,
		Left,
		Right,
		Up,
		Down
	}

	[SerializeField] State tempState = State.Normal;

	[SerializeField] Animator animator;

	[SerializeField] float SpeedLimit=2f;
	[SerializeField] float Acceleration=0.01f;
	[SerializeField] float Drag = 0.99f;
	[SerializeField] CharacterController controller;
	[SerializeField] AudioSource sound;
	[SerializeField] AudioClip grassClip;
	[SerializeField] AudioClip rockClip;
	[SerializeField] AudioClip mordernRoadClip;
	[SerializeField] float speedAffect = 0.5f;
	[SerializeField] float touchStopTime = 0.5f;

	enum FloorType
	{
		Grass,
		Rock,
		Mordern
	}

	[SerializeField] FloorType floorType;

	// Use this for initialization
	void Start () {
		if ( controller == null )
			controller = GetComponent<CharacterController>();
		Random.seed = System.DateTime.Now.Millisecond ;

		oriY = transform.position.y;
	}
	
	Vector3 m_vel;
	Vector3 m_acc;

	float oriY ;
	// Update is called once per frame
	void LateUpdate () {
		// get input and update the acceleration
		// if (Input.GetAxis("MCUp") > 0 )
		// {
		// 	ChangeToState(State.Up);
		// 	m_acc = Vector3.up * Acceleration * Input.GetAxis("MCUp");
		// }else if (Input.GetAxis("MCDown") > 0 )
		// {
		// 	ChangeToState(State.Down);
		// 	m_acc = Vector3.down * Acceleration * Input.GetAxis("MCDown");
		// }else if (Input.GetAxis("MCLeft") > 0 )
		// {
		// 	ChangeToState(State.Left);
		// 	m_acc = Vector3.left * Acceleration * Input.GetAxis("MCLeft");
		// }else if (Input.GetAxis("MCRight") > 0 )
		// {
		// 	ChangeToState(State.Right);
		// 	m_acc = Vector3.right * Acceleration * Input.GetAxis("MCRight");
		// }else 
		// {
		// 	ChangeToState(State.Normal);
		// 	m_acc = Vector3.zero;
		// }

		Vector3 dir = new Vector3(Input.GetAxis("MCHorizontal") , 0 , Input.GetAxis("MCVertical") );
		Vector3 m_acc = Acceleration * dir.normalized;

		if ( Input.GetAxis("MCHorizontal") < 0 )
		{
			ChangeToState(State.Left);
		}else if ( Input.GetAxis("MCHorizontal") > 0 )
		{
			ChangeToState(State.Right);
		}else if ( Input.GetAxis("MCVertical") < 0 )
		{
			ChangeToState(State.Down);
		}else if ( Input.GetAxis("MCVertical") > 0 )
		{
			ChangeToState(State.Up);
		}else
		{
			ChangeToState(State.Normal);
		}


		//update the velocity
		if ( ( Time.time - lastTouch ) < touchStopTime )
		{
			m_acc *= ( Time.time - lastTouch ) / touchStopTime * 0.33f ;
		}
		m_vel += m_acc * Time.deltaTime * 30f ;
		m_vel = Vector3.ClampMagnitude( m_vel , SpeedLimit );


		// transform.position += m_vel;
		m_vel *= Drag;
		m_vel.y = 0;
		controller.Move(m_vel);

		sound.pitch = controller.velocity.magnitude * speedAffect;
		if ( sound.pitch < 0.9f ) 
			sound.pitch = 0;

		Vector3 pos = transform.position;
		pos.y = oriY;
		transform.position = pos;

		RaycastHit info;
		int mask = ( LayerMask.GetMask("Floor")) | ( LayerMask.GetMask("RockRoad")) | (  LayerMask.GetMask("MordernRoad"));

		if ( Physics.Raycast( transform.position , Vector3.down , out info , 10f  ) )
		{
			
			FloorType nowType = FloorType.Grass;
			if ( info.collider.gameObject.layer == LayerMask.NameToLayer("Floor") )
				nowType = FloorType.Grass;
			else if ( info.collider.gameObject.layer == LayerMask.NameToLayer("RockRoad") )
				nowType = FloorType.Rock;
			else if ( info.collider.gameObject.layer == LayerMask.NameToLayer("MordernRoad") )
				nowType = FloorType.Mordern;

			if ( nowType != floorType ){
				switch ( nowType )
				{
				case FloorType.Grass:
					sound.clip = grassClip;
					break;
				case FloorType.Rock:
					sound.clip = rockClip;
					break;
				case FloorType.Mordern:
					sound.clip = mordernRoadClip;
					break;
				}
				floorType = nowType;
			}
		}
	}

	void ChangeToState(State s)
	{
		if (tempState != s)
		{
			switch( s )
			{
				case State.Left:
					AnimatorSetTrue("Left");
					break;
				case State.Right:
					AnimatorSetTrue("Right");
					break;
				case State.Up:
					AnimatorSetTrue("Up");
					break;
				case State.Down:
					AnimatorSetTrue("Down");
					break;
				case State.Normal:
					AnimatorSetTrue("Stay");
					break;
				default:
					break;
			}

			tempState = s;
			Debug.Log("Set to " + s);
		}
	}

	void AnimatorSetTrue(string key)
	{
		animator.SetBool("Left", false);
		animator.SetBool("Right", false);
		animator.SetBool("Up", false);
		animator.SetBool("Down", false);
		animator.SetBool("Stay", false);

		animator.SetBool(key, true);
	}

	float lastTouch = 0 ;
	public void Touch( Vector3 pos )
	{
		Vector3 toward = pos - transform.position;
		m_vel = toward.normalized * m_vel.magnitude * 3f ;

		lastTouch = Time.time;

		transform.DOShakeRotation( touchStopTime , 60f );

	}
}
