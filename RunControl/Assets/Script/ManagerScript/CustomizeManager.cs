using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyLibrary;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomizeManager : MonoBehaviour
{
    bool isOperationMat;
    string itemName;
    int Addedİndex;
    string OperationName;
    string Buy;
    string Purchased;
    int Currentindex;
    int Point;
    int SavedItem;
    GameObject[] items;
    Material[] material;

    [Header("---------------Hat Operation")]
    [SerializeField] GameObject[] Hatitems;

    [Header("---------------Weapon Operation")]
    [SerializeField] GameObject[] Weaponitems;

    [Header("---------------Ninja Material Operation")]
    [SerializeField] Material DefaultMat;
    [SerializeField] SkinnedMeshRenderer _meshRender;
    [SerializeField] Material[] NinjaMat;

    [Header("---------------language Operation")]
    [SerializeField] TextMeshProUGUI[] AllText;

    [Header("---------------General Operation")]
    [SerializeField] Animator anim;
    [SerializeField] Text PointText;
    [SerializeField] Text itemNames;
    [SerializeField] Button[] BuyAndSaveButtom;
    [SerializeField] Button[] buttons;
    [SerializeField] GameObject[] GeneralPanel;
    [SerializeField] AudioSource[] GeneralSounds;

    List<ItemInfo> _ItemInfo = new List<ItemInfo>();
    MemoryManager memory = new MemoryManager();
    DataManager dataManager = new DataManager();
    List<LanguageSet> languageMainData = new List<LanguageSet>();
    List<LanguageSet> languageText = new List<LanguageSet>();
    void Start()
    {
        DataAndStartOperation();
    }
    public void ChangeItem(string ButtonName)
    {
        GeneralSounds[0].Play();
        switch (isOperationMat)
        {
            case false:
                if (ButtonName == "Next")
                {
                    if (Currentindex != -1)
                    {
                        items[Currentindex].SetActive(false);
                    }
                    Currentindex++;
                    BuyAndSaveButManager();
                   items[Currentindex].SetActive(true);
                    itemNames.text = _ItemInfo[Currentindex + Addedİndex].ItemName;
                    buttons[0].interactable = true;
                    if (Currentindex == items.Length - 1)
                    {
                        buttons[1].interactable = false;
                    }
                }
                else
                {
                    items[Currentindex].SetActive(false);
                    Currentindex--;
                    BuyAndSaveButManager();
                    buttons[1].interactable = true;
                    if (Currentindex != -1)
                    {
                        items[Currentindex].SetActive(true);
                        itemNames.text = _ItemInfo[Currentindex + Addedİndex].ItemName;
                    }
                    else
                    {
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = Purchased;
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }
                }
             break;
            case true:
                if (ButtonName == "Next")
                {
                    Currentindex++;
                    BuyAndSaveButManager();
                    Material[] mats = _meshRender.materials;
                    mats[0] = material[Currentindex];
                    _meshRender.material = mats[0];
                    itemNames.text = _ItemInfo[Currentindex + Addedİndex].ItemName;
                    buttons[0].interactable = true;
                    if (Currentindex == material.Length - 1)
                    {
                        buttons[1].interactable = false;
                    }
                }
                else
                {
                    Currentindex--;
                    BuyAndSaveButManager();
                    buttons[1].interactable = true;
                    if (Currentindex != -1)
                    {
                        Material[] mats = _meshRender.materials;
                        mats[0] = material[Currentindex];
                        _meshRender.material = mats[0];
                        itemNames.text = _ItemInfo[Currentindex + Addedİndex].ItemName;
                    }
                    else
                    {
                        Material[] mats = _meshRender.materials;
                        mats[0] = DefaultMat;
                        _meshRender.material = mats[0];
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = Purchased;
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }
                }
                break;
        }       
    }
    public void Buyitem()
    {
        GeneralSounds[1].Play();
        BuyAndSaveButtom[0].interactable = false;
        BuyAndSaveButtom[1].interactable = true;
        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = Purchased;
        Point = Point - _ItemInfo[Currentindex + Addedİndex].Point;
        PointText.text = Point.ToString();
        memory.Save_int("TotalPoint", Point);
        _ItemInfo[Currentindex + Addedİndex].Bought = true;
        dataManager.Save(_ItemInfo,"ItemDatas");
    }
    public void Saveitem()
    {
        GeneralSounds[2].Play();
        switch (OperationName)
        {
            case "Hat":
                memory.Save_int("HatIndex", Currentindex);
                break;
            case "Weapon":
                memory.Save_int("WeaponIndex", Currentindex);
                break;

            case "Material":
                memory.Save_int("MaterialIndex", Currentindex);
                break;
        }
        anim.SetBool("Play", true);
        SavedItem = Currentindex;
        BuyAndSaveButtom[1].interactable = false;
    }
    public void OpenOperationPanel(string Operation)
    {
        GeneralSounds[0].Play();
        ChooseOperation(Operation);
        BuyAndSaveButtom[0].interactable = false;
        BuyAndSaveButtom[1].interactable = false;
        BuyAndSaveButManager();
        GeneralPanel[0].SetActive(false);
        GeneralPanel[1].SetActive(true);
        GeneralPanel[2].SetActive(true);
    }
    public void CloseOperationPanel()
    {
        GeneralSounds[0].Play();
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        GeneralPanel[0].SetActive(true);
        GeneralPanel[1].SetActive(false);
        GeneralPanel[2].SetActive(false);
        SetItem(isOperationMat,items, material, CurrentItenIndex(OperationName));
        Addedİndex = 0;
    }
    public void MainMenu()
    {
        GeneralSounds[0].Play();
        SceneManager.LoadScene(0);
    }
    void ChooseOperation(string Operation)
    {
        switch (Operation)
        {
            case "Hat":
                Addedİndex = 0;
                items= Hatitems;
                isOperationMat = false;
                OperationName = "Hat";
                Currentindex= memory.Get_int("HatIndex");         
                buttonCheck(items.Length);
                break;

            case "Weapon":
                Addedİndex = Hatitems.Length;
                items= Weaponitems;
                isOperationMat =false;
                OperationName = "Weapon";
                Currentindex = memory.Get_int("WeaponIndex");
                buttonCheck(items.Length);
                break;

            case "Material":
                Addedİndex = Hatitems.Length+Weaponitems.Length;
                material = NinjaMat;
                isOperationMat =true;
                OperationName = "Material";
                Currentindex = memory.Get_int("MaterialIndex");
                buttonCheck(material.Length);
                break;
        }
        SavedItem = Currentindex;
        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = Purchased;
        if (Currentindex == -1)
            itemNames.text = itemName;
        else
            itemNames.text = _ItemInfo[Currentindex + Addedİndex].ItemName;
    }
    void SetItem(bool IsMat, GameObject[] items, Material[] mat,int Index)
    {
            if (!IsMat)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (Index!=i)
                       items[i].SetActive(false);          
                    else
                       items[i].SetActive(true);
                }
            }
            else
            {
                if (Index != -1)
                {
                    Material[] mats = _meshRender.materials;
                    mats[0] = mat[Index];
                    _meshRender.material = mats[0];
                }
                else
                {
                    Material[] mats = _meshRender.materials;
                    mats[0] = DefaultMat;
                    _meshRender.material = mats[0];
                }                 
            }
    }
    int CurrentItenIndex(string Value)
    {
        switch (Value)
        {
            case "Hat":
                return memory.Get_int("HatIndex");              
            case "Weapon":
                return memory.Get_int("WeaponIndex");             
            case "Material":
                return memory.Get_int("MaterialIndex");         
            default:
                return -1;              
        }
    }
    void buttonCheck(int length)
    {
        if (Currentindex == -1)
        {
            buttons[0].interactable = false;
        }
        else if (Currentindex == length - 1)
        {
            buttons[1].interactable = false;
        }
          
    }
    void BuyAndSaveButManager()
    {
        if (Currentindex != -1)
        {
            BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = _ItemInfo[Currentindex + Addedİndex].Point.ToString() + "-"+ Buy;
            if (_ItemInfo[Currentindex + Addedİndex].Bought)
            {
                BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = Purchased;
                SaveSitatu();
            }
            else
            {
                BuyAndSaveButtom[1].interactable = false;
                if (_ItemInfo[Currentindex + Addedİndex].Point <= Point)
                {
                    BuyAndSaveButtom[0].interactable = true;
                }
                else
                {
                    BuyAndSaveButtom[0].interactable = false;
                }
            }
        }
        else
        {
            SaveSitatu();
        }
    }
    void SaveSitatu()
    {
        BuyAndSaveButtom[0].interactable = false;
        if (SavedItem == Currentindex)
            BuyAndSaveButtom[1].interactable = false;
        else
            BuyAndSaveButtom[1].interactable = true;
    }
    void SetLanguage(string Value)
    {
        if (Value == "TR")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_TR[i].Text;
            }
            itemName = languageText[0].Language_TR[6].Text;
            Buy= languageText[0].Language_TR[3].Text;
            Purchased=languageText[0].Language_TR[7].Text;
        }
        else if (Value == "EN")
        {
            for (int i = 0; i < AllText.Length; i++)
            {
                AllText[i].text = languageText[0].Language_EN[i].Text;
            }
            itemName = languageText[0].Language_EN[6].Text;
            Buy = languageText[0].Language_EN[3].Text;
            Purchased = languageText[0].Language_EN[7].Text;
        }
    }
    void DataAndStartOperation()
    {
        Point = memory.Get_int("TotalPoint");
        PointText.text = Point.ToString();
        SetItem(false, Hatitems, null, memory.Get_int("HatIndex"));
        SetItem(false, Weaponitems, null, memory.Get_int("WeaponIndex"));
        SetItem(true, null, NinjaMat, memory.Get_int("MaterialIndex"));
        dataManager.Load("ItemDatas");
        _ItemInfo = dataManager.GetDataList();
        languageMainData = dataManager.LoadLanguageList();
        languageText.Add(languageMainData[2]);
        SetLanguage(memory.Get_String("Language"));
    }
}
