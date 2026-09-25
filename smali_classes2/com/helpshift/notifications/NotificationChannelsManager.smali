.class public Lcom/helpshift/notifications/NotificationChannelsManager;
.super Ljava/lang/Object;
.source "NotificationChannelsManager.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;
    }
.end annotation


# static fields
.field private static final DEFAULT_CHANNEL_ID:Ljava/lang/String; = "helpshift_default_channel_id"


# instance fields
.field private final context:Landroid/content/Context;


# direct methods
.method public constructor <init>(Landroid/content/Context;)V
    .locals 0

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 30
    iput-object p1, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    return-void
.end method

.method private deleteDefaultNotificationChannel()V
    .locals 3
    .annotation build Landroid/annotation/TargetApi;
        value = 0x1a
    .end annotation

    .line 106
    iget-object v0, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-static {v0}, Lcom/helpshift/util/ApplicationUtil;->getNotificationManager(Landroid/content/Context;)Landroid/app/NotificationManager;

    move-result-object v0

    const-string v1, "helpshift_default_channel_id"

    if-eqz v0, :cond_0

    .line 109
    invoke-virtual {v0, v1}, Landroid/app/NotificationManager;->getNotificationChannel(Ljava/lang/String;)Landroid/app/NotificationChannel;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 110
    invoke-virtual {v0, v1}, Landroid/app/NotificationManager;->deleteNotificationChannel(Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method private ensureDefaultNotificationChannelCreated()V
    .locals 6
    .annotation build Landroid/annotation/TargetApi;
        value = 0x1a
    .end annotation

    const-string v0, "helpshift_default_channel_id"

    .line 60
    iget-object v1, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-static {v1}, Lcom/helpshift/util/ApplicationUtil;->getNotificationManager(Landroid/content/Context;)Landroid/app/NotificationManager;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 62
    invoke-virtual {v1, v0}, Landroid/app/NotificationManager;->getNotificationChannel(Ljava/lang/String;)Landroid/app/NotificationChannel;

    move-result-object v2

    if-nez v2, :cond_1

    .line 65
    iget-object v2, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/helpshift/R$string;->hs__default_notification_channel_name:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 66
    iget-object v3, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-virtual {v3}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    sget v4, Lcom/helpshift/R$string;->hs__default_notification_channel_desc:I

    invoke-virtual {v3, v4}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    .line 68
    new-instance v4, Landroid/app/NotificationChannel;

    const/4 v5, 0x3

    invoke-direct {v4, v0, v2, v5}, Landroid/app/NotificationChannel;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;I)V

    .line 69
    invoke-virtual {v4, v3}, Landroid/app/NotificationChannel;->setDescription(Ljava/lang/String;)V

    .line 71
    invoke-static {}, Lcom/helpshift/util/AssetsUtil;->getNotificationSoundUri()Landroid/net/Uri;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 73
    new-instance v2, Landroid/media/AudioAttributes$Builder;

    invoke-direct {v2}, Landroid/media/AudioAttributes$Builder;-><init>()V

    invoke-virtual {v2}, Landroid/media/AudioAttributes$Builder;->build()Landroid/media/AudioAttributes;

    move-result-object v2

    invoke-virtual {v4, v0, v2}, Landroid/app/NotificationChannel;->setSound(Landroid/net/Uri;Landroid/media/AudioAttributes;)V

    .line 75
    :cond_0
    invoke-virtual {v1, v4}, Landroid/app/NotificationManager;->createNotificationChannel(Landroid/app/NotificationChannel;)V

    :cond_1
    return-void
.end method

.method private getActiveChannelId(Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;)Ljava/lang/String;
    .locals 1

    .line 35
    sget-object v0, Lcom/helpshift/notifications/NotificationChannelsManager$1;->$SwitchMap$com$helpshift$notifications$NotificationChannelsManager$NotificationChannelType:[I

    invoke-virtual {p1}, Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/4 v0, 0x1

    if-ne p1, v0, :cond_0

    .line 37
    invoke-direct {p0}, Lcom/helpshift/notifications/NotificationChannelsManager;->getActiveSupportNotificationChannel()Ljava/lang/String;

    move-result-object p1

    return-object p1

    .line 40
    :cond_0
    new-instance p1, Ljava/lang/IllegalStateException;

    invoke-direct {p1}, Ljava/lang/IllegalStateException;-><init>()V

    throw p1
.end method

.method private getActiveSupportNotificationChannel()Ljava/lang/String;
    .locals 2

    .line 46
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getCoreApi()Lcom/helpshift/CoreApi;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/CoreApi;->getSDKConfigurationDM()Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;

    move-result-object v0

    const-string v1, "supportNotificationChannelId"

    invoke-virtual {v0, v1}, Lcom/helpshift/configuration/domainmodel/SDKConfigurationDM;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 47
    invoke-static {v0}, Lcom/helpshift/util/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 48
    invoke-direct {p0}, Lcom/helpshift/notifications/NotificationChannelsManager;->ensureDefaultNotificationChannelCreated()V

    const-string v0, "helpshift_default_channel_id"

    goto :goto_0

    .line 52
    :cond_0
    invoke-direct {p0}, Lcom/helpshift/notifications/NotificationChannelsManager;->deleteDefaultNotificationChannel()V

    :goto_0
    return-object v0
.end method


# virtual methods
.method public attachChannelId(Landroid/app/Notification;Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;)Landroid/app/Notification;
    .locals 2

    .line 116
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x1a

    if-lt v0, v1, :cond_0

    iget-object v0, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    .line 117
    invoke-static {v0}, Lcom/helpshift/util/ApplicationUtil;->getTargetSDKVersion(Landroid/content/Context;)I

    move-result v0

    if-lt v0, v1, :cond_0

    .line 120
    iget-object v0, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-static {v0, p1}, Landroid/app/Notification$Builder;->recoverBuilder(Landroid/content/Context;Landroid/app/Notification;)Landroid/app/Notification$Builder;

    move-result-object p1

    .line 121
    invoke-direct {p0, p2}, Lcom/helpshift/notifications/NotificationChannelsManager;->getActiveChannelId(Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/app/Notification$Builder;->setChannelId(Ljava/lang/String;)Landroid/app/Notification$Builder;

    .line 122
    invoke-virtual {p1}, Landroid/app/Notification$Builder;->build()Landroid/app/Notification;

    move-result-object p1

    return-object p1

    :cond_0
    return-object p1
.end method

.method public checkAndUpdateDefaultChannelInfo()V
    .locals 8

    .line 81
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x1a

    if-lt v0, v1, :cond_1

    iget-object v0, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    .line 82
    invoke-static {v0}, Lcom/helpshift/util/ApplicationUtil;->getTargetSDKVersion(Landroid/content/Context;)I

    move-result v0

    if-lt v0, v1, :cond_1

    const-string v0, "helpshift_default_channel_id"

    .line 84
    iget-object v1, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-static {v1}, Lcom/helpshift/util/ApplicationUtil;->getNotificationManager(Landroid/content/Context;)Landroid/app/NotificationManager;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 86
    invoke-virtual {v1, v0}, Landroid/app/NotificationManager;->getNotificationChannel(Ljava/lang/String;)Landroid/app/NotificationChannel;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 88
    invoke-virtual {v2}, Landroid/app/NotificationChannel;->getName()Ljava/lang/CharSequence;

    move-result-object v3

    .line 89
    invoke-virtual {v2}, Landroid/app/NotificationChannel;->getDescription()Ljava/lang/String;

    move-result-object v4

    .line 90
    iget-object v5, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    sget v6, Lcom/helpshift/R$string;->hs__default_notification_channel_name:I

    invoke-virtual {v5, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v5

    .line 91
    iget-object v6, p0, Lcom/helpshift/notifications/NotificationChannelsManager;->context:Landroid/content/Context;

    invoke-virtual {v6}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    sget v7, Lcom/helpshift/R$string;->hs__default_notification_channel_desc:I

    invoke-virtual {v6, v7}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v6

    .line 92
    invoke-virtual {v5, v3}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 93
    invoke-virtual {v6, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_1

    .line 94
    :cond_0
    new-instance v3, Landroid/app/NotificationChannel;

    invoke-virtual {v2}, Landroid/app/NotificationChannel;->getImportance()I

    move-result v2

    invoke-direct {v3, v0, v5, v2}, Landroid/app/NotificationChannel;-><init>(Ljava/lang/String;Ljava/lang/CharSequence;I)V

    .line 95
    invoke-virtual {v3, v6}, Landroid/app/NotificationChannel;->setDescription(Ljava/lang/String;)V

    .line 97
    invoke-virtual {v1, v3}, Landroid/app/NotificationManager;->createNotificationChannel(Landroid/app/NotificationChannel;)V

    :cond_1
    return-void
.end method
