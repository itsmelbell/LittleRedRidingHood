using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    
    private void Start()
    {
        if (SceneTransition.hasReturnPosition)
        {
            print("has return position");
            transform.position = SceneTransition.returnPosition;
            SceneTransition.hasReturnPosition = false;

            SceneTransition transition =
                FindAnyObjectByType<SceneTransition>();

            if (transition != null)
                transition.SetCamera();
        }
    }
}
