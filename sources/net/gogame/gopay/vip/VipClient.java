package net.gogame.gopay.vip;

import android.content.Context;
import android.os.Build.VERSION;
import android.provider.Settings.Secure;
import android.util.Log;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.measurement.AppMeasurement;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import io.fabric.sdk.android.services.common.IdManager;
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
    private String f1289a = null;
    /* renamed from: b */
    private String f1290b = null;
    /* renamed from: c */
    private String f1291c = null;
    /* renamed from: d */
    private String f1292d = null;
    /* renamed from: e */
    private String f1293e = null;
    /* renamed from: f */
    private VipStatus f1294f = null;
    /* renamed from: g */
    private final Set<Listener> f1295g = new HashSet();
    /* renamed from: h */
    private final Map<String, String> f1296h = new HashMap();
    /* renamed from: i */
    private final Map<String, String> f1297i = new HashMap();
    /* renamed from: j */
    private TaskQueue<BaseEvent> f1298j = null;
    /* renamed from: k */
    private final TaskQueue.Listener<BaseEvent> f1299k = new C10961(this);

    /* renamed from: net.gogame.gopay.vip.VipClient$1 */
    class C10961 implements TaskQueue.Listener<BaseEvent> {
        /* renamed from: a */
        final /* synthetic */ VipClient f1286a;

        C10961(VipClient vipClient) {
            this.f1286a = vipClient;
        }

        public /* synthetic */ boolean onTask(Object obj) {
            return m955a((BaseEvent) obj);
        }

        /* renamed from: a */
        public boolean m955a(BaseEvent baseEvent) {
            if (!(baseEvent instanceof PurchaseEvent)) {
                return true;
            }
            PurchaseEvent purchaseEvent = (PurchaseEvent) baseEvent;
            try {
                Log.v("goPay", "Tracking purchase...");
                return this.f1286a.m957a(purchaseEvent) == null ? true : true;
            } catch (HttpException e) {
                return false;
            } catch (Exception e2) {
                return true;
            }
        }
    }

    private VipClient() {
    }

    public void init(Context context, String str, String str2) {
        this.f1289a = str;
        this.f1290b = str2;
        this.f1291c = context.getApplicationContext().getPackageName();
        this.f1292d = getAppVersion(context);
        this.f1293e = Secure.getString(context.getContentResolver(), "android_id");
        if (this.f1298j == null) {
            try {
                this.f1298j = new CustomTaskQueue(context, new File(context.getFilesDir(), "gopay-vip-client-queue.dat"), this.f1299k);
                this.f1298j.start();
            } catch (Throwable e) {
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
                this.f1296h.put(str, str2);
            } else {
                this.f1296h.remove(str);
            }
        }
    }

    public void setExtraHeader(String str, String str2) {
        if (str != null) {
            if (str2 != null) {
                this.f1297i.put(str, str2);
            } else {
                this.f1297i.remove(str);
            }
        }
    }

    public void addListener(Listener listener) {
        if (listener != null) {
            this.f1295g.add(listener);
        }
    }

    public void removeListener(Listener listener) {
        if (listener != null) {
            this.f1295g.remove(listener);
        }
    }

    public VipStatus getVipStatus() {
        return this.f1294f;
    }

    /* renamed from: a */
    private void m962a(VipStatus vipStatus) {
        this.f1294f = vipStatus;
        m965b(vipStatus);
        if (this.f1294f != null) {
            Log.v("goPay", String.format("VIP status for %s: %s / %s / %s", new Object[]{this.f1294f.getGuid(), Boolean.valueOf(this.f1294f.isVip()), Boolean.valueOf(this.f1294f.isSuspended()), this.f1294f.getSuspensionMessage()}));
            return;
        }
        Log.v("goPay", "VIP status cleared due to error");
    }

    public void checkVipStatus(final String str, boolean z) {
        if (str != null) {
            if (z || this.f1294f == null || !m963a(this.f1294f.getGuid(), (Object) str)) {
                new Thread(new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ VipClient f1288b;

                    public void run() {
                        try {
                            Log.v("goPay", "Checking VIP status...");
                            this.f1288b.m962a(this.f1288b.m959a(str));
                        } catch (UnauthorizedException e) {
                            Log.e("goPay", "Unauthorized: " + e.getMessage());
                            this.f1288b.m962a(null);
                        } catch (HttpException e2) {
                            Log.e("goPay", "HTTP response: " + e2.getMessage());
                            this.f1288b.m962a(null);
                        } catch (IOException e3) {
                            Log.e("goPay", "I/O error checking VIP status");
                            this.f1288b.m962a(null);
                        } catch (JSONException e4) {
                            Log.e("goPay", "JSON error checking VIP status");
                            this.f1288b.m962a(null);
                        }
                    }
                }).start();
            }
        }
    }

    public void trackPurchase(PurchaseEvent purchaseEvent) {
        if (purchaseEvent != null) {
            this.f1298j.add(purchaseEvent);
        }
    }

    /* renamed from: a */
    private static boolean m963a(Object obj, Object obj2) {
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
    private void m965b(VipStatus vipStatus) {
        for (Listener listener : this.f1295g) {
            if (listener != null) {
                try {
                    listener.onVipStatus(vipStatus);
                } catch (Throwable e) {
                    Log.e("goPay", "Exception", e);
                }
            }
        }
    }

    /* renamed from: a */
    private Map<String, String> m956a() {
        Map<String, String> linkedHashMap = new LinkedHashMap();
        if (this.f1296h != null) {
            for (Entry entry : this.f1296h.entrySet()) {
                String str = (String) entry.getKey();
                String str2 = (String) entry.getValue();
                if (!(str == null || str2 == null || linkedHashMap.containsKey(str))) {
                    linkedHashMap.put(str, str2);
                }
            }
        }
        if (this.f1289a != null) {
            linkedHashMap.put("appId", this.f1289a);
        }
        if (this.f1291c != null) {
            linkedHashMap.put("bundle_id", this.f1291c);
        }
        if (this.f1292d != null) {
            linkedHashMap.put("app_version", this.f1292d);
        }
        linkedHashMap.put("platform", AbstractSpiCall.ANDROID_CLIENT_TYPE);
        linkedHashMap.put(IdManager.OS_VERSION_FIELD, VERSION.RELEASE);
        linkedHashMap.put("sdk", "gopay-vip-sdk-android");
        linkedHashMap.put("sdk_version", BuildConfig.VERSION_NAME);
        if (this.f1293e != null) {
            linkedHashMap.put("device_id", this.f1293e);
        }
        return linkedHashMap;
    }

    /* renamed from: b */
    private Map<String, String> m964b() {
        Map<String, String> a = m956a();
        if (this.f1289a != null) {
            a.put("appId", this.f1289a);
        }
        return a;
    }

    /* renamed from: c */
    private Map<String, String> m966c() {
        Map<String, String> a = m956a();
        if (this.f1289a != null) {
            a.put("app_id", this.f1289a);
        }
        return a;
    }

    /* renamed from: a */
    private VipStatus m959a(String str) throws JSONException, UnauthorizedException, HttpException, IOException {
        Map b = m964b();
        if (str != null) {
            b.put("guid", str);
        }
        JSONObject a = C1098a.m973a("https://gp-vip.gogame.net/vip/v1/get_vip_status/", b, this.f1290b, this.f1297i);
        return new VipStatus(str, a.optBoolean("vip_status", false), a.optBoolean("suspended", false), a.optString("suspension_message", null));
    }

    /* renamed from: a */
    private BaseBillingResponse m957a(PurchaseEvent purchaseEvent) throws JSONException, UnauthorizedException, HttpException, IOException {
        if (purchaseEvent == null) {
            return null;
        }
        Map c = m966c();
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
        c.put(AppMeasurement.Param.TIMESTAMP, String.valueOf(purchaseEvent.getTimestamp()));
        if (purchaseEvent.getOrderId() != null) {
            c.put("platform_order_id", purchaseEvent.getOrderId());
        }
        if (purchaseEvent.getVerificationStatus() != null) {
            c.put("verified", String.valueOf(purchaseEvent.getVerificationStatus().getValue()));
        } else {
            c.put("verified", String.valueOf(VerificationStatus.NOT_VERIFIED.getValue()));
        }
        c.put("sandbox", purchaseEvent.isSandbox() ? AppEventsConstants.EVENT_PARAM_VALUE_YES : AppEventsConstants.EVENT_PARAM_VALUE_NO);
        JSONObject a = C1098a.m973a("https://gp-vip.gogame.net/billing/v3/log_client_transaction/", c, this.f1290b, this.f1297i);
        BaseBillingResponse baseBillingResponse = new BaseBillingResponse();
        baseBillingResponse.setStatus(a.optBoolean("status", false));
        baseBillingResponse.setStatusCode(a.optInt("statusCode", 0));
        baseBillingResponse.setStatusMessage(a.optString("statusMsg", null));
        return baseBillingResponse;
    }
}
