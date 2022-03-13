using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WanderingAI : MonoBehaviour {
	public const float baseSpeed = 3.0f;

	public float speed = 3.0f;
	public float obstacleRange = 5.0f;

	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	private bool _alive;

	public bool alive
	{
		set { alive = value; }
		get { return alive; }
	}

	void Awake() {
		Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}
	void OnDestroy() {
		Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void Start() {
		_alive = true;
	}
	
	void Update() {
		if (_alive) {
			//transform.Translate(0, 0, speed * Time.deltaTime);
			
			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hit;
			if (Physics.SphereCast(ray, 0.75f, out hit)) {
				GameObject hitObject = hit.transform.gameObject;
				if (hitObject.GetComponent<PlayerCharacter>()) {
					if (_fireball == null)
					{
						_fireball = Instantiate(fireballPrefab) as GameObject;
						_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
						_fireball.transform.rotation = transform.rotation;
					}
				}
				else if (hit.distance < obstacleRange && hit.collider.CompareTag("Wall")) {
					float angle = Random.Range(-110, 110);
					transform.Rotate(0, angle, 0);
				}
			}
		}
	}

	private void OnSpeedChanged(float value) {
		speed = baseSpeed * value;
	}
}
