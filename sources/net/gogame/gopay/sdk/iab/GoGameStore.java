package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.content.Intent;
import net.gogame.gopay.sdk.GetCountryDetailsResponse;
import net.gogame.gopay.sdk.GoPayInAppBillingServiceExt;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.Appstore;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.OpenIabHelper.Options.Builder;
import org.onepf.oms.appstore.googleUtils.IabException;
import org.onepf.oms.appstore.googleUtils.IabResult;

public class GoGameStore implements GoPayInAppBillingServiceExt, Appstore {

    /* renamed from: a */
    private final GoPayInAppBillingService f1209a;

    /* renamed from: b */
    private final Context f1210b;

    @Deprecated
    public GoGameStore(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3) {
        this.f1210b = context;
        this.f1209a = new GoPayInAppBillingService(context, str, str2, null, str3);
    }

    public GoGameStore(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4) {
        this.f1210b = context;
        this.f1209a = new GoPayInAppBillingService(context, str, str2, str4, str3);
    }

    public GoGameStore(@NotNull Context context, @NotNull String str, @NotNull String str2, @NotNull String str3, @NotNull String str4, @NotNull String str5) {
        this(context, str, str2, str3, str4);
        this.f1209a.setGameLanguage(str5);
    }

    @Deprecated
    public static OpenIabHelper newOpenIabHelper(Context context, String str, String str2, String str3) {
        return new OpenIabHelper(context, new Builder().addAvailableStoreNames("GoGameStore").addAvailableStores(new GoGameStore(context, str, str2, str3)).setCheckInventory(false).setStoreSearchStrategy(1).build());
    }

    public boolean areOutsideLinksAllowed() {
        return false;
    }

    public String getAppstoreName() {
        return "GoGameStore";
    }

    public GetCountryDetailsResponse getCountries() {
        GoPayInAppBillingService goPayInAppBillingService = (GoPayInAppBillingService) getInAppBillingService();
        if (goPayInAppBillingService != null) {
            return goPayInAppBillingService.getCountries();
        }
        throw new IabException(new IabResult(-1008, "Unable to acquire billing service"));
    }

    public GetCountryDetailsResponse getCountries(@NotNull String str) {
        GoPayInAppBillingService goPayInAppBillingService = (GoPayInAppBillingService) getInAppBillingService();
        if (goPayInAppBillingService != null) {
            return goPayInAppBillingService.getCountries(str);
        }
        throw new IabException(new IabResult(-1008, "Unable to acquire billing service"));
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        return this.f1209a;
    }

    public int getPackageVersion(String str) {
        return 0;
    }

    @Nullable
    public Intent getProductPageIntent(String str) {
        return null;
    }

    @Nullable
    public Intent getRateItPageIntent(String str) {
        return null;
    }

    @Nullable
    public Intent getSameDeveloperPageIntent(String str) {
        return null;
    }

    public boolean isBillingAvailable(String str) {
        return true;
    }

    public boolean isPackageInstaller(String str) {
        return false;
    }
}
