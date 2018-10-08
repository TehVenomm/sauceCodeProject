package com.google.android.gms.internal;

import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.DriveSpace;
import com.google.android.gms.drive.metadata.internal.zzl;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;

public final class zzbny extends zzl<DriveSpace> {
    public zzbny(int i) {
        super("spaces", Arrays.asList(new String[]{"inDriveSpace", "isAppData", "inGooglePhotosSpace"}), Collections.emptySet(), 7000000);
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        return zzd(dataHolder, i, i2);
    }

    protected final Collection<DriveSpace> zzd(DataHolder dataHolder, int i, int i2) {
        Collection arrayList = new ArrayList();
        if (dataHolder.zze("inDriveSpace", i, i2)) {
            arrayList.add(DriveSpace.zzgdn);
        }
        if (dataHolder.zze("isAppData", i, i2)) {
            arrayList.add(DriveSpace.zzgdo);
        }
        if (dataHolder.zze("inGooglePhotosSpace", i, i2)) {
            arrayList.add(DriveSpace.zzgdp);
        }
        return arrayList;
    }
}
