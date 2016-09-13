using UnityEngine;
using System.Collections;

public class ActionMaster
{
    public enum Action
    {
        Tidy,
        Reset,
        EndTurn,
        EndGame
    };

    private int[] bowls = new int[21];
    private int bowlIndex;

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10)
        {
            throw new UnityException("Invalid pins entered.");
        }
        
        if (pins == 10) //strike => end turn
        {
            if (bowlIndex > 1) //if it's not the 1st turn 
            {
                if (previousTurnIsStrike()) //if the previous turn was a strike
                {
                    bowls[bowlIndex - 2] += pins;
                }
            }
            bowls[bowlIndex] = pins;
            bowlIndex += 2;
            return Action.EndTurn;
        }

        if (bowlIndex % 2 == 0) //1st frame of turn => tidy
        {
            if (bowlIndex > 1) //if it's not the 1st turn and to prevent array out of bounds exception
            {
                if (previousTurnIsStrike()) //if the previous turn was a strike
                {
                    bowls[bowlIndex - 2] += pins;
                }
                else if ((bowls[bowlIndex - 2] + bowls[bowlIndex - 1]) == 10) //if the previous turn was a spare
                {
                    bowls[bowlIndex - 1] += pins;
                }
            }
            
            bowls[bowlIndex] = pins;
            bowlIndex++;
            return Action.Tidy;
        }

        if (bowlIndex%2 != 0) //2nd frame of turn => end turn
        {
            if (bowlIndex > 1) //if it's not the 1st frame
            {
                if (bowls[bowlIndex - 3] >= 10) //if the previous turn was a strike
                {
                    bowls[bowlIndex - 3] += pins;
                }
            }
            
            bowls[bowlIndex] = pins;
            bowlIndex++;
            return Action.EndTurn;
        }

        throw new UnityException("No action to return");
    }

    private bool previousTurnIsStrike()
    {
        return bowls[bowlIndex - 2] == 10;
    }

    public int[] getBowls()
    {
        return bowls;
    }
}
