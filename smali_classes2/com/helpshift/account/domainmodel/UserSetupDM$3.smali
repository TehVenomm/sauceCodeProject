.class synthetic Lcom/helpshift/account/domainmodel/UserSetupDM$3;
.super Ljava/lang/Object;
.source "UserSetupDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/account/domainmodel/UserSetupDM;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 248
    invoke-static {}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->values()[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/account/domainmodel/UserSetupDM$3;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupDM$3;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->MIGRATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/account/domainmodel/UserSetupDM$3;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    return-void
.end method
