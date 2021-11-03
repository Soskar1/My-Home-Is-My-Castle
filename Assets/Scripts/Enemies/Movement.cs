using UnityEngine;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _timeToMove;
    private bool _timerIsGoing = false;
    private bool _canMove = false;
    private int _currentWayPoint = 0;

    private List<Vector2> _path = new List<Vector2>();
    private Vector2 _additionalPosition = new Vector2(.5f, .5f);

    public bool CanMove { get => _canMove; set => _canMove = value; }
    public List<Vector2> Path { get => _path; set => _path = value; }

    private void Update()
    {
        if (_canMove)
        {
            if (!_timerIsGoing)
            {
                StartCoroutine(Timer.StartTimer(_timeToMove, () => { _timerIsGoing = false; MakeStep(); }));
                _timerIsGoing = true;
            }
        }
    }

    private void MakeStep()
    {
        if ((Path.Count - 2) != _currentWayPoint)
        {
            _currentWayPoint++;
            transform.position = _path[_currentWayPoint] + _additionalPosition;
        }
        else
        {
            Debug.Log("Я дошёл до конца!");
            Destroy(gameObject);
        }
    }
}
