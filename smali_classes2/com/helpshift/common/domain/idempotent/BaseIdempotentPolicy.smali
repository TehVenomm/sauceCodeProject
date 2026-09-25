.class public abstract Lcom/helpshift/common/domain/idempotent/BaseIdempotentPolicy;
.super Ljava/lang/Object;
.source "BaseIdempotentPolicy.java"

# interfaces
.implements Lcom/helpshift/common/domain/idempotent/IdempotentPolicy;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final isRequestCompleted(I)Z
    .locals 1

    .line 17
    sget-object v0, Lcom/helpshift/common/domain/network/NetworkErrorCodes;->PROCESSING_REQUEST:Ljava/lang/Integer;

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    if-ne p1, v0, :cond_0

    const/4 p1, 0x0

    return p1

    .line 22
    :cond_0
    invoke-virtual {p0, p1}, Lcom/helpshift/common/domain/idempotent/BaseIdempotentPolicy;->shouldMarkRequestCompleted(I)Z

    move-result p1

    return p1
.end method

.method abstract shouldMarkRequestCompleted(I)Z
.end method
