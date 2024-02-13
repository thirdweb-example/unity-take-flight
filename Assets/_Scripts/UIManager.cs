using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _distanceText;

    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private TMP_Text _gameOverDistanceText;

    [SerializeField]
    private TMP_Text _rankText;

    private void Awake()
    {
        _gameOverPanel.SetActive(false);
    }

    private void Start()
    {
        CharacterManager.Instance.OnDeath.AddListener(ShowGameOverPanelAsync);
    }

    private void Update()
    {
        _distanceText.text = CharacterManager.Instance.DistanceTravelled.ToString("F0");
    }

    private async void ShowGameOverPanelAsync(float distanceTravelled)
    {
        _gameOverPanel.SetActive(true);
        _gameOverDistanceText.text = distanceTravelled.ToString("F0");
        int rank = await BlockchainManager.Instance.GetRank();
        _rankText.text = $"Global Rank: {rank}";
    }

    public async void SubmitScore()
    {
        _rankText.text = $"Global Rank: ...";
        await BlockchainManager.Instance.SubmitScore(CharacterManager.Instance.DistanceTravelled);
        int rank = await BlockchainManager.Instance.GetRank();
        _rankText.text = $"Global Rank: {rank}";
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
