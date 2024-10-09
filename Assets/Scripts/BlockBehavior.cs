using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{

    public GridObject position;
    public String type;

    public int x;
    public int y;

    // Start is called before the first frame update
    void Start()
    {
        x = position.gridPosition.x;
        y = position.gridPosition.y;
        GridManager.reference.Grid[x-1,y-1] = this.gameObject;
    }

    public virtual bool CanMove(int xMove, int yMove) {
        return false;
    }

    public void MoveTo(int xMove, int yMove) {
        GridManager.reference.Grid[x-1,y-1] = null;
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
        if (GridManager.reference.Grid[checkX-1,checkY-1] != null) { 
            if (!GridManager.reference.Grid[checkX-1,checkY-1].CompareTag("Player") &&
                GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().type.Equals("Sticky")) {
                GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove);
            }
        }
        
    }

}
