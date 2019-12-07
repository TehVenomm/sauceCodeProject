using System;

namespace Network
{
	[Serializable]
	public class UserStatus
	{
		public int sex;

		public int faceId;

		public int hairId;

		public int hairColorId;

		public int skinId;

		public int voiceId;

		public XorInt level = 0;

		public XorInt exp = 0;

		public XorInt expPrev = 0;

		public XorInt expNext = 0;

		public XorInt hp = 0;

		public XorInt atk = 0;

		public XorInt def = 0;

		public int money;

		public int crystal;

		public int eSetNo;

		public int ueSetNo;

		public int titleId;

		public int maxFollow;

		public int maxEquipItem;

		public int maxSkillItem;

		public int maxAbilityItem;

		public int tutorialStep;

		public string tutorialBit;

		public int tutorialQuestId;

		public int researchLv;

		public int questGrade;

		public int fieldGrade;

		public int present;

		public int clanId;

		public string armorUniqId;

		public string armUniqId;

		public string legUniqId;

		public string helmUniqId;

		public int showHelm;

		public DateTime nextDonationTime;

		public int fairyNum;

		public int maxEquipItemTargetNum;

		public int Money
		{
			get
			{
				return money;
			}
			set
			{
				money = value;
			}
		}

		public int Crystal
		{
			get
			{
				return crystal;
			}
			set
			{
				crystal = value;
			}
		}

		public int Exp
		{
			get
			{
				return exp;
			}
			set
			{
				exp = value;
			}
		}

		public int ExpPrev
		{
			get
			{
				return expPrev;
			}
			set
			{
				expPrev = value;
			}
		}

		public int ExpNext
		{
			get
			{
				return expNext;
			}
			set
			{
				expNext = value;
			}
		}

		public int RelativeExp => Exp - ExpPrev;

		public int RelativeExpNext => ExpNext - ExpPrev;

		public float ExpProgress01
		{
			get
			{
				if (RelativeExpNext > 0)
				{
					return (float)RelativeExp / (float)RelativeExpNext;
				}
				return 1f;
			}
		}

		public long TutorialBit => long.Parse(tutorialBit);

		public bool IsTutorialBitReady => tutorialBit != null;
	}
}
