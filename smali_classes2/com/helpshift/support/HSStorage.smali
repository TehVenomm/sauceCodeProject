.class public final Lcom/helpshift/support/HSStorage;
.super Ljava/lang/Object;
.source "HSStorage.java"


# static fields
.field static final TAG:Ljava/lang/String; = "HelpShiftDebug"

.field private static cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;


# instance fields
.field private context:Landroid/content/Context;

.field private final dbFile:Ljava/lang/String;

.field private storage:Landroid/content/SharedPreferences;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 2

    .line 33
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "fullIndex.db"

    .line 26
    iput-object v0, p0, Lcom/helpshift/support/HSStorage;->dbFile:Ljava/lang/String;

    .line 34
    iput-object p1, p0, Lcom/helpshift/support/HSStorage;->context:Landroid/content/Context;

    const-string v0, "HSJsonData"

    const/4 v1, 0x0

    .line 35
    invoke-virtual {p1, v0, v1}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    return-void
.end method

.method private storageGet(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 47
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const-string v1, ""

    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method private storageGetArr(Ljava/lang/String;)Lorg/json/JSONArray;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 43
    new-instance v0, Lorg/json/JSONArray;

    iget-object v1, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const-string v2, "[]"

    invoke-interface {v1, p1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method private storageGetInt(Ljava/lang/String;)Ljava/lang/Integer;
    .locals 1

    const/4 v0, 0x0

    .line 51
    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/HSStorage;->storageGetInt(Ljava/lang/String;I)Ljava/lang/Integer;

    move-result-object p1

    return-object p1
.end method

.method private storageGetInt(Ljava/lang/String;I)Ljava/lang/Integer;
    .locals 1

    .line 55
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result p1

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    return-object p1
.end method

.method private storageGetLong(Ljava/lang/String;)Ljava/lang/Long;
    .locals 3

    .line 71
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const-wide/16 v1, 0x0

    invoke-interface {v0, p1, v1, v2}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v0

    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    return-object p1
.end method

.method private storageGetObj(Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 39
    new-instance v0, Lorg/json/JSONObject;

    iget-object v1, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const-string v2, "{}"

    invoke-interface {v1, p1, v2}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    invoke-direct {v0, p1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    return-object v0
.end method

.method private storageSet(Ljava/lang/String;Ljava/lang/Boolean;)V
    .locals 1

    .line 99
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 100
    invoke-virtual {p2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p2

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 101
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method private storageSet(Ljava/lang/String;Ljava/lang/Integer;)V
    .locals 1

    .line 93
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 94
    invoke-virtual {p2}, Ljava/lang/Integer;->intValue()I

    move-result p2

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 95
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method private storageSet(Ljava/lang/String;Ljava/lang/Long;)V
    .locals 3

    .line 105
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 106
    invoke-virtual {p2}, Ljava/lang/Long;->longValue()J

    move-result-wide v1

    invoke-interface {v0, p1, v1, v2}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 107
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method private storageSet(Ljava/lang/String;Ljava/lang/String;)V
    .locals 1

    .line 87
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 88
    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 89
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method private storageSet(Ljava/lang/String;Lorg/json/JSONArray;)V
    .locals 1

    .line 75
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 76
    invoke-virtual {p2}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 77
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method private storageSet(Ljava/lang/String;Lorg/json/JSONObject;)V
    .locals 1

    .line 81
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 82
    invoke-virtual {p2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-interface {v0, p1, p2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 83
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method


# virtual methods
.method protected clearDatabase()V
    .locals 1

    .line 111
    invoke-static {}, Lcom/helpshift/support/storage/FaqsDataSource;->getInstance()Lcom/helpshift/support/storage/FaqsDataSource;

    move-result-object v0

    .line 112
    invoke-virtual {v0}, Lcom/helpshift/support/storage/FaqsDataSource;->clearDB()V

    .line 113
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 114
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->clear()Landroid/content/SharedPreferences$Editor;

    .line 115
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->apply()V

    return-void
.end method

.method protected clearLegacySearchIndexFile()V
    .locals 2

    .line 120
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->context:Landroid/content/Context;

    const-string v1, "tfidf.db"

    invoke-virtual {v0, v1}, Landroid/content/Context;->deleteFile(Ljava/lang/String;)Z

    return-void
.end method

.method public contains(Ljava/lang/String;)Z
    .locals 1

    .line 63
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    invoke-interface {v0, p1}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result p1

    return p1
.end method

.method protected deleteIndex()V
    .locals 2

    const/4 v0, 0x0

    .line 246
    sput-object v0, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;

    .line 247
    invoke-static {}, Lcom/helpshift/support/search/storage/SearchTokenDaoImpl;->getInstance()Lcom/helpshift/support/search/SearchTokenDao;

    move-result-object v0

    .line 248
    invoke-interface {v0}, Lcom/helpshift/support/search/SearchTokenDao;->clear()V

    .line 249
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->context:Landroid/content/Context;

    const-string v1, "fullIndex.db"

    invoke-virtual {v0, v1}, Landroid/content/Context;->deleteFile(Ljava/lang/String;)Z

    .line 250
    invoke-virtual {p0}, Lcom/helpshift/support/HSStorage;->unsetDBFlag()V

    return-void
.end method

.method protected getApiKey()Ljava/lang/String;
    .locals 1

    const-string v0, "apiKey"

    .line 124
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGet(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getAppId()Ljava/lang/String;
    .locals 1

    const-string v0, "appId"

    .line 140
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGet(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getApplicationVersion()Ljava/lang/String;
    .locals 1

    const-string v0, "applicationVersion"

    .line 156
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGet(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getDBFlag()Ljava/lang/Boolean;
    .locals 1

    const-string v0, "dbFlag"

    .line 234
    invoke-virtual {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetBoolean(Ljava/lang/String;)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method

.method protected getDomain()Ljava/lang/String;
    .locals 1

    const-string v0, "domain"

    .line 132
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGet(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getFailedApiCalls()Lorg/json/JSONObject;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "failedApiCalls"

    .line 254
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetObj(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    return-object v0
.end method

.method getLastErrorReportedTime()J
    .locals 2

    const-string v0, "lastErrorReportedTime"

    .line 265
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetLong(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    return-wide v0
.end method

.method protected getLaunchReviewCounter()I
    .locals 1

    const-string v0, "launchReviewCounter"

    .line 172
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetInt(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    return v0
.end method

.method protected getLibraryVersion()Ljava/lang/String;
    .locals 1

    const-string v0, "libraryVersion"

    .line 148
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGet(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected getReviewCounter()I
    .locals 1

    const-string v0, "reviewCounter"

    .line 164
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetInt(Ljava/lang/String;)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    return v0
.end method

.method protected getStoredFiles()Lorg/json/JSONArray;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    const-string v0, "cachedImages"

    .line 180
    invoke-direct {p0, v0}, Lcom/helpshift/support/HSStorage;->storageGetArr(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    return-object v0
.end method

.method public getString(Ljava/lang/String;)Ljava/lang/String;
    .locals 2

    .line 277
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const-string v1, ""

    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    return-object p1
.end method

.method isCacheSearchIndexNull()Z
    .locals 1

    .line 273
    sget-object v0, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method protected loadIndex()V
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;,
            Ljava/lang/ClassCastException;,
            Ljava/lang/ClassNotFoundException;
        }
    .end annotation

    .line 210
    sget-object v0, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;

    if-eqz v0, :cond_0

    return-void

    :cond_0
    const/4 v0, 0x0

    .line 218
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/support/HSStorage;->context:Landroid/content/Context;

    const-string v2, "fullIndex.db"

    invoke-virtual {v1, v2}, Landroid/content/Context;->openFileInput(Ljava/lang/String;)Ljava/io/FileInputStream;

    move-result-object v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 219
    :try_start_1
    new-instance v2, Ljava/io/ObjectInputStream;

    invoke-direct {v2, v1}, Ljava/io/ObjectInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 220
    :try_start_2
    invoke-virtual {v2}, Ljava/io/ObjectInputStream;->readObject()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/model/FaqSearchIndex;

    sput-object v0, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 223
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 224
    invoke-static {v2}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-void

    :catchall_0
    move-exception v0

    goto :goto_0

    :catchall_1
    move-exception v2

    move-object v3, v2

    move-object v2, v0

    move-object v0, v3

    goto :goto_0

    :catchall_2
    move-exception v1

    move-object v2, v0

    move-object v0, v1

    move-object v1, v2

    .line 223
    :goto_0
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 224
    invoke-static {v2}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 225
    throw v0
.end method

.method protected readIndex()Lcom/helpshift/support/model/FaqSearchIndex;
    .locals 1

    .line 230
    sget-object v0, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;

    return-object v0
.end method

.method protected setApiKey(Ljava/lang/String;)V
    .locals 1

    const-string v0, "apiKey"

    .line 128
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method protected setAppId(Ljava/lang/String;)V
    .locals 1

    const-string v0, "appId"

    .line 144
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method protected setApplicationVersion(Ljava/lang/String;)V
    .locals 1

    const-string v0, "applicationVersion"

    .line 160
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method protected setDBFlag()V
    .locals 2

    const-string v0, "dbFlag"

    const/4 v1, 0x1

    .line 238
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/Boolean;)V

    return-void
.end method

.method protected setDomain(Ljava/lang/String;)V
    .locals 1

    const-string v0, "domain"

    .line 136
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method setLastErrorReportedTime(J)V
    .locals 1

    const-string v0, "lastErrorReportedTime"

    .line 269
    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object p1

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/Long;)V

    return-void
.end method

.method protected setLaunchReviewCounter(I)V
    .locals 1

    const-string v0, "launchReviewCounter"

    .line 176
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/Integer;)V

    return-void
.end method

.method protected setLibraryVersion(Ljava/lang/String;)V
    .locals 1

    const-string v0, "libraryVersion"

    .line 152
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method protected setReviewCounter(I)V
    .locals 1

    const-string v0, "reviewCounter"

    .line 168
    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/Integer;)V

    return-void
.end method

.method protected setStoredFiles(Lorg/json/JSONArray;)V
    .locals 1

    const-string v0, "cachedImages"

    .line 184
    invoke-direct {p0, v0, p1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Lorg/json/JSONArray;)V

    return-void
.end method

.method public storageGetBoolean(Ljava/lang/String;)Ljava/lang/Boolean;
    .locals 2

    .line 67
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const/4 v1, 0x0

    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object p1

    return-object p1
.end method

.method public storageGetFloat(Ljava/lang/String;)Ljava/lang/Float;
    .locals 2

    .line 59
    iget-object v0, p0, Lcom/helpshift/support/HSStorage;->storage:Landroid/content/SharedPreferences;

    const/4 v1, 0x0

    invoke-interface {v0, p1, v1}, Landroid/content/SharedPreferences;->getFloat(Ljava/lang/String;F)F

    move-result p1

    invoke-static {p1}, Ljava/lang/Float;->valueOf(F)Ljava/lang/Float;

    move-result-object p1

    return-object p1
.end method

.method protected storeFailedApiCall(Ljava/lang/String;Lorg/json/JSONObject;)V
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 259
    invoke-virtual {p0}, Lcom/helpshift/support/HSStorage;->getFailedApiCalls()Lorg/json/JSONObject;

    move-result-object v0

    .line 260
    invoke-virtual {v0, p1, p2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    const-string p1, "failedApiCalls"

    .line 261
    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Lorg/json/JSONObject;)V

    return-void
.end method

.method protected storeIndex(Lcom/helpshift/support/model/FaqSearchIndex;)V
    .locals 4

    .line 188
    sput-object p1, Lcom/helpshift/support/HSStorage;->cachedSearchIndex:Lcom/helpshift/support/model/FaqSearchIndex;

    const/4 v0, 0x0

    .line 192
    :try_start_0
    iget-object v1, p0, Lcom/helpshift/support/HSStorage;->context:Landroid/content/Context;

    const-string v2, "fullIndex.db"

    const/4 v3, 0x0

    invoke-virtual {v1, v2, v3}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object v1
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2
    .catchall {:try_start_0 .. :try_end_0} :catchall_2

    .line 193
    :try_start_1
    new-instance v2, Ljava/io/ObjectOutputStream;

    invoke-direct {v2, v1}, Ljava/io/ObjectOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 195
    :try_start_2
    invoke-virtual {v2, p1}, Ljava/io/ObjectOutputStream;->writeObject(Ljava/lang/Object;)V

    .line 196
    invoke-virtual {v2}, Ljava/io/ObjectOutputStream;->flush()V

    .line 197
    invoke-virtual {p0}, Lcom/helpshift/support/HSStorage;->setDBFlag()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_0
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 203
    invoke-static {v1}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    goto :goto_3

    :catchall_0
    move-exception p1

    goto :goto_0

    :catch_0
    move-exception p1

    goto :goto_1

    :catchall_1
    move-exception p1

    move-object v2, v0

    :goto_0
    move-object v0, v1

    goto :goto_4

    :catch_1
    move-exception p1

    move-object v2, v0

    :goto_1
    move-object v0, v1

    goto :goto_2

    :catchall_2
    move-exception p1

    move-object v2, v0

    goto :goto_4

    :catch_2
    move-exception p1

    move-object v2, v0

    :goto_2
    :try_start_3
    const-string v1, "HelpShiftDebug"

    const-string v3, "store index"

    .line 200
    invoke-static {v1, v3, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_3

    .line 203
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 204
    :goto_3
    invoke-static {v2}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    return-void

    :catchall_3
    move-exception p1

    .line 203
    :goto_4
    invoke-static {v0}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 204
    invoke-static {v2}, Lcom/helpshift/util/IOUtils;->closeQuitely(Ljava/io/Closeable;)V

    .line 205
    throw p1
.end method

.method protected unsetDBFlag()V
    .locals 2

    const-string v0, "dbFlag"

    const/4 v1, 0x0

    .line 242
    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/helpshift/support/HSStorage;->storageSet(Ljava/lang/String;Ljava/lang/Boolean;)V

    return-void
.end method
