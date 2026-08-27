using System;
using TMPro;
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
    [SerializeField] 
    private int maxDotsAllowed = 100;


    // active dots
    static public int activeDotsCounter;
    
    // dots destroyed counter
    static public int destroyedDotsCounter;


    [SerializeField] 
    private TextMeshProUGUI uiEatedDotCounter;


    [SerializeField] 
    private TextMeshProUGUI uiActiveDotsCounter;

    [SerializeField]
    private float dotsSpawnSpeed = 0.05f;


    
    [SerializeField] 
    private float growthAmount = 0.05f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (activeDotsCounter >= maxDotsAllowed) return;

            
            
            Vector2 randomPos = new Vector2(UnityEngine.Random.Range(-max_x, max_x), UnityEngine.Random.Range(-max_y, max_y));
            GameObject newDot = Instantiate(dot_prefab, randomPos, Quaternion.identity);
            SpriteRenderer colorizer = newDot.GetComponent<SpriteRenderer>();
            colorizer.color = randomizeDotsColor();
            float randomScale = randomizeDotsScale(max_dot_scaling);
            newDot.transform.localScale = new Vector3(randomScale, randomScale, 1f);
            activeDotsCounter += 1;
    }

    // caller of spawn dots method
    


    // Scaling the dots
    private float randomizeDotsScale( float max_scaling) 
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
    
}


//editing the original prefab ==== 
//Instantiate(dot_prefab, randomPos, Quaternion.identity);
//SpriteRenderer colorizer = dot_prefab.GetComponent<SpriteRenderer>();

//if (activeDotsCounter >= maxDotsAllowed) return; this idea way better than my if  > 5 