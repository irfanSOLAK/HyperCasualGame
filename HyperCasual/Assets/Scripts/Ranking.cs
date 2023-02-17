using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    Color _runningCompletedTextColor;
    Color _runningTextColor;

    List<GameObject> _playersSortedList;

    List<Text> _leftPanelTexts;

    private void Awake()
    {
        SetMovementParameters();
        FindPlayers();
    }

    private void SetMovementParameters()
    {
        _leftPanelTexts = GameObject.FindGameObjectWithTag("RankingPanel").GetComponentsInChildren<Text>().ToList();
        _runningCompletedTextColor = GameBehaviour.Instance.GameParameters.runningCompletedTextColor;
        _runningTextColor = GameBehaviour.Instance.GameParameters.runningTextColor;
    }

    private void FindPlayers()
    {
        _playersSortedList = new List<GameObject>();
        GameObject playersParent = GameObject.FindGameObjectWithTag("PlayersParent");

        for (int i = 0; i < playersParent.transform.childCount; i++)
        {
            _playersSortedList.Add(playersParent.transform.GetChild(i).gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        SortPlayers();
        WriteRankingToTexts();
    }

    void SortPlayers()
    {
        _playersSortedList = _playersSortedList.OrderBy(z => z.transform.position.z).ToList();
    }

    private void WriteRankingToTexts()
    {
        for (int j = 0; j < _playersSortedList.Count; j++)
        {
            _leftPanelTexts[j].text = _playersSortedList[(_playersSortedList.Count - 1) - j].name;

            if (_playersSortedList[(_playersSortedList.Count - 1) - j].GetComponent<PlayerController>().CurrentState.GetType() == typeof(FinishState))
            {
                _leftPanelTexts[j].color = _runningCompletedTextColor;
                _leftPanelTexts.RemoveAt(0);
                _playersSortedList.RemoveAt((_playersSortedList.Count - 1) - j);
            }
            else
            {
                _leftPanelTexts[j].color = _runningTextColor;
            }
        }
    }
}
