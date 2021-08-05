using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public System.Action GameReset;

    float upTime;
    float currentTime;
    public Text TimeTxt;
    public Text BestTimeTxt;

    public int score = 0;
    public Text scoreTxt;
    public Text bestScoreTxt;
    public TextMeshPro scoreObjects;
    public GameObject scoreSpawn;

    [SerializeField]
    private int life = 5;
    public Text lifeTxt;
    int lifeCount = 1;
    
    public int level = 0;
    [SerializeField]
    private int maxLevel = 10;

    public bool isGameSet = true;

    public GameObject StartPanel;
    public GameObject EndPanel;
    public GameObject GamePanel;

    public AudioSource audio;

    public BestDB bestDB;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        lifeTxt.text = "Life : " + life.ToString();

        GameReset += Reset;
    }

    private void Update()
    {
        if (!GameManager.instance.isGameSet)
            CountTime();
    }

    public void GetScore(int _score)
    {
        score += _score;
        if (score < 0) score = 0;

        if(_score > 0)
        {
            TextMeshPro scoreObject = Instantiate(scoreObjects, scoreSpawn.transform.position, scoreSpawn.transform.rotation);
            scoreObject.text = "+ " + _score;

            Destroy(scoreObject, 0.85f);
        }


        scoreTxt.text = "Score : " + score.ToString();

        if (bestDB.bestScore < score)
            bestDB.bestScore = score;

        if(score >= 5000 * lifeCount)
        {
            life++;
            lifeCount++;

            lifeTxt.text = "Life : " + life.ToString();
        }    
    }

    void CountTime()
    {
        if (upTime >= 10 && level < maxLevel)
        {
            upTime = 0;
            level++;

            
        }

        currentTime += Time.deltaTime;
        upTime += Time.deltaTime;
        
        TimeTxt.text = "Time : " + ((int)currentTime).ToString();

        
    }

    public void LoseLife()
    {
        life--;
        lifeTxt.text = "Life : " + life.ToString();

        if (life <= 0)
            Death();
    }

    void Death()
    {
        isGameSet = true;

        if(bestDB.bestTime < (int)currentTime)
        bestDB.bestTime = (int)currentTime;

        bestScoreTxt.text = "Best : " + bestDB.bestScore;
        BestTimeTxt.text = "Best Time : " + bestDB.bestTime;
        SetPanel(EndPanel);
    }

    public void ResetBtn()
    {
        
        GameReset();

    }

    void SetPanel(GameObject _panel)
    {
        StartPanel.SetActive(false);
        GamePanel.SetActive(false);
        EndPanel.SetActive(false);


        _panel.SetActive(true);
    }

    private void Reset()
    {
        audio.Play();
        SceneManager.LoadScene(0);
    }

    public void StartGame()
    {
        audio.Play();
        isGameSet = false;
        SetPanel(GamePanel);
    }

    public void HelpBtn()
    {
        audio.Play();
        SceneManager.LoadScene(1);
    }
}
