package com.facebook.appevents.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.os.Bundle;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.util.Log;
import com.facebook.FacebookSdk;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Map;
import java.util.Map.Entry;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;
import org.json.JSONException;
import org.json.JSONObject;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;

class InAppPurchaseEventManager {
    private static final String AS_INTERFACE = "asInterface";
    private static final int CACHE_CLEAR_TIME_LIMIT_SEC = 604800;
    private static final String DETAILS_LIST = "DETAILS_LIST";
    private static final String GET_PURCHASES = "getPurchases";
    private static final String GET_PURCHASE_HISTORY = "getPurchaseHistory";
    private static final String GET_SKU_DETAILS = "getSkuDetails";
    private static final String INAPP = "inapp";
    private static final String INAPP_CONTINUATION_TOKEN = "INAPP_CONTINUATION_TOKEN";
    private static final String INAPP_PURCHASE_DATA_LIST = "INAPP_PURCHASE_DATA_LIST";
    private static final String IN_APP_BILLING_SERVICE = "com.android.vending.billing.IInAppBillingService";
    private static final String IN_APP_BILLING_SERVICE_STUB = "com.android.vending.billing.IInAppBillingService$Stub";
    private static final String IS_BILLING_SUPPORTED = "isBillingSupported";
    private static final String ITEM_ID_LIST = "ITEM_ID_LIST";
    private static final String LAST_CLEARED_TIME = "LAST_CLEARED_TIME";
    private static final String LAST_LOGGED_TIME_SEC = "LAST_LOGGED_TIME_SEC";
    private static final int MAX_QUERY_PURCHASE_NUM = 30;
    private static final String PACKAGE_NAME = FacebookSdk.getApplicationContext().getPackageName();
    private static final int PURCHASE_EXPIRE_TIME_SEC = 43200;
    private static final String PURCHASE_INAPP_STORE = "com.facebook.internal.PURCHASE";
    private static final int PURCHASE_STOP_QUERY_TIME_SEC = 1200;
    private static final String PURCHASE_SUBS_STORE = "com.facebook.internal.PURCHASE_SUBS";
    private static final String RESPONSE_CODE = "RESPONSE_CODE";
    private static final String SKU_DETAILS_STORE = "com.facebook.internal.SKU_DETAILS";
    private static final int SKU_DETAIL_EXPIRE_TIME_SEC = 43200;
    private static final String SUBSCRIPTION = "subs";
    private static final long SUBSCRIPTION_HARTBEAT_INTERVAL = 86400;
    private static final String TAG = InAppPurchaseEventManager.class.getCanonicalName();
    private static final HashMap<String, Class<?>> classMap = new HashMap<>();
    private static final HashMap<String, Method> methodMap = new HashMap<>();
    private static final SharedPreferences purchaseInappSharedPrefs = FacebookSdk.getApplicationContext().getSharedPreferences(PURCHASE_INAPP_STORE, 0);
    private static final SharedPreferences purchaseSubsSharedPrefs = FacebookSdk.getApplicationContext().getSharedPreferences(PURCHASE_SUBS_STORE, 0);
    private static final SharedPreferences skuDetailSharedPrefs = FacebookSdk.getApplicationContext().getSharedPreferences(SKU_DETAILS_STORE, 0);

    InAppPurchaseEventManager() {
    }

    @Nullable
    public static Object asInterface(Context context, IBinder iBinder) {
        return invokeMethod(context, IN_APP_BILLING_SERVICE_STUB, AS_INTERFACE, null, new Object[]{iBinder});
    }

    public static void clearSkuDetailsCache() {
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        long j = skuDetailSharedPrefs.getLong(LAST_CLEARED_TIME, 0);
        if (j == 0) {
            skuDetailSharedPrefs.edit().putLong(LAST_CLEARED_TIME, currentTimeMillis).apply();
        } else if (currentTimeMillis - j > 604800) {
            skuDetailSharedPrefs.edit().clear().putLong(LAST_CLEARED_TIME, currentTimeMillis).apply();
        }
    }

    private static ArrayList<String> filterPurchasesInapp(ArrayList<String> arrayList) {
        ArrayList<String> arrayList2 = new ArrayList<>();
        Editor edit = purchaseInappSharedPrefs.edit();
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            try {
                JSONObject jSONObject = new JSONObject(str);
                String string = jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
                long j = jSONObject.getLong("purchaseTime");
                String string2 = jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN);
                if (currentTimeMillis - (j / 1000) <= 43200 && !purchaseInappSharedPrefs.getString(string, "").equals(string2)) {
                    edit.putString(string, string2);
                    arrayList2.add(str);
                }
            } catch (JSONException e) {
                Log.e(TAG, "parsing purchase failure: ", e);
            }
        }
        edit.apply();
        return arrayList2;
    }

    @Nullable
    private static Class<?> getClass(Context context, String str) {
        Class<?> cls = (Class) classMap.get(str);
        if (cls != null) {
            return cls;
        }
        try {
            cls = context.getClassLoader().loadClass(str);
            classMap.put(str, cls);
            return cls;
        } catch (ClassNotFoundException e) {
            Log.e(TAG, str + " is not available, please add " + str + " to the project.", e);
            return cls;
        }
    }

    @android.support.annotation.Nullable
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.lang.reflect.Method getMethod(java.lang.Class<?> r8, java.lang.String r9) {
        /*
            r7 = 4
            r6 = 3
            r5 = 2
            r4 = 1
            r3 = 0
            java.util.HashMap<java.lang.String, java.lang.reflect.Method> r0 = methodMap
            java.lang.Object r0 = r0.get(r9)
            java.lang.reflect.Method r0 = (java.lang.reflect.Method) r0
            if (r0 == 0) goto L_0x0010
        L_0x000f:
            return r0
        L_0x0010:
            r1 = 0
            int r2 = r9.hashCode()     // Catch:{ NoSuchMethodException -> 0x0026 }
            switch(r2) {
                case -1801122596: goto L_0x006c;
                case -1450694211: goto L_0x0062;
                case -1123215065: goto L_0x004e;
                case -594356707: goto L_0x0076;
                case -573310373: goto L_0x0058;
                default: goto L_0x0018;
            }     // Catch:{ NoSuchMethodException -> 0x0026 }
        L_0x0018:
            r2 = -1
        L_0x0019:
            switch(r2) {
                case 0: goto L_0x0080;
                case 1: goto L_0x0089;
                case 2: goto L_0x00a2;
                case 3: goto L_0x00b6;
                case 4: goto L_0x00cf;
                default: goto L_0x001c;
            }     // Catch:{ NoSuchMethodException -> 0x0026 }
        L_0x001c:
            java.lang.reflect.Method r0 = r8.getDeclaredMethod(r9, r1)     // Catch:{ NoSuchMethodException -> 0x0026 }
            java.util.HashMap<java.lang.String, java.lang.reflect.Method> r1 = methodMap     // Catch:{ NoSuchMethodException -> 0x0026 }
            r1.put(r9, r0)     // Catch:{ NoSuchMethodException -> 0x0026 }
            goto L_0x000f
        L_0x0026:
            r1 = move-exception
            java.lang.String r2 = TAG
            java.lang.StringBuilder r3 = new java.lang.StringBuilder
            r3.<init>()
            java.lang.String r4 = r8.getName()
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.String r4 = "."
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.StringBuilder r3 = r3.append(r9)
            java.lang.String r4 = " method not found"
            java.lang.StringBuilder r3 = r3.append(r4)
            java.lang.String r3 = r3.toString()
            android.util.Log.e(r2, r3, r1)
            goto L_0x000f
        L_0x004e:
            java.lang.String r2 = "asInterface"
            boolean r2 = r9.equals(r2)     // Catch:{ NoSuchMethodException -> 0x0026 }
            if (r2 == 0) goto L_0x0018
            r2 = r3
            goto L_0x0019
        L_0x0058:
            java.lang.String r2 = "getSkuDetails"
            boolean r2 = r9.equals(r2)     // Catch:{ NoSuchMethodException -> 0x0026 }
            if (r2 == 0) goto L_0x0018
            r2 = r4
            goto L_0x0019
        L_0x0062:
            java.lang.String r2 = "isBillingSupported"
            boolean r2 = r9.equals(r2)     // Catch:{ NoSuchMethodException -> 0x0026 }
            if (r2 == 0) goto L_0x0018
            r2 = r5
            goto L_0x0019
        L_0x006c:
            java.lang.String r2 = "getPurchases"
            boolean r2 = r9.equals(r2)     // Catch:{ NoSuchMethodException -> 0x0026 }
            if (r2 == 0) goto L_0x0018
            r2 = r6
            goto L_0x0019
        L_0x0076:
            java.lang.String r2 = "getPurchaseHistory"
            boolean r2 = r9.equals(r2)     // Catch:{ NoSuchMethodException -> 0x0026 }
            if (r2 == 0) goto L_0x0018
            r2 = r7
            goto L_0x0019
        L_0x0080:
            r1 = 1
            java.lang.Class[] r1 = new java.lang.Class[r1]     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 0
            java.lang.Class<android.os.IBinder> r3 = android.os.IBinder.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            goto L_0x001c
        L_0x0089:
            r1 = 4
            java.lang.Class[] r1 = new java.lang.Class[r1]     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 0
            java.lang.Class r3 = java.lang.Integer.TYPE     // Catch:{ NoSuchMethodException -> 0x0026 }
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 1
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 2
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 3
            java.lang.Class<android.os.Bundle> r3 = android.os.Bundle.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            goto L_0x001c
        L_0x00a2:
            r1 = 3
            java.lang.Class[] r1 = new java.lang.Class[r1]     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 0
            java.lang.Class r3 = java.lang.Integer.TYPE     // Catch:{ NoSuchMethodException -> 0x0026 }
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 1
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 2
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            goto L_0x001c
        L_0x00b6:
            r1 = 4
            java.lang.Class[] r1 = new java.lang.Class[r1]     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 0
            java.lang.Class r3 = java.lang.Integer.TYPE     // Catch:{ NoSuchMethodException -> 0x0026 }
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 1
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 2
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            r2 = 3
            java.lang.Class<java.lang.String> r3 = java.lang.String.class
            r1[r2] = r3     // Catch:{ NoSuchMethodException -> 0x0026 }
            goto L_0x001c
        L_0x00cf:
            java.lang.Class r2 = java.lang.Integer.TYPE     // Catch:{ NoSuchMethodException -> 0x0026 }
            r1 = 5
            java.lang.Class[] r1 = new java.lang.Class[r1]
            r1[r3] = r2
            java.lang.Class<java.lang.String> r2 = java.lang.String.class
            r1[r4] = r2
            java.lang.Class<java.lang.String> r2 = java.lang.String.class
            r1[r5] = r2
            java.lang.Class<java.lang.String> r2 = java.lang.String.class
            r1[r6] = r2
            java.lang.Class<android.os.Bundle> r2 = android.os.Bundle.class
            r1[r7] = r2
            goto L_0x001c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.appevents.internal.InAppPurchaseEventManager.getMethod(java.lang.Class, java.lang.String):java.lang.reflect.Method");
    }

    private static ArrayList<String> getPurchaseHistory(Context context, Object obj, String str) {
        int i;
        Boolean bool;
        ArrayList<String> arrayList = new ArrayList<>();
        if (isBillingSupported(context, obj, str).booleanValue()) {
            Object obj2 = null;
            int i2 = 0;
            Boolean valueOf = Boolean.valueOf(false);
            while (true) {
                String str2 = PACKAGE_NAME;
                Bundle bundle = new Bundle();
                Object invokeMethod = invokeMethod(context, IN_APP_BILLING_SERVICE, GET_PURCHASE_HISTORY, obj, new Object[]{Integer.valueOf(6), str2, str, obj2, bundle});
                if (invokeMethod != null) {
                    long currentTimeMillis = System.currentTimeMillis() / 1000;
                    Bundle bundle2 = (Bundle) invokeMethod;
                    if (bundle2.getInt("RESPONSE_CODE") == 0) {
                        Iterator it = bundle2.getStringArrayList("INAPP_PURCHASE_DATA_LIST").iterator();
                        i = i2;
                        while (true) {
                            if (!it.hasNext()) {
                                bool = valueOf;
                                break;
                            }
                            String str3 = (String) it.next();
                            try {
                                if (currentTimeMillis - (new JSONObject(str3).getLong("purchaseTime") / 1000) > 1200) {
                                    bool = Boolean.valueOf(true);
                                    break;
                                }
                                arrayList.add(str3);
                                i++;
                            } catch (JSONException e) {
                                Log.e(TAG, "parsing purchase failure: ", e);
                            }
                        }
                        obj2 = bundle2.getString("INAPP_CONTINUATION_TOKEN");
                        valueOf = bool;
                        if (i >= 30 || obj2 == null || valueOf.booleanValue()) {
                            break;
                        }
                        i2 = i;
                    }
                }
                obj2 = null;
                i = i2;
                i2 = i;
            }
        }
        return arrayList;
    }

    public static ArrayList<String> getPurchaseHistoryInapp(Context context, Object obj) {
        ArrayList<String> arrayList = new ArrayList<>();
        if (obj == null) {
            return arrayList;
        }
        Class cls = getClass(context, IN_APP_BILLING_SERVICE);
        return (cls == null || getMethod(cls, GET_PURCHASE_HISTORY) == null) ? arrayList : filterPurchasesInapp(getPurchaseHistory(context, obj, "inapp"));
    }

    /* JADX WARNING: Removed duplicated region for block: B:18:0x000a A[EDGE_INSN: B:18:0x000a->B:2:0x000a ?: BREAK  , SYNTHETIC] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static java.util.ArrayList<java.lang.String> getPurchases(android.content.Context r11, java.lang.Object r12, java.lang.String r13) {
        /*
            r1 = 0
            r10 = 3
            r3 = 0
            java.util.ArrayList r4 = new java.util.ArrayList
            r4.<init>()
            if (r12 != 0) goto L_0x000b
        L_0x000a:
            return r4
        L_0x000b:
            java.lang.Boolean r0 = isBillingSupported(r11, r12, r13)
            boolean r0 = r0.booleanValue()
            if (r0 == 0) goto L_0x000a
            r0 = r1
            r2 = r3
        L_0x0017:
            java.lang.String r5 = "com.android.vending.billing.IInAppBillingService"
            java.lang.String r6 = "getPurchases"
            r7 = 4
            java.lang.Object[] r7 = new java.lang.Object[r7]
            java.lang.Integer r8 = java.lang.Integer.valueOf(r10)
            r7[r3] = r8
            r8 = 1
            java.lang.String r9 = PACKAGE_NAME
            r7[r8] = r9
            r8 = 2
            r7[r8] = r13
            r7[r10] = r0
            java.lang.Object r0 = invokeMethod(r11, r5, r6, r12, r7)
            if (r0 == 0) goto L_0x005b
            android.os.Bundle r0 = (android.os.Bundle) r0
            java.lang.String r5 = "RESPONSE_CODE"
            int r5 = r0.getInt(r5)
            if (r5 != 0) goto L_0x005b
            java.lang.String r5 = "INAPP_PURCHASE_DATA_LIST"
            java.util.ArrayList r5 = r0.getStringArrayList(r5)
            if (r5 == 0) goto L_0x000a
            int r6 = r5.size()
            int r2 = r2 + r6
            r4.addAll(r5)
            java.lang.String r5 = "INAPP_CONTINUATION_TOKEN"
            java.lang.String r0 = r0.getString(r5)
        L_0x0054:
            r5 = 30
            if (r2 >= r5) goto L_0x000a
            if (r0 != 0) goto L_0x0017
            goto L_0x000a
        L_0x005b:
            r0 = r1
            goto L_0x0054
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.appevents.internal.InAppPurchaseEventManager.getPurchases(android.content.Context, java.lang.Object, java.lang.String):java.util.ArrayList");
    }

    public static ArrayList<String> getPurchasesInapp(Context context, Object obj) {
        return filterPurchasesInapp(getPurchases(context, obj, "inapp"));
    }

    public static Map<String, SubscriptionType> getPurchasesSubs(Context context, Object obj) {
        HashMap hashMap = new HashMap();
        Iterator it = getPurchases(context, obj, "subs").iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            SubscriptionType subsType = getSubsType(str);
            if (!(subsType == SubscriptionType.DUPLICATED || subsType == SubscriptionType.UNKNOWN)) {
                hashMap.put(str, subsType);
            }
        }
        return hashMap;
    }

    public static ArrayList<String> getPurchasesSubsExpire(Context context, Object obj) {
        ArrayList<String> arrayList = new ArrayList<>();
        Map all = purchaseSubsSharedPrefs.getAll();
        if (!all.isEmpty()) {
            ArrayList purchases = getPurchases(context, obj, "subs");
            HashSet hashSet = new HashSet();
            Iterator it = purchases.iterator();
            while (it.hasNext()) {
                try {
                    hashSet.add(new JSONObject((String) it.next()).getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID));
                } catch (JSONException e) {
                    Log.e(TAG, "Error parsing purchase json", e);
                }
            }
            HashSet<String> hashSet2 = new HashSet<>();
            for (Entry key : all.entrySet()) {
                String str = (String) key.getKey();
                if (!hashSet.contains(str)) {
                    hashSet2.add(str);
                }
            }
            Editor edit = purchaseSubsSharedPrefs.edit();
            for (String str2 : hashSet2) {
                String string = purchaseSubsSharedPrefs.getString(str2, "");
                edit.remove(str2);
                if (!string.isEmpty()) {
                    arrayList.add(purchaseSubsSharedPrefs.getString(str2, ""));
                }
            }
            edit.apply();
        }
        return arrayList;
    }

    public static Map<String, String> getSkuDetails(Context context, ArrayList<String> arrayList, Object obj, boolean z) {
        Map<String, String> readSkuDetailsFromCache = readSkuDetailsFromCache(arrayList);
        ArrayList arrayList2 = new ArrayList();
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            if (!readSkuDetailsFromCache.containsKey(str)) {
                arrayList2.add(str);
            }
        }
        readSkuDetailsFromCache.putAll(getSkuDetailsFromGoogle(context, arrayList2, obj, z));
        return readSkuDetailsFromCache;
    }

    private static Map<String, String> getSkuDetailsFromGoogle(Context context, ArrayList<String> arrayList, Object obj, boolean z) {
        HashMap hashMap = new HashMap();
        if (obj != null && !arrayList.isEmpty()) {
            Bundle bundle = new Bundle();
            bundle.putStringArrayList("ITEM_ID_LIST", arrayList);
            Object invokeMethod = invokeMethod(context, IN_APP_BILLING_SERVICE, GET_SKU_DETAILS, obj, new Object[]{Integer.valueOf(3), PACKAGE_NAME, z ? "subs" : "inapp", bundle});
            if (invokeMethod != null) {
                Bundle bundle2 = (Bundle) invokeMethod;
                if (bundle2.getInt("RESPONSE_CODE") == 0) {
                    ArrayList stringArrayList = bundle2.getStringArrayList("DETAILS_LIST");
                    if (stringArrayList != null && arrayList.size() == stringArrayList.size()) {
                        for (int i = 0; i < arrayList.size(); i++) {
                            hashMap.put(arrayList.get(i), stringArrayList.get(i));
                        }
                    }
                    writeSkuDetailsToCache(hashMap);
                }
            }
        }
        return hashMap;
    }

    private static SubscriptionType getSubsType(String str) {
        SubscriptionType subscriptionType = null;
        try {
            long currentTimeMillis = System.currentTimeMillis() / 1000;
            JSONObject jSONObject = new JSONObject(str);
            String string = jSONObject.getString(AmazonAppstoreBillingService.JSON_KEY_PRODUCT_ID);
            String string2 = purchaseSubsSharedPrefs.getString(string, "");
            JSONObject jSONObject2 = string2.isEmpty() ? new JSONObject() : new JSONObject(string2);
            if (!jSONObject2.optString(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN).equals(jSONObject.get(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_PURCHASE_TOKEN))) {
                subscriptionType = currentTimeMillis - (jSONObject.getLong("purchaseTime") / 1000) < 43200 ? SubscriptionType.NEW : SubscriptionType.HEARTBEAT;
            }
            if (subscriptionType == null && !string2.isEmpty()) {
                boolean z = jSONObject2.getBoolean("autoRenewing");
                boolean z2 = jSONObject.getBoolean("autoRenewing");
                if (!z2 && z) {
                    subscriptionType = SubscriptionType.CANCEL;
                } else if (!z && z2) {
                    subscriptionType = SubscriptionType.RESTORE;
                }
            }
            SubscriptionType subscriptionType2 = (subscriptionType != null || string2.isEmpty()) ? subscriptionType : currentTimeMillis - jSONObject2.getLong(LAST_LOGGED_TIME_SEC) > SUBSCRIPTION_HARTBEAT_INTERVAL ? SubscriptionType.HEARTBEAT : SubscriptionType.DUPLICATED;
            if (subscriptionType2 == SubscriptionType.DUPLICATED) {
                return subscriptionType2;
            }
            jSONObject.put(LAST_LOGGED_TIME_SEC, currentTimeMillis);
            purchaseSubsSharedPrefs.edit().putString(string, jSONObject.toString()).apply();
            return subscriptionType2;
        } catch (JSONException e) {
            Log.e(TAG, "parsing purchase failure: ", e);
            return SubscriptionType.UNKNOWN;
        }
    }

    @Nullable
    private static Object invokeMethod(Context context, String str, String str2, Object obj, Object[] objArr) {
        Object obj2 = null;
        Class cls = getClass(context, str);
        if (cls == null) {
            return obj2;
        }
        Method method = getMethod(cls, str2);
        if (method == null) {
            return obj2;
        }
        if (obj != null) {
            obj = cls.cast(obj);
        }
        try {
            return method.invoke(obj, objArr);
        } catch (IllegalAccessException e) {
            Log.e(TAG, "Illegal access to method " + cls.getName() + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + method.getName(), e);
            return obj2;
        } catch (InvocationTargetException e2) {
            Log.e(TAG, "Invocation target exception in " + cls.getName() + AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER + method.getName(), e2);
            return obj2;
        }
    }

    private static Boolean isBillingSupported(Context context, Object obj, String str) {
        if (obj == null) {
            return Boolean.valueOf(false);
        }
        Object invokeMethod = invokeMethod(context, IN_APP_BILLING_SERVICE, IS_BILLING_SUPPORTED, obj, new Object[]{Integer.valueOf(3), PACKAGE_NAME, str});
        return Boolean.valueOf(invokeMethod != null && ((Integer) invokeMethod).intValue() == 0);
    }

    private static Map<String, String> readSkuDetailsFromCache(ArrayList<String> arrayList) {
        HashMap hashMap = new HashMap();
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        Iterator it = arrayList.iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            String string = skuDetailSharedPrefs.getString(str, null);
            if (string != null) {
                String[] split = string.split(";", 2);
                if (currentTimeMillis - Long.parseLong(split[0]) < 43200) {
                    hashMap.put(str, split[1]);
                }
            }
        }
        return hashMap;
    }

    private static void writeSkuDetailsToCache(Map<String, String> map) {
        long currentTimeMillis = System.currentTimeMillis() / 1000;
        Editor edit = skuDetailSharedPrefs.edit();
        for (Entry entry : map.entrySet()) {
            edit.putString((String) entry.getKey(), currentTimeMillis + ";" + ((String) entry.getValue()));
        }
        edit.apply();
    }
}
