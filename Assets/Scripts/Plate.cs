using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
  [SerializeField] private GameObject _prefab;
  [SerializeField] private DrawWithMouse _drawPanel;

  [SerializeField] private List<GameObject> _allPlayers;

  private void Start()
  {
    _drawPanel.OnPlayersSet += CreatePlayersOnPlate;
  }

  public void CreatePlayersOnPlate(List<Vector2> positions)
  {
    DestroyAllPlayer();

    foreach (var pos in positions)
    {
      Vector3 localPos = new Vector3(pos.x, 0f, pos.y);
      GameObject player = Instantiate(_prefab, transform);
      player.transform.localPosition = localPos;
      _allPlayers.Add(player);
    }
  }

  private void DestroyAllPlayer()
  {
    foreach (var item in _allPlayers)
      Destroy(item);

    _allPlayers.Clear();
  }
}
