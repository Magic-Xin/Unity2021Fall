using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public float difficulty = 7.0f;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;
    [SerializeField] private TextMesh score;
    [SerializeField] private TextMesh highestScore;
    [SerializeField] private Camera _camera;

    private GameObject _player;
    private List<GameObject> _enemies;

    private bool start;
    private float time;

    private int _score;
    private int _highestScore;
    // Start is called before the first frame update
    void Start()
    {
        start = false;
        time = 0.0f;
        _enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            _score = _enemies.Count;
            score.text = "Score: " + _score;
            if (_score > _highestScore)
            {
                _highestScore = _score;
                highestScore.text = "Highest Score: " + _highestScore;
            }
            time += Time.deltaTime;
            if (time >= difficulty || _enemies.Count == 0)
            {
                time %= difficulty;
                GameObject _enemy = Instantiate(enemy) as GameObject;
                _enemy.GetComponent<Rigidbody2D>().MovePosition(new Vector2(Random.Range(-1.0f, 1.0f) * 6.0f, 3.65f));
                _enemy.GetComponent<EnemyAI>().forward = Random.Range(-1.0f, 1.0f) > 0.0f ? 1.0f : -1.0f;
                _enemies.Add(_enemy);
            }
            
            if (_player == null)
            {
                _camera.GetComponent<CameraShake>().Shake(0.5f, 0.5f);
                StopGame();
            }
        }
    }

    public void StartGame()
    {
        if (!start)
        {
            _score = 0;
            time = 0.0f;
            start = true;
            _player = Instantiate(player) as GameObject;
        }
    }

    public void RestartGame()
    {
        StopGame();
        StartGame();
    }

    public void StopGame()
    {
        start = false;
        foreach (GameObject e in _enemies)
        {
            Destroy(e);
        }
        _enemies.Clear();
        if (player != null)
        {
            Destroy(_player);
        }
    }
}
