using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerControl : MonoBehaviour
{

    //other scripts 

    public SpawnManager spawnManager;
    [SerializeField]
    private Vector2 mouse_Position;

    // Perfect Growth Variables
    [SerializeField] private float currentMass = 1f;
    [SerializeField] private float massPerDot = 0.5f;
    [SerializeField] private float visualMultiplier = 1f;


    [SerializeField]
    private float CameraGrowthAmount = 0.03f;

    private Rigidbody2D player_rigidbody;

    //public int x = spawnManager.max_x;
    //public int y = spawnManager.max_y;
    [SerializeField]
    private float move_Speed;

    [SerializeField]
    float cameraZ;

    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();

    }
    private void FixedUpdate()
    {

        // Mouse position
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = 10f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouse_Position = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

        // moving the player to the mouse cursor 
        player_rigidbody.transform.position = Vector3.MoveTowards(transform.position, mouse_Position, move_Speed * Time.fixedDeltaTime);
    }
    // Update is called once per frame  
    void Update()
    {

    }


    // other obecjt destoryer
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dot"))
        {
            float playerScale = transform.localScale.x;
            float dotScale = other.transform.localScale.x;

            // check if the player is begger than dot +10%
            if (playerScale >= dotScale * 1.10f)
            {
                float distance = Vector2.Distance(transform.position, other.transform.position);
                float playerRadius = playerScale / 2f;
                // check if the player reach the center of the dot 
                if (distance <= playerRadius)
                {
                    // scale the player up
                    currentMass += massPerDot;
                    float calculatedScale = Mathf.Sqrt(currentMass) * visualMultiplier;
                    transform.localScale = new Vector3(calculatedScale, calculatedScale, 1f);

                    // zoom out the camera 
                    Vector3 newCameraPos = Camera.main.transform.position;
                    newCameraPos.z -= CameraGrowthAmount;
                    Camera.main.transform.position = newCameraPos;

                    // return dot to pool instead of destroying it
                    SpawnManager.instance.ReturnDotToPool(other.gameObject);
                }
            }
            // change the layer depend on size
            else if (dotScale > playerScale)
            {
                SpriteRenderer dotRenderer = other.GetComponent<SpriteRenderer>();
                if (dotRenderer != null)
                {
                    dotRenderer.sortingOrder = 3;
                }
            }
        }
    }

    // after player exit the dot return the layer to default
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Dot"))
        {
            SpriteRenderer dotRenderer = other.GetComponent<SpriteRenderer>();
            if (dotRenderer != null)
            {
                dotRenderer.sortingOrder = 1;
            }
        }
    }

}