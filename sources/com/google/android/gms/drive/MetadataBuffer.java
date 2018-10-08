package com.google.android.gms.drive;

import com.google.android.gms.common.data.AbstractDataBuffer;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.metadata.MetadataField;
import com.google.android.gms.drive.metadata.internal.MetadataBundle;
import com.google.android.gms.drive.metadata.internal.zzf;
import com.google.android.gms.internal.zzbhy;
import com.google.android.gms.internal.zzbnr;

public final class MetadataBuffer extends AbstractDataBuffer<Metadata> {
    private zza zzgdz;

    static final class zza extends Metadata {
        private final DataHolder zzfkz;
        private final int zzfqc;
        private final int zzgea;

        public zza(DataHolder dataHolder, int i) {
            this.zzfkz = dataHolder;
            this.zzgea = i;
            this.zzfqc = dataHolder.zzbw(i);
        }

        public final /* synthetic */ Object freeze() {
            MetadataBundle zzant = MetadataBundle.zzant();
            for (MetadataField metadataField : zzf.zzanr()) {
                if (metadataField != zzbnr.zzgly) {
                    metadataField.zza(this.zzfkz, zzant, this.zzgea, this.zzfqc);
                }
            }
            return new zzbhy(zzant);
        }

        public final boolean isDataValid() {
            return !this.zzfkz.isClosed();
        }

        public final <T> T zza(MetadataField<T> metadataField) {
            return metadataField.zza(this.zzfkz, this.zzgea, this.zzfqc);
        }
    }

    public MetadataBuffer(DataHolder dataHolder) {
        super(dataHolder);
        dataHolder.zzafh().setClassLoader(MetadataBuffer.class.getClassLoader());
    }

    public final Metadata get(int i) {
        zza zza = this.zzgdz;
        if (zza != null && zza.zzgea == i) {
            return zza;
        }
        Metadata zza2 = new zza(this.zzfkz, i);
        this.zzgdz = zza2;
        return zza2;
    }

    @Deprecated
    public final String getNextPageToken() {
        return null;
    }

    public final void release() {
        if (this.zzfkz != null) {
            zzf.zzb(this.zzfkz);
        }
        super.release();
    }
}
