.class public Lcom/helpshift/configuration/dto/RootInstallConfig;
.super Ljava/lang/Object;
.source "RootInstallConfig.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/configuration/dto/RootInstallConfig$RootInstallConfigBuilder;
    }
.end annotation


# instance fields
.field public final disableAnimations:Ljava/lang/Boolean;

.field public final disableAppLaunchEvent:Ljava/lang/Boolean;

.field public final disableErrorLogging:Ljava/lang/Boolean;

.field public final disableHelpshiftBranding:Ljava/lang/Boolean;

.field public final enableDefaultFallbackLanguage:Ljava/lang/Boolean;

.field public final enableInAppNotification:Ljava/lang/Boolean;

.field public final enableInboxPolling:Ljava/lang/Boolean;

.field public final enableNotificationMute:Ljava/lang/Boolean;

.field public final fontPath:Ljava/lang/String;

.field public final largeNotificationIcon:Ljava/lang/Integer;

.field public final notificationIcon:Ljava/lang/Integer;

.field public final notificationSound:Ljava/lang/Integer;

.field public final pluginVersion:Ljava/lang/String;

.field public final runtimeVersion:Ljava/lang/String;

.field public final sdkType:Ljava/lang/String;

.field public final supportNotificationChannelId:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Boolean;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/Integer;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 2

    move-object v0, p0

    .line 40
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    move-object v1, p1

    .line 41
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableInAppNotification:Ljava/lang/Boolean;

    move-object v1, p4

    .line 42
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableNotificationMute:Ljava/lang/Boolean;

    move-object v1, p5

    .line 43
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableHelpshiftBranding:Ljava/lang/Boolean;

    move-object v1, p6

    .line 44
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableAnimations:Ljava/lang/Boolean;

    move-object v1, p7

    .line 45
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableErrorLogging:Ljava/lang/Boolean;

    move-object v1, p8

    .line 46
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->disableAppLaunchEvent:Ljava/lang/Boolean;

    move-object v1, p9

    .line 47
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->notificationIcon:Ljava/lang/Integer;

    move-object v1, p10

    .line 48
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->largeNotificationIcon:Ljava/lang/Integer;

    move-object v1, p11

    .line 49
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->notificationSound:Ljava/lang/Integer;

    move-object v1, p2

    .line 50
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableDefaultFallbackLanguage:Ljava/lang/Boolean;

    move-object v1, p3

    .line 51
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->enableInboxPolling:Ljava/lang/Boolean;

    move-object v1, p12

    .line 52
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->fontPath:Ljava/lang/String;

    move-object v1, p13

    .line 53
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->sdkType:Ljava/lang/String;

    move-object/from16 v1, p14

    .line 54
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->pluginVersion:Ljava/lang/String;

    move-object/from16 v1, p15

    .line 55
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->runtimeVersion:Ljava/lang/String;

    move-object/from16 v1, p16

    .line 56
    iput-object v1, v0, Lcom/helpshift/configuration/dto/RootInstallConfig;->supportNotificationChannelId:Ljava/lang/String;

    return-void
.end method
