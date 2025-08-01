using UnityEngine;

public class BattleBoardManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform playerBoardParent;
    public Transform enemyBoardParent;

    public int columns = 5;
    public int rows = 4;
    public float spacing = 1.1f;

    void Start()
    {
        GenerateBoard(playerBoardParent, false); // Abajo
        GenerateBoard(enemyBoardParent, true);   // Arriba
    }

    void GenerateBoard(Transform parent, bool isEnemy)
    {
        float offsetX = -(columns - 1) * spacing / 2f;
        float offsetY = -(rows - 1) * spacing / 2f;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 cellPos = new Vector3(x * spacing + offsetX, 0, y * spacing + offsetY);
                if (isEnemy) cellPos.z += 2.5f; // Elevar tablero enemigo hacia arriba
                else cellPos.z -= 2.5f;         // Bajar tablero jugador

                GameObject cell = Instantiate(cellPrefab, parent);
                cell.transform.localPosition = cellPos;
            }
        }
    }
}
