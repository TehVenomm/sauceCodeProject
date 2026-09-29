.class public Lcom/helpshift/common/domain/idempotent/SuccessOrNonRetriableStatusCodeIdempotentPolicy;
.super Lcom/helpshift/common/domain/idempotent/BaseIdempotentPolicy;
.source "SuccessOrNonRetriableStatusCodeIdempotentPolicy.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 8
    invoke-direct {p0}, Lcom/helpshift/common/domain/idempotent/BaseIdempotentPolicy;-><init>()V

    return-void
.end method


# virtual methods
.method shouldMarkRequestCompleted(I)Z
    .locals 1

    const/16 v0, 0xc8

    if-lt p1, v0, :cond_0

    const/16 v0, 0x12c

    if-ge p1, v0, :cond_0

    const/4 p1, 0x1

    return p1

    .line 19
    :cond_0
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->NOT_RETRIABLE_STATUS_CODES:Ljava/util/Set;

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method
