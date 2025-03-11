using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    private WorldGenerator worldGenerator;
    private int tileX;
    private int tileY;

    public void Initialize(WorldGenerator generator, int x, int y)
    {
        worldGenerator = generator;
        tileX = x;
        tileY = y;
    }

    private void OnMouseDown()
    {
        // G�rer le clic sur la case (tileX, tileY)
        Debug.Log("Tile clicked: (" + tileX + ", " + tileY + ")");
        // Ajoutez ici le code pour r�agir au clic sur une case sp�cifique
    }
}
