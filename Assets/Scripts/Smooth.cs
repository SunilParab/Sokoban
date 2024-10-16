using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : BlockBehavior
{

    public override bool CanMove(int xMove, int yMove) {

        if (moving) {
            return false;
        }

        if (x + xMove < 1) {
            return false;
        } else if (x + xMove > maxX) {
            return false;
        }

        if (y + yMove < 1) {
            return false;
        } else if (y + yMove > maxY) {
            return false;
        }

        moving = true;
        if (GridManager.reference.Grid[x + xMove-1, y + yMove-1] == null) {
            StickyCheck(xMove, yMove);
            MoveTo(xMove,yMove);
            moving = false;
            return true;
        } else {
            if (GridManager.reference.Grid[x + xMove-1, y + yMove-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove)) {
                StickyCheck(xMove, yMove);
                MoveTo(xMove,yMove);
                moving = false;
                return true;
            } else {
                moving = false;
                return false;
            }
        }

    }

}
