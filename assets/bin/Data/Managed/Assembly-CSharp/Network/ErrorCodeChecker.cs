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
			switch (errorCode)
			{
			default:
				switch (errorCode)
				{
				default:
					switch (errorCode)
					{
					default:
						switch (errorCode)
						{
						default:
							switch (errorCode)
							{
							default:
								switch (errorCode)
								{
								default:
									switch (errorCode)
									{
									default:
										switch (errorCode)
										{
										default:
											switch (errorCode)
											{
											default:
												switch (errorCode)
												{
												default:
													switch (errorCode)
													{
													default:
														switch (errorCode)
														{
														default:
															switch (errorCode)
															{
															default:
																switch (errorCode)
																{
																case Error.WRN_MAINTENANCE:
																case Error.WRN_UPDATE_FORCE:
																case Error.WRN_CRYSTAL_POSSESSION_ERR:
																case Error.WRN_BAN_USER:
																case Error.WRN_RESIGNED_USER:
																case Error.WRN_REGISTER_TOO_LONG_EMAIL:
																case Error.WRN_REGISTER_TOO_LONG_SECRET_QUESTION_ANSWER:
																case Error.WRN_REGISTER_ALREADY_REGISTERED_EMAIL:
																case Error.WRN_ROB_ACCOUNT_LOGIN_DIFFERENT_EMAIL_OR_PASSWORD:
																case Error.WRN_GOLD_SAFETY_LOCK_PASSWORD_IS_NOT_VALID:
																case Error.WRN_GOLD_CHARGE_IS_NOT_ENABLED:
																case Error.WRN_GOLD_OVER_LIMIT_OF_CHARGE:
																case Error.WRN_GOLD_OVER_LIMITTER_OVERUSE:
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
																case Error.WRN_FRIEND_SEND_NO_SENT:
																case Error.WRN_FRIEND_CODE_MINE_CODE:
																case Error.WRN_FRIEND_CODE_NOT_FOUND:
																case Error.WRN_FRIEND_INVALID_MESSAGE:
																case Error.WRN_INVENTORY_OUT_PERIOD:
																case Error.WRN_SERIAL_NOT_INPUT_PERIOD:
																case Error.WRN_SERIAL_INPUT_LOCK:
																case Error.WRN_OPINION_BOX_EMPTY_MSG:
																case Error.WRN_OPTION_PARENTPASS_INVALID:
																case Error.WRN_INVITE_CODE_NOT_FOUND:
																case Error.WRN_INVITE_CODE_MINE_CODE:
																case Error.WRN_PUSH_NOTIFICATION_DEVICE_NOT_FOUND:
																case Error.WRN_GATHER_COMPLETE_NOT_COMPLETE:
																case Error.WRN_GATHER_SHORTCUT_NOT_GATHER:
																case Error.WRN_FIELD_EVENT_OUT_PERIOD:
																case Error.WRN_FIELD_CANT_ENTER_SLOT_FULL:
																case Error.WRN_FIELD_CANT_JOIN_NOT_OPEN:
																case Error.WRN_DELIVERY_OVER:
																case Error.WRN_CHAT_NOT_FOUND_SERVER:
																case Error.WRN_TASK_OVER:
																case Error.WRN_PRESENT_OPPOSITE_HAIR_STYLE_NOT_EXISTS:
																case Error.WRN_PAYMENT_GOPAY_PENDING:
																case Error.WRN_GACHA_CHANCE_CAMPAIGN_ID:
																case Error.WRN_GACHA_USER_CHANCE_DATA_UPDATE_NEED:
																case Error.WRN_GACHA_USER_CHANCE_DATA_NOT_EXIST:
																case Error.WRN_GACHA_CHANCE_CAMPAIGN_ENDED:
																	break;
																default:
																	return false;
																}
																break;
															case Error.WRN_REGISTER_CHANGE_SECRET_QUESTION_DIFFERENT_PASSWORD:
															case Error.WRN_REGISTER_BAN_DEVICE_ID:
																break;
															}
															break;
														case Error.WRN_ASSET_UPDATE:
														case Error.WRN_SHUTOUT:
														case Error.WRN_UPDATE_VERSION:
														case Error.WRN_TABLE_UPDATE:
															break;
														}
														break;
													case Error.WRN_LOUNGE_TOO_MANY_PARTIES:
													case Error.WRN_LOUNGE_ALREADY_FINISH:
													case Error.WRN_LOUNGE_EXPIRED_OVER:
													case Error.WRN_LOUNGE_SEARCH_NOT_FOUND_LOUNGE:
													case Error.WRN_LOUNGE_OWNER_REJOIN:
														break;
													}
													break;
												case Error.WRN_FRIEND_FOLLOW_SET_COUNTFULL_FOLLOWER:
												case Error.WRN_FRIEND_NOT_FOLLOW:
												case Error.WRN_FRIEND_NOT_FOLLOWER:
													break;
												}
												break;
											case Error.WRN_REGISTER_GOOGLE_REQUESTED:
											case Error.WRN_REGISTER_GOOGLE_INVALID_PASSWORD:
											case Error.WRN_REGISTER_GOOGLE_NO_ACCOUNT:
												break;
											}
											break;
										case Error.WRN_PRESENT_OVER_MONEY:
										case Error.WRN_PRESENT_OVER_ITEM:
										case Error.WRN_PRESENT_OVER_EQUIP_ITEM:
										case Error.WRN_PRESENT_OVER_SKILL_ITEM:
										case Error.WRN_PRESENT_OVER_QUEST_ITEM:
										case Error.WRN_PRESENT_OVER_EQUIP_AND_SKILL:
										case Error.WRN_PRESENT_OVER_ETC:
											break;
										}
										break;
									case Error.WRN_PARTY_TOO_MANY_PARTIES:
									case Error.WRN_PARTY_ALREADY_FINISH:
									case Error.WRN_PARTY_EXPIRED_OVER:
									case Error.WRN_PARTY_SEARCH_NOT_FOUND_QUEST:
									case Error.WRN_PARTY_SEARCH_NOT_FOUND_PARTY:
									case Error.WRN_PARTY_OWNER_REJOIN:
										break;
									}
									break;
								case Error.WRN_ROB_CHANGE_PASSWORD_TOO_SHORT:
								case Error.WRN_ROB_CHANGE_PASSWORD_DIFFERENT_NEW_PASSWORD_CONFIRM:
								case Error.WRN_ROB_CHANGE_PASSWORD_EQUAL_CURRENT_PASSWORD:
								case Error.WRN_ROB_PASSWORD_CHARACTER_TYPE:
									break;
								}
								break;
							case Error.WRN_LOUNGE_CANT_JOIN_SLOT_FULL:
							case Error.WRN_LOUNGE_CANT_INVITE_EMPTY_FOLLOW:
							case Error.WRN_LOUNGE_NOT_HAVE_STAMP:
							case Error.WRN_LOUNGE_INVALID_NAME:
							case Error.WRN_LOUNGE_CANT_INVITE_UNLOCK_USER:
							case Error.WRN_LOUNGE_CANT_JOIN_CLIENT_VERSION:
								break;
							}
							break;
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
						}
						break;
					case Error.WRN_GACHA_LIMIT_OVER:
					case Error.WRN_GACHA_TICKET_OUT_PERIOD:
					case Error.WRN_GACHA_TICKET_NOT_ENOUGH:
					case Error.WRN_GACHA_TICKET_EXPIRED:
					case Error.WRN_GACHA_TICKET_POSSESSION_ERR:
					case Error.WRN_GACHA_NO_SET_SERIES_ID:
					case Error.WRN_GACHA_DIFF_PRODUCT_ID:
					case Error.WRN_DUPLICATE_GACHA_CHANCE_CAMPAIGN:
						break;
					}
					break;
				case Error.WRN_OPTION_INVALID_BIRTHDAY:
				case Error.WRN_OPTION_FUTURE_BIRTHDAY:
				case Error.WRN_OPTION_CANT_CHANGE_NAME:
				case Error.WRN_OPTION_EMPTY_NEW_NAME:
				case Error.WRN_OPTION_INVALID_NAME:
				case Error.WRN_OPTION_INVALID_COMMENT:
				case Error.WRN_OPTION_LIMITED_COMMENT_EDIT:
				case Error.WRN_OPTION_LIMITED_NAME_EDIT:
					break;
				}
				break;
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
				break;
			}
			return true;
		}

		public static bool IsCriticalError(Error errorCode)
		{
			switch (errorCode)
			{
			default:
				switch (errorCode)
				{
				default:
					switch (errorCode)
					{
					default:
						switch (errorCode)
						{
						default:
							switch (errorCode)
							{
							default:
								switch (errorCode)
								{
								default:
									switch (errorCode)
									{
									default:
										switch (errorCode)
										{
										default:
											switch (errorCode)
											{
											default:
												switch (errorCode)
												{
												case Error.CRI_UNKNOWN:
												case Error.CRI_REGISTER_GUEST_DATA_REGIST_FAILED:
												case Error.CRI_REGISTER_DEFAULT_WEAPON_NOT_FOUND:
												case Error.CRI_REGISTER_DEFAULT_ARMOR_NOT_FOUND:
												case Error.CRI_REGISTER_GOOGLE_STATUS_BANNED:
												case Error.CRI_REGISTER_ROB_STATUS_BANNED:
												case Error.CRI_STATUS_DEGREE_DB_ERROR:
												case Error.CRI_ALCHEMY_GROW_DB_ERROR:
												case Error.CRI_ALCHEMY_EXCEED_DB_ERROR:
												case Error.CRI_GACHA_GACHA_DB_ERROR:
												case Error.CRI_FRIEND_FOLLOW_DB_ERROR:
												case Error.CRI_FRIEND_UNFOLLOW_DB_ERROR:
												case Error.CRI_FRIEND_DELETEFOLLOWER_DB_ERROR:
												case Error.CRI_FRIEND_SEND_MESSAGE_DB_ERROR:
												case Error.CRI_FRIEND_MESSAGE_DETAIL_LIST_DB_ERROR:
												case Error.CRI_FRIEND_GET_NO_READ_MESSAGE_DB_ERROR:
												case Error.CRI_PRESENT_RECEIVE_DB_ERROR:
												case Error.CRI_SHOP_BUY_DB_ERROR:
												case Error.CRI_SERIAL_INPUT_DB_ERROR:
												case Error.CRI_OPINION_POST_DB_ERROR:
												case Error.CRI_INVITE_INPUT_DB_ERROR:
												case Error.CRI_LOGIN_BONUS_LOGINBONUS_DB_ERROR:
												case Error.CRI_PHARMACY_CREATE_DB_ERROR:
												case Error.CRI_PUSH_NOTIFICATION_DEVICE_POST_DB_ERROR:
												case Error.CRI_PUSH_NOTIFICATION_DEVICE_DELETE_DB_ERROR:
												case Error.CRI_PUSH_NOTIFICATION_DEVICE_ENABLE_DB_ERROR:
												case Error.CRI_GATHER_ENTER_DB_ERROR:
												case Error.CRI_GATHER_UPDATE_DB_ERROR:
												case Error.CRI_GATHER_START_DB_ERROR:
												case Error.CRI_GATHER_COMPLETE_DB_ERROR:
												case Error.CRI_GATHER_SHORTCUT_DB_ERROR:
												case Error.CRI_BLACKLIST_ADD_DB_ERROR:
												case Error.CRI_BLACKLIST_DELETE_DB_ERROR:
												case Error.CRI_DELIVERY_COMPLETE_DB_ERROR:
												case Error.CRI_DELIVERY_HOME_DB_ERROR:
												case Error.CRI_DELIVERY_UPDATE_DB_ERROR:
												case Error.CRI_DELIVERY_QUEST_LIST_DB_ERROR:
												case Error.CRI_ACHIEVEMENT_EQUIP_COLLECTION_DB_ERROR:
												case Error.CRI_CHAT_CHANNEL_ENTER_DB_ERROR:
												case Error.CRI_TASK_COMPLETE_DB_ERROR:
												case Error.CRI_POINT_SHOP_BUY_DB_ERROR:
												case Error.CRI_REGION_OPEN_DB_ERROR:
												case Error.CRI_READ_STORY_READ_DB_ERROR:
												case Error.CRI_REVIEW_INFO_DB_ERROR:
												case Error.CRI_DEBUG_DB_ERROR:
												case Error.CRI_DEBUG_ONCE_PURCHASE_CAMPAIGN_EMPTY_ERROR:
													break;
												default:
													return false;
												}
												break;
											case Error.CRI_LOUNGE_CREATE_DB_ERROR:
											case Error.CRI_LOUNGE_APPLY_DB_ERROR:
											case Error.CRI_LOUNGE_ENTRY_DB_ERROR:
											case Error.CRI_LOUNGE_LEAVE_DB_ERROR:
											case Error.CRI_LOUNGE_EDIT_DB_ERROR:
											case Error.CRI_LOUNGE_UPDATE_EXPIRED_DB_ERROR:
											case Error.CRI_LOUNGE_JOIN_DB_ERROR:
												break;
											}
											break;
										case Error.CRI_OPTION_CHANGE_NAME_DB_ERROR:
										case Error.CRI_OPTION_STOPPER_DB_ERROR:
										case Error.CRI_OPTION_SET_PARENTPASS_DB_ERROR:
										case Error.CRI_OPTION_RESET_PARENTPASS_DB_ERROR:
										case Error.CRI_OPTION_EDIT_COMMENT_DB_ERROR:
										case Error.CRI_OPTION_BIRTHDAY_DB_ERROR:
										case Error.CRI_OPTION_EDIT_FIGURE_DB_ERROR:
											break;
										}
										break;
									case Error.CRI_SMITH_CREATE_DB_ERROR:
									case Error.CRI_SMITH_GROW_DB_ERROR:
									case Error.CRI_SMITH_EVOLVE_DB_ERROR:
									case Error.CRI_SMITH_EXCEED_DB_ERROR:
									case Error.CRI_SMITH_CHANGE_ABILITY_DB_ERROR:
									case Error.CRI_SMITH_SHADOW_EVOLVE_DB_ERROR:
									case Error.CRI_SMITH_RESTORE_DB_ERROR:
										break;
									}
									break;
								case Error.CRI_PARTY_MATCHING_DB_ERROR:
								case Error.CRI_PARTY_CREATE_DB_ERROR:
								case Error.CRI_PARTY_APPLY_DB_ERROR:
								case Error.CRI_PARTY_ENTRY_DB_ERROR:
								case Error.CRI_PARTY_LEAVE_DB_ERROR:
								case Error.CRI_PARTY_EDIT_DB_ERROR:
								case Error.CRI_PARTY_READY_DB_ERROR:
								case Error.CRI_PARTY_UPDATE_EXPIRED_DB_ERROR:
								case Error.CRI_PARTY_RANDOM_MATCHING_DB_ERROR:
									break;
								}
								break;
							case Error.CRI_STATUS_EQUIP_DB_ERROR:
							case Error.CRI_STATUS_EQUIP_SKILL_DB_ERROR:
							case Error.CRI_STATUS_EQUIP_SET_DB_ERROR:
							case Error.CRI_STATUS_DETACH_SKILL_DB_ERROR:
							case Error.CRI_STATUS_VISUAL_EQUIP_DB_ERROR:
							case Error.CRI_STATUS_TUTORIAL_DB_ERROR:
							case Error.CRI_STATUS_EQUIP_SET_NAME_DB_ERROR:
							case Error.CRI_STATUS_EQUIP_SKILL2_DB_ERROR:
							case Error.CRI_STATUS_DETACH_SKILL2_DB_ERROR:
							case Error.CRI_STATUS_EQUIP_SET_COPY_DB_ERROR:
								break;
							}
							break;
						case Error.CRI_QUEST_START_DB_ERROR:
						case Error.CRI_QUEST_RETIRE_DB_ERROR:
						case Error.CRI_QUEST_COMPLETE_DB_ERROR:
						case Error.CRI_QUEST_STORY_COMPLETE_DB_ERROR:
						case Error.CRI_QUEST_CONTINUE_DB_ERROR:
						case Error.CRI_QUEST_STORY_READ_DB_ERROR:
						case Error.CRI_PROLOGUE_STORY_READ_DB_ERROR:
						case Error.CRI_QUEST_RUSH_PROGRESS_DB_ERROR:
						case Error.CRI_QUEST_SHADOW_CHALLENGE_RESET_DB_ERROR:
						case Error.CRI_GUILD_REQUEST_EXTEND_DB_ERROR:
							break;
						}
						break;
					case Error.CRI_REGISTER_CREATE_DB_ERROR:
					case Error.CRI_REGISTER_GET_PRESENT_DB_ERROR:
					case Error.CRI_REGISTER_UPDATE_VERSION_DB_ERROR:
					case Error.CRI_REGISTER_CREATE_ROB_ACCOUNT_DB_ERROR:
					case Error.CRI_REGISTER_AUTH_ROB_DB_ERROR:
					case Error.CRI_REGISTER_CHANGE_PASSWORD_ROB_DB_ERROR:
					case Error.CRI_REGISTER_CHANGE_SECRET_QUESTION_DB_ERROR:
					case Error.CRI_REGISTER_GOOGLE_DB_ERROR:
					case Error.CRI_REGISTER_GOOGLE_AUTH_DB_ERROR:
					case Error.CRI_REGISTER_GOOGLE_CHANGE_PASSWORD_DB_ERROR:
						break;
					}
					break;
				case Error.CRI_FIELD_MATCHING_DB_ERROR:
				case Error.CRI_FIELD_LEAVE_DB_ERROR:
				case Error.CRI_FIELD_DROP_DB_ERROR:
				case Error.CRI_FIELD_QUEST_DB_ERROR:
				case Error.CRI_FIELD_CREATE_DB_ERROR:
				case Error.CRI_FIELD_ENTER_DB_ERROR:
				case Error.CRI_FIELD_CONTINUE_DB_ERROR:
				case Error.CRI_FIELD_GATHER_DB_ERROR:
				case Error.CRI_FIELD_QUEST_OPEN_PORTAL_DB_ERROR:
				case Error.CRI_FIELD_QUEST_MAP_CHANGE_DB_ERROR:
				case Error.CRI_FIELD_LIMITED_DROP_DB_ERROR:
					break;
				}
				break;
			case Error.CRI_INVENTORY_EQUIP_LOCK_DB_ERROR:
			case Error.CRI_INVENTORY_SELL_ITEM_DB_ERROR:
			case Error.CRI_INVENTORY_SELL_EQUIP_DB_ERROR:
			case Error.CRI_INVENTORY_SELL_SKILL_DB_ERROR:
			case Error.CRI_INVENTORY_EXTEND_DB_ERROR:
			case Error.CRI_INVENTORY_SELL_QUEST_ITEM_DB_ERROR:
			case Error.CRI_INVENTORY_SKILL_LOCK_DB_ERROR:
			case Error.CRI_INVENTORY_USE_ITEM_DB_ERROR:
			case Error.CRI_INVENTORY_EXCHANGE_EQUIP_DB_ERROR:
			case Error.CRI_INVENTORY_EXCHANGE_QUEST_ITEM_DB_ERROR:
			case Error.CRI_INVENTORY_EXCHANGE_ITEM_DB_ERROR:
			case Error.CRI_INVENTORY_SELL_ABILITY_ITEM_DB_ERROR:
				break;
			}
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
