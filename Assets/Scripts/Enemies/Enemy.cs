using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private Movement _movement;

    public void Spawn()
    {
        _mapData = _map.data;
        transform.position = _mapData.path[0] + _additionalPosition;
        _movement.Path = _mapData.path;
        _movement.CanMove = true;
    }
}
