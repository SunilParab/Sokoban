using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string type = "Player";
    public int x = 1;
    public int y = 1;
    public int maxX;
    public int maxY;

    public GridObject position;

    // Start is called before the first frame update
    void Start()
    {
        x = position.gridPosition.x;
        y = position.gridPosition.y;
        GridManager.reference.Grid[x-1,y-1] = this.gameObject;

        maxX = GridManager.reference.Grid.GetLength(0);
        maxY = GridManager.reference.Grid.GetLength(1);
    }

    // Update is called once per frame
    void Update()
    {

        int xChange = 0;
        int yChange = 0;

        if (Input.GetKeyDown(KeyCode.A)) {
            xChange--;
        }
        if (Input.GetKeyDown(KeyCode.D)) {
            xChange++;
        }

        if (xChange == 0) {
            if (Input.GetKeyDown(KeyCode.W)) {
                yChange--;
            }
            if (Input.GetKeyDown(KeyCode.S)) {
                yChange++;
            }
        }

        if (x + xChange < 1) {
            xChange = x - 1;
        } else if (x + xChange > maxX) {
            xChange = maxX - x;
        }

        if (y + yChange < 1) {
            yChange = y - 1;
        } else if (y + yChange > maxY) {
            yChange = maxY - y;
        }

        if (xChange != 0 || yChange != 0) {
            if (GridManager.reference.Grid[x + xChange-1,y + yChange-1] == null ||
                GridManager.reference.Grid[x + xChange-1,y + yChange-1].GetComponent<BlockBehavior>().CanMove(xChange,yChange)) {
                StickyCheck(xChange, yChange);
                if (GridManager.reference.Grid[x-1,y-1] == this.gameObject) {
                    GridManager.reference.Grid[x-1,y-1] = null;
                }
                position.gridPosition = new Vector2Int(x+xChange,y+yChange);
                GridManager.reference.Grid[x+xChange-1,y+yChange-1] = this.gameObject;
                x = position.gridPosition.x;
                y = position.gridPosition.y;
            }
        }

    }

    public void StickyCheck(int xMove, int yMove) {
        ClingyCheck(xMove,yMove);
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
            if (!GridManager.reference.Grid[checkX-1,checkY-1].CompareTag("Player")) {
                if (GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().type.Equals("Sticky")) {
                    GridManager.reference.Grid[checkX-1,checkY-1].GetComponent<BlockBehavior>().CanMove(xMove,yMove);
                }
            }
        }
        
    }

    public void ClingyCheck(int xMove, int yMove) {

        if (x - xMove < 1 || x - xMove > maxX) {
            return;
        }

        if (y - yMove < 1 || y - yMove > maxY) {
            return;
        }


        if (GridManager.reference.Grid[x-xMove-1,y-yMove-1] != null) { 
            if (!GridManager.reference.Grid[x-xMove-1,y-yMove-1].CompareTag("Player") &&
                (GridManager.reference.Grid[x-xMove-1,y-yMove-1].GetComponent<BlockBehavior>().type.Equals("Clingy"))) {
                    GridManager.reference.Grid[x-xMove-1,y-yMove-1].GetComponent<Clingy>().PullTo(xMove,yMove);
            }
        }
    }

}
