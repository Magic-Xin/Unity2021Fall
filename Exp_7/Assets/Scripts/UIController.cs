using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private Text enemyLabel;
	[SerializeField] private Text healthLabel;
	[SerializeField] private SettingsPopup settingsPopup;
	[SerializeField] private List<GameObject> bloodEffect;

	private int _score;
	private int _hurt;
	private Vector2 offset = new Vector3(0.0f, 60.0f);

	void Awake() {
		Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit);
		Messenger<Vector3>.AddListener(GameEvent.ENEMY_AIMED, OnEnemyAimed);
		Messenger.AddListener(GameEvent.NOT_ENEMY_AIMED, NotEnemyAimed);
		Messenger<int>.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
	}
	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
		Messenger<Vector3>.RemoveListener(GameEvent.ENEMY_AIMED, OnEnemyAimed);
		Messenger.RemoveListener(GameEvent.NOT_ENEMY_AIMED, NotEnemyAimed);
		Messenger<int>.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
	}

	void Start() {
		_score = 0;
		_hurt = 0;
		scoreLabel.text = _score.ToString();

		settingsPopup.Close();
	}

	private void OnEnemyHit() {
		_score += 1;
		scoreLabel.text = _score.ToString();
	}

	public void OnOpenSettings() {
		settingsPopup.Open();
	}

	public void OnPointerDown() {
		Debug.Log("pointer down");
	}

	public void OnEnemyAimed(Vector3 pos)
	{
		Vector2 sceenPos = Camera.main.WorldToScreenPoint(pos);
		enemyLabel.gameObject.transform.position = sceenPos + offset;
		enemyLabel.text = "Enemy";
	}

	public void NotEnemyAimed()
	{
		enemyLabel.text = "";
	}

	public void OnHealthChanged(int _health)
	{
		healthLabel.text = "Health: " + _health;
		_hurt++;
		StartCoroutine(ShowBlood());
	}

	private IEnumerator ShowBlood()
	{
		float alpha = bloodEffect[0].GetComponent<RawImage>().color.a;
		while (alpha < 1 && _hurt > 0)
		{
			alpha += 0.1f;
			foreach (GameObject blood in bloodEffect)
			{
				blood.GetComponent<RawImage>().color = new Color(255, 255, 255, alpha);
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield return new WaitForSeconds(3);
		_hurt--;
		while (alpha > 0 && _hurt == 0)
		{
			alpha -= 0.1f;
			foreach (GameObject blood in bloodEffect)
			{
				blood.GetComponent<RawImage>().color = new Color(255, 255, 255, alpha);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}
}
