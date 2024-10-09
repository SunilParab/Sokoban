using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public string type = "Player";
    public int x = 1;
    public int y = 1;
    public int maxX = 10;
    public int maxY = 5;

    public GridObject position;

    // Start is called before the first frame update
    void Start()
    {
        x = position.gridPosition.x;
        y = position.gridPosition.y;
        GridManager.reference.Grid[x-1,y-1] = this.gameObject;
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
                GridManager.reference.Grid[x-1,y-1] = null;
                position.gridPosition = new Vector2Int(x+xChange,y+yChange);
                GridManager.reference.Grid[x+xChange-1,y+yChange-1] = this.gameObject;
                x = position.gridPosition.x;
                y = position.gridPosition.y;
            }
        }

    }
}
