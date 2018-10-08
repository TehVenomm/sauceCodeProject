package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteDatabaseLockedException;
import android.database.sqlite.SQLiteException;
import android.database.sqlite.SQLiteFullException;
import android.os.Build.VERSION;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.SystemClock;
import android.support.annotation.WorkerThread;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.internal.safeparcel.zza;
import com.google.android.gms.common.internal.safeparcel.zzc;
import com.google.android.gms.common.util.zzd;
import java.util.ArrayList;
import java.util.List;

public final class zzcbk extends zzcdm {
    private final zzcbl zzipc = new zzcbl(this, getContext(), zzcap.zzawi());
    private boolean zzipd;

    zzcbk(zzcco zzcco) {
        super(zzcco);
    }

    @WorkerThread
    private final SQLiteDatabase getWritableDatabase() {
        if (this.zzipd) {
            return null;
        }
        SQLiteDatabase writableDatabase = this.zzipc.getWritableDatabase();
        if (writableDatabase != null) {
            return writableDatabase;
        }
        this.zzipd = true;
        return null;
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    @android.support.annotation.WorkerThread
    @android.annotation.TargetApi(11)
    private final boolean zzb(int r14, byte[] r15) {
        /*
        r13 = this;
        r13.zzatu();
        r13.zzug();
        r0 = r13.zzipd;
        if (r0 == 0) goto L_0x000c;
    L_0x000a:
        r0 = 0;
    L_0x000b:
        return r0;
    L_0x000c:
        r10 = new android.content.ContentValues;
        r10.<init>();
        r0 = "type";
        r1 = java.lang.Integer.valueOf(r14);
        r10.put(r0, r1);
        r0 = "entry";
        r10.put(r0, r15);
        com.google.android.gms.internal.zzcap.zzaws();
        r0 = 0;
        r1 = 5;
        r7 = r0;
        r0 = r1;
    L_0x0026:
        r1 = 5;
        if (r7 >= r1) goto L_0x0124;
    L_0x0029:
        r4 = 0;
        r1 = 0;
        r2 = 0;
        r5 = 0;
        r6 = 0;
        r3 = 0;
        r2 = r13.getWritableDatabase();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x00de, all -> 0x0134 }
        if (r2 != 0) goto L_0x003f;
    L_0x0035:
        r1 = 1;
        r13.zzipd = r1;	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013b, all -> 0x0138 }
        if (r2 == 0) goto L_0x003d;
    L_0x003a:
        r2.close();
    L_0x003d:
        r0 = 0;
        goto L_0x000b;
    L_0x003f:
        r2.beginTransaction();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013b, all -> 0x0138 }
        r8 = 0;
        r1 = "select count(1) from messages";
        r4 = 0;
        r3 = r2.rawQuery(r1, r4);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013b, all -> 0x0138 }
        if (r3 == 0) goto L_0x0140;
    L_0x004d:
        r1 = r3.moveToFirst();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        if (r1 == 0) goto L_0x0140;
    L_0x0053:
        r1 = 0;
        r4 = r3.getLong(r1);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
    L_0x0058:
        r8 = 100000; // 0x186a0 float:1.4013E-40 double:4.94066E-319;
        r1 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1));
        if (r1 < 0) goto L_0x00a5;
    L_0x005f:
        r1 = r13.zzauk();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r1 = r1.zzayc();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r6 = "Data loss, local db full";
        r1.log(r6);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r8 = 100000; // 0x186a0 float:1.4013E-40 double:4.94066E-319;
        r4 = r8 - r4;
        r8 = 1;
        r4 = r4 + r8;
        r1 = "messages";
        r6 = "rowid in (select rowid from messages order by rowid asc limit ?)";
        r8 = 1;
        r8 = new java.lang.String[r8];	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r9 = 0;
        r11 = java.lang.Long.toString(r4);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r8[r9] = r11;	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r1 = r2.delete(r1, r6, r8);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r8 = (long) r1;	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r1 = (r8 > r4 ? 1 : (r8 == r4 ? 0 : -1));
        if (r1 == 0) goto L_0x00a5;
    L_0x008b:
        r1 = r13.zzauk();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r1 = r1.zzayc();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r6 = "Different delete count than expected in local db. expected, received, difference";
        r11 = java.lang.Long.valueOf(r4);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r12 = java.lang.Long.valueOf(r8);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r4 = r4 - r8;
        r4 = java.lang.Long.valueOf(r4);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r1.zzd(r6, r11, r12, r4);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
    L_0x00a5:
        r1 = "messages";
        r4 = 0;
        r2.insertOrThrow(r1, r4, r10);	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r2.setTransactionSuccessful();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        r2.endTransaction();	 Catch:{ SQLiteFullException -> 0x00be, SQLiteException -> 0x013e }
        if (r3 == 0) goto L_0x00b6;
    L_0x00b3:
        r3.close();
    L_0x00b6:
        if (r2 == 0) goto L_0x00bb;
    L_0x00b8:
        r2.close();
    L_0x00bb:
        r0 = 1;
        goto L_0x000b;
    L_0x00be:
        r1 = move-exception;
        r4 = r13.zzauk();	 Catch:{ all -> 0x0118 }
        r4 = r4.zzayc();	 Catch:{ all -> 0x0118 }
        r5 = "Error writing entry to local database";
        r4.zzj(r5, r1);	 Catch:{ all -> 0x0118 }
        r1 = 1;
        r13.zzipd = r1;	 Catch:{ all -> 0x0118 }
        if (r3 == 0) goto L_0x00d4;
    L_0x00d1:
        r3.close();
    L_0x00d4:
        if (r2 == 0) goto L_0x00d9;
    L_0x00d6:
        r2.close();
    L_0x00d9:
        r1 = r7 + 1;
        r7 = r1;
        goto L_0x0026;
    L_0x00de:
        r1 = move-exception;
        r2 = r4;
        r3 = r5;
    L_0x00e1:
        r4 = android.os.Build.VERSION.SDK_INT;	 Catch:{ all -> 0x0118 }
        r5 = 11;
        if (r4 < r5) goto L_0x00fc;
    L_0x00e7:
        r4 = r1 instanceof android.database.sqlite.SQLiteDatabaseLockedException;	 Catch:{ all -> 0x0118 }
        if (r4 == 0) goto L_0x00fc;
    L_0x00eb:
        r4 = (long) r0;	 Catch:{ all -> 0x0118 }
        android.os.SystemClock.sleep(r4);	 Catch:{ all -> 0x0118 }
        r0 = r0 + 20;
    L_0x00f1:
        if (r3 == 0) goto L_0x00f6;
    L_0x00f3:
        r3.close();
    L_0x00f6:
        if (r2 == 0) goto L_0x00d9;
    L_0x00f8:
        r2.close();
        goto L_0x00d9;
    L_0x00fc:
        if (r2 == 0) goto L_0x0107;
    L_0x00fe:
        r4 = r2.inTransaction();	 Catch:{ all -> 0x0118 }
        if (r4 == 0) goto L_0x0107;
    L_0x0104:
        r2.endTransaction();	 Catch:{ all -> 0x0118 }
    L_0x0107:
        r4 = r13.zzauk();	 Catch:{ all -> 0x0118 }
        r4 = r4.zzayc();	 Catch:{ all -> 0x0118 }
        r5 = "Error writing entry to local database";
        r4.zzj(r5, r1);	 Catch:{ all -> 0x0118 }
        r1 = 1;
        r13.zzipd = r1;	 Catch:{ all -> 0x0118 }
        goto L_0x00f1;
    L_0x0118:
        r0 = move-exception;
    L_0x0119:
        if (r3 == 0) goto L_0x011e;
    L_0x011b:
        r3.close();
    L_0x011e:
        if (r2 == 0) goto L_0x0123;
    L_0x0120:
        r2.close();
    L_0x0123:
        throw r0;
    L_0x0124:
        r0 = r13.zzauk();
        r0 = r0.zzaye();
        r1 = "Failed to write entry to local database";
        r0.log(r1);
        r0 = 0;
        goto L_0x000b;
    L_0x0134:
        r0 = move-exception;
        r2 = r1;
        r3 = r6;
        goto L_0x0119;
    L_0x0138:
        r0 = move-exception;
        r3 = r6;
        goto L_0x0119;
    L_0x013b:
        r1 = move-exception;
        r3 = r5;
        goto L_0x00e1;
    L_0x013e:
        r1 = move-exception;
        goto L_0x00e1;
    L_0x0140:
        r4 = r8;
        goto L_0x0058;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcbk.zzb(int, byte[]):boolean");
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final boolean zza(zzcbc zzcbc) {
        Parcel obtain = Parcel.obtain();
        zzcbc.writeToParcel(obtain, 0);
        byte[] marshall = obtain.marshall();
        obtain.recycle();
        if (marshall.length <= 131072) {
            return zzb(0, marshall);
        }
        zzauk().zzaye().log("Event is too long for local database. Sending event directly to service");
        return false;
    }

    public final boolean zza(zzcfl zzcfl) {
        Parcel obtain = Parcel.obtain();
        zzcfl.writeToParcel(obtain, 0);
        byte[] marshall = obtain.marshall();
        obtain.recycle();
        if (marshall.length <= 131072) {
            return zzb(1, marshall);
        }
        zzauk().zzaye().log("User property too long for local database. Sending directly to service");
        return false;
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    public final boolean zzc(zzcan zzcan) {
        zzaug();
        byte[] zza = zzcfo.zza((Parcelable) zzcan);
        if (zza.length <= 131072) {
            return zzb(2, zza);
        }
        zzauk().zzaye().log("Conditional user property too long for local database. Sending directly to service");
        return false;
    }

    @TargetApi(11)
    public final List<zza> zzdv(int i) {
        Cursor cursor;
        SQLiteDatabase sQLiteDatabase;
        Cursor cursor2;
        SQLiteDatabase sQLiteDatabase2;
        SQLiteException sQLiteException;
        SQLiteDatabase sQLiteDatabase3;
        Object obj;
        int i2;
        Throwable th;
        Parcel obtain;
        zzug();
        zzatu();
        if (this.zzipd) {
            return null;
        }
        List<zza> arrayList = new ArrayList();
        if (!getContext().getDatabasePath(zzcap.zzawi()).exists()) {
            return arrayList;
        }
        int i3 = 5;
        int i4 = 0;
        while (i4 < 5) {
            SQLiteDatabase sQLiteDatabase4 = null;
            Object e;
            try {
                sQLiteDatabase4 = getWritableDatabase();
                if (sQLiteDatabase4 == null) {
                    try {
                        this.zzipd = true;
                        if (sQLiteDatabase4 != null) {
                            sQLiteDatabase4.close();
                        }
                        return null;
                    } catch (SQLiteFullException e2) {
                        e = e2;
                        cursor = null;
                        sQLiteDatabase = sQLiteDatabase4;
                    } catch (SQLiteException e3) {
                        cursor2 = null;
                        sQLiteDatabase2 = sQLiteDatabase4;
                        sQLiteException = e3;
                        sQLiteDatabase3 = sQLiteDatabase2;
                        try {
                            if (VERSION.SDK_INT >= 11) {
                            }
                            if (sQLiteDatabase3 != null) {
                                if (sQLiteDatabase3.inTransaction()) {
                                    sQLiteDatabase3.endTransaction();
                                }
                            }
                            zzauk().zzayc().zzj("Error reading entries from local database", obj);
                            this.zzipd = true;
                            i2 = i3;
                            if (cursor2 != null) {
                                cursor2.close();
                            }
                            if (sQLiteDatabase3 != null) {
                                sQLiteDatabase3.close();
                            }
                            i4++;
                            i3 = i2;
                        } catch (Throwable th2) {
                            sQLiteDatabase2 = sQLiteDatabase3;
                            th = th2;
                            sQLiteDatabase4 = sQLiteDatabase2;
                        }
                    } catch (Throwable th3) {
                        th = th3;
                        cursor2 = null;
                    }
                } else {
                    sQLiteDatabase4.beginTransaction();
                    String[] strArr = new String[]{"rowid", ShareConstants.MEDIA_TYPE, "entry"};
                    cursor2 = sQLiteDatabase4.query("messages", strArr, null, null, null, null, "rowid asc", Integer.toString(100));
                    long j = -1;
                    while (cursor2.moveToNext()) {
                        try {
                            j = cursor2.getLong(0);
                            int i5 = cursor2.getInt(1);
                            byte[] blob = cursor2.getBlob(2);
                            if (i5 == 0) {
                                obtain = Parcel.obtain();
                                try {
                                    obtain.unmarshall(blob, 0, blob.length);
                                    obtain.setDataPosition(0);
                                    zzcbc zzcbc = (zzcbc) zzcbc.CREATOR.createFromParcel(obtain);
                                    obtain.recycle();
                                    if (zzcbc != null) {
                                        arrayList.add(zzcbc);
                                    }
                                } catch (zzc e4) {
                                    zzauk().zzayc().log("Failed to load event from local database");
                                    obtain.recycle();
                                } catch (Throwable th4) {
                                    obtain.recycle();
                                    throw th4;
                                }
                            } else if (i5 == 1) {
                                obtain = Parcel.obtain();
                                try {
                                    obtain.unmarshall(blob, 0, blob.length);
                                    obtain.setDataPosition(0);
                                    e = (zzcfl) zzcfl.CREATOR.createFromParcel(obtain);
                                    obtain.recycle();
                                } catch (zzc e5) {
                                    zzauk().zzayc().log("Failed to load user property from local database");
                                    obtain.recycle();
                                    e = null;
                                } catch (Throwable th42) {
                                    obtain.recycle();
                                    throw th42;
                                }
                                if (e != null) {
                                    arrayList.add(e);
                                }
                            } else if (i5 == 2) {
                                obtain = Parcel.obtain();
                                try {
                                    obtain.unmarshall(blob, 0, blob.length);
                                    obtain.setDataPosition(0);
                                    e = (zzcan) zzcan.CREATOR.createFromParcel(obtain);
                                    obtain.recycle();
                                } catch (zzc e6) {
                                    zzauk().zzayc().log("Failed to load user property from local database");
                                    obtain.recycle();
                                    e = null;
                                } catch (Throwable th422) {
                                    obtain.recycle();
                                    throw th422;
                                }
                                if (e != null) {
                                    arrayList.add(e);
                                }
                            } else {
                                zzauk().zzayc().log("Unknown record type in local database");
                            }
                        } catch (SQLiteFullException e7) {
                            e = e7;
                            cursor = cursor2;
                            sQLiteDatabase = sQLiteDatabase4;
                        } catch (SQLiteException e32) {
                            sQLiteDatabase2 = sQLiteDatabase4;
                            obj = e32;
                            sQLiteDatabase3 = sQLiteDatabase2;
                        } catch (Throwable th5) {
                            th422 = th5;
                        }
                    }
                    if (sQLiteDatabase4.delete("messages", "rowid <= ?", new String[]{Long.toString(j)}) < arrayList.size()) {
                        zzauk().zzayc().log("Fewer entries removed from local database than expected");
                    }
                    sQLiteDatabase4.setTransactionSuccessful();
                    sQLiteDatabase4.endTransaction();
                    if (cursor2 != null) {
                        cursor2.close();
                    }
                    if (sQLiteDatabase4 != null) {
                        sQLiteDatabase4.close();
                    }
                    return arrayList;
                }
            } catch (SQLiteFullException e8) {
                e = e8;
                cursor = null;
                sQLiteDatabase = null;
                try {
                    zzauk().zzayc().zzj("Error reading entries from local database", e);
                    this.zzipd = true;
                    if (cursor != null) {
                        cursor.close();
                    }
                    if (sQLiteDatabase != null) {
                        sQLiteDatabase.close();
                        i2 = i3;
                    } else {
                        i2 = i3;
                    }
                    i4++;
                    i3 = i2;
                } catch (Throwable th22) {
                    th422 = th22;
                    sQLiteDatabase4 = sQLiteDatabase;
                    cursor2 = cursor;
                }
            } catch (SQLiteException e322) {
                cursor2 = null;
                sQLiteDatabase2 = sQLiteDatabase4;
                sQLiteException = e322;
                sQLiteDatabase3 = sQLiteDatabase2;
                if (VERSION.SDK_INT >= 11 || !(obj instanceof SQLiteDatabaseLockedException)) {
                    if (sQLiteDatabase3 != null) {
                        if (sQLiteDatabase3.inTransaction()) {
                            sQLiteDatabase3.endTransaction();
                        }
                    }
                    zzauk().zzayc().zzj("Error reading entries from local database", obj);
                    this.zzipd = true;
                    i2 = i3;
                } else {
                    SystemClock.sleep((long) i3);
                    i2 = i3 + 20;
                }
                if (cursor2 != null) {
                    cursor2.close();
                }
                if (sQLiteDatabase3 != null) {
                    sQLiteDatabase3.close();
                }
                i4++;
                i3 = i2;
            } catch (Throwable th222) {
                cursor2 = null;
                th422 = th222;
                sQLiteDatabase4 = null;
            }
        }
        zzauk().zzaye().log("Failed to read events from database in reasonable time");
        return null;
        if (cursor2 != null) {
            cursor2.close();
        }
        if (sQLiteDatabase4 != null) {
            sQLiteDatabase4.close();
        }
        throw th422;
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }
}
