using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMessageTable : Singleton<TutorialMessageTable>, IDataTable
{
	public class TutorialMessageData
	{
		public class MessageData
		{
			public enum Position
			{
				UP,
				DOWN,
				CENTER
			}

			public enum CursorType
			{
				AUTO,
				MANUAL
			}

			public int positionId;

			public int npcId;

			public int faceType;

			public string message = string.Empty;

			public string imageResourceName = string.Empty;

			public int voiceId;

			public string cursorTarget = string.Empty;

			public CursorType cursorType;

			public Vector2 cursorOffset = Vector2.zero;

			public int cursorRotDeg;

			public float cursorDelay = -1f;

			public FOCUS_PATTERN focusPattern;

			public string waitEventName = string.Empty;

			public string focusFrame;

			public float wait;

			public Position position_type => (Position)positionId;

			public bool is_smile => faceType == 1;

			public bool has_voice => voiceId > 0;

			public bool has_target => !string.IsNullOrEmpty(cursorTarget);

			public bool is_wait_event => !string.IsNullOrEmpty(waitEventName);
		}

		public int tutorialId;

		public int messageId;

		public string sceneName = string.Empty;

		public string sectionName = string.Empty;

		public string triggerEventName = string.Empty;

		public int completedTutorialStep;

		public string strAppearTutorialBit = string.Empty;

		public string strFinishTutorialBit = string.Empty;

		public int appearId;

		public int appearDeliveryId;

		public bool isNewSectionOnly;

		public string strSetBit = string.Empty;

		public FORCE_RESEND_DIALOG_FLAG resendFrag;

		public string checkKeyword = string.Empty;

		public List<MessageData> messageData;

		public TUTORIAL_MENU_BIT? GetAppearTutorialBit()
		{
			if (string.IsNullOrEmpty(strAppearTutorialBit))
			{
				return null;
			}
			return (TUTORIAL_MENU_BIT)Enum.Parse(typeof(TUTORIAL_MENU_BIT), strAppearTutorialBit);
		}

		public TUTORIAL_MENU_BIT? GetFinishTutorialBit()
		{
			if (string.IsNullOrEmpty(strFinishTutorialBit))
			{
				return null;
			}
			return (TUTORIAL_MENU_BIT)Enum.Parse(typeof(TUTORIAL_MENU_BIT), strFinishTutorialBit);
		}

		public TUTORIAL_MENU_BIT? GetSetBit()
		{
			if (string.IsNullOrEmpty(strSetBit))
			{
				return null;
			}
			return (TUTORIAL_MENU_BIT)Enum.Parse(typeof(TUTORIAL_MENU_BIT), strSetBit);
		}
	}

	private TutorialReadData m_ReadData;

	private const string NT = "id,scene_name,section_name,message_id,trigger_event_name,completed_tutorial_step,appear_tutorial_bit,finish_tutorial_bit,appear,appear_delivery_id,only_new_section,set_bit,force_resend,check_keyword,position,npc_id,face_type,message,image_resource_name,voice_id,cursor_target,focus_pattern,wait_event_name,focus_frame,wait,cursor_offset_x,cursor_offset_y,cursor_angle,cursor_delay";

	private UIntKeyTable<TutorialMessageData> tutorialSectionMessages = new UIntKeyTable<TutorialMessageData>();

	public TutorialReadData ReadData
	{
		get
		{
			if (m_ReadData == null)
			{
				TutorialStep.isChangeLocalEquip = false;
				TutorialStep.isSendFirstRewardComplete = false;
				m_ReadData = TutorialReadData.CreateAndLoad();
			}
			return m_ReadData;
		}
	}

	public bool HasSection(string section_name)
	{
		bool is_find = false;
		tutorialSectionMessages.ForEach(delegate(TutorialMessageData data)
		{
			if (!is_find && data.sectionName == section_name)
			{
				is_find = true;
			}
		});
		return is_find;
	}

	public static bool HasReadTutorialEnd()
	{
		return TutorialStep.HasAllTutorialCompleted();
	}

	public static void SendTutorialBit(TUTORIAL_MENU_BIT bit, Action<bool> callback = null)
	{
		Protocol.Force(delegate
		{
			MonoBehaviourSingleton<UserInfoManager>.I.SendTutorialBit(bit, callback);
		});
	}

	public int[] GetTutorialIds()
	{
		List<int> ret = new List<int>();
		tutorialSectionMessages.ForEach(delegate(TutorialMessageData data)
		{
			ret.Add(data.tutorialId);
		});
		return ret.ToArray();
	}

	public TutorialMessageData GetTutorialTheSection(string section_name, int message_id)
	{
		TutorialMessageData ret = null;
		tutorialSectionMessages.ForEach(delegate(TutorialMessageData o)
		{
			if (ret == null && o.sectionName == section_name && o.messageId == message_id)
			{
				ret = o;
			}
		});
		return ret;
	}

	public TutorialMessageData GetEnableExecTutorial(string section_name, bool is_force, bool is_new_section, string event_name = null)
	{
		TutorialReadData save_data = Singleton<TutorialMessageTable>.I.ReadData;
		List<TutorialMessageData> list = new List<TutorialMessageData>();
		tutorialSectionMessages.ForEach(delegate(TutorialMessageData o)
		{
			if (!(o.sectionName != section_name))
			{
				if (!TutorialStep.HasAllTutorialCompleted() && o.completedTutorialStep != -1)
				{
					if (o.completedTutorialStep == 0 || (MonoBehaviourSingleton<UserInfoManager>.IsValid() && MonoBehaviourSingleton<UserInfoManager>.I.userStatus.tutorialStep >= o.completedTutorialStep) || (o.sceneName == "StatusScene" && o.sectionName == "StatusTop" && o.messageId == 1 && TutorialStep.isChangeLocalEquip))
					{
						return;
					}
				}
				else if ((o.completedTutorialStep != 0 && o.completedTutorialStep != -1) || (!is_force && !o.GetFinishTutorialBit().HasValue && o.completedTutorialStep >= 0 && save_data.HasRead(o.tutorialId)) || (o.appearId > 0 && !save_data.HasRead(o.appearId)) || (o.appearId < 0 && save_data.LastRead() != -o.appearId))
				{
					return;
				}
				if (o.GetFinishTutorialBit().HasValue)
				{
					TUTORIAL_MENU_BIT value = o.GetFinishTutorialBit().Value;
					if (MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(value))
					{
						return;
					}
				}
				if (o.GetAppearTutorialBit().HasValue)
				{
					TUTORIAL_MENU_BIT value2 = o.GetAppearTutorialBit().Value;
					if (!MonoBehaviourSingleton<UserInfoManager>.I.CheckTutorialBit(value2))
					{
						return;
					}
				}
				if ((o.appearDeliveryId == 0 || (MonoBehaviourSingleton<DeliveryManager>.IsValid() && MonoBehaviourSingleton<DeliveryManager>.I.IsClearDelivery((uint)o.appearDeliveryId))) && (!o.isNewSectionOnly || is_new_section))
				{
					if (!string.IsNullOrEmpty(event_name))
					{
						if (o.triggerEventName != event_name)
						{
							return;
						}
					}
					else if (!string.IsNullOrEmpty(o.triggerEventName))
					{
						return;
					}
					if ((!(o.sectionName == "WorldMap") && !(o.sectionName == "RegionMap")) || !string.IsNullOrEmpty(event_name) || !MonoBehaviourSingleton<WorldMapManager>.IsValid() || (!MonoBehaviourSingleton<WorldMapManager>.I.isDisplayQuestTargetMode() && !MonoBehaviourSingleton<WorldMapManager>.I.ignoreTutorial))
					{
						if (o.sectionName == "EquipSetDetailAttachSkillDialog" && !string.IsNullOrEmpty(o.checkKeyword))
						{
							uint target_id = uint.Parse(o.checkKeyword);
							bool find_non_equip_attack_skill = false;
							MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory(delegate(SkillItemInfo data)
							{
								if (!find_non_equip_attack_skill && data != null && data.tableData.type == SKILL_SLOT_TYPE.ATTACK && !data.isAttached && target_id != data.tableID)
								{
									find_non_equip_attack_skill = true;
								}
							});
							if (!find_non_equip_attack_skill)
							{
								return;
							}
							bool is_equip_first_slot = false;
							if (MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet() == null)
							{
								MonoBehaviourSingleton<StatusManager>.I.CreateLocalEquipSetData();
							}
							int eSetNo = MonoBehaviourSingleton<UserInfoManager>.I.userStatus.eSetNo;
							EquipItemInfo main_weapon = MonoBehaviourSingleton<StatusManager>.I.GetLocalEquipSet()[eSetNo].item[0];
							if (main_weapon != null)
							{
								MonoBehaviourSingleton<InventoryManager>.I.ForAllSkillItemInventory(delegate(SkillItemInfo data)
								{
									if (!is_equip_first_slot && data != null)
									{
										EquipSetSkillData equipSetSkillData = data.equipSetSkill.Find((EquipSetSkillData x) => x.equipSetNo == MonoBehaviourSingleton<StatusManager>.I.GetCurrentEquipSetNo());
										if (equipSetSkillData != null && equipSetSkillData.equipItemUniqId == main_weapon.uniqueID && equipSetSkillData.equipSlotNo == 0)
										{
											is_equip_first_slot = true;
										}
									}
								});
							}
							if (!is_equip_first_slot)
							{
								int index = o.messageData.Count - 1;
								o.messageData[index].waitEventName = o.messageData[index].waitEventName.Replace("_DETAIL", "");
								Debug.LogWarning("replace : " + o.messageData[index].waitEventName);
							}
						}
						list.Add(o);
					}
				}
			}
		});
		if (list.Count == 0)
		{
			return null;
		}
		list.Sort((TutorialMessageData l, TutorialMessageData r) => l.messageId - r.messageId);
		return list[0];
	}

	public void CreateTable(string csv_text)
	{
		CSVReader cSVReader = new CSVReader(csv_text, "id,scene_name,section_name,message_id,trigger_event_name,completed_tutorial_step,appear_tutorial_bit,finish_tutorial_bit,appear,appear_delivery_id,only_new_section,set_bit,force_resend,check_keyword,position,npc_id,face_type,message,image_resource_name,voice_id,cursor_target,focus_pattern,wait_event_name,focus_frame,wait,cursor_offset_x,cursor_offset_y,cursor_angle,cursor_delay");
		TutorialMessageData tutorialMessageData = null;
		uint num = 0u;
		while (cSVReader.NextLine())
		{
			num = 0u;
			cSVReader.Pop(ref num);
			if (num != 0)
			{
				if (tutorialMessageData != null)
				{
					tutorialSectionMessages.Add((uint)tutorialMessageData.tutorialId, tutorialMessageData);
				}
				string value = string.Empty;
				string value2 = string.Empty;
				string value3 = string.Empty;
				int value4 = 0;
				string value5 = string.Empty;
				string value6 = string.Empty;
				int value7 = 0;
				int value8 = 0;
				int value9 = 0;
				bool value10 = false;
				string value11 = string.Empty;
				FORCE_RESEND_DIALOG_FLAG value12 = FORCE_RESEND_DIALOG_FLAG.NONE;
				string value13 = string.Empty;
				cSVReader.Pop(ref value);
				cSVReader.Pop(ref value2);
				cSVReader.Pop(ref value9);
				cSVReader.Pop(ref value3);
				cSVReader.Pop(ref value4);
				cSVReader.Pop(ref value5);
				cSVReader.Pop(ref value6);
				cSVReader.Pop(ref value7);
				cSVReader.Pop(ref value8);
				cSVReader.Pop(ref value10);
				cSVReader.Pop(ref value11);
				cSVReader.Pop(ref value12);
				cSVReader.Pop(ref value13);
				if (GetTutorialTheSection(value2, value9) != null)
				{
					Log.Warning(LOG.SYSTEM, "同一セクション内で message_id が重複しています : id " + num + " : " + value2 + " - " + value9);
					continue;
				}
				tutorialMessageData = new TutorialMessageData();
				tutorialMessageData.tutorialId = (int)num;
				tutorialMessageData.sceneName = value;
				tutorialMessageData.sectionName = value2;
				tutorialMessageData.messageId = value9;
				tutorialMessageData.triggerEventName = value3;
				tutorialMessageData.completedTutorialStep = value4;
				tutorialMessageData.strAppearTutorialBit = value5;
				tutorialMessageData.strFinishTutorialBit = value6;
				tutorialMessageData.appearId = value7;
				tutorialMessageData.appearDeliveryId = value8;
				tutorialMessageData.isNewSectionOnly = value10;
				tutorialMessageData.strSetBit = value11;
				tutorialMessageData.resendFrag = value12;
				tutorialMessageData.checkKeyword = value13;
				tutorialMessageData.messageData = new List<TutorialMessageData.MessageData>();
			}
			else
			{
				string value14 = string.Empty;
				int value15 = 0;
				bool value16 = false;
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value15);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value15);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value15);
				cSVReader.Pop(ref value15);
				cSVReader.Pop(ref value16);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value14);
				cSVReader.Pop(ref value14);
			}
			if (tutorialMessageData != null && tutorialMessageData.messageData != null)
			{
				TutorialMessageData.MessageData messageData = new TutorialMessageData.MessageData();
				cSVReader.Pop(ref messageData.positionId);
				cSVReader.Pop(ref messageData.npcId);
				cSVReader.Pop(ref messageData.faceType);
				cSVReader.Pop(ref messageData.message);
				cSVReader.Pop(ref messageData.imageResourceName);
				cSVReader.Pop(ref messageData.voiceId);
				cSVReader.Pop(ref messageData.cursorTarget);
				cSVReader.Pop(ref messageData.focusPattern);
				cSVReader.Pop(ref messageData.waitEventName);
				cSVReader.Pop(ref messageData.focusFrame);
				cSVReader.Pop(ref messageData.wait);
				if (!cSVReader.IsEmpty())
				{
					messageData.cursorType = TutorialMessageData.MessageData.CursorType.MANUAL;
					cSVReader.Pop(ref messageData.cursorOffset);
				}
				else
				{
					cSVReader.NextValue();
					cSVReader.NextValue();
				}
				if (!cSVReader.IsEmpty())
				{
					messageData.cursorType = TutorialMessageData.MessageData.CursorType.MANUAL;
				}
				cSVReader.Pop(ref messageData.cursorRotDeg);
				cSVReader.Pop(ref messageData.cursorDelay);
				tutorialMessageData.messageData.Add(messageData);
			}
		}
		if (tutorialMessageData != null)
		{
			tutorialSectionMessages.Add((uint)tutorialMessageData.tutorialId, tutorialMessageData);
		}
	}
}
