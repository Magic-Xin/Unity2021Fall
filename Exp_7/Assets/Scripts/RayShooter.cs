using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class RayShooter : MonoBehaviour
{
	private int _bullet;
	private Camera _camera;
	
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	void Awake() {
		Messenger<float>.AddListener(GameEvent.BULLET_CHANGED, OnBulletsChanged);
	}
	void OnDestroy() {
		Messenger<float>.RemoveListener(GameEvent.BULLET_CHANGED, OnBulletsChanged);
	}

	void Start() {
		_camera = GetComponent<Camera>();
		_bullet = 1;
	}

	void OnGUI() {
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}

	void Update()
	{
		Vector3 point = new Vector3(_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
		Ray ray = _camera.ScreenPointToRay(point);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			GameObject hitObject = hit.transform.gameObject;
			ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
			if (target != null)
			{
				Messenger<Vector3>.Broadcast(GameEvent.ENEMY_AIMED, hit.transform.position);
			}
			else
			{
				Messenger.Broadcast(GameEvent.NOT_ENEMY_AIMED);
			}
			if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
			{
				StartCoroutine(SphereIndicator(hit.point));
			}
		}
	}

	private IEnumerator SphereIndicator(Vector3 pos)
	{
		int bulletCount = 0;
		while (bulletCount < _bullet)
		{
			_fireball = Instantiate(fireballPrefab) as GameObject;
			_fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
			_fireball.transform.rotation = transform.rotation;
			bulletCount++;
			yield return new WaitForSeconds(0.2f);
		}

		yield return new WaitForSeconds(1);
	}
	
	private void OnBulletsChanged(float num)
	{
		_bullet = (int)num;
	}
}