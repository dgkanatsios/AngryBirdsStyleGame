using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;

public class GameManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        gameState = GameState.Start;
        slingshot.enabled = false;
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));

        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
    }




    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.BirdMovingToSlingshot:
                break;
              
                 
            case GameState.Playing:
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition();
                    gameState = GameState.BirdMovingToSlingshot;
                }
                break;
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
            default:
                break;
        }
    }



    private bool AllPigsDestroyed()
    {
        return Pigs.All(x => x == null);
    }

    private void AnimateCameraToStartPosition()
    {
        //animate the camera to start
        Camera.main.transform.positionTo(Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10,
            cameraFollow.StartingPosition).
            setOnCompleteHandler((x) =>
                        {
                            cameraFollow.IsFollowing = false;
                            if(AllPigsDestroyed())
                            {
                                gameState = GameState.Won;
                            }
                            //animate the next bird, if available
                            else if (currentBirdIndex == Birds.Count - 1)
                            {
                                //no more birds, go to finished
                                gameState = GameState.Lost;
                            }
                            else
                            {
                                slingshot.slingshotState = SlingshotState.Start;
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                            }
                        });
    }

    void AnimateBirdToSlingshot()
    {
        gameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.positionTo(Vector2.Distance(Birds[currentBirdIndex].transform.position / 10, slingshot.BirdWaitPosition.transform.position) / 10,
            slingshot.BirdWaitPosition.transform.position).
                setOnCompleteHandler((x) =>
                        {
                            x.complete();
                            x.destroy();
                            gameState = GameState.Playing;
                            slingshot.enabled = true;
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }


    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks)
        {
            if (item != null && item.rigidbody2D.velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        foreach (var item in Birds)
        {
            if (item != null && item.rigidbody2D.velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        foreach (var item in Pigs)
        {
            if (item != null && item.rigidbody2D.velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        return true;
    }

    void OnGUI()
    {
        switch (gameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(100, 100, 100, 100), "Tap the screen to start");
                break;
            case GameState.Won:
                GUI.Label(new Rect(100, 100, 100, 100), "You won! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(100, 100, 100, 100), "You lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }

    public CameraFollow cameraFollow;
    int currentBirdIndex;
    public SlingShot slingshot;
    private GameState gameState = GameState.Start;
    private List<GameObject> Bricks;
    private List<GameObject> Birds;
    private List<GameObject> Pigs;
}
