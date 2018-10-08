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
import net.gogame.gopay.sdk.C1034h;
import net.gogame.gopay.sdk.C1062j;
import net.gogame.gopay.sdk.GetCountryDetailsResponse;
import net.gogame.gopay.sdk.GoPayInAppBillingServiceExt;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.json.JSONException;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabHelper;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabPurchaseFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabHelper.OnIabSetupFinishedListener;
import org.onepf.oms.appstore.googleUtils.IabResult;
import org.onepf.oms.appstore.googleUtils.Inventory;
import org.onepf.oms.appstore.googleUtils.Purchase;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

public class GoPayInAppBillingService implements GoPayInAppBillingServiceExt, AppstoreInAppBillingService {
    /* renamed from: a */
    private final String f1000a;
    /* renamed from: b */
    private String f1001b;
    /* renamed from: c */
    private final String f1002c;
    /* renamed from: d */
    private final Map f1003d = new HashMap();
    /* renamed from: e */
    private final boolean f1004e = false;

    public GoPayInAppBillingService(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4) {
        this.f1000a = str;
        this.f1001b = str2;
        this.f1002c = null;
        C1062j.m873b(str4);
        C1062j.m876c(str3);
        C1062j.m880e(context.getPackageName());
        C1062j.m878d(Secure.getString(context.getContentResolver(), "android_id"));
        try {
            C1062j.m882f(context.getPackageManager().getPackageInfo(context.getPackageName(), 128).versionName);
        } catch (NameNotFoundException e) {
            C1062j.m882f(AppEventsConstants.EVENT_PARAM_VALUE_NO);
        }
        m763a();
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2) {
        this.f1000a = str;
        this.f1001b = str2;
        this.f1002c = null;
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2, @NotNull String str3) {
        this.f1000a = str;
        this.f1001b = str2;
        this.f1002c = str3;
    }

    /* renamed from: a */
    private void m763a() {
        new C1041d(this).execute(new Void[0]);
    }

    public void consume(Purchase purchase) {
        throw new IabException(-1009, null);
    }

    public void dispose() {
    }

    public GetCountryDetailsResponse getCountries() {
        try {
            return C1062j.m870b(this.f1000a, this.f1001b, null);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public GetCountryDetailsResponse getCountries(String str) {
        try {
            return C1062j.m870b(this.f1000a, this.f1001b, str);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public String getCurrentCountry() {
        return C1062j.m858a();
    }

    public Intent getPurchaseFlowIntent(Activity activity, String str, String str2, String str3) {
        Intent intent = new Intent(activity, PurchaseActivity.class);
        intent.putExtra("gid", this.f1000a);
        intent.putExtra("guid", this.f1001b);
        intent.putExtra("payload", str3);
        intent.putExtra("sku", str);
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, str2);
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        return intent;
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        C1042e c1042e = (C1042e) this.f1003d.get(Integer.valueOf(i));
        if (c1042e == null) {
            return false;
        }
        OnIabPurchaseFinishedListener onIabPurchaseFinishedListener = c1042e.f1135b;
        if (intent == null) {
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Bad response!"), null);
            }
            this.f1003d.remove(Integer.valueOf(i));
            return true;
        }
        int i3 = intent.getExtras().getInt("RESPONSE_CODE");
        String stringExtra = intent.getStringExtra("INAPP_PURCHASE_DATA");
        String stringExtra2 = intent.getStringExtra("INAPP_DATA_SIGNATURE");
        if (i2 == -1) {
            if (stringExtra == null) {
                new StringBuilder("Extras: ").append(intent.getExtras());
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1008, "IAB returned null purchaseData or dataSignature"), null);
                }
            } else {
                Purchase purchase;
                try {
                    purchase = new Purchase(c1042e.f1134a, stringExtra, stringExtra2, "GoGameStore");
                } catch (JSONException e) {
                    e.printStackTrace();
                    if (onIabPurchaseFinishedListener != null) {
                        onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Failed to parse purchase data."), null);
                    }
                    purchase = null;
                }
                if (onIabPurchaseFinishedListener != null) {
                    onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(0, "Success"), purchase);
                }
            }
        } else if (i2 == 0) {
            new StringBuilder("Purchase canceled - Response: ").append(IabHelper.getResponseDesc(i3));
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1005, "User canceled."), null);
            }
        } else {
            new StringBuilder("In-app billing error: Purchase failed. Result code: ").append(Integer.toString(i2)).append(". Response: ").append(IabHelper.getResponseDesc(i3));
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1006, "Unknown purchase response."), null);
            }
        }
        this.f1003d.remove(Integer.valueOf(i));
        return true;
    }

    public int isBillingSupported(int i, String str, String str2) {
        return 0;
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        try {
            Intent purchaseFlowIntent = getPurchaseFlowIntent(activity, str, str2, str3);
            this.f1003d.put(Integer.valueOf(i), new C1042e(str, str2, onIabPurchaseFinishedListener));
            activity.startActivityForResult(purchaseFlowIntent, i);
        } catch (Exception e) {
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1004, "Failed to send intent."), null);
            }
        }
    }

    @Nullable
    public Inventory queryInventory(boolean z, List list, List list2) {
        C1034h a = C1062j.m865a(this.f1000a, this.f1001b, null);
        Inventory inventory = new Inventory();
        if (a.f984b != null) {
            for (SkuDetails addSkuDetails : a.f984b) {
                inventory.addSkuDetails(addSkuDetails);
            }
            setCurrentCountry(a.f983a);
        }
        return inventory;
    }

    public void setCurrentCountry(String str) {
        C1062j.m868a(str);
    }

    public void setEmail(String str) {
        C1062j.m876c(str);
        m763a();
    }

    public void setGameLanguage(String str) {
        C1062j.m884g(str);
    }

    public void setGameUserId(String str) {
        if (!this.f1001b.equals(str)) {
            this.f1001b = str;
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
