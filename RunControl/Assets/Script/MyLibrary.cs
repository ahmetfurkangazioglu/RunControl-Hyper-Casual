using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyLibrary
{
    public class MathOperations : MonoBehaviour
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
}
