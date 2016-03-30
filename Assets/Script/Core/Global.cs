using UnityEngine;
using System.Collections;

public class Global {

	static Color[] ColorList = {
		Color.black,
		Color.blue,
		Color.red,
		Color.white,
		Color.yellow,
		Color.cyan,
		Color.green,
		Color.magenta,
		Color.black,
	};

	static public Color getRandomColor()
	{
		int i = Random.Range( 0 , ColorList.Length);
		return ColorList[i];
	}
}

[System.Serializable]
struct MaxMin
{
	public float min;
	public float max;
}