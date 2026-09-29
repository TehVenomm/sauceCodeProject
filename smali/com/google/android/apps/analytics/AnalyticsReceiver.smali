.class public Lcom/google/android/apps/analytics/AnalyticsReceiver;
.super Landroid/content/BroadcastReceiver;


# static fields
.field static final INSTALL_ACTION:Ljava/lang/String; = "com.android.vending.INSTALL_REFERRER"

.field static final REFERRER_KEY:Ljava/lang/String; = "referrer"


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    const-string v0, "referrer"

    invoke-virtual {p2, v0}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    const-string v1, "com.android.vending.INSTALL_REFERRER"

    invoke-virtual {p2}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_2

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const-string p2, "GoogleAnalyticsTracker"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "referrer="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {p2, v1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p2, Lcom/google/android/apps/analytics/PersistentHitStore;

    invoke-direct {p2, p1}, Lcom/google/android/apps/analytics/PersistentHitStore;-><init>(Landroid/content/Context;)V

    invoke-interface {p2, v0}, Lcom/google/android/apps/analytics/HitStore;->setReferrer(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    const-string p1, "GoogleAnalyticsTracker"

    const-string p2, "Referrer store attemped succeeded."

    invoke-static {p1, p2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :cond_1
    const-string p1, "GoogleAnalyticsTracker"

    const-string p2, "Referrer store attempt failed."

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :cond_2
    :goto_0
    return-void
.end method
