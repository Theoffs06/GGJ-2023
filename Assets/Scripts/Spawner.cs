using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [SerializeField] private float spawnRate;
    [SerializeField] private List<GameObject> enemies = new();
    private float _time;

    private void Update() {
        _time += Time.deltaTime;
        if (!(_time >= spawnRate)) return;
        Instantiate(enemies[Random.Range(0, enemies.Count)], transform.position, Quaternion.Euler(Vector3.zero));
        _time = 0;
    }
}
