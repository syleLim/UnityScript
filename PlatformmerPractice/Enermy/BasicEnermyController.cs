using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnermyController : MonoBehaviour
{
    /*
    *   enum makes Macro sucessive number.
    */
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    private void Update()
    {
        switch(currentState)
        {
            case State.Walking :
                UpdateWalkingState();
                break;

            case State.Knockback :
                UpdateKnockbackState();
                break;
            
            case State.Dead :
                UpdateDeadState();
                break;
        }
    }

    //----Walking State----
    private void EnterWalkingState()
    {
 
    }

    private void UpdateWalkingState()
    {

    }

    private void ExitWalkingState()
    {

    }

    //----Knocback State----
    private void EnterKnockbackState()
    {
 
    }

    private void UpdateKnockbackState()
    {

    }

    private void ExitKnockbackState()
    {

    }

    //----Dead State----
    private void EnterDeadState()
    {
 
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //----Other function----
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking :
                ExitWalkingState();
                break;
            
            case State.Knockback :
                ExitKnockbackState();
                break;

            case State.Dead :
                ExitDeadState();
                break;
        }

        switch (state)
         {
            case State.Walking :
                EnterWalkingState();
                break;
            
            case State.Knockback :
                EnterKnockbackState();
                break;

            case State.Dead :
                EnterDeadState();
                break;
         }

         currentState = state;
    }

}
