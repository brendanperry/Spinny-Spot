using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour 
{
	public void GoToScene(string name)
	{		
		Application.LoadLevel(name);
	}
}
