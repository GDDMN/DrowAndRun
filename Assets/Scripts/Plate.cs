using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
  [SerializeField] private GameObject _prefab;
  [SerializeField] private DrawWithMouse _drawPanel;

  [SerializeField] private List<GameObject> _allPlayers;

  [SerializeField] private int _unitsCount = 16;

  private void Start()
  {
    _drawPanel.OnPlayersSet += CreatePlayersOnPlate;
  }

  public void CreatePlayersOnPlate(List<Vector2> positions)
  {
    DestroyAllPlayer();

    float lenght = 0f;

    for(int i=1;i<positions.Count; i++)
    {
      lenght += Vector2.Distance(positions[i], positions[i - 1]);   
    }

    float stepLenght = lenght / _unitsCount;
    float currentPathLenght = 0f;

    for(int i=0;i<_unitsCount;i++)
    {
      Vector3 localPos = GetPoint(positions, currentPathLenght);
      currentPathLenght += stepLenght;
      GameObject player = Instantiate(_prefab, transform);
      player.transform.localPosition = localPos;
      _allPlayers.Add(player);
    }
  }

  private Vector3 GetPoint(List<Vector2> positions, float targetDistance)
  {
    for (int i = 1; i < positions.Count; i++)
    {
      Vector2 prefPointPos = positions[i-1];
      Vector2 pointPos = positions[i];
      float tempDistance = Vector2.Distance(prefPointPos, pointPos);
      float deltaDistance = targetDistance;
      targetDistance -= tempDistance;

      if(targetDistance <= 0)
      {
        float t = Mathf.InverseLerp(0, tempDistance, deltaDistance);
        Vector2 dir = pointPos - prefPointPos;
        Vector2 pos = prefPointPos + dir * t;
        Vector3 currentPos = new Vector3(pos.x, 0f, pos.y);
        return currentPos;
      }
    }

    return Vector2.zero;
  }

  private void DestroyAllPlayer()
  {
    foreach (var item in _allPlayers)
      Destroy(item);

    _allPlayers.Clear();
  }
}
