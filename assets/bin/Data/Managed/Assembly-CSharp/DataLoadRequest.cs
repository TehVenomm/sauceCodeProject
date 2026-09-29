// Decompiled with JetBrains decompiler
// Type: DataLoadRequest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;

#nullable disable
public class DataLoadRequest
{
  private string directory;
  private bool enableLoadBinary = true;
  public Action<byte[]> processCompressedTextData;
  public Action<byte[]> processCompressedBinaryData;
  public List<DataLoadRequest> depReqs = new List<DataLoadRequest>();

  public string name { get; private set; }

  public IDataTableRequestHash hash { get; private set; }

  public IDataTableRequestHash hashBinary { get; private set; }

  public string filename
  {
    get => this.GetName().ToLower() + GoGameResourceManager.GetDefaultAssetBundleExtension();
  }

  public string GetName() => this.enableLoadBinaryData ? this.name + "_b" : this.name;

  public IDataTableRequestHash GetHash() => this.enableLoadBinaryData ? this.hashBinary : this.hash;

  public string path => $"{this.directory}/{this.filename}?v={this.GetHash().ToString()}";

  public bool downloadOnly { get; private set; }

  public bool enableLoadBinaryData
  {
    get => this.processCompressedBinaryData != null && this.enableLoadBinary;
    set => this.enableLoadBinary = value;
  }

  public event System.Action onComplete;

  public event Action<DataTableLoadError> onError;

  public event Func<string, bool> onVerifyError;

  public float progress { get; set; }

  public DataTableLoadError error { get; private set; }

  public bool isCompleted { get; private set; }

  public DataLoadRequest(
    string name,
    IDataTableRequestHash hash,
    string directory,
    bool downloadOnly)
  {
    this.name = name;
    this.hash = hash;
    this.directory = directory;
    this.downloadOnly = downloadOnly;
  }

  public void Reset()
  {
    this.progress = 0.0f;
    this.enableLoadBinary = true;
    this.error = DataTableLoadError.None;
    this.isCompleted = false;
  }

  public void DependsOn(DataLoadRequest depReq) => this.depReqs.Add(depReq);

  public void OnComplete()
  {
    this.isCompleted = true;
    if (this.onComplete == null)
      return;
    this.onComplete();
  }

  public void OnError(DataTableLoadError error)
  {
    this.error = error;
    this.onError(error);
  }

  public bool OnVerifyError(string hash) => this.onVerifyError(hash);

  public void SetupLoadBinary(DataTableManifest manifest, Action<byte[]> processBinary)
  {
    this.enableLoadBinary = true;
    this.processCompressedBinaryData = processBinary;
    this.hashBinary = (IDataTableRequestHash) manifest.GetTableHash(this.GetName());
    if (this.hashBinary != null)
      return;
    this.enableLoadBinary = false;
  }
}
