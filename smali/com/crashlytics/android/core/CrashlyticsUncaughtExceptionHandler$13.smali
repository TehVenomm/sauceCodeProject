.class Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;
.super Ljava/lang/Object;
.source "CrashlyticsUncaughtExceptionHandler.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->finalizeSessions()Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/concurrent/Callable<",
        "Ljava/lang/Boolean;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V
    .locals 0

    .line 563
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public call()Ljava/lang/Boolean;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 566
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$300(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Ljava/util/concurrent/atomic/AtomicBoolean;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;->get()Z

    move-result v0

    if-nez v0, :cond_1

    .line 567
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$900(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Lcom/crashlytics/android/core/CrashlyticsCore;

    move-result-object v0

    invoke-virtual {v0}, Lcom/crashlytics/android/core/CrashlyticsCore;->getExternalCrashEventData()Lcom/crashlytics/android/core/internal/models/SessionEventData;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 570
    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v1, v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$1000(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;Lcom/crashlytics/android/core/internal/models/SessionEventData;)V

    .line 572
    :cond_0
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$1100(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V

    .line 573
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$800(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V

    .line 574
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "Open sessions were closed and a new session was opened."

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x1

    .line 576
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0

    .line 578
    :cond_1
    invoke-static {}, Lio/fabric/sdk/android/Fabric;->getLogger()Lio/fabric/sdk/android/Logger;

    move-result-object v0

    const-string v1, "Fabric"

    const-string v2, "Skipping session finalization because a crash has already occurred."

    invoke-interface {v0, v1, v2}, Lio/fabric/sdk/android/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x0

    .line 580
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method

.method public bridge synthetic call()Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 563
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$13;->call()Ljava/lang/Boolean;

    move-result-object v0

    return-object v0
.end method
