using UnityEngine;
using UnityEngine.UI;

public class ScrollViewWithMargin : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;
    public float margin = 20f; // Ajoutez la marge souhaitée

    void Start()
    {
        AdjustContentSize();
    }

    void AdjustContentSize()
    {
        RectTransform rt = scrollRect.GetComponent<RectTransform>();
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Ajustez la largeur du contenu pour qu'elle soit égale à la largeur de l'écran avec une marge de chaque côté
        float contentWidth = screenWidth - 2 * margin;
        content.sizeDelta = new Vector2(contentWidth, content.sizeDelta.y);

        // Ajustez d'autres paramètres du RectTransform du Content en fonction de vos besoins
    }
}
