using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerControl : MonoBehaviour
{
  

    [SerializeField] 
    private Vector2 mouse_Position;
    private Rigidbody2D player_rigidbody;

    [SerializeField] 
    private float move_Speed;
    void Start()
    {
        player_rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        // Mouse position
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = 10f;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreen);
        mouse_Position = new Vector2(mouseWorldPos.x, mouseWorldPos.y);
        print(mouse_Position);


        // moving the player to the mouse cursor 
        player_rigidbody.transform.position = Vector3.MoveTowards(transform.position, mouse_Position, move_Speed * Time.deltaTime);

    }


}