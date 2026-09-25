.class public Lcom/helpshift/InstallConfig$Builder;
.super Ljava/lang/Object;
.source "InstallConfig.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/InstallConfig;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field private enableDefaultFallbackLanguage:Z

.field private enableInAppNotification:Z

.field private enableInboxPolling:Z

.field private enableLogging:Z

.field private extras:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private fontPath:Ljava/lang/String;

.field private largeNotificationIcon:I

.field private notificationIcon:I

.field private notificationSound:I

.field private screenOrientation:I

.field private supportNotificationChannelId:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 95
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x1

    .line 96
    iput-boolean v0, p0, Lcom/helpshift/InstallConfig$Builder;->enableInAppNotification:Z

    .line 100
    iput-boolean v0, p0, Lcom/helpshift/InstallConfig$Builder;->enableInboxPolling:Z

    .line 101
    iput-boolean v0, p0, Lcom/helpshift/InstallConfig$Builder;->enableDefaultFallbackLanguage:Z

    const/4 v0, 0x0

    .line 102
    iput-object v0, p0, Lcom/helpshift/InstallConfig$Builder;->fontPath:Ljava/lang/String;

    const/4 v0, 0x0

    .line 103
    iput-boolean v0, p0, Lcom/helpshift/InstallConfig$Builder;->enableLogging:Z

    const/4 v0, -0x1

    .line 104
    iput v0, p0, Lcom/helpshift/InstallConfig$Builder;->screenOrientation:I

    return-void
.end method


# virtual methods
.method public build()Lcom/helpshift/InstallConfig;
    .locals 13

    .line 260
    new-instance v12, Lcom/helpshift/InstallConfig;

    iget-boolean v1, p0, Lcom/helpshift/InstallConfig$Builder;->enableInAppNotification:Z

    iget v2, p0, Lcom/helpshift/InstallConfig$Builder;->notificationIcon:I

    iget v3, p0, Lcom/helpshift/InstallConfig$Builder;->largeNotificationIcon:I

    iget v4, p0, Lcom/helpshift/InstallConfig$Builder;->notificationSound:I

    iget-boolean v5, p0, Lcom/helpshift/InstallConfig$Builder;->enableDefaultFallbackLanguage:Z

    iget-boolean v6, p0, Lcom/helpshift/InstallConfig$Builder;->enableInboxPolling:Z

    iget-object v7, p0, Lcom/helpshift/InstallConfig$Builder;->fontPath:Ljava/lang/String;

    iget-boolean v8, p0, Lcom/helpshift/InstallConfig$Builder;->enableLogging:Z

    iget v9, p0, Lcom/helpshift/InstallConfig$Builder;->screenOrientation:I

    iget-object v10, p0, Lcom/helpshift/InstallConfig$Builder;->supportNotificationChannelId:Ljava/lang/String;

    iget-object v11, p0, Lcom/helpshift/InstallConfig$Builder;->extras:Ljava/util/Map;

    move-object v0, v12

    invoke-direct/range {v0 .. v11}, Lcom/helpshift/InstallConfig;-><init>(ZIIIZZLjava/lang/String;ZILjava/lang/String;Ljava/util/Map;)V

    return-object v12
.end method

.method public setEnableDefaultFallbackLanguage(Z)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 177
    iput-boolean p1, p0, Lcom/helpshift/InstallConfig$Builder;->enableDefaultFallbackLanguage:Z

    return-object p0
.end method

.method public setEnableInAppNotification(Z)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 120
    iput-boolean p1, p0, Lcom/helpshift/InstallConfig$Builder;->enableInAppNotification:Z

    return-object p0
.end method

.method public setEnableInboxPolling(Z)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 191
    iput-boolean p1, p0, Lcom/helpshift/InstallConfig$Builder;->enableInboxPolling:Z

    return-object p0
.end method

.method public setEnableLogging(Z)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 237
    iput-boolean p1, p0, Lcom/helpshift/InstallConfig$Builder;->enableLogging:Z

    return-object p0
.end method

.method public setExtras(Ljava/util/Map;)Lcom/helpshift/InstallConfig$Builder;
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/helpshift/InstallConfig$Builder;"
        }
    .end annotation

    .line 226
    iput-object p1, p0, Lcom/helpshift/InstallConfig$Builder;->extras:Ljava/util/Map;

    return-object p0
.end method

.method public setFont(Ljava/lang/String;)Lcom/helpshift/InstallConfig$Builder;
    .locals 0
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 203
    iput-object p1, p0, Lcom/helpshift/InstallConfig$Builder;->fontPath:Ljava/lang/String;

    return-object p0
.end method

.method public setLargeNotificationIcon(I)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    if-eqz p1, :cond_0

    .line 147
    iput p1, p0, Lcom/helpshift/InstallConfig$Builder;->largeNotificationIcon:I

    :cond_0
    return-object p0
.end method

.method public setNotificationIcon(I)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    if-eqz p1, :cond_0

    .line 133
    iput p1, p0, Lcom/helpshift/InstallConfig$Builder;->notificationIcon:I

    :cond_0
    return-object p0
.end method

.method public setNotificationSound(I)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    if-eqz p1, :cond_0

    .line 161
    iput p1, p0, Lcom/helpshift/InstallConfig$Builder;->notificationSound:I

    :cond_0
    return-object p0
.end method

.method public setScreenOrientation(I)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 221
    iput p1, p0, Lcom/helpshift/InstallConfig$Builder;->screenOrientation:I

    return-object p0
.end method

.method public setSupportNotificationChannelId(Ljava/lang/String;)Lcom/helpshift/InstallConfig$Builder;
    .locals 0

    .line 249
    iput-object p1, p0, Lcom/helpshift/InstallConfig$Builder;->supportNotificationChannelId:Ljava/lang/String;

    return-object p0
.end method
