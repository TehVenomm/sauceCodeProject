package com.google.android.gms.drive.metadata.internal;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.UserMetadata;
import java.util.Arrays;
import java.util.Collections;
import net.gogame.gowrap.integrations.AbstractIntegrationSupport;

public final class zzu extends zzm<UserMetadata> {
    public zzu(String str, int i) {
        super(str, Arrays.asList(new String[]{zzab(str, "permissionId"), zzab(str, "displayName"), zzab(str, "picture"), zzab(str, "isAuthenticatedUser"), zzab(str, "emailAddress")}), Collections.emptyList(), 6000000);
    }

    private static String zzab(String str, String str2) {
        return new StringBuilder((String.valueOf(str).length() + 1) + String.valueOf(str2).length()).append(str).append(AbstractIntegrationSupport.DEFAULT_EVENT_NAME_DELIMITER).append(str2).toString();
    }

    private final String zzgt(String str) {
        return zzab(getName(), str);
    }

    protected final boolean zzb(DataHolder dataHolder, int i, int i2) {
        return dataHolder.zzft(zzgt("permissionId")) && !dataHolder.zzh(zzgt("permissionId"), i, i2);
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        String zzd = dataHolder.zzd(zzgt("permissionId"), i, i2);
        if (zzd == null) {
            return null;
        }
        String zzd2 = dataHolder.zzd(zzgt("displayName"), i, i2);
        String zzd3 = dataHolder.zzd(zzgt("picture"), i, i2);
        boolean zze = dataHolder.zze(zzgt("isAuthenticatedUser"), i, i2);
        return new UserMetadata(zzd, zzd2, zzd3, Boolean.valueOf(zze).booleanValue(), dataHolder.zzd(zzgt("emailAddress"), i, i2));
    }
}
