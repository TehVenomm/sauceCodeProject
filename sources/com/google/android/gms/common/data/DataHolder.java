package com.google.android.gms.common.data;

import android.content.ContentValues;
import android.database.CharArrayBuffer;
import android.database.CursorIndexOutOfBoundsException;
import android.database.CursorWindow;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable.Creator;
import android.util.Log;
import com.google.android.gms.common.annotation.KeepName;
import com.google.android.gms.common.internal.safeparcel.zzd;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.internal.zzc;
import java.io.Closeable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;

@KeepName
public final class DataHolder extends com.google.android.gms.common.internal.safeparcel.zza implements Closeable {
    public static final Creator<DataHolder> CREATOR = new zzf();
    private static final zza zzfqm = new zze(new String[0], null);
    private boolean mClosed;
    private int zzdxt;
    private final int zzezx;
    private final String[] zzfqf;
    private Bundle zzfqg;
    private final CursorWindow[] zzfqh;
    private final Bundle zzfqi;
    private int[] zzfqj;
    int zzfqk;
    private boolean zzfql;

    public static class zza {
        private final String[] zzfqf;
        private final ArrayList<HashMap<String, Object>> zzfqn;
        private final String zzfqo;
        private final HashMap<Object, Integer> zzfqp;
        private boolean zzfqq;
        private String zzfqr;

        private zza(String[] strArr, String str) {
            this.zzfqf = (String[]) zzbp.zzu(strArr);
            this.zzfqn = new ArrayList();
            this.zzfqo = str;
            this.zzfqp = new HashMap();
            this.zzfqq = false;
            this.zzfqr = null;
        }

        public zza zza(ContentValues contentValues) {
            zzc.zzr(contentValues);
            HashMap hashMap = new HashMap(contentValues.size());
            for (Entry entry : contentValues.valueSet()) {
                hashMap.put((String) entry.getKey(), entry.getValue());
            }
            return zza(hashMap);
        }

        public zza zza(HashMap<String, Object> hashMap) {
            int i;
            zzc.zzr(hashMap);
            if (this.zzfqo == null) {
                i = -1;
            } else {
                Object obj = hashMap.get(this.zzfqo);
                if (obj == null) {
                    i = -1;
                } else {
                    Integer num = (Integer) this.zzfqp.get(obj);
                    if (num == null) {
                        this.zzfqp.put(obj, Integer.valueOf(this.zzfqn.size()));
                        i = -1;
                    } else {
                        i = num.intValue();
                    }
                }
            }
            if (i == -1) {
                this.zzfqn.add(hashMap);
            } else {
                this.zzfqn.remove(i);
                this.zzfqn.add(i, hashMap);
            }
            this.zzfqq = false;
            return this;
        }

        public final DataHolder zzby(int i) {
            return new DataHolder(this);
        }
    }

    public static final class zzb extends RuntimeException {
        public zzb(String str) {
            super(str);
        }
    }

    DataHolder(int i, String[] strArr, CursorWindow[] cursorWindowArr, int i2, Bundle bundle) {
        this.mClosed = false;
        this.zzfql = true;
        this.zzdxt = i;
        this.zzfqf = strArr;
        this.zzfqh = cursorWindowArr;
        this.zzezx = i2;
        this.zzfqi = bundle;
    }

    private DataHolder(zza zza, int i, Bundle bundle) {
        this(zza.zzfqf, zza(zza, -1), i, null);
    }

    private DataHolder(String[] strArr, CursorWindow[] cursorWindowArr, int i, Bundle bundle) {
        this.mClosed = false;
        this.zzfql = true;
        this.zzdxt = 1;
        this.zzfqf = (String[]) zzbp.zzu(strArr);
        this.zzfqh = (CursorWindow[]) zzbp.zzu(cursorWindowArr);
        this.zzezx = i;
        this.zzfqi = bundle;
        zzaiv();
    }

    public static zza zza(String[] strArr) {
        return new zza(strArr);
    }

    private static CursorWindow[] zza(zza zza, int i) {
        int i2;
        int i3 = 0;
        if (zza.zzfqf.length == 0) {
            return new CursorWindow[0];
        }
        List zzb = zza.zzfqn;
        int size = zzb.size();
        CursorWindow cursorWindow = new CursorWindow(false);
        ArrayList arrayList = new ArrayList();
        arrayList.add(cursorWindow);
        cursorWindow.setNumColumns(zza.zzfqf.length);
        int i4 = 0;
        int i5 = 0;
        while (i5 < size) {
            try {
                CursorWindow cursorWindow2;
                int i6;
                if (!cursorWindow.allocRow()) {
                    Log.d("DataHolder", "Allocating additional cursor window for large data set (row " + i5 + ")");
                    cursorWindow = new CursorWindow(false);
                    cursorWindow.setStartPosition(i5);
                    cursorWindow.setNumColumns(zza.zzfqf.length);
                    arrayList.add(cursorWindow);
                    if (!cursorWindow.allocRow()) {
                        Log.e("DataHolder", "Unable to allocate row to hold data.");
                        arrayList.remove(cursorWindow);
                        return (CursorWindow[]) arrayList.toArray(new CursorWindow[arrayList.size()]);
                    }
                }
                Map map = (Map) zzb.get(i5);
                boolean z = true;
                for (int i7 = 0; i7 < zza.zzfqf.length && z; i7++) {
                    String str = zza.zzfqf[i7];
                    Object obj = map.get(str);
                    if (obj == null) {
                        z = cursorWindow.putNull(i5, i7);
                    } else if (obj instanceof String) {
                        z = cursorWindow.putString((String) obj, i5, i7);
                    } else if (obj instanceof Long) {
                        z = cursorWindow.putLong(((Long) obj).longValue(), i5, i7);
                    } else if (obj instanceof Integer) {
                        z = cursorWindow.putLong((long) ((Integer) obj).intValue(), i5, i7);
                    } else if (obj instanceof Boolean) {
                        z = cursorWindow.putLong(((Boolean) obj).booleanValue() ? 1 : 0, i5, i7);
                    } else if (obj instanceof byte[]) {
                        z = cursorWindow.putBlob((byte[]) obj, i5, i7);
                    } else if (obj instanceof Double) {
                        z = cursorWindow.putDouble(((Double) obj).doubleValue(), i5, i7);
                    } else if (obj instanceof Float) {
                        z = cursorWindow.putDouble((double) ((Float) obj).floatValue(), i5, i7);
                    } else {
                        String valueOf = String.valueOf(obj);
                        throw new IllegalArgumentException(new StringBuilder((String.valueOf(str).length() + 32) + String.valueOf(valueOf).length()).append("Unsupported object for column ").append(str).append(": ").append(valueOf).toString());
                    }
                }
                if (z) {
                    cursorWindow2 = cursorWindow;
                    i6 = i5;
                    i2 = 0;
                } else if (i4 != 0) {
                    throw new zzb("Could not add the value to a new CursorWindow. The size of value may be larger than what a CursorWindow can handle.");
                } else {
                    Log.d("DataHolder", "Couldn't populate window data for row " + i5 + " - allocating new window.");
                    cursorWindow.freeLastRow();
                    cursorWindow2 = new CursorWindow(false);
                    cursorWindow2.setStartPosition(i5);
                    cursorWindow2.setNumColumns(zza.zzfqf.length);
                    arrayList.add(cursorWindow2);
                    i6 = i5 - 1;
                    i2 = 1;
                }
                i5 = i6 + 1;
                i4 = i2;
                cursorWindow = cursorWindow2;
            } catch (RuntimeException e) {
                RuntimeException runtimeException = e;
                i2 = arrayList.size();
                while (i3 < i2) {
                    ((CursorWindow) arrayList.get(i3)).close();
                    i3++;
                }
                throw runtimeException;
            }
        }
        return (CursorWindow[]) arrayList.toArray(new CursorWindow[arrayList.size()]);
    }

    public static DataHolder zzbx(int i) {
        return new DataHolder(zzfqm, i, null);
    }

    private final void zzh(String str, int i) {
        if (this.zzfqg == null || !this.zzfqg.containsKey(str)) {
            String valueOf = String.valueOf(str);
            throw new IllegalArgumentException(valueOf.length() != 0 ? "No such column: ".concat(valueOf) : new String("No such column: "));
        } else if (isClosed()) {
            throw new IllegalArgumentException("Buffer is closed.");
        } else if (i < 0 || i >= this.zzfqk) {
            throw new CursorIndexOutOfBoundsException(i, this.zzfqk);
        }
    }

    public final void close() {
        synchronized (this) {
            if (!this.mClosed) {
                this.mClosed = true;
                for (CursorWindow close : this.zzfqh) {
                    close.close();
                }
            }
        }
    }

    protected final void finalize() throws Throwable {
        try {
            if (this.zzfql && this.zzfqh.length > 0 && !isClosed()) {
                close();
                String obj = toString();
                Log.e("DataBuffer", new StringBuilder(String.valueOf(obj).length() + 178).append("Internal data leak within a DataBuffer object detected!  Be sure to explicitly call release() on all DataBuffer extending objects when you are done with them. (internal object: ").append(obj).append(")").toString());
            }
            super.finalize();
        } catch (Throwable th) {
            super.finalize();
        }
    }

    public final int getCount() {
        return this.zzfqk;
    }

    public final int getStatusCode() {
        return this.zzezx;
    }

    public final boolean isClosed() {
        boolean z;
        synchronized (this) {
            z = this.mClosed;
        }
        return z;
    }

    public final void writeToParcel(Parcel parcel, int i) {
        int zze = zzd.zze(parcel);
        zzd.zza(parcel, 1, this.zzfqf, false);
        zzd.zza(parcel, 2, this.zzfqh, i, false);
        zzd.zzc(parcel, 3, this.zzezx);
        zzd.zza(parcel, 4, this.zzfqi, false);
        zzd.zzc(parcel, 1000, this.zzdxt);
        zzd.zzai(parcel, zze);
        if ((i & 1) != 0) {
            close();
        }
    }

    public final void zza(String str, int i, int i2, CharArrayBuffer charArrayBuffer) {
        zzh(str, i);
        this.zzfqh[i2].copyStringToBuffer(i, this.zzfqg.getInt(str), charArrayBuffer);
    }

    public final Bundle zzafh() {
        return this.zzfqi;
    }

    public final void zzaiv() {
        int i;
        int i2 = 0;
        this.zzfqg = new Bundle();
        for (i = 0; i < this.zzfqf.length; i++) {
            this.zzfqg.putInt(this.zzfqf[i], i);
        }
        this.zzfqj = new int[this.zzfqh.length];
        i = 0;
        while (i2 < this.zzfqh.length) {
            this.zzfqj[i2] = i;
            i += this.zzfqh[i2].getNumRows() - (i - this.zzfqh[i2].getStartPosition());
            i2++;
        }
        this.zzfqk = i;
    }

    public final long zzb(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].getLong(i, this.zzfqg.getInt(str));
    }

    public final int zzbw(int i) {
        int i2 = 0;
        boolean z = i >= 0 && i < this.zzfqk;
        zzbp.zzbg(z);
        while (i2 < this.zzfqj.length) {
            if (i < this.zzfqj[i2]) {
                i2--;
                break;
            }
            i2++;
        }
        return i2 == this.zzfqj.length ? i2 - 1 : i2;
    }

    public final int zzc(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].getInt(i, this.zzfqg.getInt(str));
    }

    public final String zzd(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].getString(i, this.zzfqg.getInt(str));
    }

    public final boolean zze(String str, int i, int i2) {
        zzh(str, i);
        return Long.valueOf(this.zzfqh[i2].getLong(i, this.zzfqg.getInt(str))).longValue() == 1;
    }

    public final float zzf(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].getFloat(i, this.zzfqg.getInt(str));
    }

    public final boolean zzft(String str) {
        return this.zzfqg.containsKey(str);
    }

    public final byte[] zzg(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].getBlob(i, this.zzfqg.getInt(str));
    }

    public final boolean zzh(String str, int i, int i2) {
        zzh(str, i);
        return this.zzfqh[i2].isNull(i, this.zzfqg.getInt(str));
    }
}
