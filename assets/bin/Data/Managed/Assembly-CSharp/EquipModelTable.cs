// Decompiled with JetBrains decompiler
// Type: EquipModelTable
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
public class EquipModelTable : Singleton<EquipModelTable>, IDataTable
{
  private EquipModelTable.Data defaultData = new EquipModelTable.Data();
  private UIntKeyTable<EquipModelTable.Data> table;

  public void CreateTable(string csv_text)
  {
    this.table = new UIntKeyTable<EquipModelTable.Data>();
    CSVReader csvReader = new CSVReader(csv_text, "type,id,face,hair,body,zbias,helm,arm,leg,hitex");
    EquipModelTable.TYPE type1 = EquipModelTable.TYPE.WEP;
    while (csvReader.NextLine())
    {
      EquipModelTable.TYPE type2 = EquipModelTable.TYPE.WEP;
      if ((bool) csvReader.Pop<EquipModelTable.TYPE>(ref type2))
        type1 = type2;
      int num1 = -1;
      if ((bool) csvReader.Pop(ref num1))
      {
        EquipModelTable.Data data = new EquipModelTable.Data();
        int num2 = 1;
        if ((bool) csvReader.Pop(ref num2) && num2 == 0)
          data.flags &= -2;
        csvReader.Pop(ref data.hairMode);
        csvReader.Pop(ref data.bodyDraw);
        num2 = 1;
        if ((bool) csvReader.Pop(ref num2) && num2 == 0)
          data.flags &= -17;
        num2 = 1;
        if ((bool) csvReader.Pop(ref num2) && num2 == 0)
          data.flags &= -3;
        num2 = 1;
        if ((bool) csvReader.Pop(ref num2) && num2 == 0)
          data.flags &= -5;
        num2 = 1;
        if ((bool) csvReader.Pop(ref num2) && num2 == 0)
          data.flags &= -9;
        csvReader.Pop(ref data.highTex);
        this.table.Add((uint) (type1 + num1), data);
      }
    }
    this.table.TrimExcess();
  }

  public EquipModelTable.Data Get(EQUIPMENT_TYPE equip_type, int model_id)
  {
    if (model_id >= 1)
    {
      EquipModelTable.TYPE type;
      switch (equip_type)
      {
        case EQUIPMENT_TYPE.ARMOR:
        case EQUIPMENT_TYPE.VISUAL_ARMOR:
          type = EquipModelTable.TYPE.BDY;
          break;
        case EQUIPMENT_TYPE.HELM:
        case EQUIPMENT_TYPE.VISUAL_HELM:
          type = EquipModelTable.TYPE.HED;
          break;
        case EQUIPMENT_TYPE.ARM:
        case EQUIPMENT_TYPE.VISUAL_ARM:
          type = EquipModelTable.TYPE.ARM;
          break;
        case EQUIPMENT_TYPE.LEG:
        case EQUIPMENT_TYPE.VISUAL_LEG:
          type = EquipModelTable.TYPE.LEG;
          break;
        default:
          type = EquipModelTable.TYPE.WEP;
          break;
      }
      EquipModelTable.Data data = this.table.Get((uint) (type + model_id));
      if (data != null)
        return data;
    }
    return this.defaultData;
  }

  public EquipModelTable.Data GetForWeapon(int model_id)
  {
    return model_id < 1 ? this.defaultData : this.Get(EQUIPMENT_TYPE.ONE_HAND_SWORD, model_id);
  }

  private enum TYPE
  {
    WEP = 10000000, // 0x00989680
    BDY = 20000000, // 0x01312D00
    HED = 30000000, // 0x01C9C380
    ARM = 40000000, // 0x02625A00
    LEG = 50000000, // 0x02FAF080
  }

  [Flags]
  private enum FLAG
  {
    FACE_DRAW = 1,
    HELM_DRAW = 2,
    ARM_DRAW = 4,
    LEG_DRAW = 8,
    Z_BIAS = 16, // 0x00000010
  }

  public class Data
  {
    public int hairMode = 1;
    public int flags = 15;
    public byte bodyDraw;
    public byte highTex;
    public const float Z_UNIT = 0.0001f;

    public float GetZBias() => (this.flags & 16 /*0x10*/) != 0 ? 0.0001f : 0.0f;

    public bool needFace => (this.flags & 1) != 0;

    public bool needHelm => (this.flags & 2) != 0;

    public bool needArm => (this.flags & 4) != 0;

    public bool needLeg => (this.flags & 8) != 0;

    public int GetHairModelID(int base_hair_model_id)
    {
      if (this.hairMode == 0)
        return -1;
      if (this.hairMode >= 10000)
        return this.hairMode;
      return this.hairMode >= 2 ? this.hairMode * 100 + base_hair_model_id % 100 : base_hair_model_id;
    }
  }
}
