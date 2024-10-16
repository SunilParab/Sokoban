using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clingy : BlockBehavior
{
    public override bool CanMove(int xMove, int yMove) {
        if (moved) {
            return false;
        }
        if (moving) {
            return true;
        }
        return false;
    }

    public void PullTo(int xMove, int yMove) {
        if (moving || moved) {
            return;
        }

        if (x + xMove < 1 || x + xMove > maxX) {
            return;
        }

        if (y + yMove < 1 || y + yMove > maxY) {
            return;
        }

        moving = true;
        if (GridManager.reference.Grid[x + xMove-1, y + yMove-1] == null) {
            StickyCheck(xMove, yMove);
            MoveTo(xMove,yMove);
            moving = false;
            return;
        } else {
            if (GridManager.reference.Grid[x + xMove-1, y + yMove-1].CompareTag("Player") ||
                GridManager.reference.Grid[x + xMove-1, y + yMove-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove)) {
                StickyCheck(xMove, yMove);
                MoveTo(xMove,yMove);
                moving = false;
                return;
            } else {
                moving = false;
                return;
            }
        }
    }

}
