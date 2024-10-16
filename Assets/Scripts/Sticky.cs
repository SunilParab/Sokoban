using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : BlockBehavior
{
    
    public override bool CanMove(int xMove, int yMove) {

        if (moved) {
            return false;
        }

        if (moving) {
            return true;
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
            StickyGrab(xMove, yMove);
            MoveTo(xMove,yMove);
            moving = false;
            return true;
        } else {
            if (GridManager.reference.Grid[x + xMove-1, y + yMove-1].CompareTag("Player") ||
                GridManager.reference.Grid[x + xMove-1, y + yMove-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove)) {
                StickyGrab(xMove, yMove);
                MoveTo(xMove,yMove);
                moving = false;
                return true;
            } else {
                moving = false;
                return false;
            }
        }

    }

    public void StickyGrab(int xMove, int yMove) {
        StickyGrabTile(x+1,y,xMove,yMove);
        StickyGrabTile(x-1,y,xMove,yMove);
        StickyGrabTile(x,y+1,xMove,yMove);
        StickyGrabTile(x,y-1,xMove,yMove);
    }

    public void StickyGrabTile(int checkX, int checkY, int xMove, int yMove) {

        if (checkX < 1 || checkX > maxX) {
            return;
        }

        if (checkY < 1 || checkY > maxY) {
            return;
        }


        if (GridManager.reference.Grid[checkX-1,checkY-1] != null) { 
            if (!GridManager.reference.Grid[checkX-1,checkY-1].CompareTag("Player")) {
                if (GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().type.Equals("Clingy")) {
                    GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<Clingy>().PullTo(xMove,yMove);
                } else {
                    GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove);
                }
            }
        }
        
    }

}
