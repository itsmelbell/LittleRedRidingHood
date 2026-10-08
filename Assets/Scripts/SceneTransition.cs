using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

//moving from one minigame to another
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

    //upon entering designated area, will flag whether it's a return point or not
    //keep track of save position if there is not one
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
            SoundEffectManager.Play("Enter");
            SceneManager.LoadScene(SceneToGo);
        }
    }

    //called by button click on homescreen
    public void firstSpawn(){
        SoundEffectManager.Play("Enter");
        SceneManager.LoadScene("OverWorld");
        GlobalHelper.getSugar(false);
        GlobalHelper.getEgg(false);
        GlobalHelper.getApple(false);
    }

    //called by button click in menu, end screen
    public void restartGame(){
        SceneManager.LoadScene("StartScreen");
        SoundEffectManager.Play("Enter");
        GlobalHelper.getSugar(false);
        GlobalHelper.getEgg(false);
        GlobalHelper.getApple(false);
        PauseController.SetPause(false);
    }

    //after each minigame
    public void ReturnToOverWorld(){
        isReturning = true;
        SceneManager.LoadScene("OverWorld");
        SoundEffectManager.Play("Enter");
    }

    //if the camera needth be changed
    public void SetCamera(){
        confiner.BoundingShape2D = mapBoundry;
        confiner.InvalidateBoundingShapeCache();
    }
}
