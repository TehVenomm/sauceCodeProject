package io.fabric.sdk.android.services.common;

import android.content.Context;
import io.fabric.sdk.android.Fabric;
import io.fabric.sdk.android.services.cache.MemoryValueCache;
import io.fabric.sdk.android.services.cache.ValueLoader;

public class InstallerPackageNameProvider {
    private static final String NO_INSTALLER_PACKAGE_NAME = "";
    private final MemoryValueCache<String> installerPackageNameCache = new MemoryValueCache();
    private final ValueLoader<String> installerPackageNameLoader = new C12381();

    /* renamed from: io.fabric.sdk.android.services.common.InstallerPackageNameProvider$1 */
    class C12381 implements ValueLoader<String> {
        C12381() {
        }

        public String load(Context context) throws Exception {
            String installerPackageName = context.getPackageManager().getInstallerPackageName(context.getPackageName());
            return installerPackageName == null ? "" : installerPackageName;
        }
    }

    public String getInstallerPackageName(Context context) {
        try {
            String str = (String) this.installerPackageNameCache.get(context, this.installerPackageNameLoader);
            return "".equals(str) ? null : str;
        } catch (Throwable e) {
            Fabric.getLogger().mo4756e("Fabric", "Failed to determine installer package name", e);
            return null;
        }
    }
}
