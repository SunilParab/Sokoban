using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{

    public GridObject position;
    public String type;

    public int x;
    public int y;
    public int maxX;
    public int maxY;
    //Lets other objects know this one is currently trying to move
    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        x = position.gridPosition.x;
        y = position.gridPosition.y;
        GridManager.reference.Grid[x-1,y-1] = this.gameObject;

        maxX = GridManager.reference.Grid.GetLength(0);
        maxY = GridManager.reference.Grid.GetLength(1);
    }

    public virtual bool CanMove(int xMove, int yMove) {
        return false;
    }

    public void MoveTo(int xMove, int yMove) {
        if (GridManager.reference.Grid[x-1,y-1] == this.gameObject) {
            GridManager.reference.Grid[x-1,y-1] = null;
        }
        position.gridPosition = new Vector2Int(x+xMove,y+yMove);
        GridManager.reference.Grid[x+xMove-1,y+yMove-1] = this.gameObject;

        x = position.gridPosition.x;
        y = position.gridPosition.y;
    }

    public void StickyCheck(int xMove, int yMove) {
        StickyCheckTile(x+1,y,xMove,yMove);
        StickyCheckTile(x-1,y,xMove,yMove);
        StickyCheckTile(x,y+1,xMove,yMove);
        StickyCheckTile(x,y-1,xMove,yMove);
    }

    public void StickyCheckTile(int checkX, int checkY, int xMove, int yMove) {

        if (checkX < 1 || checkX > maxX) {
            return;
        }

        if (checkY < 1 || checkY > maxY) {
            return;
        }


        if (GridManager.reference.Grid[checkX-1,checkY-1] != null) { 
            if (!GridManager.reference.Grid[checkX-1,checkY-1].CompareTag("Player") &&
                GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().type.Equals("Sticky")) {
                GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove);
            }
        }
        
    }

}
