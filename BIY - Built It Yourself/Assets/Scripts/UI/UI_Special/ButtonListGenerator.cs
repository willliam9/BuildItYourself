using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonListGenerator : MonoBehaviour
{
    public Button buttonPrefab;     // Bouton préfabriqué
    public Transform contentParent;  // Parent du contenu du ScrollView
    public float buttonSpacing = 30f; // Espacement vertical entre les boutons

    void Start()
    {
        GenerateButtons();
    }

    void GenerateButtons()
    {
        //Debug.Log("buttonPrefab : " + buttonPrefab);
        //Debug.Log("contentParent : " + contentParent);
        //Debug.Log("NB monde : " + WorldListManagement.worldList.worlds.Count);

        WorldListManagement.LoadWorldList();

        // Réinitialiser la position Y du contentParent à 0
        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, 0f);

        // Calculer la hauteur totale des boutons
        float totalHeight = 0f;

        for (int i = 0; i < WorldListManagement.worldList.worlds.Count; i++)
        {
            World world = WorldListManagement.worldList.worlds[i];
            Debug.Log("World : " + world.worldName);

            if (world != null)
            {
                Button newButton = Instantiate(buttonPrefab) as Button;
                newButton.transform.SetParent(contentParent, false);

                // Ajustez la position Y du bouton en fonction de l'index
                float buttonY = -i * buttonSpacing;
                RectTransform buttonRect = newButton.GetComponent<RectTransform>();
                buttonRect.anchoredPosition = new Vector2(buttonRect.anchoredPosition.x, buttonY);

                // Récupérer le composant Text du bouton
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

                // Vérifier si le composant Text existe
                if (buttonText != null)
                {
                    // Attribuer le texte du bouton avec le nom du monde
                    buttonText.text = "Le Monde N." + i + " Name: " + WorldListManagement.worldList.worlds[i].worldName + " Date: " + WorldListManagement.worldList.worlds[i].creationDate;//world.worldName;
                }

                // Ajoutez un gestionnaire d'événements pour le clic du bouton
                newButton.onClick.AddListener(() => WorldListManagement.LoadWorld(world.worldID, world.worldName));

                // Ajoutez l'espace entre les boutons à la hauteur totale
                totalHeight += newButton.GetComponent<RectTransform>().rect.height + buttonSpacing;
            }
        }

        // Ajoutez un espace supplémentaire après le dernier bouton
        totalHeight += buttonSpacing * 2;

        // Mettez à jour la taille du contenu du ScrollView
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
    }
}
