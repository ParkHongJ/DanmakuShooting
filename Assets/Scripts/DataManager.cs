using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Text 의 데이터를 class화 시켜 Global_data에 넘겨주는 작업해주는 manager

[System.Serializable]
public class Item
{
    public string num, name, objname,spritename,percent, explan;
    public Item(string num_, string name_,string objname_, string spritename_,string percent_,string explan_)
    {
        num = num_;
        name = name_;
        objname = objname_;
        spritename = spritename_;
        percent = percent_;
        explan = explan_;
    }
}
[System.Serializable]
public class Dialog
{
    public string num, type, name, subject,text;
    public Dialog(string num_,string type_,string name_,string subject_,string text_)
    {
        num = num_;
        type = type_;
        name = name_;
        subject = subject_;
        text = text_;
    }
}

public class DataManager : MonoBehaviour
{
    public TextAsset Item_DB;
    public TextAsset Dialog_DB;
    void Awake()
    {
        string[] line = Item_DB.text.Substring(0, Item_DB.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            Global_Data.Instance.ItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5]));
        }
        string[] line2 = Dialog_DB.text.Substring(0, Dialog_DB.text.Length - 1).Split('\n');
        for (int i = 0; i < line2.Length; i++)
        {
            string[] row = line2[i].Split('\t');
            Global_Data.Instance.DialogList.Add(new Dialog(row[0], row[1], row[2], row[3],row[4]));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
