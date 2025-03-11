using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPercentageSizeAdjuster : MonoBehaviour
{
    public List<Button> buttonsList; // Liste de boutons dans la ScrollView
    public float buttonWidthPercentage = 20f; // Pourcentage de la largeur de l'écran pour la largeur des boutons
    public float buttonHeightPercentage = 10f; // Pourcentage de la hauteur de l'écran pour la hauteur des boutons

    void Start()
    {
        AdjustButtonSizes();
    }

    void AdjustButtonSizes()
    {
        foreach (Button button in buttonsList)
        {
            RectTransform buttonRect = button.GetComponent<RectTransform>();
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float adjustedWidth = screenWidth * (buttonWidthPercentage / 100f);
            float adjustedHeight = screenHeight * (buttonHeightPercentage / 100f);

            buttonRect.sizeDelta = new Vector2(adjustedWidth, adjustedHeight);
        }
    }
}
