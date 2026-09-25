.class public Lcom/helpshift/model/AppInfoModel;
.super Ljava/lang/Object;
.source "AppInfoModel.java"


# static fields
.field public static final HELPSHIFT_BRANDING_DISABLE_INSTALL:Ljava/lang/String; = "disableHelpshiftBranding"

.field public static final SCREEN_ORIENTATION_KEY:Ljava/lang/String; = "screenOrientation"


# instance fields
.field public apiKey:Ljava/lang/String;

.field public disableAnimations:Ljava/lang/Boolean;

.field public disableHelpshiftBranding:Ljava/lang/Boolean;

.field public domainName:Ljava/lang/String;

.field public enableInboxPolling:Ljava/lang/Boolean;

.field private fontPath:Ljava/lang/String;

.field public largeNotificationIconId:Ljava/lang/Integer;

.field public muteNotifications:Ljava/lang/Boolean;

.field public notificationIconId:Ljava/lang/Integer;

.field public notificationSoundId:Ljava/lang/Integer;

.field public platformId:Ljava/lang/String;

.field public screenOrientation:Ljava/lang/Integer;

.field private storage:Lcom/helpshift/storage/KeyValueStorage;


# direct methods
.method protected constructor <init>(Lcom/helpshift/storage/KeyValueStorage;)V
    .locals 2

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    .line 39
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "apiKey"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->apiKey:Ljava/lang/String;

    .line 40
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "domainName"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    .line 41
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    const/4 v0, 0x0

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/util/SchemaUtil;->validateDomainName(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    .line 42
    iput-object v0, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    .line 44
    :cond_0
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "platformId"

    invoke-interface {p1, v1}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    .line 45
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/util/SchemaUtil;->validatePlatformId(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    .line 46
    iput-object v0, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    .line 48
    :cond_1
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "font"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->fontPath:Ljava/lang/String;

    .line 49
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "notificationSound"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->notificationSoundId:Ljava/lang/Integer;

    .line 50
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "notificationIcon"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->notificationIconId:Ljava/lang/Integer;

    .line 51
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "largeNotificationIcon"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->largeNotificationIconId:Ljava/lang/Integer;

    .line 52
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "disableHelpshiftBranding"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->disableHelpshiftBranding:Ljava/lang/Boolean;

    .line 53
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "enableInboxPolling"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->enableInboxPolling:Ljava/lang/Boolean;

    .line 54
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "muteNotifications"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->muteNotifications:Ljava/lang/Boolean;

    .line 55
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "disableAnimations"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Boolean;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->disableAnimations:Ljava/lang/Boolean;

    .line 56
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "screenOrientation"

    invoke-interface {p1, v0}, Lcom/helpshift/storage/KeyValueStorage;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/Integer;

    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->screenOrientation:Ljava/lang/Integer;

    return-void
.end method


# virtual methods
.method public getFontPath()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 61
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->fontPath:Ljava/lang/String;

    return-object v0
.end method

.method public install(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 120
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->apiKey:Ljava/lang/String;

    .line 121
    iput-object p2, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    .line 122
    iput-object p3, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    .line 124
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    const/4 p2, 0x0

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/util/SchemaUtil;->validateDomainName(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    .line 125
    iput-object p2, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    .line 127
    :cond_0
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    invoke-static {p1}, Lcom/helpshift/util/SchemaUtil;->validatePlatformId(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    .line 128
    iput-object p2, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    .line 131
    :cond_1
    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    const-string p2, "apiKey"

    .line 132
    iget-object p3, p0, Lcom/helpshift/model/AppInfoModel;->apiKey:Ljava/lang/String;

    invoke-virtual {p1, p2, p3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "domainName"

    .line 133
    iget-object p3, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    invoke-virtual {p1, p2, p3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string p2, "platformId"

    .line 134
    iget-object p3, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    invoke-virtual {p1, p2, p3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 136
    iget-object p2, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    invoke-interface {p2, p1}, Lcom/helpshift/storage/KeyValueStorage;->setKeyValues(Ljava/util/Map;)Z

    return-void
.end method

.method public isInstalled()Z
    .locals 1

    .line 146
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->apiKey:Ljava/lang/String;

    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->domainName:Ljava/lang/String;

    .line 147
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->platformId:Ljava/lang/String;

    .line 148
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setDisableAnimations(Ljava/lang/Boolean;)V
    .locals 2

    .line 100
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->disableAnimations:Ljava/lang/Boolean;

    .line 101
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "disableAnimations"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setDisableHelpshiftBranding(Ljava/lang/Boolean;)V
    .locals 2

    .line 90
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->disableHelpshiftBranding:Ljava/lang/Boolean;

    .line 91
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "disableHelpshiftBranding"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setEnableInboxPolling(Ljava/lang/Boolean;)V
    .locals 2

    .line 95
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->enableInboxPolling:Ljava/lang/Boolean;

    .line 96
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "enableInboxPolling"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setFontPath(Ljava/lang/String;)V
    .locals 2

    .line 65
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->fontPath:Ljava/lang/String;

    .line 66
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "font"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setLargeNotificationIconId(Ljava/lang/Integer;)V
    .locals 2

    .line 85
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->largeNotificationIconId:Ljava/lang/Integer;

    .line 86
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "largeNotificationIcon"

    iget-object v1, p0, Lcom/helpshift/model/AppInfoModel;->largeNotificationIconId:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setMuteNotifications(Ljava/lang/Boolean;)V
    .locals 2

    .line 105
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->muteNotifications:Ljava/lang/Boolean;

    .line 106
    iget-object v0, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v1, "muteNotifications"

    invoke-interface {v0, v1, p1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setNotificationIconId(Ljava/lang/Integer;)V
    .locals 2

    .line 75
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->notificationIconId:Ljava/lang/Integer;

    .line 76
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "notificationIcon"

    iget-object v1, p0, Lcom/helpshift/model/AppInfoModel;->notificationIconId:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setNotificationSoundId(Ljava/lang/Integer;)V
    .locals 2

    .line 70
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->notificationSoundId:Ljava/lang/Integer;

    .line 71
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "notificationSound"

    iget-object v1, p0, Lcom/helpshift/model/AppInfoModel;->notificationSoundId:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method

.method public setScreenOrientation(Ljava/lang/Integer;)V
    .locals 2

    .line 80
    iput-object p1, p0, Lcom/helpshift/model/AppInfoModel;->screenOrientation:Ljava/lang/Integer;

    .line 81
    iget-object p1, p0, Lcom/helpshift/model/AppInfoModel;->storage:Lcom/helpshift/storage/KeyValueStorage;

    const-string v0, "screenOrientation"

    iget-object v1, p0, Lcom/helpshift/model/AppInfoModel;->screenOrientation:Ljava/lang/Integer;

    invoke-interface {p1, v0, v1}, Lcom/helpshift/storage/KeyValueStorage;->set(Ljava/lang/String;Ljava/io/Serializable;)Z

    return-void
.end method
