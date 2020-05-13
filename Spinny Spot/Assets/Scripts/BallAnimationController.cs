using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SecPlayerPrefs;

public class BallAnimationController : MonoBehaviour {

	public Animator ballOne;
	public Animator ballTwo;
	public Animator ballThree;
	public Image ballOneImage;
	public Image ballTwoImage;
	public Image ballThreeImage;

	public Sprite[] sprites;
	public TextMeshProUGUI unlocked;
	public string totalBalls;


	// Use this for initialization
	void Start () {

		unlocked.text = "  " + (SecurePlayerPrefs.GetInt("unlockCount", 0) + 1).ToString() + "/" + totalBalls;

		InvokeRepeating("Player", 1, 9);

		ballOneImage.sprite = sprites[Random.Range(0, sprites.Length)];

		int num2 = Random.Range(0, sprites.Length);
		while(sprites[num2] == ballOneImage.sprite) {
			num2 = Random.Range(0, sprites.Length);
		}
		ballTwoImage.sprite = sprites[num2];

		int num3 = Random.Range(0, sprites.Length);
		while(sprites[num3] == ballOneImage.sprite || sprites[num3] == ballTwoImage.sprite) {
			num3 = Random.Range(0, sprites.Length);
		}
		ballThreeImage.sprite = sprites[num3];
	}

	void Player() {
		StartCoroutine("Loop");
	}

	IEnumerator Loop() {
		yield return new WaitForSeconds(3);
		ballOne.Play("BallOne", -1, 0);
		StartCoroutine(ChangeSprite(1));

		yield return new WaitForSeconds(3);
		ballTwo.Play("BallOne", -1, 0);
		StartCoroutine(ChangeSprite(2));

		yield return new WaitForSeconds(3);
		ballThree.Play("BallOne", -1, 0);
		StartCoroutine(ChangeSprite(3));
	}

	int num = 0;
	IEnumerator ChangeSprite(int ball) {
		num = Random.Range(0, sprites.Length);

		while(sprites[num] == ballOneImage.sprite || sprites[num] == ballTwoImage.sprite || sprites[num] == ballThreeImage.sprite) {
			num = Random.Range(0, sprites.Length);
		}
		yield return new WaitForSeconds(.45f);
		if(ball == 1) {
			ballOneImage.sprite = sprites[num];
		} else if(ball == 2) {
			ballTwoImage.sprite = sprites[num];
		} else {
			ballThreeImage.sprite = sprites[num];
		}
	}
	
}
