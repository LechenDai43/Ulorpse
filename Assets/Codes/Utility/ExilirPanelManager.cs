using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.Audio;

public class ElixirPanelManager : MonoBehaviour, IElixirEntityList
{

    public ScrollRect ElixirListScrollView;
    public RectTransform content;
    public GameObject ElixirPrefab;
    public Image Enlargement, Banner, Element;
    public Text Name, Description;
    public Sprite[] Elements;
    public Button CloseButton;
    public IconManager IconManager;

    public Elixir[] Elixirs;
    public ElixirListEntity[] Entities;
    // Start is called before the first frame update
    void Start()
    {
        content = ElixirListScrollView.content;
        Elixirs = GameManager.GetElixirs();
        Entities = new ElixirListEntity[Elixirs.Length];

        for (int i = 0; i < Elixirs.Length; i++) {
            GameObject newItem = Instantiate(ElixirPrefab, content);
            ElixirListEntity entity = newItem.gameObject.GetComponent<ElixirListEntity>();
            entity.Elixir = Elixirs[i];
            entity.IndexInList = i;
            entity.ListManager = this;
            entity.PopulateImage();
            Entities[i] = entity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEntity(int index) {
        if (index < Elixirs.Length) {
            ElixirListEntity entity = Entities[index];
            Enlargement.sprite = entity.ElixirImage.sprite;
            Banner.sprite = entity.Banner.sprite;
            Name.text = entity.Elixir.GetLocalizedName();
            Description.text = entity.Elixir.GetLocalizedDescription();
            int elementIndex = (int)entity.Elixir.Element - 1;
            Element.sprite = Elements[elementIndex];
        }
    }

    public void CloseButtonClicked() {
        IconManager.OpenedPanelIsClosing();
        Destroy(gameObject);
    }
}
