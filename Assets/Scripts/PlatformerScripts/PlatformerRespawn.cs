using UnityEngine;

//when player falls off a platform
public class PlatformerRespawn : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;

    private void OnTriggerEnter2D(Collider2D collision){
        if(!collision.CompareTag("Player")) return;

        if (collision.TryGetComponent(out PlatformerMovement player)){
            player.LoseLife();
            SoundEffectManager.Play("Fall");
            if(player.getLives() <= 0){
                return;
            }

            collision.gameObject.transform.position = new Vector3(x, y, 0f);
        }
    }

}
