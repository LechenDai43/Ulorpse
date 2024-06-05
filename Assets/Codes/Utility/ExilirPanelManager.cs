using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;
using UnityEngine.Audio;

public class ExilirPanelManager : MonoBehaviour
{

    public ScrollRect ExilirListScrollView;
    public RectTransform content;
    public GameObject ExilirPrefab;
    public Image Enlargement, Banner, Element;
    public Text Name, Description;
    public Sprite[] Elements;

    private Exilir[] Exilirs;
    private ExilirListEntity[] Entities;
    // Start is called before the first frame update
    void Start()
    {
        content = ExilirListScrollView.content;
        Exilirs = GameManager.GetExilirs();
        Array.Sort(Exilirs, new ExilirComparer());
        Entities = new ExilirListEntity[Exilirs.Length];

        for (int i = 0; i < Exilirs.Length; i++) {
            GameObject newItem = Instantiate(ExilirPrefab, content);
            ExilirListEntity entity = newItem.gameObject.GetComponent<ExilirListEntity>();
            entity.Exilir = Exilirs[i];
            entity.IndexInList = i;
            entity.ListManager = this;
            Entities[i] = entity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEntity(int index) {
        if (index < Exilirs.Length) {
            ExilirListEntity entity = Entities[index];
            Enlargement.sprite = entity.ExilirImage.sprite;
            Banner.sprite = entity.Banner.sprite;
            Name.text = entity.Exilir.Name;
            Description.text = entity.Exilir.Description;
            int elementIndex = (int)entity.Exilir.Element - 1;
            Element.sprite = Elements[elementIndex];
        }
    }
}