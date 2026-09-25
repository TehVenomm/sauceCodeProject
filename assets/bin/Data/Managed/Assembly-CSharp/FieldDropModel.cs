// Decompiled with JetBrains decompiler
// Type: FieldDropModel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class FieldDropModel : BaseModel
{
  public static string URL = "ajax/field/drop";

  public class RequestSendForm
  {
    public string fieldId;
    public int mapId;
    public List<int> eids = new List<int>();
    public List<FieldDropModel.RequestSendForm.EnemySignatureInfo> esigs = new List<FieldDropModel.RequestSendForm.EnemySignatureInfo>();
    public List<int> deids = new List<int>();
    public List<FieldDropModel.RequestSendForm.DropSignatureInfo> dsigs = new List<FieldDropModel.RequestSendForm.DropSignatureInfo>();

    [Serializable]
    public class EnemySignatureInfo
    {
      public int defeatKeyId;
      public string signature;
      public int sid;
      public int exp;
      public int money;
      public int ppt;
    }

    [Serializable]
    public class DropSignatureInfo
    {
      public int rewardKeyId;
      public string signature;
      public List<FieldDropModel.RequestSendForm.DropSignatureInfo.DropData> drops = new List<FieldDropModel.RequestSendForm.DropSignatureInfo.DropData>();
      public FieldDropModel.RequestSendForm.DropSignatureInfo.DeliverData deliver = new FieldDropModel.RequestSendForm.DropSignatureInfo.DeliverData();

      [Serializable]
      public class DropData
      {
        public int dropId;
        public int type;
        public int itemId;
        public int num;
        public int param_0;
      }

      [Serializable]
      public class DeliverData
      {
        public int bit;
        public int boostBit;
        public int boostNum;
        public bool isTreasure;
      }
    }
  }
}
