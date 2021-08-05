using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject TargetObject;

    public GameObject[] FoodPrefabs;

    public GameObject BombPrefab;

    float levelUp = 0;
    float maxLevelUp = 1f;

    bool isSpawn = false;
    bool isBombSpawn = false;

    private void Update()
    {
        if(!GameManager.instance.isGameSet)
        {
            TrySpawn();
        }
        
    }

    void TrySpawn()
    {
        if(!isSpawn)
        StartCoroutine(SpawnFood());

        if (!isBombSpawn)
            StartCoroutine(SpawnBomb());
    }


    IEnumerator SpawnFood()
    {
        isSpawn = true;

        int random = Random.Range(0, 4);

        GameObject Prefab = Instantiate(FoodPrefabs[random], transform.position, Quaternion.identity);

        FoodEat foodPrefab = Prefab.GetComponent<FoodEat>();

        foodPrefab.Target = TargetObject;

        if (GameManager.instance.score >= 3000 + 10000*levelUp && levelUp < 1f)
        {
            levelUp += 0.25f;
        }
            

        yield return new WaitForSeconds(3f - levelUp);

        isSpawn = false;

        yield return null;
    }


    IEnumerator SpawnBomb()
    {
        isBombSpawn = true;


        GameObject Prefab = Instantiate(BombPrefab, transform.position, Quaternion.identity);

        Bomb bombPrefab = Prefab.GetComponent<Bomb>();

        bombPrefab.Target = TargetObject;

       


        yield return new WaitForSeconds(8f - (levelUp * 2f));

        isBombSpawn = false;

        yield return null;
    }

}
