using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipTextSelector : MonoBehaviour {

	public string[] tips;
	// Use this for initialization
	void Start () {
		int rand = Random.Range(0, tips.Length);
		print(rand);
		GetComponent<TextMeshProUGUI>().text = tips[rand];
	}
}
