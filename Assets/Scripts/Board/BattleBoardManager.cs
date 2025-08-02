using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleBoardManager : MonoBehaviour
{
    [Header("Prefabs y Referencias")]
    public GameObject cellPrefab;
    public GameObject unitPrefab; // 👈 Prefab de unidad
    public Button generarUnidadButton; // 👈 Botón de UI asignado en Inspector

    [Header("Contenedores de Tablero")]
    public Transform playerBoardParent;
    public Transform enemyBoardParent;

    [Header("Dimensiones")]
    public int columns = 5;
    public int rows = 4;
    public float spacing = 1.1f;

    // Lista de celdas del jugador para encontrar vacías
    private List<Transform> playerCells = new List<Transform>();

    void Start()
    {
        GenerateBoard(playerBoardParent, false); // Tablero jugador
        GenerateBoard(enemyBoardParent, true);   // Tablero enemigo

        if (generarUnidadButton != null)
            generarUnidadButton.onClick.AddListener(GenerarUnidadAleatoria);
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
                if (isEnemy) cellPos.z += 2.5f;
                else cellPos.z -= 2.5f;

                GameObject cell = Instantiate(cellPrefab, parent);
                cell.transform.localPosition = cellPos;

                if (!isEnemy)
                    playerCells.Add(cell.transform); // Guardar celdas del jugador
            }
        }
    }

    void GenerarUnidadAleatoria()
    {
        // Filtrar celdas vacías (sin hijos)
        var celdasLibres = playerCells.Where(c => c.childCount == 0).ToList();
        if (celdasLibres.Count == 0)
        {
            Debug.Log("No hay celdas libres.");
            return;
        }

        // Escoger una aleatoria
        var celda = celdasLibres[Random.Range(0, celdasLibres.Count)];

        // Instanciar unidad dentro de esa celda
        GameObject unidad = Instantiate(unitPrefab, celda);
        unidad.transform.localPosition = Vector3.zero; // Centrar en la celda
        unidad.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);

        // (Opcional) Puedes inicializar stats aquí si usas un script como "CombatUnit"
        // unidad.GetComponent<CombatUnit>().Inicializar(1, 1); // Nivel 1, Puntos 1
    }
}
