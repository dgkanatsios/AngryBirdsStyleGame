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
                previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
            {
                Vector3 input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float delta = (previousPosition.x - input.x) * Time.deltaTime * 30;
                float newX = Mathf.Clamp(transform.position.x + delta, 0, 13.36336f);

                transform.position = new Vector3(
                    newX,
                    transform.position.y,
                    transform.position.z);

                previousPosition = input;
                Debug.Log(input.x + " " + previousPosition.x);
            }

            //else if (Input.GetMouseButtonUp(0))
            //    previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


           
        }
    }


    private float timeDragStarted;
    private Vector3 previousPosition = Vector3.zero;

    public SlingShot SlingShot;
}
