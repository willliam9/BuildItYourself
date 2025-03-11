using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;
using UnityEngine.Analytics;

public class GridValueManager : MonoBehaviour
{
    GridManager gridManager;
    GameManager gameManager;

    private Camera mainCamera;
    private GameObject objectToPlacePrefab;
    private GameObject arrowPrefab;
    private GameObject previewObject = null;
    private GameObject[] arrows = new GameObject[4];
    private MenuBuildingUI2 menuBuilding;
    private float currentRotation;
    private float arrowOffset = 1; // la distance entre les fleches et l'item 
    public bool previewMode = false;
    private bool isHoveredMenu = false; // Indicateur du survol

    public GameObject parentObject;

    public GameObject menuBuild;

    void Start()
    {
        mainCamera = Camera.main;
        gridManager = GridManager.Instance;
        gameManager = FindAnyObjectByType<GameManager>();
        menuBuilding = FindAnyObjectByType<MenuBuildingUI2>();

        arrowPrefab = gameManager.PrefabArrow;
        CreateArrows();

        //setObectToPlace("Building_Coffee Shop", Tile.TileType.Building, Tile.TileSubType.Commercial);

    }

    void Update()
    {
        HandleMouseInput();

        if (Input.GetKeyDown(KeyCode.Space) && previewMode)
        {
            ConfirmPlacement();
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            TryGetGridValue();
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            CancelObectToPlace();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            DeleteObectTile();
        }
    }
    
    /// <summary>
    /// Permet de canceler le positionnement 
    /// Auteur: Oli
    /// </summary>
    private void CancelObectToPlace()
    {
        HideArrows();
        Destroy(previewObject);
        previewObject = null;
        previewMode = false;
    }

    /// <summary>
    /// Permet de capturer le clique du joueur a une position 
    /// Auteur : Oli
    /// </summary>
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && objectToPlacePrefab != null && !isHoveredMenu)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                ArrowDirection arrowDirection = hit.collider.GetComponent<ArrowDirection>();
                if (arrowDirection != null)
                {
                    AdjustOrientationBasedOnArrow(arrowDirection);
                    return;
                }

                Vector2Int pos = gridManager.WorldToGridPosition(hit.point);

                currentRotation = gridManager.GetTile(pos.x, pos.y).Rot.y;

                PlaceOrPreviewObject(gridManager.GetCellWorldPosition(pos.x, pos.y) + new Vector3(gridManager.cellSize * 0.5f, 0, gridManager.cellSize * 0.5f));
            }
        }
    }

    /// <summary>
    /// Permet de creer les 4 fleches qui vont �tre positionner autour de l'element 
    /// Auteur : Oli
    /// </summary>
    private void CreateArrows()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        for (int i = 0; i < 4; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, Vector3.zero, Quaternion.LookRotation(directions[i], Vector3.up));
            arrow.SetActive(false);
            arrows[i] = arrow;

            arrow.AddComponent<ArrowDirection>().direction = (ArrowDirection.Direction)i;
        }
    }

    /// <summary>
    /// Permet de cacher les fleches 
    /// Auteur : oli
    /// </summary>
    private void HideArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Permet de placer l'instance du preview sur le monde (Ces juste un preview et n'est pas enregistrer dans la grid)
    /// Auteur : Oli
    /// </summary>
    /// <param name="position"></param>
    private void PlaceOrPreviewObject(Vector3 position)
    {


        if (CheckIfSomethingOnPos(position))
        {
            if (previewObject == null)
            {
                previewObject = Instantiate(objectToPlacePrefab, position, Quaternion.Euler(0, currentRotation, 0));
                UpdateArrowPositions(position);

            }
            else
            {
                previewObject.transform.position = position;
                previewObject.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
                UpdateArrowPositions(position);
            }
            previewMode = true;
        }
    }

    /// <summary>
    /// Regarde si la position est un grounde ou empty
    /// Auteur : Oli
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    private bool CheckIfSomethingOnPos(Vector3 pos)
    {
        Tile tile = gridManager.GetTile((int)pos.x,(int)pos.z);

        if (tile.Type == Tile.TileType.Empty || tile.Type == Tile.TileType.Ground)
            return true;
        else
        return false;
    }

    /// <summary>
    /// Permet d'obtenir un objet sur une tuile préçis
    /// Auteur : Wil et Oli 
    /// </summary>
    public Tile GetObjectOnTile()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector2Int gridPosition = gridManager.WorldToGridPosition(hitInfo.point);
            Tile tile = gridManager.GetTile(gridPosition.x, gridPosition.y);
            if (!string.IsNullOrEmpty(tile.Name))
            {
                return tile;
            }
            else
            {
                Debug.Log($"Erreur : l'objet ({tile.Name}) sur la tuile (X : {tile.Position.x }, Z : {tile.Position.z}) n'as pas été trouvé");
            }
        }
        else
        {
            Debug.Log("Erreur avec la condition : if (Physics.Raycast(ray, out RaycastHit hitInfo)) dans la méthode GetObjectOnTile()");
        }
        return null;
    }

    /// <summary>
    /// Permet de supprimer un objet sur une tuile  
    /// Auteur : Wil et Oli 
    /// </summary>
    private void DeleteObectTile()
    {
        Tile tile = GetObjectOnTile();
        Vector2Int pos = gridManager.WorldToGridPosition(tile.Position);
        RemoveObjectFromTile(pos);
    }


    /// <summary>
    /// Permet de confirmer le placement de l'objet (ressemble a la m�thode TryToPlaceObject, mais ne prend plus la position)
    /// Auteur : Oli 
    /// </summary>
    private void ConfirmPlacement()
    {
        if (previewObject != null)
        {

            Destroy(previewObject);

            GameObject placedObject = Instantiate(objectToPlacePrefab, previewObject.transform.position, Quaternion.Euler(0, currentRotation, 0));
            placedObject.transform.SetParent(this.transform);
            Vector2Int pos = gridManager.WorldToGridPosition(previewObject.transform.position);
            //RemoveObjectFromTile(pos);
            Tile.TileType type = gameManager.ReturnType(objectToPlacePrefab);
            Tile.TileSubType subType = gameManager.ReturnSubType(objectToPlacePrefab); //gameManager.ReturnSubType(objectToPlacePrefab);
            Tile tile = new Tile(previewObject.transform.position, new Vector3(0, currentRotation, 0), type, subType, objectToPlacePrefab.name, "", 0);
            gridManager.SetValue(pos.x, pos.y, tile);

            previewObject = null;
            HideArrows();
            previewMode = false;
            objectToPlacePrefab = null;
        }
    }

    /// <summary>
    /// Permet de positionner les fleches sur l'endroit cliquer
    /// Auteur : Oli
    /// </summary>
    /// <param name="basePosition"></param>
    private void UpdateArrowPositions(Vector3 basePosition)
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            arrows[i].SetActive(true);
            arrows[i].transform.position = basePosition + arrows[i].transform.forward * arrowOffset;
        }
    }

    /// <summary>
    /// Sert a orienter le prefab de l'element en cliquant sur les fleches
    /// Auteur : Oli
    /// </summary>
    /// <param name="arrowDirection"></param>
    private void AdjustOrientationBasedOnArrow(ArrowDirection arrowDirection)
    {
        switch (arrowDirection.direction)
        {
            case ArrowDirection.Direction.Up:
                currentRotation += 90;
                break;
            case ArrowDirection.Direction.Down:
                currentRotation -= 90;
                break;
            case ArrowDirection.Direction.Left:
                currentRotation -= 90;
                break;
            case ArrowDirection.Direction.Right:
                currentRotation += 90;
                break;
        }

        if (previewObject != null)
        {
            previewObject.transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        }
    }

    /// <summary>
    /// Debug void, permet d'aller voir les valeurs sur chaque grille 
    /// Auteur : Oli 
    /// </summary>
    private void TryGetGridValue()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector2Int gridPosition = gridManager.WorldToGridPosition(hitInfo.point);
            Tile tile = gridManager.GetTile(gridPosition.x, gridPosition.y);

            Debug.Log($"Tile at ({gridPosition.x}, {gridPosition.y}), Type : {tile.Type}, SubType : {tile.SubType}, Name: {tile.Name}");

        }
    }

    public bool GridValueHasObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector2Int gridPosition = gridManager.WorldToGridPosition(hitInfo.point);
            Tile tile = gridManager.GetTile(gridPosition.x, gridPosition.y);
            if (tile.Type == Tile.TileType.Ground)
            {
                return false;
            }
            else
                return true;
        }
        return true;
    }


    /// <summary>
    /// Permet de set le prefab a placer a l'aide du menu
    /// Auteur : William
    /// </summary>
    public void setObectToPlaceAuto(string nameValue)
    {
        // Building_House_01_color01|Building|Residential
        string[] parts = nameValue.Split('|');

        string nameBuild = parts[0];
        string typeBuildStr = parts[1];
        string subTypeBuildStr = parts[2];
        if(parts.Length >= 3)
        {
            try
            {
                if (Enum.TryParse(typeBuildStr, out Tile.TileType typeBuild))
                {
                    Debug.Log($"nameBuild : {nameBuild}, typeBuild : {typeBuild}");

                    if (Enum.TryParse(subTypeBuildStr, out Tile.TileSubType subTypeBuild))
                    {
                        Debug.Log($"subTypeBuild : {subTypeBuild}");

                        objectToPlacePrefab = gameManager.ReturnPrefabsType(nameBuild, typeBuild, subTypeBuild);
                        if (objectToPlacePrefab is null)
                        {
                            Debug.Log($"Erreur : Le prefeb est null");
                        }
                    }
                    else
                    {
                        Debug.LogError($"Erreur de conversion pour subTypeBuild : {subTypeBuildStr}");
                    }
                }
                else
                {
                    Debug.LogError($"Erreur de conversion pour typeBuild : {typeBuildStr}");
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Message : " + ex.Message + " | InnerExeption : " + ex.InnerException);
            }

        }
    }


    /// <summary>
    /// Permet de set le prefab a placer
    /// Auteur : Oli
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    public void setObectToPlace(string name, Tile.TileType type, Tile.TileSubType subType)
    {
        objectToPlacePrefab = gameManager.ReturnPrefabsType(name, type, subType);
    }

    public void OnMouseEnterTarget()
    {
        // Pour le Menu de construction
        isHoveredMenu = true;
    }

    public void OnMouseExitTarget()
    {
        // Pour le Menu de construction
        isHoveredMenu = false;
    }




    public void RemoveObjectFromTile(Vector2Int gridPosition)
    {

        Tile tile = gridManager.GetTile(gridPosition.x, gridPosition.y);



        if (parentObject.transform.childCount > 0 && (tile.Name != "Grass" && tile.Type != Tile.TileType.Water))
        {
            for (int i = 0; i < parentObject.transform.childCount; i++)
            {

                if (parentObject.transform.GetChild(i).gameObject.name == (tile.Name + "(Clone)") && parentObject.transform.GetChild(i).gameObject.transform.position == tile.Position)
                {
               Destroy(parentObject.transform.GetChild(i).gameObject);

                    tile.Name = "Grass";
                    tile.Type = Tile.TileType.Ground;
                    tile.SubType = Tile.TileSubType.Empty;
                    tile.Description = string.Empty;
                    tile.Price = 0;

                    gridManager.SetValue(gridPosition.x, gridPosition.y, tile);
                    continue;
                }
            }
        }

 
    }

   


}