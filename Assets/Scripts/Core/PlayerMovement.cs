using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader; // reads input
    [SerializeField] private PlayerData playerData;   // holds move speed, future stats

    private Rigidbody2D rb;

    public void setInputReader(InputReader reader)
    {
        inputReader = reader;
    }
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 move = inputReader.MovementInput.normalized * playerData.moveSpeed;
        rb.linearVelocity = move;
    }
}
