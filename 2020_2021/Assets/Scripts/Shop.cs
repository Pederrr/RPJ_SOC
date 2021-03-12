using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] towersNotStatic;
    private static GameObject[] towers = new GameObject[6];

    private static string[] towerDescriptions = new string[] {
        "Simple but powerfull tower that shoots single projectile at an enemy",
        "Shoots grenades that deal damage to multiple enemies",
        "Slows down enemies and slowly damages them",
        "Generates resources - RAM",
        "Generates resources - CPU",
        "Shoots bursts on enemies"
    };


    private static List<bool> unlocked; // podla indexu viem, či mozem pozadovanu vezu stavat

    private static int maxLevel = 1; //viem max level

    //tower
    private int[] priceIndex0level0 = new int[]{ 0, 10, 10 }; //money, CPU, RAM
    private int[] priceIndex0level1 = new int[]{ 0, 20, 20 };
    private int[] priceIndex0level2 = new int[]{ 20, 20, 20 };
    private int[] priceIndex0level3 = new int[]{ 30, 25, 25 };
    private int[] priceIndex0level4 = new int[]{ 40, 35, 35 };

    //mortar
    private int[] priceIndex1level0 = new int[]{ 0, 10, 10 }; //money, plastic, microchips
    private int[] priceIndex1level1 = new int[]{ 0, 20, 20 };
    private int[] priceIndex1level2 = new int[]{ 20, 20, 20 };
    private int[] priceIndex1level3 = new int[]{ 30, 25, 25 };
    private int[] priceIndex1level4 = new int[]{ 40, 35, 35 };

    //laser
    private int[] priceIndex2level0 = new int[]{ 0, 10, 10 }; //money, plastic, microchips
    private int[] priceIndex2level1 = new int[]{ 0, 20, 20 };
    private int[] priceIndex2level2 = new int[]{ 20, 20, 20 };
    private int[] priceIndex2level3 = new int[]{ 30, 25, 25 };
    private int[] priceIndex2level4 = new int[]{ 40, 35, 35 };

    //ram farm
    private int[] priceIndex3level0 = new int[]{ 50, 0, 0 };
    private int[] priceIndex3level1 = new int[]{ 70, 0, 0 };
    private int[] priceIndex3level2 = new int[]{ 70, 20, 20 };
    private int[] priceIndex3level3 = new int[]{ 80, 30, 30 };
    private int[] priceIndex3level4 = new int[]{ 90, 50, 50 };

    //cpu farm
    private int[] priceIndex4level0 = new int[]{ 50, 0, 0 };
    private int[] priceIndex4level1 = new int[]{ 70, 0, 0 };
    private int[] priceIndex4level2 = new int[]{ 70, 20, 20 };
    private int[] priceIndex4level3 = new int[]{ 80, 30, 30 };
    private int[] priceIndex4level4 = new int[]{ 90, 50, 50 };

    //minigun
    private int[] priceIndex5level0 = new int[]{ 0, 10, 10 }; //money, plastic, microchips
    private int[] priceIndex5level1 = new int[]{ 0, 20, 20 };
    private int[] priceIndex5level2 = new int[]{ 20, 20, 20 };
    private int[] priceIndex5level3 = new int[]{ 30, 25, 25 };
    private int[] priceIndex5level4 = new int[]{ 40, 35, 35 };

    private int[][] pricesIndex0 = new int[5][]; // ceny vsetkych upgradov pre jednu budovu
    private int[][] pricesIndex1 = new int[5][];
    private int[][] pricesIndex2 = new int[5][];
    private int[][] pricesIndex3 = new int[5][];
    private int[][] pricesIndex4 = new int[5][];
    private int[][] pricesIndex5 = new int[5][];

    private static int[][][] prices = new int[6][][]; //6 = počet veží

    public static int[] GetPrice(int index, int level)
    {
        return prices[index][level];
    }

    public static GameObject GetFromTowers(int index)
    {
        return towers[index];
    }

    public static string GetDesription(int index)
    {
        return towerDescriptions[index];
    }

    public static void Unlock(int index)
    {
        unlocked[index] = true;
    }

    public static bool IsUnlocked(int index)
    {
        return unlocked[index];
    }

    public static int GetMaxLevel()
    {
        return maxLevel;
    }

    public static void SetMaxLevel(int maxLvl)
    {
        maxLevel = maxLvl;
    }

    private void Awake()
    {
        maxLevel = 1;

        unlocked = new List<bool> { true, true, true, true, true, true }; //ak by som chcel niektorú vežu uzamknúť

        for (int i = 0; i < towersNotStatic.Length; i++)
        {
            towers[i] = (towersNotStatic[i]);
        }

        pricesIndex0[0] = priceIndex0level0;
        pricesIndex0[1] = priceIndex0level1;
        pricesIndex0[2] = priceIndex0level2;
        pricesIndex0[3] = priceIndex0level3;
        pricesIndex0[4] = priceIndex0level4;

        pricesIndex1[0] = priceIndex1level0;
        pricesIndex1[1] = priceIndex1level1;
        pricesIndex1[2] = priceIndex1level2;
        pricesIndex1[3] = priceIndex1level3;
        pricesIndex1[4] = priceIndex1level4;

        pricesIndex2[0] = priceIndex2level0;
        pricesIndex2[1] = priceIndex2level1;
        pricesIndex2[2] = priceIndex2level2;
        pricesIndex2[3] = priceIndex2level3;
        pricesIndex2[4] = priceIndex2level4;

        pricesIndex3[0] = priceIndex3level0;
        pricesIndex3[1] = priceIndex3level1;
        pricesIndex3[2] = priceIndex3level2;
        pricesIndex3[3] = priceIndex3level3;
        pricesIndex3[4] = priceIndex3level4;

        pricesIndex4[0] = priceIndex4level0;
        pricesIndex4[1] = priceIndex4level1;
        pricesIndex4[2] = priceIndex4level2;
        pricesIndex4[3] = priceIndex4level3;
        pricesIndex4[4] = priceIndex4level4;

        pricesIndex5[0] = priceIndex5level0;
        pricesIndex5[1] = priceIndex5level1;
        pricesIndex5[2] = priceIndex5level2;
        pricesIndex5[3] = priceIndex5level3;
        pricesIndex5[4] = priceIndex5level4;

        prices[0] = pricesIndex0;
        prices[1] = pricesIndex1;
        prices[2] = pricesIndex2;
        prices[3] = pricesIndex3;
        prices[4] = pricesIndex4;
        prices[5] = pricesIndex5;
    }
}
