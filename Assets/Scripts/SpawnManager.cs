using System;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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
    public float max_x = 50;
    public float max_y = 28;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    void Update()
    {
        Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-max_x, max_x), UnityEngine.Random.Range(-max_y, max_y));
        Instantiate(dot_prefab, randomPos, Quaternion.identity);
        SpriteRenderer colorizer = dot_prefab.GetComponent<SpriteRenderer>();
        colorizer.color = randomizeDotsColor();
        float randomScale = randomizeDotsScale(max_dot_scaling);
        dot_prefab.transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }

    // Scaling the dots
    private float randomizeDotsScale( float max_scaling) 
    {
        float randomScale = UnityEngine.Random.Range(0.5f, max_scaling);
        return randomScale;
    }

    // Coloring the dots
    private Color randomizeDotsColor() 
    {
        int randomScale = UnityEngine.Random.Range(0, 5);
        return dotsColorList[randomScale];
    }
    
}
