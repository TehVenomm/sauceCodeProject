.class public Lcom/facebook/appevents/UserDataStore;
.super Ljava/lang/Object;
.source "UserDataStore.java"


# annotations
.annotation build Landroidx/annotation/RestrictTo;
    value = {
        .enum Landroidx/annotation/RestrictTo$Scope;->LIBRARY_GROUP:Landroidx/annotation/RestrictTo$Scope;
    }
.end annotation


# static fields
.field public static final CITY:Ljava/lang/String; = "ct"

.field public static final COUNTRY:Ljava/lang/String; = "country"

.field private static final DATA_SEPARATOR:Ljava/lang/String; = ","

.field public static final DATE_OF_BIRTH:Ljava/lang/String; = "db"

.field public static final EMAIL:Ljava/lang/String; = "em"

.field public static final FIRST_NAME:Ljava/lang/String; = "fn"

.field public static final GENDER:Ljava/lang/String; = "ge"

.field private static final INTERNAL_USER_DATA_KEY:Ljava/lang/String; = "com.facebook.appevents.UserDataStore.internalUserData"

.field public static final LAST_NAME:Ljava/lang/String; = "ln"

.field private static final MAX_NUM:I = 0x5

.field public static final PHONE:Ljava/lang/String; = "ph"

.field public static final STATE:Ljava/lang/String; = "st"

.field private static final TAG:Ljava/lang/String; = "UserDataStore"

.field private static final USER_DATA_KEY:Ljava/lang/String; = "com.facebook.appevents.UserDataStore.userData"

.field public static final ZIP:Ljava/lang/String; = "zp"

.field private static final externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/ConcurrentHashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private static final internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/concurrent/ConcurrentHashMap<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field private static sharedPreferences:Landroid/content/SharedPreferences;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .line 55
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v1, 0x0

    invoke-direct {v0, v1}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>(Z)V

    sput-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 58
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    sput-object v0, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    .line 60
    new-instance v0, Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0}, Ljava/util/concurrent/ConcurrentHashMap;-><init>()V

    sput-object v0, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 47
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method static synthetic access$000()Ljava/util/concurrent/atomic/AtomicBoolean;
    .locals 1

    .line 47
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    return-object v0
.end method

.method static synthetic access$100()V
    .locals 0

    .line 47
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    return-void
.end method

.method static synthetic access$200()Landroid/content/SharedPreferences;
    .locals 1

    .line 47
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->sharedPreferences:Landroid/content/SharedPreferences;

    return-object v0
.end method

.method static synthetic access$300()Ljava/lang/String;
    .locals 1

    .line 47
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$400(Landroid/os/Bundle;)V
    .locals 0

    .line 47
    invoke-static {p0}, Lcom/facebook/appevents/UserDataStore;->updateHashUserData(Landroid/os/Bundle;)V

    return-void
.end method

.method static synthetic access$500()Ljava/util/concurrent/ConcurrentHashMap;
    .locals 1

    .line 47
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    return-object v0
.end method

.method static synthetic access$600(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 47
    invoke-static {p0, p1}, Lcom/facebook/appevents/UserDataStore;->writeDataIntoCache(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$700()Ljava/util/concurrent/ConcurrentHashMap;
    .locals 1

    .line 47
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    return-object v0
.end method

.method static clear()V
    .locals 2

    .line 162
    invoke-static {}, Lcom/facebook/appevents/InternalAppEventsLogger;->getAnalyticsExecutor()Ljava/util/concurrent/Executor;

    move-result-object v0

    new-instance v1, Lcom/facebook/appevents/UserDataStore$3;

    invoke-direct {v1}, Lcom/facebook/appevents/UserDataStore$3;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static getAllHashedUserData()Ljava/lang/String;
    .locals 2

    .line 196
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    .line 197
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    .line 199
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 200
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-interface {v0, v1}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 201
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-interface {v0, v1}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 202
    invoke-static {v0}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static getHashedUserData()Ljava/lang/String;
    .locals 2

    .line 188
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    .line 189
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->TAG:Ljava/lang/String;

    const-string v1, "initStore should have been called before calling setUserID"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 190
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    .line 192
    :cond_0
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v0}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static getInternalHashedUserData()Ljava/util/Map;
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .line 220
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    .line 221
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    .line 223
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    sget-object v1, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-direct {v0, v1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    return-object v0
.end method

.method private static declared-synchronized initAndWait()V
    .locals 5

    const-class v0, Lcom/facebook/appevents/UserDataStore;

    monitor-enter v0

    .line 206
    :try_start_0
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v1}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_0

    .line 207
    monitor-exit v0

    return-void

    .line 211
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/facebook/FacebookSdk;->getApplicationContext()Landroid/content/Context;

    move-result-object v1

    .line 210
    invoke-static {v1}, Landroid/preference/PreferenceManager;->getDefaultSharedPreferences(Landroid/content/Context;)Landroid/content/SharedPreferences;

    move-result-object v1

    sput-object v1, Lcom/facebook/appevents/UserDataStore;->sharedPreferences:Landroid/content/SharedPreferences;

    .line 212
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->sharedPreferences:Landroid/content/SharedPreferences;

    const-string v2, "com.facebook.appevents.UserDataStore.userData"

    const-string v3, ""

    invoke-interface {v1, v2, v3}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 213
    sget-object v2, Lcom/facebook/appevents/UserDataStore;->sharedPreferences:Landroid/content/SharedPreferences;

    const-string v3, "com.facebook.appevents.UserDataStore.internalUserData"

    const-string v4, ""

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 214
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v1}, Lcom/facebook/internal/Utility;->JsonStrToMap(Ljava/lang/String;)Ljava/util/Map;

    move-result-object v1

    invoke-virtual {v3, v1}, Ljava/util/concurrent/ConcurrentHashMap;->putAll(Ljava/util/Map;)V

    .line 215
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v2}, Lcom/facebook/internal/Utility;->JsonStrToMap(Ljava/lang/String;)Ljava/util/Map;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/concurrent/ConcurrentHashMap;->putAll(Ljava/util/Map;)V

    .line 216
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Ljava/util/concurrent/atomic/AtomicBoolean;->set(Z)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 217
    monitor-exit v0

    return-void

    :catchall_0
    move-exception v1

    .line 205
    monitor-exit v0

    throw v1
.end method

.method static initStore()V
    .locals 1

    .line 78
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 81
    :cond_0
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    return-void
.end method

.method private static maybeSHA256Hashed(Ljava/lang/String;)Z
    .locals 1

    const-string v0, "[A-Fa-f0-9]{64}"

    .line 322
    invoke-virtual {p0, v0}, Ljava/lang/String;->matches(Ljava/lang/String;)Z

    move-result p0

    return p0
.end method

.method private static normalizeData(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 1

    .line 293
    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    const-string v0, "em"

    .line 295
    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 296
    sget-object p0, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {p0, p1}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object p0

    invoke-virtual {p0}, Ljava/util/regex/Matcher;->matches()Z

    move-result p0

    if-eqz p0, :cond_0

    return-object p1

    .line 299
    :cond_0
    sget-object p0, Lcom/facebook/appevents/UserDataStore;->TAG:Ljava/lang/String;

    const-string p1, "Setting email failure: this is not a valid email address"

    invoke-static {p0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string p0, ""

    return-object p0

    :cond_1
    const-string v0, "ph"

    .line 304
    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    const-string p0, "[^0-9]"

    const-string v0, ""

    .line 305
    invoke-virtual {p1, p0, v0}, Ljava/lang/String;->replaceAll(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p0

    return-object p0

    :cond_2
    const-string v0, "ge"

    .line 308
    invoke-virtual {v0, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p0

    if-eqz p0, :cond_6

    .line 309
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result p0

    if-lez p0, :cond_3

    const/4 p0, 0x0

    const/4 v0, 0x1

    invoke-virtual {p1, p0, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object p0

    goto :goto_0

    :cond_3
    const-string p0, ""

    :goto_0
    const-string p1, "f"

    .line 310
    invoke-virtual {p1, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_5

    const-string p1, "m"

    invoke-virtual {p1, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_4

    goto :goto_1

    .line 313
    :cond_4
    sget-object p0, Lcom/facebook/appevents/UserDataStore;->TAG:Ljava/lang/String;

    const-string p1, "Setting gender failure: the supported value for gender is f or m"

    invoke-static {p0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string p0, ""

    return-object p0

    :cond_5
    :goto_1
    return-object p0

    :cond_6
    return-object p1
.end method

.method public static removeRules(Ljava/util/List;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 176
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    .line 177
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    .line 179
    :cond_0
    invoke-interface {p0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object p0

    :cond_1
    :goto_0
    invoke-interface {p0}, Ljava/util/Iterator;->hasNext()Z

    move-result v0

    if-eqz v0, :cond_2

    invoke-interface {p0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 180
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v1, v0}, Ljava/util/concurrent/ConcurrentHashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 181
    sget-object v1, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v1, v0}, Ljava/util/concurrent/ConcurrentHashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_2
    const-string p0, "com.facebook.appevents.UserDataStore.internalUserData"

    .line 184
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v0}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, v0}, Lcom/facebook/appevents/UserDataStore;->writeDataIntoCache(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method static setInternalUd(Ljava/util/Map;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .line 249
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->initialized:Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_0

    .line 250
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->initAndWait()V

    .line 252
    :cond_0
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_7

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 253
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 254
    invoke-interface {p0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 255
    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Lcom/facebook/appevents/UserDataStore;->normalizeData(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/facebook/internal/Utility;->sha256hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 256
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v3, v1}, Ljava/util/concurrent/ConcurrentHashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_6

    .line 257
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v3, v1}, Ljava/util/concurrent/ConcurrentHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    const/4 v4, 0x0

    if-eqz v3, :cond_1

    const-string v5, ","

    .line 260
    invoke-virtual {v3, v5}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v5

    goto :goto_1

    .line 262
    :cond_1
    new-array v5, v4, [Ljava/lang/String;

    .line 265
    :goto_1
    new-instance v6, Ljava/util/HashSet;

    invoke-static {v5}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v7

    invoke-direct {v6, v7}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    .line 266
    invoke-interface {v6, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v7

    if-eqz v7, :cond_2

    return-void

    .line 269
    :cond_2
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    .line 271
    array-length v8, v5

    if-nez v8, :cond_3

    .line 272
    invoke-virtual {v7, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_3

    .line 273
    :cond_3
    array-length v8, v5

    const/4 v9, 0x5

    if-ge v8, v9, :cond_4

    .line 274
    invoke-virtual {v7, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, ","

    invoke-virtual {v7, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_3

    :cond_4
    const/4 v3, 0x1

    :goto_2
    if-ge v3, v9, :cond_5

    .line 277
    aget-object v8, v5, v3

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v8, ","

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v3, v3, 0x1

    goto :goto_2

    .line 279
    :cond_5
    invoke-virtual {v7, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 280
    aget-object v2, v5, v4

    invoke-interface {v6, v2}, Ljava/util/Set;->remove(Ljava/lang/Object;)Z

    .line 283
    :goto_3
    sget-object v2, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v1, v3}, Ljava/util/concurrent/ConcurrentHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_0

    .line 285
    :cond_6
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v3, v1, v2}, Ljava/util/concurrent/ConcurrentHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_0

    :cond_7
    const-string p0, "com.facebook.appevents.UserDataStore.internalUserData"

    .line 289
    sget-object v0, Lcom/facebook/appevents/UserDataStore;->internalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-static {v0}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, v0}, Lcom/facebook/appevents/UserDataStore;->writeDataIntoCache(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method static setUserDataAndHash(Landroid/os/Bundle;)V
    .locals 2

    .line 99
    invoke-static {}, Lcom/facebook/appevents/InternalAppEventsLogger;->getAnalyticsExecutor()Ljava/util/concurrent/Executor;

    move-result-object v0

    new-instance v1, Lcom/facebook/appevents/UserDataStore$2;

    invoke-direct {v1, p0}, Lcom/facebook/appevents/UserDataStore$2;-><init>(Landroid/os/Bundle;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method

.method static setUserDataAndHash(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .param p0    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p2    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p4    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p5    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p6    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p7    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p8    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p9    # Ljava/lang/String;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 127
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    if-eqz p0, :cond_0

    const-string v1, "em"

    .line 129
    invoke-virtual {v0, v1, p0}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    if-eqz p1, :cond_1

    const-string p0, "fn"

    .line 132
    invoke-virtual {v0, p0, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    if-eqz p2, :cond_2

    const-string p0, "ln"

    .line 135
    invoke-virtual {v0, p0, p2}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_2
    if-eqz p3, :cond_3

    const-string p0, "ph"

    .line 138
    invoke-virtual {v0, p0, p3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_3
    if-eqz p4, :cond_4

    const-string p0, "db"

    .line 141
    invoke-virtual {v0, p0, p4}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_4
    if-eqz p5, :cond_5

    const-string p0, "ge"

    .line 144
    invoke-virtual {v0, p0, p5}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_5
    if-eqz p6, :cond_6

    const-string p0, "ct"

    .line 147
    invoke-virtual {v0, p0, p6}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_6
    if-eqz p7, :cond_7

    const-string p0, "st"

    .line 150
    invoke-virtual {v0, p0, p7}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_7
    if-eqz p8, :cond_8

    const-string p0, "zp"

    .line 153
    invoke-virtual {v0, p0, p8}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    :cond_8
    if-eqz p9, :cond_9

    const-string p0, "country"

    .line 156
    invoke-virtual {v0, p0, p9}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 158
    :cond_9
    invoke-static {v0}, Lcom/facebook/appevents/UserDataStore;->setUserDataAndHash(Landroid/os/Bundle;)V

    return-void
.end method

.method private static updateHashUserData(Landroid/os/Bundle;)V
    .locals 4

    if-nez p0, :cond_0

    return-void

    .line 231
    :cond_0
    invoke-virtual {p0}, Landroid/os/Bundle;->keySet()Ljava/util/Set;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :cond_1
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_4

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 232
    invoke-virtual {p0, v1}, Landroid/os/Bundle;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    if-nez v2, :cond_2

    goto :goto_0

    .line 236
    :cond_2
    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    .line 237
    invoke-static {v2}, Lcom/facebook/appevents/UserDataStore;->maybeSHA256Hashed(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_3

    .line 238
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v1, v2}, Ljava/util/concurrent/ConcurrentHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 240
    :cond_3
    invoke-static {v1, v2}, Lcom/facebook/appevents/UserDataStore;->normalizeData(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/facebook/internal/Utility;->sha256hash(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 242
    sget-object v3, Lcom/facebook/appevents/UserDataStore;->externalHashedUserData:Ljava/util/concurrent/ConcurrentHashMap;

    invoke-virtual {v3, v1, v2}, Ljava/util/concurrent/ConcurrentHashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_4
    return-void
.end method

.method private static writeDataIntoCache(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    .line 85
    invoke-static {}, Lcom/facebook/FacebookSdk;->getExecutor()Ljava/util/concurrent/Executor;

    move-result-object v0

    new-instance v1, Lcom/facebook/appevents/UserDataStore$1;

    invoke-direct {v1, p0, p1}, Lcom/facebook/appevents/UserDataStore$1;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    return-void
.end method
