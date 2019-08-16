package com.google.android.gms.security;

import android.content.Context;
import android.content.Intent;
import android.content.res.Resources.NotFoundException;
import android.support.annotation.Nullable;
import android.util.Log;
import com.google.android.gms.common.GoogleApiAvailabilityLight;
import com.google.android.gms.common.GooglePlayServicesUtilLight;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.CrashUtils;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.DynamiteModule.LoadingException;
import java.lang.reflect.Method;

public class ProviderInstaller {
    public static final String PROVIDER_NAME = "GmsCore_OpenSSL";
    private static final Object lock = new Object();
    /* access modifiers changed from: private */
    public static final GoogleApiAvailabilityLight zziv = GoogleApiAvailabilityLight.getInstance();
    private static Method zziw = null;

    public interface ProviderInstallListener {
        void onProviderInstallFailed(int i, Intent intent);

        void onProviderInstalled();
    }

    /* JADX WARNING: type inference failed for: r0v7, types: [java.lang.Throwable] */
    /* JADX WARNING: Multi-variable type inference failed */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public static void installIfNeeded(android.content.Context r7) throws com.google.android.gms.common.GooglePlayServicesRepairableException, com.google.android.gms.common.GooglePlayServicesNotAvailableException {
        /*
            r2 = 8
            java.lang.String r0 = "Context must not be null"
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r7, r0)
            com.google.android.gms.common.GoogleApiAvailabilityLight r0 = zziv
            r1 = 11925000(0xb5f608, float:1.6710484E-38)
            r0.verifyGooglePlayServicesIsAvailable(r7, r1)
            android.content.Context r0 = zzk(r7)
            if (r0 != 0) goto L_0x0019
            android.content.Context r0 = zzl(r7)
        L_0x0019:
            if (r0 != 0) goto L_0x0028
            java.lang.String r0 = "ProviderInstaller"
            java.lang.String r1 = "Failed to get remote context"
            android.util.Log.e(r0, r1)
            com.google.android.gms.common.GooglePlayServicesNotAvailableException r0 = new com.google.android.gms.common.GooglePlayServicesNotAvailableException
            r0.<init>(r2)
            throw r0
        L_0x0028:
            java.lang.Object r3 = lock
            monitor-enter(r3)
            java.lang.reflect.Method r1 = zziw     // Catch:{ Exception -> 0x0057 }
            if (r1 != 0) goto L_0x0049
            java.lang.ClassLoader r1 = r0.getClassLoader()     // Catch:{ Exception -> 0x0057 }
            java.lang.String r2 = "com.google.android.gms.common.security.ProviderInstallerImpl"
            java.lang.Class r1 = r1.loadClass(r2)     // Catch:{ Exception -> 0x0057 }
            java.lang.String r2 = "insertProvider"
            r4 = 1
            java.lang.Class[] r4 = new java.lang.Class[r4]     // Catch:{ Exception -> 0x0057 }
            r5 = 0
            java.lang.Class<android.content.Context> r6 = android.content.Context.class
            r4[r5] = r6     // Catch:{ Exception -> 0x0057 }
            java.lang.reflect.Method r1 = r1.getMethod(r2, r4)     // Catch:{ Exception -> 0x0057 }
            zziw = r1     // Catch:{ Exception -> 0x0057 }
        L_0x0049:
            java.lang.reflect.Method r1 = zziw     // Catch:{ Exception -> 0x0057 }
            r2 = 0
            r4 = 1
            java.lang.Object[] r4 = new java.lang.Object[r4]     // Catch:{ Exception -> 0x0057 }
            r5 = 0
            r4[r5] = r0     // Catch:{ Exception -> 0x0057 }
            r1.invoke(r2, r4)     // Catch:{ Exception -> 0x0057 }
            monitor-exit(r3)     // Catch:{ all -> 0x008d }
            return
        L_0x0057:
            r0 = move-exception
            java.lang.Throwable r1 = r0.getCause()     // Catch:{ all -> 0x008d }
            java.lang.String r2 = "ProviderInstaller"
            r4 = 6
            boolean r2 = android.util.Log.isLoggable(r2, r4)     // Catch:{ all -> 0x008d }
            if (r2 == 0) goto L_0x0080
            if (r1 != 0) goto L_0x0090
            java.lang.String r2 = r0.getMessage()     // Catch:{ all -> 0x008d }
        L_0x006b:
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ all -> 0x008d }
            int r4 = r2.length()     // Catch:{ all -> 0x008d }
            if (r4 == 0) goto L_0x0095
            java.lang.String r4 = "Failed to install provider: "
            java.lang.String r2 = r4.concat(r2)     // Catch:{ all -> 0x008d }
        L_0x007b:
            java.lang.String r4 = "ProviderInstaller"
            android.util.Log.e(r4, r2)     // Catch:{ all -> 0x008d }
        L_0x0080:
            if (r1 != 0) goto L_0x009d
        L_0x0082:
            com.google.android.gms.common.util.CrashUtils.addDynamiteErrorToDropBox(r7, r0)     // Catch:{ all -> 0x008d }
            com.google.android.gms.common.GooglePlayServicesNotAvailableException r0 = new com.google.android.gms.common.GooglePlayServicesNotAvailableException     // Catch:{ all -> 0x008d }
            r1 = 8
            r0.<init>(r1)     // Catch:{ all -> 0x008d }
            throw r0     // Catch:{ all -> 0x008d }
        L_0x008d:
            r0 = move-exception
            monitor-exit(r3)     // Catch:{ all -> 0x008d }
            throw r0
        L_0x0090:
            java.lang.String r2 = r1.getMessage()     // Catch:{ all -> 0x008d }
            goto L_0x006b
        L_0x0095:
            java.lang.String r2 = new java.lang.String     // Catch:{ all -> 0x008d }
            java.lang.String r4 = "Failed to install provider: "
            r2.<init>(r4)     // Catch:{ all -> 0x008d }
            goto L_0x007b
        L_0x009d:
            r0 = r1
            goto L_0x0082
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.security.ProviderInstaller.installIfNeeded(android.content.Context):void");
    }

    public static void installIfNeededAsync(Context context, ProviderInstallListener providerInstallListener) {
        Preconditions.checkNotNull(context, "Context must not be null");
        Preconditions.checkNotNull(providerInstallListener, "Listener must not be null");
        Preconditions.checkMainThread("Must be called on the UI thread");
        new zza(context, providerInstallListener).execute(new Void[0]);
    }

    @Nullable
    private static Context zzk(Context context) {
        try {
            return DynamiteModule.load(context, DynamiteModule.PREFER_HIGHEST_OR_LOCAL_VERSION_NO_FORCE_STAGING, "providerinstaller").getModuleContext();
        } catch (LoadingException e) {
            String valueOf = String.valueOf(e.getMessage());
            Log.w("ProviderInstaller", valueOf.length() != 0 ? "Failed to load providerinstaller module: ".concat(valueOf) : new String("Failed to load providerinstaller module: "));
            return null;
        }
    }

    @Nullable
    private static Context zzl(Context context) {
        try {
            return GooglePlayServicesUtilLight.getRemoteContext(context);
        } catch (NotFoundException e) {
            String valueOf = String.valueOf(e.getMessage());
            Log.w("ProviderInstaller", valueOf.length() != 0 ? "Failed to load GMS Core context for providerinstaller: ".concat(valueOf) : new String("Failed to load GMS Core context for providerinstaller: "));
            CrashUtils.addDynamiteErrorToDropBox(context, e);
            return null;
        }
    }
}
