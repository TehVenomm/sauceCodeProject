.class Lcom/google/android/apps/analytics/PersistentHitStore;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/google/android/apps/analytics/HitStore;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;
    }
.end annotation


# static fields
.field private static final ACCOUNT_ID:Ljava/lang/String; = "account_id"

.field private static final ACTION:Ljava/lang/String; = "action"

.field private static final CATEGORY:Ljava/lang/String; = "category"

.field private static final CREATE_CUSTOM_VARIABLES_TABLE:Ljava/lang/String;

.field private static final CREATE_CUSTOM_VAR_CACHE_TABLE:Ljava/lang/String;

.field private static final CREATE_EVENTS_TABLE:Ljava/lang/String;

.field private static final CREATE_HITS_TABLE:Ljava/lang/String;

.field private static final CREATE_INSTALL_REFERRER_TABLE:Ljava/lang/String; = "CREATE TABLE install_referrer (referrer TEXT PRIMARY KEY NOT NULL);"

.field private static final CREATE_ITEM_EVENTS_TABLE:Ljava/lang/String;

.field private static final CREATE_REFERRER_TABLE:Ljava/lang/String; = "CREATE TABLE IF NOT EXISTS referrer (referrer TEXT PRIMARY KEY NOT NULL,timestamp_referrer INTEGER NOT NULL,referrer_visit INTEGER NOT NULL DEFAULT 1,referrer_index INTEGER NOT NULL DEFAULT 1);"

.field private static final CREATE_SESSION_TABLE:Ljava/lang/String;

.field private static final CREATE_TRANSACTION_EVENTS_TABLE:Ljava/lang/String;

.field private static final CUSTOMVAR_ID:Ljava/lang/String; = "cv_id"

.field private static final CUSTOMVAR_INDEX:Ljava/lang/String; = "cv_index"

.field private static final CUSTOMVAR_NAME:Ljava/lang/String; = "cv_name"

.field private static final CUSTOMVAR_SCOPE:Ljava/lang/String; = "cv_scope"

.field private static final CUSTOMVAR_VALUE:Ljava/lang/String; = "cv_value"

.field private static final CUSTOM_VARIABLE_COLUMN_TYPE:Ljava/lang/String; = "CHAR(64) NOT NULL"

.field private static final DATABASE_NAME:Ljava/lang/String; = "google_analytics.db"

.field private static final DATABASE_VERSION:I = 0x5

.field private static final EVENT_ID:Ljava/lang/String; = "event_id"

.field private static final HIT_ID:Ljava/lang/String; = "hit_id"

.field private static final HIT_STRING:Ljava/lang/String; = "hit_string"

.field private static final HIT_TIMESTAMP:Ljava/lang/String; = "hit_time"

.field private static final ITEM_CATEGORY:Ljava/lang/String; = "item_category"

.field private static final ITEM_COUNT:Ljava/lang/String; = "item_count"

.field private static final ITEM_ID:Ljava/lang/String; = "item_id"

.field private static final ITEM_NAME:Ljava/lang/String; = "item_name"

.field private static final ITEM_PRICE:Ljava/lang/String; = "item_price"

.field private static final ITEM_SKU:Ljava/lang/String; = "item_sku"

.field private static final LABEL:Ljava/lang/String; = "label"

.field private static final MAX_HITS:I = 0x3e8

.field private static final ORDER_ID:Ljava/lang/String; = "order_id"

.field private static final RANDOM_VAL:Ljava/lang/String; = "random_val"

.field static final REFERRER:Ljava/lang/String; = "referrer"

.field static final REFERRER_COLUMN:Ljava/lang/String; = "referrer"

.field static final REFERRER_INDEX:Ljava/lang/String; = "referrer_index"

.field static final REFERRER_VISIT:Ljava/lang/String; = "referrer_visit"

.field private static final SCREEN_HEIGHT:Ljava/lang/String; = "screen_height"

.field private static final SCREEN_WIDTH:Ljava/lang/String; = "screen_width"

.field private static final SHIPPING_COST:Ljava/lang/String; = "tran_shippingcost"

.field private static final STORE_ID:Ljava/lang/String; = "store_id"

.field private static final STORE_NAME:Ljava/lang/String; = "tran_storename"

.field private static final TIMESTAMP_CURRENT:Ljava/lang/String; = "timestamp_current"

.field private static final TIMESTAMP_FIRST:Ljava/lang/String; = "timestamp_first"

.field private static final TIMESTAMP_PREVIOUS:Ljava/lang/String; = "timestamp_previous"

.field static final TIMESTAMP_REFERRER:Ljava/lang/String; = "timestamp_referrer"

.field private static final TOTAL_COST:Ljava/lang/String; = "tran_totalcost"

.field private static final TOTAL_TAX:Ljava/lang/String; = "tran_totaltax"

.field private static final TRANSACTION_ID:Ljava/lang/String; = "tran_id"

.field private static final USER_ID:Ljava/lang/String; = "user_id"

.field private static final VALUE:Ljava/lang/String; = "value"

.field private static final VISITS:Ljava/lang/String; = "visits"


# instance fields
.field private anonymizeIp:Z

.field private databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

.field private volatile numStoredHits:I

.field private random:Ljava/util/Random;

.field private sampleRate:I

.field private sessionStarted:Z

.field private storeId:I

.field private timestampCurrent:J

.field private timestampFirst:J

.field private timestampPrevious:J

.field private useStoredVisitorVars:Z

.field private visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

.field private visits:I


# direct methods
.method static constructor <clinit>()V
    .locals 6

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE events ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    const/4 v2, 0x1

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "event_id"

    const/4 v5, 0x0

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "user_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(256) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "account_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "random_val"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_first"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_previous"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_current"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "visits"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(256) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "category"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(256) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "action"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(256), "

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "label"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "value"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "screen_width"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "screen_height"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_EVENTS_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE IF NOT EXISTS session ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_first"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_previous"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "timestamp_current"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "visits"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "store_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_SESSION_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE custom_variables ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "event_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_index"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(64) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_name"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(64) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_value"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_scope"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_CUSTOM_VARIABLES_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE IF NOT EXISTS custom_var_cache ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "event_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_index"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(64) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_name"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' CHAR(64) NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_value"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "cv_scope"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_CUSTOM_VAR_CACHE_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE transaction_events ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "tran_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "event_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "order_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "tran_storename"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "tran_totalcost"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "tran_totaltax"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "tran_shippingcost"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_TRANSACTION_EVENTS_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE item_events ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "event_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "order_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_sku"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_name"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_category"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_price"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL);"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "item_count"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_ITEM_EVENTS_TABLE:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "CREATE TABLE IF NOT EXISTS hits ("

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "hit_id"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' TEXT NOT NULL,"

    new-array v3, v2, [Ljava/lang/Object;

    const-string v4, "hit_string"

    aput-object v4, v3, v5

    invoke-static {v1, v3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " \'%s\' INTEGER NOT NULL);"

    new-array v2, v2, [Ljava/lang/Object;

    const-string v3, "hit_time"

    aput-object v3, v2, v5

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_HITS_TABLE:Ljava/lang/String;

    return-void
.end method

.method constructor <init>(Landroid/content/Context;)V
    .locals 2

    const-string v0, "google_analytics.db"

    const/4 v1, 0x5

    invoke-direct {p0, p1, v0, v1}, Lcom/google/android/apps/analytics/PersistentHitStore;-><init>(Landroid/content/Context;Ljava/lang/String;I)V

    return-void
.end method

.method constructor <init>(Landroid/content/Context;Ljava/lang/String;)V
    .locals 1

    const/4 v0, 0x5

    invoke-direct {p0, p1, p2, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;-><init>(Landroid/content/Context;Ljava/lang/String;I)V

    return-void
.end method

.method constructor <init>(Landroid/content/Context;Ljava/lang/String;I)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/16 v0, 0x64

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sampleRate:I

    new-instance v0, Ljava/util/Random;

    invoke-direct {v0}, Ljava/util/Random;-><init>()V

    iput-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->random:Ljava/util/Random;

    new-instance v0, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-direct {v0, p1, p2, p3, p0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;-><init>(Landroid/content/Context;Ljava/lang/String;ILcom/google/android/apps/analytics/PersistentHitStore;)V

    iput-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->loadExistingSession()V

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getVisitorVarBuffer()Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    return-void
.end method

.method constructor <init>(Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/16 v0, 0x64

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sampleRate:I

    new-instance v0, Ljava/util/Random;

    invoke-direct {v0}, Ljava/util/Random;-><init>()V

    iput-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->random:Ljava/util/Random;

    iput-object p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->loadExistingSession()V

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getVisitorVarBuffer()Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object p1

    iput-object p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    return-void
.end method

.method static synthetic access$000()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_EVENTS_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$100()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_SESSION_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$200()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_CUSTOM_VARIABLES_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$300()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_CUSTOM_VAR_CACHE_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$400()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_TRANSACTION_EVENTS_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$500()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_ITEM_EVENTS_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$600()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/google/android/apps/analytics/PersistentHitStore;->CREATE_HITS_TABLE:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$702(Lcom/google/android/apps/analytics/PersistentHitStore;Lcom/google/android/apps/analytics/CustomVariableBuffer;)Lcom/google/android/apps/analytics/CustomVariableBuffer;
    .locals 0

    iput-object p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    return-object p1
.end method

.method static synthetic access$800(Lcom/google/android/apps/analytics/PersistentHitStore;Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;Z)V
    .locals 0

    invoke-direct {p0, p1, p2, p3}, Lcom/google/android/apps/analytics/PersistentHitStore;->putEvent(Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;Z)V

    return-void
.end method

.method static synthetic access$900(Landroid/database/sqlite/SQLiteDatabase;)Z
    .locals 0

    invoke-static {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z

    move-result p0

    return p0
.end method

.method private static endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z
    .locals 3

    :try_start_0
    invoke-virtual {p0}, Landroid/database/sqlite/SQLiteDatabase;->endTransaction()V
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    const/4 p0, 0x1

    return p0

    :catch_0
    move-exception p0

    const-string v0, "GoogleAnalyticsTracker"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "exception ending transaction:"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    invoke-static {v0, p0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const/4 p0, 0x0

    return p0
.end method

.method static formatReferrer(Ljava/lang/String;)Ljava/lang/String;
    .locals 8

    const/4 v0, 0x0

    if-nez p0, :cond_0

    return-object v0

    :cond_0
    const-string v1, "="

    invoke-virtual {p0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_1

    goto :goto_0

    :cond_1
    const-string v1, "%3D"

    invoke-virtual {p0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_b

    :try_start_0
    const-string v1, "UTF-8"

    invoke-static {p0, v1}, Ljava/net/URLDecoder;->decode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    :goto_0
    invoke-static {p0}, Lcom/google/android/apps/analytics/Utils;->parseURLParameters(Ljava/lang/String;)Ljava/util/Map;

    move-result-object p0

    const-string v1, "utm_campaign"

    invoke-interface {p0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-eqz v1, :cond_2

    const/4 v1, 0x1

    goto :goto_1

    :cond_2
    const/4 v1, 0x0

    :goto_1
    const-string v4, "utm_medium"

    invoke-interface {p0, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    if-eqz v4, :cond_3

    const/4 v4, 0x1

    goto :goto_2

    :cond_3
    const/4 v4, 0x0

    :goto_2
    const-string v5, "utm_source"

    invoke-interface {p0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    if-eqz v5, :cond_4

    const/4 v5, 0x1

    goto :goto_3

    :cond_4
    const/4 v5, 0x0

    :goto_3
    const-string v6, "gclid"

    invoke-interface {p0, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    if-eqz v6, :cond_5

    const/4 v6, 0x1

    goto :goto_4

    :cond_5
    const/4 v6, 0x0

    :goto_4
    if-nez v6, :cond_7

    if-eqz v1, :cond_6

    if-eqz v4, :cond_6

    if-nez v5, :cond_7

    :cond_6
    const-string p0, "GoogleAnalyticsTracker"

    const-string v1, "Badly formatted referrer missing campaign, medium and source or click ID"

    invoke-static {p0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-object v0

    :cond_7
    const/4 v0, 0x7

    new-array v0, v0, [[Ljava/lang/String;

    const/4 v1, 0x2

    new-array v4, v1, [Ljava/lang/String;

    const-string v5, "utmcid"

    aput-object v5, v4, v2

    const-string v5, "utm_id"

    invoke-interface {p0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    aput-object v5, v4, v3

    aput-object v4, v0, v2

    new-array v4, v1, [Ljava/lang/String;

    const-string v5, "utmcsr"

    aput-object v5, v4, v2

    const-string v5, "utm_source"

    invoke-interface {p0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    aput-object v5, v4, v3

    aput-object v4, v0, v3

    new-array v4, v1, [Ljava/lang/String;

    const-string v5, "utmgclid"

    aput-object v5, v4, v2

    const-string v5, "gclid"

    invoke-interface {p0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    aput-object v5, v4, v3

    aput-object v4, v0, v1

    const/4 v4, 0x3

    new-array v5, v1, [Ljava/lang/String;

    const-string v6, "utmccn"

    aput-object v6, v5, v2

    const-string v6, "utm_campaign"

    invoke-interface {p0, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    aput-object v6, v5, v3

    aput-object v5, v0, v4

    const/4 v4, 0x4

    new-array v5, v1, [Ljava/lang/String;

    const-string v6, "utmcmd"

    aput-object v6, v5, v2

    const-string v6, "utm_medium"

    invoke-interface {p0, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    aput-object v6, v5, v3

    aput-object v5, v0, v4

    const/4 v4, 0x5

    new-array v5, v1, [Ljava/lang/String;

    const-string v6, "utmctr"

    aput-object v6, v5, v2

    const-string v6, "utm_term"

    invoke-interface {p0, v6}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    aput-object v6, v5, v3

    aput-object v5, v0, v4

    const/4 v4, 0x6

    new-array v1, v1, [Ljava/lang/String;

    const-string v5, "utmcct"

    aput-object v5, v1, v2

    const-string v5, "utm_content"

    invoke-interface {p0, v5}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    aput-object p0, v1, v3

    aput-object v1, v0, v4

    new-instance p0, Ljava/lang/StringBuilder;

    invoke-direct {p0}, Ljava/lang/StringBuilder;-><init>()V

    const/4 v1, 0x0

    const/4 v4, 0x1

    :goto_5
    array-length v5, v0

    if-ge v1, v5, :cond_a

    aget-object v5, v0, v1

    aget-object v5, v5, v3

    if-eqz v5, :cond_9

    aget-object v5, v0, v1

    aget-object v5, v5, v3

    const-string v6, "+"

    const-string v7, "%20"

    invoke-virtual {v5, v6, v7}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v5

    const-string v6, " "

    const-string v7, "%20"

    invoke-virtual {v5, v6, v7}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v5

    if-eqz v4, :cond_8

    const/4 v4, 0x0

    goto :goto_6

    :cond_8
    const-string v6, "|"

    invoke-virtual {p0, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :goto_6
    aget-object v6, v0, v1

    aget-object v6, v6, v2

    invoke-virtual {p0, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, "="

    invoke-virtual {p0, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    :cond_9
    add-int/lit8 v1, v1, 0x1

    goto :goto_5

    :cond_a
    invoke-virtual {p0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p0

    return-object p0

    :catch_0
    :cond_b
    return-object v0
.end method

.method private getAndUpdateReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;
    .locals 13

    invoke-virtual {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->readCurrentReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;

    move-result-object v0

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return-object v1

    :cond_0
    invoke-virtual {v0}, Lcom/google/android/apps/analytics/Referrer;->getTimeStamp()J

    move-result-wide v2

    const-wide/16 v4, 0x0

    cmp-long v6, v2, v4

    if-eqz v6, :cond_1

    return-object v0

    :cond_1
    invoke-virtual {v0}, Lcom/google/android/apps/analytics/Referrer;->getIndex()I

    move-result v12

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/Referrer;->getReferrerString()Ljava/lang/String;

    move-result-object v8

    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "referrer"

    invoke-virtual {v0, v2, v8}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "timestamp_referrer"

    iget-wide v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "referrer_visit"

    iget v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "referrer_index"

    invoke-static {v12}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    invoke-direct {p0, p1, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->setReferrerDatabase(Landroid/database/sqlite/SQLiteDatabase;Landroid/content/ContentValues;)Z

    move-result p1

    if-eqz p1, :cond_2

    new-instance p1, Lcom/google/android/apps/analytics/Referrer;

    iget-wide v9, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    iget v11, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    move-object v7, p1

    invoke-direct/range {v7 .. v12}, Lcom/google/android/apps/analytics/Referrer;-><init>(Ljava/lang/String;JII)V

    return-object p1

    :cond_2
    return-object v1
.end method

.method private putEvent(Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;Z)V
    .locals 6

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Event;->isSessionInitialized()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->random:Ljava/util/Random;

    const v1, 0x7fffffff

    invoke-virtual {v0, v1}, Ljava/util/Random;->nextInt(I)I

    move-result v0

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setRandomVal(I)V

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    long-to-int v0, v0

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setTimestampFirst(I)V

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    long-to-int v0, v0

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setTimestampPrevious(I)V

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    long-to-int v0, v0

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setTimestampCurrent(I)V

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setVisits(I)V

    :cond_0
    iget-boolean v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->anonymizeIp:Z

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setAnonymizeIp(Z)V

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Event;->getUserId()I

    move-result v0

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setUserId(I)V

    :cond_1
    invoke-virtual {p0, p1, p2}, Lcom/google/android/apps/analytics/PersistentHitStore;->putCustomVariables(Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;)V

    invoke-direct {p0, p2}, Lcom/google/android/apps/analytics/PersistentHitStore;->getAndUpdateReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;

    move-result-object v0

    iget-object v1, p1, Lcom/google/android/apps/analytics/Event;->accountId:Ljava/lang/String;

    const-string v2, ","

    invoke-virtual {v1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    array-length v2, v1

    const/4 v3, 0x1

    if-ne v2, v3, :cond_2

    invoke-virtual {p0, p1, v0, p2, p3}, Lcom/google/android/apps/analytics/PersistentHitStore;->writeEventToDatabase(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;Landroid/database/sqlite/SQLiteDatabase;Z)V

    goto :goto_1

    :cond_2
    array-length v2, v1

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v2, :cond_3

    aget-object v4, v1, v3

    new-instance v5, Lcom/google/android/apps/analytics/Event;

    invoke-direct {v5, p1, v4}, Lcom/google/android/apps/analytics/Event;-><init>(Lcom/google/android/apps/analytics/Event;Ljava/lang/String;)V

    invoke-virtual {p0, v5, v0, p2, p3}, Lcom/google/android/apps/analytics/PersistentHitStore;->writeEventToDatabase(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;Landroid/database/sqlite/SQLiteDatabase;Z)V

    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    :cond_3
    :goto_1
    return-void
.end method

.method private setReferrerDatabase(Landroid/database/sqlite/SQLiteDatabase;Landroid/content/ContentValues;)Z
    .locals 3

    const/4 v0, 0x0

    :try_start_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    const-string v1, "referrer"

    const/4 v2, 0x0

    invoke-virtual {p1, v1, v2, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    const-string v1, "referrer"

    invoke-virtual {p1, v1, v2, p2}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p2

    if-eqz p2, :cond_0

    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z

    move-result p1

    if-nez p1, :cond_0

    return v0

    :cond_0
    const/4 p1, 0x1

    return p1

    :catchall_0
    move-exception p2

    goto :goto_0

    :catch_0
    move-exception p2

    :try_start_1
    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {p2}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p2

    if-eqz p2, :cond_1

    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z

    move-result p1

    if-nez p1, :cond_1

    :cond_1
    return v0

    :goto_0
    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_2

    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z

    move-result p1

    if-nez p1, :cond_2

    return v0

    :cond_2
    throw p2
.end method


# virtual methods
.method public clearReferrer()V
    .locals 3

    :try_start_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    const-string v1, "referrer"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2, v2}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public declared-synchronized deleteHit(J)V
    .locals 6

    monitor-enter p0

    :try_start_0
    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I

    iget-object v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1

    const-string v2, "hits"

    const-string v3, "hit_id = ?"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/String;

    const/4 v5, 0x0

    invoke-static {p1, p2}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v4, v5

    invoke-virtual {v1, v2, v3, v4}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    move-result p1

    sub-int/2addr v0, p1

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    goto :goto_1

    :catch_0
    move-exception p1

    :try_start_1
    const-string p2, "GoogleAnalyticsTracker"

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :goto_0
    monitor-exit p0

    return-void

    :goto_1
    monitor-exit p0

    throw p1
.end method

.method getCustomVariables(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/CustomVariableBuffer;
    .locals 10

    new-instance v0, Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-direct {v0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;-><init>()V

    const/4 v1, 0x0

    :try_start_0
    const-string v3, "custom_variables"

    const/4 v4, 0x0

    const-string v5, "event_id= ?"

    const/4 v2, 0x1

    new-array v6, v2, [Ljava/lang/String;

    const/4 v2, 0x0

    invoke-static {p1, p2}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v6, v2

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v2, p3

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :goto_0
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z

    move-result p2

    if-eqz p2, :cond_0

    new-instance p2, Lcom/google/android/apps/analytics/CustomVariable;

    const-string p3, "cv_index"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getInt(I)I

    move-result p3

    const-string v1, "cv_name"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v1

    const-string v2, "cv_value"

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    const-string v3, "cv_scope"

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getInt(I)I

    move-result v3

    invoke-direct {p2, p3, v1, v2, v3}, Lcom/google/android/apps/analytics/CustomVariable;-><init>(ILjava/lang/String;Ljava/lang/String;I)V

    invoke-virtual {v0, p2}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->setCustomVariable(Lcom/google/android/apps/analytics/CustomVariable;)V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    goto :goto_2

    :catchall_0
    move-exception p2

    goto :goto_3

    :catch_0
    move-exception p2

    move-object v1, p1

    goto :goto_1

    :catchall_1
    move-exception p2

    move-object p1, v1

    goto :goto_3

    :catch_1
    move-exception p2

    :goto_1
    :try_start_2
    const-string p1, "GoogleAnalyticsTracker"

    invoke-virtual {p2}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v1, :cond_1

    invoke-interface {v1}, Landroid/database/Cursor;->close()V

    :cond_1
    :goto_2
    return-object v0

    :goto_3
    if-eqz p1, :cond_2

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    throw p2
.end method

.method getDatabaseHelper()Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;
    .locals 1

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    return-object v0
.end method

.method getItem(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Item;
    .locals 9

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "item_events"

    const/4 v3, 0x0

    const-string v4, "event_id= ?"

    const/4 v1, 0x1

    new-array v5, v1, [Ljava/lang/String;

    const/4 v1, 0x0

    invoke-static {p1, p2}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v5, v1

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v1, p3

    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_1

    new-instance p2, Lcom/google/android/apps/analytics/Item$Builder;

    const-string p3, "order_id"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    const-string p3, "item_sku"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string p3, "item_price"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getDouble(I)D

    move-result-wide v4

    const-string p3, "item_count"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v6

    move-object v1, p2

    invoke-direct/range {v1 .. v7}, Lcom/google/android/apps/analytics/Item$Builder;-><init>(Ljava/lang/String;Ljava/lang/String;DJ)V

    const-string p3, "item_name"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Lcom/google/android/apps/analytics/Item$Builder;->setItemName(Ljava/lang/String;)Lcom/google/android/apps/analytics/Item$Builder;

    move-result-object p2

    const-string p3, "item_category"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Lcom/google/android/apps/analytics/Item$Builder;->setItemCategory(Ljava/lang/String;)Lcom/google/android/apps/analytics/Item$Builder;

    move-result-object p2

    invoke-virtual {p2}, Lcom/google/android/apps/analytics/Item$Builder;->build()Lcom/google/android/apps/analytics/Item;

    move-result-object p2
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz p1, :cond_0

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_0
    return-object p2

    :cond_1
    if-eqz p1, :cond_2

    goto :goto_1

    :catch_0
    move-exception p2

    goto :goto_0

    :catchall_0
    move-exception p2

    move-object p1, v0

    goto :goto_2

    :catch_1
    move-exception p2

    move-object p1, v0

    :goto_0
    :try_start_2
    const-string p3, "GoogleAnalyticsTracker"

    invoke-virtual {p2}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p3, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz p1, :cond_2

    :goto_1
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    return-object v0

    :catchall_1
    move-exception p2

    :goto_2
    if-eqz p1, :cond_3

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_3
    throw p2
.end method

.method public getNumStoredHits()I
    .locals 1

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I

    return v0
.end method

.method public getNumStoredHitsFromDb()I
    .locals 6

    const/4 v0, 0x0

    const/4 v1, 0x0

    :try_start_0
    iget-object v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v2}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v2

    const-string v3, "SELECT COUNT(*) from hits"

    invoke-virtual {v2, v3, v0}, Landroid/database/sqlite/SQLiteDatabase;->rawQuery(Ljava/lang/String;[Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :try_start_1
    invoke-interface {v2}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-interface {v2, v1}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v3
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    long-to-int v0, v3

    move v1, v0

    :cond_0
    if-eqz v2, :cond_1

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v1, v0

    move-object v0, v2

    goto :goto_2

    :catch_0
    move-exception v0

    move-object v5, v2

    move-object v2, v0

    move-object v0, v5

    goto :goto_0

    :catchall_1
    move-exception v1

    goto :goto_2

    :catch_1
    move-exception v2

    :goto_0
    :try_start_2
    const-string v3, "GoogleAnalyticsTracker"

    invoke-virtual {v2}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v3, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v0, :cond_1

    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_1
    :goto_1
    return v1

    :goto_2
    if-eqz v0, :cond_2

    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_2
    throw v1
.end method

.method public getReferrer()Lcom/google/android/apps/analytics/Referrer;
    .locals 2

    :try_start_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->readCurrentReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;

    move-result-object v0
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const/4 v0, 0x0

    return-object v0
.end method

.method public getSessionId()Ljava/lang/String;
    .locals 2

    iget-boolean v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    :cond_0
    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    long-to-int v0, v0

    invoke-static {v0}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getStoreId()I
    .locals 1

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    return v0
.end method

.method getTimestampCurrent()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    return-wide v0
.end method

.method getTimestampFirst()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    return-wide v0
.end method

.method getTimestampPrevious()J
    .locals 2

    iget-wide v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    return-wide v0
.end method

.method getTransaction(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Transaction;
    .locals 9

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "transaction_events"

    const/4 v3, 0x0

    const-string v4, "event_id= ?"

    const/4 v1, 0x1

    new-array v5, v1, [Ljava/lang/String;

    const/4 v1, 0x0

    invoke-static {p1, p2}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object p1

    aput-object p1, v5, v1

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v1, p3

    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result p2

    if-eqz p2, :cond_1

    new-instance p2, Lcom/google/android/apps/analytics/Transaction$Builder;

    const-string p3, "order_id"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p3

    const-string v1, "tran_totalcost"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getDouble(I)D

    move-result-wide v1

    invoke-direct {p2, p3, v1, v2}, Lcom/google/android/apps/analytics/Transaction$Builder;-><init>(Ljava/lang/String;D)V

    const-string p3, "tran_storename"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object p3

    invoke-virtual {p2, p3}, Lcom/google/android/apps/analytics/Transaction$Builder;->setStoreName(Ljava/lang/String;)Lcom/google/android/apps/analytics/Transaction$Builder;

    move-result-object p2

    const-string p3, "tran_totaltax"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getDouble(I)D

    move-result-wide v1

    invoke-virtual {p2, v1, v2}, Lcom/google/android/apps/analytics/Transaction$Builder;->setTotalTax(D)Lcom/google/android/apps/analytics/Transaction$Builder;

    move-result-object p2

    const-string p3, "tran_shippingcost"

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result p3

    invoke-interface {p1, p3}, Landroid/database/Cursor;->getDouble(I)D

    move-result-wide v1

    invoke-virtual {p2, v1, v2}, Lcom/google/android/apps/analytics/Transaction$Builder;->setShippingCost(D)Lcom/google/android/apps/analytics/Transaction$Builder;

    move-result-object p2

    invoke-virtual {p2}, Lcom/google/android/apps/analytics/Transaction$Builder;->build()Lcom/google/android/apps/analytics/Transaction;

    move-result-object p2
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    if-eqz p1, :cond_0

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_0
    return-object p2

    :cond_1
    if-eqz p1, :cond_2

    goto :goto_1

    :catch_0
    move-exception p2

    goto :goto_0

    :catchall_0
    move-exception p2

    move-object p1, v0

    goto :goto_2

    :catch_1
    move-exception p2

    move-object p1, v0

    :goto_0
    :try_start_2
    const-string p3, "GoogleAnalyticsTracker"

    invoke-virtual {p2}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p3, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz p1, :cond_2

    :goto_1
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    return-object v0

    :catchall_1
    move-exception p2

    :goto_2
    if-eqz p1, :cond_3

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_3
    throw p2
.end method

.method public getVisitorCustomVar(I)Ljava/lang/String;
    .locals 2

    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->getCustomVariableAt(I)Lcom/google/android/apps/analytics/CustomVariable;

    move-result-object p1

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/CustomVariable;->getScope()I

    move-result v0

    const/4 v1, 0x1

    if-eq v0, v1, :cond_0

    goto :goto_0

    :cond_0
    invoke-virtual {p1}, Lcom/google/android/apps/analytics/CustomVariable;->getValue()Ljava/lang/String;

    move-result-object p1

    return-object p1

    :cond_1
    :goto_0
    const/4 p1, 0x0

    return-object p1
.end method

.method public getVisitorId()Ljava/lang/String;
    .locals 5

    iget-boolean v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    if-nez v0, :cond_0

    const/4 v0, 0x0

    return-object v0

    :cond_0
    const-string v0, "%d.%d"

    const/4 v1, 0x2

    new-array v1, v1, [Ljava/lang/Object;

    const/4 v2, 0x0

    iget v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    aput-object v3, v1, v2

    const/4 v2, 0x1

    iget-wide v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    invoke-static {v3, v4}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    aput-object v3, v1, v2

    invoke-static {v0, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method getVisitorVarBuffer()Lcom/google/android/apps/analytics/CustomVariableBuffer;
    .locals 2

    :try_start_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getVisitorVarBuffer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object v0
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    return-object v0

    :catch_0
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance v0, Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-direct {v0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;-><init>()V

    return-object v0
.end method

.method getVisitorVarBuffer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/CustomVariableBuffer;
    .locals 11

    new-instance v0, Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-direct {v0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;-><init>()V

    const/4 v1, 0x0

    :try_start_0
    const-string v3, "custom_var_cache"

    const/4 v4, 0x0

    const-string v5, "cv_scope= ?"

    const/4 v2, 0x1

    new-array v6, v2, [Ljava/lang/String;

    const/4 v7, 0x0

    invoke-static {v2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v2

    aput-object v2, v6, v7

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    move-object v2, p1

    invoke-virtual/range {v2 .. v9}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :goto_0
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z

    move-result v1

    if-eqz v1, :cond_0

    new-instance v1, Lcom/google/android/apps/analytics/CustomVariable;

    const-string v2, "cv_index"

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {p1, v2}, Landroid/database/Cursor;->getInt(I)I

    move-result v2

    const-string v3, "cv_name"

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    const-string v4, "cv_value"

    invoke-interface {p1, v4}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v4

    invoke-interface {p1, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v4

    const-string v5, "cv_scope"

    invoke-interface {p1, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {p1, v5}, Landroid/database/Cursor;->getInt(I)I

    move-result v5

    invoke-direct {v1, v2, v3, v4, v5}, Lcom/google/android/apps/analytics/CustomVariable;-><init>(ILjava/lang/String;Ljava/lang/String;I)V

    invoke-virtual {v0, v1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->setCustomVariable(Lcom/google/android/apps/analytics/CustomVariable;)V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    goto :goto_2

    :catch_0
    move-exception v1

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object p1, v1

    goto :goto_3

    :catch_1
    move-exception p1

    move-object v10, v1

    move-object v1, p1

    move-object p1, v10

    :goto_1
    :try_start_2
    const-string v2, "GoogleAnalyticsTracker"

    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz p1, :cond_1

    :goto_2
    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_1
    return-object v0

    :catchall_1
    move-exception v0

    :goto_3
    if-eqz p1, :cond_2

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    throw v0
.end method

.method getVisits()I
    .locals 1

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    return v0
.end method

.method public loadExistingSession()V
    .locals 2

    :try_start_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->loadExistingSession(Landroid/database/sqlite/SQLiteDatabase;)V
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method

.method public loadExistingSession(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 9

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "session"

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v1, p1

    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :try_start_1
    invoke-interface {v1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2

    const/4 v3, 0x1

    const/4 v4, 0x0

    const-wide/16 v5, 0x0

    if-eqz v2, :cond_2

    invoke-interface {v1, v4}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v7

    iput-wide v7, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    invoke-interface {v1, v3}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v7

    iput-wide v7, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    const/4 v0, 0x2

    invoke-interface {v1, v0}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v7

    iput-wide v7, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    const/4 v0, 0x3

    invoke-interface {v1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    const/4 v0, 0x4

    invoke-interface {v1, v0}, Landroid/database/Cursor;->getInt(I)I

    move-result v0

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-virtual {p0, p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->readCurrentReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;

    move-result-object p1

    iget-wide v7, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    cmp-long v0, v7, v5

    if-eqz v0, :cond_0

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Referrer;->getTimeStamp()J

    move-result-wide v7

    cmp-long p1, v7, v5

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v3, 0x0

    :cond_1
    :goto_0
    iput-boolean v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    move-object v0, v1

    goto :goto_1

    :cond_2
    iput-boolean v4, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    iput-boolean v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->useStoredVisitorVars:Z

    new-instance v2, Ljava/security/SecureRandom;

    invoke-direct {v2}, Ljava/security/SecureRandom;-><init>()V

    invoke-virtual {v2}, Ljava/security/SecureRandom;->nextInt()I

    move-result v2

    const v3, 0x7fffffff

    and-int/2addr v2, v3

    iput v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-interface {v1}, Landroid/database/Cursor;->close()V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :try_start_2
    new-instance v1, Landroid/content/ContentValues;

    invoke-direct {v1}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "timestamp_first"

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "timestamp_previous"

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "timestamp_current"

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "visits"

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "store_id"

    iget v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "session"

    invoke-virtual {p1, v2, v0, v1}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J
    :try_end_2
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_2 .. :try_end_2} :catch_1
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    :goto_1
    if-eqz v0, :cond_3

    goto :goto_3

    :catchall_0
    move-exception p1

    move-object v0, v1

    goto :goto_4

    :catch_0
    move-exception p1

    move-object v0, v1

    goto :goto_2

    :catchall_1
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception p1

    :goto_2
    :try_start_3
    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v0, :cond_3

    :goto_3
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_3
    return-void

    :goto_4
    if-eqz v0, :cond_4

    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    :cond_4
    throw p1
.end method

.method public peekEvents(ILandroid/database/sqlite/SQLiteDatabase;I)[Lcom/google/android/apps/analytics/Event;
    .locals 28

    move-object/from16 v1, p0

    move-object/from16 v0, p2

    new-instance v11, Ljava/util/ArrayList;

    invoke-direct {v11}, Ljava/util/ArrayList;-><init>()V

    const/4 v12, 0x0

    const/4 v13, 0x0

    :try_start_0
    const-string v3, "events"

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const-string v9, "event_id"

    invoke-static/range {p1 .. p1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v10

    move-object/from16 v2, p2

    invoke-virtual/range {v2 .. v10}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v2
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :goto_0
    :try_start_1
    invoke-interface {v2}, Landroid/database/Cursor;->moveToNext()Z

    move-result v3

    if-eqz v3, :cond_5

    new-instance v3, Lcom/google/android/apps/analytics/Event;

    invoke-interface {v2, v12}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v14

    const/4 v4, 0x2

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v16

    const/4 v4, 0x3

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v17

    const/4 v4, 0x4

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v18

    const/4 v4, 0x5

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v19

    const/4 v4, 0x6

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v20

    const/4 v4, 0x7

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v21

    const/16 v4, 0x8

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v22

    const/16 v4, 0x9

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v23

    const/16 v4, 0xa

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v24

    const/16 v4, 0xb

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v25

    const/16 v4, 0xc

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v26

    const/16 v4, 0xd

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v27

    move-object v13, v3

    invoke-direct/range {v13 .. v27}, Lcom/google/android/apps/analytics/Event;-><init>(JLjava/lang/String;IIIIILjava/lang/String;Ljava/lang/String;Ljava/lang/String;III)V

    const/4 v4, 0x1

    invoke-interface {v2, v4}, Landroid/database/Cursor;->getInt(I)I

    move-result v5

    invoke-virtual {v3, v5}, Lcom/google/android/apps/analytics/Event;->setUserId(I)V

    const-string v5, "event_id"

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v5

    invoke-interface {v2, v5}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v5

    const-string v7, "__##GOOGLETRANSACTION##__"

    iget-object v8, v3, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v7, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_1

    invoke-virtual {v1, v5, v6, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getTransaction(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Transaction;

    move-result-object v4

    if-nez v4, :cond_0

    const-string v7, "GoogleAnalyticsTracker"

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "missing expected transaction for event "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v5, v6}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v7, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_0
    invoke-virtual {v3, v4}, Lcom/google/android/apps/analytics/Event;->setTransaction(Lcom/google/android/apps/analytics/Transaction;)V

    :goto_1
    move/from16 v7, p3

    goto :goto_3

    :cond_1
    const-string v7, "__##GOOGLEITEM##__"

    iget-object v8, v3, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v7, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_3

    invoke-virtual {v1, v5, v6, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getItem(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Item;

    move-result-object v4

    if-nez v4, :cond_2

    const-string v7, "GoogleAnalyticsTracker"

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "missing expected item for event "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v5, v6}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-static {v7, v5}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_2
    invoke-virtual {v3, v4}, Lcom/google/android/apps/analytics/Event;->setItem(Lcom/google/android/apps/analytics/Item;)V

    goto :goto_1

    :cond_3
    move/from16 v7, p3

    if-le v7, v4, :cond_4

    invoke-virtual {v1, v5, v6, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getCustomVariables(JLandroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object v4

    goto :goto_2

    :cond_4
    new-instance v4, Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-direct {v4}, Lcom/google/android/apps/analytics/CustomVariableBuffer;-><init>()V

    :goto_2
    invoke-virtual {v3, v4}, Lcom/google/android/apps/analytics/Event;->setCustomVariableBuffer(Lcom/google/android/apps/analytics/CustomVariableBuffer;)V

    :goto_3
    invoke-interface {v11, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto/16 :goto_0

    :cond_5
    if-eqz v2, :cond_6

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_6
    invoke-interface {v11}, Ljava/util/List;->size()I

    move-result v0

    new-array v0, v0, [Lcom/google/android/apps/analytics/Event;

    invoke-interface {v11, v0}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/google/android/apps/analytics/Event;

    return-object v0

    :catchall_0
    move-exception v0

    goto :goto_5

    :catch_0
    move-exception v0

    move-object v13, v2

    goto :goto_4

    :catchall_1
    move-exception v0

    move-object v2, v13

    goto :goto_5

    :catch_1
    move-exception v0

    :goto_4
    :try_start_2
    const-string v2, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-array v0, v12, [Lcom/google/android/apps/analytics/Event;
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v13, :cond_7

    invoke-interface {v13}, Landroid/database/Cursor;->close()V

    :cond_7
    return-object v0

    :goto_5
    if-eqz v2, :cond_8

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_8
    throw v0
.end method

.method public peekHits()[Lcom/google/android/apps/analytics/Hit;
    .locals 1

    const/16 v0, 0x3e8

    invoke-virtual {p0, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->peekHits(I)[Lcom/google/android/apps/analytics/Hit;

    move-result-object v0

    return-object v0
.end method

.method public peekHits(I)[Lcom/google/android/apps/analytics/Hit;
    .locals 13

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    const/4 v1, 0x0

    const/4 v2, 0x0

    :try_start_0
    iget-object v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v3}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getReadableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v4

    const-string v5, "hits"

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const-string v11, "hit_id"

    invoke-static {p1}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v12

    invoke-virtual/range {v4 .. v12}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :goto_0
    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToNext()Z

    move-result v2

    if-eqz v2, :cond_0

    new-instance v2, Lcom/google/android/apps/analytics/Hit;

    const/4 v3, 0x1

    invoke-interface {p1, v3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v4

    invoke-direct {v2, v3, v4, v5}, Lcom/google/android/apps/analytics/Hit;-><init>(Ljava/lang/String;J)V

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0

    :cond_0
    if-eqz p1, :cond_1

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_1
    invoke-interface {v0}, Ljava/util/List;->size()I

    move-result p1

    new-array p1, p1, [Lcom/google/android/apps/analytics/Hit;

    invoke-interface {v0, p1}, Ljava/util/List;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object p1

    check-cast p1, [Lcom/google/android/apps/analytics/Hit;

    return-object p1

    :catchall_0
    move-exception v0

    move-object v2, p1

    goto :goto_2

    :catch_0
    move-exception v0

    move-object v2, p1

    goto :goto_1

    :catchall_1
    move-exception v0

    goto :goto_2

    :catch_1
    move-exception v0

    :goto_1
    :try_start_2
    const-string p1, "GoogleAnalyticsTracker"

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-array p1, v1, [Lcom/google/android/apps/analytics/Hit;
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz v2, :cond_2

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_2
    return-object p1

    :goto_2
    if-eqz v2, :cond_3

    invoke-interface {v2}, Landroid/database/Cursor;->close()V

    :cond_3
    throw v0
.end method

.method putCustomVariables(Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 10

    const-string v0, "__##GOOGLEITEM##__"

    iget-object v1, p1, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_7

    const-string v0, "__##GOOGLETRANSACTION##__"

    iget-object v1, p1, Lcom/google/android/apps/analytics/Event;->category:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto/16 :goto_3

    :cond_0
    :try_start_0
    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Event;->getCustomVariableBuffer()Lcom/google/android/apps/analytics/CustomVariableBuffer;

    move-result-object v0

    iget-boolean v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->useStoredVisitorVars:Z

    const/4 v2, 0x5

    const/4 v3, 0x0

    const/4 v4, 0x1

    if-eqz v1, :cond_4

    if-nez v0, :cond_1

    new-instance v0, Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-direct {v0}, Lcom/google/android/apps/analytics/CustomVariableBuffer;-><init>()V

    invoke-virtual {p1, v0}, Lcom/google/android/apps/analytics/Event;->setCustomVariableBuffer(Lcom/google/android/apps/analytics/CustomVariableBuffer;)V

    :cond_1
    const/4 p1, 0x1

    :goto_0
    if-gt p1, v2, :cond_3

    iget-object v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-virtual {v1, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->getCustomVariableAt(I)Lcom/google/android/apps/analytics/CustomVariable;

    move-result-object v1

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->getCustomVariableAt(I)Lcom/google/android/apps/analytics/CustomVariable;

    move-result-object v5

    if-eqz v1, :cond_2

    if-nez v5, :cond_2

    invoke-virtual {v0, v1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->setCustomVariable(Lcom/google/android/apps/analytics/CustomVariable;)V

    :cond_2
    add-int/lit8 p1, p1, 0x1

    goto :goto_0

    :cond_3
    iput-boolean v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->useStoredVisitorVars:Z

    :cond_4
    if-eqz v0, :cond_7

    const/4 p1, 0x1

    :goto_1
    if-gt p1, v2, :cond_7

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->isIndexAvailable(I)Z

    move-result v1

    if-nez v1, :cond_6

    invoke-virtual {v0, p1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->getCustomVariableAt(I)Lcom/google/android/apps/analytics/CustomVariable;

    move-result-object v1

    new-instance v5, Landroid/content/ContentValues;

    invoke-direct {v5}, Landroid/content/ContentValues;-><init>()V

    const-string v6, "event_id"

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v7

    invoke-virtual {v5, v6, v7}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v6, "cv_index"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v7

    invoke-static {v7}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v7

    invoke-virtual {v5, v6, v7}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v6, "cv_name"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v5, v6, v7}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v6, "cv_scope"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getScope()I

    move-result v7

    invoke-static {v7}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v7

    invoke-virtual {v5, v6, v7}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v6, "cv_value"

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getValue()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v5, v6, v7}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string v6, "custom_var_cache"

    const-string v7, "cv_index = ?"

    new-array v8, v4, [Ljava/lang/String;

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v9

    invoke-static {v9}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v3

    invoke-virtual {p2, v6, v5, v7, v8}, Landroid/database/sqlite/SQLiteDatabase;->update(Ljava/lang/String;Landroid/content/ContentValues;Ljava/lang/String;[Ljava/lang/String;)I

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getScope()I

    move-result v5

    if-ne v5, v4, :cond_5

    iget-object v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-virtual {v5, v1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->setCustomVariable(Lcom/google/android/apps/analytics/CustomVariable;)V

    goto :goto_2

    :cond_5
    iget-object v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visitorCVCache:Lcom/google/android/apps/analytics/CustomVariableBuffer;

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/CustomVariable;->getIndex()I

    move-result v1

    invoke-virtual {v5, v1}, Lcom/google/android/apps/analytics/CustomVariableBuffer;->clearCustomVariableAt(I)V
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    :cond_6
    :goto_2
    add-int/lit8 p1, p1, 0x1

    goto :goto_1

    :catch_0
    move-exception p1

    const-string p2, "GoogleAnalyticsTracker"

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    :cond_7
    :goto_3
    return-void
.end method

.method public putEvent(Lcom/google/android/apps/analytics/Event;)V
    .locals 4

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I

    const/16 v1, 0x3e8

    if-lt v0, v1, :cond_0

    const-string p1, "GoogleAnalyticsTracker"

    const-string v0, "Store full. Not storing last event."

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sampleRate:I

    const/16 v1, 0x64

    if-eq v0, v1, :cond_3

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Event;->getUserId()I

    move-result v0

    const/4 v2, -0x1

    if-ne v0, v2, :cond_1

    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    goto :goto_0

    :cond_1
    invoke-virtual {p1}, Lcom/google/android/apps/analytics/Event;->getUserId()I

    move-result v0

    :goto_0
    rem-int/lit16 v0, v0, 0x2710

    iget v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sampleRate:I

    mul-int/lit8 v2, v2, 0x64

    if-lt v0, v2, :cond_3

    invoke-static {}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getInstance()Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/GoogleAnalyticsTracker;->getDebug()Z

    move-result p1

    if-eqz p1, :cond_2

    const-string p1, "GoogleAnalyticsTracker"

    const-string v0, "User has been sampled out. Aborting hit."

    invoke-static {p1, v0}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    :cond_2
    return-void

    :cond_3
    monitor-enter p0

    :try_start_0
    iget-object v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v0}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v0
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :try_start_1
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->beginTransaction()V

    iget-boolean v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    if-nez v1, :cond_4

    invoke-virtual {p0, v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->storeUpdatedSession(Landroid/database/sqlite/SQLiteDatabase;)V

    :cond_4
    const/4 v1, 0x1

    invoke-direct {p0, p1, v0, v1}, Lcom/google/android/apps/analytics/PersistentHitStore;->putEvent(Lcom/google/android/apps/analytics/Event;Landroid/database/sqlite/SQLiteDatabase;Z)V

    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->setTransactionSuccessful()V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    :try_start_2
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_5

    :goto_1
    invoke-static {v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    goto :goto_2

    :catchall_0
    move-exception p1

    goto :goto_3

    :catch_0
    move-exception p1

    :try_start_3
    const-string v1, "GoogleAnalyticsTracker"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "putEventOuter:"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    :try_start_4
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result p1

    if-eqz p1, :cond_5

    goto :goto_1

    :cond_5
    :goto_2
    monitor-exit p0

    return-void

    :goto_3
    invoke-virtual {v0}, Landroid/database/sqlite/SQLiteDatabase;->inTransaction()Z

    move-result v1

    if-eqz v1, :cond_6

    invoke-static {v0}, Lcom/google/android/apps/analytics/PersistentHitStore;->endTransaction(Landroid/database/sqlite/SQLiteDatabase;)Z

    :cond_6
    throw p1

    :catchall_1
    move-exception p1

    goto :goto_4

    :catch_1
    move-exception p1

    const-string v0, "GoogleAnalyticsTracker"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Can\'t get db: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    monitor-exit p0

    return-void

    :goto_4
    monitor-exit p0
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    throw p1
.end method

.method readCurrentReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;
    .locals 10

    const/4 v0, 0x0

    :try_start_0
    const-string v2, "referrer"

    const-string v1, "referrer"

    const-string v3, "timestamp_referrer"

    const-string v4, "referrer_visit"

    const-string v5, "referrer_index"

    filled-new-array {v1, v3, v4, v5}, [Ljava/lang/String;

    move-result-object v3

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    move-object v1, p1

    invoke-virtual/range {v1 .. v8}, Landroid/database/sqlite/SQLiteDatabase;->query(Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object p1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    :try_start_1
    invoke-interface {p1}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v1

    if-eqz v1, :cond_0

    const-string v1, "timestamp_referrer"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getLong(I)J

    move-result-wide v4

    const-string v1, "referrer_visit"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v6

    const-string v1, "referrer_index"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getInt(I)I

    move-result v7

    const-string v1, "referrer"

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v1

    invoke-interface {p1, v1}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3

    new-instance v1, Lcom/google/android/apps/analytics/Referrer;

    move-object v2, v1

    invoke-direct/range {v2 .. v7}, Lcom/google/android/apps/analytics/Referrer;-><init>(Ljava/lang/String;JII)V
    :try_end_1
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    move-object v0, v1

    :cond_0
    if-eqz p1, :cond_1

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_1
    return-object v0

    :catch_0
    move-exception v1

    goto :goto_0

    :catchall_0
    move-exception p1

    move-object v9, v0

    move-object v0, p1

    move-object p1, v9

    goto :goto_1

    :catch_1
    move-exception v1

    move-object p1, v0

    :goto_0
    :try_start_2
    const-string v2, "GoogleAnalyticsTracker"

    invoke-virtual {v1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    if-eqz p1, :cond_2

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_2
    return-object v0

    :catchall_1
    move-exception v0

    :goto_1
    if-eqz p1, :cond_3

    invoke-interface {p1}, Landroid/database/Cursor;->close()V

    :cond_3
    throw v0
.end method

.method public setAnonymizeIp(Z)V
    .locals 0

    iput-boolean p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->anonymizeIp:Z

    return-void
.end method

.method public setReferrer(Ljava/lang/String;)Z
    .locals 12

    invoke-static {p1}, Lcom/google/android/apps/analytics/PersistentHitStore;->formatReferrer(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return v0

    :cond_0
    :try_start_0
    iget-object v1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {v1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object v1
    :try_end_0
    .catch Landroid/database/sqlite/SQLiteException; {:try_start_0 .. :try_end_0} :catch_0

    invoke-virtual {p0, v1}, Lcom/google/android/apps/analytics/PersistentHitStore;->readCurrentReferrer(Landroid/database/sqlite/SQLiteDatabase;)Lcom/google/android/apps/analytics/Referrer;

    move-result-object v2

    new-instance v3, Landroid/content/ContentValues;

    invoke-direct {v3}, Landroid/content/ContentValues;-><init>()V

    const-string v4, "referrer"

    invoke-virtual {v3, v4, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "timestamp_referrer"

    const-wide/16 v4, 0x0

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v6

    invoke-virtual {v3, p1, v6}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string p1, "referrer_visit"

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v3, p1, v6}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-wide/16 v6, 0x1

    if-eqz v2, :cond_2

    invoke-virtual {v2}, Lcom/google/android/apps/analytics/Referrer;->getIndex()I

    move-result p1

    int-to-long v8, p1

    invoke-virtual {v2}, Lcom/google/android/apps/analytics/Referrer;->getTimeStamp()J

    move-result-wide v10

    cmp-long p1, v10, v4

    if-lez p1, :cond_1

    add-long/2addr v6, v8

    goto :goto_0

    :cond_1
    move-wide v6, v8

    :cond_2
    :goto_0
    const-string p1, "referrer_index"

    invoke-static {v6, v7}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v3, p1, v2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    invoke-direct {p0, v1, v3}, Lcom/google/android/apps/analytics/PersistentHitStore;->setReferrerDatabase(Landroid/database/sqlite/SQLiteDatabase;Landroid/content/ContentValues;)Z

    move-result p1

    if-eqz p1, :cond_3

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->startNewVisit()V

    const/4 p1, 0x1

    return p1

    :cond_3
    return v0

    :catch_0
    move-exception p1

    const-string v1, "GoogleAnalyticsTracker"

    invoke-virtual {p1}, Landroid/database/sqlite/SQLiteException;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return v0
.end method

.method public setSampleRate(I)V
    .locals 0

    iput p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sampleRate:I

    return-void
.end method

.method public declared-synchronized startNewVisit()V
    .locals 1

    monitor-enter p0

    const/4 v0, 0x0

    :try_start_0
    iput-boolean v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->useStoredVisitorVars:Z

    invoke-virtual {p0}, Lcom/google/android/apps/analytics/PersistentHitStore;->getNumStoredHitsFromDb()I

    move-result v0

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method storeUpdatedSession(Landroid/database/sqlite/SQLiteDatabase;)V
    .locals 7

    iget-object p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->databaseHelper:Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;

    invoke-virtual {p1}, Lcom/google/android/apps/analytics/PersistentHitStore$DataBaseHelper;->getWritableDatabase()Landroid/database/sqlite/SQLiteDatabase;

    move-result-object p1

    const-string v0, "session"

    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1, v1}, Landroid/database/sqlite/SQLiteDatabase;->delete(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;)I

    iget-wide v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    const-wide/16 v4, 0x0

    cmp-long v0, v2, v4

    const-wide/16 v2, 0x3e8

    const/4 v4, 0x1

    if-nez v0, :cond_0

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v5

    div-long/2addr v5, v2

    iput-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    iput-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    iput-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    iput v4, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    goto :goto_0

    :cond_0
    iget-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    iput-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v5

    div-long/2addr v5, v2

    iput-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    iget-wide v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    iget-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    cmp-long v0, v2, v5

    if-nez v0, :cond_1

    iget-wide v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    const-wide/16 v5, 0x1

    add-long/2addr v2, v5

    iput-wide v2, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    :cond_1
    iget v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    add-int/2addr v0, v4

    iput v0, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    :goto_0
    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    const-string v2, "timestamp_first"

    iget-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampFirst:J

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "timestamp_previous"

    iget-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampPrevious:J

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "timestamp_current"

    iget-wide v5, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->timestampCurrent:J

    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string v2, "visits"

    iget v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->visits:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "store_id"

    iget v3, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->storeId:I

    invoke-static {v3}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Integer;)V

    const-string v2, "session"

    invoke-virtual {p1, v2, v1, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    iput-boolean v4, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->sessionStarted:Z

    return-void
.end method

.method writeEventToDatabase(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;Landroid/database/sqlite/SQLiteDatabase;Z)V
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Landroid/database/sqlite/SQLiteException;
        }
    .end annotation

    new-instance v0, Landroid/content/ContentValues;

    invoke-direct {v0}, Landroid/content/ContentValues;-><init>()V

    invoke-static {p1, p2}, Lcom/google/android/apps/analytics/HitBuilder;->constructHitRequestPath(Lcom/google/android/apps/analytics/Event;Lcom/google/android/apps/analytics/Referrer;)Ljava/lang/String;

    move-result-object p1

    const-string p2, "hit_string"

    invoke-virtual {v0, p2, p1}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/String;)V

    const-string p1, "hit_time"

    if-eqz p4, :cond_0

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v1

    goto :goto_0

    :cond_0
    const-wide/16 v1, 0x0

    :goto_0
    invoke-static {v1, v2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p2

    invoke-virtual {v0, p1, p2}, Landroid/content/ContentValues;->put(Ljava/lang/String;Ljava/lang/Long;)V

    const-string p1, "hits"

    const/4 p2, 0x0

    invoke-virtual {p3, p1, p2, v0}, Landroid/database/sqlite/SQLiteDatabase;->insert(Ljava/lang/String;Ljava/lang/String;Landroid/content/ContentValues;)J

    iget p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I

    add-int/lit8 p1, p1, 0x1

    iput p1, p0, Lcom/google/android/apps/analytics/PersistentHitStore;->numStoredHits:I

    return-void
.end method
