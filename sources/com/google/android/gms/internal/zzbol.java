package com.google.android.gms.internal;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.DriveFolder;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.internal.zzm;
import java.util.Arrays;

public final class zzbol extends zzm<DriveId> {
    public static final zzbol zzgmr = new zzbol();

    private zzbol() {
        super("driveId", Arrays.asList(new String[]{"sqlId", "resourceId", "mimeType"}), Arrays.asList(new String[]{"dbInstanceId"}), 4100000);
    }

    protected final boolean zzb(DataHolder dataHolder, int i, int i2) {
        for (String zzft : zzano()) {
            if (!dataHolder.zzft(zzft)) {
                return false;
            }
        }
        return true;
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        long j = dataHolder.zzafh().getLong("dbInstanceId");
        int i3 = DriveFolder.MIME_TYPE.equals(dataHolder.zzd(zzbnr.zzglq.getName(), i, i2)) ? 1 : 0;
        String zzd = dataHolder.zzd("resourceId", i, i2);
        long zzb = dataHolder.zzb("sqlId", i, i2);
        if ("generated-android-null".equals(zzd)) {
            zzd = null;
        }
        return new DriveId(zzd, Long.valueOf(zzb).longValue(), j, i3);
    }
}
