.class public Ljp/colopl/drapro/LocalNotificationAlarmReceiver;
.super Landroid/content/BroadcastReceiver;
.source "LocalNotificationAlarmReceiver.java"


# static fields
.field public static final EXTRA_ACTIVITY:Ljava/lang/String; = "activity"

.field public static final EXTRA_BODY:Ljava/lang/String; = "body"

.field public static final EXTRA_ID:Ljava/lang/String; = "id"

.field public static final EXTRA_LARGEICON:Ljava/lang/String; = "largeIcon"

.field public static final EXTRA_SMALLICON:Ljava/lang/String; = "smallIcon"

.field public static final EXTRA_TITLE:Ljava/lang/String; = "title"

.field public static final PRIMARY_CHANNEL:Ljava/lang/String; = "default"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 19
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 13

    const-string v0, "Unity"

    const-string v1, "SuccessReceive"

    .line 31
    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 35
    invoke-virtual {p2}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    .line 36
    invoke-virtual {v0}, Landroid/os/Bundle;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    const-string p2, "id"

    .line 38
    invoke-virtual {v0, p2}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result p2

    const-string v2, "title"

    .line 39
    invoke-virtual {v0, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "body"

    .line 40
    invoke-virtual {v0, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    move-object v12, v0

    move v0, p2

    move-object p2, v12

    goto :goto_0

    :cond_0
    const-string v0, "id"

    .line 44
    invoke-virtual {p2, v0, v1}, Landroid/content/Intent;->getIntExtra(Ljava/lang/String;I)I

    move-result v0

    const-string v2, "title"

    .line 45
    invoke-virtual {p2, v2}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "body"

    .line 46
    invoke-virtual {p2, v3}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    .line 49
    :goto_0
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    const-string v4, "push_icon"

    const-string v5, "drawable"

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v3, v4, v5, v6}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v3

    .line 52
    new-instance v4, Landroid/content/Intent;

    const-class v5, Ljp/colopl/drapro/StartActivity;

    invoke-direct {v4, p1, v5}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    .line 53
    invoke-static {p1, v1, v4, v1}, Landroid/app/PendingIntent;->getActivity(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v4

    .line 56
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const-string v7, "app_icon"

    const-string v8, "mipmap"

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v6, v7, v8, v9}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v6

    .line 55
    invoke-static {v5, v6}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object v5

    .line 59
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const v7, 0x1050005

    invoke-virtual {v6, v7}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v6

    .line 61
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v7

    const v8, 0x1050006

    invoke-virtual {v7, v8}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v7

    .line 63
    invoke-virtual {v5}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v8

    const/4 v9, 0x1

    if-gt v8, v6, :cond_1

    invoke-virtual {v5}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v8

    if-le v8, v7, :cond_2

    .line 64
    :cond_1
    invoke-static {v5, v6, v7, v9}, Landroid/graphics/Bitmap;->createScaledBitmap(Landroid/graphics/Bitmap;IIZ)Landroid/graphics/Bitmap;

    move-result-object v5

    :cond_2
    const-string v6, "notification"

    .line 69
    invoke-virtual {p1, v6}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Landroid/app/NotificationManager;

    .line 73
    sget v7, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v8, 0x1a

    if-lt v7, v8, :cond_3

    .line 75
    new-instance v7, Landroid/app/NotificationChannel;

    const-string v8, "default"

    const-string v10, "Primary Channel"

    const/4 v11, 0x3

    invoke-direct {v7, v8, v10, v11}, Landroid/app/NotificationChannel;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;I)V

    .line 76
    invoke-virtual {v7, v1}, Landroid/app/NotificationChannel;->setLockscreenVisibility(I)V

    .line 77
    invoke-virtual {v6, v7}, Landroid/app/NotificationManager;->createNotificationChannel(Landroid/app/NotificationChannel;)V

    .line 79
    new-instance v1, Landroid/app/Notification$Builder;

    const-string v7, "default"

    invoke-direct {v1, p1, v7}, Landroid/app/Notification$Builder;-><init>(Landroid/content/Context;Ljava/lang/String;)V

    goto :goto_1

    .line 83
    :cond_3
    new-instance v1, Landroid/app/Notification$Builder;

    invoke-direct {v1, p1}, Landroid/app/Notification$Builder;-><init>(Landroid/content/Context;)V

    .line 86
    :goto_1
    invoke-virtual {v1, v2}, Landroid/app/Notification$Builder;->setTicker(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    .line 87
    invoke-virtual {p1, v2}, Landroid/app/Notification$Builder;->setContentTitle(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/app/Notification$Builder;->setContentText(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    .line 88
    invoke-virtual {p1, v3}, Landroid/app/Notification$Builder;->setSmallIcon(I)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-virtual {p1, v5}, Landroid/app/Notification$Builder;->setLargeIcon(Landroid/graphics/Bitmap;)Landroid/app/Notification$Builder;

    move-result-object p1

    .line 89
    invoke-virtual {p1, v4}, Landroid/app/Notification$Builder;->setContentIntent(Landroid/app/PendingIntent;)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-virtual {p1, v9}, Landroid/app/Notification$Builder;->setDefaults(I)Landroid/app/Notification$Builder;

    move-result-object p1

    .line 91
    invoke-virtual {p1, v9}, Landroid/app/Notification$Builder;->setAutoCancel(Z)Landroid/app/Notification$Builder;

    .line 92
    invoke-virtual {v1}, Landroid/app/Notification$Builder;->build()Landroid/app/Notification;

    move-result-object p1

    invoke-virtual {v6, v0, p1}, Landroid/app/NotificationManager;->notify(ILandroid/app/Notification;)V

    return-void
.end method
