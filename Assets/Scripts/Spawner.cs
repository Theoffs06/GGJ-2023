using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    [Header("Params of spawner")]
    [SerializeField] private float spawnRate;

    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private List<GameObject> enemies = new();
    private float _time;

    private void Update() {
        _time += Time.deltaTime;
        if (!(_time >= spawnRate)) return;
        Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(Random.Range(transform.position.x - sizeX / 2, transform.position.x + sizeX / 2),1, Random.Range(transform.position.z - sizeY / 2, transform.position.z + sizeY / 2)), Quaternion.Euler(Vector3.zero));
        _time = 0;
    }
}
