namespace Network
{
	public class EventListData : EventData
	{
		public int leftBadge;

		public int rightBadge;

		public string rightValue;

		public int place;

		public BADGE_1_CATEGORY leftBadgeEnum
		{
			get;
			protected set;
		}

		public BADGE_2_CATEGORY rightBadgeEnum
		{
			get;
			protected set;
		}

		public EVENT_DISPLAY_PLACE placeEnum
		{
			get;
			protected set;
		}

		public override void SetupEnum()
		{
			base.SetupEnum();
			leftBadgeEnum = (BADGE_1_CATEGORY)leftBadge;
			rightBadgeEnum = (BADGE_2_CATEGORY)rightBadge;
			placeEnum = (EVENT_DISPLAY_PLACE)place;
		}
	}
}
