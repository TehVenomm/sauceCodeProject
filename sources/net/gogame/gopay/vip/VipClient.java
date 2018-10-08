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
    private String f3677a = null;
    /* renamed from: b */
    private String f3678b = null;
    /* renamed from: c */
    private String f3679c = null;
    /* renamed from: d */
    private String f3680d = null;
    /* renamed from: e */
    private String f3681e = null;
    /* renamed from: f */
    private VipStatus f3682f = null;
    /* renamed from: g */
    private final Set<Listener> f3683g = new HashSet();
    /* renamed from: h */
    private final Map<String, String> f3684h = new HashMap();
    /* renamed from: i */
    private final Map<String, String> f3685i = new HashMap();
    /* renamed from: j */
    private TaskQueue<BaseEvent> f3686j = null;
    /* renamed from: k */
    private final TaskQueue.Listener<BaseEvent> f3687k = new C14121(this);

    /* renamed from: net.gogame.gopay.vip.VipClient$1 */
    class C14121 implements TaskQueue.Listener<BaseEvent> {
        /* renamed from: a */
        final /* synthetic */ VipClient f3674a;

        C14121(VipClient vipClient) {
            this.f3674a = vipClient;
        }

        public /* synthetic */ boolean onTask(Object obj) {
            return m3980a((BaseEvent) obj);
        }

        /* renamed from: a */
        public boolean m3980a(BaseEvent baseEvent) {
            if (!(baseEvent instanceof PurchaseEvent)) {
                return true;
            }
            PurchaseEvent purchaseEvent = (PurchaseEvent) baseEvent;
            try {
                Log.v("goPay", "Tracking purchase...");
                return this.f3674a.m3982a(purchaseEvent) == null ? true : true;
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
        this.f3677a = str;
        this.f3678b = str2;
        this.f3679c = context.getApplicationContext().getPackageName();
        this.f3680d = getAppVersion(context);
        this.f3681e = Secure.getString(context.getContentResolver(), "android_id");
        if (this.f3686j == null) {
            try {
                this.f3686j = new CustomTaskQueue(context, new File(context.getFilesDir(), "gopay-vip-client-queue.dat"), this.f3687k);
                this.f3686j.start();
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
                this.f3684h.put(str, str2);
            } else {
                this.f3684h.remove(str);
            }
        }
    }

    public void setExtraHeader(String str, String str2) {
        if (str != null) {
            if (str2 != null) {
                this.f3685i.put(str, str2);
            } else {
                this.f3685i.remove(str);
            }
        }
    }

    public void addListener(Listener listener) {
        if (listener != null) {
            this.f3683g.add(listener);
        }
    }

    public void removeListener(Listener listener) {
        if (listener != null) {
            this.f3683g.remove(listener);
        }
    }

    public VipStatus getVipStatus() {
        return this.f3682f;
    }

    /* renamed from: a */
    private void m3987a(VipStatus vipStatus) {
        this.f3682f = vipStatus;
        m3990b(vipStatus);
        if (this.f3682f != null) {
            Log.v("goPay", String.format("VIP status for %s: %s / %s / %s", new Object[]{this.f3682f.getGuid(), Boolean.valueOf(this.f3682f.isVip()), Boolean.valueOf(this.f3682f.isSuspended()), this.f3682f.getSuspensionMessage()}));
            return;
        }
        Log.v("goPay", "VIP status cleared due to error");
    }

    public void checkVipStatus(final String str, boolean z) {
        if (str != null) {
            if (z || this.f3682f == null || !m3988a(this.f3682f.getGuid(), (Object) str)) {
                new Thread(new Runnable(this) {
                    /* renamed from: b */
                    final /* synthetic */ VipClient f3676b;

                    public void run() {
                        try {
                            Log.v("goPay", "Checking VIP status...");
                            this.f3676b.m3987a(this.f3676b.m3984a(str));
                        } catch (UnauthorizedException e) {
                            Log.e("goPay", "Unauthorized: " + e.getMessage());
                            this.f3676b.m3987a(null);
                        } catch (HttpException e2) {
                            Log.e("goPay", "HTTP response: " + e2.getMessage());
                            this.f3676b.m3987a(null);
                        } catch (IOException e3) {
                            Log.e("goPay", "I/O error checking VIP status");
                            this.f3676b.m3987a(null);
                        } catch (JSONException e4) {
                            Log.e("goPay", "JSON error checking VIP status");
                            this.f3676b.m3987a(null);
                        }
                    }
                }).start();
            }
        }
    }

    public void trackPurchase(PurchaseEvent purchaseEvent) {
        if (purchaseEvent != null) {
            this.f3686j.add(purchaseEvent);
        }
    }

    /* renamed from: a */
    private static boolean m3988a(Object obj, Object obj2) {
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
    private void m3990b(VipStatus vipStatus) {
        for (Listener listener : this.f3683g) {
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
    private Map<String, String> m3981a() {
        Map<String, String> linkedHashMap = new LinkedHashMap();
        if (this.f3684h != null) {
            for (Entry entry : this.f3684h.entrySet()) {
                String str = (String) entry.getKey();
                String str2 = (String) entry.getValue();
                if (!(str == null || str2 == null || linkedHashMap.containsKey(str))) {
                    linkedHashMap.put(str, str2);
                }
            }
        }
        if (this.f3677a != null) {
            linkedHashMap.put("appId", this.f3677a);
        }
        if (this.f3679c != null) {
            linkedHashMap.put("bundle_id", this.f3679c);
        }
        if (this.f3680d != null) {
            linkedHashMap.put("app_version", this.f3680d);
        }
        linkedHashMap.put("platform", AbstractSpiCall.ANDROID_CLIENT_TYPE);
        linkedHashMap.put(IdManager.OS_VERSION_FIELD, VERSION.RELEASE);
        linkedHashMap.put("sdk", "gopay-vip-sdk-android");
        linkedHashMap.put("sdk_version", BuildConfig.VERSION_NAME);
        if (this.f3681e != null) {
            linkedHashMap.put("device_id", this.f3681e);
        }
        return linkedHashMap;
    }

    /* renamed from: b */
    private Map<String, String> m3989b() {
        Map<String, String> a = m3981a();
        if (this.f3677a != null) {
            a.put("appId", this.f3677a);
        }
        return a;
    }

    /* renamed from: c */
    private Map<String, String> m3991c() {
        Map<String, String> a = m3981a();
        if (this.f3677a != null) {
            a.put("app_id", this.f3677a);
        }
        return a;
    }

    /* renamed from: a */
    private VipStatus m3984a(String str) throws JSONException, UnauthorizedException, HttpException, IOException {
        Map b = m3989b();
        if (str != null) {
            b.put("guid", str);
        }
        JSONObject a = C1414a.m3998a("https://gp-vip.gogame.net/vip/v1/get_vip_status/", b, this.f3678b, this.f3685i);
        return new VipStatus(str, a.optBoolean("vip_status", false), a.optBoolean("suspended", false), a.optString("suspension_message", null));
    }

    /* renamed from: a */
    private BaseBillingResponse m3982a(PurchaseEvent purchaseEvent) throws JSONException, UnauthorizedException, HttpException, IOException {
        if (purchaseEvent == null) {
            return null;
        }
        Map c = m3991c();
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
        JSONObject a = C1414a.m3998a("https://gp-vip.gogame.net/billing/v3/log_client_transaction/", c, this.f3678b, this.f3685i);
        BaseBillingResponse baseBillingResponse = new BaseBillingResponse();
        baseBillingResponse.setStatus(a.optBoolean("status", false));
        baseBillingResponse.setStatusCode(a.optInt("statusCode", 0));
        baseBillingResponse.setStatusMessage(a.optString("statusMsg", null));
        return baseBillingResponse;
    }
}
