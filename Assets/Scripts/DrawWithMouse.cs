using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DrawWithMouse : MonoBehaviour
{
  private Vector3 _previousPosition;
  private Image _image;

  [SerializeField] private float _minDistance = 0.1f;
  [SerializeField] private GameObject _pointPrefab;
  [SerializeField] private RectTransform _canvas;
  [SerializeField] private List<GameObject> _points;
  
  public event Action<List<Vector2>> OnPlayersSet;
  

  private void Start()
  {
    _previousPosition = transform.position;
    _image = GetComponent<Image>();
  }

  private void Update()
  {
    if(Input.GetMouseButton(0))
    {
      Vector2 currentPosition = new Vector2();

      RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas,
                                                              Input.mousePosition,
                                                              null, out currentPosition);

      float minHeight = _image.rectTransform.localPosition.y - _image.rectTransform.sizeDelta.y / 2;
      float maxHeight = _image.rectTransform.localPosition.y + _image.rectTransform.sizeDelta.y / 2;
      float minWight = _image.rectTransform.localPosition.x - _image.rectTransform.sizeDelta.x / 2;
      float maxWight = _image.rectTransform.localPosition.x + _image.rectTransform.sizeDelta.x / 2;


      bool IsNotInBorders = (currentPosition.x > maxWight) ||
                            (currentPosition.x < minWight) ||
                            (currentPosition.y > maxHeight) ||
                            (currentPosition.y < minHeight);

      if (IsNotInBorders)
        return;

      currentPosition = Input.mousePosition;

      if (Vector2.Distance(currentPosition, _previousPosition) > _minDistance)
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
      positions.Add(item.transform.localPosition / 10f);

    return positions;
  }

  private void DestroyAllPoints()
  {
    foreach (var item in _points)
      Destroy(item);

    _points.Clear();
  }

}
