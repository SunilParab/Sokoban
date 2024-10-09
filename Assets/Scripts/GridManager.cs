using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject[,] Grid;
    public GridMaker maker;

    public static GridManager reference;

    void Awake() {
        Grid = new GameObject[(int)maker.dimensions.x,(int)maker.dimensions.y];
        /*for (int i = 0; i < Grid.GetLength(0); i++) {
            for (int j = 0; j < Grid.GetLength(1); j++) {
                Grid[i,j] = "empty";
            }
        }*/
        reference = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
