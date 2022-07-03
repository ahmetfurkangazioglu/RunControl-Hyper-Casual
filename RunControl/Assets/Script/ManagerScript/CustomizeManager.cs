using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyLibrary;

public class CustomizeManager : MonoBehaviour
{
    string isOperationMat;
    string itemName;
    int Added›ndex;
    GameObject[] items;
    [Header("Hat Operation")]
    public GameObject[] Hatitems;

    [Header("Weapon Operation")]
    public GameObject[] Weaponitems;

   [Header("Ninja Material Operation")]
    public Material DefaultMat;
    public SkinnedMeshRenderer _meshRender;
    public Material[] NinjaMat;

    [Header("General Operation")]
    public int Currentindex;
    public Text itemNames;
    public Button[] buttons;
    public GameObject[] GeneralPanel;
    public List<ItemInfo> _ItemInfo = new List<ItemInfo>();

    MemoryManager memory = new MemoryManager();
    DataManager dataManager = new DataManager();


    void Start()
    {
        memory.Save_int("CurrentItem", Currentindex);
        Currentindex = memory.Get_int("CurrentItem");
        if (Currentindex ==-1)
        {
            itemNames.text = "ﬁapka yok";
            buttons[0].interactable = false;
        }
        else if (Currentindex== Hatitems.Length-1)
        {
            buttons[1].interactable = false;
        }
        dataManager.Load("ItemDatas");
        _ItemInfo = dataManager.GetDataList();
    }

    public void ChangeItem(string ButtonName)
    {
        switch (isOperationMat)
        {
            case "No":

                if (ButtonName == "Next")
                {
                    if (Currentindex != -1)
                    {
                        items[Currentindex].SetActive(false);
                    }
                    Currentindex++;
                    items[Currentindex].SetActive(true);
                    itemNames.text = _ItemInfo[Currentindex + Added›ndex].ItemName;
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
                        items[Currentindex].SetActive(true);
                        itemNames.text = _ItemInfo[Currentindex + Added›ndex].ItemName;
                    }
                    else
                    {
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }

                }
             break;

            case "Yes":

                if (ButtonName == "Next")
                {
                    Currentindex++;
                    Material[] mats = _meshRender.materials;
                    mats[0] = NinjaMat[Currentindex];
                    _meshRender.material = mats[0];
                    itemNames.text = _ItemInfo[Currentindex + Added›ndex].ItemName;
                    buttons[0].interactable = true;
                    if (Currentindex == NinjaMat.Length - 1)
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
                        mats[0] = NinjaMat[Currentindex];
                        _meshRender.material = mats[0];
                        itemNames.text = _ItemInfo[Currentindex + Added›ndex].ItemName;
                    }
                    else
                    {
                        Material[] mats = _meshRender.materials;
                        mats[0] = DefaultMat;
                        _meshRender.material = mats[0];
                        buttons[0].interactable = false;
                        itemNames.text = itemName;
                    }

                }
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
        buttons[0].interactable = false;
        buttons[1].interactable = true;
        GeneralPanel[0].SetActive(true);
        GeneralPanel[1].SetActive(false);
        GeneralPanel[2].SetActive(false);
        Added›ndex = 0;
        Currentindex = -1; 
    }

    private void ChooseOperation(string Operation)
    {
        switch (Operation)
        {
            case "Hat":
                Added›ndex = 0;
                items= Hatitems;
                isOperationMat = "No";
                itemName = "ﬁapka Yok";
                break;

            case "Weapon":
                Added›ndex = Hatitems.Length;
                items= Weaponitems;
                isOperationMat = "No";
                itemName = "Silah Yok";
                break;

            case "Material":
                Added›ndex = Hatitems.Length+Weaponitems.Length;
                isOperationMat = "Yes";
                itemName = "Kost¸m Yok";
                break;
        }
       
    }
}
