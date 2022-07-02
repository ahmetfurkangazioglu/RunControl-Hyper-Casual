using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyLibrary;

public class CustomizeManager : MonoBehaviour
{
    public int Currentindex;
    [Header("Item Setting")]
    public GameObject[] Hatitems;
    public GameObject[] Weaponitems;
    public Material[] NinjaMat;
    public Text HatNames;
    [Header("Other Setting")]
    public Button[] buttons;

    MemoryManager memory = new MemoryManager();
    public List<ItemInfo> _ItemInfo = new List<ItemInfo>();
    DataManager dataManager = new DataManager();


    void Start()
    {
        memory.Save_int("CurrentItem", Currentindex);
        Currentindex = memory.Get_int("CurrentItem");
        if (Currentindex ==-1)
        {
            HatNames.text = "Þapka yok";
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
        if (ButtonName=="Next")
        {
            if (Currentindex != -1)
            {
                Hatitems[Currentindex].SetActive(false);
            }
            Currentindex++;
            Hatitems[Currentindex].SetActive(true);
            HatNames.text = _ItemInfo[Currentindex].ItemName;
            buttons[0].interactable = true;
            if (Currentindex== Hatitems.Length-1)
            {
                buttons[1].interactable = false;
            }
 
        }
        else
        {
            Hatitems[Currentindex].SetActive(false);
            Currentindex--;
            buttons[1].interactable = true;
            if (Currentindex!=-1)
            {
                Hatitems[Currentindex].SetActive(true);
                HatNames.text = _ItemInfo[Currentindex].ItemName;
            }
            else
            {
                buttons[0].interactable = false;
                HatNames.text = "Þapka yok";
            }
           
        }
    }
}
