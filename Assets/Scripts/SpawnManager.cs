using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    Color[] dotsColorList = new Color[]
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        Color.magenta
    };


    [SerializeField]
    private GameObject dot_prefab;


    [SerializeField]
    private float max_dot_scaling = 2f;

    // dot spawn area
    public float max_x = 500;
    public float max_y = 280;
    [SerializeField]
    private int maxDotsAllowed = 10000;


    // active dots
    static public int activeDotsCounter;

    // dots destroyed counter
    static public int destroyedDotsCounter;


    [SerializeField]
    private TextMeshProUGUI uiEatedDotCounter;


    [SerializeField]
    private TextMeshProUGUI uiActiveDotsCounter;

    [SerializeField]
    private float dotsSpawnSpeed = 0.01f;


    [SerializeField]
    private float growthAmount = 0.05f;

    [SerializeField]
    private Transform playerRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Object Pool Queue
    private Queue<GameObject> dotPool = new Queue<GameObject>();
    void Awake()
    {
        instance = this;

        // made them at the awaky of the game
        for (int i = 0; i < maxDotsAllowed; i++)
        {
            GameObject dot = Instantiate(dot_prefab, transform);
            dot.SetActive(false);
            dotPool.Enqueue(dot);
        }
    }

    void Start()
    {
        activeDotsCounter = 0;
        destroyedDotsCounter = 0;
        InvokeRepeating(nameof(tryToSpawnADot), 0.5f, dotsSpawnSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        uiEatedDotCounter.text = destroyedDotsCounter.ToString();
        uiActiveDotsCounter.text = activeDotsCounter.ToString();
    }


    // spawn dots method
    void tryToSpawnADot()
    {
        if (activeDotsCounter >= maxDotsAllowed || dotPool.Count == 0) return;

        GameObject dot = dotPool.Dequeue();

        Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-max_x, max_x), UnityEngine.Random.Range(-max_y, max_y));
        dot.transform.position = randomPos;

        SpriteRenderer colorizer = dot.GetComponent<SpriteRenderer>();
        colorizer.color = randomizeDotsColor();
        colorizer.sortingOrder = 1;
        float randomScale = randomizeDotsScale(max_dot_scaling);
        dot.transform.localScale = new Vector3(randomScale, randomScale, 1f);

        dot.SetActive(true);
        activeDotsCounter += 1;
    }

    // Return dot to pool
    public void ReturnDotToPool(GameObject dot)
    {
        dot.SetActive(false);
        dotPool.Enqueue(dot);
        activeDotsCounter -= 1;
        destroyedDotsCounter += 1;
    }

    // caller of spawn dots method



    // Scaling the dots
    private float randomizeDotsScale(float max_scaling)
    {
        float randomScale = UnityEngine.Random.Range(0.5f, max_scaling);
        return randomScale;
    }

    // Coloring the dots
    private Color randomizeDotsColor()
    {
        int randomScale = UnityEngine.Random.Range(0, dotsColorList.Length);
        return dotsColorList[randomScale];
    }

}//editing the original prefab ==== //Instantiate(dot_prefab, randomPos, Quaternion.identity);//SpriteRenderer colorizer = dot_prefab.GetComponent<SpriteRenderer>();//if (activeDotsCounter >= maxDotsAllowed) return; this idea way better than my if  > 5