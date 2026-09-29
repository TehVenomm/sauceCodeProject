// Decompiled with JetBrains decompiler
// Type: FieldMapInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

#nullable disable
public class FieldMapInfo
{
  public FieldMapTable.FieldMapTableData fieldMap;
  public CLEAR_STATUS status;

  public FieldMapInfo(FieldMapTable.FieldMapTableData _table, int _status)
  {
    this.fieldMap = _table;
    this.status = (CLEAR_STATUS) _status;
  }
}
