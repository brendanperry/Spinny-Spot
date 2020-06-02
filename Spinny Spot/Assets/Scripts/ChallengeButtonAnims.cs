using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeButtonAnims : MonoBehaviour {
	public GameObject currency;
	public UITransition_Position endless;
	public UITransition_Position endless2;
	public GameObject home;
	public GameObject triangle;
	public GameObject square;
	public GameObject pentagon;
	public GameObject bomb;
	public GameObject info;
	// Use this for initialization
	public void Animate() {
		currency.GetComponent<UITransition_Fade>().AnimateReverse();
		endless.GetComponent<UITransition_Scale>().AnimateReverse();
		home.GetComponent<UITransition_Scale>().AnimateReverse();
		info.GetComponent<UITransition_Scale>().AnimateReverse();
		triangle.GetComponent<UITransition_Position>().AnimateReverse();
		square.GetComponent<UITransition_Position>().AnimateReverse();
		pentagon.GetComponent<UITransition_Position>().AnimateReverse();
		bomb.GetComponent<UITransition_Position>().AnimateReverse();
	}

	void Start() {
		StartCoroutine(EndlessAnimate());
	}

	IEnumerator EndlessAnimate() {
		endless.Reset();
		endless.Animate();
		yield return new WaitForSeconds(3);
		endless2.Reset();
		endless2.Animate();
		yield return new WaitForSeconds(3);
		endless.Reset();
		endless.Animate();
		yield return new WaitForSeconds(3);
		endless2.Reset();
		endless2.Animate();
		yield return new WaitForSeconds(3);
		StartCoroutine(EndlessAnimate());
	}
}
