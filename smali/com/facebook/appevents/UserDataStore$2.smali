.class final Lcom/facebook/appevents/UserDataStore$2;
.super Ljava/lang/Object;
.source "UserDataStore.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/facebook/appevents/UserDataStore;->setUserDataAndHash(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$ud:Landroid/os/Bundle;


# direct methods
.method constructor <init>(Landroid/os/Bundle;)V
    .locals 0

    .line 99
    iput-object p1, p0, Lcom/facebook/appevents/UserDataStore$2;->val$ud:Landroid/os/Bundle;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    invoke-static {p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->isObjectCrashing(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    return-void

    .line 102
    :cond_0
    :try_start_0
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->access$000()Ljava/util/concurrent/atomic/AtomicBoolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_1

    .line 103
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->access$300()Ljava/lang/String;

    move-result-object v0

    const-string v1, "initStore should have been called before calling setUserData"

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 104
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->access$100()V

    .line 107
    :cond_1
    iget-object v0, p0, Lcom/facebook/appevents/UserDataStore$2;->val$ud:Landroid/os/Bundle;

    invoke-static {v0}, Lcom/facebook/appevents/UserDataStore;->access$400(Landroid/os/Bundle;)V

    const-string v0, "com.facebook.appevents.UserDataStore.userData"

    .line 109
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->access$500()Ljava/util/concurrent/ConcurrentHashMap;

    move-result-object v1

    invoke-static {v1}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/facebook/appevents/UserDataStore;->access$600(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "com.facebook.appevents.UserDataStore.internalUserData"

    .line 111
    invoke-static {}, Lcom/facebook/appevents/UserDataStore;->access$700()Ljava/util/concurrent/ConcurrentHashMap;

    move-result-object v1

    invoke-static {v1}, Lcom/facebook/internal/Utility;->mapToJsonStr(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v1

    .line 110
    invoke-static {v0, v1}, Lcom/facebook/appevents/UserDataStore;->access$600(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Throwable; {:try_start_0 .. :try_end_0} :catch_0

    return-void

    :catch_0
    move-exception v0

    .line 112
    invoke-static {v0, p0}, Lcom/facebook/internal/instrument/crashshield/CrashShieldHandler;->handleThrowable(Ljava/lang/Throwable;Ljava/lang/Object;)V

    return-void
.end method
