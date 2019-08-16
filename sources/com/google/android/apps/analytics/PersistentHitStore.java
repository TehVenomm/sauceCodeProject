package com.google.android.apps.analytics;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import java.util.HashSet;
import java.util.Map;
import java.util.Random;
import p017io.fabric.sdk.android.services.settings.SettingsJsonConstants;

class PersistentHitStore implements HitStore {
    private static final String ACCOUNT_ID = "account_id";
    private static final String ACTION = "action";
    private static final String CATEGORY = "category";
    /* access modifiers changed from: private */
    public static final String CREATE_CUSTOM_VARIABLES_TABLE = ("CREATE TABLE custom_variables (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{CUSTOMVAR_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{EVENT_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{CUSTOMVAR_INDEX}) + String.format(" '%s' CHAR(64) NOT NULL,", new Object[]{CUSTOMVAR_NAME}) + String.format(" '%s' CHAR(64) NOT NULL,", new Object[]{CUSTOMVAR_VALUE}) + String.format(" '%s' INTEGER NOT NULL);", new Object[]{CUSTOMVAR_SCOPE}));
    /* access modifiers changed from: private */
    public static final String CREATE_CUSTOM_VAR_CACHE_TABLE = ("CREATE TABLE IF NOT EXISTS custom_var_cache (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{CUSTOMVAR_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{EVENT_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{CUSTOMVAR_INDEX}) + String.format(" '%s' CHAR(64) NOT NULL,", new Object[]{CUSTOMVAR_NAME}) + String.format(" '%s' CHAR(64) NOT NULL,", new Object[]{CUSTOMVAR_VALUE}) + String.format(" '%s' INTEGER NOT NULL);", new Object[]{CUSTOMVAR_SCOPE}));
    /* access modifiers changed from: private */
    public static final String CREATE_EVENTS_TABLE = ("CREATE TABLE events (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{EVENT_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{"user_id"}) + String.format(" '%s' CHAR(256) NOT NULL,", new Object[]{ACCOUNT_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{RANDOM_VAL}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{TIMESTAMP_FIRST}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{TIMESTAMP_PREVIOUS}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{TIMESTAMP_CURRENT}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{VISITS}) + String.format(" '%s' CHAR(256) NOT NULL,", new Object[]{CATEGORY}) + String.format(" '%s' CHAR(256) NOT NULL,", new Object[]{"action"}) + String.format(" '%s' CHAR(256), ", new Object[]{LABEL}) + String.format(" '%s' INTEGER,", new Object[]{"value"}) + String.format(" '%s' INTEGER,", new Object[]{SCREEN_WIDTH}) + String.format(" '%s' INTEGER);", new Object[]{SCREEN_HEIGHT}));
    /* access modifiers changed from: private */
    public static final String CREATE_HITS_TABLE = ("CREATE TABLE IF NOT EXISTS hits (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{HIT_ID}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{HIT_STRING}) + String.format(" '%s' INTEGER NOT NULL);", new Object[]{HIT_TIMESTAMP}));
    private static final String CREATE_INSTALL_REFERRER_TABLE = "CREATE TABLE install_referrer (referrer TEXT PRIMARY KEY NOT NULL);";
    /* access modifiers changed from: private */
    public static final String CREATE_ITEM_EVENTS_TABLE = ("CREATE TABLE item_events (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{"item_id"}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{EVENT_ID}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{ORDER_ID}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{ITEM_SKU}) + String.format(" '%s' TEXT,", new Object[]{"item_name"}) + String.format(" '%s' TEXT,", new Object[]{"item_category"}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{ITEM_PRICE}) + String.format(" '%s' TEXT NOT NULL);", new Object[]{ITEM_COUNT}));
    private static final String CREATE_REFERRER_TABLE = "CREATE TABLE IF NOT EXISTS referrer (referrer TEXT PRIMARY KEY NOT NULL,timestamp_referrer INTEGER NOT NULL,referrer_visit INTEGER NOT NULL DEFAULT 1,referrer_index INTEGER NOT NULL DEFAULT 1);";
    /* access modifiers changed from: private */
    public static final String CREATE_SESSION_TABLE = ("CREATE TABLE IF NOT EXISTS session (" + String.format(" '%s' INTEGER PRIMARY KEY,", new Object[]{TIMESTAMP_FIRST}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{TIMESTAMP_PREVIOUS}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{TIMESTAMP_CURRENT}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{VISITS}) + String.format(" '%s' INTEGER NOT NULL);", new Object[]{STORE_ID}));
    /* access modifiers changed from: private */
    public static final String CREATE_TRANSACTION_EVENTS_TABLE = ("CREATE TABLE transaction_events (" + String.format(" '%s' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,", new Object[]{TRANSACTION_ID}) + String.format(" '%s' INTEGER NOT NULL,", new Object[]{EVENT_ID}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{ORDER_ID}) + String.format(" '%s' TEXT,", new Object[]{STORE_NAME}) + String.format(" '%s' TEXT NOT NULL,", new Object[]{TOTAL_COST}) + String.format(" '%s' TEXT,", new Object[]{TOTAL_TAX}) + String.format(" '%s' TEXT);", new Object[]{SHIPPING_COST}));
    private static final String CUSTOMVAR_ID = "cv_id";
    private static final String CUSTOMVAR_INDEX = "cv_index";
    private static final String CUSTOMVAR_NAME = "cv_name";
    private static final String CUSTOMVAR_SCOPE = "cv_scope";
    private static final String CUSTOMVAR_VALUE = "cv_value";
    private static final String CUSTOM_VARIABLE_COLUMN_TYPE = "CHAR(64) NOT NULL";
    private static final String DATABASE_NAME = "google_analytics.db";
    private static final int DATABASE_VERSION = 5;
    private static final String EVENT_ID = "event_id";
    private static final String HIT_ID = "hit_id";
    private static final String HIT_STRING = "hit_string";
    private static final String HIT_TIMESTAMP = "hit_time";
    private static final String ITEM_CATEGORY = "item_category";
    private static final String ITEM_COUNT = "item_count";
    private static final String ITEM_ID = "item_id";
    private static final String ITEM_NAME = "item_name";
    private static final String ITEM_PRICE = "item_price";
    private static final String ITEM_SKU = "item_sku";
    private static final String LABEL = "label";
    private static final int MAX_HITS = 1000;
    private static final String ORDER_ID = "order_id";
    private static final String RANDOM_VAL = "random_val";
    static final String REFERRER = "referrer";
    static final String REFERRER_COLUMN = "referrer";
    static final String REFERRER_INDEX = "referrer_index";
    static final String REFERRER_VISIT = "referrer_visit";
    private static final String SCREEN_HEIGHT = "screen_height";
    private static final String SCREEN_WIDTH = "screen_width";
    private static final String SHIPPING_COST = "tran_shippingcost";
    private static final String STORE_ID = "store_id";
    private static final String STORE_NAME = "tran_storename";
    private static final String TIMESTAMP_CURRENT = "timestamp_current";
    private static final String TIMESTAMP_FIRST = "timestamp_first";
    private static final String TIMESTAMP_PREVIOUS = "timestamp_previous";
    static final String TIMESTAMP_REFERRER = "timestamp_referrer";
    private static final String TOTAL_COST = "tran_totalcost";
    private static final String TOTAL_TAX = "tran_totaltax";
    private static final String TRANSACTION_ID = "tran_id";
    private static final String USER_ID = "user_id";
    private static final String VALUE = "value";
    private static final String VISITS = "visits";
    private boolean anonymizeIp;
    private DataBaseHelper databaseHelper;
    private volatile int numStoredHits;
    private Random random;
    private int sampleRate;
    private boolean sessionStarted;
    private int storeId;
    private long timestampCurrent;
    private long timestampFirst;
    private long timestampPrevious;
    private boolean useStoredVisitorVars;
    /* access modifiers changed from: private */
    public CustomVariableBuffer visitorCVCache;
    private int visits;

    static class DataBaseHelper extends SQLiteOpenHelper {
        private final int databaseVersion;
        private final PersistentHitStore store;

        public DataBaseHelper(Context context, PersistentHitStore persistentHitStore) {
            this(context, PersistentHitStore.DATABASE_NAME, 5, persistentHitStore);
        }

        DataBaseHelper(Context context, String str, int i, PersistentHitStore persistentHitStore) {
            super(context, str, null, i);
            this.databaseVersion = i;
            this.store = persistentHitStore;
        }

        public DataBaseHelper(Context context, String str, PersistentHitStore persistentHitStore) {
            this(context, str, 5, persistentHitStore);
        }

        private void createECommerceTables(SQLiteDatabase sQLiteDatabase) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS transaction_events;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_TRANSACTION_EVENTS_TABLE);
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS item_events;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_ITEM_EVENTS_TABLE);
        }

        private void createHitTable(SQLiteDatabase sQLiteDatabase) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS hits;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_HITS_TABLE);
        }

        private void createReferrerTable(SQLiteDatabase sQLiteDatabase) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS referrer;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_REFERRER_TABLE);
        }

        /* JADX WARNING: Removed duplicated region for block: B:49:0x00e8  */
        /* JADX WARNING: Removed duplicated region for block: B:52:0x00f1  */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        private void fixReferrerTable(android.database.sqlite.SQLiteDatabase r13) {
            /*
                r12 = this;
                r11 = -1
                r10 = 0
                r8 = 1
                r9 = 0
                java.lang.String r1 = "referrer"
                r2 = 0
                r3 = 0
                r4 = 0
                r5 = 0
                r6 = 0
                r7 = 0
                r0 = r13
                android.database.Cursor r6 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x00fc, all -> 0x00fa }
                java.lang.String[] r3 = r6.getColumnNames()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r0 = r10
                r1 = r10
                r2 = r10
            L_0x0018:
                int r4 = r3.length     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r2 >= r4) goto L_0x0036
                r4 = r3[r2]     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r5 = "referrer_index"
                boolean r4 = r4.equals(r5)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r4 == 0) goto L_0x002a
                r1 = r8
            L_0x0026:
                int r10 = r2 + 1
                r2 = r10
                goto L_0x0018
            L_0x002a:
                r4 = r3[r2]     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r5 = "referrer_visit"
                boolean r4 = r4.equals(r5)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r4 == 0) goto L_0x0026
                r0 = r8
                goto L_0x0026
            L_0x0036:
                if (r1 == 0) goto L_0x003a
                if (r0 != 0) goto L_0x00b1
            L_0x003a:
                boolean r0 = r6.moveToFirst()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r0 == 0) goto L_0x00f5
                java.lang.String r0 = "referrer_visit"
                int r4 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r0 = "referrer_index"
                int r5 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                com.google.android.apps.analytics.Referrer r0 = new com.google.android.apps.analytics.Referrer     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r1 = "referrer"
                int r1 = r6.getColumnIndex(r1)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r1 = r6.getString(r1)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r2 = "timestamp_referrer"
                int r2 = r6.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                long r2 = r6.getLong(r2)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r4 != r11) goto L_0x00c0
                r4 = r8
            L_0x0065:
                if (r5 != r11) goto L_0x00c5
                r5 = r8
            L_0x0068:
                r0.<init>(r1, r2, r4, r5)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
            L_0x006b:
                r13.beginTransaction()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r12.createReferrerTable(r13)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                if (r0 == 0) goto L_0x00ae
                android.content.ContentValues r1 = new android.content.ContentValues     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r1.<init>()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r2 = "referrer"
                java.lang.String r3 = r0.getReferrerString()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r1.put(r2, r3)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r2 = "timestamp_referrer"
                long r4 = r0.getTimeStamp()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.Long r3 = java.lang.Long.valueOf(r4)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r1.put(r2, r3)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r2 = "referrer_visit"
                int r3 = r0.getVisit()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r1.put(r2, r3)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r2 = "referrer_index"
                int r0 = r0.getIndex()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.Integer r0 = java.lang.Integer.valueOf(r0)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                r1.put(r2, r0)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                java.lang.String r0 = "referrer"
                r2 = 0
                r13.insert(r0, r2, r1)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
            L_0x00ae:
                r13.setTransactionSuccessful()     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
            L_0x00b1:
                if (r6 == 0) goto L_0x00b6
                r6.close()
            L_0x00b6:
                boolean r0 = r13.inTransaction()
                if (r0 == 0) goto L_0x00bf
                com.google.android.apps.analytics.PersistentHitStore.endTransaction(r13)
            L_0x00bf:
                return
            L_0x00c0:
                int r4 = r6.getInt(r4)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                goto L_0x0065
            L_0x00c5:
                int r5 = r6.getInt(r5)     // Catch:{ SQLiteException -> 0x00ca, all -> 0x00e4 }
                goto L_0x0068
            L_0x00ca:
                r0 = move-exception
                r9 = r6
            L_0x00cc:
                java.lang.String r1 = "GoogleAnalyticsTracker"
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x00f8 }
                android.util.Log.e(r1, r0)     // Catch:{ all -> 0x00f8 }
                if (r9 == 0) goto L_0x00da
                r9.close()
            L_0x00da:
                boolean r0 = r13.inTransaction()
                if (r0 == 0) goto L_0x00bf
                com.google.android.apps.analytics.PersistentHitStore.endTransaction(r13)
                goto L_0x00bf
            L_0x00e4:
                r0 = move-exception
                r9 = r6
            L_0x00e6:
                if (r9 == 0) goto L_0x00eb
                r9.close()
            L_0x00eb:
                boolean r1 = r13.inTransaction()
                if (r1 == 0) goto L_0x00f4
                com.google.android.apps.analytics.PersistentHitStore.endTransaction(r13)
            L_0x00f4:
                throw r0
            L_0x00f5:
                r0 = r9
                goto L_0x006b
            L_0x00f8:
                r0 = move-exception
                goto L_0x00e6
            L_0x00fa:
                r0 = move-exception
                goto L_0x00e6
            L_0x00fc:
                r0 = move-exception
                goto L_0x00cc
            */
            throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.DataBaseHelper.fixReferrerTable(android.database.sqlite.SQLiteDatabase):void");
        }

        private void migrateEventsToHits(SQLiteDatabase sQLiteDatabase, int i) {
            this.store.loadExistingSession(sQLiteDatabase);
            this.store.visitorCVCache = this.store.getVisitorVarBuffer(sQLiteDatabase);
            Event[] peekEvents = this.store.peekEvents(1000, sQLiteDatabase, i);
            for (Event access$800 : peekEvents) {
                this.store.putEvent(access$800, sQLiteDatabase, false);
            }
            sQLiteDatabase.execSQL("DELETE from events;");
            sQLiteDatabase.execSQL("DELETE from item_events;");
            sQLiteDatabase.execSQL("DELETE from transaction_events;");
            sQLiteDatabase.execSQL("DELETE from custom_variables;");
        }

        /* JADX WARNING: Removed duplicated region for block: B:23:0x007f  */
        /* JADX WARNING: Removed duplicated region for block: B:25:0x0084  */
        /* JADX WARNING: Removed duplicated region for block: B:29:0x008d  */
        /* JADX WARNING: Removed duplicated region for block: B:31:0x0092  */
        /* JADX WARNING: Removed duplicated region for block: B:46:? A[RETURN, SYNTHETIC] */
        /* Code decompiled incorrectly, please refer to instructions dump. */
        private void migratePreV4Referrer(android.database.sqlite.SQLiteDatabase r12) {
            /*
                r11 = this;
                r8 = 0
                java.lang.String r1 = "install_referrer"
                r0 = 1
                java.lang.String[] r2 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x0071, all -> 0x0088 }
                r0 = 0
                java.lang.String r3 = "referrer"
                r2[r0] = r3     // Catch:{ SQLiteException -> 0x0071, all -> 0x0088 }
                r3 = 0
                r4 = 0
                r5 = 0
                r6 = 0
                r7 = 0
                r0 = r12
                android.database.Cursor r9 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0071, all -> 0x0088 }
                boolean r0 = r9.moveToFirst()     // Catch:{ SQLiteException -> 0x009c, all -> 0x0096 }
                if (r0 == 0) goto L_0x00a6
                r0 = 0
                java.lang.String r10 = r9.getString(r0)     // Catch:{ SQLiteException -> 0x009c, all -> 0x0096 }
                java.lang.String r1 = "session"
                r2 = 0
                r3 = 0
                r4 = 0
                r5 = 0
                r6 = 0
                r7 = 0
                r0 = r12
                android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x009c, all -> 0x0096 }
                boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                if (r0 == 0) goto L_0x00a3
                r0 = 0
                long r2 = r1.getLong(r0)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
            L_0x0038:
                android.content.ContentValues r0 = new android.content.ContentValues     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                r0.<init>()     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                java.lang.String r4 = "referrer"
                r0.put(r4, r10)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                java.lang.String r4 = "timestamp_referrer"
                java.lang.Long r2 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                r0.put(r4, r2)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                java.lang.String r2 = "referrer_visit"
                r3 = 1
                java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                r0.put(r2, r3)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                java.lang.String r2 = "referrer_index"
                r3 = 1
                java.lang.Integer r3 = java.lang.Integer.valueOf(r3)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                r0.put(r2, r3)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                java.lang.String r2 = "referrer"
                r3 = 0
                r12.insert(r2, r3, r0)     // Catch:{ SQLiteException -> 0x00a0, all -> 0x00a8 }
                r0 = r1
            L_0x0066:
                if (r9 == 0) goto L_0x006b
                r9.close()
            L_0x006b:
                if (r0 == 0) goto L_0x0070
                r0.close()
            L_0x0070:
                return
            L_0x0071:
                r0 = move-exception
                r1 = r8
                r2 = r8
            L_0x0074:
                java.lang.String r3 = "GoogleAnalyticsTracker"
                java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0099 }
                android.util.Log.e(r3, r0)     // Catch:{ all -> 0x0099 }
                if (r2 == 0) goto L_0x0082
                r2.close()
            L_0x0082:
                if (r1 == 0) goto L_0x0070
                r1.close()
                goto L_0x0070
            L_0x0088:
                r0 = move-exception
                r1 = r8
                r9 = r8
            L_0x008b:
                if (r9 == 0) goto L_0x0090
                r9.close()
            L_0x0090:
                if (r1 == 0) goto L_0x0095
                r1.close()
            L_0x0095:
                throw r0
            L_0x0096:
                r0 = move-exception
                r1 = r8
                goto L_0x008b
            L_0x0099:
                r0 = move-exception
                r9 = r2
                goto L_0x008b
            L_0x009c:
                r0 = move-exception
                r1 = r8
                r2 = r9
                goto L_0x0074
            L_0x00a0:
                r0 = move-exception
                r2 = r9
                goto L_0x0074
            L_0x00a3:
                r2 = 0
                goto L_0x0038
            L_0x00a6:
                r0 = r8
                goto L_0x0066
            L_0x00a8:
                r0 = move-exception
                goto L_0x008b
            */
            throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.DataBaseHelper.migratePreV4Referrer(android.database.sqlite.SQLiteDatabase):void");
        }

        /* access modifiers changed from: 0000 */
        public void createCustomVariableTables(SQLiteDatabase sQLiteDatabase) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS custom_variables;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_CUSTOM_VARIABLES_TABLE);
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS custom_var_cache;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_CUSTOM_VAR_CACHE_TABLE);
            for (int i = 1; i <= 5; i++) {
                ContentValues contentValues = new ContentValues();
                contentValues.put(PersistentHitStore.EVENT_ID, Integer.valueOf(0));
                contentValues.put(PersistentHitStore.CUSTOMVAR_INDEX, Integer.valueOf(i));
                contentValues.put(PersistentHitStore.CUSTOMVAR_NAME, "");
                contentValues.put(PersistentHitStore.CUSTOMVAR_SCOPE, Integer.valueOf(3));
                contentValues.put(PersistentHitStore.CUSTOMVAR_VALUE, "");
                sQLiteDatabase.insert("custom_var_cache", PersistentHitStore.EVENT_ID, contentValues);
            }
        }

        public void onCreate(SQLiteDatabase sQLiteDatabase) {
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS events;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_EVENTS_TABLE);
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS install_referrer;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_INSTALL_REFERRER_TABLE);
            sQLiteDatabase.execSQL("DROP TABLE IF EXISTS session;");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_SESSION_TABLE);
            if (this.databaseVersion > 1) {
                createCustomVariableTables(sQLiteDatabase);
            }
            if (this.databaseVersion > 2) {
                createECommerceTables(sQLiteDatabase);
            }
            if (this.databaseVersion > 3) {
                createHitTable(sQLiteDatabase);
                createReferrerTable(sQLiteDatabase);
            }
        }

        public void onDowngrade(SQLiteDatabase sQLiteDatabase, int i, int i2) {
            Log.w(GoogleAnalyticsTracker.LOG_TAG, "Downgrading database version from " + i + " to " + i2 + " not recommended.");
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_REFERRER_TABLE);
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_HITS_TABLE);
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_CUSTOM_VAR_CACHE_TABLE);
            sQLiteDatabase.execSQL(PersistentHitStore.CREATE_SESSION_TABLE);
            HashSet hashSet = new HashSet();
            Cursor query = sQLiteDatabase.query("custom_var_cache", null, null, null, null, null, null, null);
            while (query.moveToNext()) {
                try {
                    hashSet.add(Integer.valueOf(query.getInt(query.getColumnIndex(PersistentHitStore.CUSTOMVAR_INDEX))));
                } catch (SQLiteException e) {
                    Log.e(GoogleAnalyticsTracker.LOG_TAG, "Error on downgrade: " + e.toString());
                } finally {
                    query.close();
                }
            }
            int i3 = 1;
            while (true) {
                int i4 = i3;
                if (i4 <= 5) {
                    try {
                        if (!hashSet.contains(Integer.valueOf(i4))) {
                            ContentValues contentValues = new ContentValues();
                            contentValues.put(PersistentHitStore.EVENT_ID, Integer.valueOf(0));
                            contentValues.put(PersistentHitStore.CUSTOMVAR_INDEX, Integer.valueOf(i4));
                            contentValues.put(PersistentHitStore.CUSTOMVAR_NAME, "");
                            contentValues.put(PersistentHitStore.CUSTOMVAR_SCOPE, Integer.valueOf(3));
                            contentValues.put(PersistentHitStore.CUSTOMVAR_VALUE, "");
                            sQLiteDatabase.insert("custom_var_cache", PersistentHitStore.EVENT_ID, contentValues);
                        }
                    } catch (SQLiteException e2) {
                        Log.e(GoogleAnalyticsTracker.LOG_TAG, "Error inserting custom variable on downgrade: " + e2.toString());
                    }
                    i3 = i4 + 1;
                } else {
                    return;
                }
            }
        }

        public void onOpen(SQLiteDatabase sQLiteDatabase) {
            if (sQLiteDatabase.isReadOnly()) {
                Log.w(GoogleAnalyticsTracker.LOG_TAG, "Warning: Need to update database, but it's read only.");
            } else {
                fixReferrerTable(sQLiteDatabase);
            }
        }

        public void onUpgrade(SQLiteDatabase sQLiteDatabase, int i, int i2) {
            if (i > i2) {
                onDowngrade(sQLiteDatabase, i, i2);
                return;
            }
            if (i < 2 && i2 > 1) {
                createCustomVariableTables(sQLiteDatabase);
            }
            if (i < 3 && i2 > 2) {
                createECommerceTables(sQLiteDatabase);
            }
            if (i < 4 && i2 > 3) {
                createHitTable(sQLiteDatabase);
                createReferrerTable(sQLiteDatabase);
                migrateEventsToHits(sQLiteDatabase, i);
                migratePreV4Referrer(sQLiteDatabase);
            }
        }
    }

    PersistentHitStore(Context context) {
        this(context, DATABASE_NAME, 5);
    }

    PersistentHitStore(Context context, String str) {
        this(context, str, 5);
    }

    PersistentHitStore(Context context, String str, int i) {
        this.sampleRate = 100;
        this.random = new Random();
        this.databaseHelper = new DataBaseHelper(context, str, i, this);
        loadExistingSession();
        this.visitorCVCache = getVisitorVarBuffer();
    }

    PersistentHitStore(DataBaseHelper dataBaseHelper) {
        this.sampleRate = 100;
        this.random = new Random();
        this.databaseHelper = dataBaseHelper;
        loadExistingSession();
        this.visitorCVCache = getVisitorVarBuffer();
    }

    /* access modifiers changed from: private */
    public static boolean endTransaction(SQLiteDatabase sQLiteDatabase) {
        try {
            sQLiteDatabase.endTransaction();
            return true;
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, "exception ending transaction:" + e.toString());
            return false;
        }
    }

    static String formatReferrer(String str) {
        if (str == null) {
            return null;
        }
        if (!str.contains("=")) {
            if (!str.contains("%3D")) {
                return null;
            }
            try {
                str = URLDecoder.decode(str, "UTF-8");
            } catch (UnsupportedEncodingException e) {
                return null;
            }
        }
        Map parseURLParameters = Utils.parseURLParameters(str);
        boolean z = parseURLParameters.get("utm_campaign") != null;
        boolean z2 = parseURLParameters.get("utm_medium") != null;
        boolean z3 = parseURLParameters.get("utm_source") != null;
        if ((parseURLParameters.get("gclid") != null) || (z && z2 && z3)) {
            String[][] strArr = {new String[]{"utmcid", (String) parseURLParameters.get("utm_id")}, new String[]{"utmcsr", (String) parseURLParameters.get("utm_source")}, new String[]{"utmgclid", (String) parseURLParameters.get("gclid")}, new String[]{"utmccn", (String) parseURLParameters.get("utm_campaign")}, new String[]{"utmcmd", (String) parseURLParameters.get("utm_medium")}, new String[]{"utmctr", (String) parseURLParameters.get("utm_term")}, new String[]{"utmcct", (String) parseURLParameters.get("utm_content")}};
            StringBuilder sb = new StringBuilder();
            boolean z4 = true;
            for (int i = 0; i < strArr.length; i++) {
                if (strArr[i][1] != null) {
                    String replace = strArr[i][1].replace("+", "%20").replace(" ", "%20");
                    if (z4) {
                        z4 = false;
                    } else {
                        sb.append("|");
                    }
                    sb.append(strArr[i][0]).append("=").append(replace);
                }
            }
            return sb.toString();
        }
        Log.w(GoogleAnalyticsTracker.LOG_TAG, "Badly formatted referrer missing campaign, medium and source or click ID");
        return null;
    }

    private Referrer getAndUpdateReferrer(SQLiteDatabase sQLiteDatabase) {
        Referrer readCurrentReferrer = readCurrentReferrer(sQLiteDatabase);
        if (readCurrentReferrer != null) {
            if (readCurrentReferrer.getTimeStamp() != 0) {
                return readCurrentReferrer;
            }
            int index = readCurrentReferrer.getIndex();
            String referrerString = readCurrentReferrer.getReferrerString();
            ContentValues contentValues = new ContentValues();
            contentValues.put("referrer", referrerString);
            contentValues.put(TIMESTAMP_REFERRER, Long.valueOf(this.timestampCurrent));
            contentValues.put(REFERRER_VISIT, Integer.valueOf(this.visits));
            contentValues.put(REFERRER_INDEX, Integer.valueOf(index));
            if (setReferrerDatabase(sQLiteDatabase, contentValues)) {
                return new Referrer(referrerString, this.timestampCurrent, this.visits, index);
            }
        }
        return null;
    }

    /* access modifiers changed from: private */
    public void putEvent(Event event, SQLiteDatabase sQLiteDatabase, boolean z) {
        if (!event.isSessionInitialized()) {
            event.setRandomVal(this.random.nextInt(Integer.MAX_VALUE));
            event.setTimestampFirst((int) this.timestampFirst);
            event.setTimestampPrevious((int) this.timestampPrevious);
            event.setTimestampCurrent((int) this.timestampCurrent);
            event.setVisits(this.visits);
        }
        event.setAnonymizeIp(this.anonymizeIp);
        if (event.getUserId() == -1) {
            event.setUserId(this.storeId);
        }
        putCustomVariables(event, sQLiteDatabase);
        Referrer andUpdateReferrer = getAndUpdateReferrer(sQLiteDatabase);
        String[] split = event.accountId.split(",");
        if (split.length == 1) {
            writeEventToDatabase(event, andUpdateReferrer, sQLiteDatabase, z);
            return;
        }
        for (String event2 : split) {
            writeEventToDatabase(new Event(event, event2), andUpdateReferrer, sQLiteDatabase, z);
        }
    }

    private boolean setReferrerDatabase(SQLiteDatabase sQLiteDatabase, ContentValues contentValues) {
        try {
            sQLiteDatabase.beginTransaction();
            sQLiteDatabase.delete("referrer", null, null);
            sQLiteDatabase.insert("referrer", null, contentValues);
            sQLiteDatabase.setTransactionSuccessful();
            return !sQLiteDatabase.inTransaction() || endTransaction(sQLiteDatabase);
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            if (!sQLiteDatabase.inTransaction() || !endTransaction(sQLiteDatabase)) {
            }
            return false;
        } catch (Throwable th) {
            if (sQLiteDatabase.inTransaction() && !endTransaction(sQLiteDatabase)) {
                return false;
            }
            throw th;
        }
    }

    public void clearReferrer() {
        try {
            this.databaseHelper.getWritableDatabase().delete("referrer", null, null);
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
        }
    }

    public void deleteHit(long j) {
        synchronized (this) {
            try {
                this.numStoredHits -= this.databaseHelper.getWritableDatabase().delete("hits", "hit_id = ?", new String[]{Long.toString(j)});
            } catch (SQLiteException e) {
                Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            }
        }
        return;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x006e  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.CustomVariableBuffer getCustomVariables(long r12, android.database.sqlite.SQLiteDatabase r14) {
        /*
            r11 = this;
            r8 = 0
            com.google.android.apps.analytics.CustomVariableBuffer r9 = new com.google.android.apps.analytics.CustomVariableBuffer
            r9.<init>()
            java.lang.String r1 = "custom_variables"
            r2 = 0
            java.lang.String r3 = "event_id= ?"
            r0 = 1
            java.lang.String[] r4 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x0072, all -> 0x0075 }
            r0 = 0
            java.lang.String r5 = java.lang.Long.toString(r12)     // Catch:{ SQLiteException -> 0x0072, all -> 0x0075 }
            r4[r0] = r5     // Catch:{ SQLiteException -> 0x0072, all -> 0x0075 }
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r14
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0072, all -> 0x0075 }
        L_0x001d:
            boolean r0 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0054 }
            if (r0 == 0) goto L_0x0064
            com.google.android.apps.analytics.CustomVariable r0 = new com.google.android.apps.analytics.CustomVariable     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r2 = "cv_index"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x0054 }
            int r2 = r1.getInt(r2)     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r3 = "cv_name"
            int r3 = r1.getColumnIndex(r3)     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r3 = r1.getString(r3)     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r4 = "cv_value"
            int r4 = r1.getColumnIndex(r4)     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r4 = r1.getString(r4)     // Catch:{ SQLiteException -> 0x0054 }
            java.lang.String r5 = "cv_scope"
            int r5 = r1.getColumnIndex(r5)     // Catch:{ SQLiteException -> 0x0054 }
            int r5 = r1.getInt(r5)     // Catch:{ SQLiteException -> 0x0054 }
            r0.<init>(r2, r3, r4, r5)     // Catch:{ SQLiteException -> 0x0054 }
            r9.setCustomVariable(r0)     // Catch:{ SQLiteException -> 0x0054 }
            goto L_0x001d
        L_0x0054:
            r0 = move-exception
        L_0x0055:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x006a }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x006a }
            if (r1 == 0) goto L_0x0063
            r1.close()
        L_0x0063:
            return r9
        L_0x0064:
            if (r1 == 0) goto L_0x0063
            r1.close()
            goto L_0x0063
        L_0x006a:
            r0 = move-exception
            r8 = r1
        L_0x006c:
            if (r8 == 0) goto L_0x0071
            r8.close()
        L_0x0071:
            throw r0
        L_0x0072:
            r0 = move-exception
            r1 = r8
            goto L_0x0055
        L_0x0075:
            r0 = move-exception
            goto L_0x006c
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.getCustomVariables(long, android.database.sqlite.SQLiteDatabase):com.google.android.apps.analytics.CustomVariableBuffer");
    }

    /* access modifiers changed from: 0000 */
    public DataBaseHelper getDatabaseHelper() {
        return this.databaseHelper;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x008d  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.Item getItem(long r12, android.database.sqlite.SQLiteDatabase r14) {
        /*
            r11 = this;
            r8 = 0
            java.lang.String r1 = "item_events"
            r2 = 0
            java.lang.String r3 = "event_id= ?"
            r0 = 1
            java.lang.String[] r4 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x0078, all -> 0x0089 }
            r0 = 0
            java.lang.String r5 = java.lang.Long.toString(r12)     // Catch:{ SQLiteException -> 0x0078, all -> 0x0089 }
            r4[r0] = r5     // Catch:{ SQLiteException -> 0x0078, all -> 0x0089 }
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r14
            android.database.Cursor r9 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0078, all -> 0x0089 }
            boolean r0 = r9.moveToFirst()     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            if (r0 == 0) goto L_0x0071
            com.google.android.apps.analytics.Item$Builder r1 = new com.google.android.apps.analytics.Item$Builder     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = "order_id"
            int r0 = r9.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r2 = r9.getString(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = "item_sku"
            int r0 = r9.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r3 = r9.getString(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = "item_price"
            int r0 = r9.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            double r4 = r9.getDouble(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = "item_count"
            int r0 = r9.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            long r6 = r9.getLong(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            r1.<init>(r2, r3, r4, r6)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = "item_name"
            int r0 = r9.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r0 = r9.getString(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            com.google.android.apps.analytics.Item$Builder r0 = r1.setItemName(r0)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r1 = "item_category"
            int r1 = r9.getColumnIndex(r1)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            java.lang.String r1 = r9.getString(r1)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            com.google.android.apps.analytics.Item$Builder r0 = r0.setItemCategory(r1)     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            com.google.android.apps.analytics.Item r0 = r0.build()     // Catch:{ SQLiteException -> 0x0094, all -> 0x0097 }
            if (r9 == 0) goto L_0x0070
            r9.close()
        L_0x0070:
            return r0
        L_0x0071:
            if (r9 == 0) goto L_0x0076
            r9.close()
        L_0x0076:
            r0 = r8
            goto L_0x0070
        L_0x0078:
            r0 = move-exception
            r1 = r8
        L_0x007a:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0091 }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x0091 }
            if (r1 == 0) goto L_0x0076
            r1.close()
            goto L_0x0076
        L_0x0089:
            r0 = move-exception
            r9 = r8
        L_0x008b:
            if (r9 == 0) goto L_0x0090
            r9.close()
        L_0x0090:
            throw r0
        L_0x0091:
            r0 = move-exception
            r9 = r1
            goto L_0x008b
        L_0x0094:
            r0 = move-exception
            r1 = r9
            goto L_0x007a
        L_0x0097:
            r0 = move-exception
            goto L_0x008b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.getItem(long, android.database.sqlite.SQLiteDatabase):com.google.android.apps.analytics.Item");
    }

    public int getNumStoredHits() {
        return this.numStoredHits;
    }

    public int getNumStoredHitsFromDb() {
        Cursor cursor = null;
        int i = 0;
        try {
            Cursor rawQuery = this.databaseHelper.getReadableDatabase().rawQuery("SELECT COUNT(*) from hits", null);
            if (rawQuery.moveToFirst()) {
                i = (int) rawQuery.getLong(0);
            }
            if (rawQuery != null) {
                rawQuery.close();
            }
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            if (cursor != null) {
                cursor.close();
            }
        } catch (Throwable th) {
            if (cursor != null) {
                cursor.close();
            }
            throw th;
        }
        return i;
    }

    public Referrer getReferrer() {
        try {
            return readCurrentReferrer(this.databaseHelper.getReadableDatabase());
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            return null;
        }
    }

    public String getSessionId() {
        if (!this.sessionStarted) {
            return null;
        }
        return Integer.toString((int) this.timestampCurrent);
    }

    public int getStoreId() {
        return this.storeId;
    }

    /* access modifiers changed from: 0000 */
    public long getTimestampCurrent() {
        return this.timestampCurrent;
    }

    /* access modifiers changed from: 0000 */
    public long getTimestampFirst() {
        return this.timestampFirst;
    }

    /* access modifiers changed from: 0000 */
    public long getTimestampPrevious() {
        return this.timestampPrevious;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:22:0x0087  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.Transaction getTransaction(long r10, android.database.sqlite.SQLiteDatabase r12) {
        /*
            r9 = this;
            r8 = 0
            java.lang.String r1 = "transaction_events"
            r2 = 0
            java.lang.String r3 = "event_id= ?"
            r0 = 1
            java.lang.String[] r4 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x0072, all -> 0x008d }
            r0 = 0
            java.lang.String r5 = java.lang.Long.toString(r10)     // Catch:{ SQLiteException -> 0x0072, all -> 0x008d }
            r4[r0] = r5     // Catch:{ SQLiteException -> 0x0072, all -> 0x008d }
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r12
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0072, all -> 0x008d }
            boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x008b }
            if (r0 == 0) goto L_0x006b
            com.google.android.apps.analytics.Transaction$Builder r0 = new com.google.android.apps.analytics.Transaction$Builder     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = "order_id"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = r1.getString(r2)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r3 = "tran_totalcost"
            int r3 = r1.getColumnIndex(r3)     // Catch:{ SQLiteException -> 0x008b }
            double r4 = r1.getDouble(r3)     // Catch:{ SQLiteException -> 0x008b }
            r0.<init>(r2, r4)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = "tran_storename"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = r1.getString(r2)     // Catch:{ SQLiteException -> 0x008b }
            com.google.android.apps.analytics.Transaction$Builder r0 = r0.setStoreName(r2)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = "tran_totaltax"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x008b }
            double r2 = r1.getDouble(r2)     // Catch:{ SQLiteException -> 0x008b }
            com.google.android.apps.analytics.Transaction$Builder r0 = r0.setTotalTax(r2)     // Catch:{ SQLiteException -> 0x008b }
            java.lang.String r2 = "tran_shippingcost"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x008b }
            double r2 = r1.getDouble(r2)     // Catch:{ SQLiteException -> 0x008b }
            com.google.android.apps.analytics.Transaction$Builder r0 = r0.setShippingCost(r2)     // Catch:{ SQLiteException -> 0x008b }
            com.google.android.apps.analytics.Transaction r0 = r0.build()     // Catch:{ SQLiteException -> 0x008b }
            if (r1 == 0) goto L_0x006a
            r1.close()
        L_0x006a:
            return r0
        L_0x006b:
            if (r1 == 0) goto L_0x0070
            r1.close()
        L_0x0070:
            r0 = r8
            goto L_0x006a
        L_0x0072:
            r0 = move-exception
            r1 = r8
        L_0x0074:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0083 }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x0083 }
            if (r1 == 0) goto L_0x0070
            r1.close()
            goto L_0x0070
        L_0x0083:
            r0 = move-exception
            r8 = r1
        L_0x0085:
            if (r8 == 0) goto L_0x008a
            r8.close()
        L_0x008a:
            throw r0
        L_0x008b:
            r0 = move-exception
            goto L_0x0074
        L_0x008d:
            r0 = move-exception
            goto L_0x0085
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.getTransaction(long, android.database.sqlite.SQLiteDatabase):com.google.android.apps.analytics.Transaction");
    }

    public String getVisitorCustomVar(int i) {
        CustomVariable customVariableAt = this.visitorCVCache.getCustomVariableAt(i);
        if (customVariableAt == null || customVariableAt.getScope() != 1) {
            return null;
        }
        return customVariableAt.getValue();
    }

    public String getVisitorId() {
        if (!this.sessionStarted) {
            return null;
        }
        return String.format("%d.%d", new Object[]{Integer.valueOf(this.storeId), Long.valueOf(this.timestampFirst)});
    }

    /* access modifiers changed from: 0000 */
    public CustomVariableBuffer getVisitorVarBuffer() {
        try {
            return getVisitorVarBuffer(this.databaseHelper.getReadableDatabase());
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            return new CustomVariableBuffer();
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:19:0x006f  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.CustomVariableBuffer getVisitorVarBuffer(android.database.sqlite.SQLiteDatabase r11) {
        /*
            r10 = this;
            r8 = 0
            com.google.android.apps.analytics.CustomVariableBuffer r9 = new com.google.android.apps.analytics.CustomVariableBuffer
            r9.<init>()
            java.lang.String r1 = "custom_var_cache"
            r2 = 0
            java.lang.String r3 = "cv_scope= ?"
            r0 = 1
            java.lang.String[] r4 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x0073, all -> 0x0076 }
            r0 = 0
            r5 = 1
            java.lang.String r5 = java.lang.Integer.toString(r5)     // Catch:{ SQLiteException -> 0x0073, all -> 0x0076 }
            r4[r0] = r5     // Catch:{ SQLiteException -> 0x0073, all -> 0x0076 }
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r11
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x0073, all -> 0x0076 }
        L_0x001e:
            boolean r0 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0055 }
            if (r0 == 0) goto L_0x0065
            com.google.android.apps.analytics.CustomVariable r0 = new com.google.android.apps.analytics.CustomVariable     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r2 = "cv_index"
            int r2 = r1.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x0055 }
            int r2 = r1.getInt(r2)     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r3 = "cv_name"
            int r3 = r1.getColumnIndex(r3)     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r3 = r1.getString(r3)     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r4 = "cv_value"
            int r4 = r1.getColumnIndex(r4)     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r4 = r1.getString(r4)     // Catch:{ SQLiteException -> 0x0055 }
            java.lang.String r5 = "cv_scope"
            int r5 = r1.getColumnIndex(r5)     // Catch:{ SQLiteException -> 0x0055 }
            int r5 = r1.getInt(r5)     // Catch:{ SQLiteException -> 0x0055 }
            r0.<init>(r2, r3, r4, r5)     // Catch:{ SQLiteException -> 0x0055 }
            r9.setCustomVariable(r0)     // Catch:{ SQLiteException -> 0x0055 }
            goto L_0x001e
        L_0x0055:
            r0 = move-exception
        L_0x0056:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x006b }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x006b }
            if (r1 == 0) goto L_0x0064
            r1.close()
        L_0x0064:
            return r9
        L_0x0065:
            if (r1 == 0) goto L_0x0064
            r1.close()
            goto L_0x0064
        L_0x006b:
            r0 = move-exception
            r8 = r1
        L_0x006d:
            if (r8 == 0) goto L_0x0072
            r8.close()
        L_0x0072:
            throw r0
        L_0x0073:
            r0 = move-exception
            r1 = r8
            goto L_0x0056
        L_0x0076:
            r0 = move-exception
            goto L_0x006d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.getVisitorVarBuffer(android.database.sqlite.SQLiteDatabase):com.google.android.apps.analytics.CustomVariableBuffer");
    }

    /* access modifiers changed from: 0000 */
    public int getVisits() {
        return this.visits;
    }

    public void loadExistingSession() {
        try {
            loadExistingSession(this.databaseHelper.getWritableDatabase());
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:33:0x00ce  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void loadExistingSession(android.database.sqlite.SQLiteDatabase r15) {
        /*
            r14 = this;
            r8 = 1
            r9 = 0
            r12 = 0
            r10 = 0
            java.lang.String r1 = "session"
            r2 = 0
            r3 = 0
            r4 = 0
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r15
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            boolean r0 = r1.moveToFirst()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            if (r0 == 0) goto L_0x005a
            r0 = 0
            long r2 = r1.getLong(r0)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r14.timestampFirst = r2     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0 = 1
            long r2 = r1.getLong(r0)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r14.timestampPrevious = r2     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0 = 2
            long r2 = r1.getLong(r0)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r14.timestampCurrent = r2     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0 = 3
            int r0 = r1.getInt(r0)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r14.visits = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0 = 4
            int r0 = r1.getInt(r0)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r14.storeId = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            com.google.android.apps.analytics.Referrer r0 = r14.readCurrentReferrer(r15)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            long r2 = r14.timestampFirst     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            int r2 = (r2 > r12 ? 1 : (r2 == r12 ? 0 : -1))
            if (r2 == 0) goto L_0x0058
            if (r0 == 0) goto L_0x004f
            long r2 = r0.getTimeStamp()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            int r0 = (r2 > r12 ? 1 : (r2 == r12 ? 0 : -1))
            if (r0 == 0) goto L_0x0058
        L_0x004f:
            r0 = r8
        L_0x0050:
            r14.sessionStarted = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
        L_0x0052:
            if (r1 == 0) goto L_0x0057
            r1.close()
        L_0x0057:
            return
        L_0x0058:
            r0 = r9
            goto L_0x0050
        L_0x005a:
            r0 = 0
            r14.sessionStarted = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0 = 1
            r14.useStoredVisitorVars = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            java.security.SecureRandom r0 = new java.security.SecureRandom     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            int r0 = r0.nextInt()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r2 = 2147483647(0x7fffffff, float:NaN)
            r0 = r0 & r2
            r14.storeId = r0     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            r1.close()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x00c7 }
            android.content.ContentValues r0 = new android.content.ContentValues     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.<init>()     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "timestamp_first"
            r2 = 0
            java.lang.Long r2 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.put(r1, r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "timestamp_previous"
            r2 = 0
            java.lang.Long r2 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.put(r1, r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "timestamp_current"
            r2 = 0
            java.lang.Long r2 = java.lang.Long.valueOf(r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.put(r1, r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "visits"
            r2 = 0
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.put(r1, r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "store_id"
            int r2 = r14.storeId     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.Integer r2 = java.lang.Integer.valueOf(r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r0.put(r1, r2)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            java.lang.String r1 = "session"
            r2 = 0
            r15.insert(r1, r2, r0)     // Catch:{ SQLiteException -> 0x00b5, all -> 0x00d9 }
            r1 = r10
            goto L_0x0052
        L_0x00b5:
            r1 = move-exception
            r0 = r10
            r2 = r1
        L_0x00b8:
            java.lang.String r1 = "GoogleAnalyticsTracker"
            java.lang.String r2 = r2.toString()     // Catch:{ all -> 0x00d5 }
            android.util.Log.e(r1, r2)     // Catch:{ all -> 0x00d5 }
            if (r0 == 0) goto L_0x0057
            r0.close()
            goto L_0x0057
        L_0x00c7:
            r0 = move-exception
            r2 = r0
            r3 = r1
        L_0x00ca:
            r10 = r3
            r0 = r2
        L_0x00cc:
            if (r10 == 0) goto L_0x00d1
            r10.close()
        L_0x00d1:
            throw r0
        L_0x00d2:
            r2 = move-exception
            r0 = r1
            goto L_0x00b8
        L_0x00d5:
            r1 = move-exception
            r2 = r1
            r3 = r0
            goto L_0x00ca
        L_0x00d9:
            r0 = move-exception
            goto L_0x00cc
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.loadExistingSession(android.database.sqlite.SQLiteDatabase):void");
    }

    /* JADX WARNING: Removed duplicated region for block: B:30:0x011a  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.Event[] peekEvents(int r21, android.database.sqlite.SQLiteDatabase r22, int r23) {
        /*
            r20 = this;
            java.util.ArrayList r19 = new java.util.ArrayList
            r19.<init>()
            java.lang.String r3 = "events"
            r4 = 0
            r5 = 0
            r6 = 0
            r7 = 0
            r8 = 0
            java.lang.String r9 = "event_id"
            java.lang.String r10 = java.lang.Integer.toString(r21)     // Catch:{ SQLiteException -> 0x0151, all -> 0x0149 }
            r2 = r22
            android.database.Cursor r18 = r2.query(r3, r4, r5, r6, r7, r8, r9, r10)     // Catch:{ SQLiteException -> 0x0151, all -> 0x0149 }
        L_0x0018:
            boolean r2 = r18.moveToNext()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            if (r2 == 0) goto L_0x0135
            com.google.android.apps.analytics.Event r3 = new com.google.android.apps.analytics.Event     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 0
            r0 = r18
            long r4 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 2
            r0 = r18
            java.lang.String r6 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 3
            r0 = r18
            int r7 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 4
            r0 = r18
            int r8 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 5
            r0 = r18
            int r9 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 6
            r0 = r18
            int r10 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 7
            r0 = r18
            int r11 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 8
            r0 = r18
            java.lang.String r12 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 9
            r0 = r18
            java.lang.String r13 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 10
            r0 = r18
            java.lang.String r14 = r0.getString(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 11
            r0 = r18
            int r15 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 12
            r0 = r18
            int r16 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 13
            r0 = r18
            int r17 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r3.<init>(r4, r6, r7, r8, r9, r10, r11, r12, r13, r14, r15, r16, r17)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2 = 1
            r0 = r18
            int r2 = r0.getInt(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r3.setUserId(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r2 = "event_id"
            r0 = r18
            int r2 = r0.getColumnIndex(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r0 = r18
            long r4 = r0.getLong(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r2 = "__##GOOGLETRANSACTION##__"
            java.lang.String r6 = r3.category     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            boolean r2 = r2.equals(r6)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            if (r2 == 0) goto L_0x00e7
            r0 = r20
            r1 = r22
            com.google.android.apps.analytics.Transaction r2 = r0.getTransaction(r4, r1)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            if (r2 != 0) goto L_0x00c8
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r6.<init>()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r7 = "GoogleAnalyticsTracker"
            java.lang.String r8 = "missing expected transaction for event "
            java.lang.StringBuilder r6 = r6.append(r8)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.StringBuilder r4 = r6.append(r4)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r4 = r4.toString()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            android.util.Log.w(r7, r4)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
        L_0x00c8:
            r3.setTransaction(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
        L_0x00cb:
            r0 = r19
            r0.add(r3)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            goto L_0x0018
        L_0x00d2:
            r2 = move-exception
            r3 = r18
        L_0x00d5:
            java.lang.String r4 = "GoogleAnalyticsTracker"
            java.lang.String r2 = r2.toString()     // Catch:{ all -> 0x014d }
            android.util.Log.e(r4, r2)     // Catch:{ all -> 0x014d }
            r2 = 0
            com.google.android.apps.analytics.Event[] r2 = new com.google.android.apps.analytics.Event[r2]     // Catch:{ all -> 0x014d }
            if (r3 == 0) goto L_0x00e6
            r3.close()
        L_0x00e6:
            return r2
        L_0x00e7:
            java.lang.String r2 = "__##GOOGLEITEM##__"
            java.lang.String r6 = r3.category     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            boolean r2 = r2.equals(r6)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            if (r2 == 0) goto L_0x011e
            r0 = r20
            r1 = r22
            com.google.android.apps.analytics.Item r2 = r0.getItem(r4, r1)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            if (r2 != 0) goto L_0x0113
            java.lang.StringBuilder r6 = new java.lang.StringBuilder     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r6.<init>()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r7 = "GoogleAnalyticsTracker"
            java.lang.String r8 = "missing expected item for event "
            java.lang.StringBuilder r6 = r6.append(r8)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.StringBuilder r4 = r6.append(r4)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            java.lang.String r4 = r4.toString()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            android.util.Log.w(r7, r4)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
        L_0x0113:
            r3.setItem(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            goto L_0x00cb
        L_0x0117:
            r2 = move-exception
        L_0x0118:
            if (r18 == 0) goto L_0x011d
            r18.close()
        L_0x011d:
            throw r2
        L_0x011e:
            r2 = 1
            r0 = r23
            if (r0 <= r2) goto L_0x012f
            r0 = r20
            r1 = r22
            com.google.android.apps.analytics.CustomVariableBuffer r2 = r0.getCustomVariables(r4, r1)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
        L_0x012b:
            r3.setCustomVariableBuffer(r2)     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            goto L_0x00cb
        L_0x012f:
            com.google.android.apps.analytics.CustomVariableBuffer r2 = new com.google.android.apps.analytics.CustomVariableBuffer     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            r2.<init>()     // Catch:{ SQLiteException -> 0x00d2, all -> 0x0117 }
            goto L_0x012b
        L_0x0135:
            if (r18 == 0) goto L_0x013a
            r18.close()
        L_0x013a:
            int r2 = r19.size()
            com.google.android.apps.analytics.Event[] r2 = new com.google.android.apps.analytics.Event[r2]
            r0 = r19
            java.lang.Object[] r2 = r0.toArray(r2)
            com.google.android.apps.analytics.Event[] r2 = (com.google.android.apps.analytics.Event[]) r2
            goto L_0x00e6
        L_0x0149:
            r2 = move-exception
            r18 = 0
            goto L_0x0118
        L_0x014d:
            r2 = move-exception
            r18 = r3
            goto L_0x0118
        L_0x0151:
            r2 = move-exception
            r3 = 0
            goto L_0x00d5
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.peekEvents(int, android.database.sqlite.SQLiteDatabase, int):com.google.android.apps.analytics.Event[]");
    }

    public Hit[] peekHits() {
        return peekHits(1000);
    }

    /* JADX WARNING: Removed duplicated region for block: B:19:0x005f  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.Hit[] peekHits(int r12) {
        /*
            r11 = this;
            r9 = 0
            java.util.ArrayList r10 = new java.util.ArrayList
            r10.<init>()
            com.google.android.apps.analytics.PersistentHitStore$DataBaseHelper r0 = r11.databaseHelper     // Catch:{ SQLiteException -> 0x0063, all -> 0x0066 }
            android.database.sqlite.SQLiteDatabase r0 = r0.getReadableDatabase()     // Catch:{ SQLiteException -> 0x0063, all -> 0x0066 }
            java.lang.String r1 = "hits"
            r2 = 0
            r3 = 0
            r4 = 0
            r5 = 0
            r6 = 0
            java.lang.String r7 = "hit_id"
            java.lang.String r8 = java.lang.Integer.toString(r12)     // Catch:{ SQLiteException -> 0x0063, all -> 0x0066 }
            android.database.Cursor r1 = r0.query(r1, r2, r3, r4, r5, r6, r7, r8)     // Catch:{ SQLiteException -> 0x0063, all -> 0x0066 }
        L_0x001d:
            boolean r0 = r1.moveToNext()     // Catch:{ SQLiteException -> 0x0036 }
            if (r0 == 0) goto L_0x0049
            com.google.android.apps.analytics.Hit r0 = new com.google.android.apps.analytics.Hit     // Catch:{ SQLiteException -> 0x0036 }
            r2 = 1
            java.lang.String r2 = r1.getString(r2)     // Catch:{ SQLiteException -> 0x0036 }
            r3 = 0
            long r4 = r1.getLong(r3)     // Catch:{ SQLiteException -> 0x0036 }
            r0.<init>(r2, r4)     // Catch:{ SQLiteException -> 0x0036 }
            r10.add(r0)     // Catch:{ SQLiteException -> 0x0036 }
            goto L_0x001d
        L_0x0036:
            r0 = move-exception
        L_0x0037:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x005b }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x005b }
            r0 = 0
            com.google.android.apps.analytics.Hit[] r0 = new com.google.android.apps.analytics.Hit[r0]     // Catch:{ all -> 0x005b }
            if (r1 == 0) goto L_0x0048
            r1.close()
        L_0x0048:
            return r0
        L_0x0049:
            if (r1 == 0) goto L_0x004e
            r1.close()
        L_0x004e:
            int r0 = r10.size()
            com.google.android.apps.analytics.Hit[] r0 = new com.google.android.apps.analytics.Hit[r0]
            java.lang.Object[] r0 = r10.toArray(r0)
            com.google.android.apps.analytics.Hit[] r0 = (com.google.android.apps.analytics.Hit[]) r0
            goto L_0x0048
        L_0x005b:
            r0 = move-exception
            r9 = r1
        L_0x005d:
            if (r9 == 0) goto L_0x0062
            r9.close()
        L_0x0062:
            throw r0
        L_0x0063:
            r0 = move-exception
            r1 = r9
            goto L_0x0037
        L_0x0066:
            r0 = move-exception
            goto L_0x005d
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.peekHits(int):com.google.android.apps.analytics.Hit[]");
    }

    /* access modifiers changed from: 0000 */
    public void putCustomVariables(Event event, SQLiteDatabase sQLiteDatabase) {
        CustomVariableBuffer customVariableBuffer;
        if (!"__##GOOGLEITEM##__".equals(event.category) && !"__##GOOGLETRANSACTION##__".equals(event.category)) {
            try {
                CustomVariableBuffer customVariableBuffer2 = event.getCustomVariableBuffer();
                if (this.useStoredVisitorVars) {
                    if (customVariableBuffer2 == null) {
                        customVariableBuffer2 = new CustomVariableBuffer();
                        event.setCustomVariableBuffer(customVariableBuffer2);
                    }
                    for (int i = 1; i <= 5; i++) {
                        CustomVariable customVariableAt = this.visitorCVCache.getCustomVariableAt(i);
                        CustomVariable customVariableAt2 = customVariableBuffer2.getCustomVariableAt(i);
                        if (customVariableAt != null && customVariableAt2 == null) {
                            customVariableBuffer2.setCustomVariable(customVariableAt);
                        }
                    }
                    this.useStoredVisitorVars = false;
                    customVariableBuffer = customVariableBuffer2;
                } else {
                    customVariableBuffer = customVariableBuffer2;
                }
                if (customVariableBuffer != null) {
                    for (int i2 = 1; i2 <= 5; i2++) {
                        if (!customVariableBuffer.isIndexAvailable(i2)) {
                            CustomVariable customVariableAt3 = customVariableBuffer.getCustomVariableAt(i2);
                            ContentValues contentValues = new ContentValues();
                            contentValues.put(EVENT_ID, Integer.valueOf(0));
                            contentValues.put(CUSTOMVAR_INDEX, Integer.valueOf(customVariableAt3.getIndex()));
                            contentValues.put(CUSTOMVAR_NAME, customVariableAt3.getName());
                            contentValues.put(CUSTOMVAR_SCOPE, Integer.valueOf(customVariableAt3.getScope()));
                            contentValues.put(CUSTOMVAR_VALUE, customVariableAt3.getValue());
                            sQLiteDatabase.update("custom_var_cache", contentValues, "cv_index = ?", new String[]{Integer.toString(customVariableAt3.getIndex())});
                            if (customVariableAt3.getScope() == 1) {
                                this.visitorCVCache.setCustomVariable(customVariableAt3);
                            } else {
                                this.visitorCVCache.clearCustomVariableAt(customVariableAt3.getIndex());
                            }
                        }
                    }
                }
            } catch (SQLiteException e) {
                Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            }
        }
    }

    public void putEvent(Event event) {
        if (this.numStoredHits >= 1000) {
            Log.w(GoogleAnalyticsTracker.LOG_TAG, "Store full. Not storing last event.");
            return;
        }
        if (this.sampleRate != 100) {
            if ((event.getUserId() == -1 ? this.storeId : event.getUserId()) % 10000 >= this.sampleRate * 100) {
                if (GoogleAnalyticsTracker.getInstance().getDebug()) {
                    Log.v(GoogleAnalyticsTracker.LOG_TAG, "User has been sampled out. Aborting hit.");
                    return;
                }
                return;
            }
        }
        synchronized (this) {
            try {
                SQLiteDatabase writableDatabase = this.databaseHelper.getWritableDatabase();
                try {
                    writableDatabase.beginTransaction();
                    if (!this.sessionStarted) {
                        storeUpdatedSession(writableDatabase);
                    }
                    putEvent(event, writableDatabase, true);
                    writableDatabase.setTransactionSuccessful();
                    if (writableDatabase.inTransaction()) {
                        endTransaction(writableDatabase);
                    }
                } catch (SQLiteException e) {
                    Log.e(GoogleAnalyticsTracker.LOG_TAG, "putEventOuter:" + e.toString());
                    if (writableDatabase.inTransaction()) {
                        endTransaction(writableDatabase);
                    }
                } catch (Throwable th) {
                    if (writableDatabase.inTransaction()) {
                        endTransaction(writableDatabase);
                    }
                    throw th;
                }
            } catch (SQLiteException e2) {
                Log.e(GoogleAnalyticsTracker.LOG_TAG, "Can't get db: " + e2.toString());
            }
        }
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:20:0x0073  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public com.google.android.apps.analytics.Referrer readCurrentReferrer(android.database.sqlite.SQLiteDatabase r10) {
        /*
            r9 = this;
            r8 = 0
            java.lang.String r1 = "referrer"
            r0 = 4
            java.lang.String[] r2 = new java.lang.String[r0]     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            r0 = 0
            java.lang.String r3 = "referrer"
            r2[r0] = r3     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            r0 = 1
            java.lang.String r3 = "timestamp_referrer"
            r2[r0] = r3     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            r0 = 2
            java.lang.String r3 = "referrer_visit"
            r2[r0] = r3     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            r0 = 3
            java.lang.String r3 = "referrer_index"
            r2[r0] = r3     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            r3 = 0
            r4 = 0
            r5 = 0
            r6 = 0
            r7 = 0
            r0 = r10
            android.database.Cursor r6 = r0.query(r1, r2, r3, r4, r5, r6, r7)     // Catch:{ SQLiteException -> 0x005d, all -> 0x006f }
            boolean r0 = r6.moveToFirst()     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            if (r0 == 0) goto L_0x007d
            java.lang.String r0 = "timestamp_referrer"
            int r0 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            long r2 = r6.getLong(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            java.lang.String r0 = "referrer_visit"
            int r0 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            int r4 = r6.getInt(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            java.lang.String r0 = "referrer_index"
            int r0 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            int r5 = r6.getInt(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            java.lang.String r0 = "referrer"
            int r0 = r6.getColumnIndex(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            java.lang.String r1 = r6.getString(r0)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            com.google.android.apps.analytics.Referrer r0 = new com.google.android.apps.analytics.Referrer     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
            r0.<init>(r1, r2, r4, r5)     // Catch:{ SQLiteException -> 0x007a, all -> 0x007f }
        L_0x0057:
            if (r6 == 0) goto L_0x005c
            r6.close()
        L_0x005c:
            return r0
        L_0x005d:
            r0 = move-exception
            r1 = r8
        L_0x005f:
            java.lang.String r2 = "GoogleAnalyticsTracker"
            java.lang.String r0 = r0.toString()     // Catch:{ all -> 0x0077 }
            android.util.Log.e(r2, r0)     // Catch:{ all -> 0x0077 }
            if (r1 == 0) goto L_0x006d
            r1.close()
        L_0x006d:
            r0 = r8
            goto L_0x005c
        L_0x006f:
            r0 = move-exception
            r6 = r8
        L_0x0071:
            if (r6 == 0) goto L_0x0076
            r6.close()
        L_0x0076:
            throw r0
        L_0x0077:
            r0 = move-exception
            r6 = r1
            goto L_0x0071
        L_0x007a:
            r0 = move-exception
            r1 = r6
            goto L_0x005f
        L_0x007d:
            r0 = r8
            goto L_0x0057
        L_0x007f:
            r0 = move-exception
            goto L_0x0071
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.apps.analytics.PersistentHitStore.readCurrentReferrer(android.database.sqlite.SQLiteDatabase):com.google.android.apps.analytics.Referrer");
    }

    public void setAnonymizeIp(boolean z) {
        this.anonymizeIp = z;
    }

    public boolean setReferrer(String str) {
        long j;
        String formatReferrer = formatReferrer(str);
        if (formatReferrer == null) {
            return false;
        }
        try {
            SQLiteDatabase writableDatabase = this.databaseHelper.getWritableDatabase();
            Referrer readCurrentReferrer = readCurrentReferrer(writableDatabase);
            ContentValues contentValues = new ContentValues();
            contentValues.put("referrer", formatReferrer);
            contentValues.put(TIMESTAMP_REFERRER, Long.valueOf(0));
            contentValues.put(REFERRER_VISIT, Integer.valueOf(0));
            if (readCurrentReferrer != null) {
                j = (long) readCurrentReferrer.getIndex();
                if (readCurrentReferrer.getTimeStamp() > 0) {
                    j++;
                }
            } else {
                j = 1;
            }
            contentValues.put(REFERRER_INDEX, Long.valueOf(j));
            if (!setReferrerDatabase(writableDatabase, contentValues)) {
                return false;
            }
            startNewVisit();
            return true;
        } catch (SQLiteException e) {
            Log.e(GoogleAnalyticsTracker.LOG_TAG, e.toString());
            return false;
        }
    }

    public void setSampleRate(int i) {
        this.sampleRate = i;
    }

    public void startNewVisit() {
        synchronized (this) {
            this.sessionStarted = false;
            this.useStoredVisitorVars = true;
            this.numStoredHits = getNumStoredHitsFromDb();
        }
    }

    /* access modifiers changed from: 0000 */
    public void storeUpdatedSession(SQLiteDatabase sQLiteDatabase) {
        SQLiteDatabase writableDatabase = this.databaseHelper.getWritableDatabase();
        writableDatabase.delete(SettingsJsonConstants.SESSION_KEY, null, null);
        if (this.timestampFirst == 0) {
            long currentTimeMillis = System.currentTimeMillis() / 1000;
            this.timestampFirst = currentTimeMillis;
            this.timestampPrevious = currentTimeMillis;
            this.timestampCurrent = currentTimeMillis;
            this.visits = 1;
        } else {
            this.timestampPrevious = this.timestampCurrent;
            this.timestampCurrent = System.currentTimeMillis() / 1000;
            if (this.timestampCurrent == this.timestampPrevious) {
                this.timestampCurrent++;
            }
            this.visits++;
        }
        ContentValues contentValues = new ContentValues();
        contentValues.put(TIMESTAMP_FIRST, Long.valueOf(this.timestampFirst));
        contentValues.put(TIMESTAMP_PREVIOUS, Long.valueOf(this.timestampPrevious));
        contentValues.put(TIMESTAMP_CURRENT, Long.valueOf(this.timestampCurrent));
        contentValues.put(VISITS, Integer.valueOf(this.visits));
        contentValues.put(STORE_ID, Integer.valueOf(this.storeId));
        writableDatabase.insert(SettingsJsonConstants.SESSION_KEY, null, contentValues);
        this.sessionStarted = true;
    }

    /* access modifiers changed from: 0000 */
    public void writeEventToDatabase(Event event, Referrer referrer, SQLiteDatabase sQLiteDatabase, boolean z) throws SQLiteException {
        ContentValues contentValues = new ContentValues();
        contentValues.put(HIT_STRING, HitBuilder.constructHitRequestPath(event, referrer));
        contentValues.put(HIT_TIMESTAMP, Long.valueOf(z ? System.currentTimeMillis() : 0));
        sQLiteDatabase.insert("hits", null, contentValues);
        this.numStoredHits++;
    }
}
