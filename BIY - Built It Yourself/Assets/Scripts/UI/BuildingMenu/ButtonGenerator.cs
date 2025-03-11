using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollableButtonList : MonoBehaviour
{
    public Button buttonPrefab;
    public Transform contentTransform;
    public float buttonSpacing = 10f;

    private void Start()
    {
        GenerateButtons();
    }

    private void GenerateButtons()
    {
        // Exemple de liste de noms de boutons
        List<string> buttonNames = new List<string> { "Bouton 1", "Bouton 2", "Bouton 3", "Bouton 4", "Bouton 5", "Bouton 6", "Bouton 7" };

        float totalWidth = 0f;

        foreach (string buttonName in buttonNames)
        {
            Button buttonInstance = Instantiate(buttonPrefab, contentTransform);
            buttonInstance.GetComponentInChildren<Text>().text = buttonName;

            // Mettez à jour la largeur totale des boutons
            totalWidth += buttonInstance.GetComponent<RectTransform>().rect.width + buttonSpacing;
        }

        // Définissez la largeur du contenu pour permettre le défilement horizontal
        RectTransform contentRect = contentTransform.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(totalWidth, contentRect.sizeDelta.y);
    }
}