using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class BodyPanelManager : MonoBehaviour, IExilirEntityList
{
    public Image MetalIcon, WoodIcon, WaterIcon, FireIcon, EarthIcon;
    public Image MetalBanner, WoodBanner, WaterBanner, FireBanner, EarthBanner;
    public Text MetalTitle, WoodTitle, WaterTitle, FireTitle, EarthTitle;
    public Text MetalName, WoodName, WaterName, FireName, EarthName;
    public Text MetalDetail, WoodDetail, WaterDetail, FireDetail, EarthDetail;
    public ScrollRect ListOfExilirs;
    public GameObject ExilirEntityPrefab;
    public GameObject RightPanel, DetailPanel;
    public Image DetailEnlargement, DetailBanner, DetailElement;
    public Text DetailName, DetailDescription;
    public Button SelectButton, CloseButton, CancelButton;

    public ArrayList MetalExilirs = new ArrayList(), WoodExilirs = new ArrayList(), 
    WaterExilirs = new ArrayList(), FireExilirs = new ArrayList(), 
    EarthExilirs = new ArrayList(), targetExilirs;
    public Sprite[] ExilirSprites, BannerSprites, ElementSprites;
    public IconManager IconManager;
    // Start is called before the first frame update
    void Start()
    {
        Exilir[] Exiliers = GameManager.GetExilirs();
        foreach (Exilir exilir in Exiliers) {
            switch(exilir.Element) {
                case Elements.Metal:
                    MetalExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Metal)) {
                        PopulateEquippedExilir(exilir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                    }
                    break;
                case Elements.Wood:
                    WoodExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Wood)) {
                        PopulateEquippedExilir(exilir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                    }
                    break;
                case Elements.Water:
                    WaterExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Water)) {
                        PopulateEquippedExilir(exilir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                    }
                    break;
                case Elements.Fire:
                    FireExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Fire)) {
                        PopulateEquippedExilir(exilir, FireIcon, FireBanner, FireName, FireDetail);
                    }
                    break;
                case Elements.Earth:
                    EarthExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Earth)) {
                        PopulateEquippedExilir(exilir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                    }
                    break;
                case Elements.Normal:
                    MetalExilirs.Add(exilir);
                    WoodExilirs.Add(exilir);
                    WaterExilirs.Add(exilir);
                    FireExilirs.Add(exilir);
                    EarthExilirs.Add(exilir);
                    if (GameManager.IsExilirEquipped(exilir, Elements.Metal)) {
                        PopulateEquippedExilir(exilir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                    }
                    else if (GameManager.IsExilirEquipped(exilir, Elements.Wood)) {
                        PopulateEquippedExilir(exilir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                    }
                    else if (GameManager.IsExilirEquipped(exilir, Elements.Water)) {
                        PopulateEquippedExilir(exilir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                    }
                    else if (GameManager.IsExilirEquipped(exilir, Elements.Fire)) {
                        PopulateEquippedExilir(exilir, FireIcon, FireBanner, FireName, FireDetail);
                    }
                    else if (GameManager.IsExilirEquipped(exilir, Elements.Earth)) {
                        PopulateEquippedExilir(exilir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                    }
                    break;
            }
        }

        // populate the title text
    }

    private void PopulateEquippedExilir(Exilir exilir, Image icon, Image banner, Text name, Text description) {
        Color emptyColor = new Color(1.0f,1.0f,1.0f,0.0f), fullColor = new Color(1.0f,1.0f,1.0f,1.0f);
        icon.sprite = null;
        icon.color = emptyColor;
        if (exilir.IsSpecialSprite) {
            icon.gameObject.transform.parent.GetComponent<Image>().sprite = exilir.SpecialSprite;
        }
        else {    
            int elementIndex = (int)exilir.Element - 1;
            icon.gameObject.transform.parent.GetComponent<Image>().sprite = ExilirSprites[elementIndex];
        }
        
        icon.gameObject.transform.parent.GetComponent<Image>().color = fullColor;
        int rarityIndex = (int)exilir.Rarity - 2;
        banner.sprite = BannerSprites[rarityIndex];
        name.text = exilir.GetLocalizedName();
        description.text = exilir.GetLocalizedDescription();
    }

    private bool isRightPanelOpen;
    private bool isDetailPenalOpen;
    public ExilirListEntity[] Entities;
    private Elements SelectElement;
    public void OpenRightPanel(int key) {
        if (isDetailPenalOpen) {
            return;
        }

        targetExilirs = new ArrayList();
        switch(key) {
            case 1:
                targetExilirs = MetalExilirs;
                SelectElement = Elements.Metal;
                break;
            case 2:
                targetExilirs = WoodExilirs;
                SelectElement = Elements.Wood;
                break;
            case 3:
                targetExilirs = WaterExilirs;
                SelectElement = Elements.Water;
                break;
            case 4:
                targetExilirs = FireExilirs;
                SelectElement = Elements.Fire;
                break;
            case 5:
                targetExilirs = EarthExilirs;
                SelectElement = Elements.Earth;
                break;
        }

        isRightPanelOpen = true;
        RightPanel.SetActive(true);

        RectTransform content = ListOfExilirs.content;
        foreach (Transform child in content.transform) {
            GameObject.Destroy(child.gameObject);
        }
        Entities = new ExilirListEntity[targetExilirs.Count];
        for (int i = 0; i < targetExilirs.Count; i++) {
           GameObject newItem = Instantiate(ExilirEntityPrefab, content);
           ExilirListEntity entity = newItem.gameObject.GetComponent<ExilirListEntity>();
           entity.Exilir = (Exilir)targetExilirs[i];
           entity.IndexInList = i;
           entity.ListManager = this;
           entity.PopulateImage();
           Entities[i] = entity;
        }
    }

    private Exilir selectedExilir;
    public void SelectEntity(int index) {
       isDetailPenalOpen = true;

       DetailPanel.SetActive(true);

       ExilirListEntity entity = Entities[index];
       selectedExilir = entity.Exilir;
       DetailEnlargement.sprite = entity.ExilirImage.sprite;
       DetailBanner.sprite = entity.Banner.sprite;
       DetailName.text = entity.Exilir.GetLocalizedName();
       DetailDescription.text = entity.Exilir.GetLocalizedDescription();
       int elementIndex = (int)entity.Exilir.Element - 1;
       DetailElement.sprite = ElementSprites[elementIndex];
    }

    public void CloseButtonCliked() {
        if (isDetailPenalOpen) {
            DetailPanel.SetActive(false);
            selectedExilir = null;
            return;
        }
        
        IconManager.OpenedPanelIsClosing();
        Destroy(gameObject);
    }

    public void SelectButtonCliked() {
         switch(SelectElement) {
            case Elements.Metal:
                PopulateEquippedExilir(selectedExilir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                GameManager.EquipExilir(selectedExilir, Elements.Metal);
                break;
            case Elements.Wood:
                PopulateEquippedExilir(selectedExilir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                GameManager.EquipExilir(selectedExilir, Elements.Wood);
                break;
            case Elements.Water:
                PopulateEquippedExilir(selectedExilir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                GameManager.EquipExilir(selectedExilir, Elements.Water);
                break;
            case Elements.Fire:
                PopulateEquippedExilir(selectedExilir, FireIcon, FireBanner, FireName, FireDetail);
                GameManager.EquipExilir(selectedExilir, Elements.Fire);
                break;
            case Elements.Earth:
                PopulateEquippedExilir(selectedExilir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                GameManager.EquipExilir(selectedExilir, Elements.Earth);
                break;
         }
         DetailPanel.SetActive(false);
         isDetailPenalOpen = false;
         selectedExilir = null;
    }

    public void CancelButtonClicked() {
         DetailPanel.SetActive(false);
         isDetailPenalOpen = false;
         selectedExilir = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
