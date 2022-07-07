using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyLibrary;
using TMPro;

public class CustomizeManager : MonoBehaviour
{
    bool isOperationMat;
    string itemName;
    int AddedÝndex;
    string OperationName;
    GameObject[] items;
    Material[] material;
    [Header("Hat Operation")]
    public GameObject[] Hatitems;

    [Header("Weapon Operation")]
    public GameObject[] Weaponitems;

   [Header("Ninja Material Operation")]
    public Material DefaultMat;
    public SkinnedMeshRenderer _meshRender;
    public Material[] NinjaMat;

    [Header("General Operation")]
    int Currentindex;
    public Text itemNames;
    public Button[] BuyAndSaveButtom;
    public Button[] buttons;
    public GameObject[] GeneralPanel;
    public List<ItemInfo> _ItemInfo = new List<ItemInfo>();

    MemoryManager memory = new MemoryManager();
    DataManager dataManager = new DataManager();


    void Start()
    {
        SetItem(false, Hatitems, null, memory.Get_int("HatIndex"));
        SetItem(false, Weaponitems, null, memory.Get_int("WeaponIndex"));
        SetItem(true, null, NinjaMat, memory.Get_int("MaterialIndex"));
        dataManager.Load("ItemDatas");
        _ItemInfo = dataManager.GetDataList();
    }

    public void ChangeItem(string ButtonName)
    {
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
                    items[Currentindex].SetActive(true);
                   BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = _ItemInfo[Currentindex + AddedÝndex].Point.ToString() +"- Satýn al";
                    itemNames.text = _ItemInfo[Currentindex + AddedÝndex].ItemName;
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
                    buttons[1].interactable = true;
                    if (Currentindex != -1)
                    {
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = _ItemInfo[Currentindex + AddedÝndex].Point.ToString() + "- Satýn al";
                        items[Currentindex].SetActive(true);
                        itemNames.text = _ItemInfo[Currentindex + AddedÝndex].ItemName;
                    }
                    else
                    {
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = "Satýn alýnamaz";
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }

                }
             break;

            case true:

                if (ButtonName == "Next")
                {
                    Currentindex++;
                    Material[] mats = _meshRender.materials;
                    mats[0] = material[Currentindex];
                    _meshRender.material = mats[0];
                    BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = _ItemInfo[Currentindex + AddedÝndex].Point.ToString() + "- Satýn al";
                    itemNames.text = _ItemInfo[Currentindex + AddedÝndex].ItemName;
                    buttons[0].interactable = true;
                    if (Currentindex == material.Length - 1)
                    {
                        buttons[1].interactable = false;
                    }
                }
                else
                {
                    Currentindex--;
                    buttons[1].interactable = true;
                    if (Currentindex != -1)
                    {
                        Material[] mats = _meshRender.materials;
                        mats[0] = material[Currentindex];
                        _meshRender.material = mats[0];
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text = _ItemInfo[Currentindex + AddedÝndex].Point.ToString() + "- Satýn al";
                        itemNames.text = _ItemInfo[Currentindex + AddedÝndex].ItemName;
                    }
                    else
                    {
                        Material[] mats = _meshRender.materials;
                        mats[0] = DefaultMat;
                        _meshRender.material = mats[0];
                        BuyAndSaveButtom[0].GetComponentInChildren<TextMeshProUGUI>().text ="- Satýn alýnamaz";
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }

                }
                break;
        }       
    }
    public void Buyitem()
    {

    }
    public void Saveitem()
    {
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
    }

    public void OpenOperationPanel(string Operation)
    {
        ChooseOperation(Operation);    
        GeneralPanel[0].SetActive(false);
        GeneralPanel[1].SetActive(true);
        GeneralPanel[2].SetActive(true);
    }
    public void CloseOperationPanel()
    {
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        GeneralPanel[0].SetActive(true);
        GeneralPanel[1].SetActive(false);
        GeneralPanel[2].SetActive(false);
        SetItem(isOperationMat,items, material, CurrentItenIndex(OperationName));
        AddedÝndex = 0;
    }

    private void ChooseOperation(string Operation)
    {
        switch (Operation)
        {
            case "Hat":
                AddedÝndex = 0;
                items= Hatitems;
                isOperationMat = false;
                itemName = "Þapka Yok";
                OperationName = "Hat";
                Currentindex= memory.Get_int("HatIndex");
                buttonCheck(items.Length);
                break;

            case "Weapon":
                AddedÝndex = Hatitems.Length;
                items= Weaponitems;
                isOperationMat =false;
                itemName = "Silah Yok";
                OperationName = "Weapon";
                Currentindex = memory.Get_int("WeaponIndex");
                buttonCheck(items.Length);
                break;

            case "Material":
                AddedÝndex = Hatitems.Length+Weaponitems.Length;
                material = NinjaMat;
                isOperationMat =true;
                itemName = "Kostüm Yok";
                OperationName = "Material";
                Currentindex = memory.Get_int("MaterialIndex");
                buttonCheck(material.Length);
                break;
        }
    }
    private void SetItem(bool IsMat, GameObject[] items, Material[] mat,int Index)
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
    private int CurrentItenIndex(string Value)
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
    private void buttonCheck(int length)
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
}
