using System.Collections.Generic;
using System;
using UnityEngine;

public class DrawWithMouse : MonoBehaviour
{
  private Vector3 _previousPosition;

  [SerializeField] private float _minDistance = 0.1f;
  [SerializeField] private GameObject _pointPrefab;

  [SerializeField] private List<GameObject> _points;
  
  public event Action<List<Vector2>> OnPlayersSet;

  private void Start()
  {
    _previousPosition = transform.position;
  }

  private void Update()
  {
    if(Input.GetMouseButton(0))
    {
      Vector2 currentPosition = Input.mousePosition;

      Debug.Log(currentPosition);

      if(Vector2.Distance(currentPosition,_previousPosition) > _minDistance)
      {
        CreateNewPoint(currentPosition);
        _previousPosition = currentPosition;
      }
    }
    if(Input.GetMouseButtonUp(0))
    {
      OnPlayersSet?.Invoke(GetPointPos());
      DestroyAllPoints();
    }
    
  }

  private void CreateNewPoint(Vector2 position)
  {
    GameObject point = Instantiate(_pointPrefab, position, Quaternion.identity);
    point.transform.SetParent(transform);

    _points.Add(point);
  }

  private List<Vector2> GetPointPos()
  {
    List<Vector2>  positions = new List<Vector2>();


    foreach (var item in _points)
      positions.Add(item.transform.localPosition.normalized);

    return positions;
  }

  private void DestroyAllPoints()
  {
    foreach (var item in _points)
      Destroy(item);

    _points.Clear();
  }

}
