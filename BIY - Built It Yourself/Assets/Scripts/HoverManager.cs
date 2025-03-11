using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public Color hoverColor;
    public Color validPlacementColor;
    public Color invalidPlacementColor;

    private GridManager gridManager;
    private GameObject lastHoveredObject;
    private Color originalColor;
    private GridValueManager gridValueManager;
    private GameManager gameManager;
    void Start()
    {
        gridManager = GridManager.Instance;
        gridValueManager = FindObjectOfType<GridValueManager>();
        gameManager = FindAnyObjectByType<GameManager>();

        if (gridManager == null)
        {
            Debug.LogError("GridManager instance is not set. Please ensure GridManager is initialized.");
            return;
        }
    }

    // Update is called once per frame

    /// <summary>
    /// Permet de v�rifier si une grille est survoler et changer ca couleur 
    /// Auteur :Oli
    /// </summary>
    void Update()
    {
        if (!gridValueManager.previewMode && !gameManager.TirdPerson )
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.CompareTag("Ground"))
                {
                    if (lastHoveredObject != null && lastHoveredObject != hitObject)
                    {
                        ResetHoveredObjectColor();
                    }

                    if (lastHoveredObject != hitObject)
                    {
                        changeHoverColor(hitObject, true);
                        lastHoveredObject = hitObject;
                    }
                }
                else
                {
                    changeHoverColor(hitObject, false);
                }
            }
            else
            {
                if (lastHoveredObject != null)
                {
                    ResetHoveredObjectColor();
                }
            }
        }
    }


    /// <summary>
    /// Permet de r�initialiser la couleur de la grille 
    /// Auteur : Oli
    /// </summary>
    private void ResetHoveredObjectColor()
    {
        if (lastHoveredObject != null)
        {
            Renderer hitRenderer = lastHoveredObject.GetComponent<Renderer>();
            hitRenderer.material.color = originalColor; 
            lastHoveredObject = null;
        }
    }

    private bool CheckIfValidPlacement(GameObject hitObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector2Int gridPosition = gridManager.WorldToGridPosition(hitInfo.point);
            Tile tile = gridManager.GetTile(gridPosition.x, gridPosition.y);
            if (tile.Type == Tile.TileType.Ground)
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }

    private void changeHoverColor(GameObject hitObject, bool b)
    {
        Renderer hitRenderer = hitObject.GetComponent<Renderer>();
        originalColor = hitRenderer.material.color;
        bool isValidPlacement = CheckIfValidPlacement(hitObject);

        if (isValidPlacement)
            hitRenderer.material.color = validPlacementColor;
        else
            hitRenderer.material.color = invalidPlacementColor;
    }
}
