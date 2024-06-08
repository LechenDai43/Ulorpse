using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class BodyPanelManager : MonoBehaviour, IElixirEntityList
{
    public Image MetalIcon, WoodIcon, WaterIcon, FireIcon, EarthIcon;
    public Image MetalBanner, WoodBanner, WaterBanner, FireBanner, EarthBanner;
    public Text MetalName, WoodName, WaterName, FireName, EarthName;
    public Text MetalDetail, WoodDetail, WaterDetail, FireDetail, EarthDetail;
    public ScrollRect ListOfElixirs;
    public GameObject ElixirEntityPrefab;
    public GameObject RightPanel, DetailPanel;
    public Image DetailEnlargement, DetailBanner, DetailElement;
    public Text DetailName, DetailDescription;
    public Button SelectButton, CloseButton, CancelButton;

    public ArrayList MetalElixirs = new ArrayList(), WoodElixirs = new ArrayList(), 
    WaterElixirs = new ArrayList(), FireElixirs = new ArrayList(), 
    EarthElixirs = new ArrayList(), targetElixirs;
    public Sprite[] ElixirSprites, BannerSprites, ElementSprites;
    public IconManager IconManager;
    // Start is called before the first frame update
    void Start()
    {
        Elixir[] Exiliers = GameManager.GetElixirs();
        foreach (Elixir elixir in Exiliers) {
            switch(elixir.Element) {
                case Elements.Metal:
                    MetalElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Metal)) {
                        PopulateEquippedElixir(elixir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                    }
                    break;
                case Elements.Wood:
                    WoodElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Wood)) {
                        PopulateEquippedElixir(elixir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                    }
                    break;
                case Elements.Water:
                    WaterElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Water)) {
                        PopulateEquippedElixir(elixir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                    }
                    break;
                case Elements.Fire:
                    FireElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Fire)) {
                        PopulateEquippedElixir(elixir, FireIcon, FireBanner, FireName, FireDetail);
                    }
                    break;
                case Elements.Earth:
                    EarthElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Earth)) {
                        PopulateEquippedElixir(elixir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                    }
                    break;
                case Elements.Normal:
                    MetalElixirs.Add(elixir);
                    WoodElixirs.Add(elixir);
                    WaterElixirs.Add(elixir);
                    FireElixirs.Add(elixir);
                    EarthElixirs.Add(elixir);
                    if (GameManager.IsElixirEquipped(elixir, Elements.Metal)) {
                        PopulateEquippedElixir(elixir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                    }
                    else if (GameManager.IsElixirEquipped(elixir, Elements.Wood)) {
                        PopulateEquippedElixir(elixir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                    }
                    else if (GameManager.IsElixirEquipped(elixir, Elements.Water)) {
                        PopulateEquippedElixir(elixir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                    }
                    else if (GameManager.IsElixirEquipped(elixir, Elements.Fire)) {
                        PopulateEquippedElixir(elixir, FireIcon, FireBanner, FireName, FireDetail);
                    }
                    else if (GameManager.IsElixirEquipped(elixir, Elements.Earth)) {
                        PopulateEquippedElixir(elixir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                    }
                    break;
            }
        }

        // populate the title text
    }

    private void PopulateEquippedElixir(Elixir elixir, Image icon, Image banner, Text name, Text description) {
        Color emptyColor = new Color(1.0f,1.0f,1.0f,0.0f), fullColor = new Color(1.0f,1.0f,1.0f,1.0f);
        icon.sprite = null;
        icon.color = emptyColor;
        if (elixir.IsSpecialSprite) {
            icon.gameObject.transform.parent.GetComponent<Image>().sprite = elixir.SpecialSprite;
        }
        else {    
            int elementIndex = (int)elixir.Element - 1;
            icon.gameObject.transform.parent.GetComponent<Image>().sprite = ElixirSprites[elementIndex];
        }
        
        icon.gameObject.transform.parent.GetComponent<Image>().color = fullColor;
        int rarityIndex = (int)elixir.Rarity - 2;
        banner.sprite = BannerSprites[rarityIndex];
        name.text = elixir.GetLocalizedName();
        description.text = elixir.GetLocalizedDescription();
    }

    private bool isRightPanelOpen;
    private bool isDetailPenalOpen;
    public ElixirListEntity[] Entities;
    private Elements SelectElement;
    public void OpenRightPanel(int key) {
        if (isDetailPenalOpen) {
            return;
        }

        targetElixirs = new ArrayList();
        switch(key) {
            case 1:
                targetElixirs = MetalElixirs;
                SelectElement = Elements.Metal;
                break;
            case 2:
                targetElixirs = WoodElixirs;
                SelectElement = Elements.Wood;
                break;
            case 3:
                targetElixirs = WaterElixirs;
                SelectElement = Elements.Water;
                break;
            case 4:
                targetElixirs = FireElixirs;
                SelectElement = Elements.Fire;
                break;
            case 5:
                targetElixirs = EarthElixirs;
                SelectElement = Elements.Earth;
                break;
        }

        isRightPanelOpen = true;
        RightPanel.SetActive(true);

        RectTransform content = ListOfElixirs.content;
        foreach (Transform child in content.transform) {
            GameObject.Destroy(child.gameObject);
        }
        Entities = new ElixirListEntity[targetElixirs.Count];
        for (int i = 0; i < targetElixirs.Count; i++) {
           GameObject newItem = Instantiate(ElixirEntityPrefab, content);
           ElixirListEntity entity = newItem.gameObject.GetComponent<ElixirListEntity>();
           entity.Elixir = (Elixir)targetElixirs[i];
           entity.IndexInList = i;
           entity.ListManager = this;
           entity.PopulateImage();
           Entities[i] = entity;
        }
    }

    private Elixir selectedElixir;
    public void SelectEntity(int index) {
       isDetailPenalOpen = true;

       DetailPanel.SetActive(true);

       ElixirListEntity entity = Entities[index];
       selectedElixir = entity.Elixir;
       DetailEnlargement.sprite = entity.ElixirImage.sprite;
       DetailBanner.sprite = entity.Banner.sprite;
       DetailName.text = entity.Elixir.GetLocalizedName();
       DetailDescription.text = entity.Elixir.GetLocalizedDescription();
       int elementIndex = (int)entity.Elixir.Element - 1;
       DetailElement.sprite = ElementSprites[elementIndex];
    }

    public void CloseButtonCliked() {
        if (isDetailPenalOpen) {
            DetailPanel.SetActive(false);
            selectedElixir = null;
            return;
        }
        
        IconManager.OpenedPanelIsClosing();
        Destroy(gameObject);
    }

    public void SelectButtonCliked() {
         switch(SelectElement) {
            case Elements.Metal:
                PopulateEquippedElixir(selectedElixir, MetalIcon, MetalBanner, MetalName, MetalDetail);
                GameManager.EquipElixir(selectedElixir, Elements.Metal);
                break;
            case Elements.Wood:
                PopulateEquippedElixir(selectedElixir, WoodIcon, WoodBanner, WoodName, WoodDetail);
                GameManager.EquipElixir(selectedElixir, Elements.Wood);
                break;
            case Elements.Water:
                PopulateEquippedElixir(selectedElixir, WaterIcon, WaterBanner, WaterName, WaterDetail);
                GameManager.EquipElixir(selectedElixir, Elements.Water);
                break;
            case Elements.Fire:
                PopulateEquippedElixir(selectedElixir, FireIcon, FireBanner, FireName, FireDetail);
                GameManager.EquipElixir(selectedElixir, Elements.Fire);
                break;
            case Elements.Earth:
                PopulateEquippedElixir(selectedElixir, EarthIcon, EarthBanner, EarthName, EarthDetail);
                GameManager.EquipElixir(selectedElixir, Elements.Earth);
                break;
         }
         DetailPanel.SetActive(false);
         isDetailPenalOpen = false;
         selectedElixir = null;
    }

    public void CancelButtonClicked() {
         DetailPanel.SetActive(false);
         isDetailPenalOpen = false;
         selectedElixir = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
