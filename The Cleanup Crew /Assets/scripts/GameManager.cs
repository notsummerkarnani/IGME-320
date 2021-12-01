using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> enemies;
    public int numEnemies;
    public GameObject enemyPrefab;

    List<GameObject> items;
    public int numItems;
    public GameObject itemPrefab;

    List<GameObject> bins;
    public int numBins;
    public GameObject binPrefab;

    public static bool endScene = false;
    public static float[] lanes = new float[4] { -3.0f, -1.0f, 1.0f, 3.0f };

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        items = new List<GameObject>();
        bins = new List<GameObject>();
        endScene = false;

        numEnemies = (int)Mathf.Sqrt(score.totalScore) - score.highestDeposit;
        for (int i= 0; i < numEnemies; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemies.Add(enemy);
        }

        numItems = (int)Mathf.Sqrt(score.totalScore) - score.highestDeposit;
        for(int i = 0; i < numItems; i++)
        {
            GameObject item = Instantiate(itemPrefab);
            items.Add(item);
        }

        numBins = 1;
        for (int i = 0; i < numBins; i++)
        {
            GameObject bin = Instantiate(binPrefab);
            bins.Add(bin);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endScene)
        {
            gameObject.GetComponent<SceneController>().ChangeScene();
        }
        
        GenerateObjects(enemyPrefab, enemies, numEnemies);
        GenerateObjects(itemPrefab, items, numItems);
        GenerateObjects(binPrefab, bins, numBins);

        if (numEnemies < 20)
        {
            numEnemies = (int)Mathf.Sqrt(score.totalScore) - score.highestDeposit;
            numItems = numEnemies;
        }
        
    }

    void GenerateObjects(GameObject _prefab, List<GameObject> _objects, int _numObjects)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            if (_objects[i] == null)
            {
                _objects[i] = Instantiate(_prefab);
            }
        }
        if (_objects.Count <= _numObjects)
        {
            GameObject myObject = Instantiate(_prefab);
            _objects.Add(myObject);
        }
    }
}
