package com.google.android.gms.drive.query.internal;

import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.zzb;
import com.google.android.gms.drive.query.Filter;
import java.util.List;

public final class zzk implements zzj<Boolean> {
    private Boolean zzgnt = Boolean.valueOf(false);

    private zzk() {
    }

    public static boolean zza(Filter filter) {
        return filter == null ? false : ((Boolean) filter.zza(new zzk())).booleanValue();
    }

    public final /* synthetic */ Object zza(zzb zzb, Object obj) {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zza(zzx zzx, MetadataField metadataField, Object obj) {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zza(zzx zzx, List list) {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzany() {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzanz() {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzd(MetadataField metadataField) {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzd(MetadataField metadataField, Object obj) {
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzgu(String str) {
        if (!str.isEmpty()) {
            this.zzgnt = Boolean.valueOf(true);
        }
        return this.zzgnt;
    }

    public final /* synthetic */ Object zzv(Object obj) {
        return this.zzgnt;
    }
}
