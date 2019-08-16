package org.onepf.oms.appstore;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.text.TextUtils;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.Arrays;
import org.jetbrains.annotations.NotNull;
import org.jetbrains.annotations.Nullable;
import org.onepf.oms.AppstoreInAppBillingService;
import org.onepf.oms.DefaultAppstore;
import org.onepf.oms.OpenIabHelper;
import org.onepf.oms.appstore.nokiaUtils.NokiaSkuFormatException;
import org.onepf.oms.appstore.nokiaUtils.NokiaStoreHelper;
import org.onepf.oms.util.Logger;

public class NokiaStore extends DefaultAppstore {
    private static final String EXPECTED_SHA1_FINGERPRINT = "C476A7D71C4CB92641A699C1F1CAC93CA81E0396";
    public static final String NOKIA_BILLING_PERMISSION = "com.nokia.payment.BILLING";
    public static final String NOKIA_INSTALLER = "com.nokia.payment.iapenabler";
    public static final String VENDING_ACTION = "com.nokia.payment.iapenabler.InAppBillingService.BIND";
    @Nullable
    private NokiaStoreHelper billingService = null;
    private final Context context;

    public NokiaStore(Context context2) {
        Logger.m1031i("NokiaStore.NokiaStore");
        this.context = context2;
    }

    public static void checkSku(@NotNull String str) {
        if (!TextUtils.isDigitsOnly(str)) {
            throw new NokiaSkuFormatException();
        }
    }

    @NotNull
    private static byte[] hexStringToByteArray(@NotNull String str) {
        int length = str.length();
        byte[] bArr = new byte[(length / 2)];
        for (int i = 0; i < length; i += 2) {
            bArr[i / 2] = (byte) ((byte) ((Character.digit(str.charAt(i), 16) << 4) + Character.digit(str.charAt(i + 1), 16)));
        }
        return bArr;
    }

    private boolean verifyFingreprint() {
        try {
            PackageInfo packageInfo = this.context.getPackageManager().getPackageInfo(NOKIA_INSTALLER, 64);
            if (packageInfo.signatures.length == 1) {
                if (Arrays.equals(MessageDigest.getInstance("SHA1").digest(packageInfo.signatures[0].toByteArray()), hexStringToByteArray(EXPECTED_SHA1_FINGERPRINT))) {
                    Logger.m1032i("isBillingAvailable", "NIAP signature verified");
                    return true;
                }
            }
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        } catch (NameNotFoundException e2) {
            e2.printStackTrace();
        }
        return false;
    }

    public String getAppstoreName() {
        return OpenIabHelper.NAME_NOKIA;
    }

    @Nullable
    public AppstoreInAppBillingService getInAppBillingService() {
        if (this.billingService == null) {
            this.billingService = new NokiaStoreHelper(this.context, this);
        }
        return this.billingService;
    }

    public int getPackageVersion(String str) {
        Logger.m1025d("getPackageVersion: packageName = " + str);
        return -1;
    }

    public boolean isBillingAvailable(String str) {
        Logger.m1031i("NokiaStore.isBillingAvailable");
        Logger.m1026d("packageName = ", str);
        for (PackageInfo packageInfo : this.context.getPackageManager().getInstalledPackages(0)) {
            if (NOKIA_INSTALLER.equals(packageInfo.packageName)) {
                return verifyFingreprint();
            }
        }
        return false;
    }

    public boolean isPackageInstaller(String str) {
        Logger.m1026d("sPackageInstaller: packageName = ", str);
        String installerPackageName = this.context.getPackageManager().getInstallerPackageName(str);
        Logger.m1026d("installerPackageName = ", installerPackageName);
        return NOKIA_INSTALLER.equals(installerPackageName);
    }
}
