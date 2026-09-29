.class synthetic Lcom/helpshift/notifications/NotificationChannelsManager$1;
.super Ljava/lang/Object;
.source "NotificationChannelsManager.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/notifications/NotificationChannelsManager;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$notifications$NotificationChannelsManager$NotificationChannelType:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 35
    invoke-static {}, Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;->values()[Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/notifications/NotificationChannelsManager$1;->$SwitchMap$com$helpshift$notifications$NotificationChannelsManager$NotificationChannelType:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/notifications/NotificationChannelsManager$1;->$SwitchMap$com$helpshift$notifications$NotificationChannelsManager$NotificationChannelType:[I

    sget-object v1, Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;->SUPPORT:Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;

    invoke-virtual {v1}, Lcom/helpshift/notifications/NotificationChannelsManager$NotificationChannelType;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    return-void
.end method
