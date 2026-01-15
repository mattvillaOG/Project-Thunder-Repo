using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    RacingMovement player; //connects to the player movement script

    [SerializeField] GameObject rockPrefab, powerupPrefab; // spawns rocks and powerups

    [Header("Spawners")]
    [SerializeField] float rockSpawnTime, powerupSpawnTime; //sets the amount of time between object spawns
    [SerializeField] float spawnRandomVariance; //potential extra time added to spawns (making them slightly random)
    [SerializeField] float spawnDistanceZ; //how far away to spawn the objects
    [SerializeField] float spawnRadiusX; //how far from center to spawn

    [Header("Starting Rocks")] //for the initial rocks that spawn in when the scene loads
    [SerializeField] float startDist; //how far away start-rocks spawn
    [SerializeField] float minimumStartRange; //shortest distance between rocks at the start
    [SerializeField] float maximumStartRange; //longest distance between rocks at the start
    [SerializeField] float distanceMultPowerups; //distance multiplier for start-spawned powerups

    public int currentHighScore = 0;

    public static GameManager instance; // instance of game manager is publically accessible

    void Start()
    {
        // singleton code
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // only one game manager! it never gets destroyed
            DontDestroyOnLoad(this.gameObject);
            instance = this;
            //SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //spawn rocks at the start
        float zPos = startDist;

        while (zPos > -320)
        {
            float xPos = -spawnRadiusX + (Random.Range(0, spawnRadiusX * 2)); //get a random x position within range

            //create a rock!
            GameObject newRock = Instantiate(rockPrefab);
            newRock.transform.position = new Vector3(xPos, -2.8f, zPos);

            zPos -= Random.Range(minimumStartRange, maximumStartRange);
        }
        
        //spawn powerups at the start
        zPos = startDist - minimumStartRange * distanceMultPowerups;

        while (zPos > -320)
        {
            float xPos = -spawnRadiusX + (Random.Range(0, spawnRadiusX * 2)); //get a random x position within range

            //create a powerup!
            GameObject newPowerup = Instantiate(powerupPrefab);
            newPowerup.transform.position = new Vector3(xPos, -2.8f, zPos);

            zPos -= Random.Range(minimumStartRange * distanceMultPowerups, maximumStartRange * distanceMultPowerups);
        }
    }

    private void Update()
    {
        //quit the game!
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainLevel")
        {
            // begin spawn coroutines
            StartCoroutine(SpawnRock(rockSpawnTime + Random.Range(0, spawnRandomVariance)));
            StartCoroutine(SpawnPowerup(powerupSpawnTime + Random.Range(0, spawnRandomVariance)));
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<RacingMovement>();
        }
        else
        {
            player = null;
            StopAllCoroutines(); // don't spawn objects outside the main scene!
        }
    }

    IEnumerator SpawnRock(float time)
    {
        yield return new WaitForSeconds(time);

        float xPos = -spawnRadiusX + (Random.Range(0, spawnRadiusX * 2)); //get a random x position within range
        float zPos = player.gameObject.transform.position.z + spawnDistanceZ; //get a position above the player

        //create a rock!
        GameObject newRock = Instantiate(rockPrefab);
        newRock.transform.position = new Vector3(xPos, -2.8f, zPos);

        //run the coroutine again at a new random time
        StartCoroutine(SpawnRock(rockSpawnTime + Random.Range(0, spawnRandomVariance)));
    }

    IEnumerator SpawnPowerup(float time)
    {
        yield return new WaitForSeconds(time);

        float xPos = -spawnRadiusX + (Random.Range(0, spawnRadiusX * 2)); //get a random x position within range
        float zPos = player.gameObject.transform.position.z + spawnDistanceZ; //get a position in front of the player

        //create a lightning bolt!
        GameObject newPowerup = Instantiate(powerupPrefab);
        newPowerup.transform.position = new Vector3(xPos, -2.8f, zPos);

        //run the coroutine again at a new random time
        StartCoroutine(SpawnPowerup(powerupSpawnTime + Random.Range(0,spawnRandomVariance)));
    }

    public void SetHighScore(int highScore)
    {
        currentHighScore = highScore;
    }
}
