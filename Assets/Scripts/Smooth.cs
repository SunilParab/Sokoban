using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : BlockBehavior
{

    public override bool CanMove(int xMove, int yMove) {

        if (GridManager.reference.Grid[x + xMove-1, y + yMove-1] == null) {
            StickyCheck(xMove, yMove);
            MoveTo(xMove,yMove);
            return true;
        } else {
            if (GridManager.reference.Grid[x + xMove-1, y + yMove-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove)) {
                StickyCheck(xMove, yMove);
                MoveTo(xMove,yMove);
                return true;
            } else {
                return false;
            }
        }

    }

}
