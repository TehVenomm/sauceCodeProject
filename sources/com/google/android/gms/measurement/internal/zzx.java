package com.google.android.gms.measurement.internal;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.os.Parcelable;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.games.Notifications;
import com.google.android.gms.internal.measurement.zzbs.zzc;
import com.google.android.gms.internal.measurement.zzbs.zzc.zza;
import com.google.android.gms.internal.measurement.zzbs.zze;
import com.google.android.gms.internal.measurement.zzbs.zzg;
import com.google.android.gms.internal.measurement.zzey;
import com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

final class zzx extends zzjh {
    /* access modifiers changed from: private */
    public static final String[] zzek = {"last_bundled_timestamp", "ALTER TABLE events ADD COLUMN last_bundled_timestamp INTEGER;", "last_bundled_day", "ALTER TABLE events ADD COLUMN last_bundled_day INTEGER;", "last_sampled_complex_event_id", "ALTER TABLE events ADD COLUMN last_sampled_complex_event_id INTEGER;", "last_sampling_rate", "ALTER TABLE events ADD COLUMN last_sampling_rate INTEGER;", "last_exempt_from_sampling", "ALTER TABLE events ADD COLUMN last_exempt_from_sampling INTEGER;", "current_session_count", "ALTER TABLE events ADD COLUMN current_session_count INTEGER;"};
    /* access modifiers changed from: private */
    public static final String[] zzel = {"origin", "ALTER TABLE user_attributes ADD COLUMN origin TEXT;"};
    /* access modifiers changed from: private */
    public static final String[] zzem = {"app_version", "ALTER TABLE apps ADD COLUMN app_version TEXT;", "app_store", "ALTER TABLE apps ADD COLUMN app_store TEXT;", "gmp_version", "ALTER TABLE apps ADD COLUMN gmp_version INTEGER;", "dev_cert_hash", "ALTER TABLE apps ADD COLUMN dev_cert_hash INTEGER;", "measurement_enabled", "ALTER TABLE apps ADD COLUMN measurement_enabled INTEGER;", "last_bundle_start_timestamp", "ALTER TABLE apps ADD COLUMN last_bundle_start_timestamp INTEGER;", "day", "ALTER TABLE apps ADD COLUMN day INTEGER;", "daily_public_events_count", "ALTER TABLE apps ADD COLUMN daily_public_events_count INTEGER;", "daily_events_count", "ALTER TABLE apps ADD COLUMN daily_events_count INTEGER;", "daily_conversions_count", "ALTER TABLE apps ADD COLUMN daily_conversions_count INTEGER;", "remote_config", "ALTER TABLE apps ADD COLUMN remote_config BLOB;", "config_fetched_time", "ALTER TABLE apps ADD COLUMN config_fetched_time INTEGER;", "failed_config_fetch_time", "ALTER TABLE apps ADD COLUMN failed_config_fetch_time INTEGER;", "app_version_int", "ALTER TABLE apps ADD COLUMN app_version_int INTEGER;", "firebase_instance_id", "ALTER TABLE apps ADD COLUMN firebase_instance_id TEXT;", "daily_error_events_count", "ALTER TABLE apps ADD COLUMN daily_error_events_count INTEGER;", "daily_realtime_events_count", "ALTER TABLE apps ADD COLUMN daily_realtime_events_count INTEGER;", "health_monitor_sample", "ALTER TABLE apps ADD COLUMN health_monitor_sample TEXT;", "android_id", "ALTER TABLE apps ADD COLUMN android_id INTEGER;", "adid_reporting_enabled", "ALTER TABLE apps ADD COLUMN adid_reporting_enabled INTEGER;", "ssaid_reporting_enabled", "ALTER TABLE apps ADD COLUMN ssaid_reporting_enabled INTEGER;", "admob_app_id", "ALTER TABLE apps ADD COLUMN admob_app_id TEXT;", "linked_admob_app_id", "ALTER TABLE apps ADD COLUMN linked_admob_app_id TEXT;", "dynamite_version", "ALTER TABLE apps ADD COLUMN dynamite_version INTEGER;", "safelisted_events", "ALTER TABLE apps ADD COLUMN safelisted_events TEXT;"};
    /* access modifiers changed from: private */
    public static final String[] zzen = {"realtime", "ALTER TABLE raw_events ADD COLUMN realtime INTEGER;"};
    /* access modifiers changed from: private */
    public static final String[] zzeo = {"has_realtime", "ALTER TABLE queue ADD COLUMN has_realtime INTEGER;", "retry_count", "ALTER TABLE queue ADD COLUMN retry_count INTEGER;"};
    /* access modifiers changed from: private */
    public static final String[] zzep = {"session_scoped", "ALTER TABLE event_filters ADD COLUMN session_scoped BOOLEAN;"};
    /* access modifiers changed from: private */
    public static final String[] zzeq = {"session_scoped", "ALTER TABLE property_filters ADD COLUMN session_scoped BOOLEAN;"};
    /* access modifiers changed from: private */
    public static final String[] zzer = {"previous_install_count", "ALTER TABLE app2 ADD COLUMN previous_install_count INTEGER;"};
    private final zzy zzes = new zzy(this, getContext(), "google_app_measurement.db");
    /* access modifiers changed from: private */
    public final zzjd zzet = new zzjd(zzx());

    zzx(zzjg zzjg) {
        super(zzjg);
    }

    @WorkerThread
    private final long zza(String str, String[] strArr) {
        Cursor cursor = null;
        try {
            cursor = getWritableDatabase().rawQuery(str, strArr);
            if (cursor.moveToFirst()) {
                long j = cursor.getLong(0);
                if (cursor != null) {
                    cursor.close();
                }
                return j;
            }
            throw new SQLiteException("Database returned empty set");
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Database error", str, e);
            throw e;
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    @WorkerThread
    private final long zza(String str, String[] strArr, long j) {
        Cursor cursor = null;
        try {
            Cursor rawQuery = getWritableDatabase().rawQuery(str, strArr);
            if (rawQuery.moveToFirst()) {
                j = rawQuery.getLong(0);
                if (rawQuery != null) {
                    rawQuery.close();
                }
            } else if (rawQuery != null) {
                rawQuery.close();
            }
            return j;
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Database error", str, e);
            throw e;
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    @WorkerThread
    @VisibleForTesting
    private final Object zza(Cursor cursor, int i) {
        int type = cursor.getType(i);
        switch (type) {
            case 0:
                zzab().zzgk().zzao("Loaded invalid null value from database");
                return null;
            case 1:
                return Long.valueOf(cursor.getLong(i));
            case 2:
                return Double.valueOf(cursor.getDouble(i));
            case 3:
                return cursor.getString(i);
            case 4:
                zzab().zzgk().zzao("Loaded invalid blob type value, ignoring it");
                return null;
            default:
                zzab().zzgk().zza("Loaded invalid unknown value type, ignoring it", Integer.valueOf(type));
                return null;
        }
    }

    @WorkerThread
    private static void zza(ContentValues contentValues, String str, Object obj) {
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotNull(obj);
        if (obj instanceof String) {
            contentValues.put(str, (String) obj);
        } else if (obj instanceof Long) {
            contentValues.put(str, (Long) obj);
        } else if (obj instanceof Double) {
            contentValues.put(str, (Double) obj);
        } else {
            throw new IllegalArgumentException("Invalid value type");
        }
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r0v10, types: [java.lang.Boolean] */
    /* JADX WARNING: type inference failed for: r0v12, types: [java.lang.Boolean] */
    /* JADX WARNING: type inference failed for: r0v13, types: [java.lang.Object] */
    /* JADX WARNING: type inference failed for: r0v17, types: [java.lang.Integer] */
    /* JADX WARNING: type inference failed for: r0v18 */
    /* JADX WARNING: type inference failed for: r0v19 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r0v0
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY], java.lang.Integer, java.lang.Boolean]
      uses: [java.lang.Boolean, java.lang.Object]
      mth insns count: 69
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.core.ProcessClass.lambda$processDependencies$0(ProcessClass.java:49)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:49)
    	at jadx.core.ProcessClass.process(ProcessClass.java:35)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 3 */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final boolean zza(java.lang.String r7, int r8, com.google.android.gms.internal.measurement.zzbk.zza r9) {
        /*
            r6 = this;
            r2 = 0
            r0 = 0
            r6.zzbi()
            r6.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r7)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r9)
            java.lang.String r1 = r9.zzjz()
            boolean r1 = android.text.TextUtils.isEmpty(r1)
            if (r1 == 0) goto L_0x0041
            com.google.android.gms.measurement.internal.zzef r1 = r6.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r7)
            boolean r4 = r9.zzkb()
            if (r4 == 0) goto L_0x0032
            int r0 = r9.getId()
            java.lang.Integer r0 = java.lang.Integer.valueOf(r0)
        L_0x0032:
            java.lang.String r4 = "Event filter had no event name. Audience definition ignored. appId, audienceId, filterId"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r8)
            java.lang.String r0 = java.lang.String.valueOf(r0)
            r1.zza(r4, r3, r5, r0)
            r0 = r2
        L_0x0040:
            return r0
        L_0x0041:
            byte[] r3 = r9.toByteArray()
            android.content.ContentValues r4 = new android.content.ContentValues
            r4.<init>()
            java.lang.String r1 = "app_id"
            r4.put(r1, r7)
            java.lang.String r1 = "audience_id"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r8)
            r4.put(r1, r5)
            boolean r1 = r9.zzkb()
            if (r1 == 0) goto L_0x00bd
            int r1 = r9.getId()
            java.lang.Integer r1 = java.lang.Integer.valueOf(r1)
        L_0x0066:
            java.lang.String r5 = "filter_id"
            r4.put(r5, r1)
            java.lang.String r1 = "event_name"
            java.lang.String r5 = r9.zzjz()
            r4.put(r1, r5)
            com.google.android.gms.measurement.internal.zzs r1 = r6.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r5 = com.google.android.gms.measurement.internal.zzak.zziy
            boolean r1 = r1.zze(r7, r5)
            if (r1 == 0) goto L_0x0093
            boolean r1 = r9.zzkh()
            if (r1 == 0) goto L_0x008e
            boolean r0 = r9.zzki()
            java.lang.Boolean r0 = java.lang.Boolean.valueOf(r0)
        L_0x008e:
            java.lang.String r1 = "session_scoped"
            r4.put(r1, r0)
        L_0x0093:
            java.lang.String r0 = "data"
            r4.put(r0, r3)
            android.database.sqlite.SQLiteDatabase r0 = r6.getWritableDatabase()     // Catch:{ SQLiteException -> 0x00bf }
            java.lang.String r1 = "event_filters"
            r3 = 0
            r5 = 5
            long r0 = r0.insertWithOnConflict(r1, r3, r4, r5)     // Catch:{ SQLiteException -> 0x00bf }
            r4 = -1
            int r0 = (r0 > r4 ? 1 : (r0 == r4 ? 0 : -1))
            if (r0 != 0) goto L_0x00bb
            com.google.android.gms.measurement.internal.zzef r0 = r6.zzab()     // Catch:{ SQLiteException -> 0x00bf }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x00bf }
            java.lang.String r1 = "Failed to insert event filter (got -1). appId"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r7)     // Catch:{ SQLiteException -> 0x00bf }
            r0.zza(r1, r3)     // Catch:{ SQLiteException -> 0x00bf }
        L_0x00bb:
            r0 = 1
            goto L_0x0040
        L_0x00bd:
            r1 = r0
            goto L_0x0066
        L_0x00bf:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r6.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r3 = "Error storing event filter. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r7)
            r1.zza(r3, r4, r0)
            r0 = r2
            goto L_0x0040
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, int, com.google.android.gms.internal.measurement.zzbk$zza):boolean");
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r0v11, types: [java.lang.Boolean] */
    /* JADX WARNING: type inference failed for: r0v13, types: [java.lang.Boolean] */
    /* JADX WARNING: type inference failed for: r0v14, types: [java.lang.Object] */
    /* JADX WARNING: type inference failed for: r0v18, types: [java.lang.Integer] */
    /* JADX WARNING: type inference failed for: r0v19 */
    /* JADX WARNING: type inference failed for: r0v20 */
    /* JADX WARNING: Multi-variable type inference failed. Error: jadx.core.utils.exceptions.JadxRuntimeException: No candidate types for var: r0v0
      assigns: [?[int, float, boolean, short, byte, char, OBJECT, ARRAY], java.lang.Integer, java.lang.Boolean]
      uses: [java.lang.Boolean, java.lang.Object]
      mth insns count: 70
    	at jadx.core.dex.visitors.typeinference.TypeSearch.fillTypeCandidates(TypeSearch.java:237)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.typeinference.TypeSearch.run(TypeSearch.java:53)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.runMultiVariableSearch(TypeInferenceVisitor.java:99)
    	at jadx.core.dex.visitors.typeinference.TypeInferenceVisitor.visit(TypeInferenceVisitor.java:92)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:27)
    	at jadx.core.dex.visitors.DepthTraversal.lambda$visit$1(DepthTraversal.java:14)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.dex.visitors.DepthTraversal.visit(DepthTraversal.java:14)
    	at jadx.core.ProcessClass.process(ProcessClass.java:30)
    	at jadx.core.ProcessClass.lambda$processDependencies$0(ProcessClass.java:49)
    	at java.base/java.util.ArrayList.forEach(ArrayList.java:1540)
    	at jadx.core.ProcessClass.processDependencies(ProcessClass.java:49)
    	at jadx.core.ProcessClass.process(ProcessClass.java:35)
    	at jadx.api.JadxDecompiler.processClass(JadxDecompiler.java:311)
    	at jadx.api.JavaClass.decompile(JavaClass.java:62)
    	at jadx.api.JadxDecompiler.lambda$appendSourcesSave$0(JadxDecompiler.java:217)
     */
    /* JADX WARNING: Unknown variable types count: 3 */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final boolean zza(java.lang.String r7, int r8, com.google.android.gms.internal.measurement.zzbk.zzd r9) {
        /*
            r6 = this;
            r2 = 0
            r0 = 0
            r6.zzbi()
            r6.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r7)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r9)
            java.lang.String r1 = r9.getPropertyName()
            boolean r1 = android.text.TextUtils.isEmpty(r1)
            if (r1 == 0) goto L_0x0041
            com.google.android.gms.measurement.internal.zzef r1 = r6.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r7)
            boolean r4 = r9.zzkb()
            if (r4 == 0) goto L_0x0032
            int r0 = r9.getId()
            java.lang.Integer r0 = java.lang.Integer.valueOf(r0)
        L_0x0032:
            java.lang.String r4 = "Property filter had no property name. Audience definition ignored. appId, audienceId, filterId"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r8)
            java.lang.String r0 = java.lang.String.valueOf(r0)
            r1.zza(r4, r3, r5, r0)
            r0 = r2
        L_0x0040:
            return r0
        L_0x0041:
            byte[] r3 = r9.toByteArray()
            android.content.ContentValues r4 = new android.content.ContentValues
            r4.<init>()
            java.lang.String r1 = "app_id"
            r4.put(r1, r7)
            java.lang.String r1 = "audience_id"
            java.lang.Integer r5 = java.lang.Integer.valueOf(r8)
            r4.put(r1, r5)
            boolean r1 = r9.zzkb()
            if (r1 == 0) goto L_0x00bd
            int r1 = r9.getId()
            java.lang.Integer r1 = java.lang.Integer.valueOf(r1)
        L_0x0066:
            java.lang.String r5 = "filter_id"
            r4.put(r5, r1)
            java.lang.String r1 = "property_name"
            java.lang.String r5 = r9.getPropertyName()
            r4.put(r1, r5)
            com.google.android.gms.measurement.internal.zzs r1 = r6.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r5 = com.google.android.gms.measurement.internal.zzak.zziy
            boolean r1 = r1.zze(r7, r5)
            if (r1 == 0) goto L_0x0093
            boolean r1 = r9.zzkh()
            if (r1 == 0) goto L_0x008e
            boolean r0 = r9.zzki()
            java.lang.Boolean r0 = java.lang.Boolean.valueOf(r0)
        L_0x008e:
            java.lang.String r1 = "session_scoped"
            r4.put(r1, r0)
        L_0x0093:
            java.lang.String r0 = "data"
            r4.put(r0, r3)
            android.database.sqlite.SQLiteDatabase r0 = r6.getWritableDatabase()     // Catch:{ SQLiteException -> 0x00bf }
            java.lang.String r1 = "property_filters"
            r3 = 0
            r5 = 5
            long r0 = r0.insertWithOnConflict(r1, r3, r4, r5)     // Catch:{ SQLiteException -> 0x00bf }
            r4 = -1
            int r0 = (r0 > r4 ? 1 : (r0 == r4 ? 0 : -1))
            if (r0 != 0) goto L_0x00d4
            com.google.android.gms.measurement.internal.zzef r0 = r6.zzab()     // Catch:{ SQLiteException -> 0x00bf }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x00bf }
            java.lang.String r1 = "Failed to insert property filter (got -1). appId"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r7)     // Catch:{ SQLiteException -> 0x00bf }
            r0.zza(r1, r3)     // Catch:{ SQLiteException -> 0x00bf }
            r0 = r2
            goto L_0x0040
        L_0x00bd:
            r1 = r0
            goto L_0x0066
        L_0x00bf:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r1 = r6.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()
            java.lang.String r3 = "Error storing property filter. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r7)
            r1.zza(r3, r4, r0)
            r0 = r2
            goto L_0x0040
        L_0x00d4:
            r0 = 1
            goto L_0x0040
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, int, com.google.android.gms.internal.measurement.zzbk$zzd):boolean");
    }

    private final boolean zza(String str, List<Integer> list) {
        Preconditions.checkNotEmpty(str);
        zzbi();
        zzo();
        SQLiteDatabase writableDatabase = getWritableDatabase();
        try {
            long zza = zza("select count(1) from audience_filter_values where app_id=?", new String[]{str});
            int max = Math.max(0, Math.min(2000, zzad().zzb(str, zzak.zzhk)));
            if (zza <= ((long) max)) {
                return false;
            }
            ArrayList arrayList = new ArrayList();
            int i = 0;
            while (true) {
                if (i < list.size()) {
                    Integer num = (Integer) list.get(i);
                    if (num == null || !(num instanceof Integer)) {
                        break;
                    }
                    arrayList.add(Integer.toString(num.intValue()));
                    i++;
                } else {
                    String join = TextUtils.join(",", arrayList);
                    String sb = new StringBuilder(String.valueOf(join).length() + 2).append("(").append(join).append(")").toString();
                    if (writableDatabase.delete("audience_filter_values", new StringBuilder(String.valueOf(sb).length() + 140).append("audience_id in (select audience_id from audience_filter_values where app_id=? and audience_id not in ").append(sb).append(" order by rowid desc limit -1 offset ?)").toString(), new String[]{str, Integer.toString(max)}) > 0) {
                        return true;
                    }
                }
            }
            return false;
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Database error querying filters. appId", zzef.zzam(str), e);
            return false;
        }
    }

    private final boolean zzcg() {
        return getContext().getDatabasePath("google_app_measurement.db").exists();
    }

    @WorkerThread
    public final void beginTransaction() {
        zzbi();
        getWritableDatabase().beginTransaction();
    }

    @WorkerThread
    public final void endTransaction() {
        zzbi();
        getWritableDatabase().endTransaction();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    @VisibleForTesting
    public final SQLiteDatabase getWritableDatabase() {
        zzo();
        try {
            return this.zzes.getWritableDatabase();
        } catch (SQLiteException e) {
            zzab().zzgn().zza("Error opening database", e);
            throw e;
        }
    }

    @WorkerThread
    public final void setTransactionSuccessful() {
        zzbi();
        getWritableDatabase().setTransactionSuccessful();
    }

    public final long zza(zzg zzg) throws IOException {
        zzo();
        zzbi();
        Preconditions.checkNotNull(zzg);
        Preconditions.checkNotEmpty(zzg.zzag());
        byte[] byteArray = zzg.toByteArray();
        long zza = zzgw().zza(byteArray);
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzg.zzag());
        contentValues.put("metadata_fingerprint", Long.valueOf(zza));
        contentValues.put("metadata", byteArray);
        try {
            getWritableDatabase().insertWithOnConflict("raw_events_metadata", null, contentValues, 4);
            return zza;
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing raw event metadata. appId", zzef.zzam(zzg.zzag()), e);
            throw e;
        }
    }

    /* JADX WARNING: type inference failed for: r0v0 */
    /* JADX WARNING: type inference failed for: r2v0, types: [android.database.Cursor] */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r1v1, types: [android.database.Cursor] */
    /* JADX WARNING: type inference failed for: r2v3 */
    /* JADX WARNING: type inference failed for: r1v2 */
    /* JADX WARNING: type inference failed for: r0v3, types: [android.util.Pair<com.google.android.gms.internal.measurement.zzbs$zzc, java.lang.Long>] */
    /* JADX WARNING: type inference failed for: r0v5, types: [android.util.Pair] */
    /* JADX WARNING: type inference failed for: r1v5 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x0089  */
    /* JADX WARNING: Unknown variable types count: 3 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final android.util.Pair<com.google.android.gms.internal.measurement.zzbs.zzc, java.lang.Long> zza(java.lang.String r7, java.lang.Long r8) {
        /*
            r6 = this;
            r0 = 0
            r6.zzo()
            r6.zzbi()
            android.database.sqlite.SQLiteDatabase r1 = r6.getWritableDatabase()     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            java.lang.String r2 = "select main_event, children_to_process from main_event_params where app_id=? and event_id=?"
            r3 = 2
            java.lang.String[] r3 = new java.lang.String[r3]     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            r4 = 0
            r3[r4] = r7     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            r4 = 1
            java.lang.String r5 = java.lang.String.valueOf(r8)     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            r3[r4] = r5     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            android.database.Cursor r1 = r1.rawQuery(r2, r3)     // Catch:{ SQLiteException -> 0x006f, all -> 0x0084 }
            boolean r2 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x0091 }
            if (r2 != 0) goto L_0x0037
            com.google.android.gms.measurement.internal.zzef r2 = r6.zzab()     // Catch:{ SQLiteException -> 0x0091 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()     // Catch:{ SQLiteException -> 0x0091 }
            java.lang.String r3 = "Main event not found"
            r2.zzao(r3)     // Catch:{ SQLiteException -> 0x0091 }
            if (r1 == 0) goto L_0x0036
            r1.close()
        L_0x0036:
            return r0
        L_0x0037:
            r2 = 0
            byte[] r2 = r1.getBlob(r2)     // Catch:{ SQLiteException -> 0x0091 }
            r3 = 1
            long r4 = r1.getLong(r3)     // Catch:{ SQLiteException -> 0x0091 }
            com.google.android.gms.internal.measurement.zzel r3 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x0057 }
            com.google.android.gms.internal.measurement.zzbs$zzc r2 = com.google.android.gms.internal.measurement.zzbs.zzc.zzc(r2, r3)     // Catch:{ IOException -> 0x0057 }
            java.lang.Long r3 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0091 }
            android.util.Pair r0 = android.util.Pair.create(r2, r3)     // Catch:{ SQLiteException -> 0x0091 }
            if (r1 == 0) goto L_0x0036
            r1.close()
            goto L_0x0036
        L_0x0057:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzef r3 = r6.zzab()     // Catch:{ SQLiteException -> 0x0091 }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ SQLiteException -> 0x0091 }
            java.lang.String r4 = "Failed to merge main event. appId, eventId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r7)     // Catch:{ SQLiteException -> 0x0091 }
            r3.zza(r4, r5, r8, r2)     // Catch:{ SQLiteException -> 0x0091 }
            if (r1 == 0) goto L_0x0036
            r1.close()
            goto L_0x0036
        L_0x006f:
            r2 = move-exception
            r1 = r0
        L_0x0071:
            com.google.android.gms.measurement.internal.zzef r3 = r6.zzab()     // Catch:{ all -> 0x008d }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ all -> 0x008d }
            java.lang.String r4 = "Error selecting main event"
            r3.zza(r4, r2)     // Catch:{ all -> 0x008d }
            if (r1 == 0) goto L_0x0036
            r1.close()
            goto L_0x0036
        L_0x0084:
            r1 = move-exception
            r2 = r0
            r3 = r1
        L_0x0087:
            if (r2 == 0) goto L_0x008c
            r2.close()
        L_0x008c:
            throw r3
        L_0x008d:
            r0 = move-exception
            r2 = r1
            r3 = r0
            goto L_0x0087
        L_0x0091:
            r2 = move-exception
            goto L_0x0071
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, java.lang.Long):android.util.Pair");
    }

    /* JADX WARNING: Removed duplicated region for block: B:37:0x0131  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzw zza(long r10, java.lang.String r12, boolean r13, boolean r14, boolean r15, boolean r16, boolean r17) {
        /*
            r9 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            r9.zzo()
            r9.zzbi()
            com.google.android.gms.measurement.internal.zzw r8 = new com.google.android.gms.measurement.internal.zzw
            r8.<init>()
            android.database.sqlite.SQLiteDatabase r0 = r9.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            java.lang.String r1 = "apps"
            r2 = 6
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 0
            java.lang.String r4 = "day"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 1
            java.lang.String r4 = "daily_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 2
            java.lang.String r4 = "daily_public_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 3
            java.lang.String r4 = "daily_conversions_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 4
            java.lang.String r4 = "daily_error_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r3 = 5
            java.lang.String r4 = "daily_realtime_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r5 = 0
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0113, all -> 0x012d }
            boolean r2 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x0135 }
            if (r2 != 0) goto L_0x0061
            com.google.android.gms.measurement.internal.zzef r0 = r9.zzab()     // Catch:{ SQLiteException -> 0x0135 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgn()     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r2 = "Not updating daily counts, app is not known. appId"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x0135 }
            r0.zza(r2, r3)     // Catch:{ SQLiteException -> 0x0135 }
            if (r1 == 0) goto L_0x0060
            r1.close()
        L_0x0060:
            return r8
        L_0x0061:
            r2 = 0
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            int r2 = (r2 > r10 ? 1 : (r2 == r10 ? 0 : -1))
            if (r2 != 0) goto L_0x008d
            r2 = 1
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            r8.zzeg = r2     // Catch:{ SQLiteException -> 0x0135 }
            r2 = 2
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            r8.zzef = r2     // Catch:{ SQLiteException -> 0x0135 }
            r2 = 3
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            r8.zzeh = r2     // Catch:{ SQLiteException -> 0x0135 }
            r2 = 4
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            r8.zzei = r2     // Catch:{ SQLiteException -> 0x0135 }
            r2 = 5
            long r2 = r1.getLong(r2)     // Catch:{ SQLiteException -> 0x0135 }
            r8.zzej = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x008d:
            if (r13 == 0) goto L_0x0096
            long r2 = r8.zzeg     // Catch:{ SQLiteException -> 0x0135 }
            r4 = 1
            long r2 = r2 + r4
            r8.zzeg = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x0096:
            if (r14 == 0) goto L_0x009f
            long r2 = r8.zzef     // Catch:{ SQLiteException -> 0x0135 }
            r4 = 1
            long r2 = r2 + r4
            r8.zzef = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x009f:
            if (r15 == 0) goto L_0x00a8
            long r2 = r8.zzeh     // Catch:{ SQLiteException -> 0x0135 }
            r4 = 1
            long r2 = r2 + r4
            r8.zzeh = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x00a8:
            if (r16 == 0) goto L_0x00b1
            long r2 = r8.zzei     // Catch:{ SQLiteException -> 0x0135 }
            r4 = 1
            long r2 = r2 + r4
            r8.zzei = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x00b1:
            if (r17 == 0) goto L_0x00ba
            long r2 = r8.zzej     // Catch:{ SQLiteException -> 0x0135 }
            r4 = 1
            long r2 = r2 + r4
            r8.zzej = r2     // Catch:{ SQLiteException -> 0x0135 }
        L_0x00ba:
            android.content.ContentValues r2 = new android.content.ContentValues     // Catch:{ SQLiteException -> 0x0135 }
            r2.<init>()     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "day"
            java.lang.Long r4 = java.lang.Long.valueOf(r10)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "daily_public_events_count"
            long r4 = r8.zzef     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "daily_events_count"
            long r4 = r8.zzeg     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "daily_conversions_count"
            long r4 = r8.zzeh     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "daily_error_events_count"
            long r4 = r8.zzei     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "daily_realtime_events_count"
            long r4 = r8.zzej     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x0135 }
            r2.put(r3, r4)     // Catch:{ SQLiteException -> 0x0135 }
            java.lang.String r3 = "apps"
            java.lang.String r4 = "app_id=?"
            r5 = 1
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x0135 }
            r6 = 0
            r5[r6] = r12     // Catch:{ SQLiteException -> 0x0135 }
            r0.update(r3, r2, r4, r5)     // Catch:{ SQLiteException -> 0x0135 }
            if (r1 == 0) goto L_0x0060
            r1.close()
            goto L_0x0060
        L_0x0113:
            r0 = move-exception
            r1 = 0
        L_0x0115:
            com.google.android.gms.measurement.internal.zzef r2 = r9.zzab()     // Catch:{ all -> 0x0137 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x0137 }
            java.lang.String r3 = "Error updating daily counts. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ all -> 0x0137 }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x0137 }
            if (r1 == 0) goto L_0x0060
            r1.close()
            goto L_0x0060
        L_0x012d:
            r0 = move-exception
            r1 = 0
        L_0x012f:
            if (r1 == 0) goto L_0x0134
            r1.close()
        L_0x0134:
            throw r0
        L_0x0135:
            r0 = move-exception
            goto L_0x0115
        L_0x0137:
            r0 = move-exception
            goto L_0x012f
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(long, java.lang.String, boolean, boolean, boolean, boolean, boolean):com.google.android.gms.measurement.internal.zzw");
    }

    /* JADX WARNING: Removed duplicated region for block: B:57:0x0105  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<android.util.Pair<com.google.android.gms.internal.measurement.zzbs.zzg, java.lang.Long>> zza(java.lang.String r12, int r13, int r14) {
        /*
            r11 = this;
            r10 = 0
            r1 = 1
            r9 = 0
            r11.zzo()
            r11.zzbi()
            if (r13 <= 0) goto L_0x0053
            r0 = r1
        L_0x000c:
            com.google.android.gms.common.internal.Preconditions.checkArgument(r0)
            if (r14 <= 0) goto L_0x0055
        L_0x0011:
            com.google.android.gms.common.internal.Preconditions.checkArgument(r1)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            android.database.sqlite.SQLiteDatabase r0 = r11.getWritableDatabase()     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            java.lang.String r1 = "queue"
            r2 = 3
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            r3 = 0
            java.lang.String r4 = "rowid"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            r3 = 1
            java.lang.String r4 = "data"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            r3 = 2
            java.lang.String r4 = "retry_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            r5 = 0
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            r5 = 0
            r6 = 0
            java.lang.String r7 = "rowid"
            java.lang.String r8 = java.lang.String.valueOf(r13)     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            android.database.Cursor r2 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteException -> 0x00e3, all -> 0x0101 }
            boolean r0 = r2.moveToFirst()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            if (r0 != 0) goto L_0x0057
            java.util.List r0 = java.util.Collections.emptyList()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            if (r2 == 0) goto L_0x0052
            r2.close()
        L_0x0052:
            return r0
        L_0x0053:
            r0 = r9
            goto L_0x000c
        L_0x0055:
            r1 = r9
            goto L_0x0011
        L_0x0057:
            java.util.ArrayList r1 = new java.util.ArrayList     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r1.<init>()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r3 = r9
        L_0x005d:
            r0 = 0
            long r4 = r2.getLong(r0)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r0 = 1
            byte[] r0 = r2.getBlob(r0)     // Catch:{ IOException -> 0x00bb }
            com.google.android.gms.measurement.internal.zzjo r6 = r11.zzgw()     // Catch:{ IOException -> 0x00bb }
            byte[] r6 = r6.zzb(r0)     // Catch:{ IOException -> 0x00bb }
            boolean r0 = r1.isEmpty()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            if (r0 != 0) goto L_0x0079
            int r0 = r6.length     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            int r0 = r0 + r3
            if (r0 > r14) goto L_0x00b4
        L_0x0079:
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r0 = com.google.android.gms.internal.measurement.zzbs.zzg.zzpr()     // Catch:{ IOException -> 0x00cf }
            com.google.android.gms.internal.measurement.zzel r7 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x00cf }
            com.google.android.gms.internal.measurement.zzdh r0 = r0.zzf(r6, r7)     // Catch:{ IOException -> 0x00cf }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r0 = (com.google.android.gms.internal.measurement.zzbs.zzg.zza) r0     // Catch:{ IOException -> 0x00cf }
            r7 = 2
            boolean r7 = r2.isNull(r7)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            if (r7 != 0) goto L_0x0096
            r7 = 2
            int r7 = r2.getInt(r7)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r0.zzw(r7)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
        L_0x0096:
            int r6 = r6.length     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            com.google.android.gms.internal.measurement.zzgi r0 = r0.zzug()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            com.google.android.gms.internal.measurement.zzey r0 = (com.google.android.gms.internal.measurement.zzey) r0     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            com.google.android.gms.internal.measurement.zzbs$zzg r0 = (com.google.android.gms.internal.measurement.zzbs.zzg) r0     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            android.util.Pair r0 = android.util.Pair.create(r0, r4)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r1.add(r0)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            int r0 = r3 + r6
        L_0x00ac:
            boolean r3 = r2.moveToNext()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            if (r3 == 0) goto L_0x00b4
            if (r0 <= r14) goto L_0x010f
        L_0x00b4:
            if (r2 == 0) goto L_0x00b9
            r2.close()
        L_0x00b9:
            r0 = r1
            goto L_0x0052
        L_0x00bb:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r11.zzab()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            java.lang.String r5 = "Failed to unzip queued bundle. appId"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r4.zza(r5, r6, r0)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r0 = r3
            goto L_0x00ac
        L_0x00cf:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r11.zzab()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            java.lang.String r5 = "Failed to merge queued bundle. appId"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r4.zza(r5, r6, r0)     // Catch:{ SQLiteException -> 0x010c, all -> 0x0112 }
            r0 = r3
            goto L_0x00ac
        L_0x00e3:
            r0 = move-exception
            r1 = r10
        L_0x00e5:
            com.google.android.gms.measurement.internal.zzef r2 = r11.zzab()     // Catch:{ all -> 0x0109 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x0109 }
            java.lang.String r3 = "Error querying bundles. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ all -> 0x0109 }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x0109 }
            java.util.List r0 = java.util.Collections.emptyList()     // Catch:{ all -> 0x0109 }
            if (r1 == 0) goto L_0x0052
            r1.close()
            goto L_0x0052
        L_0x0101:
            r0 = move-exception
            r2 = r10
        L_0x0103:
            if (r2 == 0) goto L_0x0108
            r2.close()
        L_0x0108:
            throw r0
        L_0x0109:
            r0 = move-exception
            r2 = r1
            goto L_0x0103
        L_0x010c:
            r0 = move-exception
            r1 = r2
            goto L_0x00e5
        L_0x010f:
            r3 = r0
            goto L_0x005d
        L_0x0112:
            r0 = move-exception
            goto L_0x0103
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, int, int):java.util.List");
    }

    /* JADX WARNING: Code restructure failed: missing block: B:33:0x00e4, code lost:
        r0 = e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:34:0x00e5, code lost:
        r1 = r7;
        r13 = r2;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:43:0x0103, code lost:
        r7.close();
     */
    /* JADX WARNING: Code restructure failed: missing block: B:51:0x0110, code lost:
        r0 = th;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:43:0x0103  */
    /* JADX WARNING: Removed duplicated region for block: B:51:0x0110 A[ExcHandler: all (th java.lang.Throwable), Splitter:B:9:0x007c] */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<com.google.android.gms.measurement.internal.zzjp> zza(java.lang.String r12, java.lang.String r13, java.lang.String r14) {
        /*
            r11 = this;
            r10 = 0
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            r11.zzo()
            r11.zzbi()
            java.util.ArrayList r9 = new java.util.ArrayList
            r9.<init>()
            java.util.ArrayList r0 = new java.util.ArrayList     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r1 = 3
            r0.<init>(r1)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r0.add(r12)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.StringBuilder r1 = new java.lang.StringBuilder     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r2 = "app_id=?"
            r1.<init>(r2)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            boolean r2 = android.text.TextUtils.isEmpty(r13)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            if (r2 != 0) goto L_0x002d
            r0.add(r13)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r2 = " and origin=?"
            r1.append(r2)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
        L_0x002d:
            boolean r2 = android.text.TextUtils.isEmpty(r14)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            if (r2 != 0) goto L_0x0045
            java.lang.String r2 = java.lang.String.valueOf(r14)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r3 = "*"
            java.lang.String r2 = r2.concat(r3)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r0.add(r2)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r2 = " and name glob ?"
            r1.append(r2)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
        L_0x0045:
            int r2 = r0.size()     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.Object[] r4 = r0.toArray(r2)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String[] r4 = (java.lang.String[]) r4     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            android.database.sqlite.SQLiteDatabase r0 = r11.getWritableDatabase()     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r3 = r1.toString()     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            java.lang.String r1 = "user_attributes"
            r2 = 4
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r5 = 0
            java.lang.String r6 = "name"
            r2[r5] = r6     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r5 = 1
            java.lang.String r6 = "set_timestamp"
            r2[r5] = r6     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r5 = 2
            java.lang.String r6 = "value"
            r2[r5] = r6     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r5 = 3
            java.lang.String r6 = "origin"
            r2[r5] = r6     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            r5 = 0
            r6 = 0
            java.lang.String r7 = "rowid"
            java.lang.String r8 = "1001"
            android.database.Cursor r7 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteException -> 0x010a, all -> 0x00ff }
            boolean r0 = r7.moveToFirst()     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            if (r0 != 0) goto L_0x008a
            if (r7 == 0) goto L_0x0087
            r7.close()
        L_0x0087:
            r0 = r9
        L_0x0088:
            return r0
        L_0x0089:
            r13 = r2
        L_0x008a:
            int r0 = r9.size()     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            r1 = 1000(0x3e8, float:1.401E-42)
            if (r0 < r1) goto L_0x00ac
            com.google.android.gms.measurement.internal.zzef r0 = r11.zzab()     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            java.lang.String r1 = "Read more than the max allowed user properties, ignoring excess"
            r2 = 1000(0x3e8, float:1.401E-42)
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            r0.zza(r1, r2)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
        L_0x00a5:
            if (r7 == 0) goto L_0x00aa
            r7.close()
        L_0x00aa:
            r0 = r9
            goto L_0x0088
        L_0x00ac:
            r0 = 0
            java.lang.String r3 = r7.getString(r0)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            r0 = 1
            long r4 = r7.getLong(r0)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            r0 = 2
            java.lang.Object r6 = r11.zza(r7, r0)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            r0 = 3
            java.lang.String r2 = r7.getString(r0)     // Catch:{ SQLiteException -> 0x010d, all -> 0x0110 }
            if (r6 != 0) goto L_0x00da
            com.google.android.gms.measurement.internal.zzef r0 = r11.zzab()     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            java.lang.String r1 = "(2)Read invalid user property value, ignoring it"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            r0.zza(r1, r3, r2, r14)     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
        L_0x00d3:
            boolean r0 = r7.moveToNext()     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            if (r0 != 0) goto L_0x0089
            goto L_0x00a5
        L_0x00da:
            com.google.android.gms.measurement.internal.zzjp r0 = new com.google.android.gms.measurement.internal.zzjp     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            r1 = r12
            r0.<init>(r1, r2, r3, r4, r6)     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            r9.add(r0)     // Catch:{ SQLiteException -> 0x00e4, all -> 0x0110 }
            goto L_0x00d3
        L_0x00e4:
            r0 = move-exception
            r1 = r7
            r13 = r2
        L_0x00e7:
            com.google.android.gms.measurement.internal.zzef r2 = r11.zzab()     // Catch:{ all -> 0x0107 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x0107 }
            java.lang.String r3 = "(2)Error querying user properties"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ all -> 0x0107 }
            r2.zza(r3, r4, r13, r0)     // Catch:{ all -> 0x0107 }
            if (r1 == 0) goto L_0x00fd
            r1.close()
        L_0x00fd:
            r0 = r10
            goto L_0x0088
        L_0x00ff:
            r0 = move-exception
            r7 = r10
        L_0x0101:
            if (r7 == 0) goto L_0x0106
            r7.close()
        L_0x0106:
            throw r0
        L_0x0107:
            r0 = move-exception
            r7 = r1
            goto L_0x0101
        L_0x010a:
            r0 = move-exception
            r1 = r10
            goto L_0x00e7
        L_0x010d:
            r0 = move-exception
            r1 = r7
            goto L_0x00e7
        L_0x0110:
            r0 = move-exception
            goto L_0x0101
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, java.lang.String, java.lang.String):java.util.List");
    }

    @WorkerThread
    public final void zza(zzae zzae) {
        Long l = null;
        Preconditions.checkNotNull(zzae);
        zzo();
        zzbi();
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzae.zzce);
        contentValues.put("name", zzae.name);
        contentValues.put("lifetime_count", Long.valueOf(zzae.zzfg));
        contentValues.put("current_bundle_count", Long.valueOf(zzae.zzfh));
        contentValues.put("last_fire_timestamp", Long.valueOf(zzae.zzfj));
        contentValues.put("last_bundled_timestamp", Long.valueOf(zzae.zzfk));
        contentValues.put("last_bundled_day", zzae.zzfl);
        contentValues.put("last_sampled_complex_event_id", zzae.zzfm);
        contentValues.put("last_sampling_rate", zzae.zzfn);
        if (zzad().zze(zzae.zzce, zzak.zziz)) {
            contentValues.put("current_session_count", Long.valueOf(zzae.zzfi));
        }
        if (zzae.zzfo != null && zzae.zzfo.booleanValue()) {
            l = Long.valueOf(1);
        }
        contentValues.put("last_exempt_from_sampling", l);
        try {
            if (getWritableDatabase().insertWithOnConflict("events", null, contentValues, 5) == -1) {
                zzab().zzgk().zza("Failed to insert/update event aggregates (got -1). appId", zzef.zzam(zzae.zzce));
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing event aggregates. appId", zzef.zzam(zzae.zzce), e);
        }
    }

    @WorkerThread
    public final void zza(zzf zzf) {
        Preconditions.checkNotNull(zzf);
        zzo();
        zzbi();
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzf.zzag());
        contentValues.put("app_instance_id", zzf.getAppInstanceId());
        contentValues.put("gmp_app_id", zzf.getGmpAppId());
        contentValues.put("resettable_device_id_hash", zzf.zzai());
        contentValues.put("last_bundle_index", Long.valueOf(zzf.zzar()));
        contentValues.put("last_bundle_start_timestamp", Long.valueOf(zzf.zzaj()));
        contentValues.put("last_bundle_end_timestamp", Long.valueOf(zzf.zzak()));
        contentValues.put("app_version", zzf.zzal());
        contentValues.put("app_store", zzf.zzan());
        contentValues.put("gmp_version", Long.valueOf(zzf.zzao()));
        contentValues.put("dev_cert_hash", Long.valueOf(zzf.zzap()));
        contentValues.put("measurement_enabled", Boolean.valueOf(zzf.isMeasurementEnabled()));
        contentValues.put("day", Long.valueOf(zzf.zzav()));
        contentValues.put("daily_public_events_count", Long.valueOf(zzf.zzaw()));
        contentValues.put("daily_events_count", Long.valueOf(zzf.zzax()));
        contentValues.put("daily_conversions_count", Long.valueOf(zzf.zzay()));
        contentValues.put("config_fetched_time", Long.valueOf(zzf.zzas()));
        contentValues.put("failed_config_fetch_time", Long.valueOf(zzf.zzat()));
        contentValues.put("app_version_int", Long.valueOf(zzf.zzam()));
        contentValues.put("firebase_instance_id", zzf.getFirebaseInstanceId());
        contentValues.put("daily_error_events_count", Long.valueOf(zzf.zzba()));
        contentValues.put("daily_realtime_events_count", Long.valueOf(zzf.zzaz()));
        contentValues.put("health_monitor_sample", zzf.zzbb());
        contentValues.put("android_id", Long.valueOf(zzf.zzbd()));
        contentValues.put("adid_reporting_enabled", Boolean.valueOf(zzf.zzbe()));
        contentValues.put("ssaid_reporting_enabled", Boolean.valueOf(zzf.zzbf()));
        contentValues.put("admob_app_id", zzf.zzah());
        contentValues.put("dynamite_version", Long.valueOf(zzf.zzaq()));
        if (zzf.zzbh() != null) {
            if (zzf.zzbh().size() == 0) {
                zzab().zzgn().zza("Safelisted events should not be an empty list. appId", zzf.zzag());
            } else {
                contentValues.put("safelisted_events", TextUtils.join(",", zzf.zzbh()));
            }
        }
        try {
            SQLiteDatabase writableDatabase = getWritableDatabase();
            if (((long) writableDatabase.update("apps", contentValues, "app_id = ?", new String[]{zzf.zzag()})) == 0 && writableDatabase.insertWithOnConflict("apps", null, contentValues, 5) == -1) {
                zzab().zzgk().zza("Failed to insert/update app (got -1). appId", zzef.zzam(zzf.zzag()));
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing app. appId", zzef.zzam(zzf.zzag()), e);
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Code restructure failed: missing block: B:21:?, code lost:
        r8 = r2.zzzg;
        r9 = r8.length;
        r1 = 0;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:22:0x00a5, code lost:
        if (r1 >= r9) goto L_0x00c6;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:24:0x00ad, code lost:
        if (r8[r1].zzkb() != false) goto L_0x00c3;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:25:0x00af, code lost:
        zzab().zzgn().zza("Property filter with no ID. Audience definition ignored. appId, audienceId", com.google.android.gms.measurement.internal.zzef.zzam(r13), r2.zzzf);
     */
    /* JADX WARNING: Code restructure failed: missing block: B:26:0x00c3, code lost:
        r1 = r1 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:27:0x00c6, code lost:
        r8 = r2.zzzh;
        r9 = r8.length;
        r1 = 0;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:28:0x00ca, code lost:
        if (r1 >= r9) goto L_0x0141;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:30:0x00d2, code lost:
        if (zza(r13, r7, r8[r1]) != false) goto L_0x011f;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:31:0x00d4, code lost:
        r1 = false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:32:0x00d5, code lost:
        if (r1 == false) goto L_0x00e6;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:33:0x00d7, code lost:
        r8 = r2.zzzg;
        r9 = r8.length;
        r2 = 0;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:34:0x00db, code lost:
        if (r2 >= r9) goto L_0x00e6;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:36:0x00e3, code lost:
        if (zza(r13, r7, r8[r2]) != false) goto L_0x0122;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:37:0x00e5, code lost:
        r1 = false;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:38:0x00e6, code lost:
        if (r1 != false) goto L_0x006d;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:39:0x00e8, code lost:
        zzbi();
        zzo();
        com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r13);
        r1 = getWritableDatabase();
        r1.delete("property_filters", "app_id=? and audience_id=?", new java.lang.String[]{r13, java.lang.String.valueOf(r7)});
        r1.delete("event_filters", "app_id=? and audience_id=?", new java.lang.String[]{r13, java.lang.String.valueOf(r7)});
     */
    /* JADX WARNING: Code restructure failed: missing block: B:40:0x011f, code lost:
        r1 = r1 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:41:0x0122, code lost:
        r2 = r2 + 1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:48:0x0141, code lost:
        r1 = true;
     */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final void zza(java.lang.String r13, com.google.android.gms.internal.measurement.zzbv[] r14) {
        /*
            r12 = this;
            r3 = 1
            r0 = 0
            r12.zzbi()
            r12.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r13)
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r14)
            android.database.sqlite.SQLiteDatabase r5 = r12.getWritableDatabase()
            r5.beginTransaction()
            r12.zzbi()     // Catch:{ all -> 0x0099 }
            r12.zzo()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r13)     // Catch:{ all -> 0x0099 }
            android.database.sqlite.SQLiteDatabase r1 = r12.getWritableDatabase()     // Catch:{ all -> 0x0099 }
            java.lang.String r2 = "property_filters"
            java.lang.String r4 = "app_id=?"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ all -> 0x0099 }
            r7 = 0
            r6[r7] = r13     // Catch:{ all -> 0x0099 }
            r1.delete(r2, r4, r6)     // Catch:{ all -> 0x0099 }
            java.lang.String r2 = "event_filters"
            java.lang.String r4 = "app_id=?"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ all -> 0x0099 }
            r7 = 0
            r6[r7] = r13     // Catch:{ all -> 0x0099 }
            r1.delete(r2, r4, r6)     // Catch:{ all -> 0x0099 }
            int r6 = r14.length     // Catch:{ all -> 0x0099 }
            r4 = r0
        L_0x003e:
            if (r4 >= r6) goto L_0x0125
            r2 = r14[r4]
            r12.zzbi()     // Catch:{ all -> 0x0099 }
            r12.zzo()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r13)     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r2)     // Catch:{ all -> 0x0099 }
            com.google.android.gms.internal.measurement.zzbk$zza[] r1 = r2.zzzh     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r1)     // Catch:{ all -> 0x0099 }
            com.google.android.gms.internal.measurement.zzbk$zzd[] r1 = r2.zzzg     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r1)     // Catch:{ all -> 0x0099 }
            java.lang.Integer r1 = r2.zzzf     // Catch:{ all -> 0x0099 }
            if (r1 != 0) goto L_0x0071
            com.google.android.gms.measurement.internal.zzef r1 = r12.zzab()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()     // Catch:{ all -> 0x0099 }
            java.lang.String r2 = "Audience with no ID. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r13)     // Catch:{ all -> 0x0099 }
            r1.zza(r2, r7)     // Catch:{ all -> 0x0099 }
        L_0x006d:
            int r1 = r4 + 1
            r4 = r1
            goto L_0x003e
        L_0x0071:
            java.lang.Integer r1 = r2.zzzf     // Catch:{ all -> 0x0099 }
            int r7 = r1.intValue()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.internal.measurement.zzbk$zza[] r8 = r2.zzzh     // Catch:{ all -> 0x0099 }
            int r9 = r8.length     // Catch:{ all -> 0x0099 }
            r1 = r0
        L_0x007b:
            if (r1 >= r9) goto L_0x00a1
            r10 = r8[r1]     // Catch:{ all -> 0x0099 }
            boolean r10 = r10.zzkb()     // Catch:{ all -> 0x0099 }
            if (r10 != 0) goto L_0x009e
            com.google.android.gms.measurement.internal.zzef r1 = r12.zzab()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()     // Catch:{ all -> 0x0099 }
            java.lang.String r7 = "Event filter with no ID. Audience definition ignored. appId, audienceId"
            java.lang.Object r8 = com.google.android.gms.measurement.internal.zzef.zzam(r13)     // Catch:{ all -> 0x0099 }
            java.lang.Integer r2 = r2.zzzf     // Catch:{ all -> 0x0099 }
            r1.zza(r7, r8, r2)     // Catch:{ all -> 0x0099 }
            goto L_0x006d
        L_0x0099:
            r0 = move-exception
            r5.endTransaction()
            throw r0
        L_0x009e:
            int r1 = r1 + 1
            goto L_0x007b
        L_0x00a1:
            com.google.android.gms.internal.measurement.zzbk$zzd[] r8 = r2.zzzg     // Catch:{ all -> 0x0099 }
            int r9 = r8.length     // Catch:{ all -> 0x0099 }
            r1 = r0
        L_0x00a5:
            if (r1 >= r9) goto L_0x00c6
            r10 = r8[r1]     // Catch:{ all -> 0x0099 }
            boolean r10 = r10.zzkb()     // Catch:{ all -> 0x0099 }
            if (r10 != 0) goto L_0x00c3
            com.google.android.gms.measurement.internal.zzef r1 = r12.zzab()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgn()     // Catch:{ all -> 0x0099 }
            java.lang.String r7 = "Property filter with no ID. Audience definition ignored. appId, audienceId"
            java.lang.Object r8 = com.google.android.gms.measurement.internal.zzef.zzam(r13)     // Catch:{ all -> 0x0099 }
            java.lang.Integer r2 = r2.zzzf     // Catch:{ all -> 0x0099 }
            r1.zza(r7, r8, r2)     // Catch:{ all -> 0x0099 }
            goto L_0x006d
        L_0x00c3:
            int r1 = r1 + 1
            goto L_0x00a5
        L_0x00c6:
            com.google.android.gms.internal.measurement.zzbk$zza[] r8 = r2.zzzh     // Catch:{ all -> 0x0099 }
            int r9 = r8.length     // Catch:{ all -> 0x0099 }
            r1 = r0
        L_0x00ca:
            if (r1 >= r9) goto L_0x0141
            r10 = r8[r1]     // Catch:{ all -> 0x0099 }
            boolean r10 = r12.zza(r13, r7, r10)     // Catch:{ all -> 0x0099 }
            if (r10 != 0) goto L_0x011f
            r1 = r0
        L_0x00d5:
            if (r1 == 0) goto L_0x00e6
            com.google.android.gms.internal.measurement.zzbk$zzd[] r8 = r2.zzzg     // Catch:{ all -> 0x0099 }
            int r9 = r8.length     // Catch:{ all -> 0x0099 }
            r2 = r0
        L_0x00db:
            if (r2 >= r9) goto L_0x00e6
            r10 = r8[r2]     // Catch:{ all -> 0x0099 }
            boolean r10 = r12.zza(r13, r7, r10)     // Catch:{ all -> 0x0099 }
            if (r10 != 0) goto L_0x0122
            r1 = r0
        L_0x00e6:
            if (r1 != 0) goto L_0x006d
            r12.zzbi()     // Catch:{ all -> 0x0099 }
            r12.zzo()     // Catch:{ all -> 0x0099 }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r13)     // Catch:{ all -> 0x0099 }
            android.database.sqlite.SQLiteDatabase r1 = r12.getWritableDatabase()     // Catch:{ all -> 0x0099 }
            java.lang.String r2 = "property_filters"
            java.lang.String r8 = "app_id=? and audience_id=?"
            r9 = 2
            java.lang.String[] r9 = new java.lang.String[r9]     // Catch:{ all -> 0x0099 }
            r10 = 0
            r9[r10] = r13     // Catch:{ all -> 0x0099 }
            r10 = 1
            java.lang.String r11 = java.lang.String.valueOf(r7)     // Catch:{ all -> 0x0099 }
            r9[r10] = r11     // Catch:{ all -> 0x0099 }
            r1.delete(r2, r8, r9)     // Catch:{ all -> 0x0099 }
            java.lang.String r2 = "event_filters"
            java.lang.String r8 = "app_id=? and audience_id=?"
            r9 = 2
            java.lang.String[] r9 = new java.lang.String[r9]     // Catch:{ all -> 0x0099 }
            r10 = 0
            r9[r10] = r13     // Catch:{ all -> 0x0099 }
            r10 = 1
            java.lang.String r7 = java.lang.String.valueOf(r7)     // Catch:{ all -> 0x0099 }
            r9[r10] = r7     // Catch:{ all -> 0x0099 }
            r1.delete(r2, r8, r9)     // Catch:{ all -> 0x0099 }
            goto L_0x006d
        L_0x011f:
            int r1 = r1 + 1
            goto L_0x00ca
        L_0x0122:
            int r2 = r2 + 1
            goto L_0x00db
        L_0x0125:
            java.util.ArrayList r1 = new java.util.ArrayList     // Catch:{ all -> 0x0099 }
            r1.<init>()     // Catch:{ all -> 0x0099 }
            int r2 = r14.length     // Catch:{ all -> 0x0099 }
        L_0x012b:
            if (r0 >= r2) goto L_0x0137
            r3 = r14[r0]     // Catch:{ all -> 0x0099 }
            java.lang.Integer r3 = r3.zzzf     // Catch:{ all -> 0x0099 }
            r1.add(r3)     // Catch:{ all -> 0x0099 }
            int r0 = r0 + 1
            goto L_0x012b
        L_0x0137:
            r12.zza(r13, r1)     // Catch:{ all -> 0x0099 }
            r5.setTransactionSuccessful()     // Catch:{ all -> 0x0099 }
            r5.endTransaction()
            return
        L_0x0141:
            r1 = r3
            goto L_0x00d5
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zza(java.lang.String, com.google.android.gms.internal.measurement.zzbv[]):void");
    }

    @WorkerThread
    public final boolean zza(zzg zzg, boolean z) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(zzg);
        Preconditions.checkNotEmpty(zzg.zzag());
        Preconditions.checkState(zzg.zzof());
        zzca();
        long currentTimeMillis = zzx().currentTimeMillis();
        if (zzg.zznr() < currentTimeMillis - zzs.zzbs() || zzg.zznr() > zzs.zzbs() + currentTimeMillis) {
            zzab().zzgn().zza("Storing bundle outside of the max uploading time span. appId, now, timestamp", zzef.zzam(zzg.zzag()), Long.valueOf(currentTimeMillis), Long.valueOf(zzg.zznr()));
        }
        try {
            byte[] zzc = zzgw().zzc(zzg.toByteArray());
            zzab().zzgs().zza("Saving bundle, size", Integer.valueOf(zzc.length));
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", zzg.zzag());
            contentValues.put("bundle_end_timestamp", Long.valueOf(zzg.zznr()));
            contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, zzc);
            contentValues.put("has_realtime", Integer.valueOf(z ? 1 : 0));
            if (zzg.zzpn()) {
                contentValues.put("retry_count", Integer.valueOf(zzg.zzpo()));
            }
            try {
                if (getWritableDatabase().insert("queue", null, contentValues) != -1) {
                    return true;
                }
                zzab().zzgk().zza("Failed to insert bundle (got -1). appId", zzef.zzam(zzg.zzag()));
                return false;
            } catch (SQLiteException e) {
                zzab().zzgk().zza("Error storing bundle. appId", zzef.zzam(zzg.zzag()), e);
                return false;
            }
        } catch (IOException e2) {
            zzab().zzgk().zza("Data loss. Failed to serialize bundle. appId", zzef.zzam(zzg.zzag()), e2);
            return false;
        }
    }

    public final boolean zza(zzaf zzaf, long j, boolean z) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(zzaf);
        Preconditions.checkNotEmpty(zzaf.zzce);
        zza zzah = zzc.zzmq().zzah(zzaf.zzfp);
        Iterator it = zzaf.zzfq.iterator();
        while (it.hasNext()) {
            String str = (String) it.next();
            zze.zza zzbz = zze.zzng().zzbz(str);
            zzgw().zza(zzbz, zzaf.zzfq.get(str));
            zzah.zza(zzbz);
        }
        byte[] byteArray = ((zzc) ((zzey) zzah.zzug())).toByteArray();
        zzab().zzgs().zza("Saving event, name, data size", zzy().zzaj(zzaf.name), Integer.valueOf(byteArray.length));
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzaf.zzce);
        contentValues.put("name", zzaf.name);
        contentValues.put("timestamp", Long.valueOf(zzaf.timestamp));
        contentValues.put("metadata_fingerprint", Long.valueOf(j));
        contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, byteArray);
        contentValues.put("realtime", Integer.valueOf(z ? 1 : 0));
        try {
            if (getWritableDatabase().insert("raw_events", null, contentValues) != -1) {
                return true;
            }
            zzab().zzgk().zza("Failed to insert raw event (got -1). appId", zzef.zzam(zzaf.zzce));
            return false;
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing raw event. appId", zzef.zzam(zzaf.zzce), e);
            return false;
        }
    }

    @WorkerThread
    public final boolean zza(zzjp zzjp) {
        Preconditions.checkNotNull(zzjp);
        zzo();
        zzbi();
        if (zze(zzjp.zzce, zzjp.name) == null) {
            if (zzjs.zzbk(zzjp.name)) {
                if (zza("select count(1) from user_attributes where app_id=? and name not like '!_%' escape '!'", new String[]{zzjp.zzce}) >= 25) {
                    return false;
                }
            } else if (!zzad().zze(zzjp.zzce, zzak.zzij)) {
                if (zza("select count(1) from user_attributes where app_id=? and origin=? AND name like '!_%' escape '!'", new String[]{zzjp.zzce, zzjp.origin}) >= 25) {
                    return false;
                }
            } else if (!"_npa".equals(zzjp.name)) {
                if (zza("select count(1) from user_attributes where app_id=? and origin=? AND name like '!_%' escape '!'", new String[]{zzjp.zzce, zzjp.origin}) >= 25) {
                    return false;
                }
            }
        }
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzjp.zzce);
        contentValues.put("origin", zzjp.origin);
        contentValues.put("name", zzjp.name);
        contentValues.put("set_timestamp", Long.valueOf(zzjp.zztr));
        zza(contentValues, "value", zzjp.value);
        try {
            if (getWritableDatabase().insertWithOnConflict("user_attributes", null, contentValues, 5) == -1) {
                zzab().zzgk().zza("Failed to insert/update user property (got -1). appId", zzef.zzam(zzjp.zzce));
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing user property. appId", zzef.zzam(zzjp.zzce), e);
        }
        return true;
    }

    @WorkerThread
    public final boolean zza(zzq zzq) {
        Preconditions.checkNotNull(zzq);
        zzo();
        zzbi();
        if (zze(zzq.packageName, zzq.zzdw.name) == null) {
            if (zza("SELECT COUNT(1) FROM conditional_properties WHERE app_id=?", new String[]{zzq.packageName}) >= 1000) {
                return false;
            }
        }
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzq.packageName);
        contentValues.put("origin", zzq.origin);
        contentValues.put("name", zzq.zzdw.name);
        zza(contentValues, "value", zzq.zzdw.getValue());
        contentValues.put(ConditionalUserProperty.ACTIVE, Boolean.valueOf(zzq.active));
        contentValues.put(ConditionalUserProperty.TRIGGER_EVENT_NAME, zzq.triggerEventName);
        contentValues.put(ConditionalUserProperty.TRIGGER_TIMEOUT, Long.valueOf(zzq.triggerTimeout));
        zzz();
        contentValues.put("timed_out_event", zzjs.zza((Parcelable) zzq.zzdx));
        contentValues.put(ConditionalUserProperty.CREATION_TIMESTAMP, Long.valueOf(zzq.creationTimestamp));
        zzz();
        contentValues.put("triggered_event", zzjs.zza((Parcelable) zzq.zzdy));
        contentValues.put(ConditionalUserProperty.TRIGGERED_TIMESTAMP, Long.valueOf(zzq.zzdw.zztr));
        contentValues.put(ConditionalUserProperty.TIME_TO_LIVE, Long.valueOf(zzq.timeToLive));
        zzz();
        contentValues.put("expired_event", zzjs.zza((Parcelable) zzq.zzdz));
        try {
            if (getWritableDatabase().insertWithOnConflict("conditional_properties", null, contentValues, 5) == -1) {
                zzab().zzgk().zza("Failed to insert/update conditional user property (got -1)", zzef.zzam(zzq.packageName));
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing conditional user property", zzef.zzam(zzq.packageName), e);
        }
        return true;
    }

    public final boolean zza(String str, Long l, long j, zzc zzc) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(zzc);
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotNull(l);
        byte[] byteArray = zzc.toByteArray();
        zzab().zzgs().zza("Saving complex main event, appId, data size", zzy().zzaj(str), Integer.valueOf(byteArray.length));
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", str);
        contentValues.put("event_id", l);
        contentValues.put("children_to_process", Long.valueOf(j));
        contentValues.put("main_event", byteArray);
        try {
            if (getWritableDatabase().insertWithOnConflict("main_event_params", null, contentValues, 5) != -1) {
                return true;
            }
            zzab().zzgk().zza("Failed to insert complex main event (got -1). appId", zzef.zzam(str));
            return false;
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error storing complex main event. appId", zzef.zzam(str), e);
            return false;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:36:0x00ab  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<com.google.android.gms.measurement.internal.zzjp> zzaa(java.lang.String r12) {
        /*
            r11 = this;
            r10 = 0
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            r11.zzo()
            r11.zzbi()
            java.util.ArrayList r9 = new java.util.ArrayList
            r9.<init>()
            android.database.sqlite.SQLiteDatabase r0 = r11.getWritableDatabase()     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            java.lang.String r1 = "user_attributes"
            r2 = 4
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r3 = 0
            java.lang.String r4 = "name"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r3 = 1
            java.lang.String r4 = "origin"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r3 = 2
            java.lang.String r4 = "set_timestamp"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r3 = 3
            java.lang.String r4 = "value"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r5 = 0
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            r5 = 0
            r6 = 0
            java.lang.String r7 = "rowid"
            java.lang.String r8 = "1000"
            android.database.Cursor r7 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteException -> 0x00b2, all -> 0x00b5 }
            boolean r0 = r7.moveToFirst()     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            if (r0 != 0) goto L_0x004b
            if (r7 == 0) goto L_0x0049
            r7.close()
        L_0x0049:
            r0 = r9
        L_0x004a:
            return r0
        L_0x004b:
            r0 = 0
            java.lang.String r3 = r7.getString(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            r0 = 1
            java.lang.String r2 = r7.getString(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            if (r2 != 0) goto L_0x0059
            java.lang.String r2 = ""
        L_0x0059:
            r0 = 2
            long r4 = r7.getLong(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            r0 = 3
            java.lang.Object r6 = r11.zza(r7, r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            if (r6 != 0) goto L_0x0083
            com.google.android.gms.measurement.internal.zzef r0 = r11.zzab()     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            java.lang.String r1 = "Read invalid user property value, ignoring it. appId"
            java.lang.Object r2 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            r0.zza(r1, r2)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
        L_0x0076:
            boolean r0 = r7.moveToNext()     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            if (r0 != 0) goto L_0x004b
            if (r7 == 0) goto L_0x0081
            r7.close()
        L_0x0081:
            r0 = r9
            goto L_0x004a
        L_0x0083:
            com.google.android.gms.measurement.internal.zzjp r0 = new com.google.android.gms.measurement.internal.zzjp     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            r1 = r12
            r0.<init>(r1, r2, r3, r4, r6)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            r9.add(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x00a7 }
            goto L_0x0076
        L_0x008d:
            r0 = move-exception
            r1 = r7
        L_0x008f:
            com.google.android.gms.measurement.internal.zzef r2 = r11.zzab()     // Catch:{ all -> 0x00af }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x00af }
            java.lang.String r3 = "Error querying user properties. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ all -> 0x00af }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x00af }
            if (r1 == 0) goto L_0x00a5
            r1.close()
        L_0x00a5:
            r0 = r10
            goto L_0x004a
        L_0x00a7:
            r0 = move-exception
            r10 = r7
        L_0x00a9:
            if (r10 == 0) goto L_0x00ae
            r10.close()
        L_0x00ae:
            throw r0
        L_0x00af:
            r0 = move-exception
            r10 = r1
            goto L_0x00a9
        L_0x00b2:
            r0 = move-exception
            r1 = r10
            goto L_0x008f
        L_0x00b5:
            r0 = move-exception
            goto L_0x00a9
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzaa(java.lang.String):java.util.List");
    }

    /* JADX WARNING: Removed duplicated region for block: B:59:0x0270  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzf zzab(java.lang.String r12) {
        /*
            r11 = this;
            r9 = 1
            r8 = 0
            r10 = 0
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            r11.zzo()
            r11.zzbi()
            android.database.sqlite.SQLiteDatabase r0 = r11.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            java.lang.String r1 = "apps"
            r2 = 28
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 0
            java.lang.String r4 = "app_instance_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 1
            java.lang.String r4 = "gmp_app_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 2
            java.lang.String r4 = "resettable_device_id_hash"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 3
            java.lang.String r4 = "last_bundle_index"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 4
            java.lang.String r4 = "last_bundle_start_timestamp"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 5
            java.lang.String r4 = "last_bundle_end_timestamp"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 6
            java.lang.String r4 = "app_version"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 7
            java.lang.String r4 = "app_store"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 8
            java.lang.String r4 = "gmp_version"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 9
            java.lang.String r4 = "dev_cert_hash"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 10
            java.lang.String r4 = "measurement_enabled"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 11
            java.lang.String r4 = "day"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 12
            java.lang.String r4 = "daily_public_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 13
            java.lang.String r4 = "daily_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 14
            java.lang.String r4 = "daily_conversions_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 15
            java.lang.String r4 = "config_fetched_time"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 16
            java.lang.String r4 = "failed_config_fetch_time"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 17
            java.lang.String r4 = "app_version_int"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 18
            java.lang.String r4 = "firebase_instance_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 19
            java.lang.String r4 = "daily_error_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 20
            java.lang.String r4 = "daily_realtime_events_count"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 21
            java.lang.String r4 = "health_monitor_sample"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 22
            java.lang.String r4 = "android_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 23
            java.lang.String r4 = "adid_reporting_enabled"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 24
            java.lang.String r4 = "ssaid_reporting_enabled"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 25
            java.lang.String r4 = "admob_app_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 26
            java.lang.String r4 = "dynamite_version"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r3 = 27
            java.lang.String r4 = "safelisted_events"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r5 = 0
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r3 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0251, all -> 0x027a }
            boolean r0 = r3.moveToFirst()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 != 0) goto L_0x00d2
            if (r3 == 0) goto L_0x00d0
            r3.close()
        L_0x00d0:
            r0 = r10
        L_0x00d1:
            return r0
        L_0x00d2:
            com.google.android.gms.measurement.internal.zzf r2 = new com.google.android.gms.measurement.internal.zzf     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            com.google.android.gms.measurement.internal.zzjg r0 = r11.zzkz     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            com.google.android.gms.measurement.internal.zzfj r0 = r0.zzjt()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.<init>(r0, r12)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 0
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zza(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 1
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzb(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 2
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzd(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 3
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzk(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 4
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zze(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 5
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzf(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 6
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzf(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 7
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzg(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 8
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzh(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 9
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzi(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 10
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 != 0) goto L_0x013f
            r0 = 10
            int r0 = r3.getInt(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x0234
        L_0x013f:
            r0 = r9
        L_0x0140:
            r2.setMeasurementEnabled(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 11
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzn(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 12
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzo(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 13
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzp(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 14
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzq(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 15
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzl(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 16
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzm(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 17
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x0237
            r0 = -2147483648(0xffffffff80000000, double:NaN)
        L_0x0184:
            r2.zzg(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 18
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zze(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 19
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzs(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 20
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzr(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 21
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzh(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 22
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x0240
            r0 = 0
        L_0x01b5:
            r2.zzt(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 23
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 != 0) goto L_0x01c8
            r0 = 23
            int r0 = r3.getInt(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x0248
        L_0x01c8:
            r0 = r9
        L_0x01c9:
            r2.zzb(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 24
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 != 0) goto L_0x01dc
            r0 = 24
            int r0 = r3.getInt(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x01dd
        L_0x01dc:
            r8 = r9
        L_0x01dd:
            r2.zzc(r8)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 25
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zzc(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 26
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x024a
            r0 = 0
        L_0x01f3:
            r2.zzj(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0 = 27
            boolean r0 = r3.isNull(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 != 0) goto L_0x0212
            r0 = 27
            java.lang.String r0 = r3.getString(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            java.lang.String r1 = ","
            r4 = -1
            java.lang.String[] r0 = r0.split(r1, r4)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            java.util.List r0 = java.util.Arrays.asList(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r2.zza(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
        L_0x0212:
            r2.zzaf()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            boolean r0 = r3.moveToNext()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            if (r0 == 0) goto L_0x022c
            com.google.android.gms.measurement.internal.zzef r0 = r11.zzab()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            com.google.android.gms.measurement.internal.zzeh r0 = r0.zzgk()     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            java.lang.String r1 = "Got multiple records for app, expected one. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            r0.zza(r1, r4)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
        L_0x022c:
            if (r3 == 0) goto L_0x027c
            r3.close()
            r0 = r2
            goto L_0x00d1
        L_0x0234:
            r0 = r8
            goto L_0x0140
        L_0x0237:
            r0 = 17
            int r0 = r3.getInt(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            long r0 = (long) r0     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            goto L_0x0184
        L_0x0240:
            r0 = 22
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            goto L_0x01b5
        L_0x0248:
            r0 = r8
            goto L_0x01c9
        L_0x024a:
            r0 = 26
            long r0 = r3.getLong(r0)     // Catch:{ SQLiteException -> 0x0274, all -> 0x026c }
            goto L_0x01f3
        L_0x0251:
            r0 = move-exception
            r1 = r10
        L_0x0253:
            com.google.android.gms.measurement.internal.zzef r2 = r11.zzab()     // Catch:{ all -> 0x0277 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x0277 }
            java.lang.String r3 = "Error querying app. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r12)     // Catch:{ all -> 0x0277 }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x0277 }
            if (r1 == 0) goto L_0x0269
            r1.close()
        L_0x0269:
            r0 = r10
            goto L_0x00d1
        L_0x026c:
            r0 = move-exception
        L_0x026d:
            r10 = r3
        L_0x026e:
            if (r10 == 0) goto L_0x0273
            r10.close()
        L_0x0273:
            throw r0
        L_0x0274:
            r0 = move-exception
            r1 = r3
            goto L_0x0253
        L_0x0277:
            r0 = move-exception
            r3 = r1
            goto L_0x026d
        L_0x027a:
            r0 = move-exception
            goto L_0x026e
        L_0x027c:
            r0 = r2
            goto L_0x00d1
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzab(java.lang.String):com.google.android.gms.measurement.internal.zzf");
    }

    public final long zzac(String str) {
        Preconditions.checkNotEmpty(str);
        zzo();
        zzbi();
        try {
            return (long) getWritableDatabase().delete("raw_events", "rowid in (select rowid from raw_events where app_id=? order by rowid desc limit -1 offset ?)", new String[]{str, String.valueOf(Math.max(0, Math.min(1000000, zzad().zzb(str, zzak.zzgu))))});
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error deleting over the limit events. appId", zzef.zzam(str), e);
            return 0;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:26:0x0074  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final byte[] zzad(java.lang.String r10) {
        /*
            r9 = this;
            r8 = 0
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r10)
            r9.zzo()
            r9.zzbi()
            android.database.sqlite.SQLiteDatabase r0 = r9.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            java.lang.String r1 = "apps"
            r2 = 1
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            r3 = 0
            java.lang.String r4 = "remote_config"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            r5 = 0
            r4[r5] = r10     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0056, all -> 0x007a }
            boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x0078 }
            if (r0 != 0) goto L_0x0034
            if (r1 == 0) goto L_0x0032
            r1.close()
        L_0x0032:
            r0 = r8
        L_0x0033:
            return r0
        L_0x0034:
            r0 = 0
            byte[] r0 = r1.getBlob(r0)     // Catch:{ SQLiteException -> 0x0078 }
            boolean r2 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0078 }
            if (r2 == 0) goto L_0x0050
            com.google.android.gms.measurement.internal.zzef r2 = r9.zzab()     // Catch:{ SQLiteException -> 0x0078 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0078 }
            java.lang.String r3 = "Got multiple records for app config, expected one. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ SQLiteException -> 0x0078 }
            r2.zza(r3, r4)     // Catch:{ SQLiteException -> 0x0078 }
        L_0x0050:
            if (r1 == 0) goto L_0x0033
            r1.close()
            goto L_0x0033
        L_0x0056:
            r0 = move-exception
            r1 = r8
        L_0x0058:
            com.google.android.gms.measurement.internal.zzef r2 = r9.zzab()     // Catch:{ all -> 0x0070 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x0070 }
            java.lang.String r3 = "Error querying remote config. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ all -> 0x0070 }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x0070 }
            if (r1 == 0) goto L_0x006e
            r1.close()
        L_0x006e:
            r0 = r8
            goto L_0x0033
        L_0x0070:
            r0 = move-exception
            r8 = r1
        L_0x0072:
            if (r8 == 0) goto L_0x0077
            r8.close()
        L_0x0077:
            throw r0
        L_0x0078:
            r0 = move-exception
            goto L_0x0058
        L_0x007a:
            r0 = move-exception
            goto L_0x0072
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzad(java.lang.String):byte[]");
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:29:0x0086  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.Map<java.lang.Integer, java.util.List<java.lang.Integer>> zzae(java.lang.String r7) {
        /*
            r6 = this;
            r2 = 0
            r6.zzbi()
            r6.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r7)
            android.support.v4.util.ArrayMap r1 = new android.support.v4.util.ArrayMap
            r1.<init>()
            android.database.sqlite.SQLiteDatabase r0 = r6.getWritableDatabase()
            java.lang.String r3 = "select audience_id, filter_id from event_filters where app_id = ? and session_scoped = 1 UNION select audience_id, filter_id from property_filters where app_id = ? and session_scoped = 1;"
            r4 = 2
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0068, all -> 0x0082 }
            r5 = 0
            r4[r5] = r7     // Catch:{ SQLiteException -> 0x0068, all -> 0x0082 }
            r5 = 1
            r4[r5] = r7     // Catch:{ SQLiteException -> 0x0068, all -> 0x0082 }
            android.database.Cursor r3 = r0.rawQuery(r3, r4)     // Catch:{ SQLiteException -> 0x0068, all -> 0x0082 }
            boolean r0 = r3.moveToFirst()     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            if (r0 != 0) goto L_0x0032
            java.util.Map r0 = java.util.Collections.emptyMap()     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            if (r3 == 0) goto L_0x0031
            r3.close()
        L_0x0031:
            return r0
        L_0x0032:
            r0 = 0
            int r4 = r3.getInt(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            java.lang.Integer r0 = java.lang.Integer.valueOf(r4)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            java.lang.Object r0 = r1.get(r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            java.util.List r0 = (java.util.List) r0     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            if (r0 != 0) goto L_0x004f
            java.util.ArrayList r0 = new java.util.ArrayList     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            r1.put(r4, r0)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
        L_0x004f:
            r4 = 1
            int r4 = r3.getInt(r4)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            r0.add(r4)     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            boolean r0 = r3.moveToNext()     // Catch:{ SQLiteException -> 0x008d, all -> 0x0090 }
            if (r0 != 0) goto L_0x0032
            if (r3 == 0) goto L_0x0066
            r3.close()
        L_0x0066:
            r0 = r1
            goto L_0x0031
        L_0x0068:
            r0 = move-exception
            r1 = r2
        L_0x006a:
            com.google.android.gms.measurement.internal.zzef r3 = r6.zzab()     // Catch:{ all -> 0x008a }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ all -> 0x008a }
            java.lang.String r4 = "Database error querying scoped filters. appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r7)     // Catch:{ all -> 0x008a }
            r3.zza(r4, r5, r0)     // Catch:{ all -> 0x008a }
            if (r1 == 0) goto L_0x0080
            r1.close()
        L_0x0080:
            r0 = r2
            goto L_0x0031
        L_0x0082:
            r0 = move-exception
            r3 = r2
        L_0x0084:
            if (r3 == 0) goto L_0x0089
            r3.close()
        L_0x0089:
            throw r0
        L_0x008a:
            r0 = move-exception
            r3 = r1
            goto L_0x0084
        L_0x008d:
            r0 = move-exception
            r1 = r3
            goto L_0x006a
        L_0x0090:
            r0 = move-exception
            goto L_0x0084
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzae(java.lang.String):java.util.Map");
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:33:0x0098  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.Map<java.lang.Integer, com.google.android.gms.internal.measurement.zzbs.zzi> zzaf(java.lang.String r10) {
        /*
            r9 = this;
            r8 = 0
            r9.zzbi()
            r9.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r10)
            android.database.sqlite.SQLiteDatabase r0 = r9.getWritableDatabase()
            java.lang.String r1 = "audience_filter_values"
            r2 = 2
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            r3 = 0
            java.lang.String r4 = "audience_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            r3 = 1
            java.lang.String r4 = "current_results"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            java.lang.String r3 = "app_id=?"
            r4 = 1
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            r5 = 0
            r4[r5] = r10     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r2 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x009f, all -> 0x00a2 }
            boolean r0 = r2.moveToFirst()     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            if (r0 != 0) goto L_0x0039
            if (r2 == 0) goto L_0x0037
            r2.close()
        L_0x0037:
            r0 = r8
        L_0x0038:
            return r0
        L_0x0039:
            android.support.v4.util.ArrayMap r0 = new android.support.v4.util.ArrayMap     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
        L_0x003e:
            r1 = 0
            int r3 = r2.getInt(r1)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            r1 = 1
            byte[] r1 = r2.getBlob(r1)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            com.google.android.gms.internal.measurement.zzel r4 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x0063 }
            com.google.android.gms.internal.measurement.zzbs$zzi r1 = com.google.android.gms.internal.measurement.zzbs.zzi.zze(r1, r4)     // Catch:{ IOException -> 0x0063 }
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            r0.put(r3, r1)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
        L_0x0057:
            boolean r1 = r2.moveToNext()     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            if (r1 != 0) goto L_0x003e
            if (r2 == 0) goto L_0x0038
            r2.close()
            goto L_0x0038
        L_0x0063:
            r1 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r9.zzab()     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            java.lang.String r5 = "Failed to merge filter results. appId, audienceId, error"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            r4.zza(r5, r6, r3, r1)     // Catch:{ SQLiteException -> 0x007a, all -> 0x0094 }
            goto L_0x0057
        L_0x007a:
            r0 = move-exception
            r1 = r2
        L_0x007c:
            com.google.android.gms.measurement.internal.zzef r2 = r9.zzab()     // Catch:{ all -> 0x009c }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x009c }
            java.lang.String r3 = "Database error querying filter results. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ all -> 0x009c }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x009c }
            if (r1 == 0) goto L_0x0092
            r1.close()
        L_0x0092:
            r0 = r8
            goto L_0x0038
        L_0x0094:
            r0 = move-exception
            r8 = r2
        L_0x0096:
            if (r8 == 0) goto L_0x009b
            r8.close()
        L_0x009b:
            throw r0
        L_0x009c:
            r0 = move-exception
            r8 = r1
            goto L_0x0096
        L_0x009f:
            r0 = move-exception
            r1 = r8
            goto L_0x007c
        L_0x00a2:
            r0 = move-exception
            goto L_0x0096
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzaf(java.lang.String):java.util.Map");
    }

    public final long zzag(String str) {
        Preconditions.checkNotEmpty(str);
        return zza("select count(1) from events where app_id=? and name not like '!_%' escape '!'", new String[]{str}, 0);
    }

    @WorkerThread
    public final List<zzq> zzb(String str, String str2, String str3) {
        Preconditions.checkNotEmpty(str);
        zzo();
        zzbi();
        ArrayList arrayList = new ArrayList(3);
        arrayList.add(str);
        StringBuilder sb = new StringBuilder("app_id=?");
        if (!TextUtils.isEmpty(str2)) {
            arrayList.add(str2);
            sb.append(" and origin=?");
        }
        if (!TextUtils.isEmpty(str3)) {
            arrayList.add(String.valueOf(str3).concat("*"));
            sb.append(" and name glob ?");
        }
        return zzb(sb.toString(), (String[]) arrayList.toArray(new String[arrayList.size()]));
    }

    /* JADX WARNING: Removed duplicated region for block: B:33:0x0164  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.List<com.google.android.gms.measurement.internal.zzq> zzb(java.lang.String r24, java.lang.String[] r25) {
        /*
            r23 = this;
            r23.zzo()
            r23.zzbi()
            java.util.ArrayList r20 = new java.util.ArrayList
            r20.<init>()
            android.database.sqlite.SQLiteDatabase r2 = r23.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            java.lang.String r3 = "conditional_properties"
            r4 = 13
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 0
            java.lang.String r6 = "app_id"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 1
            java.lang.String r6 = "origin"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 2
            java.lang.String r6 = "name"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 3
            java.lang.String r6 = "value"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 4
            java.lang.String r6 = "active"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 5
            java.lang.String r6 = "trigger_event_name"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 6
            java.lang.String r6 = "trigger_timeout"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 7
            java.lang.String r6 = "timed_out_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 8
            java.lang.String r6 = "creation_timestamp"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 9
            java.lang.String r6 = "triggered_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 10
            java.lang.String r6 = "triggered_timestamp"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 11
            java.lang.String r6 = "time_to_live"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r5 = 12
            java.lang.String r6 = "expired_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            r7 = 0
            r8 = 0
            java.lang.String r9 = "rowid"
            java.lang.String r10 = "1001"
            r5 = r24
            r6 = r25
            android.database.Cursor r21 = r2.query(r3, r4, r5, r6, r7, r8, r9, r10)     // Catch:{ SQLiteException -> 0x0145, all -> 0x015f }
            boolean r2 = r21.moveToFirst()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            if (r2 != 0) goto L_0x0077
            if (r21 == 0) goto L_0x0074
            r21.close()
        L_0x0074:
            r2 = r20
        L_0x0076:
            return r2
        L_0x0077:
            int r2 = r20.size()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r3 = 1000(0x3e8, float:1.401E-42)
            if (r2 < r3) goto L_0x009a
            com.google.android.gms.measurement.internal.zzef r2 = r23.zzab()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            java.lang.String r3 = "Read more than the max allowed conditional properties, ignoring extra"
            r4 = 1000(0x3e8, float:1.401E-42)
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2.zza(r3, r4)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
        L_0x0092:
            if (r21 == 0) goto L_0x0097
            r21.close()
        L_0x0097:
            r2 = r20
            goto L_0x0076
        L_0x009a:
            r2 = 0
            r0 = r21
            java.lang.String r8 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 1
            r0 = r21
            java.lang.String r7 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 2
            r0 = r21
            java.lang.String r3 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 3
            r0 = r23
            r1 = r21
            java.lang.Object r6 = r0.zza(r1, r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 4
            r0 = r21
            int r2 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            if (r2 == 0) goto L_0x0142
            r11 = 1
        L_0x00c2:
            r2 = 5
            r0 = r21
            java.lang.String r12 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 6
            r0 = r21
            long r14 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzjo r2 = r23.zzgw()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r4 = 7
            r0 = r21
            byte[] r4 = r0.getBlob(r4)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r5 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            android.os.Parcelable r13 = r2.zza(r4, r5)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzai r13 = (com.google.android.gms.measurement.internal.zzai) r13     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 8
            r0 = r21
            long r9 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzjo r2 = r23.zzgw()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r4 = 9
            r0 = r21
            byte[] r4 = r0.getBlob(r4)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r5 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            android.os.Parcelable r16 = r2.zza(r4, r5)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzai r16 = (com.google.android.gms.measurement.internal.zzai) r16     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 10
            r0 = r21
            long r4 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2 = 11
            r0 = r21
            long r17 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzjo r2 = r23.zzgw()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r19 = 12
            r0 = r21
            r1 = r19
            byte[] r19 = r0.getBlob(r1)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r22 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r0 = r19
            r1 = r22
            android.os.Parcelable r19 = r2.zza(r0, r1)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzai r19 = (com.google.android.gms.measurement.internal.zzai) r19     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzjn r2 = new com.google.android.gms.measurement.internal.zzjn     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r2.<init>(r3, r4, r6, r7)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            com.google.android.gms.measurement.internal.zzq r5 = new com.google.android.gms.measurement.internal.zzq     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r6 = r8
            r8 = r2
            r5.<init>(r6, r7, r8, r9, r11, r12, r13, r14, r16, r17, r19)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            r0 = r20
            r0.add(r5)     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            boolean r2 = r21.moveToNext()     // Catch:{ SQLiteException -> 0x016c, all -> 0x0170 }
            if (r2 != 0) goto L_0x0077
            goto L_0x0092
        L_0x0142:
            r11 = 0
            goto L_0x00c2
        L_0x0145:
            r2 = move-exception
            r3 = 0
        L_0x0147:
            com.google.android.gms.measurement.internal.zzef r4 = r23.zzab()     // Catch:{ all -> 0x0168 }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x0168 }
            java.lang.String r5 = "Error querying conditional user property value"
            r4.zza(r5, r2)     // Catch:{ all -> 0x0168 }
            java.util.List r2 = java.util.Collections.emptyList()     // Catch:{ all -> 0x0168 }
            if (r3 == 0) goto L_0x0076
            r3.close()
            goto L_0x0076
        L_0x015f:
            r2 = move-exception
            r21 = 0
        L_0x0162:
            if (r21 == 0) goto L_0x0167
            r21.close()
        L_0x0167:
            throw r2
        L_0x0168:
            r2 = move-exception
            r21 = r3
            goto L_0x0162
        L_0x016c:
            r2 = move-exception
            r3 = r21
            goto L_0x0147
        L_0x0170:
            r2 = move-exception
            goto L_0x0162
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzb(java.lang.String, java.lang.String[]):java.util.List");
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    @VisibleForTesting
    public final void zzb(List<Long> list) {
        zzo();
        zzbi();
        Preconditions.checkNotNull(list);
        Preconditions.checkNotZero(list.size());
        if (zzcg()) {
            String join = TextUtils.join(",", list);
            String sb = new StringBuilder(String.valueOf(join).length() + 2).append("(").append(join).append(")").toString();
            if (zza(new StringBuilder(String.valueOf(sb).length() + 80).append("SELECT COUNT(1) FROM queue WHERE rowid IN ").append(sb).append(" AND retry_count =  2147483647 LIMIT 1").toString(), (String[]) null) > 0) {
                zzab().zzgn().zzao("The number of upload retries exceeds the limit. Will remain unchanged.");
            }
            try {
                getWritableDatabase().execSQL(new StringBuilder(String.valueOf(sb).length() + Notifications.NOTIFICATION_TYPES_ALL).append("UPDATE queue SET retry_count = IFNULL(retry_count, 0) + 1 WHERE rowid IN ").append(sb).append(" AND (retry_count IS NULL OR retry_count < 2147483647)").toString());
            } catch (SQLiteException e) {
                zzab().zzgk().zza("Error incrementing retry count. error", e);
            }
        }
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    /* JADX WARNING: Removed duplicated region for block: B:21:0x003c  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.lang.String zzby() {
        /*
            r5 = this;
            r0 = 0
            android.database.sqlite.SQLiteDatabase r1 = r5.getWritableDatabase()
            java.lang.String r2 = "select app_id from queue order by has_realtime desc, rowid asc limit 1;"
            r3 = 0
            android.database.Cursor r2 = r1.rawQuery(r2, r3)     // Catch:{ SQLiteException -> 0x0023, all -> 0x0038 }
            boolean r1 = r2.moveToFirst()     // Catch:{ SQLiteException -> 0x0043 }
            if (r1 == 0) goto L_0x001d
            r1 = 0
            java.lang.String r0 = r2.getString(r1)     // Catch:{ SQLiteException -> 0x0043 }
            if (r2 == 0) goto L_0x001c
            r2.close()
        L_0x001c:
            return r0
        L_0x001d:
            if (r2 == 0) goto L_0x001c
            r2.close()
            goto L_0x001c
        L_0x0023:
            r1 = move-exception
            r2 = r0
        L_0x0025:
            com.google.android.gms.measurement.internal.zzef r3 = r5.zzab()     // Catch:{ all -> 0x0040 }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ all -> 0x0040 }
            java.lang.String r4 = "Database error getting next bundle app id"
            r3.zza(r4, r1)     // Catch:{ all -> 0x0040 }
            if (r2 == 0) goto L_0x001c
            r2.close()
            goto L_0x001c
        L_0x0038:
            r1 = move-exception
            r2 = r0
        L_0x003a:
            if (r2 == 0) goto L_0x003f
            r2.close()
        L_0x003f:
            throw r1
        L_0x0040:
            r0 = move-exception
            r1 = r0
            goto L_0x003a
        L_0x0043:
            r1 = move-exception
            goto L_0x0025
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzby():java.lang.String");
    }

    public final boolean zzbz() {
        return zza("select count(1) > 0 from queue where has_realtime = 1", (String[]) null) != 0;
    }

    /* JADX WARNING: Removed duplicated region for block: B:59:0x0179  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzae zzc(java.lang.String r23, java.lang.String r24) {
        /*
            r22 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r23)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r24)
            r22.zzo()
            r22.zzbi()
            com.google.android.gms.measurement.internal.zzs r2 = r22.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r3 = com.google.android.gms.measurement.internal.zzak.zziz
            r0 = r23
            boolean r21 = r2.zze(r0, r3)
            java.util.ArrayList r4 = new java.util.ArrayList
            r2 = 8
            java.lang.String[] r2 = new java.lang.String[r2]
            r3 = 0
            java.lang.String r5 = "lifetime_count"
            r2[r3] = r5
            r3 = 1
            java.lang.String r5 = "current_bundle_count"
            r2[r3] = r5
            r3 = 2
            java.lang.String r5 = "last_fire_timestamp"
            r2[r3] = r5
            r3 = 3
            java.lang.String r5 = "last_bundled_timestamp"
            r2[r3] = r5
            r3 = 4
            java.lang.String r5 = "last_bundled_day"
            r2[r3] = r5
            r3 = 5
            java.lang.String r5 = "last_sampled_complex_event_id"
            r2[r3] = r5
            r3 = 6
            java.lang.String r5 = "last_sampling_rate"
            r2[r3] = r5
            r3 = 7
            java.lang.String r5 = "last_exempt_from_sampling"
            r2[r3] = r5
            java.util.List r2 = java.util.Arrays.asList(r2)
            r4.<init>(r2)
            if (r21 == 0) goto L_0x0054
            java.lang.String r2 = "current_session_count"
            r4.add(r2)
        L_0x0054:
            android.database.sqlite.SQLiteDatabase r2 = r22.getWritableDatabase()     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            java.lang.String r3 = "events"
            r5 = 0
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            java.lang.Object[] r4 = r4.toArray(r5)     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            java.lang.String[] r4 = (java.lang.String[]) r4     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            java.lang.String r5 = "app_id=? and name=?"
            r6 = 2
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            r7 = 0
            r6[r7] = r23     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            r7 = 1
            r6[r7] = r24     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            r7 = 0
            r8 = 0
            r9 = 0
            android.database.Cursor r20 = r2.query(r3, r4, r5, r6, r7, r8, r9)     // Catch:{ SQLiteException -> 0x014f, all -> 0x0174 }
            boolean r2 = r20.moveToFirst()     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 != 0) goto L_0x0082
            if (r20 == 0) goto L_0x0080
            r20.close()
        L_0x0080:
            r3 = 0
        L_0x0081:
            return r3
        L_0x0082:
            r2 = 0
            r0 = r20
            long r6 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r2 = 1
            r0 = r20
            long r8 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r2 = 2
            r0 = r20
            long r12 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r2 = 3
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 == 0) goto L_0x011d
            r14 = 0
        L_0x00a2:
            r2 = 4
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 == 0) goto L_0x0126
            r16 = 0
        L_0x00ad:
            r2 = 5
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 == 0) goto L_0x0133
            r17 = 0
        L_0x00b8:
            r2 = 6
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 == 0) goto L_0x0140
            r18 = 0
        L_0x00c3:
            r19 = 0
            r2 = 7
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 != 0) goto L_0x00e0
            r2 = 7
            r0 = r20
            long r2 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r4 = 1
            int r2 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1))
            if (r2 != 0) goto L_0x014d
            r2 = 1
        L_0x00dc:
            java.lang.Boolean r19 = java.lang.Boolean.valueOf(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
        L_0x00e0:
            r10 = 0
            if (r21 == 0) goto L_0x00f6
            r2 = 8
            r0 = r20
            boolean r2 = r0.isNull(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 != 0) goto L_0x00f6
            r2 = 8
            r0 = r20
            long r10 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
        L_0x00f6:
            com.google.android.gms.measurement.internal.zzae r3 = new com.google.android.gms.measurement.internal.zzae     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r4 = r23
            r5 = r24
            r3.<init>(r4, r5, r6, r8, r10, r12, r14, r16, r17, r18, r19)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            boolean r2 = r20.moveToNext()     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            if (r2 == 0) goto L_0x0116
            com.google.android.gms.measurement.internal.zzef r2 = r22.zzab()     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            java.lang.String r4 = "Got multiple records for event aggregates, expected one. appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r23)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            r2.zza(r4, r5)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
        L_0x0116:
            if (r20 == 0) goto L_0x0081
            r20.close()
            goto L_0x0081
        L_0x011d:
            r2 = 3
            r0 = r20
            long r14 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            goto L_0x00a2
        L_0x0126:
            r2 = 4
            r0 = r20
            long r2 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            java.lang.Long r16 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            goto L_0x00ad
        L_0x0133:
            r2 = 5
            r0 = r20
            long r2 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            java.lang.Long r17 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            goto L_0x00b8
        L_0x0140:
            r2 = 6
            r0 = r20
            long r2 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0181, all -> 0x0185 }
            java.lang.Long r18 = java.lang.Long.valueOf(r2)
            goto L_0x00c3
        L_0x014d:
            r2 = 0
            goto L_0x00dc
        L_0x014f:
            r2 = move-exception
            r3 = 0
        L_0x0151:
            com.google.android.gms.measurement.internal.zzef r4 = r22.zzab()     // Catch:{ all -> 0x017d }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x017d }
            java.lang.String r5 = "Error querying events. appId"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r23)     // Catch:{ all -> 0x017d }
            com.google.android.gms.measurement.internal.zzed r7 = r22.zzy()     // Catch:{ all -> 0x017d }
            r0 = r24
            java.lang.String r7 = r7.zzaj(r0)     // Catch:{ all -> 0x017d }
            r4.zza(r5, r6, r7, r2)     // Catch:{ all -> 0x017d }
            if (r3 == 0) goto L_0x0171
            r3.close()
        L_0x0171:
            r3 = 0
            goto L_0x0081
        L_0x0174:
            r2 = move-exception
            r20 = 0
        L_0x0177:
            if (r20 == 0) goto L_0x017c
            r20.close()
        L_0x017c:
            throw r2
        L_0x017d:
            r2 = move-exception
            r20 = r3
            goto L_0x0177
        L_0x0181:
            r2 = move-exception
            r3 = r20
            goto L_0x0151
        L_0x0185:
            r2 = move-exception
            goto L_0x0177
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzc(java.lang.String, java.lang.String):com.google.android.gms.measurement.internal.zzae");
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzca() {
        zzo();
        zzbi();
        if (zzcg()) {
            long j = zzac().zzlm.get();
            long elapsedRealtime = zzx().elapsedRealtime();
            if (Math.abs(elapsedRealtime - j) > ((Long) zzak.zzhd.get(null)).longValue()) {
                zzac().zzlm.set(elapsedRealtime);
                zzo();
                zzbi();
                if (zzcg()) {
                    int delete = getWritableDatabase().delete("queue", "abs(bundle_end_timestamp - ?) > cast(? as integer)", new String[]{String.valueOf(zzx().currentTimeMillis()), String.valueOf(zzs.zzbs())});
                    if (delete > 0) {
                        zzab().zzgs().zza("Deleted stale rows. rowsDeleted", Integer.valueOf(delete));
                    }
                }
            }
        }
    }

    @WorkerThread
    public final long zzcb() {
        return zza("select max(bundle_end_timestamp) from queue", (String[]) null, 0);
    }

    @WorkerThread
    public final long zzcc() {
        return zza("select max(timestamp) from raw_events", (String[]) null, 0);
    }

    public final boolean zzcd() {
        return zza("select count(1) > 0 from raw_events", (String[]) null) != 0;
    }

    public final boolean zzce() {
        return zza("select count(1) > 0 from raw_events where realtime = 1", (String[]) null) != 0;
    }

    public final long zzcf() {
        Cursor cursor = null;
        long j = -1;
        try {
            cursor = getWritableDatabase().rawQuery("select rowid from raw_events order by rowid desc limit 1;", null);
            if (cursor.moveToFirst()) {
                j = cursor.getLong(0);
                if (cursor != null) {
                    cursor.close();
                }
            } else if (cursor != null) {
                cursor.close();
            }
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error querying raw events", e);
            if (cursor != null) {
                cursor.close();
            }
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
        return j;
    }

    @WorkerThread
    public final void zzd(String str, String str2) {
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotEmpty(str2);
        zzo();
        zzbi();
        try {
            zzab().zzgs().zza("Deleted user attribute rows", Integer.valueOf(getWritableDatabase().delete("user_attributes", "app_id=? and name=?", new String[]{str, str2})));
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error deleting user attribute. appId", zzef.zzam(str), zzy().zzal(str2), e);
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:26:0x009d  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzjp zze(java.lang.String r10, java.lang.String r11) {
        /*
            r9 = this;
            r8 = 0
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r10)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r11)
            r9.zzo()
            r9.zzbi()
            android.database.sqlite.SQLiteDatabase r0 = r9.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            java.lang.String r1 = "user_attributes"
            r2 = 3
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r3 = 0
            java.lang.String r4 = "set_timestamp"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r3 = 1
            java.lang.String r4 = "value"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r3 = 2
            java.lang.String r4 = "origin"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            java.lang.String r3 = "app_id=? and name=?"
            r4 = 2
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r5 = 0
            r4[r5] = r10     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r5 = 1
            r4[r5] = r11     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r7 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0077, all -> 0x0099 }
            boolean r0 = r7.moveToFirst()     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            if (r0 != 0) goto L_0x0044
            if (r7 == 0) goto L_0x0042
            r7.close()
        L_0x0042:
            r0 = r8
        L_0x0043:
            return r0
        L_0x0044:
            r0 = 0
            long r4 = r7.getLong(r0)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            r0 = 1
            java.lang.Object r6 = r9.zza(r7, r0)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            r0 = 2
            java.lang.String r2 = r7.getString(r0)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            com.google.android.gms.measurement.internal.zzjp r0 = new com.google.android.gms.measurement.internal.zzjp     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            r1 = r10
            r3 = r11
            r0.<init>(r1, r2, r3, r4, r6)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            boolean r1 = r7.moveToNext()     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            if (r1 == 0) goto L_0x0071
            com.google.android.gms.measurement.internal.zzef r1 = r9.zzab()     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgk()     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            java.lang.String r2 = "Got multiple records for user property, expected one. appId"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
            r1.zza(r2, r3)     // Catch:{ SQLiteException -> 0x00a4, all -> 0x00a7 }
        L_0x0071:
            if (r7 == 0) goto L_0x0043
            r7.close()
            goto L_0x0043
        L_0x0077:
            r0 = move-exception
            r1 = r8
        L_0x0079:
            com.google.android.gms.measurement.internal.zzef r2 = r9.zzab()     // Catch:{ all -> 0x00a1 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x00a1 }
            java.lang.String r3 = "Error querying user property. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ all -> 0x00a1 }
            com.google.android.gms.measurement.internal.zzed r5 = r9.zzy()     // Catch:{ all -> 0x00a1 }
            java.lang.String r5 = r5.zzal(r11)     // Catch:{ all -> 0x00a1 }
            r2.zza(r3, r4, r5, r0)     // Catch:{ all -> 0x00a1 }
            if (r1 == 0) goto L_0x0097
            r1.close()
        L_0x0097:
            r0 = r8
            goto L_0x0043
        L_0x0099:
            r0 = move-exception
            r7 = r8
        L_0x009b:
            if (r7 == 0) goto L_0x00a0
            r7.close()
        L_0x00a0:
            throw r0
        L_0x00a1:
            r0 = move-exception
            r7 = r1
            goto L_0x009b
        L_0x00a4:
            r0 = move-exception
            r1 = r7
            goto L_0x0079
        L_0x00a7:
            r0 = move-exception
            goto L_0x009b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zze(java.lang.String, java.lang.String):com.google.android.gms.measurement.internal.zzjp");
    }

    /* JADX WARNING: Removed duplicated region for block: B:30:0x014c  */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final com.google.android.gms.measurement.internal.zzq zzf(java.lang.String r22, java.lang.String r23) {
        /*
            r21 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r22)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r23)
            r21.zzo()
            r21.zzbi()
            android.database.sqlite.SQLiteDatabase r2 = r21.getWritableDatabase()     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            java.lang.String r3 = "conditional_properties"
            r4 = 11
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 0
            java.lang.String r6 = "origin"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 1
            java.lang.String r6 = "value"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 2
            java.lang.String r6 = "active"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 3
            java.lang.String r6 = "trigger_event_name"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 4
            java.lang.String r6 = "trigger_timeout"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 5
            java.lang.String r6 = "timed_out_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 6
            java.lang.String r6 = "creation_timestamp"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 7
            java.lang.String r6 = "triggered_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 8
            java.lang.String r6 = "triggered_timestamp"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 9
            java.lang.String r6 = "time_to_live"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r5 = 10
            java.lang.String r6 = "expired_event"
            r4[r5] = r6     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            java.lang.String r5 = "app_id=? and name=?"
            r6 = 2
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r7 = 0
            r6[r7] = r22     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r7 = 1
            r6[r7] = r23     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            r7 = 0
            r8 = 0
            r9 = 0
            android.database.Cursor r20 = r2.query(r3, r4, r5, r6, r7, r8, r9)     // Catch:{ SQLiteException -> 0x0122, all -> 0x0147 }
            boolean r2 = r20.moveToFirst()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            if (r2 != 0) goto L_0x006f
            if (r20 == 0) goto L_0x006d
            r20.close()
        L_0x006d:
            r5 = 0
        L_0x006e:
            return r5
        L_0x006f:
            r2 = 0
            r0 = r20
            java.lang.String r7 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 1
            r0 = r21
            r1 = r20
            java.lang.Object r6 = r0.zza(r1, r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 2
            r0 = r20
            int r2 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            if (r2 == 0) goto L_0x011f
            r11 = 1
        L_0x0089:
            r2 = 3
            r0 = r20
            java.lang.String r12 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 4
            r0 = r20
            long r14 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzjo r2 = r21.zzgw()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r3 = 5
            r0 = r20
            byte[] r3 = r0.getBlob(r3)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r4 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable r13 = r2.zza(r3, r4)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzai r13 = (com.google.android.gms.measurement.internal.zzai) r13     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 6
            r0 = r20
            long r9 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzjo r2 = r21.zzgw()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r3 = 7
            r0 = r20
            byte[] r3 = r0.getBlob(r3)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r4 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable r16 = r2.zza(r3, r4)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzai r16 = (com.google.android.gms.measurement.internal.zzai) r16     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 8
            r0 = r20
            long r4 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2 = 9
            r0 = r20
            long r17 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzjo r2 = r21.zzgw()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r3 = 10
            r0 = r20
            byte[] r3 = r0.getBlob(r3)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable$Creator<com.google.android.gms.measurement.internal.zzai> r8 = com.google.android.gms.measurement.internal.zzai.CREATOR     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            android.os.Parcelable r19 = r2.zza(r3, r8)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzai r19 = (com.google.android.gms.measurement.internal.zzai) r19     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzjn r2 = new com.google.android.gms.measurement.internal.zzjn     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r3 = r23
            r2.<init>(r3, r4, r6, r7)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzq r5 = new com.google.android.gms.measurement.internal.zzq     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r6 = r22
            r8 = r2
            r5.<init>(r6, r7, r8, r9, r11, r12, r13, r14, r16, r17, r19)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            boolean r2 = r20.moveToNext()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            if (r2 == 0) goto L_0x0118
            com.google.android.gms.measurement.internal.zzef r2 = r21.zzab()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            java.lang.String r3 = "Got multiple records for conditional property, expected one"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r22)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            com.google.android.gms.measurement.internal.zzed r6 = r21.zzy()     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r0 = r23
            java.lang.String r6 = r6.zzal(r0)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
            r2.zza(r3, r4, r6)     // Catch:{ SQLiteException -> 0x0154, all -> 0x0158 }
        L_0x0118:
            if (r20 == 0) goto L_0x006e
            r20.close()
            goto L_0x006e
        L_0x011f:
            r11 = 0
            goto L_0x0089
        L_0x0122:
            r2 = move-exception
            r3 = 0
        L_0x0124:
            com.google.android.gms.measurement.internal.zzef r4 = r21.zzab()     // Catch:{ all -> 0x0150 }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x0150 }
            java.lang.String r5 = "Error querying conditional property"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r22)     // Catch:{ all -> 0x0150 }
            com.google.android.gms.measurement.internal.zzed r7 = r21.zzy()     // Catch:{ all -> 0x0150 }
            r0 = r23
            java.lang.String r7 = r7.zzal(r0)     // Catch:{ all -> 0x0150 }
            r4.zza(r5, r6, r7, r2)     // Catch:{ all -> 0x0150 }
            if (r3 == 0) goto L_0x0144
            r3.close()
        L_0x0144:
            r5 = 0
            goto L_0x006e
        L_0x0147:
            r2 = move-exception
            r20 = 0
        L_0x014a:
            if (r20 == 0) goto L_0x014f
            r20.close()
        L_0x014f:
            throw r2
        L_0x0150:
            r2 = move-exception
            r20 = r3
            goto L_0x014a
        L_0x0154:
            r2 = move-exception
            r3 = r20
            goto L_0x0124
        L_0x0158:
            r2 = move-exception
            goto L_0x014a
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzf(java.lang.String, java.lang.String):com.google.android.gms.measurement.internal.zzq");
    }

    @WorkerThread
    public final int zzg(String str, String str2) {
        boolean z = false;
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotEmpty(str2);
        zzo();
        zzbi();
        try {
            return getWritableDatabase().delete("conditional_properties", "app_id=? and name=?", new String[]{str, str2});
        } catch (SQLiteException e) {
            zzab().zzgk().zza("Error deleting conditional property", zzef.zzam(str), zzy().zzal(str2), e);
            return z;
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:37:0x00b1  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.Map<java.lang.Integer, java.util.List<com.google.android.gms.internal.measurement.zzbk.zza>> zzh(java.lang.String r11, java.lang.String r12) {
        /*
            r10 = this;
            r9 = 0
            r10.zzbi()
            r10.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r11)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            android.support.v4.util.ArrayMap r8 = new android.support.v4.util.ArrayMap
            r8.<init>()
            android.database.sqlite.SQLiteDatabase r0 = r10.getWritableDatabase()
            java.lang.String r1 = "event_filters"
            r2 = 2
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r3 = 0
            java.lang.String r4 = "audience_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r3 = 1
            java.lang.String r4 = "data"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            java.lang.String r3 = "app_id=? AND event_name=?"
            r4 = 2
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 0
            r4[r5] = r11     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 1
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0047
            java.util.Map r0 = java.util.Collections.emptyMap()     // Catch:{ SQLiteException -> 0x0094 }
            if (r1 == 0) goto L_0x0046
            r1.close()
        L_0x0046:
            return r0
        L_0x0047:
            r0 = 1
            byte[] r0 = r1.getBlob(r0)     // Catch:{ SQLiteException -> 0x0094 }
            com.google.android.gms.internal.measurement.zzel r2 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x0081 }
            com.google.android.gms.internal.measurement.zzbk$zza r2 = com.google.android.gms.internal.measurement.zzbk.zza.zza(r0, r2)     // Catch:{ IOException -> 0x0081 }
            r0 = 0
            int r3 = r1.getInt(r0)     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Integer r0 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Object r0 = r8.get(r0)     // Catch:{ SQLiteException -> 0x0094 }
            java.util.List r0 = (java.util.List) r0     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0071
            java.util.ArrayList r0 = new java.util.ArrayList     // Catch:{ SQLiteException -> 0x0094 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x0094 }
            r8.put(r3, r0)     // Catch:{ SQLiteException -> 0x0094 }
        L_0x0071:
            r0.add(r2)     // Catch:{ SQLiteException -> 0x0094 }
        L_0x0074:
            boolean r0 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0047
            if (r1 == 0) goto L_0x007f
            r1.close()
        L_0x007f:
            r0 = r8
            goto L_0x0046
        L_0x0081:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r2 = r10.zzab()     // Catch:{ SQLiteException -> 0x0094 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.String r3 = "Failed to merge filter. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r11)     // Catch:{ SQLiteException -> 0x0094 }
            r2.zza(r3, r4, r0)     // Catch:{ SQLiteException -> 0x0094 }
            goto L_0x0074
        L_0x0094:
            r0 = move-exception
        L_0x0095:
            com.google.android.gms.measurement.internal.zzef r2 = r10.zzab()     // Catch:{ all -> 0x00ad }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x00ad }
            java.lang.String r3 = "Database error querying filters. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r11)     // Catch:{ all -> 0x00ad }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x00ad }
            if (r1 == 0) goto L_0x00ab
            r1.close()
        L_0x00ab:
            r0 = r9
            goto L_0x0046
        L_0x00ad:
            r0 = move-exception
            r9 = r1
        L_0x00af:
            if (r9 == 0) goto L_0x00b4
            r9.close()
        L_0x00b4:
            throw r0
        L_0x00b5:
            r0 = move-exception
            r1 = r9
            goto L_0x0095
        L_0x00b8:
            r0 = move-exception
            goto L_0x00af
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzh(java.lang.String, java.lang.String):java.util.Map");
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:37:0x00b1  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.util.Map<java.lang.Integer, java.util.List<com.google.android.gms.internal.measurement.zzbk.zzd>> zzi(java.lang.String r11, java.lang.String r12) {
        /*
            r10 = this;
            r9 = 0
            r10.zzbi()
            r10.zzo()
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r11)
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r12)
            android.support.v4.util.ArrayMap r8 = new android.support.v4.util.ArrayMap
            r8.<init>()
            android.database.sqlite.SQLiteDatabase r0 = r10.getWritableDatabase()
            java.lang.String r1 = "property_filters"
            r2 = 2
            java.lang.String[] r2 = new java.lang.String[r2]     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r3 = 0
            java.lang.String r4 = "audience_id"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r3 = 1
            java.lang.String r4 = "data"
            r2[r3] = r4     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            java.lang.String r3 = "app_id=? AND property_name=?"
            r4 = 2
            java.lang.String[] r4 = new java.lang.String[r4]     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 0
            r4[r5] = r11     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 1
            r4[r5] = r12     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            r5 = 0
            r6 = 0
            r7 = 0
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00b8 }
            boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0047
            java.util.Map r0 = java.util.Collections.emptyMap()     // Catch:{ SQLiteException -> 0x0094 }
            if (r1 == 0) goto L_0x0046
            r1.close()
        L_0x0046:
            return r0
        L_0x0047:
            r0 = 1
            byte[] r0 = r1.getBlob(r0)     // Catch:{ SQLiteException -> 0x0094 }
            com.google.android.gms.internal.measurement.zzel r2 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x0081 }
            com.google.android.gms.internal.measurement.zzbk$zzd r2 = com.google.android.gms.internal.measurement.zzbk.zzd.zzb(r0, r2)     // Catch:{ IOException -> 0x0081 }
            r0 = 0
            int r3 = r1.getInt(r0)     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Integer r0 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Object r0 = r8.get(r0)     // Catch:{ SQLiteException -> 0x0094 }
            java.util.List r0 = (java.util.List) r0     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0071
            java.util.ArrayList r0 = new java.util.ArrayList     // Catch:{ SQLiteException -> 0x0094 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x0094 }
            r8.put(r3, r0)     // Catch:{ SQLiteException -> 0x0094 }
        L_0x0071:
            r0.add(r2)     // Catch:{ SQLiteException -> 0x0094 }
        L_0x0074:
            boolean r0 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0094 }
            if (r0 != 0) goto L_0x0047
            if (r1 == 0) goto L_0x007f
            r1.close()
        L_0x007f:
            r0 = r8
            goto L_0x0046
        L_0x0081:
            r0 = move-exception
            com.google.android.gms.measurement.internal.zzef r2 = r10.zzab()     // Catch:{ SQLiteException -> 0x0094 }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ SQLiteException -> 0x0094 }
            java.lang.String r3 = "Failed to merge filter"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r11)     // Catch:{ SQLiteException -> 0x0094 }
            r2.zza(r3, r4, r0)     // Catch:{ SQLiteException -> 0x0094 }
            goto L_0x0074
        L_0x0094:
            r0 = move-exception
        L_0x0095:
            com.google.android.gms.measurement.internal.zzef r2 = r10.zzab()     // Catch:{ all -> 0x00ad }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x00ad }
            java.lang.String r3 = "Database error querying filters. appId"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r11)     // Catch:{ all -> 0x00ad }
            r2.zza(r3, r4, r0)     // Catch:{ all -> 0x00ad }
            if (r1 == 0) goto L_0x00ab
            r1.close()
        L_0x00ab:
            r0 = r9
            goto L_0x0046
        L_0x00ad:
            r0 = move-exception
            r9 = r1
        L_0x00af:
            if (r9 == 0) goto L_0x00b4
            r9.close()
        L_0x00b4:
            throw r0
        L_0x00b5:
            r0 = move-exception
            r1 = r9
            goto L_0x0095
        L_0x00b8:
            r0 = move-exception
            goto L_0x00af
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzi(java.lang.String, java.lang.String):java.util.Map");
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    @VisibleForTesting
    public final long zzj(String str, String str2) {
        Object obj;
        long j;
        Preconditions.checkNotEmpty(str);
        Preconditions.checkNotEmpty(str2);
        zzo();
        zzbi();
        SQLiteDatabase writableDatabase = getWritableDatabase();
        writableDatabase.beginTransaction();
        try {
            long zza = zza(new StringBuilder(String.valueOf(str2).length() + 32).append("select ").append(str2).append(" from app2 where app_id=?").toString(), new String[]{str}, -1);
            if (zza == -1) {
                ContentValues contentValues = new ContentValues();
                contentValues.put("app_id", str);
                contentValues.put("first_open_count", Integer.valueOf(0));
                contentValues.put("previous_install_count", Integer.valueOf(0));
                if (writableDatabase.insertWithOnConflict("app2", null, contentValues, 5) == -1) {
                    zzab().zzgk().zza("Failed to insert column (got -1). appId", zzef.zzam(str), str2);
                    writableDatabase.endTransaction();
                    return -1;
                }
                zza = 0;
            }
            try {
                ContentValues contentValues2 = new ContentValues();
                contentValues2.put("app_id", str);
                contentValues2.put(str2, Long.valueOf(1 + zza));
                if (((long) writableDatabase.update("app2", contentValues2, "app_id = ?", new String[]{str})) == 0) {
                    zzab().zzgk().zza("Failed to update column (got 0). appId", zzef.zzam(str), str2);
                    writableDatabase.endTransaction();
                    return -1;
                }
                writableDatabase.setTransactionSuccessful();
                writableDatabase.endTransaction();
                return zza;
            } catch (SQLiteException e) {
                obj = e;
                j = zza;
                try {
                    zzab().zzgk().zza("Error inserting column. appId", zzef.zzam(str), str2, obj);
                    return j;
                } finally {
                    writableDatabase.endTransaction();
                }
            }
        } catch (SQLiteException e2) {
            obj = e2;
            j = 0;
            zzab().zzgk().zza("Error inserting column. appId", zzef.zzam(str), str2, obj);
            return j;
        }
    }

    /* JADX WARNING: type inference failed for: r2v0, types: [android.database.Cursor] */
    /* JADX WARNING: type inference failed for: r2v1 */
    /* JADX WARNING: type inference failed for: r2v2, types: [android.database.Cursor] */
    /* JADX WARNING: type inference failed for: r2v3 */
    /* JADX WARNING: type inference failed for: r2v6 */
    /* JADX WARNING: type inference failed for: r2v7 */
    /* JADX WARNING: Multi-variable type inference failed */
    /* JADX WARNING: Removed duplicated region for block: B:24:0x0058  */
    /* JADX WARNING: Unknown variable types count: 2 */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final java.lang.String zzu(long r8) {
        /*
            r7 = this;
            r0 = 0
            r7.zzo()
            r7.zzbi()
            android.database.sqlite.SQLiteDatabase r1 = r7.getWritableDatabase()     // Catch:{ SQLiteException -> 0x003f, all -> 0x0054 }
            java.lang.String r2 = "select app_id from apps where app_id in (select distinct app_id from raw_events) and config_fetched_time < ? order by failed_config_fetch_time limit 1;"
            r3 = 1
            java.lang.String[] r3 = new java.lang.String[r3]     // Catch:{ SQLiteException -> 0x003f, all -> 0x0054 }
            r4 = 0
            java.lang.String r5 = java.lang.String.valueOf(r8)     // Catch:{ SQLiteException -> 0x003f, all -> 0x0054 }
            r3[r4] = r5     // Catch:{ SQLiteException -> 0x003f, all -> 0x0054 }
            android.database.Cursor r2 = r1.rawQuery(r2, r3)     // Catch:{ SQLiteException -> 0x003f, all -> 0x0054 }
            boolean r1 = r2.moveToFirst()     // Catch:{ SQLiteException -> 0x005f }
            if (r1 != 0) goto L_0x0034
            com.google.android.gms.measurement.internal.zzef r1 = r7.zzab()     // Catch:{ SQLiteException -> 0x005f }
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgs()     // Catch:{ SQLiteException -> 0x005f }
            java.lang.String r3 = "No expired configs for apps with pending events"
            r1.zzao(r3)     // Catch:{ SQLiteException -> 0x005f }
            if (r2 == 0) goto L_0x0033
            r2.close()
        L_0x0033:
            return r0
        L_0x0034:
            r1 = 0
            java.lang.String r0 = r2.getString(r1)     // Catch:{ SQLiteException -> 0x005f }
            if (r2 == 0) goto L_0x0033
            r2.close()
            goto L_0x0033
        L_0x003f:
            r1 = move-exception
            r2 = r0
        L_0x0041:
            com.google.android.gms.measurement.internal.zzef r3 = r7.zzab()     // Catch:{ all -> 0x005c }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ all -> 0x005c }
            java.lang.String r4 = "Error selecting expired configs"
            r3.zza(r4, r1)     // Catch:{ all -> 0x005c }
            if (r2 == 0) goto L_0x0033
            r2.close()
            goto L_0x0033
        L_0x0054:
            r1 = move-exception
            r2 = r0
        L_0x0056:
            if (r2 == 0) goto L_0x005b
            r2.close()
        L_0x005b:
            throw r1
        L_0x005c:
            r0 = move-exception
            r1 = r0
            goto L_0x0056
        L_0x005f:
            r1 = move-exception
            goto L_0x0041
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzx.zzu(long):java.lang.String");
    }
}
