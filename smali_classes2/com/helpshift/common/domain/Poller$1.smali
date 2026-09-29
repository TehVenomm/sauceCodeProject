.class Lcom/helpshift/common/domain/Poller$1;
.super Ljava/lang/Object;
.source "Poller.java"

# interfaces
.implements Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/common/domain/Poller;->getPollerRetryPollicy()Lcom/helpshift/common/poller/HttpBackoff$RetryPolicy;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/common/domain/Poller;


# direct methods
.method constructor <init>(Lcom/helpshift/common/domain/Poller;)V
    .locals 0

    .line 87
    iput-object p1, p0, Lcom/helpshift/common/domain/Poller$1;->this$0:Lcom/helpshift/common/domain/Poller;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public shouldRetry(I)Z
    .locals 2

    .line 95
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->AUTH_TOKEN_NOT_PROVIDED:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    const/4 v1, 0x0

    if-eq p1, v0, :cond_2

    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->INVALID_AUTH_TOKEN:Ljava/lang/Integer;

    .line 96
    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    if-ne p1, v0, :cond_0

    goto :goto_0

    .line 100
    :cond_0
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NOT_RETRIABLE_STATUS_CODES:Ljava/util/Set;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    return v1

    :cond_1
    const/4 p1, 0x1

    return p1

    :cond_2
    :goto_0
    return v1
.end method
