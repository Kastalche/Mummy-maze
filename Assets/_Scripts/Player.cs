
using UnityEngine;
using UnityEngine.Events;


public class Player : MonoBehaviour
{ 
    [SerializeField] private PlayerMovement playerMovement;
    public Tile startPosition { get; set; }
    //id ; isbot; input -> gridmng
}