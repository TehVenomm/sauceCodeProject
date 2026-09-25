.class Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;
.super Ljava/lang/Object;
.source "CrashlyticsUncaughtExceptionHandler.java"

# interfaces
.implements Ljava/util/concurrent/Callable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->cacheKeyData(Ljava/util/Map;)V
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

.field final synthetic val$keyData:Ljava/util/Map;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;Ljava/util/Map;)V
    .locals 0

    .line 481
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    iput-object p2, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->val$keyData:Ljava/util/Map;

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

    .line 481
    invoke-virtual {p0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->call()Ljava/lang/Void;

    move-result-object v0

    return-object v0
.end method

.method public call()Ljava/lang/Void;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/Exception;
        }
    .end annotation

    .line 484
    iget-object v0, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v0}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$600(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Ljava/lang/String;

    move-result-object v0

    .line 485
    new-instance v1, Lcom/crashlytics/android/core/MetaDataStore;

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-static {v2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$700(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)Ljava/io/File;

    move-result-object v2

    invoke-direct {v1, v2}, Lcom/crashlytics/android/core/MetaDataStore;-><init>(Ljava/io/File;)V

    iget-object v2, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$11;->val$keyData:Ljava/util/Map;

    invoke-virtual {v1, v0, v2}, Lcom/crashlytics/android/core/MetaDataStore;->writeKeyData(Ljava/lang/String;Ljava/util/Map;)V

    const/4 v0, 0x0

    return-object v0
.end method
