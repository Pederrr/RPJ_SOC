using UnityEngine;
using UnityEngine.SceneManagement;

public class MainTower : MonoBehaviour
{
    private static float health;
    private static int wave;

    //Currencies
    private static int energy;
    private static int CPU;
    private static int RAM;

    [SerializeField] private int startHP = 10; 
    [SerializeField] private int startEnergy = 0;
    [SerializeField] private int startCPU = 0;
    [SerializeField] private int startRAM = 0;

    private static int level;

    [SerializeField] private int maxLevel = 5;
    private static int maxLvl;

    public static void SetWave(int _wave)
    {
        wave = _wave;
    }

    public static int GetWave()
    {
        return wave;
    }

    //Money
    public static void AddEnergy(int amount)
    {
        energy += amount;
    }

    public static void SubtractEnergy(int amount)
    {
        energy -= amount;
    }

    public static int ShowEnergy()
    {
        return energy;
    }

    //Microchips
    public static void AddCPU(int amount)
    {
        CPU += amount;
    }

    public static void SubtractCPU(int amount)
    {
        CPU -= amount;
    }

    public static int ShowCPU()
    {
        return CPU;
    }

    //Plastic
    public static void AddRAM(int amount)
    {
        RAM += amount;
    }

    public static void SubtractRAM(int amount)
    {
        RAM -= amount;
    }

    public static int ShowRAM()
    {
        return RAM;
    }

    public static float ShowHealth()
    {
        return health;
    }

    public static int GetLevel()
    {
        return level;
    }

    public static int GetMaxLevel()
    {
        return maxLvl;
    }

    private void Awake()
    {
        health = startHP;
        energy = startEnergy;
        CPU = startCPU;
        RAM = startRAM;
        level = 1;
        maxLvl = maxLevel;
    }

    public static void GetHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            AudioManager.Instance.Play("theme", false);

            int highScore = PlayerPrefs.GetInt("highScore", 0);
            Spawner spawn = GameObject.Find("Spawner").GetComponent<Spawner>(); //viem že by sa to takto nemalo
            
            if (wave > highScore && spawn.GetEndless()) //highscore mením iba ak ide o endless mód
            {
                PlayerPrefs.SetInt("highScore", wave);
            }

            SceneManager.LoadScene("GameOver");
        }
    }

    public void Upgrade()//OnMouseDown
    {
        GameManager.SetSelectedObject(null);
        GameManager.SetActiveUI(null);
        if (level < maxLevel)
        {
            if (energy >= 50 * level && RAM >= 50 * level && CPU >= 50 * level)
            {
                energy -= 50 * level;
                RAM -= 50 * level;
                CPU -= 50 * level;
                level++;
                health += 2;  
                Shop.SetMaxLevel(level); //Zvýšenie max levlu veží
            }
        }
    }
}
