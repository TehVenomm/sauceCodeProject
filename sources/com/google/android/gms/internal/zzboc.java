package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.v4.util.LongSparseArray;
import android.util.SparseArray;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.metadata.CustomPropertyKey;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties;
import com.google.android.gms.drive.metadata.internal.AppVisibleCustomProperties.zza;
import com.google.android.gms.drive.metadata.internal.zzc;
import com.google.android.gms.drive.metadata.internal.zzg;
import com.google.android.gms.drive.metadata.internal.zzm;
import java.util.Arrays;

public class zzboc extends zzm<AppVisibleCustomProperties> {
    public static final zzg zzgmk = new zzbod();

    public zzboc(int i) {
        super("customProperties", Arrays.asList(new String[]{"hasCustomProperties", "sqlId"}), Arrays.asList(new String[]{"customPropertiesExtra", "customPropertiesExtraHolder"}), 5000000);
    }

    private static void zzd(DataHolder dataHolder) {
        Bundle zzafh = dataHolder.zzafh();
        if (zzafh != null) {
            synchronized (dataHolder) {
                DataHolder dataHolder2 = (DataHolder) zzafh.getParcelable("customPropertiesExtraHolder");
                if (dataHolder2 != null) {
                    dataHolder2.close();
                    zzafh.remove("customPropertiesExtraHolder");
                }
            }
        }
    }

    private static AppVisibleCustomProperties zzf(DataHolder dataHolder, int i, int i2) {
        Bundle zzafh = dataHolder.zzafh();
        SparseArray sparseParcelableArray = zzafh.getSparseParcelableArray("customPropertiesExtra");
        if (sparseParcelableArray == null) {
            if (zzafh.getParcelable("customPropertiesExtraHolder") != null) {
                synchronized (dataHolder) {
                    DataHolder dataHolder2 = (DataHolder) dataHolder.zzafh().getParcelable("customPropertiesExtraHolder");
                    if (dataHolder2 == null) {
                    } else {
                        try {
                            int i3;
                            zza zza;
                            Bundle zzafh2 = dataHolder2.zzafh();
                            String string = zzafh2.getString("entryIdColumn");
                            String string2 = zzafh2.getString("keyColumn");
                            String string3 = zzafh2.getString("visibilityColumn");
                            String string4 = zzafh2.getString("valueColumn");
                            LongSparseArray longSparseArray = new LongSparseArray();
                            for (i3 = 0; i3 < dataHolder2.getCount(); i3++) {
                                int zzbw = dataHolder2.zzbw(i3);
                                long zzb = dataHolder2.zzb(string, i3, zzbw);
                                String zzd = dataHolder2.zzd(string2, i3, zzbw);
                                int zzc = dataHolder2.zzc(string3, i3, zzbw);
                                zzc zzc2 = new zzc(new CustomPropertyKey(zzd, zzc), dataHolder2.zzd(string4, i3, zzbw));
                                zza = (zza) longSparseArray.get(zzb);
                                if (zza == null) {
                                    zza = new zza();
                                    longSparseArray.put(zzb, zza);
                                }
                                zza.zza(zzc2);
                            }
                            SparseArray sparseArray = new SparseArray();
                            for (i3 = 0; i3 < dataHolder.getCount(); i3++) {
                                DataHolder dataHolder3 = dataHolder;
                                zza = (zza) longSparseArray.get(dataHolder3.zzb("sqlId", i3, dataHolder.zzbw(i3)));
                                if (zza != null) {
                                    sparseArray.append(i3, zza.zzanq());
                                }
                            }
                            dataHolder.zzafh().putSparseParcelableArray("customPropertiesExtra", sparseArray);
                        } finally {
                            dataHolder2.close();
                            dataHolder.zzafh().remove("customPropertiesExtraHolder");
                        }
                    }
                }
                sparseParcelableArray = zzafh.getSparseParcelableArray("customPropertiesExtra");
            }
            if (sparseParcelableArray == null) {
                return AppVisibleCustomProperties.zzgkj;
            }
        }
        return (AppVisibleCustomProperties) sparseParcelableArray.get(i, AppVisibleCustomProperties.zzgkj);
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        return zzf(dataHolder, i, i2);
    }
}
