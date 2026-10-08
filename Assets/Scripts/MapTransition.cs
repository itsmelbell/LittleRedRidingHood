using UnityEngine;
using Unity.Cinemachine;

//Within the overworld, switch from one area to the next
public class MapTransition : MonoBehaviour
{
   [SerializeField] PolygonCollider2D mapBoundry;
   CinemachineConfiner2D confiner;
   [SerializeField] Direction direction;
   [SerializeField] float additivePos = 2f;

   enum Direction {Up, Down, Left, Right}

   private void Awake(){
    confiner = FindAnyObjectByType<CinemachineConfiner2D>();
   }

    //walk into a map boundry, change camera
   private void OnTriggerEnter2D(Collider2D collision){
    if (collision.gameObject.CompareTag("Player")){
        confiner.BoundingShape2D = mapBoundry;
        confiner.InvalidateBoundingShapeCache();
        UpdatePlayerPosition(collision.gameObject);
    }
   }

    //move player
   private void UpdatePlayerPosition(GameObject player){
    Vector3 newPos = player.transform.position;

    switch(direction){
        case Direction.Up:
            newPos.y += additivePos;
            break;
        case Direction.Down:
            newPos.y -= additivePos;
            break;
        case Direction.Left:
            newPos.x -= additivePos;
            break;
        case Direction.Right:
            newPos.x += additivePos;
            break;
    }
    player.transform.position = newPos;
   }
}
