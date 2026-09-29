using UnityEngine;
using Unity.Cinemachine;

public class Door : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set;}
    public string DoorID { get; private set;}
    [SerializeField] PolygonCollider2D mapBoundry;
    [SerializeField] private GameObject player;
    CinemachineConfiner2D confiner;

    [SerializeField] private CinemachineCamera cCam;
    [SerializeField] private float zoneOrthographicSize = 3f;
    [SerializeField] private float x;
    [SerializeField] private float y;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        DoorID ??= GlobalHelper.GenerateUniqueID(gameObject);
    }

    private void Awake(){
        confiner = FindAnyObjectByType<CinemachineConfiner2D>();
    }

    public void Interact(){
        if(!CanInteract()) return;
        throughDoor();
    }

    public bool CanInteract(){
        return !IsOpened;
    }

    private void throughDoor()
    {
        confiner.BoundingShape2D = mapBoundry;
        confiner.InvalidateBoundingShapeCache();
        cCam.Lens.OrthographicSize = zoneOrthographicSize;

        player.transform.position = new Vector3(x, y, 0f);
    }
    
}
