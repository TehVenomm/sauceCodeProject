package com.crashlytics.android.answers;

import android.os.Bundle;
import com.facebook.internal.ServerProtocol;
import com.google.android.gms.actions.SearchIntents;
import com.google.firebase.analytics.FirebaseAnalytics.Event;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.math.BigDecimal;
import java.util.Arrays;
import java.util.HashSet;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import p017io.fabric.sdk.android.Fabric;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public class FirebaseAnalyticsEventMapper {
    private static final Set<String> EVENT_NAMES = new HashSet(Arrays.asList(new String[]{"app_clear_data", "app_exception", "app_remove", "app_upgrade", "app_install", "app_update", "firebase_campaign", "error", "first_open", "first_visit", "in_app_purchase", "notification_dismiss", "notification_foreground", "notification_open", "notification_receive", "os_update", "session_start", "user_engagement", "ad_exposure", "adunit_exposure", "ad_query", "ad_activeview", "ad_impression", "ad_click", "screen_view", "firebase_extra_parameter"}));
    private static final String FIREBASE_LEVEL_NAME = "level_name";
    private static final String FIREBASE_METHOD = "method";
    private static final String FIREBASE_RATING = "rating";
    private static final String FIREBASE_SUCCESS = "success";

    private String mapAttribute(String str) {
        if (str == null || str.length() == 0) {
            return "fabric_unnamed_parameter";
        }
        String replaceAll = str.replaceAll("[^\\p{Alnum}_]+", EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
        if (replaceAll.startsWith("ga_") || replaceAll.startsWith("google_") || replaceAll.startsWith("firebase_") || !Character.isLetter(replaceAll.charAt(0))) {
            replaceAll = "fabric_" + replaceAll;
        }
        return replaceAll.length() > 40 ? replaceAll.substring(0, 40) : replaceAll;
    }

    private Integer mapBooleanValue(String str) {
        if (str == null) {
            return null;
        }
        return Integer.valueOf(str.equals(ServerProtocol.DIALOG_RETURN_SCOPES_TRUE) ? 1 : 0);
    }

    private void mapCustomEventAttributes(Bundle bundle, Map<String, Object> map) {
        for (Entry entry : map.entrySet()) {
            Object value = entry.getValue();
            String mapAttribute = mapAttribute((String) entry.getKey());
            if (value instanceof String) {
                bundle.putString(mapAttribute, entry.getValue().toString());
            } else if (value instanceof Double) {
                bundle.putDouble(mapAttribute, ((Double) entry.getValue()).doubleValue());
            } else if (value instanceof Long) {
                bundle.putLong(mapAttribute, ((Long) entry.getValue()).longValue());
            } else if (value instanceof Integer) {
                bundle.putInt(mapAttribute, ((Integer) entry.getValue()).intValue());
            }
        }
    }

    private String mapCustomEventName(String str) {
        if (str == null || str.length() == 0) {
            return "fabric_unnamed_event";
        }
        if (EVENT_NAMES.contains(str)) {
            return "fabric_" + str;
        }
        String replaceAll = str.replaceAll("[^\\p{Alnum}_]+", EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
        if (replaceAll.startsWith("ga_") || replaceAll.startsWith("google_") || replaceAll.startsWith("firebase_") || !Character.isLetter(replaceAll.charAt(0))) {
            replaceAll = "fabric_" + replaceAll;
        }
        return replaceAll.length() > 40 ? replaceAll.substring(0, 40) : replaceAll;
    }

    private Double mapDouble(Object obj) {
        String valueOf = String.valueOf(obj);
        if (valueOf == null) {
            return null;
        }
        return Double.valueOf(valueOf);
    }

    private Bundle mapPredefinedEvent(SessionEvent sessionEvent) {
        Bundle bundle = new Bundle();
        if (AbstractIntegrationSupport.DEFAULT_PURCHASE_CATEGORY.equals(sessionEvent.predefinedType)) {
            putString(bundle, Param.ITEM_ID, (String) sessionEvent.predefinedAttributes.get("itemId"));
            putString(bundle, Param.ITEM_NAME, (String) sessionEvent.predefinedAttributes.get("itemName"));
            putString(bundle, Param.ITEM_CATEGORY, (String) sessionEvent.predefinedAttributes.get(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
            putDouble(bundle, "value", mapPriceValue(sessionEvent.predefinedAttributes.get("itemPrice")));
            putString(bundle, Param.CURRENCY, (String) sessionEvent.predefinedAttributes.get(Param.CURRENCY));
        } else if ("addToCart".equals(sessionEvent.predefinedType)) {
            putString(bundle, Param.ITEM_ID, (String) sessionEvent.predefinedAttributes.get("itemId"));
            putString(bundle, Param.ITEM_NAME, (String) sessionEvent.predefinedAttributes.get("itemName"));
            putString(bundle, Param.ITEM_CATEGORY, (String) sessionEvent.predefinedAttributes.get(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE));
            putDouble(bundle, Param.PRICE, mapPriceValue(sessionEvent.predefinedAttributes.get("itemPrice")));
            putDouble(bundle, "value", mapPriceValue(sessionEvent.predefinedAttributes.get("itemPrice")));
            putString(bundle, Param.CURRENCY, (String) sessionEvent.predefinedAttributes.get(Param.CURRENCY));
            bundle.putLong(Param.QUANTITY, 1);
        } else if ("startCheckout".equals(sessionEvent.predefinedType)) {
            putLong(bundle, Param.QUANTITY, Long.valueOf((long) ((Integer) sessionEvent.predefinedAttributes.get("itemCount")).intValue()));
            putDouble(bundle, "value", mapPriceValue(sessionEvent.predefinedAttributes.get("totalPrice")));
            putString(bundle, Param.CURRENCY, (String) sessionEvent.predefinedAttributes.get(Param.CURRENCY));
        } else if ("contentView".equals(sessionEvent.predefinedType)) {
            putString(bundle, Param.CONTENT_TYPE, (String) sessionEvent.predefinedAttributes.get("contentType"));
            putString(bundle, Param.ITEM_ID, (String) sessionEvent.predefinedAttributes.get("contentId"));
            putString(bundle, Param.ITEM_NAME, (String) sessionEvent.predefinedAttributes.get("contentName"));
        } else if (Event.SEARCH.equals(sessionEvent.predefinedType)) {
            putString(bundle, Param.SEARCH_TERM, (String) sessionEvent.predefinedAttributes.get(SearchIntents.EXTRA_QUERY));
        } else if ("share".equals(sessionEvent.predefinedType)) {
            putString(bundle, "method", (String) sessionEvent.predefinedAttributes.get("method"));
            putString(bundle, Param.CONTENT_TYPE, (String) sessionEvent.predefinedAttributes.get("contentType"));
            putString(bundle, Param.ITEM_ID, (String) sessionEvent.predefinedAttributes.get("contentId"));
            putString(bundle, Param.ITEM_NAME, (String) sessionEvent.predefinedAttributes.get("contentName"));
        } else if (FIREBASE_RATING.equals(sessionEvent.predefinedType)) {
            putString(bundle, FIREBASE_RATING, String.valueOf(sessionEvent.predefinedAttributes.get(FIREBASE_RATING)));
            putString(bundle, Param.CONTENT_TYPE, (String) sessionEvent.predefinedAttributes.get("contentType"));
            putString(bundle, Param.ITEM_ID, (String) sessionEvent.predefinedAttributes.get("contentId"));
            putString(bundle, Param.ITEM_NAME, (String) sessionEvent.predefinedAttributes.get("contentName"));
        } else if ("signUp".equals(sessionEvent.predefinedType)) {
            putString(bundle, "method", (String) sessionEvent.predefinedAttributes.get("method"));
        } else if (Event.LOGIN.equals(sessionEvent.predefinedType)) {
            putString(bundle, "method", (String) sessionEvent.predefinedAttributes.get("method"));
        } else if ("invite".equals(sessionEvent.predefinedType)) {
            putString(bundle, "method", (String) sessionEvent.predefinedAttributes.get("method"));
        } else if ("levelStart".equals(sessionEvent.predefinedType)) {
            putString(bundle, "level_name", (String) sessionEvent.predefinedAttributes.get("levelName"));
        } else if ("levelEnd".equals(sessionEvent.predefinedType)) {
            putDouble(bundle, Param.SCORE, mapDouble(sessionEvent.predefinedAttributes.get(Param.SCORE)));
            putString(bundle, "level_name", (String) sessionEvent.predefinedAttributes.get("levelName"));
            putInt(bundle, "success", mapBooleanValue((String) sessionEvent.predefinedAttributes.get("success")));
        }
        mapCustomEventAttributes(bundle, sessionEvent.customAttributes);
        return bundle;
    }

    /* Code decompiled incorrectly, please refer to instructions dump. */
    private java.lang.String mapPredefinedEventName(java.lang.String r6, boolean r7) {
        /*
            r5 = this;
            r3 = 2
            r2 = 1
            r1 = 0
            r0 = -1
            if (r7 == 0) goto L_0x0011
            int r4 = r6.hashCode()
            switch(r4) {
                case -902468296: goto L_0x002a;
                case 103149417: goto L_0x0034;
                case 1743324417: goto L_0x0020;
                default: goto L_0x000d;
            }
        L_0x000d:
            r4 = r0
        L_0x000e:
            switch(r4) {
                case 0: goto L_0x003e;
                case 1: goto L_0x0041;
                case 2: goto L_0x0044;
                default: goto L_0x0011;
            }
        L_0x0011:
            int r4 = r6.hashCode()
            switch(r4) {
                case -2131650889: goto L_0x00bb;
                case -1183699191: goto L_0x00a3;
                case -938102371: goto L_0x0083;
                case -906336856: goto L_0x006f;
                case -902468296: goto L_0x008d;
                case -389087554: goto L_0x0065;
                case 23457852: goto L_0x0051;
                case 103149417: goto L_0x0097;
                case 109400031: goto L_0x0079;
                case 196004670: goto L_0x00af;
                case 1664021448: goto L_0x005b;
                case 1743324417: goto L_0x0047;
                default: goto L_0x0018;
            }
        L_0x0018:
            switch(r0) {
                case 0: goto L_0x00c7;
                case 1: goto L_0x00cb;
                case 2: goto L_0x00cf;
                case 3: goto L_0x00d3;
                case 4: goto L_0x00d7;
                case 5: goto L_0x00db;
                case 6: goto L_0x00df;
                case 7: goto L_0x00e3;
                case 8: goto L_0x00e7;
                case 9: goto L_0x00eb;
                case 10: goto L_0x00ef;
                case 11: goto L_0x00f3;
                default: goto L_0x001b;
            }
        L_0x001b:
            java.lang.String r0 = r5.mapCustomEventName(r6)
        L_0x001f:
            return r0
        L_0x0020:
            java.lang.String r4 = "purchase"
            boolean r4 = r6.equals(r4)
            if (r4 == 0) goto L_0x000d
            r4 = r1
            goto L_0x000e
        L_0x002a:
            java.lang.String r4 = "signUp"
            boolean r4 = r6.equals(r4)
            if (r4 == 0) goto L_0x000d
            r4 = r2
            goto L_0x000e
        L_0x0034:
            java.lang.String r4 = "login"
            boolean r4 = r6.equals(r4)
            if (r4 == 0) goto L_0x000d
            r4 = r3
            goto L_0x000e
        L_0x003e:
            java.lang.String r0 = "failed_ecommerce_purchase"
            goto L_0x001f
        L_0x0041:
            java.lang.String r0 = "failed_sign_up"
            goto L_0x001f
        L_0x0044:
            java.lang.String r0 = "failed_login"
            goto L_0x001f
        L_0x0047:
            java.lang.String r2 = "purchase"
            boolean r2 = r6.equals(r2)
            if (r2 == 0) goto L_0x0018
            r0 = r1
            goto L_0x0018
        L_0x0051:
            java.lang.String r1 = "addToCart"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = r2
            goto L_0x0018
        L_0x005b:
            java.lang.String r1 = "startCheckout"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = r3
            goto L_0x0018
        L_0x0065:
            java.lang.String r1 = "contentView"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 3
            goto L_0x0018
        L_0x006f:
            java.lang.String r1 = "search"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 4
            goto L_0x0018
        L_0x0079:
            java.lang.String r1 = "share"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 5
            goto L_0x0018
        L_0x0083:
            java.lang.String r1 = "rating"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 6
            goto L_0x0018
        L_0x008d:
            java.lang.String r1 = "signUp"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 7
            goto L_0x0018
        L_0x0097:
            java.lang.String r1 = "login"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 8
            goto L_0x0018
        L_0x00a3:
            java.lang.String r1 = "invite"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 9
            goto L_0x0018
        L_0x00af:
            java.lang.String r1 = "levelStart"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 10
            goto L_0x0018
        L_0x00bb:
            java.lang.String r1 = "levelEnd"
            boolean r1 = r6.equals(r1)
            if (r1 == 0) goto L_0x0018
            r0 = 11
            goto L_0x0018
        L_0x00c7:
            java.lang.String r0 = "ecommerce_purchase"
            goto L_0x001f
        L_0x00cb:
            java.lang.String r0 = "add_to_cart"
            goto L_0x001f
        L_0x00cf:
            java.lang.String r0 = "begin_checkout"
            goto L_0x001f
        L_0x00d3:
            java.lang.String r0 = "select_content"
            goto L_0x001f
        L_0x00d7:
            java.lang.String r0 = "search"
            goto L_0x001f
        L_0x00db:
            java.lang.String r0 = "share"
            goto L_0x001f
        L_0x00df:
            java.lang.String r0 = "rate_content"
            goto L_0x001f
        L_0x00e3:
            java.lang.String r0 = "sign_up"
            goto L_0x001f
        L_0x00e7:
            java.lang.String r0 = "login"
            goto L_0x001f
        L_0x00eb:
            java.lang.String r0 = "invite"
            goto L_0x001f
        L_0x00ef:
            java.lang.String r0 = "level_start"
            goto L_0x001f
        L_0x00f3:
            java.lang.String r0 = "level_end"
            goto L_0x001f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.crashlytics.android.answers.FirebaseAnalyticsEventMapper.mapPredefinedEventName(java.lang.String, boolean):java.lang.String");
    }

    private Double mapPriceValue(Object obj) {
        if (((Long) obj) == null) {
            return null;
        }
        return Double.valueOf(new BigDecimal(((Long) obj).longValue()).divide(AddToCartEvent.MICRO_CONSTANT).doubleValue());
    }

    private void putDouble(Bundle bundle, String str, Double d) {
        Double mapDouble = mapDouble(d);
        if (mapDouble != null) {
            bundle.putDouble(str, mapDouble.doubleValue());
        }
    }

    private void putInt(Bundle bundle, String str, Integer num) {
        if (num != null) {
            bundle.putInt(str, num.intValue());
        }
    }

    private void putLong(Bundle bundle, String str, Long l) {
        if (l != null) {
            bundle.putLong(str, l.longValue());
        }
    }

    private void putString(Bundle bundle, String str, String str2) {
        if (str2 != null) {
            bundle.putString(str, str2);
        }
    }

    public FirebaseAnalyticsEvent mapEvent(SessionEvent sessionEvent) {
        Bundle bundle;
        String mapCustomEventName;
        boolean z = true;
        boolean z2 = Type.CUSTOM.equals(sessionEvent.type) && sessionEvent.customType != null;
        boolean z3 = Type.PREDEFINED.equals(sessionEvent.type) && sessionEvent.predefinedType != null;
        if (!z2 && !z3) {
            return null;
        }
        if (z3) {
            bundle = mapPredefinedEvent(sessionEvent);
        } else {
            Bundle bundle2 = new Bundle();
            if (sessionEvent.customAttributes != null) {
                mapCustomEventAttributes(bundle2, sessionEvent.customAttributes);
                bundle = bundle2;
            } else {
                bundle = bundle2;
            }
        }
        if (z3) {
            String str = (String) sessionEvent.predefinedAttributes.get("success");
            if (str == null || Boolean.parseBoolean(str)) {
                z = false;
            }
            mapCustomEventName = mapPredefinedEventName(sessionEvent.predefinedType, z);
        } else {
            mapCustomEventName = mapCustomEventName(sessionEvent.customType);
        }
        Fabric.getLogger().mo20969d(Answers.TAG, "Logging event into firebase...");
        return new FirebaseAnalyticsEvent(mapCustomEventName, bundle);
    }
}
