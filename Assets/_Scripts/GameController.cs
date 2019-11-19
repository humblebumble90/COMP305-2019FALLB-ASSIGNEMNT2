using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    [Header("ScoreBoard")]
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _lives;
    [SerializeField]
    private int _scores;

    public Text HPLabel;
    public Text ScoreLabel;
    public Text LivesLabel;

    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            HPLabel.text = "HP: " + _hp.ToString();

        }
    }

    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            LivesLabel.text = "Lives: " + _lives.ToString();
        }
    }

    public int Score
    {
        get
        {
            return _scores;
        }
        set
        {
            _scores = value;
            ScoreLabel.text = "Score: " + _scores.ToString();
        }
    }

    [Header("BGM")]
    public AudioSource bGM;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 100;
        Lives = 5;
        Score = 0;
        bGM.volume = 0.5f;
        bGM.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        realtimeDisplaying();
        if(_hp <= 0)
        {
            DecreaseALive();
        }
    }
    
    void realtimeDisplaying()
    {
        HPLabel.text = "HP: " + _hp.ToString();
        LivesLabel.text = "Lives: " + _lives.ToString();
        ScoreLabel.text = "Score: " + _scores.ToString();
    }

    void DecreaseALive()
    {
            _lives -= 1;
            _hp = 100;
    }
}
