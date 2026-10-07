using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    
    private void Start()
    {
        if (!SceneTransition.isReturning) return;
        
            print("has return position");
            transform.position = SceneTransition.returnPosition;
            SceneTransition.hasReturnPosition = false;
            SceneTransition.isReturning = false;

            SceneTransition transition =
                FindAnyObjectByType<SceneTransition>();

            if (transition != null)
                transition.SetCamera();
        
    }
}
