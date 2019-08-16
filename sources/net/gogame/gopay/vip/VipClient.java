package net.gogame.gopay.vip;

import android.content.Context;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;
import net.gogame.gopay.vip.IVipClient.Listener;
import net.gogame.gopay.vip.PurchaseEvent.VerificationStatus;
import org.json.JSONException;
import org.json.JSONObject;

public class VipClient implements IVipClient {
    public static final VipClient INSTANCE = new VipClient();

    /* renamed from: a */
    private String f1355a = null;

    /* renamed from: b */
    private String f1356b = null;

    /* renamed from: c */
    private String f1357c = null;

    /* renamed from: d */
    private String f1358d = null;

    /* renamed from: e */
    private String f1359e = null;

    /* renamed from: f */
    private VipStatus f1360f = null;

    /* renamed from: g */
    private final Set<Listener> f1361g = new HashSet();

    /* renamed from: h */
    private final Map<String, String> f1362h = new HashMap();

    /* renamed from: i */
    private final Map<String, String> f1363i = new HashMap();

    /* renamed from: j */
    private TaskQueue<BaseEvent> f1364j = null;

    /* renamed from: k */
    private final TaskQueue.Listener<BaseEvent> f1365k = new TaskQueue.Listener<BaseEvent>() {
        /* renamed from: a */
        public boolean onTask(BaseEvent baseEvent) {
            if (!(baseEvent instanceof PurchaseEvent)) {
                return true;
            }
            PurchaseEvent purchaseEvent = (PurchaseEvent) baseEvent;
            try {
                Log.v("goPay", "Tracking purchase...");
                if (VipClient.this.m970a(purchaseEvent) == null) {
                }
                return true;
            } catch (HttpException e) {
                return false;
            } catch (Exception e2) {
                return true;
            }
        }
    };

    private VipClient() {
    }

    public void init(Context context, String str, String str2) {
        this.f1355a = str;
        this.f1356b = str2;
        this.f1357c = context.getApplicationContext().getPackageName();
        this.f1358d = getAppVersion(context);
        this.f1359e = Secure.getString(context.getContentResolver(), "android_id");
        if (this.f1364j == null) {
            try {
                this.f1364j = new CustomTaskQueue(context, new File(context.getFilesDir(), "gopay-vip-client-queue.dat"), this.f1365k);
                this.f1364j.start();
            } catch (Exception e) {
                Log.e("goPay", "Exception", e);
            }
        }
    }

    public static String getAppVersion(Context context) {
        try {
            return context.getPackageManager().getPackageInfo(context.getPackageName(), 0).versionName;
        } catch (Exception e) {
            return null;
        }
    }

    public void setExtraData(String str, String str2) {
        if (str != null) {
            if (str2 != null) {
                this.f1362h.put(str, str2);
            } else {
                this.f1362h.remove(str);
            }
        }
    }

    public void setExtraHeader(String str, String str2) {
        if (str != null) {
            if (str2 != null) {
                this.f1363i.put(str, str2);
            } else {
                this.f1363i.remove(str);
            }
        }
    }

    public void addListener(Listener listener) {
        if (listener != null) {
            this.f1361g.add(listener);
        }
    }

    public void removeListener(Listener listener) {
        if (listener != null) {
            this.f1361g.remove(listener);
        }
    }

    public VipStatus getVipStatus() {
        return this.f1360f;
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public void m975a(VipStatus vipStatus) {
        this.f1360f = vipStatus;
        m978b(vipStatus);
        if (this.f1360f != null) {
            Log.v("goPay", String.format("VIP status for %s: %s / %s / %s", new Object[]{this.f1360f.getGuid(), Boolean.valueOf(this.f1360f.isVip()), Boolean.valueOf(this.f1360f.isSuspended()), this.f1360f.getSuspensionMessage()}));
            return;
        }
        Log.v("goPay", "VIP status cleared due to error");
    }

    public void checkVipStatus(final String str, boolean z) {
        if (str != null) {
            if (z || this.f1360f == null || !m976a((Object) this.f1360f.getGuid(), (Object) str)) {
                new Thread(new Runnable() {
                    public void run() {
                        try {
                            Log.v("goPay", "Checking VIP status...");
                            VipClient.this.m975a(VipClient.this.m972a(str));
                        } catch (UnauthorizedException e) {
                            Log.e("goPay", "Unauthorized: " + e.getMessage());
                            VipClient.this.m975a((VipStatus) null);
                        } catch (HttpException e2) {
                            Log.e("goPay", "HTTP response: " + e2.getMessage());
                            VipClient.this.m975a((VipStatus) null);
                        } catch (IOException e3) {
                            Log.e("goPay", "I/O error checking VIP status");
                            VipClient.this.m975a((VipStatus) null);
                        } catch (JSONException e4) {
                            Log.e("goPay", "JSON error checking VIP status");
                            VipClient.this.m975a((VipStatus) null);
                        }
                    }
                }).start();
            }
        }
    }

    public void trackPurchase(PurchaseEvent purchaseEvent) {
        if (purchaseEvent != null) {
            this.f1364j.add(purchaseEvent);
        }
    }

    /* renamed from: a */
    private static boolean m976a(Object obj, Object obj2) {
        if (obj == null && obj2 == null) {
            return true;
        }
        if (obj != null && obj2 == null) {
            return false;
        }
        if (obj != null || obj2 == null) {
            return obj.equals(obj2);
        }
        return false;
    }

    /* renamed from: b */
    private void m978b(VipStatus vipStatus) {
        for (Listener listener : this.f1361g) {
            if (listener != null) {
                try {
                    listener.onVipStatus(vipStatus);
                } catch (Exception e) {
                    Log.e("goPay", "Exception", e);
                }
            }
        }
    }

    /* renamed from: a */
    private Map<String, String> m969a() {
        LinkedHashMap linkedHashMap = new LinkedHashMap();
        if (this.f1362h != null) {
            for (Entry entry : this.f1362h.entrySet()) {
                String str = (String) entry.getKey();
                String str2 = (String) entry.getValue();
                if (!(str == null || str2 == null || linkedHashMap.containsKey(str))) {
                    linkedHashMap.put(str, str2);
                }
            }
        }
        if (this.f1355a != null) {
            linkedHashMap.put("appId", this.f1355a);
        }
        if (this.f1357c != null) {
            linkedHashMap.put("bundle_id", this.f1357c);
        }
        if (this.f1358d != null) {
            linkedHashMap.put("app_version", this.f1358d);
        }
        linkedHashMap.put("platform", "android");
        linkedHashMap.put("os_version", VERSION.RELEASE);
        linkedHashMap.put("sdk", "gopay-vip-sdk-android");
        linkedHashMap.put("sdk_version", BuildConfig.VERSION_NAME);
        if (this.f1359e != null) {
            linkedHashMap.put("device_id", this.f1359e);
        }
        return linkedHashMap;
    }

    /* renamed from: b */
    private Map<String, String> m977b() {
        Map<String, String> a = m969a();
        if (this.f1355a != null) {
            a.put("appId", this.f1355a);
        }
        return a;
    }

    /* renamed from: c */
    private Map<String, String> m979c() {
        Map<String, String> a = m969a();
        if (this.f1355a != null) {
            a.put("app_id", this.f1355a);
        }
        return a;
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public VipStatus m972a(String str) throws JSONException, UnauthorizedException, HttpException, IOException {
        Map b = m977b();
        if (str != null) {
            b.put("guid", str);
        }
        JSONObject a = C1666a.m987a("https://gp-vip.gogame.net/vip/v1/get_vip_status/", b, this.f1356b, this.f1363i);
        return new VipStatus(str, a.optBoolean("vip_status", false), a.optBoolean("suspended", false), a.optString("suspension_message", null));
    }

    /* access modifiers changed from: private */
    /* renamed from: a */
    public BaseBillingResponse m970a(PurchaseEvent purchaseEvent) throws JSONException, UnauthorizedException, HttpException, IOException {
        if (purchaseEvent == null) {
            return null;
        }
        Map c = m979c();
        if (purchaseEvent.getReferenceId() != null) {
            c.put("reference_id", purchaseEvent.getReferenceId());
        }
        if (purchaseEvent.getGuid() != null) {
            c.put("guid", purchaseEvent.getGuid());
        }
        if (purchaseEvent.getProductId() != null) {
            c.put("sku_id", purchaseEvent.getProductId());
        }
        if (purchaseEvent.getCurrencyCode() != null) {
            c.put("currency_code", purchaseEvent.getCurrencyCode());
        }
        c.put(Param.PRICE, String.valueOf(purchaseEvent.getPrice()));
        c.put("timestamp", String.valueOf(purchaseEvent.getTimestamp()));
        if (purchaseEvent.getOrderId() != null) {
            c.put("platform_order_id", purchaseEvent.getOrderId());
        }
        if (purchaseEvent.getVerificationStatus() != null) {
            c.put("verified", String.valueOf(purchaseEvent.getVerificationStatus().getValue()));
        } else {
            c.put("verified", String.valueOf(VerificationStatus.NOT_VERIFIED.getValue()));
        }
        c.put("sandbox", purchaseEvent.isSandbox() ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        JSONObject a = C1666a.m987a("https://gp-vip.gogame.net/billing/v3/log_client_transaction/", c, this.f1356b, this.f1363i);
        BaseBillingResponse baseBillingResponse = new BaseBillingResponse();
        baseBillingResponse.setStatus(a.optBoolean("status", false));
        baseBillingResponse.setStatusCode(a.optInt("statusCode", 0));
        baseBillingResponse.setStatusMessage(a.optString("statusMsg", null));
        return baseBillingResponse;
    }
}
