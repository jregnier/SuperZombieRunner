using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Text continueText;
    public Text scoreText;

    private bool gameStarted;
    private TimeManager timeManager;
    private GameObject player;
    private GameObject floor;
    private Spawner spawner;
    private float blinkTime = 0f;
    private bool blink;
    private float timeElapsed = 0f;
    private float bestTime = 0f;
    private bool beatBestTime;

    public void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        floor = GameObject.Find("Foreground");
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
    }   

    // Use this for initialization
    void Start()
    {
        var floorHeight = floor.transform.localScale.y;

        var pos = floor.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
        floor.transform.position = pos;

        spawner.active = false;

        Time.timeScale = 0;

        continueText.text = "Press Any Button To Start";

        bestTime = PlayerPrefs.GetFloat("BestTime", bestTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted != true && Time.timeScale == 0)
        {
            if (Input.anyKeyDown)
            {
                timeManager.ManipulateTime(1, 1f);
                ResetGame();
            }
        }

        if (!gameStarted)
        {
            blinkTime++;

            if ((blinkTime % 40) == 0)
            {
                blink = !blink;
            }

            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);

            scoreText.color = beatBestTime ? Color.red : Color.white;
            scoreText.text = "TIME: " + this.FormatTime(timeElapsed) + System.Environment.NewLine + "BEST: " + this.FormatTime(bestTime);
        }
        else
        {
            timeElapsed += Time.deltaTime;
            scoreText.text = "TIME: " + this.FormatTime(timeElapsed);
        }
    }

    void OnPlayerKilled()
    {
        spawner.active = false;

        var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
        playerDestroyScript.DestroyCallback -= this.OnPlayerKilled;

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        timeManager.ManipulateTime(0, 5.5f);
        gameStarted = false;

        continueText.text = "Press Any Button To Restart";

        if (timeElapsed > bestTime)
        {
            bestTime = timeElapsed;

            PlayerPrefs.SetFloat("BestTime", bestTime);

            beatBestTime = true;
        }
    }

    void ResetGame()
    {
        spawner.active = true;

        player = GameObjectUtility.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsToUnits) / 2 + 100, 0));

        var playerDestroyScript = player.GetComponent<DestroyOffScreen>();
        playerDestroyScript.DestroyCallback += this.OnPlayerKilled;

        gameStarted = true;

        continueText.canvasRenderer.SetAlpha(0);

        timeElapsed = 0f;
        beatBestTime = false;
        scoreText.color = Color.white;
    }

    string FormatTime(float value)
    {
        TimeSpan t = TimeSpan.FromSeconds(value);

        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }
}
