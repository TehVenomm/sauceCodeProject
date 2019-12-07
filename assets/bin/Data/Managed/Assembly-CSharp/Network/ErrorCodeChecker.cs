namespace Network
{
	public class ErrorCodeChecker
	{
		public static bool IsSuccess(Error errorCode)
		{
			return errorCode == Error.None;
		}

		public static bool IsWarning(Error errorCode)
		{
			if (errorCode <= Error.WRN_FRIEND_SEND_NO_SENT)
			{
				switch (errorCode)
				{
				case Error.WRN_MAINTENANCE:
				case Error.WRN_UPDATE_FORCE:
				case Error.WRN_CRYSTAL_POSSESSION_ERR:
				case Error.WRN_ASSET_UPDATE:
				case Error.WRN_SHUTOUT:
				case Error.WRN_UPDATE_VERSION:
				case Error.WRN_TABLE_UPDATE:
				case Error.WRN_BAN_USER:
				case Error.WRN_RESIGNED_USER:
				case Error.WRN_REGISTER_TOO_LONG_EMAIL:
				case Error.WRN_REGISTER_TOO_LONG_SECRET_QUESTION_ANSWER:
				case Error.WRN_REGISTER_ALREADY_REGISTERED_EMAIL:
				case Error.WRN_REGISTER_CHANGE_SECRET_QUESTION_DIFFERENT_PASSWORD:
				case Error.WRN_REGISTER_BAN_DEVICE_ID:
				case Error.WRN_REGISTER_GOOGLE_REQUESTED:
				case Error.WRN_REGISTER_GOOGLE_INVALID_PASSWORD:
				case Error.WRN_REGISTER_GOOGLE_NO_ACCOUNT:
				case Error.WRN_ROB_ACCOUNT_LOGIN_DIFFERENT_EMAIL_OR_PASSWORD:
				case Error.WRN_ROB_CHANGE_PASSWORD_TOO_SHORT:
				case Error.WRN_ROB_CHANGE_PASSWORD_DIFFERENT_NEW_PASSWORD_CONFIRM:
				case Error.WRN_ROB_CHANGE_PASSWORD_EQUAL_CURRENT_PASSWORD:
				case Error.WRN_ROB_PASSWORD_CHARACTER_TYPE:
				case Error.WRN_GOLD_SAFETY_LOCK_PASSWORD_IS_NOT_VALID:
				case Error.WRN_GOLD_CHARGE_IS_NOT_ENABLED:
				case Error.WRN_GOLD_OVER_LIMIT_OF_CHARGE:
				case Error.WRN_GOLD_OVER_LIMITTER_OVERUSE:
				case Error.WRN_SERIES_ARENA_INVALID_DATA:
				case Error.WRN_QUEST_START_NOT_FREE:
				case Error.WRN_QUEST_IS_ORDER:
				case Error.WRN_QUEST_IS_PAYING:
				case Error.WRN_SHADOW_QUEST_CANT_JOIN_COUNT:
				case Error.WRN_EXPLORE_CANT_CREATE_PARTY:
				case Error.WRN_GUILD_REQUEST_EXPIRED:
				case Error.WRN_STATUS_SKILL_ITEM_OVER:
				case Error.WRN_STATUS_EQUIP_ITEM_OVER:
				case Error.WRN_STATUS_EQUIP_SET_NAME_LENGTH:
				case Error.WRN_SMITH_OVER_EQUIP_ITEM_NUM:
				case Error.WRN_SMITH_ITEM_OUT_PERIOD:
				case Error.WRN_GACHA_LIMIT_OVER:
				case Error.WRN_GACHA_TICKET_OUT_PERIOD:
				case Error.WRN_GACHA_TICKET_NOT_ENOUGH:
				case Error.WRN_GACHA_TICKET_EXPIRED:
				case Error.WRN_GACHA_TICKET_POSSESSION_ERR:
				case Error.WRN_GACHA_NO_SET_SERIES_ID:
				case Error.WRN_GACHA_DIFF_PRODUCT_ID:
				case Error.WRN_DUPLICATE_GACHA_CHANCE_CAMPAIGN:
				case Error.WRN_FRIEND_FOLLOW_SET_COUNTFULL_FOLLOWER:
				case Error.WRN_FRIEND_NOT_FOLLOW:
				case Error.WRN_FRIEND_NOT_FOLLOWER:
				case Error.WRN_FRIEND_SEND_NO_SENT:
					break;
				default:
					goto IL_04cb;
				}
			}
			else
			{
				switch (errorCode)
				{
				case Error.WRN_FRIEND_CODE_MINE_CODE:
				case Error.WRN_FRIEND_CODE_NOT_FOUND:
				case Error.WRN_FRIEND_INVALID_MESSAGE:
				case Error.WRN_INVENTORY_OUT_PERIOD:
				case Error.WRN_PRESENT_OVER_MONEY:
				case Error.WRN_PRESENT_OVER_ITEM:
				case Error.WRN_PRESENT_OVER_EQUIP_ITEM:
				case Error.WRN_PRESENT_OVER_SKILL_ITEM:
				case Error.WRN_PRESENT_OVER_QUEST_ITEM:
				case Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL:
				case Error.WRN_PRESENT_OVER_ETC:
				case Error.WRN_PRESENT_OVER_ACCESSORY:
				case Error.WRN_SERIAL_NOT_INPUT_PERIOD:
				case Error.WRN_SERIAL_INPUT_LOCK:
				case Error.WRN_OPINION_BOX_EMPTY_MSG:
				case Error.WRN_OPTION_PARENTPASS_INVALID:
				case Error.WRN_OPTION_INVALID_BIRTHDAY:
				case Error.WRN_OPTION_FUTURE_BIRTHDAY:
				case Error.WRN_OPTION_CANT_CHANGE_NAME:
				case Error.WRN_OPTION_EMPTY_NEW_NAME:
				case Error.WRN_OPTION_INVALID_NAME:
				case Error.WRN_OPTION_INVALID_COMMENT:
				case Error.WRN_OPTION_LIMITED_COMMENT_EDIT:
				case Error.WRN_OPTION_LIMITED_NAME_EDIT:
				case Error.WRN_INVITE_CODE_NOT_FOUND:
				case Error.WRN_INVITE_CODE_MINE_CODE:
				case Error.WRN_PUSH_NOTIFICATION_DEVICE_NOT_FOUND:
				case Error.WRN_GATHER_COMPLETE_NOT_COMPLETE:
				case Error.WRN_GATHER_SHORTCUT_NOT_GATHER:
				case Error.WRN_FIELD_EVENT_OUT_PERIOD:
				case Error.WRN_FIELD_CANT_ENTER_SLOT_FULL:
				case Error.WRN_FIELD_CANT_JOIN_NOT_OPEN:
				case Error.WRN_DELIVERY_OVER:
				case Error.WRN_PARTY_TOO_MANY_PARTIES:
				case Error.WRN_PARTY_ALREADY_FINISH:
				case Error.WRN_PARTY_EXPIRED_OVER:
				case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
				case Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY:
				case Error.WRN_PARTY_OWNER_REJOIN:
				case Error.WRN_PARTY_CANT_JOIN_LEVEL:
				case Error.WRN_PARTY_CANT_JOIN_POWER:
				case Error.WRN_PARTY_CANT_JOIN_SLOT_FULL:
				case Error.WRN_PARTY_CANT_JOIN_CLIENT_VERSION:
				case Error.WRN_PARTY_CANT_JOIN_CLOSE:
				case Error.WRN_PARTY_CANT_INVITE_EMPTY_FOLLOW:
				case Error.WRN_PARTY_CANT_JOIN_ASSET_VERSION:
				case Error.WRN_PARTY_CANT_JOIN_DIFF_QUEST:
				case Error.WRN_PARTY_CANT_JOIN_LOUNGE:
				case Error.WRN_PARTY_CANT_JOIN_PORTAL_RELEASE:
				case Error.WRN_CHAT_NOT_FOUND_SERVER:
				case Error.WRN_TASK_OVER:
				case Error.WRN_LOUNGE_TOO_MANY_PARTIES:
				case Error.WRN_LOUNGE_ALREADY_FINISH:
				case Error.WRN_LOUNGE_EXPIRED_OVER:
				case Error.WRN_LOUNGE_SEARCH_NOT_FOUND_LOUNGE:
				case Error.WRN_LOUNGE_OWNER_REJOIN:
				case Error.WRN_LOUNGE_CANT_JOIN_SLOT_FULL:
				case Error.WRN_LOUNGE_CANT_INVITE_EMPTY_FOLLOW:
				case Error.WRN_LOUNGE_NOT_HAVE_STAMP:
				case Error.WRN_LOUNGE_INVALID_NAME:
				case Error.WRN_LOUNGE_CANT_INVITE_UNLOCK_USER:
				case Error.WRN_LOUNGE_CANT_JOIN_CLIENT_VERSION:
				case Error.WRN_PRESENT_OPPOSITE_HAIR_STYLE_NOT_EXISTS:
				case Error.WRN_CLAN_NO_MONEY:
				case Error.WRN_CLAN_NOT_JOINED:
				case Error.WRN_CLAN_NO_PERMISSION:
				case Error.WRN_CLAN_NOT_EXISTS_CLAN:
				case Error.WRN_CLAN_NO_VACANCY:
				case Error.WRN_CLAN_NOT_EXISTS_REQUEST:
				case Error.WRN_CLAN_USER_LV_NOT_ENOUGH:
				case Error.WRN_CLAN_INVALID_CLAN_NAME:
				case Error.WRN_CLAN_INVALID_CLAN_COMMENT:
				case Error.WRN_CLAN_REQUEST_MAX:
				case Error.WRN_CLAN_NOTICE_BOARD_INTERVAL:
				case Error.WRN_CLAN_NOTICE_BOARD_LENGTH_LIMIT_OVER:
				case Error.WRN_CLAN_NOT_EXISTS_INVITE:
				case Error.WRN_PAYMENT_GOPAY_PENDING:
				case Error.WRN_GACHA_CHANCE_CAMPAIGN_ID:
				case Error.WRN_GACHA_USER_CHANCE_DATA_UPDATE_NEED:
				case Error.WRN_GACHA_USER_CHANCE_DATA_NOT_EXIST:
				case Error.WRN_GACHA_CHANCE_CAMPAIGN_ENDED:
				case Error.WRN_GACHA_ONCE_PURCHASE_WRONG_PURCHASE:
				case Error.WRN_GACHA_ONCE_ALREADY_PURCHASED_ITEM:
				case Error.WRN_GACHA_ONCE_PURCHASE_WRONG_ITEM:
				case Error.WRN_GACHA_ONCE_PURCHASE_USE_ERROR:
				case Error.WRN_GACHA_ONCE_PURCHASE_WRONG_ITEM_ORDER:
				case Error.WRN_GACHA_STEP_UP_FREE_COUPON_USE_FAILED:
				case Error.WRN_GACHA_CHANCE_CAMPAIGN_ENDED_CANNOT_USE_FREEGACHA:
				case Error.WRN_GACHA_CHANCE_CAMPAIGN_DIFFERENT_TYPE:
				case Error.WRN_GACHA_CHANCE_CAMPAIGN_NOT_RIGHT_CAMPAIGN_TYPE_USE_FREEGACHA:
				case Error.WRN_GACHA_ONCE_PURCHASE_CAMPAIGN_NOT_OPEN_YET:
					break;
				default:
					goto IL_04cb;
				}
			}
			return true;
			IL_04cb:
			return false;
		}

		public static bool IsCriticalError(Error errorCode)
		{
			if (errorCode <= Error.CRI_OPTION_EDIT_FIGURE_DB_ERROR)
			{
				if (errorCode <= Error.CRI_SMITH_RESTORE_DB_ERROR)
				{
					if (errorCode <= Error.CRI_REGISTER_ROB_STATUS_BANNED)
					{
						if (errorCode <= Error.CRI_REGISTER_GUEST_DATA_REGIST_FAILED)
						{
							if (errorCode == Error.CRI_UNKNOWN || errorCode == Error.CRI_REGISTER_GUEST_DATA_REGIST_FAILED)
							{
								goto IL_027c;
							}
						}
						else if ((uint)(errorCode - 3025) <= 1u || errorCode == Error.CRI_REGISTER_GOOGLE_STATUS_BANNED || errorCode == Error.CRI_REGISTER_ROB_STATUS_BANNED)
						{
							goto IL_027c;
						}
					}
					else if (errorCode <= Error.CRI_GUILD_REQUEST_EXTEND_DB_ERROR)
					{
						if ((uint)(errorCode - 3901) <= 9u || (uint)(errorCode - 5901) <= 9u)
						{
							goto IL_027c;
						}
					}
					else if (errorCode == Error.CRI_STATUS_DEGREE_DB_ERROR || (uint)(errorCode - 6901) <= 9u || (uint)(errorCode - 7901) <= 6u)
					{
						goto IL_027c;
					}
				}
				else if (errorCode <= Error.CRI_INVENTORY_SELL_ABILITY_ITEM_DB_ERROR)
				{
					if (errorCode <= Error.CRI_ALCHEMY_EXCEED_DB_ERROR)
					{
						if (errorCode == Error.CRI_ALCHEMY_GROW_DB_ERROR || errorCode == Error.CRI_ALCHEMY_EXCEED_DB_ERROR)
						{
							goto IL_027c;
						}
					}
					else if (errorCode == Error.CRI_GACHA_GACHA_DB_ERROR || (uint)(errorCode - 13901) <= 5u || (uint)(errorCode - 15901) <= 11u)
					{
						goto IL_027c;
					}
				}
				else if (errorCode <= Error.CRI_SHOP_BUY_DB_ERROR)
				{
					if (errorCode == Error.CRI_PRESENT_RECEIVE_DB_ERROR || errorCode == Error.CRI_SHOP_BUY_DB_ERROR)
					{
						goto IL_027c;
					}
				}
				else if (errorCode == Error.CRI_SERIAL_INPUT_DB_ERROR || errorCode == Error.CRI_OPINION_POST_DB_ERROR || (uint)(errorCode - 23901) <= 6u)
				{
					goto IL_027c;
				}
			}
			else if (errorCode <= Error.CRI_ACHIEVEMENT_EQUIP_COLLECTION_DB_ERROR)
			{
				if (errorCode <= Error.CRI_GATHER_SHORTCUT_DB_ERROR)
				{
					if (errorCode <= Error.CRI_LOGIN_BONUS_LOGINBONUS_DB_ERROR)
					{
						if (errorCode == Error.CRI_INVITE_INPUT_DB_ERROR || errorCode == Error.CRI_LOGIN_BONUS_LOGINBONUS_DB_ERROR)
						{
							goto IL_027c;
						}
					}
					else if (errorCode == Error.CRI_PHARMACY_CREATE_DB_ERROR || (uint)(errorCode - 27901) <= 2u || (uint)(errorCode - 28901) <= 4u)
					{
						goto IL_027c;
					}
				}
				else if (errorCode <= Error.CRI_FIELD_LIMITED_DROP_DB_ERROR)
				{
					if ((uint)(errorCode - 29901) <= 1u || (uint)(errorCode - 30901) <= 10u)
					{
						goto IL_027c;
					}
				}
				else if ((uint)(errorCode - 31901) <= 3u || (uint)(errorCode - 32901) <= 8u || errorCode == Error.CRI_ACHIEVEMENT_EQUIP_COLLECTION_DB_ERROR)
				{
					goto IL_027c;
				}
			}
			else if (errorCode <= Error.CRI_REGION_OPEN_DB_ERROR)
			{
				if (errorCode <= Error.CRI_TASK_COMPLETE_DB_ERROR)
				{
					if (errorCode == Error.CRI_CHAT_CHANNEL_ENTER_DB_ERROR || errorCode == Error.CRI_TASK_COMPLETE_DB_ERROR)
					{
						goto IL_027c;
					}
				}
				else if (errorCode == Error.CRI_POINT_SHOP_BUY_DB_ERROR || (uint)(errorCode - 37901) <= 6u || errorCode == Error.CRI_REGION_OPEN_DB_ERROR)
				{
					goto IL_027c;
				}
			}
			else if (errorCode <= Error.CRI_REVIEW_INFO_DB_ERROR)
			{
				if (errorCode == Error.CRI_READ_STORY_READ_DB_ERROR || errorCode == Error.CRI_REVIEW_INFO_DB_ERROR)
				{
					goto IL_027c;
				}
			}
			else if ((uint)(errorCode - 42901) <= 11u || errorCode == Error.CRI_CLAN_MESSAGE_POST_DB_ERROR || (uint)(errorCode - 100901) <= 1u)
			{
				goto IL_027c;
			}
			return false;
			IL_027c:
			return true;
		}

		public static bool IsError(Error errorCode)
		{
			if (IsSuccess(errorCode) || IsWarning(errorCode) || IsCriticalError(errorCode))
			{
				return false;
			}
			return true;
		}
	}
}
