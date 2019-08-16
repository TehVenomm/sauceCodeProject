package net.gogame.gopay.sdk.iab;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager.NameNotFoundException;
import android.provider.Settings.Secure;
import com.facebook.appevents.AppEventsConstants;
import com.facebook.share.internal.MessengerShareContentUtility;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;
import net.gogame.gopay.sdk.C1363h;
import net.gogame.gopay.sdk.C1406j;
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
    /* access modifiers changed from: private */

    /* renamed from: a */
    public final String f1231a;
    /* access modifiers changed from: private */

    /* renamed from: b */
    public String f1232b;

    /* renamed from: c */
    private final String f1233c;

    /* renamed from: d */
    private final Map f1234d = new HashMap();

    /* renamed from: e */
    private final boolean f1235e = false;

    public GoPayInAppBillingService(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4) {
        this.f1231a = str;
        this.f1232b = str2;
        this.f1233c = null;
        C1406j.m873b(str4);
        C1406j.m876c(str3);
        C1406j.m880e(context.getPackageName());
        C1406j.m878d(Secure.getString(context.getContentResolver(), "android_id"));
        try {
            C1406j.m882f(context.getPackageManager().getPackageInfo(context.getPackageName(), 128).versionName);
        } catch (NameNotFoundException e) {
            C1406j.m882f(AppEventsConstants.EVENT_PARAM_VALUE_NO);
        }
        m949a();
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2) {
        this.f1231a = str;
        this.f1232b = str2;
        this.f1233c = null;
    }

    @Deprecated
    public GoPayInAppBillingService(@NotNull String str, @NotNull String str2, @NotNull String str3) {
        this.f1231a = str;
        this.f1232b = str2;
        this.f1233c = str3;
    }

    /* renamed from: a */
    private void m949a() {
        new C1625d(this).execute(new Void[0]);
    }

    public void consume(Purchase purchase) {
        throw new IabException(-1009, (String) null);
    }

    public void dispose() {
    }

    public GetCountryDetailsResponse getCountries() {
        try {
            return C1406j.m870b(this.f1231a, this.f1232b, null);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public GetCountryDetailsResponse getCountries(String str) {
        try {
            return C1406j.m870b(this.f1231a, this.f1232b, str);
        } catch (Exception e) {
            throw new IabException(-1008, e.getLocalizedMessage());
        }
    }

    public String getCurrentCountry() {
        return C1406j.m858a();
    }

    public Intent getPurchaseFlowIntent(Activity activity, String str, String str2, String str3) {
        Intent intent = new Intent(activity, PurchaseActivity.class);
        intent.putExtra("gid", this.f1231a);
        intent.putExtra("guid", this.f1232b);
        intent.putExtra(MessengerShareContentUtility.ATTACHMENT_PAYLOAD, str3);
        intent.putExtra("sku", str);
        intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, str2);
        intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
        return intent;
    }

    public boolean handleActivityResult(int i, int i2, Intent intent) {
        Purchase purchase;
        C1626e eVar = (C1626e) this.f1234d.get(Integer.valueOf(i));
        if (eVar == null) {
            return false;
        }
        OnIabPurchaseFinishedListener onIabPurchaseFinishedListener = eVar.f1281b;
        if (intent == null) {
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1002, "Bad response!"), null);
            }
            this.f1234d.remove(Integer.valueOf(i));
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
                try {
                    purchase = new Purchase(eVar.f1280a, stringExtra, stringExtra2, "GoGameStore");
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
        this.f1234d.remove(Integer.valueOf(i));
        return true;
    }

    public int isBillingSupported(int i, String str, String str2) {
        return 0;
    }

    public void launchPurchaseFlow(Activity activity, String str, String str2, int i, OnIabPurchaseFinishedListener onIabPurchaseFinishedListener, String str3) {
        try {
            Intent purchaseFlowIntent = getPurchaseFlowIntent(activity, str, str2, str3);
            this.f1234d.put(Integer.valueOf(i), new C1626e(str, str2, onIabPurchaseFinishedListener));
            activity.startActivityForResult(purchaseFlowIntent, i);
        } catch (Exception e) {
            if (onIabPurchaseFinishedListener != null) {
                onIabPurchaseFinishedListener.onIabPurchaseFinished(new IabResult(-1004, "Failed to send intent."), null);
            }
        }
    }

    @Nullable
    public Inventory queryInventory(boolean z, List list, List list2) {
        C1363h a = C1406j.m865a(this.f1231a, this.f1232b, null);
        Inventory inventory = new Inventory();
        if (a.f1010b != null) {
            for (SkuDetails addSkuDetails : a.f1010b) {
                inventory.addSkuDetails(addSkuDetails);
            }
            setCurrentCountry(a.f1009a);
        }
        return inventory;
    }

    public void setCurrentCountry(String str) {
        C1406j.m868a(str);
    }

    public void setEmail(String str) {
        C1406j.m876c(str);
        m949a();
    }

    public void setGameLanguage(String str) {
        C1406j.m884g(str);
    }

    public void setGameUserId(String str) {
        if (!this.f1232b.equals(str)) {
            this.f1232b = str;
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
