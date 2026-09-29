// Decompiled with JetBrains decompiler
// Type: UIWaveMatchAnnounce
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class UIWaveMatchAnnounce : UIAnnounceBase<UIWaveMatchAnnounce>
{
  private const int kCountDownSec = 5;
  private const string kCountDownSprPrefix = "WaveEncount_";
  private const float kCutInDispSec = 2f;
  private const string kCutInSprPrefix = "WaveCount_";
  [SerializeField]
  protected UILabel timeLabel;
  [SerializeField]
  protected GameObject CountDownObj;
  [SerializeField]
  protected UISprite CountDownSpr;
  [SerializeField]
  protected UITweener[] CutInAnim;
  [SerializeField]
  protected GameObject CutInObj;
  [SerializeField]
  protected GameObject CutInNumberObj;
  [SerializeField]
  protected UISprite CutInNumberSpr10;
  [SerializeField]
  protected UISprite CutInNumberSpr01;
  [SerializeField]
  protected GameObject CutInFinalObj;
  private InGameSettingsManager.WaveMatchParam wmSetting;
  private Coop_Model_WaveMatchInfo wmInfo;
  private UIWaveMatchAnnounce.eState state;
  private float countSec;
  private int lastInteger;
  private bool isShowWave;
  private int dispDispSec = 3;
  private bool isEvent;

  protected override float GetDispSec() => (float) this.dispDispSec;

  public void Announce(Coop_Model_WaveMatchInfo info)
  {
    this.wmInfo = info;
    this.isEvent = QuestManager.IsValidInGameWaveMatch(true);
    ((Behaviour) this.CutInAnim[0]).enabled = false;
    this.CutInAnim[0].ResetToBeginning();
    ((Behaviour) this.CutInAnim[1]).enabled = false;
    this.CutInAnim[1].ResetToBeginning();
    if (this.wmInfo.popGuardSec <= 0)
      this.StartCutIn();
    else if (this.wmInfo.popGuardSec <= 5)
      this.StartCountDown();
    else
      this.StartAnnounce();
    MonoBehaviourSingleton<InGameProgress>.I.SetWaveMatchWave(info.no);
  }

  private void Update()
  {
    switch (this.state)
    {
      case UIWaveMatchAnnounce.eState.Announce:
        this.UpdateAnnounce();
        break;
      case UIWaveMatchAnnounce.eState.CountDown:
        this.UpdateCountDown();
        break;
      case UIWaveMatchAnnounce.eState.CutIn:
        this.UpdateCutIn();
        break;
    }
    if (!MonoBehaviourSingleton<UIQuestInfoWaveMatch>.IsValid() || !this.isShowWave)
      return;
    MonoBehaviourSingleton<UIQuestInfoWaveMatch>.I.SetWaveNow(this.wmInfo.no, this.wmInfo.finalNo, this.wmInfo.isFinal > 0);
  }

  private void StartAnnounce()
  {
    this.dispDispSec = this.wmInfo.popGuardSec - 5;
    if (!this.AnnounceStart())
      return;
    this.timeLabel.text = InGameProgress.GetTimeToStringMMSS(this.wmInfo.popGuardSec);
    this.lastInteger = this.wmInfo.popGuardSec;
    this.countSec = (float) this.wmInfo.popGuardSec;
    this.state = UIWaveMatchAnnounce.eState.Announce;
  }

  private void UpdateAnnounce()
  {
    this.countSec -= Time.deltaTime;
    int time_int = Mathf.FloorToInt(this.countSec);
    if (this.lastInteger != time_int)
    {
      this.lastInteger = time_int;
      this.timeLabel.text = InGameProgress.GetTimeToStringMMSS(time_int);
    }
    if (this.lastInteger > 5)
      return;
    this.StartCountDown();
  }

  private void StartCountDown()
  {
    this.CountDownSpr.spriteName = "WaveEncount_5";
    this.CountDownObj.SetActive(true);
    this.state = UIWaveMatchAnnounce.eState.CountDown;
  }

  private void UpdateCountDown()
  {
    this.countSec -= Time.deltaTime;
    int num = Mathf.FloorToInt(this.countSec);
    if (this.lastInteger != num)
    {
      this.lastInteger = num;
      this.CountDownSpr.spriteName = "WaveEncount_" + (object) this.lastInteger;
    }
    if ((double) this.countSec > 1.0)
      return;
    this.CountDownObj.SetActive(false);
    this.StartCutIn();
  }

  private void StartCutIn()
  {
    bool isFinal = this.wmInfo.isFinal > 0;
    if (this.wmSetting == null)
      this.wmSetting = MonoBehaviourSingleton<InGameSettingsManager>.I.GetWaveMatchParam();
    SoundManager.PlayOneshotJingle(this.wmSetting.waveJingleId);
    this.panelChange.UnLock();
    ((Behaviour) this.CutInAnim[0]).enabled = true;
    this.CutInAnim[0].PlayForward();
    ((Behaviour) this.CutInAnim[1]).enabled = true;
    this.CutInAnim[1].PlayForward();
    if (!isFinal)
    {
      this.CutInNumberSpr10.spriteName = "WaveCount_" + (object) (this.wmInfo.no / 10);
      this.CutInNumberSpr01.spriteName = "WaveCount_" + (object) (this.wmInfo.no % 10);
    }
    this.CutInFinalObj.SetActive(isFinal);
    this.CutInNumberObj.SetActive(!isFinal);
    this.CutInObj.SetActive(true);
    this.countSec = 2f;
    if (MonoBehaviourSingleton<UIQuestInfoWaveMatch>.IsValid())
    {
      this.isShowWave = true;
      MonoBehaviourSingleton<UIQuestInfoWaveMatch>.I.SetWaveNow(this.wmInfo.no, this.wmInfo.finalNo, isFinal);
    }
    if (this.isEvent && MonoBehaviourSingleton<StageObjectManager>.IsValid() && Object.op_Inequality((Object) MonoBehaviourSingleton<StageObjectManager>.I.self, (Object) null))
      MonoBehaviourSingleton<StageObjectManager>.I.self.CheckWaveMatchAutoRevive();
    this.state = UIWaveMatchAnnounce.eState.CutIn;
  }

  private void UpdateCutIn()
  {
    this.countSec -= Time.deltaTime;
    if ((double) this.countSec > 0.0)
      return;
    this.CutInFinalObj.SetActive(false);
    this.CutInNumberObj.SetActive(false);
    this.CutInObj.SetActive(false);
    this.panelChange.Lock();
    this.state = UIWaveMatchAnnounce.eState.None;
  }

  private enum eState
  {
    None,
    Announce,
    CountDown,
    CutIn,
  }
}
