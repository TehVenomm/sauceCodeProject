package net.gogame.gopay.sdk.iab;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.provider.Settings.Secure;
import com.facebook.appevents.AppEventsConstants;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import net.gogame.gopay.sdk.C1350h;
import net.gogame.gopay.sdk.C1378j;
import net.gogame.gopay.sdk.GetCountryDetailsResponse;
import net.gogame.gopay.sdk.GoPayInAppBillingServiceExt;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper;
import org.onepf.oms.appstore.googleUtils.IabHelper$OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

public class GoPayInAppBillingService implements GoPayInAppBillingServiceExt, AppstoreInAppBillingService {
    /* renamed from: a */
    private final String f3388a;
    /* renamed from: b */
    private String f3389b;
    /* renamed from: c */
    private final String f3390c;
    /* renamed from: d */
    private final Map f3391d = new HashMap();
    /* renamed from: e */
    private final boolean f3392e = false;

    public GoPayInAppBillingService(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4) {
        this.f3388a = str;
        this.f3389b = str2;
        this.f3390c = null;
        C1378j.m3898b(str4);
        C1378j.m3901c(str3);
        C1378j.m3905e(context.getPackageName());
        C1378j.m3903d(Secure.getString(context.getContentResolver(), "android_id"));
        try {
            C1378j.m3907f(context.getPackageManager().getPackageInfo(context.getPackageName(), 128).versionName);
        } catch (NameNotFoundException e) {
            C1378j.m3907f(AppEventsConstants.EVENT_PARAM_VALUE_NO);
        }
        m3788a();
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2) {
        this.f3388a = str;
        this.f3389b = str2;
        this.f3390c = null;
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2, @NotNull String str3) {
        this.f3388a = str;
        this.f3389b = str2;
        this.f3390c = str3;
    }

    /* renamed from: a */
    private void m3788a() {
        new C1357d(this).execute(new Void[0]);
    }

    public void consume(Purchase purchase) {
        throw new IabException(-1009, null);
    }

    public void dispose() {
    }

    public GetCountryDetailsResponse getCountries() {
        try {
            return C1378j.m3895b(this.f3388a, this.f3389b, null);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public GetCountryDetailsResponse getCountries(String str) {
        try {
            return C1378j.m3895b(this.f3388a, this.f3389b, str);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public String getCurrentCountry() {
        return C1378j.m3883a();
    }

    public Intent getPurchaseFlowIntent(Activity activity, String str, String str2, String str3) {
        Intent intent = new Intent(activity, PurchaseActivity.class);
        intent.putExtra("gid", this.f3388a);
        intent.putExtra("guid", this.f3389b);
        intent.putExtra("payload", str3);
        intent.putExtra("sku", str);
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, str2);
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        return intent;
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        C1358e c1358e = (C1358e) this.f3391d.get(Integer.valueOf(i));
        if (c1358e == null) {
            return false;
        }
        IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener = c1358e.f3523b;
        if (intent == null) {
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Bad response!"), null);
            }
            this.f3391d.remove(Integer.valueOf(i));
            return true;
        }
        int i3 = intent.getExtras().getInt("RESPONSE_CODE");
        String stringExtra = intent.getStringExtra("INAPP_PURCHASE_DATA");
        String stringExtra2 = intent.getStringExtra("INAPP_DATA_SIGNATURE");
        if (i2 == -1) {
            if (stringExtra == null) {
                new StringBuilder("Extras: ").append(intent.getExtras());
                if (iabHelper$OnIabPurchaseFinishedListener != null) {
                    iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1008, "IAB returned null purchaseData or dataSignature"), null);
                }
            } else {
                Purchase purchase;
                try {
                    purchase = new Purchase(c1358e.f3522a, stringExtra, stringExtra2, "GoGameStore");
                } catch (JSONException e) {
                    e.printStackTrace();
                    if (iabHelper$OnIabPurchaseFinishedListener != null) {
                        iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Failed to parse purchase data."), null);
                    }
                    purchase = null;
                }
                if (iabHelper$OnIabPurchaseFinishedListener != null) {
                    iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(0, "Success"), purchase);
                }
            }
        } else if (i2 == 0) {
            new StringBuilder("Purchase canceled - Response: ").append(IabHelper.getResponseDesc(i3));
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1005, "User canceled."), null);
            }
        } else {
            new StringBuilder("In-app billing error: Purchase failed. Result code: ").append(Integer.toString(i2)).append(". Response: ").append(IabHelper.getResponseDesc(i3));
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1006, "Unknown purchase response."), null);
            }
        }
        this.f3391d.remove(Integer.valueOf(i));
        return true;
    }

    public int isBillingSupported(int i, String str, String str2) {
        return 0;
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, IabHelper$OnIabPurchaseFinishedListener iabHelper$OnIabPurchaseFinishedListener, String str3) {
        try {
            Intent purchaseFlowIntent = getPurchaseFlowIntent(activity, str, str2, str3);
            this.f3391d.put(Integer.valueOf(i), new C1358e(str, str2, iabHelper$OnIabPurchaseFinishedListener));
            activity.startActivityForResult(purchaseFlowIntent, i);
        } catch (Exception e) {
            if (iabHelper$OnIabPurchaseFinishedListener != null) {
                iabHelper$OnIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1004, "Failed to send intent."), null);
            }
        }
    }

    @Nullable
    public Inventory queryInventory(boolean z, List list, List list2) {
        C1350h a = C1378j.m3890a(this.f3388a, this.f3389b, null);
        Inventory inventory = new Inventory();
        if (a.f3372b != null) {
            for (SkuDetails addSkuDetails : a.f3372b) {
                inventory.addSkuDetails(addSkuDetails);
            }
            setCurrentCountry(a.f3371a);
        }
        return inventory;
    }

    public void setCurrentCountry(String str) {
        C1378j.m3893a(str);
    }

    public void setEmail(String str) {
        C1378j.m3901c(str);
        m3788a();
    }

    public void setGameLanguage(String str) {
        C1378j.m3909g(str);
    }

    public void setGameUserId(String str) {
        if (!this.f3389b.equals(str)) {
            this.f3389b = str;
        }
    }

    public void startSetup(OnIabSetupFinishedListener onIabSetupFinishedListener) {
        if (onIabSetupFinishedListener != null) {
            onIabSetupFinishedListener.onIabSetupFinished(new IabResult(0, "Setup successful."));
        }
    }

    public boolean subscriptionsSupported() {
        return false;
    }
}
