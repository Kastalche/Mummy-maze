
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour
{
    [SerializeField] private CharacterMovement playerMovement; //i am not sure i need this.
    private int id { get; set; }

    public Tile startPosition { get; set; }
    public Tile TargetPosition;

    public bool isBot;
    public bool isMummy;

    public void GoToStartPosition()
    {
        transform.position = startPosition.transform.position;
    }
}