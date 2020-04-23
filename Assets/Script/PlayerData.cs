using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class MyPlayerData
{
    public int StageNumber;
    public int StageScore;
}
public class PlayerData : MonoBehaviour {

    // Use this for initialization

    public int DataSize = 10;
    public MyPlayerData[] m_Data;
    void Awake()
    {
        LoadData();
    }
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetStageScore(int iStageNumber)
    {
        return m_Data[iStageNumber - 1].StageScore;
    }

    public void SaveData(int iStageNumber, int iScore)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.dataPath + "/PlayerInfoData.dat", FileMode.Create);

        for(int i = 0; i < DataSize; ++i)
        {
            if (i == iStageNumber - 1)
            {
                MyPlayerData data = new MyPlayerData();
                data.StageNumber = iStageNumber;
                if (m_Data[i].StageScore < iScore)
                    data.StageScore = iScore;
                else
                    data.StageScore = m_Data[i].StageScore;
                bf.Serialize(file, data);
            }
            else
                bf.Serialize(file, m_Data[i]);
        }
        file.Close();
    }

    public void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(Application.dataPath + "/PlayerInfoData.dat", FileMode.OpenOrCreate);
        if (file != null && file.Length > 0)
        {
            for (int i = 0; i < DataSize; ++i)
            {
                MyPlayerData Data = (MyPlayerData)bf.Deserialize(file);
                m_Data[i] = Data;
            }
           
        }
        else if(file != null)
        {
            MyPlayerData data = new MyPlayerData();
            for (int i = 0; i < DataSize; ++i)
            {
                data.StageNumber = i + 1;
                data.StageScore = 0;
                bf.Serialize(file, data);
            }
        }
        file.Close();
    }
}
