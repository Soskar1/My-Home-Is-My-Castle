using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private int _entityNumberToSpawn;
    [SerializeField] private float _timeToSpawn;

    private bool _timerIsGoing;

    public void SpawnEnemies()
    {
        for (int index = 0; index < _entityNumberToSpawn; index++)
        {
            if (!_timerIsGoing)
            {
                _timerIsGoing = true;
                StartCoroutine(Timer.StartTimer(_timeToSpawn, () => { Spawn(); _timerIsGoing = false;}));
            }
        }
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(_enemy, transform.position, Quaternion.identity);
        enemy.Spawn();
    }
}
