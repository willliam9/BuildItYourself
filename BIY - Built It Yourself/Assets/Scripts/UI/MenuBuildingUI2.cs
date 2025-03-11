using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MenuBuildingUI2;

public class MenuBuildingUI2 : MonoBehaviour
{
    public GameObject BtnOpenMenuBuilding;
    public GameObject BtnCloseMenuBuilding;
    public GameObject BtnCloseMenuTab;
    public GameObject PanelTabMenuBuild;
    public GameObject PanelAllMenuTabBuild;

    [System.Serializable]
    public class CategoryData
    {
        [SerializeField]
        public GameObject menu;
        [SerializeField]
        public GameObject panelTabMenu;
        [SerializeField]
        public Dictionary<string, GameObject> categories = new Dictionary<string, GameObject>();
    }

    [Header("Sections bâtiments")]
    [SerializeField] private CategoryData buildingData;

    [Header("Sections Nature")]
    [SerializeField] private CategoryData natureData;

    [Header("Sections sols")]
    [SerializeField] private CategoryData groundData;

    [Header("Sections décorations")]
    [SerializeField] private CategoryData decorationData;

    [Header("Sections essentiels")]
    [SerializeField] private CategoryData supplyData;

    [Header("Sections services")]
    [SerializeField] private CategoryData serviceData;

    void Start()
    {
        InitializeCategories();
    }

    private void InitializeCategories()
    {
        AddCategoryData("ResidentialPanel", buildingData);
        AddCategoryData("CommercialPanel", buildingData);
        AddCategoryData("IndustrialPanel", buildingData);
        AddCategoryData("LeisurePanel", buildingData);
        AddCategoryData("MonumentPanel", buildingData);
        AddCategoryData("TouristPanel", buildingData);


        AddCategoryData("TreePanel", natureData);
        AddCategoryData("RockPanel", natureData);
        AddCategoryData("MushroomPanel", natureData);
        AddCategoryData("PlantPanel", natureData);
        AddCategoryData("FlowerPanel", natureData);
        AddCategoryData("SpecialPanel", natureData);


        AddCategoryData("GroundPanel", groundData);
        AddCategoryData("RoadPanel", groundData);


        AddCategoryData("DecorationPanel", decorationData);


        AddCategoryData("WaterPanel", supplyData);
        AddCategoryData("ElectricityPanel", supplyData);
        AddCategoryData("TrashPanel", supplyData);


        AddCategoryData("PolicePanel", serviceData);
        AddCategoryData("HealthPanel", serviceData);
        AddCategoryData("FirePanel", serviceData);
        AddCategoryData("EducationPanel", serviceData);
    }

    private void AddCategoryData(string categoryName, CategoryData categoryData)
    {
        Transform categoryTransform = categoryData.menu.transform.Find(categoryName);
        if (categoryTransform != null)
        {
            categoryData.categories.Add(categoryName, categoryTransform.gameObject);
        }
        else
        {
            Debug.LogError($"Category '{categoryName}' not found in menu '{categoryData.menu.name}'.");
        }
    }

    public void CloseMenuBuildingButton()
    {
        ShowHideAllCategories(false);
        ShowHideAllMenus(false);
        BtnOpenMenuBuilding.SetActive(true);
        BtnCloseMenuBuilding.SetActive(false);
        PanelTabMenuBuild.SetActive(false);
        BtnCloseMenuTab.SetActive(false);
        ShowHideAllTabs(false);
    }

    public void CloseSubTabMenu()
    {
        //PanelTabMenuBuild.SetActive(false);
        BtnCloseMenuTab.SetActive(false);
        PanelAllMenuTabBuild.SetActive(false);
        PanelTabMenuBuild.SetActive(true);
        //ShowHideAllTabs(false);
    }

    public void OpenMenuBuild()
    {
        ShowHideCategory(buildingData);
        BtnOpenMenuBuilding.SetActive(false);
        BtnCloseMenuBuilding.SetActive(true);
        PanelTabMenuBuild.SetActive(true);
        BtnCloseMenuTab.SetActive(false);
        ShowHideAllTabs(false);
        buildingData.menu.SetActive(true);
        //ShowHideCategoryData(buildingData, true);
    }

    public void OpenMenuBuilding()
    {
        ShowHideCategory(buildingData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(buildingData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    public void OpenMenuNature()
    {
        ShowHideCategory(natureData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(natureData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    public void OpenMenuGround()
    {
        ShowHideCategory(groundData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(groundData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    public void OpenMenuDecoration()
    {
        ShowHideCategory(decorationData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(decorationData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    public void OpenMenuSupply()
    {
        ShowHideCategory(supplyData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(supplyData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    public void OpenMenuService()
    {
        ShowHideCategory(serviceData);
        BtnCloseMenuTab.SetActive(true);
        PanelTabMenuBuild.SetActive(false);
        PanelAllMenuTabBuild.SetActive(true);
        ShowHideTab(serviceData.panelTabMenu);
        buildingData.menu.SetActive(true);
    }

    private void ShowHideAllCategories(bool hide)
    {
        ShowHideCategoryData(buildingData, hide);
        ShowHideCategoryData(natureData, hide);
        ShowHideCategoryData(groundData, hide);
        ShowHideCategoryData(decorationData, hide);
        ShowHideCategoryData(supplyData, hide);
        ShowHideCategoryData(serviceData, hide);
        BtnOpenMenuBuilding.SetActive(false);
    }

    private void ShowHideCategory(CategoryData categoryData)
    {
        ShowHideAllCategories(false);
        categoryData.menu.SetActive(true);
    }

    private void ShowHideCategoryData(CategoryData categoryData, bool hide)
    {
        foreach (var category in categoryData.categories.Values)
        {
            category.SetActive(hide);
        }
        categoryData.menu.SetActive(hide);
        categoryData.panelTabMenu.SetActive(hide);
    }


    private void ShowHideCategoryDataIndex(CategoryData categoryData, bool hide, int index)
    {
        int i = 1;
        foreach (var category in categoryData.categories.Values)
        {
            if (i == index)
                category.SetActive(hide);
            else
                category.SetActive(!hide);
            i++;
        }
        categoryData.menu.SetActive(hide);
        categoryData.panelTabMenu.SetActive(hide);
    }


    private void ShowHideAllMenus(bool show)
    {
        ShowHideCategoryData(natureData, show);
        ShowHideCategoryData(groundData, show);
        ShowHideCategoryData(decorationData, show);
        ShowHideCategoryData(supplyData, show);
        ShowHideCategoryData(serviceData, show);
    }

    private void ShowHideAllTabs(bool show)
    {
        ShowHideCategoryData(buildingData, show);
        ShowHideCategoryData(natureData, show);
        ShowHideCategoryData(groundData, show);
        ShowHideCategoryData(decorationData, show);
        ShowHideCategoryData(supplyData, show);
        ShowHideCategoryData(serviceData, show);
    }

    private void ShowHideTab(GameObject tab)
    {
        ShowHideAllTabs(false);
        tab.SetActive(true);
    }



    public void OpenSubMenuBuild(string index)
    {
        string[] indexParts = index.Split('_');

        if (indexParts.Length == 2 && int.TryParse(indexParts[0], out int indexCategory) && int.TryParse(indexParts[1], out int indexSubCategory))
        {
            switch (indexCategory)
            {
                case 1: // BuildingPanel
                    OpenSubCategory(GetBuildingPanelName(indexSubCategory), buildingData, indexSubCategory);
                    break;

                case 2: // NaturePanel
                    OpenSubCategory(GetNaturePanelName(indexSubCategory), natureData, indexSubCategory);
                    break;

                case 3: // GroundPanel
                    OpenSubCategory(GetGroundPanelName(indexSubCategory), groundData, indexSubCategory);
                    break;

                case 4: // DecorationPanel
                    OpenSubCategory(GetDecorationPanelName(indexSubCategory), decorationData, indexSubCategory);
                    break;

                case 5: // SupplyPanel
                    OpenSubCategory(GetSupplyPanelName(indexSubCategory), supplyData, indexSubCategory);
                    break;

                case 6: // ServicePanel
                    OpenSubCategory(GetServicePanelName(indexSubCategory), serviceData, indexSubCategory);
                    break;

                default:
                    Debug.LogError("Invalid indexCategory");
                    break;
            }
        }
        else
        {
            Debug.LogError("Invalid index format");
        }
    }

    // Méthodes auxiliaires pour obtenir le nom correct du panneau en fonction de l'index
    private string GetBuildingPanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "ResidentialPanel";
            case 2: return "CommercialPanel";
            case 3: return "IndustrialPanel";
            case 4: return "LeisurePanel";
            case 5: return "MonumentPanel";
            case 6: return "TouristPanel";
            default: return null;
        }
    }

    private string GetNaturePanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "TreePanel";
            case 2: return "RockPanel";
            case 3: return "MushroomPanel";
            case 4: return "FlowerPanel";
            case 5: return "PlantPanel";
            case 6: return "SpecialPanel";
            default: return null;
        }
    }

    private string GetGroundPanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "GroundPanel";
            case 2: return "RoadPanel";
            case 3: return "d";
            case 4: return "d";
            case 5: return "d";
            case 6: return "d";
            default: return null;
        }
    }

    private string GetDecorationPanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "DecorationPanel";
            case 2: return "Panel";
            case 3: return "Panel";
            case 4: return "Panel";
            case 5: return "Panel";
            case 6: return "Panel";
            default: return null;
        }
    }

    private string GetSupplyPanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "WaterPanel";
            case 2: return "ElectricityPanel";
            case 3: return "TrashPanel";
            case 4: return "Panel";
            case 5: return "Panel";
            case 6: return "Panel";
            default: return null;
        }
    }

    private string GetServicePanelName(int indexSubCategory)
    {
        switch (indexSubCategory)
        {
            case 1: return "PolicePanel";
            case 2: return "HealthPanel";
            case 3: return "FirePanel";
            case 4: return "EducationPanel";
            case 5: return "Panel";
            case 6: return "Panel";
            default: return null;
        }
    }

    // Ouvrir une sous-catégorie spécifique
    public void OpenSubCategory(string subCategoryName, CategoryData categoryData, int index)
    {
        ShowHideCategoryData(categoryData, false);
        ShowHideCategoryDataIndex(categoryData, true, index);
        categoryData.categories[subCategoryName].SetActive(true);
    }
}
