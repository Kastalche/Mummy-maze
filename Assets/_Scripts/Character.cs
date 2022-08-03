
using UnityEngine;
using UnityEngine.Events;


public class Character : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement; //i am not sure i need this.
    public Tile startPosition { get; set; }
    public bool isBot;
    public bool isMummy;

    public Tile TargetPosition;      //for mummy -> player from compare players, for players -> exit tile
    public void GoToStartPosition()
    {
        transform.position = startPosition.transform.position;
    }
    //public int id;
    //id ; isbot; input -> gridmng
}