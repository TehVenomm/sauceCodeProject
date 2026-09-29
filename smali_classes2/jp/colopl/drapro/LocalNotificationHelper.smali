.class public Ljp/colopl/drapro/LocalNotificationHelper;
.super Ljava/lang/Object;
.source "LocalNotificationHelper.java"


# static fields
.field private static final REGISTERED_NOTIFICATIONS_KEY:Ljava/lang/String; = "NOTIFICATIONS"

.field private static final REGISTERED_NOTIFICATIONS_STORE:Ljava/lang/String; = "jp.colopl.drapro.LocalNotificationHelper.NOTIFICATIONS_STORE_KEY"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static CancelAll()V
    .locals 2

    .line 63
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 64
    new-instance v1, Ljp/colopl/drapro/LocalNotificationHelper$2;

    invoke-direct {v1, v0}, Ljp/colopl/drapro/LocalNotificationHelper$2;-><init>(Landroid/app/Activity;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method public static Register(ILjava/lang/String;Ljava/lang/String;I)V
    .locals 8

    .line 31
    sget-object v6, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    const-string v0, "Unity"

    .line 32
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Register "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    const-string v2, " ; "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " ; "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, " ; "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 33
    new-instance v7, Ljp/colopl/drapro/LocalNotificationHelper$1;

    move-object v0, v7

    move v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, v6

    move v5, p3

    invoke-direct/range {v0 .. v5}, Ljp/colopl/drapro/LocalNotificationHelper$1;-><init>(ILjava/lang/String;Ljava/lang/String;Landroid/app/Activity;I)V

    invoke-virtual {v6, v7}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    return-void
.end method

.method static synthetic access$000(I)V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 24
    invoke-static {p0}, Ljp/colopl/drapro/LocalNotificationHelper;->addRegisteredNotification(I)V

    return-void
.end method

.method static synthetic access$100()[Ljava/lang/Integer;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 24
    invoke-static {}, Ljp/colopl/drapro/LocalNotificationHelper;->getRegisteredNotificatinos()[Ljava/lang/Integer;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$200(Ljava/util/List;)V
    .locals 0

    .line 24
    invoke-static {p0}, Ljp/colopl/drapro/LocalNotificationHelper;->storeRegisteredNotifications(Ljava/util/List;)V

    return-void
.end method

.method private static addRegisteredNotification(I)V
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 91
    new-instance v0, Ljava/util/LinkedList;

    invoke-static {}, Ljp/colopl/drapro/LocalNotificationHelper;->getRegisteredNotificatinos()[Ljava/lang/Integer;

    move-result-object v1

    invoke-static {v1}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v1

    invoke-direct {v0, v1}, Ljava/util/LinkedList;-><init>(Ljava/util/Collection;)V

    .line 92
    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/LinkedList;->indexOf(Ljava/lang/Object;)I

    move-result v1

    if-ltz v1, :cond_0

    return-void

    .line 96
    :cond_0
    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p0

    invoke-virtual {v0, p0}, Ljava/util/LinkedList;->add(Ljava/lang/Object;)Z

    .line 97
    invoke-static {v0}, Ljp/colopl/drapro/LocalNotificationHelper;->storeRegisteredNotifications(Ljava/util/List;)V

    return-void
.end method

.method private static getRegisteredNotificatinos()[Ljava/lang/Integer;
    .locals 5
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;
        }
    .end annotation

    .line 102
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    const-string v1, "jp.colopl.drapro.LocalNotificationHelper.NOTIFICATIONS_STORE_KEY"

    const/4 v2, 0x0

    .line 103
    invoke-virtual {v0, v1, v2}, Landroid/app/Activity;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    .line 105
    new-instance v1, Lorg/json/JSONArray;

    const-string v3, "NOTIFICATIONS"

    const-string v4, "[]"

    invoke-interface {v0, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {v1, v0}, Lorg/json/JSONArray;-><init>(Ljava/lang/String;)V

    .line 106
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v0

    new-array v0, v0, [Ljava/lang/Integer;

    .line 107
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v3

    :goto_0
    if-ge v2, v3, :cond_0

    .line 108
    invoke-virtual {v1, v2}, Lorg/json/JSONArray;->getInt(I)I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v0, v2

    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_0
    return-object v0
.end method

.method private static storeRegisteredNotifications(Ljava/util/List;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/lang/Integer;",
            ">;)V"
        }
    .end annotation

    .line 115
    new-instance v0, Lorg/json/JSONArray;

    invoke-direct {v0, p0}, Lorg/json/JSONArray;-><init>(Ljava/util/Collection;)V

    .line 117
    sget-object p0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    const-string v1, "jp.colopl.drapro.LocalNotificationHelper.NOTIFICATIONS_STORE_KEY"

    const/4 v2, 0x0

    .line 118
    invoke-virtual {p0, v1, v2}, Landroid/app/Activity;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object p0

    .line 119
    invoke-interface {p0}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object p0

    const-string v1, "NOTIFICATIONS"

    .line 120
    invoke-virtual {v0}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-interface {p0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    const-string v1, "Unity"

    .line 121
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "storeRegisteredNotifications "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Lorg/json/JSONArray;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 122
    invoke-interface {p0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    return-void
.end method
