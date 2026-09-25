.class Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;
.super Ljava/lang/Object;
.source "CrashlyticsUncaughtExceptionHandler.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->ensureOpenSessionExists()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/concurrent/Callable<",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V
    .locals 0

    .line 500
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public bridge synthetic call()Ljava/lang/Object;
    .locals 1
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 500
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->call()Ljava/lang/Void;

    move-result-object v0

    return-object v0
.end method

.method public call()Ljava/lang/Void;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 503
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-virtual {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->hasOpenSession()Z

    move-result v0

    if-nez v0, :cond_0

    .line 504
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$800(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V

    goto :goto_0

    .line 506
    :cond_0
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$400(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Lcom/crashlytics/android/core/LogFileManager;

    move-result-object v0

    iget-object v1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$12;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v1}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$600(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/crashlytics/android/core/LogFileManager;->onSessionChange(Ljava/lang/String;)V

    :goto_0
    const/4 v0, 0x0

    return-object v0
.end method
