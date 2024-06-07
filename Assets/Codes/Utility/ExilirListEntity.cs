using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class ExilirListEntity : MonoBehaviour
{

    public Image ExilirImage, Banner;
    public Exilir Exilir;
    public Sprite[] RarityBanners;
    public Sprite[] ElementalSprites;
    public int IndexInList;
    public IExilirEntityList ListManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateImage() {
        int rarityIndex = (int)Exilir.Rarity - 2;
        Banner.sprite = RarityBanners[rarityIndex];

        int elementIndex = (int)Exilir.Element - 1;
        ExilirImage.sprite = ElementalSprites[elementIndex];

        if (Exilir.IsSpecialSprite) {
            ExilirImage.sprite = Exilir.SpecialSprite;
        }
    }

    public void SelectThisExilir() {
        ListManager.SelectEntity(IndexInList);
    }
}
