using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (SlingShot.slingshotState == SlingshotState.Idle && GameManager.CurrentGameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeDragStarted = Time.time;
                dragSpeed = 0f;
                previousPosition = Input.mousePosition;
            }

            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
            {
                Vector3 input = Input.mousePosition;
                float deltaX = (previousPosition.x - input.x)  * dragSpeed;
                float deltaY = (previousPosition.y - input.y) * dragSpeed;
                
                float newX = Mathf.Clamp(transform.position.x + deltaX, 0, 13.36336f);
                float newY = Mathf.Clamp(transform.position.y + deltaY, 0, 2.715f);
                
                transform.position = new Vector3(
                    newX,
                    newY,
                    transform.position.z);

                previousPosition = input;
                if(dragSpeed < 0.1f) dragSpeed += 0.005f;
                Debug.Log(dragSpeed);
            }
        }
    }

    private float dragSpeed = 0.01f;
    private float timeDragStarted;
    private Vector3 previousPosition = Vector3.zero;

    public SlingShot SlingShot;
}
