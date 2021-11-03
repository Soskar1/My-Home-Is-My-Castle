using Map.Generation;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected MapGenerator _map;
    protected MapData _mapData;
    protected Vector2 _additionalPosition = new Vector2(.5f, .5f);
}
