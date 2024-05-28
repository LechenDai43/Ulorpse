using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class CharacterSetting : MonoBehaviour
{

    public Sprite[] Sprites;
    public Image MalePortrait, FemalePortrait;
    public Button MaleButton, FemaleButton, ConfirmButton;
    // Start is called before the first frame update
    void Start()
    {
        int offset = (int)GameManager.GetProvince();
        MalePortrait.sprite = Sprites[(offset - 1) * 2];
        MalePortrait.SetNativeSize();
        FemalePortrait.sprite = Sprites[(offset - 1) * 2 + 1];
        FemalePortrait.SetNativeSize();
        ConfirmButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenderSetting(int _gender) {
        GameManager.SetGender((Genders)_gender);
        ConfirmButton.interactable = true;
    }

    public InputField FirstName, LastName;
    private bool _GenderSelected;
    public void ContinueButtonClicked() {
        if (!_GenderSelected) {
            MaleButton.gameObject.SetActive(false);
            FemaleButton.gameObject.SetActive(false);
            MalePortrait.gameObject.SetActive(false);
            FemalePortrait.gameObject.SetActive(false);
            FirstName.gameObject.SetActive(true);
            LastName.gameObject.SetActive(true);
            _GenderSelected = true;
        }
    }

}
