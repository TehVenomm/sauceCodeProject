.class final Ljp/colopl/drapro/LocalNotificationHelper$2;
.super Ljava/lang/Object;
.source "LocalNotificationHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/LocalNotificationHelper;->CancelAll()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$a:Landroid/app/Activity;


# direct methods
.method constructor <init>(Landroid/app/Activity;)V
    .locals 0

    .line 64
    iput-object p1, p0, Ljp/colopl/drapro/LocalNotificationHelper$2;->val$a:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 8

    .line 67
    :try_start_0
    invoke-static {}, Ljp/colopl/drapro/LocalNotificationHelper;->access$100()[Ljava/lang/Integer;

    move-result-object v0

    .line 69
    array-length v1, v0

    const/4 v2, 0x0

    const/4 v3, 0x0

    :goto_0
    if-ge v3, v1, :cond_1

    .line 70
    aget-object v4, v0, v3

    invoke-virtual {v4}, Ljava/lang/Integer;->intValue()I

    move-result v4

    .line 71
    new-instance v5, Landroid/content/Intent;

    iget-object v6, p0, Ljp/colopl/drapro/LocalNotificationHelper$2;->val$a:Landroid/app/Activity;

    const-class v7, Ljp/colopl/drapro/LocalNotificationAlarmReceiver;

    invoke-direct {v5, v6, v7}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 72
    iget-object v6, p0, Ljp/colopl/drapro/LocalNotificationHelper$2;->val$a:Landroid/app/Activity;

    const/high16 v7, 0x20000000

    invoke-static {v6, v4, v5, v7}, Landroid/app/PendingIntent;->getBroadcast(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v4

    if-eqz v4, :cond_0

    .line 75
    iget-object v5, p0, Ljp/colopl/drapro/LocalNotificationHelper$2;->val$a:Landroid/app/Activity;

    const-string v6, "alarm"

    invoke-virtual {v5, v6}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Landroid/app/AlarmManager;

    .line 76
    invoke-virtual {v5, v4}, Landroid/app/AlarmManager;->cancel(Landroid/app/PendingIntent;)V

    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 80
    :cond_1
    new-array v0, v2, [Ljava/lang/Integer;

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    invoke-static {v0}, Ljp/colopl/drapro/LocalNotificationHelper;->access$200(Ljava/util/List;)V

    const-string v0, "Unity"

    const-string v1, "SuccessCancelAll"

    .line 81
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    .line 83
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    :goto_1
    return-void
.end method
