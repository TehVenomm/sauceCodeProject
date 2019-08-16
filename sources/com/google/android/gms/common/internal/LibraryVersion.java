package com.google.android.gms.common.internal;

import com.google.android.gms.common.annotation.KeepForSdk;
import com.google.android.gms.common.util.VisibleForTesting;
import java.util.concurrent.ConcurrentHashMap;

@KeepForSdk
public class LibraryVersion {
    private static final GmsLogger zzel = new GmsLogger("LibraryVersion", "");
    private static LibraryVersion zzem = new LibraryVersion();
    private ConcurrentHashMap<String, String> zzen = new ConcurrentHashMap<>();

    @VisibleForTesting
    protected LibraryVersion() {
    }

    @KeepForSdk
    public static LibraryVersion getInstance() {
        return zzem;
    }

    /* JADX WARNING: Removed duplicated region for block: B:12:0x006e  */
    /* JADX WARNING: Removed duplicated region for block: B:23:0x00ad  */
    /* JADX WARNING: Removed duplicated region for block: B:25:0x00ba  */
    @com.google.android.gms.common.annotation.KeepForSdk
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public java.lang.String getVersion(@android.support.annotation.NonNull java.lang.String r7) {
        /*
            r6 = this;
            r1 = 0
            java.lang.String r0 = "Please provide a valid libraryName"
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r7, r0)
            java.util.concurrent.ConcurrentHashMap<java.lang.String, java.lang.String> r0 = r6.zzen
            boolean r0 = r0.containsKey(r7)
            if (r0 == 0) goto L_0x0017
            java.util.concurrent.ConcurrentHashMap<java.lang.String, java.lang.String> r0 = r6.zzen
            java.lang.Object r0 = r0.get(r7)
            java.lang.String r0 = (java.lang.String) r0
        L_0x0016:
            return r0
        L_0x0017:
            java.util.Properties r0 = new java.util.Properties
            r0.<init>()
            java.lang.Class<com.google.android.gms.common.internal.LibraryVersion> r2 = com.google.android.gms.common.internal.LibraryVersion.class
            java.lang.String r3 = "/%s.properties"
            r4 = 1
            java.lang.Object[] r4 = new java.lang.Object[r4]     // Catch:{ IOException -> 0x00a0 }
            r5 = 0
            r4[r5] = r7     // Catch:{ IOException -> 0x00a0 }
            java.lang.String r3 = java.lang.String.format(r3, r4)     // Catch:{ IOException -> 0x00a0 }
            java.io.InputStream r2 = r2.getResourceAsStream(r3)     // Catch:{ IOException -> 0x00a0 }
            if (r2 == 0) goto L_0x007f
            r0.load(r2)     // Catch:{ IOException -> 0x00a0 }
            java.lang.String r2 = "version"
            r3 = 0
            java.lang.String r1 = r0.getProperty(r2, r3)     // Catch:{ IOException -> 0x00a0 }
            com.google.android.gms.common.internal.GmsLogger r0 = zzel     // Catch:{ IOException -> 0x00c2 }
            java.lang.String r2 = java.lang.String.valueOf(r7)     // Catch:{ IOException -> 0x00c2 }
            int r2 = r2.length()     // Catch:{ IOException -> 0x00c2 }
            java.lang.String r3 = java.lang.String.valueOf(r1)     // Catch:{ IOException -> 0x00c2 }
            int r3 = r3.length()     // Catch:{ IOException -> 0x00c2 }
            java.lang.StringBuilder r4 = new java.lang.StringBuilder     // Catch:{ IOException -> 0x00c2 }
            int r2 = r2 + 12
            int r2 = r2 + r3
            r4.<init>(r2)     // Catch:{ IOException -> 0x00c2 }
            java.lang.String r2 = "LibraryVersion"
            java.lang.StringBuilder r3 = r4.append(r7)     // Catch:{ IOException -> 0x00c2 }
            java.lang.String r4 = " version is "
            java.lang.StringBuilder r3 = r3.append(r4)     // Catch:{ IOException -> 0x00c2 }
            java.lang.StringBuilder r3 = r3.append(r1)     // Catch:{ IOException -> 0x00c2 }
            java.lang.String r3 = r3.toString()     // Catch:{ IOException -> 0x00c2 }
            r0.mo13935v(r2, r3)     // Catch:{ IOException -> 0x00c2 }
            r0 = r1
        L_0x006c:
            if (r0 != 0) goto L_0x0079
            java.lang.String r0 = "UNKNOWN"
            com.google.android.gms.common.internal.GmsLogger r1 = zzel
            java.lang.String r2 = "LibraryVersion"
            java.lang.String r3 = ".properties file is dropped during release process. Failure to read app version isexpected druing Google internal testing where locally-built libraries are used"
            r1.mo13926d(r2, r3)
        L_0x0079:
            java.util.concurrent.ConcurrentHashMap<java.lang.String, java.lang.String> r1 = r6.zzen
            r1.put(r7, r0)
            goto L_0x0016
        L_0x007f:
            com.google.android.gms.common.internal.GmsLogger r2 = zzel     // Catch:{ IOException -> 0x00a0 }
            java.lang.String r0 = java.lang.String.valueOf(r7)     // Catch:{ IOException -> 0x00a0 }
            int r3 = r0.length()     // Catch:{ IOException -> 0x00a0 }
            if (r3 == 0) goto L_0x0098
            java.lang.String r3 = "Failed to get app version for libraryName: "
            java.lang.String r0 = r3.concat(r0)     // Catch:{ IOException -> 0x00a0 }
        L_0x0091:
            java.lang.String r3 = "LibraryVersion"
            r2.mo13928e(r3, r0)     // Catch:{ IOException -> 0x00a0 }
            r0 = r1
            goto L_0x006c
        L_0x0098:
            java.lang.String r0 = new java.lang.String     // Catch:{ IOException -> 0x00a0 }
            java.lang.String r3 = "Failed to get app version for libraryName: "
            r0.<init>(r3)     // Catch:{ IOException -> 0x00a0 }
            goto L_0x0091
        L_0x00a0:
            r0 = move-exception
        L_0x00a1:
            com.google.android.gms.common.internal.GmsLogger r3 = zzel
            java.lang.String r2 = java.lang.String.valueOf(r7)
            int r4 = r2.length()
            if (r4 == 0) goto L_0x00ba
            java.lang.String r4 = "Failed to get app version for libraryName: "
            java.lang.String r2 = r4.concat(r2)
        L_0x00b3:
            java.lang.String r4 = "LibraryVersion"
            r3.mo13929e(r4, r2, r0)
            r0 = r1
            goto L_0x006c
        L_0x00ba:
            java.lang.String r2 = new java.lang.String
            java.lang.String r4 = "Failed to get app version for libraryName: "
            r2.<init>(r4)
            goto L_0x00b3
        L_0x00c2:
            r0 = move-exception
            goto L_0x00a1
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.common.internal.LibraryVersion.getVersion(java.lang.String):java.lang.String");
    }
}
