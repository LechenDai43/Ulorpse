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
    public InitialSelectionManager ISM;
    public Text Prompt, ConfirmPrompt;
    // Start is called before the first frame update
    void Start()
    {
        int offset = (int)GameManager.GetProvince();
        MalePortrait.sprite = Sprites[(offset - 1) * 2];
        MalePortrait.SetNativeSize();
        FemalePortrait.sprite = Sprites[(offset - 1) * 2 + 1];
        FemalePortrait.SetNativeSize();
        ConfirmButton.interactable = false;
        Prompt.text = ISM.SelectionLines["Select5"];

        
        MaleButton.gameObject.GetComponentInChildren<Text>().text = ISM.SelectionSceneTexts["Prince"];
        FemaleButton.gameObject.GetComponentInChildren<Text>().text = ISM.SelectionSceneTexts["Princess"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject BorderObject;
    public Sprite BorderSprite;
    public void GenderSetting(int _gender) {
        GameManager.SetGender((Genders)_gender);
        ConfirmButton.interactable = true;
        ConfirmPrompt.gameObject.SetActive(true);

        Image BorderImage;
        if (BorderObject == null) {
            BorderObject = new GameObject("Border");
            BorderImage = BorderObject.AddComponent<Image>();
            BorderImage.sprite = BorderSprite;
        }
        else {
            BorderImage = BorderObject.GetComponent<Image>();
        }        

        string ConfirmPromptValue = ISM.SelectionSceneTexts["GenderConfirm"];
        if (_gender == 1) {
            BorderObject.transform.SetParent(MalePortrait.transform, false);
            RectTransform ImageRect = MalePortrait.rectTransform;
            BorderImage.rectTransform.sizeDelta = new Vector2(ImageRect.rect.width + 15 * 2, ImageRect.rect.height + 15 * 2);
            ConfirmPromptValue = ConfirmPromptValue.Replace("{%gender%}", ISM.SelectionSceneTexts["Prince"].Replace("Åâ",""));
            GameManager.SetCharacterPortrait(MalePortrait.sprite);
        }
        else if (_gender == 2) {
            BorderObject.transform.SetParent(FemalePortrait.transform, false);
            RectTransform ImageRect = FemalePortrait.rectTransform;
            BorderImage.rectTransform.sizeDelta = new Vector2(ImageRect.rect.width + 15 * 2, ImageRect.rect.height + 15 * 2);
            ConfirmPromptValue = ConfirmPromptValue.Replace("{%gender%}", ISM.SelectionSceneTexts["Princess"].Replace("Åä",""));
            GameManager.SetCharacterPortrait(FemalePortrait.sprite);
        }
        else {
            return;
        }
        ConfirmPrompt.text = ConfirmPromptValue;
    }

    public InputField FirstName, LastName;
    public Text FirstNameLable, LastNameLable;
    private bool _GenderSelected;
    public void ContinueButtonClicked() {
        if (!_GenderSelected) {
            ConfirmPrompt.gameObject.SetActive(false);
            BorderObject.SetActive(false);
            MaleButton.gameObject.SetActive(false);
            FemaleButton.gameObject.SetActive(false);
            MalePortrait.gameObject.SetActive(GameManager.GetGender() == Genders.Male);
            FemalePortrait.gameObject.SetActive(GameManager.GetGender() == Genders.Female);
            FirstName.gameObject.SetActive(true);
            LastName.gameObject.SetActive(true);
            FirstNameLable.text = ISM.SelectionSceneTexts["FirstName"];
            LastNameLable.text = ISM.SelectionSceneTexts["LastName"];
            _GenderSelected = true;
            Prompt.text = ISM.SelectionLines["Select6"];

            switch (GameManager.GetProvince())
            {
                case Provinces.Azure:
                    LastName.text = ISM.SelectionSceneTexts["House1"];
                    break;
                case Provinces.Tranquil:
                    LastName.text = ISM.SelectionSceneTexts["House2"];
                    break;
                case Provinces.Indulge:
                    LastName.text = ISM.SelectionSceneTexts["House3"];
                    break;
            }


            
            ConfirmButton.interactable = false;
        }
        else {
            GameManager.SetFirstName(FirstName.text);
            GameManager.SetLastName(LastName.text);
            GameManager.Save();
        }
    }

    public void CheckNullInput() {
        if (FirstName.text == "" || LastName.text == "") {
            ConfirmButton.interactable = false;
        } 
        else {            
            ConfirmButton.interactable = true;
        }
    }

}
