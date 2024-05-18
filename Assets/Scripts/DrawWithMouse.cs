using System.Collections.Generic;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
  private Vector3 _previousPosition;

  [SerializeField] private float _minDistance = 0.1f;
  [SerializeField] private GameObject _pointPrefab;

  [SerializeField] private List<Vector2> _points;

  private void Start()
  {;
    _previousPosition = transform.position;
  }

  private void Update()
  {
    if(Input.GetMouseButton(0))
    {
      Vector2 currentPosition = Input.mousePosition;
      
      if(Vector2.Distance(currentPosition,_previousPosition) > _minDistance)
      {
        CreateNewPoint(currentPosition);
        _previousPosition = currentPosition;
      }
    }
  }

  private void CreateNewPoint(Vector2 position)
  {
    GameObject point = Instantiate(_pointPrefab, position, Quaternion.identity);
    point.transform.SetParent(transform);

    _points.Add(point.transform.position);
  }

}
