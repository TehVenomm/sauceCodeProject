.class final Ljp/colopl/drapro/LocalNotificationHelper$1;
.super Ljava/lang/Object;
.source "LocalNotificationHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/LocalNotificationHelper;->Register(ILjava/lang/String;Ljava/lang/String;I)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$a:Landroid/app/Activity;

.field final synthetic val$afterSeconds:I

.field final synthetic val$body:Ljava/lang/String;

.field final synthetic val$id:I

.field final synthetic val$title:Ljava/lang/String;


# direct methods
.method constructor <init>(ILjava/lang/String;Ljava/lang/String;Landroid/app/Activity;I)V
    .locals 0

    .line 33
    iput p1, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$id:I

    iput-object p2, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$title:Ljava/lang/String;

    iput-object p3, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$body:Ljava/lang/String;

    iput-object p4, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$a:Landroid/app/Activity;

    iput p5, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$afterSeconds:I

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 5

    .line 35
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.media.action.DISPLAY_NOTIFICATION"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v1, "android.intent.category.DEFAULT"

    .line 36
    invoke-virtual {v0, v1}, Landroid/content/Intent;->addCategory(Ljava/lang/String;)Landroid/content/Intent;

    .line 37
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "id"

    .line 38
    iget v3, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$id:I

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "title"

    .line 39
    iget-object v3, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$title:Ljava/lang/String;

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v2, "body"

    .line 40
    iget-object v3, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$body:Ljava/lang/String;

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 41
    invoke-virtual {v0, v1}, Landroid/content/Intent;->putExtras(Landroid/os/Bundle;)Landroid/content/Intent;

    .line 42
    iget-object v1, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$a:Landroid/app/Activity;

    const-class v2, Ljp/colopl/drapro/LocalNotificationAlarmReceiver;

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->setClass(Landroid/content/Context;Ljava/lang/Class;)Landroid/content/Intent;

    .line 44
    iget-object v1, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$a:Landroid/app/Activity;

    iget v2, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$id:I

    const/high16 v3, 0x8000000

    invoke-static {v1, v2, v0, v3}, Landroid/app/PendingIntent;->getBroadcast(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v0

    .line 45
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v1

    .line 46
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v2

    invoke-virtual {v1, v2, v3}, Ljava/util/Calendar;->setTimeInMillis(J)V

    .line 47
    iget v2, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$afterSeconds:I

    const/16 v3, 0xd

    invoke-virtual {v1, v3, v2}, Ljava/util/Calendar;->add(II)V

    .line 48
    iget-object v2, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$a:Landroid/app/Activity;

    const-string v3, "alarm"

    invoke-virtual {v2, v3}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/app/AlarmManager;

    .line 49
    invoke-virtual {v1}, Ljava/util/Calendar;->getTimeInMillis()J

    move-result-wide v3

    const/4 v1, 0x0

    invoke-virtual {v2, v1, v3, v4, v0}, Landroid/app/AlarmManager;->set(IJLandroid/app/PendingIntent;)V

    const-string v0, "Unity"

    const-string v1, "SuccessSet"

    .line 50
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 52
    :try_start_0
    iget v0, p0, Ljp/colopl/drapro/LocalNotificationHelper$1;->val$id:I

    invoke-static {v0}, Ljp/colopl/drapro/LocalNotificationHelper;->access$000(I)V

    const-string v0, "Unity"

    const-string v1, "SuccessRegister"

    .line 53
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    .line 55
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V

    :goto_0
    return-void
.end method
