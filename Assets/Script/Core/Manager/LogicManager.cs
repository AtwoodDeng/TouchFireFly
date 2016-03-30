using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class LogicManager : MonoBehaviour {

	public LogicManager() { s_Instance = this; }
	public static LogicManager Instance { get { return s_Instance; } }
	private static LogicManager s_Instance;

	[SerializeField] GameObject land;
	[SerializeField] Text text;

	[SerializeField] GameObject[] mordernRoads;
	[SerializeField] GameObject[] buildings;
	[SerializeField] GameObject people;
	[SerializeField] public Transform destination;
	[SerializeField] AudioSource original;
	[SerializeField] AudioSource final;
	[SerializeField] Light environment;

	[SerializeField] CameraFilterPack_TV_ARCADE effect;



	public Vector2 moveRange 
	{
		get {
			return land.transform.localScale / 2;
		}
	}

	void Start()
	{
		foreach(GameObject mr in mordernRoads)
		{
			mr.SetActive(false);
		}

		foreach(GameObject b in buildings)
		{
			b.SetActive(false);
		}
		people.SetActive(false);
		environment.DOIntensity( 0.03f , 200f );
	}

	int number = 0;
	public void Touch()
	{
		number ++;
		text.DOText( "You touch the firefly( " + number + " times)" , 1f );
		text.color = Color.white;
		text.DOFade( 0 , 2f ).SetDelay( 2f );
	}

	int state = 0; 
	void Update()
	{
		
		if ( Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftControl))
		{
			AddMordernRoad();
		}

		if  ( Input.GetKeyDown(KeyCode.B) && Input.GetKey(KeyCode.LeftControl))
		{
			AddBuilding();
		}

		if  ( Input.GetKeyDown(KeyCode.P) && Input.GetKey(KeyCode.LeftControl) )
		{
			AddPeople();
		}

		if  ( Input.GetKeyDown(KeyCode.F) && Input.GetKey(KeyCode.LeftControl) )
		{
			showFinal();
		}

		if  ( Input.GetKeyDown(KeyCode.Escape) && Input.GetKey(KeyCode.LeftControl) )
		{
			Application.Quit();
		}

		switch ( state ) 
		{
		case 0:
		case 1:
		case 2:
			if ( number >= 3 + state || Time.time > 45f + state * 10f )
				{
					AddMordernRoad();
					state ++;
				}
			break;
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
		case 9:

			if ( number >= 6 + ( state - 3 ) || Time.time > 100f + ( state - 3 ) * 9f )
			{
				AddBuilding();
				state ++;
			}
			break;
		case 10:

			if ( number >= 15 + ( state - 10 ) || Time.time > 180f + ( state - 10 ) * 10f )
			{
				AddPeople();
				state ++;
			}
			break;
		case 11:

			if ( number >= 18 + ( state - 11 ) || Time.time > 220f + ( state - 11 ) * 10f )
			{
				showFinal();
				state ++;
			}
			break;
		default:
			break;
		}

	}

	int mordernRoadIndex = 0;
	void AddMordernRoad()
	{
		if ( mordernRoads.Length > mordernRoadIndex)
		{
			mordernRoads[mordernRoadIndex].SetActive(true);
			mordernRoads[mordernRoadIndex].GetComponent<CollisionKill>().Show();
			mordernRoadIndex++;
		}
	}

	int buildingIndex = 0;
	void AddBuilding()
	{
		original.DOFade( 0 , 20f );
		if ( buildings.Length > buildingIndex)
		{
			buildings[buildingIndex].SetActive(true);
			buildings[buildingIndex].GetComponent<CollisionKill>().Show();
			buildingIndex++;
		}
	}

	void AddPeople()
	{
		final.Play();
		final.DOFade( 1f , 25f );
		people.SetActive(true);
	}

	void showFinal()
	{
		effect.enabled = true;
		text.text= "";
		text.DOText( "Why the subject from 400 years ago would like to play something like this? \n Is that fun?" , 5f );
		text.color = Color.white;
		text.DOFade( 0 , 2f ).SetDelay( 15f );
		GetComponent<Camera>().DOOrthoSize( 5f , 10f ).SetRelative( true );
		Camera[] cameras = GetComponentsInChildren<Camera>();
		foreach( Camera c in cameras)
		{
			c.DOOrthoSize( 5f , 10f ).SetRelative( true );
		}
	}

}
