package com.google.android.gms.common.data;

import android.content.ContentValues;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.data.DataHolder.zza;
import com.google.android.gms.common.internal.safeparcel.SafeParcelable;

public class zzd<T extends SafeParcelable> extends AbstractDataBuffer<T> {
    private static final String[] zzfqd = new String[]{ShareConstants.WEB_DIALOG_PARAM_DATA};
    private final Creator<T> zzfqe;

    public zzd(DataHolder dataHolder, Creator<T> creator) {
        super(dataHolder);
        this.zzfqe = creator;
    }

    public static <T extends SafeParcelable> void zza(zza zza, T t) {
        Parcel obtain = Parcel.obtain();
        t.writeToParcel(obtain, 0);
        ContentValues contentValues = new ContentValues();
        contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, obtain.marshall());
        zza.zza(contentValues);
        obtain.recycle();
    }

    public static zza zzaiu() {
        return DataHolder.zza(zzfqd);
    }

    public /* synthetic */ Object get(int i) {
        return zzbv(i);
    }

    public T zzbv(int i) {
        byte[] zzg = this.zzfkz.zzg(ShareConstants.WEB_DIALOG_PARAM_DATA, i, this.zzfkz.zzbw(i));
        Parcel obtain = Parcel.obtain();
        obtain.unmarshall(zzg, 0, zzg.length);
        obtain.setDataPosition(0);
        SafeParcelable safeParcelable = (SafeParcelable) this.zzfqe.createFromParcel(obtain);
        obtain.recycle();
        return safeParcelable;
    }
}
