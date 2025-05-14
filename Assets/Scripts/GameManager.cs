using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    instance = go.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText; 
    [SerializeField] private GameObject playButton; 
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject getReady;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
            
        instance = this;
        DontDestroyOnLoad(gameObject);

        Pause();
    }

    public void GameOver() 
    {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        getReady.SetActive(false);          // Just the 1st Time

        Time.timeScale = 1;
        player.enabled = true;

        // Assure to destroy all the GO Pillars existing on the Scene
        Pillars[] pillars = FindObjectsByType<Pillars>(FindObjectsSortMode.None);
        foreach(Pillars pillar in pillars)
            Destroy(pillar.gameObject);

    }
    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }
}
