.class synthetic Lcom/helpshift/common/AutoRetryFailedEventDM$5;
.super Ljava/lang/Object;
.source "AutoRetryFailedEventDM.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/common/AutoRetryFailedEventDM;
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

    .line 192
    invoke-static {}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->values()[Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    move-result-object v0

    array-length v0, v0

    new-array v0, v0, [I

    sput-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$5;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    :try_start_0
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$5;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->PUSH_TOKEN:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result v1

    const/4 v2, 0x1

    aput v2, v0, v1
    :try_end_0
    .catch Ljava/lang/NoSuchFieldError; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :try_start_1
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$5;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->CONVERSATION:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result v1

    const/4 v2, 0x2

    aput v2, v0, v1
    :try_end_1
    .catch Ljava/lang/NoSuchFieldError; {:try_start_1 .. :try_end_1} :catch_1

    :catch_1
    :try_start_2
    sget-object v0, Lcom/helpshift/common/AutoRetryFailedEventDM$5;->$SwitchMap$com$helpshift$common$AutoRetryFailedEventDM$EventType:[I

    sget-object v1, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->SYNC_USER:Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;

    invoke-virtual {v1}, Lcom/helpshift/common/AutoRetryFailedEventDM$EventType;->ordinal()I

    move-result v1

    const/4 v2, 0x3

    aput v2, v0, v1
    :try_end_2
    .catch Ljava/lang/NoSuchFieldError; {:try_start_2 .. :try_end_2} :catch_2

    :catch_2
    return-void
.end method
