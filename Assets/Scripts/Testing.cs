using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour 
{
    private Grid<TestGridObject> grid;

    private void Start() 
    {
        grid = new Grid<TestGridObject>(24, 16, 1f, new Vector3(-12, -8), (Grid<TestGridObject> g, int x, int y) => new TestGridObject(g, x, y));
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(1))
            Debug.Log(grid.GetGridObject(UtilsClass.GetMouseWorldPosition()));

        if (Input.GetMouseButtonDown(0))
        {
            TestGridObject testObject = grid.GetGridObject(UtilsClass.GetMouseWorldPosition());
            testObject?.AddValue(5);
        }
    }

    /*private void HandleClickToModifyGrid() 
    {
        if (Input.GetMouseButtonDown(0)) 
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
    }*/
}

public class TestGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;

    private Grid<TestGridObject> grid;
    private int x;
    private int y;
    private int value;

    public TestGridObject(Grid<TestGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        value += addValue;
        value = Mathf.Clamp(value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)value / MAX;
    }

    public override string ToString()
    {
        return value.ToString();
    }
}
