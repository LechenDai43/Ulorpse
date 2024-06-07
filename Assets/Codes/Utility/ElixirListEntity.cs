using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class ElixirListEntity : MonoBehaviour
{

    public Image ElixirImage, Banner;
    public Elixir Elixir;
    public Sprite[] RarityBanners;
    public Sprite[] ElementalSprites;
    public int IndexInList;
    public IElixirEntityList ListManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateImage() {
        int rarityIndex = (int)Elixir.Rarity - 2;
        Banner.sprite = RarityBanners[rarityIndex];

        int elementIndex = (int)Elixir.Element - 1;
        ElixirImage.sprite = ElementalSprites[elementIndex];

        if (Elixir.IsSpecialSprite) {
            ElixirImage.sprite = Elixir.SpecialSprite;
        }
    }

    public void SelectThisElixir() {
        ListManager.SelectEntity(IndexInList);
    }
}
