package com.google.android.gms.internal;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import android.util.Pair;
import bolts.MeasurementEvent;
import com.facebook.share.internal.ShareConstants;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.measurement.AppMeasurement;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.common.CommonUtils;
import java.io.File;
import java.io.IOException;
import java.security.MessageDigest;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Set;

final class zzcaq extends zzcdm {
    private static final Map<String, String> zzimk;
    private static final Map<String, String> zziml;
    private static final Map<String, String> zzimm;
    private static final Map<String, String> zzimn;
    private static final Map<String, String> zzimo;
    private final zzcat zzimp = new zzcat(this, getContext(), zzcap.zzawh());
    private final zzcfi zzimq = new zzcfi(zzvu());

    static {
        Map arrayMap = new ArrayMap(1);
        zzimk = arrayMap;
        arrayMap.put(Param.ORIGIN, "ALTER TABLE user_attributes ADD COLUMN origin TEXT;");
        arrayMap = new ArrayMap(18);
        zziml = arrayMap;
        arrayMap.put("app_version", "ALTER TABLE apps ADD COLUMN app_version TEXT;");
        zziml.put("app_store", "ALTER TABLE apps ADD COLUMN app_store TEXT;");
        zziml.put("gmp_version", "ALTER TABLE apps ADD COLUMN gmp_version INTEGER;");
        zziml.put("dev_cert_hash", "ALTER TABLE apps ADD COLUMN dev_cert_hash INTEGER;");
        zziml.put("measurement_enabled", "ALTER TABLE apps ADD COLUMN measurement_enabled INTEGER;");
        zziml.put("last_bundle_start_timestamp", "ALTER TABLE apps ADD COLUMN last_bundle_start_timestamp INTEGER;");
        zziml.put("day", "ALTER TABLE apps ADD COLUMN day INTEGER;");
        zziml.put("daily_public_events_count", "ALTER TABLE apps ADD COLUMN daily_public_events_count INTEGER;");
        zziml.put("daily_events_count", "ALTER TABLE apps ADD COLUMN daily_events_count INTEGER;");
        zziml.put("daily_conversions_count", "ALTER TABLE apps ADD COLUMN daily_conversions_count INTEGER;");
        zziml.put("remote_config", "ALTER TABLE apps ADD COLUMN remote_config BLOB;");
        zziml.put("config_fetched_time", "ALTER TABLE apps ADD COLUMN config_fetched_time INTEGER;");
        zziml.put("failed_config_fetch_time", "ALTER TABLE apps ADD COLUMN failed_config_fetch_time INTEGER;");
        zziml.put("app_version_int", "ALTER TABLE apps ADD COLUMN app_version_int INTEGER;");
        zziml.put("firebase_instance_id", "ALTER TABLE apps ADD COLUMN firebase_instance_id TEXT;");
        zziml.put("daily_error_events_count", "ALTER TABLE apps ADD COLUMN daily_error_events_count INTEGER;");
        zziml.put("daily_realtime_events_count", "ALTER TABLE apps ADD COLUMN daily_realtime_events_count INTEGER;");
        zziml.put("health_monitor_sample", "ALTER TABLE apps ADD COLUMN health_monitor_sample TEXT;");
        zziml.put("android_id", "ALTER TABLE apps ADD COLUMN android_id INTEGER;");
        arrayMap = new ArrayMap(1);
        zzimm = arrayMap;
        arrayMap.put("realtime", "ALTER TABLE raw_events ADD COLUMN realtime INTEGER;");
        arrayMap = new ArrayMap(1);
        zzimn = arrayMap;
        arrayMap.put("has_realtime", "ALTER TABLE queue ADD COLUMN has_realtime INTEGER;");
        arrayMap = new ArrayMap(1);
        zzimo = arrayMap;
        arrayMap.put("previous_install_count", "ALTER TABLE app2 ADD COLUMN previous_install_count INTEGER;");
    }

    zzcaq(zzcco zzcco) {
        super(zzcco);
    }

    @WorkerThread
    private final long zza(String str, String[] strArr, long j) {
        Cursor cursor = null;
        try {
            cursor = getWritableDatabase().rawQuery(str, strArr);
            if (cursor.moveToFirst()) {
                j = cursor.getLong(0);
                if (cursor != null) {
                    cursor.close();
                }
            } else if (cursor != null) {
                cursor.close();
            }
            return j;
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Database error", str, e);
            throw e;
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
        }
    }

    @WorkerThread
    private final Object zza(Cursor cursor, int i) {
        int type = cursor.getType(i);
        switch (type) {
            case 0:
                zzauk().zzayc().log("Loaded invalid null value from database");
                return null;
            case 1:
                return Long.valueOf(cursor.getLong(i));
            case 2:
                return Double.valueOf(cursor.getDouble(i));
            case 3:
                return cursor.getString(i);
            case 4:
                zzauk().zzayc().log("Loaded invalid blob type value, ignoring it");
                return null;
            default:
                zzauk().zzayc().zzj("Loaded invalid unknown value type, ignoring it", Integer.valueOf(type));
                return null;
        }
    }

    @WorkerThread
    private static void zza(ContentValues contentValues, String str, Object obj) {
        zzbp.zzgf(str);
        zzbp.zzu(obj);
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

    static void zza(zzcbo zzcbo, SQLiteDatabase sQLiteDatabase) {
        if (zzcbo == null) {
            throw new IllegalArgumentException("Monitor must not be null");
        }
        File file = new File(sQLiteDatabase.getPath());
        if (!file.setReadable(false, false)) {
            zzcbo.zzaye().log("Failed to turn off database read permission");
        }
        if (!file.setWritable(false, false)) {
            zzcbo.zzaye().log("Failed to turn off database write permission");
        }
        if (!file.setReadable(true, true)) {
            zzcbo.zzaye().log("Failed to turn on database read permission for owner");
        }
        if (!file.setWritable(true, true)) {
            zzcbo.zzaye().log("Failed to turn on database write permission for owner");
        }
    }

    @WorkerThread
    static void zza(zzcbo zzcbo, SQLiteDatabase sQLiteDatabase, String str, String str2, String str3, Map<String, String> map) throws SQLiteException {
        if (zzcbo == null) {
            throw new IllegalArgumentException("Monitor must not be null");
        }
        if (!zza(zzcbo, sQLiteDatabase, str)) {
            sQLiteDatabase.execSQL(str2);
        }
        try {
            zza(zzcbo, sQLiteDatabase, str, str3, map);
        } catch (SQLiteException e) {
            zzcbo.zzayc().zzj("Failed to verify columns on table that was just created", str);
            throw e;
        }
    }

    @WorkerThread
    private static void zza(zzcbo zzcbo, SQLiteDatabase sQLiteDatabase, String str, String str2, Map<String, String> map) throws SQLiteException {
        if (zzcbo == null) {
            throw new IllegalArgumentException("Monitor must not be null");
        }
        Iterable zzb = zzb(sQLiteDatabase, str);
        String[] split = str2.split(",");
        int length = split.length;
        int i = 0;
        while (i < length) {
            String str3 = split[i];
            if (zzb.remove(str3)) {
                i++;
            } else {
                throw new SQLiteException(new StringBuilder((String.valueOf(str).length() + 35) + String.valueOf(str3).length()).append("Table ").append(str).append(" is missing required column: ").append(str3).toString());
            }
        }
        if (map != null) {
            for (Entry entry : map.entrySet()) {
                if (!zzb.remove(entry.getKey())) {
                    sQLiteDatabase.execSQL((String) entry.getValue());
                }
            }
        }
        if (!zzb.isEmpty()) {
            zzcbo.zzaye().zze("Table has extra columns. table, columns", str, TextUtils.join(", ", zzb));
        }
    }

    @WorkerThread
    private static boolean zza(zzcbo zzcbo, SQLiteDatabase sQLiteDatabase, String str) {
        Cursor query;
        Object e;
        Throwable th;
        Cursor cursor = null;
        if (zzcbo == null) {
            throw new IllegalArgumentException("Monitor must not be null");
        }
        try {
            SQLiteDatabase sQLiteDatabase2 = sQLiteDatabase;
            query = sQLiteDatabase2.query("SQLITE_MASTER", new String[]{"name"}, "name=?", new String[]{str}, null, null, null);
            try {
                boolean moveToFirst = query.moveToFirst();
                if (query == null) {
                    return moveToFirst;
                }
                query.close();
                return moveToFirst;
            } catch (SQLiteException e2) {
                e = e2;
                try {
                    zzcbo.zzaye().zze("Error querying for table", str, e);
                    if (query != null) {
                        query.close();
                    }
                    return false;
                } catch (Throwable th2) {
                    th = th2;
                    cursor = query;
                    if (cursor != null) {
                        cursor.close();
                    }
                    throw th;
                }
            }
        } catch (SQLiteException e3) {
            e = e3;
            query = null;
            zzcbo.zzaye().zze("Error querying for table", str, e);
            if (query != null) {
                query.close();
            }
            return false;
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    @WorkerThread
    private final boolean zza(String str, int i, zzcfq zzcfq) {
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzbp.zzu(zzcfq);
        if (TextUtils.isEmpty(zzcfq.zzixj)) {
            zzauk().zzaye().zzd("Event filter had no event name. Audience definition ignored. appId, audienceId, filterId", zzcbo.zzjf(str), Integer.valueOf(i), String.valueOf(zzcfq.zzixi));
            return false;
        }
        try {
            byte[] bArr = new byte[zzcfq.zzbjo()];
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzcfq.zza(zzi);
            zzi.zzccd();
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", str);
            contentValues.put("audience_id", Integer.valueOf(i));
            contentValues.put("filter_id", zzcfq.zzixi);
            contentValues.put(MeasurementEvent.MEASUREMENT_EVENT_NAME_KEY, zzcfq.zzixj);
            contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, bArr);
            try {
                if (getWritableDatabase().insertWithOnConflict("event_filters", null, contentValues, 5) == -1) {
                    zzauk().zzayc().zzj("Failed to insert event filter (got -1). appId", zzcbo.zzjf(str));
                }
                return true;
            } catch (SQLiteException e) {
                zzauk().zzayc().zze("Error storing event filter. appId", zzcbo.zzjf(str), e);
                return false;
            }
        } catch (IOException e2) {
            zzauk().zzayc().zze("Configuration loss. Failed to serialize event filter. appId", zzcbo.zzjf(str), e2);
            return false;
        }
    }

    @WorkerThread
    private final boolean zza(String str, int i, zzcft zzcft) {
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzbp.zzu(zzcft);
        if (TextUtils.isEmpty(zzcft.zzixy)) {
            zzauk().zzaye().zzd("Property filter had no property name. Audience definition ignored. appId, audienceId, filterId", zzcbo.zzjf(str), Integer.valueOf(i), String.valueOf(zzcft.zzixi));
            return false;
        }
        try {
            byte[] bArr = new byte[zzcft.zzbjo()];
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzcft.zza(zzi);
            zzi.zzccd();
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", str);
            contentValues.put("audience_id", Integer.valueOf(i));
            contentValues.put("filter_id", zzcft.zzixi);
            contentValues.put("property_name", zzcft.zzixy);
            contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, bArr);
            try {
                if (getWritableDatabase().insertWithOnConflict("property_filters", null, contentValues, 5) != -1) {
                    return true;
                }
                zzauk().zzayc().zzj("Failed to insert property filter (got -1). appId", zzcbo.zzjf(str));
                return false;
            } catch (SQLiteException e) {
                zzauk().zzayc().zze("Error storing property filter. appId", zzcbo.zzjf(str), e);
                return false;
            }
        } catch (IOException e2) {
            zzauk().zzayc().zze("Configuration loss. Failed to serialize property filter. appId", zzcbo.zzjf(str), e2);
            return false;
        }
    }

    private final boolean zzaxp() {
        return getContext().getDatabasePath(zzcap.zzawh()).exists();
    }

    @WorkerThread
    private final long zzb(String str, String[] strArr) {
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
            zzauk().zzayc().zze("Database error", str, e);
            throw e;
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
        }
    }

    @WorkerThread
    private static Set<String> zzb(SQLiteDatabase sQLiteDatabase, String str) {
        Set<String> hashSet = new HashSet();
        Cursor rawQuery = sQLiteDatabase.rawQuery(new StringBuilder(String.valueOf(str).length() + 22).append("SELECT * FROM ").append(str).append(" LIMIT 0").toString(), null);
        try {
            Collections.addAll(hashSet, rawQuery.getColumnNames());
            return hashSet;
        } finally {
            rawQuery.close();
        }
    }

    private final boolean zzc(String str, List<Integer> list) {
        zzbp.zzgf(str);
        zzwh();
        zzug();
        SQLiteDatabase writableDatabase = getWritableDatabase();
        try {
            if (zzb("select count(1) from audience_filter_values where app_id=?", new String[]{str}) <= ((long) Math.max(0, Math.min(2000, zzaum().zzb(str, zzcbe.zziox))))) {
                return false;
            }
            Iterable arrayList = new ArrayList();
            for (int i = 0; i < list.size(); i++) {
                Integer num = (Integer) list.get(i);
                if (num == null || !(num instanceof Integer)) {
                    break;
                }
                arrayList.add(Integer.toString(num.intValue()));
            }
            String join = TextUtils.join(",", arrayList);
            join = new StringBuilder(String.valueOf(join).length() + 2).append("(").append(join).append(")").toString();
            if (writableDatabase.delete("audience_filter_values", new StringBuilder(String.valueOf(join).length() + 140).append("audience_id in (select audience_id from audience_filter_values where app_id=? and audience_id not in ").append(join).append(" order by rowid desc limit -1 offset ?)").toString(), new String[]{str, Integer.toString(r5)}) > 0) {
                return true;
            }
            return false;
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Database error querying filters. appId", zzcbo.zzjf(str), e);
            return false;
        }
    }

    @WorkerThread
    public final void beginTransaction() {
        zzwh();
        getWritableDatabase().beginTransaction();
    }

    @WorkerThread
    public final void endTransaction() {
        zzwh();
        getWritableDatabase().endTransaction();
    }

    @WorkerThread
    final SQLiteDatabase getWritableDatabase() {
        zzug();
        try {
            return this.zzimp.getWritableDatabase();
        } catch (SQLiteException e) {
            zzauk().zzaye().zzj("Error opening database", e);
            throw e;
        }
    }

    @WorkerThread
    public final void setTransactionSuccessful() {
        zzwh();
        getWritableDatabase().setTransactionSuccessful();
    }

    public final long zza(zzcgc zzcgc) throws IOException {
        zzug();
        zzwh();
        zzbp.zzu(zzcgc);
        zzbp.zzgf(zzcgc.zzch);
        try {
            long j;
            Object obj = new byte[zzcgc.zzbjo()];
            zzegg zzi = zzegg.zzi(obj, 0, obj.length);
            zzcgc.zza(zzi);
            zzi.zzccd();
            zzcdl zzaug = zzaug();
            zzbp.zzu(obj);
            zzaug.zzug();
            MessageDigest zzed = zzcfo.zzed(CommonUtils.MD5_INSTANCE);
            if (zzed == null) {
                zzaug.zzauk().zzayc().log("Failed to get MD5");
                j = 0;
            } else {
                j = zzcfo.zzq(zzed.digest(obj));
            }
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", zzcgc.zzch);
            contentValues.put("metadata_fingerprint", Long.valueOf(j));
            contentValues.put("metadata", obj);
            try {
                getWritableDatabase().insertWithOnConflict("raw_events_metadata", null, contentValues, 4);
                return j;
            } catch (SQLiteException e) {
                zzauk().zzayc().zze("Error storing raw event metadata. appId", zzcbo.zzjf(zzcgc.zzch), e);
                throw e;
            }
        } catch (IOException e2) {
            zzauk().zzayc().zze("Data loss. Failed to serialize event metadata. appId", zzcbo.zzjf(zzcgc.zzch), e2);
            throw e2;
        }
    }

    @WorkerThread
    public final zzcar zza(long j, String str, boolean z, boolean z2, boolean z3, boolean z4, boolean z5) {
        Object e;
        Throwable th;
        zzbp.zzgf(str);
        zzug();
        zzwh();
        zzcar zzcar = new zzcar();
        Cursor query;
        try {
            SQLiteDatabase writableDatabase = getWritableDatabase();
            query = writableDatabase.query("apps", new String[]{"day", "daily_events_count", "daily_public_events_count", "daily_conversions_count", "daily_error_events_count", "daily_realtime_events_count"}, "app_id=?", new String[]{str}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    if (query.getLong(0) == j) {
                        zzcar.zzims = query.getLong(1);
                        zzcar.zzimr = query.getLong(2);
                        zzcar.zzimt = query.getLong(3);
                        zzcar.zzimu = query.getLong(4);
                        zzcar.zzimv = query.getLong(5);
                    }
                    if (z) {
                        zzcar.zzims++;
                    }
                    if (z2) {
                        zzcar.zzimr++;
                    }
                    if (z3) {
                        zzcar.zzimt++;
                    }
                    if (z4) {
                        zzcar.zzimu++;
                    }
                    if (z5) {
                        zzcar.zzimv++;
                    }
                    ContentValues contentValues = new ContentValues();
                    contentValues.put("day", Long.valueOf(j));
                    contentValues.put("daily_public_events_count", Long.valueOf(zzcar.zzimr));
                    contentValues.put("daily_events_count", Long.valueOf(zzcar.zzims));
                    contentValues.put("daily_conversions_count", Long.valueOf(zzcar.zzimt));
                    contentValues.put("daily_error_events_count", Long.valueOf(zzcar.zzimu));
                    contentValues.put("daily_realtime_events_count", Long.valueOf(zzcar.zzimv));
                    writableDatabase.update("apps", contentValues, "app_id=?", new String[]{str});
                    if (query != null) {
                        query.close();
                    }
                } else {
                    zzauk().zzaye().zzj("Not updating daily counts, app is not known. appId", zzcbo.zzjf(str));
                    if (query != null) {
                        query.close();
                    }
                }
            } catch (SQLiteException e2) {
                e = e2;
                try {
                    zzauk().zzayc().zze("Error updating daily counts. appId", zzcbo.zzjf(str), e);
                    if (query != null) {
                        query.close();
                    }
                    return zzcar;
                } catch (Throwable th2) {
                    th = th2;
                    if (query != null) {
                        query.close();
                    }
                    throw th;
                }
            }
        } catch (SQLiteException e3) {
            e = e3;
            query = null;
            zzauk().zzayc().zze("Error updating daily counts. appId", zzcbo.zzjf(str), e);
            if (query != null) {
                query.close();
            }
            return zzcar;
        } catch (Throwable th3) {
            th = th3;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
        return zzcar;
    }

    @WorkerThread
    public final void zza(zzcaj zzcaj) {
        zzbp.zzu(zzcaj);
        zzug();
        zzwh();
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzcaj.getAppId());
        contentValues.put("app_instance_id", zzcaj.getAppInstanceId());
        contentValues.put("gmp_app_id", zzcaj.getGmpAppId());
        contentValues.put("resettable_device_id_hash", zzcaj.zzauo());
        contentValues.put("last_bundle_index", Long.valueOf(zzcaj.zzaux()));
        contentValues.put("last_bundle_start_timestamp", Long.valueOf(zzcaj.zzauq()));
        contentValues.put("last_bundle_end_timestamp", Long.valueOf(zzcaj.zzaur()));
        contentValues.put("app_version", zzcaj.zzul());
        contentValues.put("app_store", zzcaj.zzaut());
        contentValues.put("gmp_version", Long.valueOf(zzcaj.zzauu()));
        contentValues.put("dev_cert_hash", Long.valueOf(zzcaj.zzauv()));
        contentValues.put("measurement_enabled", Boolean.valueOf(zzcaj.zzauw()));
        contentValues.put("day", Long.valueOf(zzcaj.zzavb()));
        contentValues.put("daily_public_events_count", Long.valueOf(zzcaj.zzavc()));
        contentValues.put("daily_events_count", Long.valueOf(zzcaj.zzavd()));
        contentValues.put("daily_conversions_count", Long.valueOf(zzcaj.zzave()));
        contentValues.put("config_fetched_time", Long.valueOf(zzcaj.zzauy()));
        contentValues.put("failed_config_fetch_time", Long.valueOf(zzcaj.zzauz()));
        contentValues.put("app_version_int", Long.valueOf(zzcaj.zzaus()));
        contentValues.put("firebase_instance_id", zzcaj.zzaup());
        contentValues.put("daily_error_events_count", Long.valueOf(zzcaj.zzavg()));
        contentValues.put("daily_realtime_events_count", Long.valueOf(zzcaj.zzavf()));
        contentValues.put("health_monitor_sample", zzcaj.zzavh());
        contentValues.put("android_id", Long.valueOf(zzcaj.zzavj()));
        try {
            SQLiteDatabase writableDatabase = getWritableDatabase();
            if (((long) writableDatabase.update("apps", contentValues, "app_id = ?", new String[]{zzcaj.getAppId()})) == 0 && writableDatabase.insertWithOnConflict("apps", null, contentValues, 5) == -1) {
                zzauk().zzayc().zzj("Failed to insert/update app (got -1). appId", zzcbo.zzjf(zzcaj.getAppId()));
            }
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Error storing app. appId", zzcbo.zzjf(zzcaj.getAppId()), e);
        }
    }

    @WorkerThread
    public final void zza(zzcay zzcay) {
        zzbp.zzu(zzcay);
        zzug();
        zzwh();
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzcay.mAppId);
        contentValues.put("name", zzcay.mName);
        contentValues.put("lifetime_count", Long.valueOf(zzcay.zzind));
        contentValues.put("current_bundle_count", Long.valueOf(zzcay.zzine));
        contentValues.put("last_fire_timestamp", Long.valueOf(zzcay.zzinf));
        try {
            if (getWritableDatabase().insertWithOnConflict("events", null, contentValues, 5) == -1) {
                zzauk().zzayc().zzj("Failed to insert/update event aggregates (got -1). appId", zzcbo.zzjf(zzcay.mAppId));
            }
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Error storing event aggregates. appId", zzcbo.zzjf(zzcay.mAppId), e);
        }
    }

    @WorkerThread
    final void zza(String str, zzcfp[] zzcfpArr) {
        int i = 0;
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzbp.zzu(zzcfpArr);
        SQLiteDatabase writableDatabase = getWritableDatabase();
        writableDatabase.beginTransaction();
        zzwh();
        zzug();
        zzbp.zzgf(str);
        SQLiteDatabase writableDatabase2 = getWritableDatabase();
        writableDatabase2.delete("property_filters", "app_id=?", new String[]{str});
        writableDatabase2.delete("event_filters", "app_id=?", new String[]{str});
        int length = zzcfpArr.length;
        int i2 = 0;
        while (i2 < length) {
            zzcfp zzcfp = zzcfpArr[i2];
            try {
                zzwh();
                zzug();
                zzbp.zzgf(str);
                zzbp.zzu(zzcfp);
                zzbp.zzu(zzcfp.zzixg);
                zzbp.zzu(zzcfp.zzixf);
                if (zzcfp.zzixe == null) {
                    zzauk().zzaye().zzj("Audience with no ID. appId", zzcbo.zzjf(str));
                } else {
                    int i3;
                    int intValue = zzcfp.zzixe.intValue();
                    for (zzcfq zzcfq : zzcfp.zzixg) {
                        if (zzcfq.zzixi == null) {
                            zzauk().zzaye().zze("Event filter with no ID. Audience definition ignored. appId, audienceId", zzcbo.zzjf(str), zzcfp.zzixe);
                            break;
                        }
                    }
                    for (zzcft zzcft : zzcfp.zzixf) {
                        if (zzcft.zzixi == null) {
                            zzauk().zzaye().zze("Property filter with no ID. Audience definition ignored. appId, audienceId", zzcbo.zzjf(str), zzcfp.zzixe);
                            break;
                        }
                    }
                    for (zzcfq zzcfq2 : zzcfp.zzixg) {
                        if (!zza(str, intValue, zzcfq2)) {
                            i3 = 0;
                            break;
                        }
                    }
                    i3 = 1;
                    if (i3 != 0) {
                        for (zzcft zzcft2 : zzcfp.zzixf) {
                            if (!zza(str, intValue, zzcft2)) {
                                i3 = 0;
                                break;
                            }
                        }
                    }
                    if (i3 == 0) {
                        zzwh();
                        zzug();
                        zzbp.zzgf(str);
                        writableDatabase2 = getWritableDatabase();
                        writableDatabase2.delete("property_filters", "app_id=? and audience_id=?", new String[]{str, String.valueOf(intValue)});
                        writableDatabase2.delete("event_filters", "app_id=? and audience_id=?", new String[]{str, String.valueOf(intValue)});
                    }
                }
                i2++;
            } finally {
                writableDatabase.endTransaction();
            }
        }
        List arrayList = new ArrayList();
        int length2 = zzcfpArr.length;
        while (i < length2) {
            arrayList.add(zzcfpArr[i].zzixe);
            i++;
        }
        zzc(str, arrayList);
        writableDatabase.setTransactionSuccessful();
    }

    @WorkerThread
    public final boolean zza(zzcan zzcan) {
        zzbp.zzu(zzcan);
        zzug();
        zzwh();
        if (zzaj(zzcan.packageName, zzcan.zzima.name) == null) {
            long zzb = zzb("SELECT COUNT(1) FROM conditional_properties WHERE app_id=?", new String[]{zzcan.packageName});
            zzcap.zzawa();
            if (zzb >= 1000) {
                return false;
            }
        }
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzcan.packageName);
        contentValues.put(Param.ORIGIN, zzcan.zzilz);
        contentValues.put("name", zzcan.zzima.name);
        zza(contentValues, Param.VALUE, zzcan.zzima.getValue());
        contentValues.put("active", Boolean.valueOf(zzcan.zzimc));
        contentValues.put("trigger_event_name", zzcan.zzimd);
        contentValues.put("trigger_timeout", Long.valueOf(zzcan.zzimf));
        zzaug();
        contentValues.put("timed_out_event", zzcfo.zza(zzcan.zzime));
        contentValues.put("creation_timestamp", Long.valueOf(zzcan.zzimb));
        zzaug();
        contentValues.put("triggered_event", zzcfo.zza(zzcan.zzimg));
        contentValues.put("triggered_timestamp", Long.valueOf(zzcan.zzima.zziwu));
        contentValues.put("time_to_live", Long.valueOf(zzcan.zzimh));
        zzaug();
        contentValues.put("expired_event", zzcfo.zza(zzcan.zzimi));
        try {
            if (getWritableDatabase().insertWithOnConflict("conditional_properties", null, contentValues, 5) == -1) {
                zzauk().zzayc().zzj("Failed to insert/update conditional user property (got -1)", zzcbo.zzjf(zzcan.packageName));
            }
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Error storing conditional user property", zzcbo.zzjf(zzcan.packageName), e);
        }
        return true;
    }

    public final boolean zza(zzcax zzcax, long j, boolean z) {
        zzug();
        zzwh();
        zzbp.zzu(zzcax);
        zzbp.zzgf(zzcax.mAppId);
        zzego zzcfz = new zzcfz();
        zzcfz.zziyu = Long.valueOf(zzcax.zzinb);
        zzcfz.zziys = new zzcga[zzcax.zzinc.size()];
        Iterator it = zzcax.zzinc.iterator();
        int i = 0;
        while (it.hasNext()) {
            String str = (String) it.next();
            zzcga zzcga = new zzcga();
            zzcfz.zziys[i] = zzcga;
            zzcga.name = str;
            zzaug().zza(zzcga, zzcax.zzinc.get(str));
            i++;
        }
        try {
            byte[] bArr = new byte[zzcfz.zzbjo()];
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzcfz.zza(zzi);
            zzi.zzccd();
            zzauk().zzayi().zze("Saving event, name, data size", zzauf().zzjc(zzcax.mName), Integer.valueOf(bArr.length));
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", zzcax.mAppId);
            contentValues.put("name", zzcax.mName);
            contentValues.put(AppMeasurement.Param.TIMESTAMP, Long.valueOf(zzcax.zzfcw));
            contentValues.put("metadata_fingerprint", Long.valueOf(j));
            contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, bArr);
            contentValues.put("realtime", Integer.valueOf(z ? 1 : 0));
            try {
                if (getWritableDatabase().insert("raw_events", null, contentValues) != -1) {
                    return true;
                }
                zzauk().zzayc().zzj("Failed to insert raw event (got -1). appId", zzcbo.zzjf(zzcax.mAppId));
                return false;
            } catch (SQLiteException e) {
                zzauk().zzayc().zze("Error storing raw event. appId", zzcbo.zzjf(zzcax.mAppId), e);
                return false;
            }
        } catch (IOException e2) {
            zzauk().zzayc().zze("Data loss. Failed to serialize event params/data. appId", zzcbo.zzjf(zzcax.mAppId), e2);
            return false;
        }
    }

    @WorkerThread
    public final boolean zza(zzcfn zzcfn) {
        zzbp.zzu(zzcfn);
        zzug();
        zzwh();
        if (zzaj(zzcfn.mAppId, zzcfn.mName) == null) {
            long zzb;
            if (zzcfo.zzju(zzcfn.mName)) {
                zzb = zzb("select count(1) from user_attributes where app_id=? and name not like '!_%' escape '!'", new String[]{zzcfn.mAppId});
                zzcap.zzavx();
                if (zzb >= 25) {
                    return false;
                }
            }
            zzb = zzb("select count(1) from user_attributes where app_id=? and origin=? AND name like '!_%' escape '!'", new String[]{zzcfn.mAppId, zzcfn.mOrigin});
            zzcap.zzavz();
            if (zzb >= 25) {
                return false;
            }
        }
        ContentValues contentValues = new ContentValues();
        contentValues.put("app_id", zzcfn.mAppId);
        contentValues.put(Param.ORIGIN, zzcfn.mOrigin);
        contentValues.put("name", zzcfn.mName);
        contentValues.put("set_timestamp", Long.valueOf(zzcfn.zziwy));
        zza(contentValues, Param.VALUE, zzcfn.mValue);
        try {
            if (getWritableDatabase().insertWithOnConflict("user_attributes", null, contentValues, 5) == -1) {
                zzauk().zzayc().zzj("Failed to insert/update user property (got -1). appId", zzcbo.zzjf(zzcfn.mAppId));
            }
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Error storing user property. appId", zzcbo.zzjf(zzcfn.mAppId), e);
        }
        return true;
    }

    @WorkerThread
    public final boolean zza(zzcgc zzcgc, boolean z) {
        zzug();
        zzwh();
        zzbp.zzu(zzcgc);
        zzbp.zzgf(zzcgc.zzch);
        zzbp.zzu(zzcgc.zzize);
        zzaxj();
        long currentTimeMillis = zzvu().currentTimeMillis();
        if (zzcgc.zzize.longValue() < currentTimeMillis - zzcap.zzawl() || zzcgc.zzize.longValue() > zzcap.zzawl() + currentTimeMillis) {
            zzauk().zzaye().zzd("Storing bundle outside of the max uploading time span. appId, now, timestamp", zzcbo.zzjf(zzcgc.zzch), Long.valueOf(currentTimeMillis), zzcgc.zzize);
        }
        try {
            byte[] bArr = new byte[zzcgc.zzbjo()];
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzcgc.zza(zzi);
            zzi.zzccd();
            bArr = zzaug().zzo(bArr);
            zzauk().zzayi().zzj("Saving bundle, size", Integer.valueOf(bArr.length));
            ContentValues contentValues = new ContentValues();
            contentValues.put("app_id", zzcgc.zzch);
            contentValues.put("bundle_end_timestamp", zzcgc.zzize);
            contentValues.put(ShareConstants.WEB_DIALOG_PARAM_DATA, bArr);
            contentValues.put("has_realtime", Integer.valueOf(z ? 1 : 0));
            try {
                if (getWritableDatabase().insert("queue", null, contentValues) != -1) {
                    return true;
                }
                zzauk().zzayc().zzj("Failed to insert bundle (got -1). appId", zzcbo.zzjf(zzcgc.zzch));
                return false;
            } catch (SQLiteException e) {
                zzauk().zzayc().zze("Error storing bundle. appId", zzcbo.zzjf(zzcgc.zzch), e);
                return false;
            }
        } catch (IOException e2) {
            zzauk().zzayc().zze("Data loss. Failed to serialize bundle. appId", zzcbo.zzjf(zzcgc.zzch), e2);
            return false;
        }
    }

    public final void zzae(List<Long> list) {
        zzbp.zzu(list);
        zzug();
        zzwh();
        StringBuilder stringBuilder = new StringBuilder("rowid in (");
        for (int i = 0; i < list.size(); i++) {
            if (i != 0) {
                stringBuilder.append(",");
            }
            stringBuilder.append(((Long) list.get(i)).longValue());
        }
        stringBuilder.append(")");
        int delete = getWritableDatabase().delete("raw_events", stringBuilder.toString(), null);
        if (delete != list.size()) {
            zzauk().zzayc().zze("Deleted fewer rows from raw events table than expected", Integer.valueOf(delete), Integer.valueOf(list.size()));
        }
    }

    @WorkerThread
    public final zzcay zzah(String str, String str2) {
        Cursor query;
        Object e;
        Cursor cursor;
        Throwable th;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
        try {
            query = getWritableDatabase().query("events", new String[]{"lifetime_count", "current_bundle_count", "last_fire_timestamp"}, "app_id=? and name=?", new String[]{str, str2}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    zzcay zzcay = new zzcay(str, str2, query.getLong(0), query.getLong(1), query.getLong(2));
                    if (query.moveToNext()) {
                        zzauk().zzayc().zzj("Got multiple records for event aggregates, expected one. appId", zzcbo.zzjf(str));
                    }
                    if (query == null) {
                        return zzcay;
                    }
                    query.close();
                    return zzcay;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
                try {
                    zzauk().zzayc().zzd("Error querying events. appId", zzcbo.zzjf(str), zzauf().zzjc(str2), e);
                    if (cursor != null) {
                        cursor.close();
                    }
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    query = cursor;
                    if (query != null) {
                        query.close();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            zzauk().zzayc().zzd("Error querying events. appId", zzcbo.zzjf(str), zzauf().zzjc(str2), e);
            if (cursor != null) {
                cursor.close();
            }
            return null;
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
    }

    @WorkerThread
    public final void zzai(String str, String str2) {
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
        try {
            zzauk().zzayi().zzj("Deleted user attribute rows", Integer.valueOf(getWritableDatabase().delete("user_attributes", "app_id=? and name=?", new String[]{str, str2})));
        } catch (SQLiteException e) {
            zzauk().zzayc().zzd("Error deleting user attribute. appId", zzcbo.zzjf(str), zzauf().zzje(str2), e);
        }
    }

    @WorkerThread
    public final zzcfn zzaj(String str, String str2) {
        Object e;
        Cursor cursor;
        Throwable th;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
        Cursor query;
        try {
            query = getWritableDatabase().query("user_attributes", new String[]{"set_timestamp", Param.VALUE, Param.ORIGIN}, "app_id=? and name=?", new String[]{str, str2}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    String str3 = str;
                    zzcfn zzcfn = new zzcfn(str3, query.getString(2), str2, query.getLong(0), zza(query, 1));
                    if (query.moveToNext()) {
                        zzauk().zzayc().zzj("Got multiple records for user property, expected one. appId", zzcbo.zzjf(str));
                    }
                    if (query == null) {
                        return zzcfn;
                    }
                    query.close();
                    return zzcfn;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
                try {
                    zzauk().zzayc().zzd("Error querying user property. appId", zzcbo.zzjf(str), zzauf().zzje(str2), e);
                    if (cursor != null) {
                        cursor.close();
                    }
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    query = cursor;
                    if (query != null) {
                        query.close();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            zzauk().zzayc().zzd("Error querying user property. appId", zzcbo.zzjf(str), zzauf().zzje(str2), e);
            if (cursor != null) {
                cursor.close();
            }
            return null;
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
    }

    @WorkerThread
    public final zzcan zzak(String str, String str2) {
        Object e;
        Cursor cursor;
        Throwable th;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
        Cursor query;
        try {
            query = getWritableDatabase().query("conditional_properties", new String[]{Param.ORIGIN, Param.VALUE, "active", "trigger_event_name", "trigger_timeout", "timed_out_event", "creation_timestamp", "triggered_event", "triggered_timestamp", "time_to_live", "expired_event"}, "app_id=? and name=?", new String[]{str, str2}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    String string = query.getString(0);
                    Object zza = zza(query, 1);
                    boolean z = query.getInt(2) != 0;
                    String string2 = query.getString(3);
                    long j = query.getLong(4);
                    zzcbc zzcbc = (zzcbc) zzaug().zzb(query.getBlob(5), zzcbc.CREATOR);
                    long j2 = query.getLong(6);
                    zzcbc zzcbc2 = (zzcbc) zzaug().zzb(query.getBlob(7), zzcbc.CREATOR);
                    long j3 = query.getLong(8);
                    zzcan zzcan = new zzcan(str, string, new zzcfl(str2, j3, zza, string), j2, z, string2, zzcbc, j, zzcbc2, query.getLong(9), (zzcbc) zzaug().zzb(query.getBlob(10), zzcbc.CREATOR));
                    if (query.moveToNext()) {
                        zzauk().zzayc().zze("Got multiple records for conditional property, expected one", zzcbo.zzjf(str), zzauf().zzje(str2));
                    }
                    if (query == null) {
                        return zzcan;
                    }
                    query.close();
                    return zzcan;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
                try {
                    zzauk().zzayc().zzd("Error querying conditional property", zzcbo.zzjf(str), zzauf().zzje(str2), e);
                    if (cursor != null) {
                        cursor.close();
                    }
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    query = cursor;
                    if (query != null) {
                        query.close();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            zzauk().zzayc().zzd("Error querying conditional property", zzcbo.zzjf(str), zzauf().zzje(str2), e);
            if (cursor != null) {
                cursor.close();
            }
            return null;
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
    }

    @WorkerThread
    public final int zzal(String str, String str2) {
        int i = 0;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
        try {
            i = getWritableDatabase().delete("conditional_properties", "app_id=? and name=?", new String[]{str, str2});
        } catch (SQLiteException e) {
            zzauk().zzayc().zzd("Error deleting conditional property", zzcbo.zzjf(str), zzauf().zzje(str2), e);
        }
        return i;
    }

    final Map<Integer, List<zzcfq>> zzam(String str, String str2) {
        Object e;
        Throwable th;
        Cursor cursor = null;
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        Map<Integer, List<zzcfq>> arrayMap = new ArrayMap();
        Cursor query;
        try {
            query = getWritableDatabase().query("event_filters", new String[]{"audience_id", ShareConstants.WEB_DIALOG_PARAM_DATA}, "app_id=? AND event_name=?", new String[]{str, str2}, null, null, null);
            if (query.moveToFirst()) {
                do {
                    try {
                        byte[] blob = query.getBlob(1);
                        zzegf zzh = zzegf.zzh(blob, 0, blob.length);
                        zzego zzcfq = new zzcfq();
                        try {
                            zzcfq.zza(zzh);
                            int i = query.getInt(0);
                            List list = (List) arrayMap.get(Integer.valueOf(i));
                            if (list == null) {
                                list = new ArrayList();
                                arrayMap.put(Integer.valueOf(i), list);
                            }
                            list.add(zzcfq);
                        } catch (IOException e2) {
                            zzauk().zzayc().zze("Failed to merge filter. appId", zzcbo.zzjf(str), e2);
                        }
                    } catch (SQLiteException e3) {
                        e = e3;
                    }
                } while (query.moveToNext());
                if (query != null) {
                    query.close();
                }
                return arrayMap;
            }
            Map<Integer, List<zzcfq>> emptyMap = Collections.emptyMap();
            if (query == null) {
                return emptyMap;
            }
            query.close();
            return emptyMap;
        } catch (SQLiteException e4) {
            e = e4;
            query = null;
            try {
                zzauk().zzayc().zze("Database error querying filters. appId", zzcbo.zzjf(str), e);
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (Throwable th2) {
                th = th2;
                cursor = query;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    final Map<Integer, List<zzcft>> zzan(String str, String str2) {
        Object e;
        Throwable th;
        Cursor cursor = null;
        zzwh();
        zzug();
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        Map<Integer, List<zzcft>> arrayMap = new ArrayMap();
        Cursor query;
        try {
            query = getWritableDatabase().query("property_filters", new String[]{"audience_id", ShareConstants.WEB_DIALOG_PARAM_DATA}, "app_id=? AND property_name=?", new String[]{str, str2}, null, null, null);
            if (query.moveToFirst()) {
                do {
                    try {
                        byte[] blob = query.getBlob(1);
                        zzegf zzh = zzegf.zzh(blob, 0, blob.length);
                        zzego zzcft = new zzcft();
                        try {
                            zzcft.zza(zzh);
                            int i = query.getInt(0);
                            List list = (List) arrayMap.get(Integer.valueOf(i));
                            if (list == null) {
                                list = new ArrayList();
                                arrayMap.put(Integer.valueOf(i), list);
                            }
                            list.add(zzcft);
                        } catch (IOException e2) {
                            zzauk().zzayc().zze("Failed to merge filter", zzcbo.zzjf(str), e2);
                        }
                    } catch (SQLiteException e3) {
                        e = e3;
                    }
                } while (query.moveToNext());
                if (query != null) {
                    query.close();
                }
                return arrayMap;
            }
            Map<Integer, List<zzcft>> emptyMap = Collections.emptyMap();
            if (query == null) {
                return emptyMap;
            }
            query.close();
            return emptyMap;
        } catch (SQLiteException e4) {
            e = e4;
            query = null;
            try {
                zzauk().zzayc().zze("Database error querying filters. appId", zzcbo.zzjf(str), e);
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (Throwable th2) {
                th = th2;
                cursor = query;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    @WorkerThread
    protected final long zzao(String str, String str2) {
        long j;
        Object obj;
        zzbp.zzgf(str);
        zzbp.zzgf(str2);
        zzug();
        zzwh();
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
                    zzauk().zzayc().zze("Failed to insert column (got -1). appId", zzcbo.zzjf(str), str2);
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
                    zzauk().zzayc().zze("Failed to update column (got 0). appId", zzcbo.zzjf(str), str2);
                    writableDatabase.endTransaction();
                    return -1;
                }
                writableDatabase.setTransactionSuccessful();
                writableDatabase.endTransaction();
                return zza;
            } catch (SQLiteException e) {
                SQLiteException sQLiteException = e;
                j = zza;
                SQLiteException sQLiteException2 = sQLiteException;
                try {
                    zzauk().zzayc().zzd("Error inserting column. appId", zzcbo.zzjf(str), str2, obj);
                    return j;
                } finally {
                    writableDatabase.endTransaction();
                }
            }
        } catch (SQLiteException e2) {
            obj = e2;
            j = 0;
            zzauk().zzayc().zzd("Error inserting column. appId", zzcbo.zzjf(str), str2, obj);
            return j;
        }
    }

    @WorkerThread
    public final String zzaxh() {
        String string;
        Object e;
        Throwable th;
        Cursor cursor = null;
        Cursor rawQuery;
        try {
            rawQuery = getWritableDatabase().rawQuery("select app_id from queue order by has_realtime desc, rowid asc limit 1;", null);
            try {
                if (rawQuery.moveToFirst()) {
                    string = rawQuery.getString(0);
                    if (rawQuery != null) {
                        rawQuery.close();
                    }
                } else if (rawQuery != null) {
                    rawQuery.close();
                }
            } catch (SQLiteException e2) {
                e = e2;
                try {
                    zzauk().zzayc().zzj("Database error getting next bundle app id", e);
                    if (rawQuery != null) {
                        rawQuery.close();
                    }
                    return string;
                } catch (Throwable th2) {
                    th = th2;
                    cursor = rawQuery;
                    if (cursor != null) {
                        cursor.close();
                    }
                    throw th;
                }
            }
        } catch (SQLiteException e3) {
            e = e3;
            rawQuery = null;
            zzauk().zzayc().zzj("Database error getting next bundle app id", e);
            if (rawQuery != null) {
                rawQuery.close();
            }
            return string;
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
        return string;
    }

    public final boolean zzaxi() {
        return zzb("select count(1) > 0 from queue where has_realtime = 1", null) != 0;
    }

    @WorkerThread
    final void zzaxj() {
        zzug();
        zzwh();
        if (zzaxp()) {
            long j = zzaul().zziqj.get();
            long elapsedRealtime = zzvu().elapsedRealtime();
            if (Math.abs(elapsedRealtime - j) > zzcap.zzawm()) {
                zzaul().zziqj.set(elapsedRealtime);
                zzug();
                zzwh();
                if (zzaxp()) {
                    int delete = getWritableDatabase().delete("queue", "abs(bundle_end_timestamp - ?) > cast(? as integer)", new String[]{String.valueOf(zzvu().currentTimeMillis()), String.valueOf(zzcap.zzawl())});
                    if (delete > 0) {
                        zzauk().zzayi().zzj("Deleted stale rows. rowsDeleted", Integer.valueOf(delete));
                    }
                }
            }
        }
    }

    @WorkerThread
    public final long zzaxk() {
        return zza("select max(bundle_end_timestamp) from queue", null, 0);
    }

    @WorkerThread
    public final long zzaxl() {
        return zza("select max(timestamp) from raw_events", null, 0);
    }

    public final boolean zzaxm() {
        return zzb("select count(1) > 0 from raw_events", null) != 0;
    }

    public final boolean zzaxn() {
        return zzb("select count(1) > 0 from raw_events where realtime = 1", null) != 0;
    }

    public final long zzaxo() {
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
            zzauk().zzayc().zzj("Error querying raw events", e);
            if (cursor != null) {
                cursor.close();
            }
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
        }
        return j;
    }

    public final String zzba(long j) {
        Cursor rawQuery;
        String string;
        Object e;
        Throwable th;
        Cursor cursor = null;
        zzug();
        zzwh();
        try {
            rawQuery = getWritableDatabase().rawQuery("select app_id from apps where app_id in (select distinct app_id from raw_events) and config_fetched_time < ? order by failed_config_fetch_time limit 1;", new String[]{String.valueOf(j)});
            try {
                if (rawQuery.moveToFirst()) {
                    string = rawQuery.getString(0);
                    if (rawQuery != null) {
                        rawQuery.close();
                    }
                } else {
                    zzauk().zzayi().log("No expired configs for apps with pending events");
                    if (rawQuery != null) {
                        rawQuery.close();
                    }
                }
            } catch (SQLiteException e2) {
                e = e2;
                try {
                    zzauk().zzayc().zzj("Error selecting expired configs", e);
                    if (rawQuery != null) {
                        rawQuery.close();
                    }
                    return string;
                } catch (Throwable th2) {
                    th = th2;
                    cursor = rawQuery;
                    if (cursor != null) {
                        cursor.close();
                    }
                    throw th;
                }
            }
        } catch (SQLiteException e3) {
            e = e3;
            rawQuery = cursor;
            zzauk().zzayc().zzj("Error selecting expired configs", e);
            if (rawQuery != null) {
                rawQuery.close();
            }
            return string;
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
        return string;
    }

    public final List<zzcan> zzc(String str, String[] strArr) {
        Object e;
        Cursor cursor;
        Throwable th;
        zzug();
        zzwh();
        List<zzcan> arrayList = new ArrayList();
        Cursor query;
        try {
            SQLiteDatabase writableDatabase = getWritableDatabase();
            zzcap.zzawa();
            query = writableDatabase.query("conditional_properties", new String[]{"app_id", Param.ORIGIN, "name", Param.VALUE, "active", "trigger_event_name", "trigger_timeout", "timed_out_event", "creation_timestamp", "triggered_event", "triggered_timestamp", "time_to_live", "expired_event"}, str, strArr, null, null, "rowid", "1001");
            try {
                if (query.moveToFirst()) {
                    do {
                        if (arrayList.size() >= zzcap.zzawa()) {
                            zzauk().zzayc().zzj("Read more than the max allowed conditional properties, ignoring extra", Integer.valueOf(zzcap.zzawa()));
                            break;
                        }
                        String string = query.getString(0);
                        String string2 = query.getString(1);
                        String string3 = query.getString(2);
                        Object zza = zza(query, 3);
                        boolean z = query.getInt(4) != 0;
                        String string4 = query.getString(5);
                        long j = query.getLong(6);
                        zzcbc zzcbc = (zzcbc) zzaug().zzb(query.getBlob(7), zzcbc.CREATOR);
                        long j2 = query.getLong(8);
                        zzcbc zzcbc2 = (zzcbc) zzaug().zzb(query.getBlob(9), zzcbc.CREATOR);
                        long j3 = query.getLong(10);
                        List<zzcan> list = arrayList;
                        list.add(new zzcan(string, string2, new zzcfl(string3, j3, zza, string2), j2, z, string4, zzcbc, j, zzcbc2, query.getLong(11), (zzcbc) zzaug().zzb(query.getBlob(12), zzcbc.CREATOR)));
                    } while (query.moveToNext());
                    if (query != null) {
                        query.close();
                    }
                    return arrayList;
                }
                if (query != null) {
                    query.close();
                }
                return arrayList;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
            } catch (Throwable th2) {
                th = th2;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            try {
                zzauk().zzayc().zzj("Error querying conditional user property value", e);
                List<zzcan> emptyList = Collections.emptyList();
                if (cursor == null) {
                    return emptyList;
                }
                cursor.close();
                return emptyList;
            } catch (Throwable th3) {
                th = th3;
                query = cursor;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    @android.support.annotation.WorkerThread
    public final java.util.List<com.google.android.gms.internal.zzcfn> zzg(java.lang.String r12, java.lang.String r13, java.lang.String r14) {
        /*
        r11 = this;
        r10 = 0;
        com.google.android.gms.common.internal.zzbp.zzgf(r12);
        r11.zzug();
        r11.zzwh();
        r9 = new java.util.ArrayList;
        r9.<init>();
        r0 = new java.util.ArrayList;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r1 = 3;
        r0.<init>(r1);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r0.add(r12);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r1 = new java.lang.StringBuilder;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r2 = "app_id=?";
        r1.<init>(r2);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r2 = android.text.TextUtils.isEmpty(r13);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        if (r2 != 0) goto L_0x002d;
    L_0x0025:
        r0.add(r13);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r2 = " and origin=?";
        r1.append(r2);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
    L_0x002d:
        r2 = android.text.TextUtils.isEmpty(r14);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        if (r2 != 0) goto L_0x0045;
    L_0x0033:
        r2 = java.lang.String.valueOf(r14);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r3 = "*";
        r2 = r2.concat(r3);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r0.add(r2);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r2 = " and name glob ?";
        r1.append(r2);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
    L_0x0045:
        r2 = r0.size();	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r2 = new java.lang.String[r2];	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r4 = r0.toArray(r2);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r4 = (java.lang.String[]) r4;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r0 = r11.getWritableDatabase();	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r3 = r1.toString();	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        com.google.android.gms.internal.zzcap.zzavy();	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r1 = "user_attributes";
        r2 = 4;
        r2 = new java.lang.String[r2];	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r5 = 0;
        r6 = "name";
        r2[r5] = r6;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r5 = 1;
        r6 = "set_timestamp";
        r2[r5] = r6;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r5 = 2;
        r6 = "value";
        r2[r5] = r6;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r5 = 3;
        r6 = "origin";
        r2[r5] = r6;	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r5 = 0;
        r6 = 0;
        r7 = "rowid";
        r8 = "1001";
        r7 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8);	 Catch:{ SQLiteException -> 0x0111, all -> 0x0106 }
        r0 = r7.moveToFirst();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        if (r0 != 0) goto L_0x008d;
    L_0x0085:
        if (r7 == 0) goto L_0x008a;
    L_0x0087:
        r7.close();
    L_0x008a:
        r0 = r9;
    L_0x008b:
        return r0;
    L_0x008c:
        r13 = r2;
    L_0x008d:
        r0 = r9.size();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r1 = com.google.android.gms.internal.zzcap.zzavy();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        if (r0 < r1) goto L_0x00b3;
    L_0x0097:
        r0 = r11.zzauk();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r0 = r0.zzayc();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r1 = "Read more than the max allowed user properties, ignoring excess";
        r2 = com.google.android.gms.internal.zzcap.zzavy();	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r2 = java.lang.Integer.valueOf(r2);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r0.zzj(r1, r2);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
    L_0x00ac:
        if (r7 == 0) goto L_0x00b1;
    L_0x00ae:
        r7.close();
    L_0x00b1:
        r0 = r9;
        goto L_0x008b;
    L_0x00b3:
        r0 = 0;
        r3 = r7.getString(r0);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r0 = 1;
        r4 = r7.getLong(r0);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r0 = 2;
        r6 = r11.zza(r7, r0);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        r0 = 3;
        r2 = r7.getString(r0);	 Catch:{ SQLiteException -> 0x0114, all -> 0x0117 }
        if (r6 != 0) goto L_0x00e1;
    L_0x00c9:
        r0 = r11.zzauk();	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        r0 = r0.zzayc();	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        r1 = "(2)Read invalid user property value, ignoring it";
        r3 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        r0.zzd(r1, r3, r2, r14);	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
    L_0x00da:
        r0 = r7.moveToNext();	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        if (r0 != 0) goto L_0x008c;
    L_0x00e0:
        goto L_0x00ac;
    L_0x00e1:
        r0 = new com.google.android.gms.internal.zzcfn;	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        r1 = r12;
        r0.<init>(r1, r2, r3, r4, r6);	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        r9.add(r0);	 Catch:{ SQLiteException -> 0x00eb, all -> 0x0117 }
        goto L_0x00da;
    L_0x00eb:
        r0 = move-exception;
        r1 = r7;
        r13 = r2;
    L_0x00ee:
        r2 = r11.zzauk();	 Catch:{ all -> 0x010e }
        r2 = r2.zzayc();	 Catch:{ all -> 0x010e }
        r3 = "(2)Error querying user properties";
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ all -> 0x010e }
        r2.zzd(r3, r4, r13, r0);	 Catch:{ all -> 0x010e }
        if (r1 == 0) goto L_0x0104;
    L_0x0101:
        r1.close();
    L_0x0104:
        r0 = r10;
        goto L_0x008b;
    L_0x0106:
        r0 = move-exception;
        r7 = r10;
    L_0x0108:
        if (r7 == 0) goto L_0x010d;
    L_0x010a:
        r7.close();
    L_0x010d:
        throw r0;
    L_0x010e:
        r0 = move-exception;
        r7 = r1;
        goto L_0x0108;
    L_0x0111:
        r0 = move-exception;
        r1 = r10;
        goto L_0x00ee;
    L_0x0114:
        r0 = move-exception;
        r1 = r7;
        goto L_0x00ee;
    L_0x0117:
        r0 = move-exception;
        goto L_0x0108;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcaq.zzg(java.lang.String, java.lang.String, java.lang.String):java.util.List<com.google.android.gms.internal.zzcfn>");
    }

    @WorkerThread
    public final List<zzcan> zzh(String str, String str2, String str3) {
        zzbp.zzgf(str);
        zzug();
        zzwh();
        List arrayList = new ArrayList(3);
        arrayList.add(str);
        StringBuilder stringBuilder = new StringBuilder("app_id=?");
        if (!TextUtils.isEmpty(str2)) {
            arrayList.add(str2);
            stringBuilder.append(" and origin=?");
        }
        if (!TextUtils.isEmpty(str3)) {
            arrayList.add(String.valueOf(str3).concat("*"));
            stringBuilder.append(" and name glob ?");
        }
        return zzc(stringBuilder.toString(), (String[]) arrayList.toArray(new String[arrayList.size()]));
    }

    @WorkerThread
    public final List<zzcfn> zziv(String str) {
        Cursor query;
        Object e;
        Cursor cursor;
        Throwable th;
        zzbp.zzgf(str);
        zzug();
        zzwh();
        List<zzcfn> arrayList = new ArrayList();
        try {
            String[] strArr = new String[]{"name", Param.ORIGIN, "set_timestamp", Param.VALUE};
            String[] strArr2 = new String[]{str};
            query = getWritableDatabase().query("user_attributes", strArr, "app_id=?", strArr2, null, null, "rowid", String.valueOf(zzcap.zzavy()));
            try {
                if (query.moveToFirst()) {
                    do {
                        String string = query.getString(0);
                        String string2 = query.getString(1);
                        if (string2 == null) {
                            string2 = "";
                        }
                        long j = query.getLong(2);
                        Object zza = zza(query, 3);
                        if (zza == null) {
                            zzauk().zzayc().zzj("Read invalid user property value, ignoring it. appId", zzcbo.zzjf(str));
                        } else {
                            arrayList.add(new zzcfn(str, string2, string, j, zza));
                        }
                    } while (query.moveToNext());
                    if (query != null) {
                        query.close();
                    }
                    return arrayList;
                }
                if (query != null) {
                    query.close();
                }
                return arrayList;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
            } catch (Throwable th2) {
                th = th2;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            try {
                zzauk().zzayc().zze("Error querying user properties. appId", zzcbo.zzjf(str), e);
                if (cursor != null) {
                    cursor.close();
                }
                return null;
            } catch (Throwable th3) {
                th = th3;
                query = cursor;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
    }

    @WorkerThread
    public final zzcaj zziw(String str) {
        Object e;
        Cursor cursor;
        Throwable th;
        Cursor cursor2 = null;
        zzbp.zzgf(str);
        zzug();
        zzwh();
        try {
            Cursor query = getWritableDatabase().query("apps", new String[]{"app_instance_id", "gmp_app_id", "resettable_device_id_hash", "last_bundle_index", "last_bundle_start_timestamp", "last_bundle_end_timestamp", "app_version", "app_store", "gmp_version", "dev_cert_hash", "measurement_enabled", "day", "daily_public_events_count", "daily_events_count", "daily_conversions_count", "config_fetched_time", "failed_config_fetch_time", "app_version_int", "firebase_instance_id", "daily_error_events_count", "daily_realtime_events_count", "health_monitor_sample", "android_id"}, "app_id=?", new String[]{str}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    zzcaj zzcaj = new zzcaj(this.zzikb, str);
                    zzcaj.zzim(query.getString(0));
                    zzcaj.zzin(query.getString(1));
                    zzcaj.zzio(query.getString(2));
                    zzcaj.zzaq(query.getLong(3));
                    zzcaj.zzal(query.getLong(4));
                    zzcaj.zzam(query.getLong(5));
                    zzcaj.setAppVersion(query.getString(6));
                    zzcaj.zziq(query.getString(7));
                    zzcaj.zzao(query.getLong(8));
                    zzcaj.zzap(query.getLong(9));
                    zzcaj.setMeasurementEnabled((query.isNull(10) ? 1 : query.getInt(10)) != 0);
                    zzcaj.zzat(query.getLong(11));
                    zzcaj.zzau(query.getLong(12));
                    zzcaj.zzav(query.getLong(13));
                    zzcaj.zzaw(query.getLong(14));
                    zzcaj.zzar(query.getLong(15));
                    zzcaj.zzas(query.getLong(16));
                    zzcaj.zzan(query.isNull(17) ? -2147483648L : (long) query.getInt(17));
                    zzcaj.zzip(query.getString(18));
                    zzcaj.zzay(query.getLong(19));
                    zzcaj.zzax(query.getLong(20));
                    zzcaj.zzir(query.getString(21));
                    zzcaj.zzaz(query.isNull(22) ? 0 : query.getLong(22));
                    zzcaj.zzaun();
                    if (query.moveToNext()) {
                        zzauk().zzayc().zzj("Got multiple records for app, expected one. appId", zzcbo.zzjf(str));
                    }
                    if (query == null) {
                        return zzcaj;
                    }
                    query.close();
                    return zzcaj;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (SQLiteException e2) {
                e = e2;
                cursor = query;
                try {
                    zzauk().zzayc().zze("Error querying app. appId", zzcbo.zzjf(str), e);
                    if (cursor != null) {
                        cursor.close();
                    }
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    query = cursor;
                    cursor2 = query;
                    if (cursor2 != null) {
                        cursor2.close();
                    }
                    throw th;
                }
            } catch (Throwable th3) {
                th = th3;
                cursor2 = query;
                if (cursor2 != null) {
                    cursor2.close();
                }
                throw th;
            }
        } catch (SQLiteException e3) {
            e = e3;
            cursor = null;
            zzauk().zzayc().zze("Error querying app. appId", zzcbo.zzjf(str), e);
            if (cursor != null) {
                cursor.close();
            }
            return null;
        } catch (Throwable th4) {
            th = th4;
            if (cursor2 != null) {
                cursor2.close();
            }
            throw th;
        }
    }

    public final long zzix(String str) {
        zzbp.zzgf(str);
        zzug();
        zzwh();
        try {
            return (long) getWritableDatabase().delete("raw_events", "rowid in (select rowid from raw_events where app_id=? order by rowid desc limit -1 offset ?)", new String[]{str, String.valueOf(Math.max(0, Math.min(1000000, zzaum().zzb(str, zzcbe.zzioh))))});
        } catch (SQLiteException e) {
            zzauk().zzayc().zze("Error deleting over the limit events. appId", zzcbo.zzjf(str), e);
            return 0;
        }
    }

    @WorkerThread
    public final byte[] zziy(String str) {
        Cursor query;
        Object e;
        Throwable th;
        Cursor cursor = null;
        zzbp.zzgf(str);
        zzug();
        zzwh();
        try {
            query = getWritableDatabase().query("apps", new String[]{"remote_config"}, "app_id=?", new String[]{str}, null, null, null);
            try {
                if (query.moveToFirst()) {
                    byte[] blob = query.getBlob(0);
                    if (query.moveToNext()) {
                        zzauk().zzayc().zzj("Got multiple records for app config, expected one. appId", zzcbo.zzjf(str));
                    }
                    if (query == null) {
                        return blob;
                    }
                    query.close();
                    return blob;
                }
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (SQLiteException e2) {
                e = e2;
                try {
                    zzauk().zzayc().zze("Error querying remote config. appId", zzcbo.zzjf(str), e);
                    if (query != null) {
                        query.close();
                    }
                    return null;
                } catch (Throwable th2) {
                    th = th2;
                    cursor = query;
                    if (cursor != null) {
                        cursor.close();
                    }
                    throw th;
                }
            }
        } catch (SQLiteException e3) {
            e = e3;
            query = null;
            zzauk().zzayc().zze("Error querying remote config. appId", zzcbo.zzjf(str), e);
            if (query != null) {
                query.close();
            }
            return null;
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    final Map<Integer, zzcgd> zziz(String str) {
        Object e;
        Throwable th;
        Cursor cursor = null;
        zzwh();
        zzug();
        zzbp.zzgf(str);
        Cursor query;
        try {
            query = getWritableDatabase().query("audience_filter_values", new String[]{"audience_id", "current_results"}, "app_id=?", new String[]{str}, null, null, null);
            if (query.moveToFirst()) {
                Map<Integer, zzcgd> arrayMap = new ArrayMap();
                do {
                    int i = query.getInt(0);
                    byte[] blob = query.getBlob(1);
                    zzegf zzh = zzegf.zzh(blob, 0, blob.length);
                    zzego zzcgd = new zzcgd();
                    try {
                        zzcgd.zza(zzh);
                        try {
                            arrayMap.put(Integer.valueOf(i), zzcgd);
                        } catch (SQLiteException e2) {
                            e = e2;
                        }
                    } catch (IOException e3) {
                        zzauk().zzayc().zzd("Failed to merge filter results. appId, audienceId, error", zzcbo.zzjf(str), Integer.valueOf(i), e3);
                    }
                } while (query.moveToNext());
                if (query == null) {
                    return arrayMap;
                }
                query.close();
                return arrayMap;
            }
            if (query != null) {
                query.close();
            }
            return null;
        } catch (SQLiteException e4) {
            e = e4;
            query = null;
            try {
                zzauk().zzayc().zze("Database error querying filter results. appId", zzcbo.zzjf(str), e);
                if (query != null) {
                    query.close();
                }
                return null;
            } catch (Throwable th2) {
                th = th2;
                cursor = query;
                if (cursor != null) {
                    cursor.close();
                }
                throw th;
            }
        } catch (Throwable th3) {
            th = th3;
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
    }

    public final long zzja(String str) {
        zzbp.zzgf(str);
        return zza("select count(1) from events where app_id=? and name not like '!_%' escape '!'", new String[]{str}, 0);
    }

    @WorkerThread
    public final List<Pair<zzcgc, Long>> zzl(String str, int i, int i2) {
        List<Pair<zzcgc, Long>> arrayList;
        Object e;
        Cursor cursor;
        Throwable th;
        boolean z = true;
        zzug();
        zzwh();
        zzbp.zzbh(i > 0);
        if (i2 <= 0) {
            z = false;
        }
        zzbp.zzbh(z);
        zzbp.zzgf(str);
        Cursor query;
        try {
            query = getWritableDatabase().query("queue", new String[]{"rowid", ShareConstants.WEB_DIALOG_PARAM_DATA}, "app_id=?", new String[]{str}, null, null, "rowid", String.valueOf(i));
            try {
                if (query.moveToFirst()) {
                    arrayList = new ArrayList();
                    int i3 = 0;
                    while (true) {
                        long j = query.getLong(0);
                        int length;
                        try {
                            byte[] zzp = zzaug().zzp(query.getBlob(1));
                            if (!arrayList.isEmpty() && zzp.length + i3 > i2) {
                                break;
                            }
                            zzegf zzh = zzegf.zzh(zzp, 0, zzp.length);
                            zzego zzcgc = new zzcgc();
                            try {
                                zzcgc.zza(zzh);
                                length = zzp.length + i3;
                                arrayList.add(Pair.create(zzcgc, Long.valueOf(j)));
                            } catch (IOException e2) {
                                zzauk().zzayc().zze("Failed to merge queued bundle. appId", zzcbo.zzjf(str), e2);
                                length = i3;
                            }
                            if (!query.moveToNext() || length > i2) {
                                break;
                            }
                            i3 = length;
                        } catch (IOException e22) {
                            zzauk().zzayc().zze("Failed to unzip queued bundle. appId", zzcbo.zzjf(str), e22);
                            length = i3;
                        }
                    }
                    if (query != null) {
                        query.close();
                    }
                } else {
                    arrayList = Collections.emptyList();
                    if (query != null) {
                        query.close();
                    }
                }
            } catch (SQLiteException e3) {
                e = e3;
                cursor = query;
            } catch (Throwable th2) {
                th = th2;
            }
        } catch (SQLiteException e4) {
            e = e4;
            cursor = null;
            try {
                zzauk().zzayc().zze("Error querying bundles. appId", zzcbo.zzjf(str), e);
                arrayList = Collections.emptyList();
                if (cursor != null) {
                    cursor.close();
                }
                return arrayList;
            } catch (Throwable th3) {
                th = th3;
                query = cursor;
                if (query != null) {
                    query.close();
                }
                throw th;
            }
        } catch (Throwable th4) {
            th = th4;
            query = null;
            if (query != null) {
                query.close();
            }
            throw th;
        }
        return arrayList;
    }

    protected final void zzuh() {
    }
}
