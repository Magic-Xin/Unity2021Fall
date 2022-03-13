using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
	private int _health;
	private bool _hurt;

	void Start() {
		_health = 5;
		_hurt = false;
	}

	public void Hurt(int damage) {
		_health -= damage;
		Messenger<int>.Broadcast(GameEvent.HEALTH_CHANGED, _health);
		Debug.Log("Health: " + _health);
	}
}
