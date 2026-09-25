.class Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$5;
.super Landroid/content/BroadcastReceiver;
.source "CrashlyticsUncaughtExceptionHandler.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;-><init>(Ljava/lang/Thread$UncaughtExceptionHandler;Lcom/crashlytics/android/core/CrashlyticsListener;Lcom/crashlytics/android/core/CrashlyticsExecutorServiceWrapper;Lio/fabric/sdk/android/services/common/IdManager;Lcom/crashlytics/android/core/SessionDataWriter;Lcom/crashlytics/android/core/CrashlyticsCore;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;)V
    .locals 0

    .line 229
    iput-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$5;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 0

    .line 232
    iget-object p1, p0, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler$5;->this$0:Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;

    const/4 p2, 0x1

    invoke-static {p1, p2}, Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;->access$102(Lcom/crashlytics/android/core/CrashlyticsUncaughtExceptionHandler;Z)Z

    return-void
.end method
