using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct EnemyProbability {
    public GameObject prefab; 
    public int probability;
}

public class Spawner : MonoBehaviour {
    [Header("Params of spawner")]
    [SerializeField] private float spawnRate;

    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private List<EnemyProbability> enemies = new();
    private float _time;

    private void Update() {
        _time += Time.deltaTime;
        if (!(_time >= spawnRate)) return;
        Instantiate(GetEnemy(), new Vector3(Random.Range(transform.position.x - sizeX / 2, transform.position.x + sizeX / 2),1, Random.Range(transform.position.z - sizeY / 2, transform.position.z + sizeY / 2)), Quaternion.Euler(Vector3.zero));
        _time = 0;
    }

    private GameObject GetEnemy() {
        var probabilitySum = enemies.Sum(x => x.probability);
        var rng = Random.Range(0,probabilitySum);
        var tmpSum = 0;
        foreach (var enemy in enemies) {
            tmpSum += enemy.probability;
            if (rng < tmpSum) return enemy.prefab;
        }
        return null;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,new Vector3(sizeX,sizeY));
    }
}
