.class public Lcom/helpshift/InstallConfig;
.super Ljava/lang/Object;
.source "InstallConfig.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/InstallConfig$Builder;
    }
.end annotation


# instance fields
.field private final enableDefaultFallbackLanguage:Z

.field private final enableInAppNotification:Z

.field private final enableInboxPolling:Z

.field private final enableLogging:Z

.field private final extras:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private final fontPath:Ljava/lang/String;

.field private final largeNotificationIcon:I

.field private final notificationIcon:I

.field private final notificationSound:I

.field private final screenOrientation:I

.field private final supportNotificationChannelId:Ljava/lang/String;


# direct methods
.method constructor <init>(ZIIIZZLjava/lang/String;ZILjava/lang/String;Ljava/util/Map;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(ZIIIZZ",
            "Ljava/lang/String;",
            "ZI",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .line 38
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 39
    iput-boolean p1, p0, Lcom/helpshift/InstallConfig;->enableInAppNotification:Z

    .line 40
    iput p2, p0, Lcom/helpshift/InstallConfig;->notificationIcon:I

    .line 41
    iput p3, p0, Lcom/helpshift/InstallConfig;->largeNotificationIcon:I

    .line 42
    iput p4, p0, Lcom/helpshift/InstallConfig;->notificationSound:I

    .line 43
    iput-boolean p5, p0, Lcom/helpshift/InstallConfig;->enableDefaultFallbackLanguage:Z

    .line 44
    iput-boolean p6, p0, Lcom/helpshift/InstallConfig;->enableInboxPolling:Z

    .line 45
    iput-object p7, p0, Lcom/helpshift/InstallConfig;->fontPath:Ljava/lang/String;

    .line 46
    iput p9, p0, Lcom/helpshift/InstallConfig;->screenOrientation:I

    .line 47
    iput-object p11, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    .line 48
    iput-boolean p8, p0, Lcom/helpshift/InstallConfig;->enableLogging:Z

    .line 49
    iput-object p10, p0, Lcom/helpshift/InstallConfig;->supportNotificationChannelId:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public toMap()Ljava/util/Map;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .line 56
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    const-string v1, "enableInAppNotification"

    .line 57
    iget-boolean v2, p0, Lcom/helpshift/InstallConfig;->enableInAppNotification:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 58
    iget v1, p0, Lcom/helpshift/InstallConfig;->notificationIcon:I

    if-eqz v1, :cond_0

    const-string v1, "notificationIcon"

    .line 59
    iget v2, p0, Lcom/helpshift/InstallConfig;->notificationIcon:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 61
    :cond_0
    iget v1, p0, Lcom/helpshift/InstallConfig;->largeNotificationIcon:I

    if-eqz v1, :cond_1

    const-string v1, "largeNotificationIcon"

    .line 62
    iget v2, p0, Lcom/helpshift/InstallConfig;->largeNotificationIcon:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 64
    :cond_1
    iget v1, p0, Lcom/helpshift/InstallConfig;->notificationSound:I

    if-eqz v1, :cond_2

    const-string v1, "notificationSound"

    .line 65
    iget v2, p0, Lcom/helpshift/InstallConfig;->notificationSound:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    :cond_2
    const-string v1, "enableDefaultFallbackLanguage"

    .line 67
    iget-boolean v2, p0, Lcom/helpshift/InstallConfig;->enableDefaultFallbackLanguage:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "enableInboxPolling"

    .line 68
    iget-boolean v2, p0, Lcom/helpshift/InstallConfig;->enableInboxPolling:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "enableLogging"

    .line 69
    iget-boolean v2, p0, Lcom/helpshift/InstallConfig;->enableLogging:Z

    invoke-static {v2}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "font"

    .line 70
    iget-object v2, p0, Lcom/helpshift/InstallConfig;->fontPath:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "screenOrientation"

    .line 71
    iget v2, p0, Lcom/helpshift/InstallConfig;->screenOrientation:I

    invoke-static {v2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 72
    iget-object v1, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    if-eqz v1, :cond_5

    .line 75
    iget-object v1, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    const-string v2, "disableErrorLogging"

    invoke-interface {v1, v2}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    if-eqz v1, :cond_3

    .line 77
    iget-object v2, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    const-string v3, "disableErrorReporting"

    invoke-interface {v2, v3, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 80
    :cond_3
    iget-object v1, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    invoke-interface {v1}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :cond_4
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_5

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 81
    iget-object v3, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    if-eqz v3, :cond_4

    .line 82
    iget-object v3, p0, Lcom/helpshift/InstallConfig;->extras:Ljava/util/Map;

    invoke-interface {v3, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    invoke-interface {v0, v2, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :cond_5
    const-string v1, "sdkType"

    const-string v2, "android"

    .line 86
    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v1, "supportNotificationChannelId"

    .line 87
    iget-object v2, p0, Lcom/helpshift/InstallConfig;->supportNotificationChannelId:Ljava/lang/String;

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    return-object v0
.end method
