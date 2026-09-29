// Decompiled with JetBrains decompiler
// Type: StageTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class StageTable : Singleton<StageTable>, IDataTable
{
  public StringKeyTable<StageTable.StageData> dataTable { get; private set; }

  public void CreateTable(string csv_text)
  {
    this.dataTable = TableUtility.CreateStringKeyTable<StageTable.StageData>(csv_text, new TableUtility.CallBackStringKeyReadCSV<StageTable.StageData>(StageTable.StageData.cb), "name,scene,ground,sky,attributeID,cameraLinkEffect,cameraLinkEffectY0,rootEffect,useEffect0,useEffect1,useEffect2,useEffect3,useEffect4,useEffect5,useEffect6,useEffect7");
    this.dataTable.TrimExcess();
  }

  public StageTable.StageData GetData(string name)
  {
    return this.dataTable == null ? (StageTable.StageData) null : this.dataTable.Get(name);
  }

  public class StageData
  {
    public const int USE_EFFECT_NUM = 8;
    public string scene;
    public string ground;
    public string sky;
    public int attributeID;
    public string cameraLinkEffect;
    public string cameraLinkEffectY0;
    public string rootEffect;
    public string[] useEffects = new string[8];
    public const string NT = "name,scene,ground,sky,attributeID,cameraLinkEffect,cameraLinkEffectY0,rootEffect,useEffect0,useEffect1,useEffect2,useEffect3,useEffect4,useEffect5,useEffect6,useEffect7";

    public static bool cb(CSVReader csv, StageTable.StageData data, ref string key)
    {
      csv.Pop(ref data.scene);
      csv.Pop(ref data.ground);
      csv.Pop(ref data.sky);
      data.attributeID = 1;
      csv.Pop(ref data.attributeID);
      csv.Pop(ref data.cameraLinkEffect);
      csv.Pop(ref data.cameraLinkEffectY0);
      csv.Pop(ref data.rootEffect);
      for (int index = 0; index < 8; ++index)
        csv.Pop(ref data.useEffects[index]);
      return true;
    }
  }
}
