using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;


public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string SceneToGo;
    [SerializeField] private Transform returnPoint; 
    public static Vector3 returnPosition;
    public static bool hasReturnPosition;
    [SerializeField] private PolygonCollider2D mapBoundry;
    CinemachineConfiner2D confiner;
    public static bool isReturning;

    private void Awake()
    {
        confiner = FindAnyObjectByType<CinemachineConfiner2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player")){
            if(returnPoint != null){
                returnPosition = returnPoint.position;
                print("Saved return position" + returnPosition);
                hasReturnPosition = true;
            } else {
                print("no position saved");
            }
            
            isReturning = false;
            SceneManager.LoadScene(SceneToGo);
        }
    }

    public void firstSpawn(){
        SceneManager.LoadScene("OverWorld");
        GlobalHelper.getSugar(false);
        GlobalHelper.getEgg(false);
        GlobalHelper.getApple(false);
    }

    public void restartGame(){
        SceneManager.LoadScene("StartScreen");
        GlobalHelper.getSugar(false);
        GlobalHelper.getEgg(false);
        GlobalHelper.getApple(false);
        PauseController.SetPause(false);
    }

    public void ReturnToOverWorld(){
        isReturning = true;
        SceneManager.LoadScene("OverWorld");
    }

    public void SetCamera(){
        confiner.BoundingShape2D = mapBoundry;
        confiner.InvalidateBoundingShapeCache();
    }
}
