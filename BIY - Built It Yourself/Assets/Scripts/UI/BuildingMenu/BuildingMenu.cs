using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
//using static System.Net.Mime.MediaTypeNames;

public class BuildingMenu : MonoBehaviour
{
    [System.Serializable]
    public class Category
    {
        public string name;
        public List<SubCategory> subCategories = new List<SubCategory>();
    }

    [System.Serializable]
    public class SubCategory
    {
        public string name;
        public List<BuildingButton> buildingButtons;
        public List<GameObject> prefabList;
    }

    [System.Serializable]
    public class BuildingButton
    {
        public string name;
        public GameObject buildingPrefab;
    }

    public List<Category> categories = new List<Category>();
    public Transform content;
    public Button categoryButtonPrefab;
    public Button subCategoryButtonPrefab;
    public Button buildingButtonPrefab;
    public ScrollRect scrollRect;

    private void Start()
    {
        GenerateMenu();
    }

    private void GenerateMenu()
    {
        foreach (Category category in categories)
        {
            Button categoryButton = Instantiate(categoryButtonPrefab, content);
            //categoryButton.GetComponentInChildren<Text>().text = category.name;
            categoryButton.onClick.AddListener(() => ShowSubCategories(category.subCategories));
        }
    }

    private void ShowSubCategories(List<SubCategory> subCategories)
    {
        ClearContainer(content);

        foreach (SubCategory subCategory in subCategories)
        {
            Button subCategoryButton = Instantiate(subCategoryButtonPrefab, content);
            subCategoryButton.GetComponentInChildren<Text>().text = subCategory.name;
            subCategoryButton.onClick.AddListener(() => ShowBuildings(subCategory.buildingButtons, subCategory.prefabList));
        }
    }

    private void ShowBuildings(List<BuildingButton> buildingButtons, List<GameObject> prefabList)
    {
        ClearContainer(content);

        for (int i = 0; i < buildingButtons.Count; i++)
        {
            Button buildingButtonInstance = Instantiate(buildingButtonPrefab, content);
            buildingButtonInstance.GetComponentInChildren<Text>().text = buildingButtons[i].name;
            buildingButtonInstance.onClick.AddListener(() => HandleBuildingSelection(prefabList[i]));
        }
    }

    private void HandleBuildingSelection(GameObject buildingPrefab)
    {
        // Implement the logic to handle the selected building prefab
        Debug.Log($"Selected Building: {buildingPrefab.name}");
        // Instantiate(buildingPrefab);
        // Place the building logic...
    }

    private void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
}
