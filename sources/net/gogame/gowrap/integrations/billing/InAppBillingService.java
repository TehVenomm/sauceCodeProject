package net.gogame.gowrap.integrations.billing;

import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.Parcelable;
import android.os.RemoteException;
import android.util.Log;
import com.android.vending.billing.IInAppBillingService;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.UUID;
import net.gogame.gopay.sdk.iab.PurchaseActivity;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.gopay.GoPaySupport;
import org.onepf.oms.appstore.AmazonAppstoreBillingService;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

public class InAppBillingService implements IInAppBillingService {
    private final IInAppBillingService delegate;
    private final Map<String, SkuDetails> skuDetailsMap = new HashMap();

    public InAppBillingService(IInAppBillingService iInAppBillingService) {
        this.delegate = iInAppBillingService;
    }

    public int isBillingSupported(int i, String str, String str2) throws RemoteException {
        int isBillingSupported = this.delegate.isBillingSupported(i, str, str2);
        Log.v(Constants.TAG, String.format("isBillingSupported(%d, %s, %s)=%d", new Object[]{Integer.valueOf(i), str, str2, Integer.valueOf(isBillingSupported)}));
        return isBillingSupported;
    }

    public Bundle getSkuDetails(int i, String str, String str2, Bundle bundle) throws RemoteException {
        Bundle skuDetails = this.delegate.getSkuDetails(i, str, str2, bundle);
        Log.v(Constants.TAG, String.format("getSkuDetails(%d, %s, %s, %s)=%s", new Object[]{Integer.valueOf(i), str, str2, bundle, skuDetails}));
        ArrayList stringArrayList = skuDetails.getStringArrayList("DETAILS_LIST");
        if (stringArrayList != null) {
            Iterator it = stringArrayList.iterator();
            while (it.hasNext()) {
                String str3 = (String) it.next();
                if (str3 != null) {
                    Log.v(Constants.TAG, str3);
                    try {
                        SkuDetails skuDetails2 = new SkuDetails(str3);
                        this.skuDetailsMap.put(skuDetails2.getSku(), skuDetails2);
                    } catch (Throwable e) {
                        Log.e(Constants.TAG, "Error parsing details", e);
                    }
                }
            }
        }
        return skuDetails;
    }

    public Bundle getBuyIntent(int i, String str, String str2, String str3, String str4) throws RemoteException {
        try {
            String guid = GoPaySupport.INSTANCE.getGuid();
            Context currentActivity = GoPaySupport.INSTANCE.getCurrentActivity();
            SkuDetails skuDetails = (SkuDetails) this.skuDetailsMap.get(str2);
            Intent intent = new Intent(currentActivity, PurchaseActivity.class);
            intent.putExtra("gid", GoPaySupport.INSTANCE.getAppId());
            intent.putExtra("guid", guid);
            intent.putExtra("payload", str4);
            intent.putExtra("sku", str2);
            intent.putExtra(AmazonAppstoreBillingService.JSON_KEY_RECEIPT_ITEM_TYPE, str3);
            intent.putExtra("referenceId", UUID.randomUUID().toString().replaceAll("-", ""));
            if (skuDetails != null) {
                intent.putExtra("json", skuDetails.getJson());
            }
            Parcelable activity = PendingIntent.getActivity(currentActivity, 0, intent, 134217728);
            Bundle bundle = new Bundle();
            bundle.putInt("RESPONSE_CODE", 0);
            bundle.putParcelable("BUY_INTENT", activity);
            Log.v(Constants.TAG, String.format("getBuyIntent(%d, %s, %s, %s, %s)=%s", new Object[]{Integer.valueOf(i), str, str2, str3, str4, bundle}));
            return bundle;
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
            return null;
        }
    }

    public Bundle getPurchases(int i, String str, String str2, String str3) throws RemoteException {
        Bundle purchases = this.delegate.getPurchases(i, str, str2, str3);
        Log.v(Constants.TAG, String.format("getPurchases(%d, %s, %s, %s)=%s", new Object[]{Integer.valueOf(i), str, str2, str3, purchases}));
        return purchases;
    }

    public int consumePurchase(int i, String str, String str2) throws RemoteException {
        int consumePurchase = this.delegate.consumePurchase(i, str, str2);
        Log.v(Constants.TAG, String.format("consumePurchase(%d, %s, %s)=%d", new Object[]{Integer.valueOf(i), str, str2, Integer.valueOf(consumePurchase)}));
        return consumePurchase;
    }

    public IBinder asBinder() {
        IBinder asBinder = this.delegate.asBinder();
        Log.v(Constants.TAG, String.format("asBinder()=%s", new Object[]{asBinder}));
        return asBinder;
    }
}
