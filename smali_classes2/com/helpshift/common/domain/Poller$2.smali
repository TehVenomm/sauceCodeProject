.class synthetic Lcom/helpshift/common/domain/Poller$2;
.super Ljava/lang/Object;
.source "Poller.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/domain/Poller;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1008
    name = null
.end annotation


# static fields
.field static final synthetic $SwitchMap$com$helpshift$common$domain$PollingInterval:[I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .line 60
    invoke-static {}, Lcom/helpshift/common/domain/PollingInterval;->values()[Lcom/helpshift/common/domain/PollingInterval;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/common/domain/Poller$2;->$SwitchMap$com$helpshift$common$domain$PollingInterval:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/common/domain/Poller$2;->$SwitchMap$com$helpshift$common$domain$PollingInterval:[I

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->AGGRESSIVE:Lcom/helpshift/common/domain/PollingInterval;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/PollingInterval;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/common/domain/Poller$2;->$SwitchMap$com$helpshift$common$domain$PollingInterval:[I

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->PASSIVE:Lcom/helpshift/common/domain/PollingInterval;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/PollingInterval;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/common/domain/Poller$2;->$SwitchMap$com$helpshift$common$domain$PollingInterval:[I

    sget-object v1, Lcom/helpshift/common/domain/PollingInterval;->CONSERVATIVE:Lcom/helpshift/common/domain/PollingInterval;

    invoke-virtual {v1}, Lcom/helpshift/common/domain/PollingInterval;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
