package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.WorkerThread;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;

public final class zzeb extends zzg {
    private final zzea zzjv = new zzea(this, getContext(), "google_app_measurement_local.db");
    private boolean zzjw;

    zzeb(zzfj zzfj) {
        super(zzfj);
    }

    @WorkerThread
    @VisibleForTesting
    private final SQLiteDatabase getWritableDatabase() throws SQLiteException {
        if (this.zzjw) {
            return null;
        }
        SQLiteDatabase writableDatabase = this.zzjv.getWritableDatabase();
        if (writableDatabase != null) {
            return writableDatabase;
        }
        this.zzjw = true;
        return null;
    }

    private static long zza(SQLiteDatabase sQLiteDatabase) {
        Cursor cursor;
        try {
            SQLiteDatabase sQLiteDatabase2 = sQLiteDatabase;
            Cursor query = sQLiteDatabase2.query("messages", new String[]{"rowid"}, "type=?", new String[]{"3"}, null, null, "rowid desc", AppEventsConstants.EVENT_PARAM_VALUE_YES);
            try {
                if (query.moveToFirst()) {
                    long j = query.getLong(0);
                    if (query == null) {
                        return j;
                    }
                    query.close();
                    return j;
                }
                if (query != null) {
                    query.close();
                }
                return -1;
            } catch (Throwable th) {
                th = th;
                cursor = query;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        } catch (Throwable th2) {
            th = th2;
            cursor = null;
        }
    }

    /* JADX WARNING: Code restructure failed: missing block: B:34:0x00bc, code lost:
        r1 = move-exception;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:36:?, code lost:
        zzab().zzgk().zza("Error writing entry to local database", r1);
        r13.zzjw = true;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:37:0x00cd, code lost:
        if (r3 != null) goto L_0x00cf;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:38:0x00cf, code lost:
        r3.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:0x00d2, code lost:
        if (r2 != null) goto L_0x00d4;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:40:0x00d4, code lost:
        r2.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:43:0x00dd, code lost:
        r2 = null;
        r3 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:49:0x00e7, code lost:
        r3.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:51:0x00ec, code lost:
        r2.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:52:0x00f0, code lost:
        r1 = e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:53:0x00f1, code lost:
        r2 = null;
        r3 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:57:0x00f9, code lost:
        if (r2.inTransaction() != false) goto L_0x00fb;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:58:0x00fb, code lost:
        r2.endTransaction();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:61:0x0110, code lost:
        r3.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:63:0x0115, code lost:
        r2.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:64:0x0119, code lost:
        r0 = th;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:65:0x011a, code lost:
        r2 = null;
        r3 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:67:0x011e, code lost:
        r3.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:69:0x0123, code lost:
        r2.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:72:0x0137, code lost:
        r0 = th;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:73:0x0138, code lost:
        r3 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:75:0x013c, code lost:
        r1 = e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:76:0x013d, code lost:
        r3 = null;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:79:0x0142, code lost:
        r3 = null;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:34:0x00bc A[ExcHandler: SQLiteFullException (r1v10 'e' android.database.sqlite.SQLiteFullException A[CUSTOM_DECLARE]), Splitter:B:7:0x002d] */
    /* JADX WARNING: Removed duplicated region for block: B:49:0x00e7  */
    /* JADX WARNING: Removed duplicated region for block: B:51:0x00ec  */
    /* JADX WARNING: Removed duplicated region for block: B:55:0x00f5 A[SYNTHETIC, Splitter:B:55:0x00f5] */
    /* JADX WARNING: Removed duplicated region for block: B:61:0x0110  */
    /* JADX WARNING: Removed duplicated region for block: B:63:0x0115  */
    /* JADX WARNING: Removed duplicated region for block: B:67:0x011e  */
    /* JADX WARNING: Removed duplicated region for block: B:69:0x0123  */
    /* JADX WARNING: Removed duplicated region for block: B:87:0x00d7 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:89:0x00d7 A[SYNTHETIC] */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final boolean zza(int r14, byte[] r15) {
        /*
            r13 = this;
            r13.zzm()
            r13.zzo()
            boolean r0 = r13.zzjw
            if (r0 == 0) goto L_0x000c
            r0 = 0
        L_0x000b:
            return r0
        L_0x000c:
            android.content.ContentValues r12 = new android.content.ContentValues
            r12.<init>()
            java.lang.String r0 = "type"
            java.lang.Integer r1 = java.lang.Integer.valueOf(r14)
            r12.put(r0, r1)
            java.lang.String r0 = "entry"
            r12.put(r0, r15)
            r1 = 0
            r0 = 5
            r9 = r1
        L_0x0022:
            r1 = 5
            if (r9 >= r1) goto L_0x0127
            r1 = 0
            r5 = 0
            r7 = 0
            r2 = 0
            r4 = 0
            r6 = 0
            r8 = 0
            r3 = 0
            android.database.sqlite.SQLiteDatabase r2 = r13.getWritableDatabase()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x00dc, SQLiteException -> 0x00f0, all -> 0x0119 }
            if (r2 != 0) goto L_0x003d
            r1 = 1
            r13.zzjw = r1     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0141, SQLiteException -> 0x013c, all -> 0x0137 }
            if (r2 == 0) goto L_0x003b
            r2.close()
        L_0x003b:
            r0 = 0
            goto L_0x000b
        L_0x003d:
            r2.beginTransaction()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0141, SQLiteException -> 0x013c, all -> 0x0137 }
            r10 = 0
            java.lang.String r1 = "select count(1) from messages"
            r5 = 0
            android.database.Cursor r3 = r2.rawQuery(r1, r5)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0141, SQLiteException -> 0x013c, all -> 0x0137 }
            if (r3 == 0) goto L_0x0146
            boolean r1 = r3.moveToFirst()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            if (r1 == 0) goto L_0x0146
            r1 = 0
            long r4 = r3.getLong(r1)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
        L_0x0056:
            r6 = 100000(0x186a0, double:4.94066E-319)
            int r1 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r1 < 0) goto L_0x00a3
            com.google.android.gms.measurement.internal.zzef r1 = r13.zzab()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            java.lang.String r6 = "Data loss, local db full"
            r1.zzao(r6)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r6 = 100000(0x186a0, double:4.94066E-319)
            long r4 = r6 - r4
            r6 = 1
            long r4 = r4 + r6
            java.lang.String r1 = "messages"
            java.lang.String r6 = "rowid in (select rowid from messages order by rowid asc limit ?)"
            r7 = 1
            java.lang.String[] r7 = new java.lang.String[r7]     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r8 = 0
            java.lang.String r10 = java.lang.Long.toString(r4)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r7[r8] = r10     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            int r1 = r2.delete(r1, r6, r7)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            long r6 = (long) r1     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            int r1 = (r6 > r4 ? 1 : (r6 == r4 ? 0 : -1))
            if (r1 == 0) goto L_0x00a3
            com.google.android.gms.measurement.internal.zzef r1 = r13.zzab()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            java.lang.String r8 = "Different delete count than expected in local db. expected, received, difference"
            java.lang.Long r10 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            java.lang.Long r11 = java.lang.Long.valueOf(r6)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            long r4 = r4 - r6
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r1.zza(r8, r10, r11, r4)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
        L_0x00a3:
            java.lang.String r1 = "messages"
            r4 = 0
            r2.insertOrThrow(r1, r4, r12)     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r2.setTransactionSuccessful()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            r2.endTransaction()     // Catch:{ SQLiteFullException -> 0x00bc, SQLiteDatabaseLockedException -> 0x0144, SQLiteException -> 0x013f }
            if (r3 == 0) goto L_0x00b4
            r3.close()
        L_0x00b4:
            if (r2 == 0) goto L_0x00b9
            r2.close()
        L_0x00b9:
            r0 = 1
            goto L_0x000b
        L_0x00bc:
            r1 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r13.zzab()     // Catch:{ all -> 0x013a }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x013a }
            java.lang.String r5 = "Error writing entry to local database"
            r4.zza(r5, r1)     // Catch:{ all -> 0x013a }
            r1 = 1
            r13.zzjw = r1     // Catch:{ all -> 0x013a }
            if (r3 == 0) goto L_0x00d2
            r3.close()
        L_0x00d2:
            if (r2 == 0) goto L_0x00d7
            r2.close()
        L_0x00d7:
            int r1 = r9 + 1
            r9 = r1
            goto L_0x0022
        L_0x00dc:
            r2 = move-exception
            r2 = r1
            r3 = r4
        L_0x00df:
            long r4 = (long) r0
            android.os.SystemClock.sleep(r4)     // Catch:{ all -> 0x013a }
            int r0 = r0 + 20
            if (r3 == 0) goto L_0x00ea
            r3.close()
        L_0x00ea:
            if (r2 == 0) goto L_0x00d7
            r2.close()
            goto L_0x00d7
        L_0x00f0:
            r1 = move-exception
            r2 = r5
            r3 = r6
        L_0x00f3:
            if (r2 == 0) goto L_0x00fe
            boolean r4 = r2.inTransaction()     // Catch:{ all -> 0x013a }
            if (r4 == 0) goto L_0x00fe
            r2.endTransaction()     // Catch:{ all -> 0x013a }
        L_0x00fe:
            com.google.android.gms.measurement.internal.zzef r4 = r13.zzab()     // Catch:{ all -> 0x013a }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x013a }
            java.lang.String r5 = "Error writing entry to local database"
            r4.zza(r5, r1)     // Catch:{ all -> 0x013a }
            r1 = 1
            r13.zzjw = r1     // Catch:{ all -> 0x013a }
            if (r3 == 0) goto L_0x0113
            r3.close()
        L_0x0113:
            if (r2 == 0) goto L_0x00d7
            r2.close()
            goto L_0x00d7
        L_0x0119:
            r0 = move-exception
            r2 = r7
            r3 = r8
        L_0x011c:
            if (r3 == 0) goto L_0x0121
            r3.close()
        L_0x0121:
            if (r2 == 0) goto L_0x0126
            r2.close()
        L_0x0126:
            throw r0
        L_0x0127:
            com.google.android.gms.measurement.internal.zzef r0 = r13.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()
            java.lang.String r1 = "Failed to write entry to local database"
            r0.zzao(r1)
            r0 = 0
            goto L_0x000b
        L_0x0137:
            r0 = move-exception
            r3 = r8
            goto L_0x011c
        L_0x013a:
            r0 = move-exception
            goto L_0x011c
        L_0x013c:
            r1 = move-exception
            r3 = r6
            goto L_0x00f3
        L_0x013f:
            r1 = move-exception
            goto L_0x00f3
        L_0x0141:
            r1 = move-exception
            r3 = r4
            goto L_0x00df
        L_0x0144:
            r1 = move-exception
            goto L_0x00df
        L_0x0146:
            r4 = r10
            goto L_0x0056
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzeb.zza(int, byte[]):boolean");
    }

    @VisibleForTesting
    private final boolean zzcg() {
        return getContext().getDatabasePath("google_app_measurement_local.db").exists();
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    @WorkerThread
    public final void resetAnalyticsData() {
        zzm();
        zzo();
        try {
            int delete = getWritableDatabase().delete("messages", null, null) + 0;
            if (delete > 0) {
                zzab().zzgs().zza("Reset local analytics data. records", Integer.valueOf(delete));
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error resetting local analytics data. error", e);
        }
    }

    public final boolean zza(zzai zzai) {
        Parcel obtain = Parcel.obtain();
        zzai.writeToParcel(obtain, 0);
        byte[] marshall = obtain.marshall();
        obtain.recycle();
        if (marshall.length <= 131072) {
            return zza(0, marshall);
        }
        zzab().zzgn().zzao("Event is too long for local database. Sending event directly to service");
        return false;
    }

    public final boolean zza(zzjn zzjn) {
        Parcel obtain = Parcel.obtain();
        zzjn.writeToParcel(obtain, 0);
        byte[] marshall = obtain.marshall();
        obtain.recycle();
        if (marshall.length <= 131072) {
            return zza(1, marshall);
        }
        zzab().zzgn().zzao("User property too long for local database. Sending directly to service");
        return false;
    }

    public final /* bridge */ /* synthetic */ zzfc zzaa() {
        return super.zzaa();
    }

    public final /* bridge */ /* synthetic */ zzef zzab() {
        return super.zzab();
    }

    public final /* bridge */ /* synthetic */ zzeo zzac() {
        return super.zzac();
    }

    public final /* bridge */ /* synthetic */ zzs zzad() {
        return super.zzad();
    }

    public final /* bridge */ /* synthetic */ zzr zzae() {
        return super.zzae();
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    /* JADX INFO: finally extract failed */
    /* JADX WARNING: Removed duplicated region for block: B:102:0x0178  */
    /* JADX WARNING: Removed duplicated region for block: B:104:0x017d  */
    /* JADX WARNING: Removed duplicated region for block: B:171:0x00d1 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:69:0x011a  */
    /* JADX WARNING: Removed duplicated region for block: B:71:0x011f  */
    /* JADX WARNING: Removed duplicated region for block: B:79:0x012c A[SYNTHETIC, Splitter:B:79:0x012c] */
    /* JADX WARNING: Removed duplicated region for block: B:85:0x0147  */
    /* JADX WARNING: Removed duplicated region for block: B:87:0x014c  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<com.google.android.gms.common.internal.safeparcel.AbstractSafeParcelable> zzc(int r15) {
        /*
            r14 = this;
            r14.zzo()
            r14.zzm()
            boolean r0 = r14.zzjw
            if (r0 == 0) goto L_0x000c
            r0 = 0
        L_0x000b:
            return r0
        L_0x000c:
            java.util.ArrayList r9 = new java.util.ArrayList
            r9.<init>()
            boolean r0 = r14.zzcg()
            if (r0 != 0) goto L_0x0019
            r0 = r9
            goto L_0x000b
        L_0x0019:
            r10 = 5
            r0 = 0
            r11 = r0
        L_0x001c:
            r0 = 5
            if (r11 >= r0) goto L_0x022c
            r0 = 0
            r4 = 0
            r2 = 0
            r3 = 0
            android.database.sqlite.SQLiteDatabase r0 = r14.getWritableDatabase()     // Catch:{ SQLiteFullException -> 0x0262, SQLiteDatabaseLockedException -> 0x0258, SQLiteException -> 0x024e, all -> 0x023c }
            if (r0 != 0) goto L_0x0033
            r1 = 1
            r14.zzjw = r1     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            if (r0 == 0) goto L_0x0031
            r0.close()
        L_0x0031:
            r0 = 0
            goto L_0x000b
        L_0x0033:
            r0.beginTransaction()     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            com.google.android.gms.measurement.internal.zzs r1 = r14.zzad()     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r2 = com.google.android.gms.measurement.internal.zzak.zzjd     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            boolean r1 = r1.zza(r2)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            if (r1 == 0) goto L_0x00d7
            long r6 = zza(r0)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r3 = 0
            r4 = 0
            r12 = -1
            int r1 = (r6 > r12 ? 1 : (r6 == r12 ? 0 : -1))
            if (r1 == 0) goto L_0x005a
            java.lang.String r3 = "rowid<?"
            r1 = 1
            java.lang.String[] r4 = new java.lang.String[r1]     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r1 = 0
            java.lang.String r2 = java.lang.String.valueOf(r6)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r4[r1] = r2     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
        L_0x005a:
            r1 = 100
            java.lang.String r8 = java.lang.Integer.toString(r1)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            java.lang.String r1 = "messages"
            r2 = 3
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r5 = 0
            java.lang.String r6 = "rowid"
            r2[r5] = r6     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r5 = 1
            java.lang.String r6 = "type"
            r2[r5] = r6     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r5 = 2
            java.lang.String r6 = "entry"
            r2[r5] = r6     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r5 = 0
            r6 = 0
            java.lang.String r7 = "rowid asc"
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r2 = r1
        L_0x007d:
            r4 = -1
        L_0x007f:
            boolean r1 = r2.moveToNext()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            if (r1 == 0) goto L_0x01f4
            r1 = 0
            long r4 = r2.getLong(r1)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 1
            int r1 = r2.getInt(r1)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r3 = 2
            byte[] r3 = r2.getBlob(r3)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            if (r1 != 0) goto L_0x0151
            android.os.Parcel r6 = android.os.Parcel.obtain()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 0
            int r7 = r3.length     // Catch:{ ParseException -> 0x00fd }
            r6.unmarshall(r3, r1, r7)     // Catch:{ ParseException -> 0x00fd }
            r1 = 0
            r6.setDataPosition(r1)     // Catch:{ ParseException -> 0x00fd }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r1 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ ParseException -> 0x00fd }
            java.lang.Object r1 = r1.createFromParcel(r6)     // Catch:{ ParseException -> 0x00fd }
            com.google.android.gms.measurement.internal.zzai r1 = (com.google.android.gms.measurement.internal.zzai) r1     // Catch:{ ParseException -> 0x00fd }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            if (r1 == 0) goto L_0x007f
            r9.add(r1)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x00b4:
            r1 = move-exception
            r3 = r0
        L_0x00b6:
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()     // Catch:{ all -> 0x026f }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ all -> 0x026f }
            java.lang.String r4 = "Error reading entries from local database"
            r0.zza(r4, r1)     // Catch:{ all -> 0x026f }
            r0 = 1
            r14.zzjw = r0     // Catch:{ all -> 0x026f }
            if (r2 == 0) goto L_0x00cb
            r2.close()
        L_0x00cb:
            if (r3 == 0) goto L_0x026c
            r3.close()
            r0 = r10
        L_0x00d1:
            int r1 = r11 + 1
            r11 = r1
            r10 = r0
            goto L_0x001c
        L_0x00d7:
            r1 = 100
            java.lang.String r8 = java.lang.Integer.toString(r1)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            java.lang.String r1 = "messages"
            r2 = 3
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r3 = 0
            java.lang.String r4 = "rowid"
            r2[r3] = r4     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r3 = 1
            java.lang.String r4 = "type"
            r2[r3] = r4     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r3 = 2
            java.lang.String r4 = "entry"
            r2[r3] = r4     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r3 = 0
            r4 = 0
            r5 = 0
            r6 = 0
            java.lang.String r7 = "rowid asc"
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteFullException -> 0x0267, SQLiteDatabaseLockedException -> 0x025d, SQLiteException -> 0x0253, all -> 0x0242 }
            r2 = r1
            goto L_0x007d
        L_0x00fd:
            r1 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ all -> 0x0123 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ all -> 0x0123 }
            java.lang.String r3 = "Failed to load event from local database"
            r1.zzao(r3)     // Catch:{ all -> 0x0123 }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x0110:
            r1 = move-exception
            r1 = r0
        L_0x0112:
            long r4 = (long) r10
            android.os.SystemClock.sleep(r4)     // Catch:{ all -> 0x0248 }
            int r0 = r10 + 20
            if (r2 == 0) goto L_0x011d
            r2.close()
        L_0x011d:
            if (r1 == 0) goto L_0x00d1
            r1.close()
            goto L_0x00d1
        L_0x0123:
            r1 = move-exception
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            throw r1     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x0128:
            r1 = move-exception
            r3 = r0
        L_0x012a:
            if (r3 == 0) goto L_0x0135
            boolean r0 = r3.inTransaction()     // Catch:{ all -> 0x026f }
            if (r0 == 0) goto L_0x0135
            r3.endTransaction()     // Catch:{ all -> 0x026f }
        L_0x0135:
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()     // Catch:{ all -> 0x026f }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ all -> 0x026f }
            java.lang.String r4 = "Error reading entries from local database"
            r0.zza(r4, r1)     // Catch:{ all -> 0x026f }
            r0 = 1
            r14.zzjw = r0     // Catch:{ all -> 0x026f }
            if (r2 == 0) goto L_0x014a
            r2.close()
        L_0x014a:
            if (r3 == 0) goto L_0x026c
            r3.close()
            r0 = r10
            goto L_0x00d1
        L_0x0151:
            r6 = 1
            if (r1 != r6) goto L_0x0199
            android.os.Parcel r6 = android.os.Parcel.obtain()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 0
            int r7 = r3.length     // Catch:{ ParseException -> 0x0181 }
            r6.unmarshall(r3, r1, r7)     // Catch:{ ParseException -> 0x0181 }
            r1 = 0
            r6.setDataPosition(r1)     // Catch:{ ParseException -> 0x0181 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzjn> r1 = com.google.android.gms.measurement.internal.zzjn.CREATOR     // Catch:{ ParseException -> 0x0181 }
            java.lang.Object r1 = r1.createFromParcel(r6)     // Catch:{ ParseException -> 0x0181 }
            com.google.android.gms.measurement.internal.zzjn r1 = (com.google.android.gms.measurement.internal.zzjn) r1     // Catch:{ ParseException -> 0x0181 }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x016c:
            if (r1 == 0) goto L_0x007f
            r9.add(r1)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x0173:
            r1 = move-exception
            r3 = r0
            r4 = r2
        L_0x0176:
            if (r4 == 0) goto L_0x017b
            r4.close()
        L_0x017b:
            if (r3 == 0) goto L_0x0180
            r3.close()
        L_0x0180:
            throw r1
        L_0x0181:
            r1 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ all -> 0x0194 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ all -> 0x0194 }
            java.lang.String r3 = "Failed to load user property from local database"
            r1.zzao(r3)     // Catch:{ all -> 0x0194 }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 0
            goto L_0x016c
        L_0x0194:
            r1 = move-exception
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            throw r1     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x0199:
            r6 = 2
            if (r1 != r6) goto L_0x01d3
            android.os.Parcel r6 = android.os.Parcel.obtain()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 0
            int r7 = r3.length     // Catch:{ ParseException -> 0x01bb }
            r6.unmarshall(r3, r1, r7)     // Catch:{ ParseException -> 0x01bb }
            r1 = 0
            r6.setDataPosition(r1)     // Catch:{ ParseException -> 0x01bb }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzq> r1 = com.google.android.gms.measurement.internal.zzq.CREATOR     // Catch:{ ParseException -> 0x01bb }
            java.lang.Object r1 = r1.createFromParcel(r6)     // Catch:{ ParseException -> 0x01bb }
            com.google.android.gms.measurement.internal.zzq r1 = (com.google.android.gms.measurement.internal.zzq) r1     // Catch:{ ParseException -> 0x01bb }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x01b4:
            if (r1 == 0) goto L_0x007f
            r9.add(r1)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x01bb:
            r1 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ all -> 0x01ce }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ all -> 0x01ce }
            java.lang.String r3 = "Failed to load user property from local database"
            r1.zzao(r3)     // Catch:{ all -> 0x01ce }
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r1 = 0
            goto L_0x01b4
        L_0x01ce:
            r1 = move-exception
            r6.recycle()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            throw r1     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x01d3:
            r3 = 3
            if (r1 != r3) goto L_0x01e5
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            java.lang.String r3 = "Skipping app launch break"
            r1.zzao(r3)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x01e5:
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            java.lang.String r3 = "Unknown record type in local database"
            r1.zzao(r3)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            goto L_0x007f
        L_0x01f4:
            java.lang.String r1 = "messages"
            java.lang.String r3 = "rowid <= ?"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r7 = 0
            java.lang.String r4 = java.lang.Long.toString(r4)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r6[r7] = r4     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            int r1 = r0.delete(r1, r3, r6)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            int r3 = r9.size()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            if (r1 >= r3) goto L_0x0219
            com.google.android.gms.measurement.internal.zzef r1 = r14.zzab()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            java.lang.String r3 = "Fewer entries removed from local database than expected"
            r1.zzao(r3)     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
        L_0x0219:
            r0.setTransactionSuccessful()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            r0.endTransaction()     // Catch:{ SQLiteFullException -> 0x00b4, SQLiteDatabaseLockedException -> 0x0110, SQLiteException -> 0x0128, all -> 0x0173 }
            if (r2 == 0) goto L_0x0224
            r2.close()
        L_0x0224:
            if (r0 == 0) goto L_0x0229
            r0.close()
        L_0x0229:
            r0 = r9
            goto L_0x000b
        L_0x022c:
            com.google.android.gms.measurement.internal.zzef r0 = r14.zzab()
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()
            java.lang.String r1 = "Failed to read events from database in reasonable time"
            r0.zzao(r1)
            r0 = 0
            goto L_0x000b
        L_0x023c:
            r1 = move-exception
            r0 = 0
            r3 = r2
            r4 = r0
            goto L_0x0176
        L_0x0242:
            r1 = move-exception
            r2 = 0
            r3 = r0
            r4 = r2
            goto L_0x0176
        L_0x0248:
            r0 = move-exception
            r3 = r1
        L_0x024a:
            r1 = r0
            r4 = r2
            goto L_0x0176
        L_0x024e:
            r1 = move-exception
            r2 = 0
            r3 = r4
            goto L_0x012a
        L_0x0253:
            r1 = move-exception
            r2 = 0
            r3 = r0
            goto L_0x012a
        L_0x0258:
            r1 = move-exception
            r2 = 0
            r1 = r0
            goto L_0x0112
        L_0x025d:
            r1 = move-exception
            r2 = 0
            r1 = r0
            goto L_0x0112
        L_0x0262:
            r0 = move-exception
            r2 = 0
            r1 = r0
            goto L_0x00b6
        L_0x0267:
            r1 = move-exception
            r2 = 0
            r3 = r0
            goto L_0x00b6
        L_0x026c:
            r0 = r10
            goto L_0x00d1
        L_0x026f:
            r0 = move-exception
            goto L_0x024a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzeb.zzc(int):java.util.List");
    }

    public final boolean zzc(zzq zzq) {
        zzz();
        byte[] zza = zzjs.zza((Parcelable) zzq);
        if (zza.length <= 131072) {
            return zza(2, zza);
        }
        zzab().zzgn().zzao("Conditional user property too long for local database. Sending directly to service");
        return false;
    }

    @WorkerThread
    public final boolean zzgh() {
        return zza(3, new byte[0]);
    }

    /* JADX WARNING: Removed duplicated region for block: B:33:0x006f  */
    /* JADX WARNING: Removed duplicated region for block: B:37:0x0077 A[SYNTHETIC, Splitter:B:37:0x0077] */
    /* JADX WARNING: Removed duplicated region for block: B:43:0x0092  */
    /* JADX WARNING: Removed duplicated region for block: B:47:0x009a  */
    /* JADX WARNING: Removed duplicated region for block: B:59:0x0061 A[SYNTHETIC] */
    /* JADX WARNING: Removed duplicated region for block: B:61:0x0061 A[SYNTHETIC] */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final boolean zzgi() {
        /*
            r12 = this;
            r6 = 5
            r5 = 0
            r1 = 1
            r0 = 0
            r12.zzo()
            r12.zzm()
            boolean r2 = r12.zzjw
            if (r2 == 0) goto L_0x000f
        L_0x000e:
            return r0
        L_0x000f:
            boolean r2 = r12.zzcg()
            if (r2 == 0) goto L_0x000e
            r2 = r6
            r7 = r0
        L_0x0017:
            if (r7 >= r6) goto L_0x009e
            android.database.sqlite.SQLiteDatabase r4 = r12.getWritableDatabase()     // Catch:{ SQLiteFullException -> 0x004a, SQLiteDatabaseLockedException -> 0x0065, SQLiteException -> 0x0073, all -> 0x0096 }
            if (r4 != 0) goto L_0x0028
            r3 = 1
            r12.zzjw = r3     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            if (r4 == 0) goto L_0x000e
            r4.close()
            goto L_0x000e
        L_0x0028:
            r4.beginTransaction()     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            java.lang.String r3 = "messages"
            java.lang.String r8 = "type == ?"
            r9 = 1
            java.lang.String[] r9 = new java.lang.String[r9]     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            r10 = 0
            r11 = 3
            java.lang.String r11 = java.lang.Integer.toString(r11)     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            r9[r10] = r11     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            r4.delete(r3, r8, r9)     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            r4.setTransactionSuccessful()     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            r4.endTransaction()     // Catch:{ SQLiteFullException -> 0x00b3, SQLiteDatabaseLockedException -> 0x00b1, SQLiteException -> 0x00af }
            if (r4 == 0) goto L_0x0048
            r4.close()
        L_0x0048:
            r0 = r1
            goto L_0x000e
        L_0x004a:
            r3 = move-exception
            r4 = r5
        L_0x004c:
            com.google.android.gms.measurement.internal.zzef r8 = r12.zzab()     // Catch:{ all -> 0x00ad }
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgk()     // Catch:{ all -> 0x00ad }
            java.lang.String r9 = "Error deleting app launch break from local database"
            r8.zza(r9, r3)     // Catch:{ all -> 0x00ad }
            r3 = 1
            r12.zzjw = r3     // Catch:{ all -> 0x00ad }
            if (r4 == 0) goto L_0x0061
            r4.close()
        L_0x0061:
            int r3 = r7 + 1
            r7 = r3
            goto L_0x0017
        L_0x0065:
            r3 = move-exception
            r4 = r5
        L_0x0067:
            long r8 = (long) r2
            android.os.SystemClock.sleep(r8)     // Catch:{ all -> 0x00ad }
            int r2 = r2 + 20
            if (r4 == 0) goto L_0x0061
            r4.close()
            goto L_0x0061
        L_0x0073:
            r3 = move-exception
            r4 = r5
        L_0x0075:
            if (r4 == 0) goto L_0x0080
            boolean r8 = r4.inTransaction()     // Catch:{ all -> 0x00ad }
            if (r8 == 0) goto L_0x0080
            r4.endTransaction()     // Catch:{ all -> 0x00ad }
        L_0x0080:
            com.google.android.gms.measurement.internal.zzef r8 = r12.zzab()     // Catch:{ all -> 0x00ad }
            com.google.android.gms.measurement.internal.zzeh r8 = r8.zzgk()     // Catch:{ all -> 0x00ad }
            java.lang.String r9 = "Error deleting app launch break from local database"
            r8.zza(r9, r3)     // Catch:{ all -> 0x00ad }
            r3 = 1
            r12.zzjw = r3     // Catch:{ all -> 0x00ad }
            if (r4 == 0) goto L_0x0061
            r4.close()
            goto L_0x0061
        L_0x0096:
            r0 = move-exception
            r4 = r5
        L_0x0098:
            if (r4 == 0) goto L_0x009d
            r4.close()
        L_0x009d:
            throw r0
        L_0x009e:
            com.google.android.gms.measurement.internal.zzef r1 = r12.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()
            java.lang.String r2 = "Error deleting app launch break from local database in reasonable time"
            r1.zzao(r2)
            goto L_0x000e
        L_0x00ad:
            r0 = move-exception
            goto L_0x0098
        L_0x00af:
            r3 = move-exception
            goto L_0x0075
        L_0x00b1:
            r3 = move-exception
            goto L_0x0067
        L_0x00b3:
            r3 = move-exception
            goto L_0x004c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzeb.zzgi():boolean");
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final /* bridge */ /* synthetic */ void zzn() {
        super.zzn();
    }

    public final /* bridge */ /* synthetic */ void zzo() {
        super.zzo();
    }

    public final /* bridge */ /* synthetic */ zza zzp() {
        return super.zzp();
    }

    public final /* bridge */ /* synthetic */ zzgp zzq() {
        return super.zzq();
    }

    public final /* bridge */ /* synthetic */ zzdy zzr() {
        return super.zzr();
    }

    public final /* bridge */ /* synthetic */ zzhv zzs() {
        return super.zzs();
    }

    public final /* bridge */ /* synthetic */ zzhq zzt() {
        return super.zzt();
    }

    public final /* bridge */ /* synthetic */ zzeb zzu() {
        return super.zzu();
    }

    public final /* bridge */ /* synthetic */ zziw zzv() {
        return super.zzv();
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }
}
