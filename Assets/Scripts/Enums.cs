using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public enum SlingshotState
    {
        Start,
        UserPulling,
        BirdFlying
    }

    public enum GameState
    {
        Start,
        BirdMovingToSlingshot,
        Playing,
        Won,
        Lost
    }


    public enum BirdState
    {
        BeforeThrown,
        Thrown
    }
    
}
