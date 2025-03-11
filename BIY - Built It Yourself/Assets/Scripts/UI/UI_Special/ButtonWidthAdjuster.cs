using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeAdjuster : MonoBehaviour
{
    public List<Button> buttonsList; // Liste de boutons dans la ScrollView
    public float buttonWidth = 200f; // Largeur souhait�e pour les boutons
    public float buttonHeight = 50f; // Hauteur souhait�e pour les boutons

    void Start()
    {
        AdjustButtonSizes();
    }

    void AdjustButtonSizes()
    {
        foreach (Button button in buttonsList)
        {
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            buttonRect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
        }
    }
}