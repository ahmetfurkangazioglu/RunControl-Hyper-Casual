using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace MyLibrary
{
    public class MathOperations
    {
        public static void Multiply(int Value, List<GameObject> NpcPooling,Transform position,List<GameObject> CreateEffectPooling)
        {
            int ForValue = (MainControl.NpcAmount * Value) - MainControl.NpcAmount;
            int Commit = 0;
            foreach (var item in NpcPooling)
            {
                if (Commit < ForValue)
                {
                    NpcPoolPositive(Commit, item, position, CreateEffectPooling, out Commit);
                }
                else
                {
                    MainControl.NpcAmount *= Value;
                    break;
                }

            }
        }
        public static void Plus(int Value, List<GameObject> NpcPooling, Transform position, List<GameObject> CreateEffectPooling)
        {
           
            int Commit = 0;
            foreach (var item in NpcPooling)
            {
                if (Commit < Value)
                {
                    NpcPoolPositive(Commit, item, position, CreateEffectPooling,out Commit);
                }
                else
                {
                    MainControl.NpcAmount += Value;
                    break;
                }

            }
        }    
        public static void Minus(int Value, List<GameObject> NpcPooling, List<GameObject> DeadEffectPooling)
        {

            int Commit = 0;
            foreach (var item in NpcPooling)
            {
                if (Value>= MainControl.NpcAmount)
                {
                    NpcPoolNegative(Commit, item, DeadEffectPooling, true, out Commit);
                }
                else
                {
                    if (Commit < Value)
                    {
                        NpcPoolNegative(Commit, item, DeadEffectPooling, false, out Commit);
                    }
                    else
                    {
                        MainControl.NpcAmount -= Value;
                        break;
                    }

                }

            }
        }    
        public static void Divided(int Value, List<GameObject> NpcPooling, List<GameObject> DeadEffectPooling)
        {
            int Mod = MainControl.NpcAmount % Value;
            int ForValue = (MainControl.NpcAmount - (MainControl.NpcAmount / Value))- Mod;
            int Commit = 0;
            foreach (var item in NpcPooling)
            {
                if (Value >= MainControl.NpcAmount)
                {
                   NpcPoolNegative(Commit, item, DeadEffectPooling, true, out Commit);
                }
                else
                {
                    if (Commit < ForValue)
                    {
                        NpcPoolNegative(Commit, item, DeadEffectPooling, false, out Commit);
                    }
                    else
                    {                      
                        MainControl.NpcAmount = (MainControl.NpcAmount/Value)+Mod;
                        break;
                    }

                }

            }
        }
        public static void EffectPoolingManager(List<GameObject> EffectPooling,GameObject Obje)
        {
            foreach (var item2 in EffectPooling)
            {
                if (!item2.activeInHierarchy)
                {
                    Vector3 pos = new Vector3(Obje.transform.position.x, Obje.transform.position.y, Obje.transform.position.z +0.2f);
                    item2.SetActive(true);
                    item2.transform.position = pos;
                    item2.GetComponent<ParticleSystem>().Play();
                    item2.GetComponent<AudioSource>().Play();
                    break;
                }           
            }
        }
        public static void BodyStain(List<GameObject> BodyStain, GameObject Obje)
        {
            foreach (var item2 in BodyStain)
            {
                if (!item2.activeInHierarchy)
                {
                    item2.SetActive(true);
                    item2.transform.position = Obje.transform.position;
                    break;
                }
            }
        }
        private static void NpcPoolPositive(int Commit, GameObject Npc, Transform position, List<GameObject> CreateEffectPooling, out int OutNumber)
        {
            if (!Npc.activeInHierarchy)
            {
                Npc.transform.position = position.position;
                Npc.SetActive(true);
                EffectPoolingManager(CreateEffectPooling, Npc);
                Commit++;
            }
            OutNumber = Commit;
        }
        private static void NpcPoolNegative(int Commit, GameObject Npc, List<GameObject> DeadEffectPooling, bool reset, out int OutNumber)
        {
            if (Npc.activeInHierarchy)
            {
                EffectPoolingManager(DeadEffectPooling, Npc);
                Npc.transform.position = Vector3.zero;
                Npc.SetActive(false);
                if (!reset)
                {
                    Commit++;
                }
                else
                    MainControl.NpcAmount = 1;
            }
            OutNumber = Commit;
        }
    }
    public class MemoryManager
    {
        public void Save_int(string Name, int Value)
        {
            PlayerPrefs.SetInt(Name, Value);
        }
        public void Save_string(string Name, string Value)
        {
            PlayerPrefs.SetString(Name, Value);
        }
        public void Save_float(string Name, float Value)
        {
            PlayerPrefs.SetFloat(Name, Value);
        }
        public int Get_int(string Name)
        {
            return PlayerPrefs.GetInt(Name);
        }
        public string  Get_String(string Name)
        {
            return PlayerPrefs.GetString(Name);
        }
        public float Get_Float(string Name)
        {
            return PlayerPrefs.GetFloat(Name);
        }
        public void PrefsControl()
        {
            if (!PlayerPrefs.HasKey("FinalLevel"))
            {
                Save_string("FinalLevel", null);
                Save_int("CurrentLevel", 5);
                Save_int("TotalPoint", 850);
                Save_int("HatIndex", -1);
                Save_int("WeaponIndex", -1);
                Save_int("MaterialIndex", -1);

            }
        }
    }
    public class DataManager
    {
        public void Save(List<ItemInfo> ItemInfo,string FileName,string FileMode="gd")
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath +"/"+FileName+"."+FileMode);
            bf.Serialize(file, ItemInfo);
            file.Close();
        }
        public void FirstSave(List<ItemInfo> ItemInfo, string FileName, string FileMode = "gd")
        {
            if (!File.Exists(Application.persistentDataPath + "/" + FileName + "." + FileMode))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + FileName + "." + FileMode);
                bf.Serialize(file, ItemInfo);
                file.Close();
            }
        }
        List<ItemInfo> IteminList;
        public void Load(string FileName, string FileMode = "gd")
        {
            if (File.Exists(Application.persistentDataPath + "/" + FileName + "." + FileMode))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.OpenRead(Application.persistentDataPath + "/" + FileName + "." + FileMode);
                IteminList = (List<ItemInfo>)bf.Deserialize(file);
                file.Close();
            }
        }
        public List<ItemInfo> GetDataList()
        {
            return IteminList;
        }
    }
    [Serializable]
    public class ItemInfo
    {
        public int ItemGroup;
        public int ItemId;
        public string ItemName;
        public int Point;
        public bool Bought;
    }
}
