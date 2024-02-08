using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class DataManager : Singleton<DataManager>
{
    public string weaponPath; 
     public WeaponSO weaponSO;

     public void Start()
     {
         Init();
     }

     public void Init()
     {
         weaponPath = Application.streamingAssetsPath + "\\weaponData.json";
         if (!File.Exists(weaponPath))
         {
             File.Create(weaponPath);
         }

         SaveWeaponData();

     }

     public void SaveWeaponData()
     {
         try
         {
             List<IWeaponData> weaponDatas = new List<IWeaponData>();
             weaponDatas.AddRange(weaponSO.rangedWeapons);
             weaponDatas.AddRange(weaponSO.meleeWeapons);
             foreach (var item in weaponDatas)
             {
                 print(weaponDatas.Count);
             }

             Dictionary<string, RangedData[]> weaponDataDict = new Dictionary<string, RangedData[]>();
             weaponDataDict.Add("weapons", weaponSO.rangedWeapons);
             string s = JsonConvert.SerializeObject(weaponDataDict, Formatting.Indented);
             File.WriteAllText(weaponPath, s);
         }
         catch(JsonException e)
         {
             print(e);
         }
     }

     public static void SaveBestTime(float time)
     {
         PlayerPrefs.SetFloat("BestTime", time);
     }

     public static float LoadBestTime()
     {
         return PlayerPrefs.GetFloat("BestTime");
     }
}
