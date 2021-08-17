using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Text 의 데이터를 class화 시켜 Global_data에 넘겨주는 작업해주는 manager

[System.Serializable]
public class Item
{
    public string num, objname, objtype,modelname,itemname ,explan,percent;
    public Item(string num_, string name_,string objtype_, string spritename_,string itemname_, string explan_, string percent_)
    {
        num = num_;
        objname = name_;
        objtype = objtype_;
        modelname = spritename_;
        itemname = itemname_;
        explan = explan_;
        percent = percent_;
      
    }
}
[System.Serializable]
public class Dialog
{
    public string num, type, name, subject,image_n,text;
    public Dialog(string num_,string type_,string name_,string subject_,string image_,string text_)
    {
        num = num_;
        type = type_;
        name = name_;
        subject = subject_;
        image_n = image_;
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
            Global_Data.Instance.ItemList.Add(new Item(row[0], row[1], row[2], row[3], row[4], row[5], row[6]));
        }
        string[] line2 = Dialog_DB.text.Substring(0, Dialog_DB.text.Length - 1).Split('\n');
        for (int i = 0; i < line2.Length; i++)
        {
            string[] row = line2[i].Split('\t');
            Global_Data.Instance.DialogList.Add(new Dialog(row[0], row[1], row[2], row[3],row[4], row[5]));
        }
        print(Item_DB);
        
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
