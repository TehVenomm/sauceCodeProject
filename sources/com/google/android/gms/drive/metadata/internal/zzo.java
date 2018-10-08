package com.google.android.gms.drive.metadata.internal;

import android.os.Bundle;
import com.google.android.gms.common.data.DataHolder;
import com.google.android.gms.drive.DriveId;
import com.google.android.gms.drive.metadata.SearchableCollectionMetadataField;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;

public final class zzo extends zzl<DriveId> implements SearchableCollectionMetadataField<DriveId> {
    public static final zzg zzgks = new zzp();

    public zzo(int i) {
        super("parents", Collections.emptySet(), Arrays.asList(new String[]{"parentsExtra", "dbInstanceId", "parentsExtraHolder"}), 4100000);
    }

    private static void zzd(DataHolder dataHolder) {
        Bundle zzafh = dataHolder.zzafh();
        if (zzafh != null) {
            synchronized (dataHolder) {
                DataHolder dataHolder2 = (DataHolder) zzafh.getParcelable("parentsExtraHolder");
                if (dataHolder2 != null) {
                    dataHolder2.close();
                    zzafh.remove("parentsExtraHolder");
                }
            }
        }
    }

    protected final /* synthetic */ Object zzc(DataHolder dataHolder, int i, int i2) {
        return zzd(dataHolder, i, i2);
    }

    protected final Collection<DriveId> zzd(DataHolder dataHolder, int i, int i2) {
        Bundle zzafh = dataHolder.zzafh();
        List parcelableArrayList = zzafh.getParcelableArrayList("parentsExtra");
        if (parcelableArrayList == null) {
            if (zzafh.getParcelable("parentsExtraHolder") != null) {
                synchronized (dataHolder) {
                    DataHolder dataHolder2 = (DataHolder) dataHolder.zzafh().getParcelable("parentsExtraHolder");
                    if (dataHolder2 == null) {
                    } else {
                        try {
                            int count = dataHolder.getCount();
                            ArrayList arrayList = new ArrayList(count);
                            Map hashMap = new HashMap(count);
                            for (int i3 = 0; i3 < count; i3++) {
                                int zzbw = dataHolder.zzbw(i3);
                                ParentDriveIdSet parentDriveIdSet = new ParentDriveIdSet();
                                arrayList.add(parentDriveIdSet);
                                hashMap.put(Long.valueOf(dataHolder.zzb("sqlId", i3, zzbw)), parentDriveIdSet);
                            }
                            Bundle zzafh2 = dataHolder2.zzafh();
                            String string = zzafh2.getString("childSqlIdColumn");
                            String string2 = zzafh2.getString("parentSqlIdColumn");
                            String string3 = zzafh2.getString("parentResIdColumn");
                            int count2 = dataHolder2.getCount();
                            for (count = 0; count < count2; count++) {
                                int zzbw2 = dataHolder2.zzbw(count);
                                ParentDriveIdSet parentDriveIdSet2 = (ParentDriveIdSet) hashMap.get(Long.valueOf(dataHolder2.zzb(string, count, zzbw2)));
                                parentDriveIdSet2.zzgkr.add(new zzq(dataHolder2.zzd(string3, count, zzbw2), dataHolder2.zzb(string2, count, zzbw2), 1));
                            }
                            dataHolder.zzafh().putParcelableArrayList("parentsExtra", arrayList);
                        } finally {
                            dataHolder2.close();
                            dataHolder.zzafh().remove("parentsExtraHolder");
                        }
                    }
                }
                parcelableArrayList = zzafh.getParcelableArrayList("parentsExtra");
            }
            if (parcelableArrayList == null) {
                return null;
            }
        }
        return ((ParentDriveIdSet) parcelableArrayList.get(i)).zzab(zzafh.getLong("dbInstanceId"));
    }

    protected final /* synthetic */ Object zzm(Bundle bundle) {
        return zzn(bundle);
    }

    protected final Collection<DriveId> zzn(Bundle bundle) {
        Collection zzn = super.zzn(bundle);
        return zzn == null ? null : new HashSet(zzn);
    }
}
