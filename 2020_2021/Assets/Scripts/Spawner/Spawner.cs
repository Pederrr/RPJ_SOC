using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private bool tutorial;
    [SerializeField] private bool endless;
    [SerializeField] private EnemiesDictionary[] enemies;
    private int waveMaxDanger = 1;
    private int enemyLevel = 1;
    [SerializeField] private Wave[] waves;

    private bool sendingWave;
    private bool newWave;

    private int waveIndex;

    private static float timeToNextWave;

    private static int activeEnemies;

    [SerializeField] private GameObject skipButton;

    public static void DecrementActiveEnemies()
    {
        activeEnemies--;
    }

    public static void AddActiveEnemies(int count)
    {
        activeEnemies += count;
    }
    
    public static int GetActiveEnemies()
    {
        return activeEnemies;
    }

    public static float GetTimeToNextWave()
    {
        return timeToNextWave;
    }

    public bool GetEndless()
    {
        return endless;
    }

    public void SkipToNextWave() //funkcia pre button
    {
        //pridať nejakú odmenu --  timeToNextWave * k - zaokruhliť a prida5 k aktuálnym peniazom
        timeToNextWave = 0f;
        newWave = true;
        skipButton.SetActive(false);
    }

    private void Awake()
    {
        sendingWave = false;
        newWave = false;
        waveIndex = 0;
        timeToNextWave = 20f;
    }

    private void Update()
    {
        if (newWave)
        {
            if (!sendingWave)
            {
                if (endless)
                {
                    StartCoroutine(EndlessSpawn());
                }
                else
                {
                    if (waveIndex >= waves.Length)
                    {
                        if (activeEnemies <= 0 && !endless)
                        {
                            if (tutorial)
                            {
                                GameObject tutorial = GameObject.Find("TutorialManager");
                                Tutorial script = tutorial.GetComponent<Tutorial>();
                                script.EndTutorial();
                            }
                            else
                            {
                                GameManager.Win(waveIndex);
                            }
                            this.enabled = false;
                        }
                    }
                    else
                    {
                        StartCoroutine(Spawn());
                    }
                }
            }
        }
        else if (timeToNextWave > 0)
        {
            timeToNextWave -= Time.deltaTime;
            if(timeToNextWave <= 0)
            {
                timeToNextWave = 0f; //kvoli zobrazovaniu v UI
                newWave = true;
                skipButton.SetActive(false);
            }

            if (activeEnemies <= 0)
            {
                if (waveIndex >= waves.Length && !endless)
                {
                    if (tutorial)
                    {
                        GameObject tutorial = GameObject.Find("TutorialManager");
                        Tutorial script = tutorial.GetComponent<Tutorial>();
                        script.EndTutorial();
                    }
                    else
                    {
                        if (endless)
                        {
                            GameManager.Win(waveIndex);
                        }
                        else
                        {
                            GameManager.Win(0);
                        }
                    }
                    this.enabled = false;
                }
            }
        }
    }

    private void SpawnEnemy(GameObject enemy, int level)
    {
        GameObject newEnemy = Pools.GetFromPool(enemy.name, enemy);
        newEnemy.name = enemy.name;
        newEnemy.transform.position = transform.position;
        newEnemy.transform.rotation = transform.rotation;
        newEnemy.GetComponent<Enemy>().SetLevel(level);
        newEnemy.SetActive(true);
    }

    private IEnumerator Spawn()
    {
        MainTower.SetWave(waveIndex + 1);
        sendingWave = true;

        Wave wave = waves[waveIndex];

        int current = 0;

        foreach (int count in wave.GetCount())
        {
            for (int i = 0; i < count; i++)
            {
                activeEnemies++;

                SpawnEnemy(wave.GetEnemies()[current], 1);

                yield return new WaitForSeconds(1f / wave.GetRate()[current]);
            }
            current++;
        }

        sendingWave = false;
        newWave = false;

        timeToNextWave = wave.GetTimeToNextWave();
        skipButton.SetActive(true);

        waveIndex++;
        if (tutorial)
        {
            GameObject tutorial = GameObject.Find("TutorialManager");
            Tutorial script = tutorial.GetComponent<Tutorial>();
            script.EndWave();
        }
    }

    private IEnumerator EndlessSpawn()
    {
        MainTower.SetWave(waveIndex + 1);
        sendingWave = true;

        if (waveIndex != 0 && waveIndex % 10 == 0) //Každých 10 waves sa zvýši level enemies
        {
            enemyLevel++;
        }

        int totalDanger = 0;
        while (waveMaxDanger > totalDanger)
        {
            int index = Random.Range(0, enemies.Length);

            while (totalDanger + enemies[index].dangerIndex > waveMaxDanger) 
                //aby som zabezpečil, že to bude presne, alebo menej
            {
                index--;
                if (index == 0 || index == 1)
                {
                    index = Random.Range(0, 1); //aby nemal jeden enemy vacsiu sancu na spawn
                }
            }

            activeEnemies++;
            SpawnEnemy(enemies[index].enemy, enemyLevel);
            totalDanger += enemies[index].dangerIndex;
            yield return new WaitForSeconds(1f);
        }

        waveMaxDanger += 1;

        sendingWave = false;
        newWave = false;

        timeToNextWave = 10;
        skipButton.SetActive(true);

        waveIndex++;
    }
}
