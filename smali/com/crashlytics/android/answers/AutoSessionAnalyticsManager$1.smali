.class Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;
.super Ljava/lang/Object;
.source "AutoSessionAnalyticsManager.java"

# interfaces
.implements Landroid/app/Application$ActivityLifecycleCallbacks;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;


# direct methods
.method constructor <init>(Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;)V
    .locals 0

    .line 25
    iput-object p1, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onActivityCreated(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 28
    iget-object p2, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {p2, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onCreate(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivityDestroyed(Landroid/app/Activity;)V
    .locals 1

    .line 33
    iget-object v0, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onDestroy(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivityPaused(Landroid/app/Activity;)V
    .locals 1

    .line 38
    iget-object v0, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onPause(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivityResumed(Landroid/app/Activity;)V
    .locals 1

    .line 43
    iget-object v0, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onResume(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivitySaveInstanceState(Landroid/app/Activity;Landroid/os/Bundle;)V
    .locals 0

    .line 48
    iget-object p2, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {p2, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onSaveInstanceState(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivityStarted(Landroid/app/Activity;)V
    .locals 1

    .line 53
    iget-object v0, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onStart(Landroid/app/Activity;)V

    return-void
.end method

.method public onActivityStopped(Landroid/app/Activity;)V
    .locals 1

    .line 58
    iget-object v0, p0, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager$1;->this$0:Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;

    invoke-virtual {v0, p1}, Lcom/crashlytics/android/answers/AutoSessionAnalyticsManager;->onStop(Landroid/app/Activity;)V

    return-void
.end method
