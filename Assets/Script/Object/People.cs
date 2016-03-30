using UnityEngine;
using System.Collections;

public class People : MonoBehaviour {
	[SerializeField] AudioSource sound;
	[SerializeField] float speedAffect = 0.8f;
	NavMeshAgent agent;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		if ( sound == null )
			sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		agent.destination = LogicManager.Instance.destination.position;

	}
}
