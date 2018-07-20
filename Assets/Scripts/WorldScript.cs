using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour
{

    public GameObject planet;
    public GameObject earth;
    public GameObject canon;
    public GameObject multPowerUp;
    public GameObject shieldPowerUp;
    public static float vertExtent;
    public static float horzExtent;
    public static GameObject player;
    public static GameObject weapon;
    public GameObject explosionAudio;
    public static AudioSource explAudio;
    AudioSource audioFile;
    GameObject sl;
    GameObject go;
    GUIText gameOver;
    GUIText scoreLabel;
    public static int score = 0;
    public static int mult = 1;
    int enemyCounter;
    int powerUpCounter;
    int maxEnemyCounter;
    int maxPowerUpCounter;


    // Use this for initialization
    void Start()
    {
        vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
        horzExtent = vertExtent * Screen.width / Screen.height;
        player = (GameObject)Instantiate(earth, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
        weapon = (GameObject)Instantiate(canon, new Vector3(0, 50, 0), Quaternion.Euler(0, 0, 0));
        enemyCounter = 0;
        maxEnemyCounter = 300;
        maxPowerUpCounter = 2000;
        powerUpCounter = maxPowerUpCounter;
        sl = new GameObject("Score");
        go = new GameObject("GameOver");
        scoreLabel = (GUIText)sl.AddComponent(typeof(GUIText));
        scoreLabel.text = "Score: " + score;
        scoreLabel.fontSize = 20;
        scoreLabel.transform.position = new Vector3(0.08f, 0.92f, 0);
        gameOver = (GUIText)go.AddComponent(typeof(GUIText));
        gameOver.enabled = false;
        gameOver.fontSize = 20;
        gameOver.transform.position = new Vector3(0.3f, 0.5f, 0);
        explAudio = explosionAudio.GetComponent<AudioSource>();
        explAudio.volume = MetaData.EffectVolume;
        audioFile = GetComponent<AudioSource>();
        audioFile.volume = MetaData.BackgroundVolume;
        audioFile.Play();
    }

    // Update is called once per frame
    void Update()
    {
        scoreLabel.text = "Score: " + score;
        if (score > 15000)
            maxEnemyCounter = 100;
        else if (score > 5000)
            maxEnemyCounter = 200;
        if (!audioFile.isPlaying)
            audioFile.Play();
        if (Input.GetKeyDown("r"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.Escape))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if (player == null)
        {
            gameOver.text = "You got " + score + " Points! Press R to retry! Press ESCAPE to go back!";
            gameOver.enabled = true;
        }
        else
        {
            if (enemyCounter == 0)
            {
                GenerateEnemy();
                enemyCounter = maxEnemyCounter;
            }
            else
                enemyCounter--;
            if (powerUpCounter == 0)
            {
                GeneratePowerUp();
                powerUpCounter = maxPowerUpCounter;
            }
            else
                powerUpCounter--;
        }
    }

    void GenerateEnemy()
    {
        int side = Random.Range(0, 4);
        GameObject enemy = null;
        switch (side)
        {
            case 0:
                enemy = (GameObject)Instantiate(planet, new Vector3(-horzExtent - 300, Random.Range(-vertExtent, vertExtent), 0), Quaternion.Euler(0, 0, 0));
                break;
            case 1:
                enemy = (GameObject)Instantiate(planet, new Vector3(horzExtent + 300, Random.Range(-vertExtent, vertExtent), 0), Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                enemy = (GameObject)Instantiate(planet, new Vector3(Random.Range(-horzExtent, horzExtent), -vertExtent - 300, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                enemy = (GameObject)Instantiate(planet, new Vector3(Random.Range(-horzExtent, horzExtent), vertExtent + 300, 0), Quaternion.Euler(0, 0, 0));
                break;
        }
        enemy.GetComponent<Renderer>().material.color = Color.red;
        enemy.GetComponent<Rigidbody2D>().AddForce((player.transform.position - enemy.transform.position).normalized);
    }

    void GeneratePowerUp()
    {
        int side = Random.Range(0, 2);
        switch (side)
        {
            case 0:
                Instantiate(multPowerUp, new Vector3(Random.Range(-horzExtent + 50, horzExtent - 50), Random.Range(-vertExtent + 50, vertExtent - 50), 0), Quaternion.Euler(0, 0, 0));
                break;
            case 1:
                Instantiate(shieldPowerUp, new Vector3(Random.Range(-horzExtent + 50, horzExtent - 50), Random.Range(-vertExtent + 50, vertExtent - 50), 0), Quaternion.Euler(0, 0, 0));
                break;
        }
    }
}
