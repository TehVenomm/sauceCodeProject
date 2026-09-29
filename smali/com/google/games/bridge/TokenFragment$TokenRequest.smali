.class Lcom/google/games/bridge/TokenFragment$TokenRequest;
.super Ljava/lang/Object;
.source "TokenFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/google/games/bridge/TokenFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0xa
    name = "TokenRequest"
.end annotation


# instance fields
.field private accountName:Ljava/lang/String;

.field private doAuthCode:Z

.field private doEmail:Z

.field private doIdToken:Z

.field private forceRefresh:Z

.field private hidePopups:Z

.field private pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

.field private scopes:[Ljava/lang/String;

.field private webClientId:Ljava/lang/String;


# direct methods
.method public constructor <init>(ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)V
    .locals 1

    .line 456
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 457
    new-instance v0, Lcom/google/games/bridge/TokenPendingResult;

    invoke-direct {v0}, Lcom/google/games/bridge/TokenPendingResult;-><init>()V

    iput-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    .line 458
    iput-boolean p1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doAuthCode:Z

    .line 459
    iput-boolean p2, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doEmail:Z

    .line 460
    iput-boolean p3, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doIdToken:Z

    .line 461
    iput-object p4, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->webClientId:Ljava/lang/String;

    .line 462
    iput-boolean p5, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->forceRefresh:Z

    if-eqz p6, :cond_0

    .line 463
    array-length p1, p6

    if-lez p1, :cond_0

    .line 464
    array-length p1, p6

    new-array p1, p1, [Ljava/lang/String;

    iput-object p1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->scopes:[Ljava/lang/String;

    .line 465
    iget-object p1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->scopes:[Ljava/lang/String;

    array-length p2, p6

    const/4 p3, 0x0

    invoke-static {p6, p3, p1, p3, p2}, Ljava/lang/System;->arraycopy(Ljava/lang/Object;ILjava/lang/Object;II)V

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    .line 467
    iput-object p1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->scopes:[Ljava/lang/String;

    .line 469
    :goto_0
    iput-boolean p7, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->hidePopups:Z

    .line 470
    iput-object p8, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->accountName:Ljava/lang/String;

    return-void
.end method

.method static synthetic access$200(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z
    .locals 0

    .line 442
    iget-boolean p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doAuthCode:Z

    return p0
.end method

.method static synthetic access$300(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z
    .locals 0

    .line 442
    iget-boolean p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doEmail:Z

    return p0
.end method

.method static synthetic access$400(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z
    .locals 0

    .line 442
    iget-boolean p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doIdToken:Z

    return p0
.end method

.method static synthetic access$500(Lcom/google/games/bridge/TokenFragment$TokenRequest;)[Ljava/lang/String;
    .locals 0

    .line 442
    iget-object p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->scopes:[Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$600(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z
    .locals 0

    .line 442
    iget-boolean p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->hidePopups:Z

    return p0
.end method

.method static synthetic access$700(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Ljava/lang/String;
    .locals 0

    .line 442
    iget-object p0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->accountName:Ljava/lang/String;

    return-object p0
.end method


# virtual methods
.method public cancel()V
    .locals 1

    .line 486
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    invoke-virtual {v0}, Lcom/google/games/bridge/TokenPendingResult;->cancel()V

    return-void
.end method

.method public getAuthCode()Ljava/lang/String;
    .locals 1

    .line 506
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    iget-object v0, v0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0}, Lcom/google/games/bridge/TokenResult;->getAuthCode()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 1

    .line 498
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    iget-object v0, v0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0}, Lcom/google/games/bridge/TokenResult;->getEmail()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getForceRefresh()Z
    .locals 1

    .line 521
    iget-boolean v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->forceRefresh:Z

    return v0
.end method

.method public getIdToken()Ljava/lang/String;
    .locals 1

    .line 502
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    iget-object v0, v0, Lcom/google/games/bridge/TokenPendingResult;->result:Lcom/google/games/bridge/TokenResult;

    invoke-virtual {v0}, Lcom/google/games/bridge/TokenResult;->getIdToken()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPendingResponse()Lcom/google/android/gms/common/api/PendingResult;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Lcom/google/android/gms/common/api/PendingResult<",
            "Lcom/google/games/bridge/TokenResult;",
            ">;"
        }
    .end annotation

    .line 474
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    return-object v0
.end method

.method public getWebClientId()Ljava/lang/String;
    .locals 1

    .line 517
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->webClientId:Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, ""

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->webClientId:Ljava/lang/String;

    :goto_0
    return-object v0
.end method

.method public setAuthCode(Ljava/lang/String;)V
    .locals 1

    .line 490
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setAuthCode(Ljava/lang/String;)V

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 1

    .line 482
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setEmail(Ljava/lang/String;)V

    return-void
.end method

.method public setIdToken(Ljava/lang/String;)V
    .locals 1

    .line 494
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setIdToken(Ljava/lang/String;)V

    return-void
.end method

.method public setResult(I)V
    .locals 1

    .line 478
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->pendingResponse:Lcom/google/games/bridge/TokenPendingResult;

    invoke-virtual {v0, p1}, Lcom/google/games/bridge/TokenPendingResult;->setStatus(I)V

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    .line 511
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p0}, Ljava/lang/Object;->hashCode()I

    move-result v1

    invoke-static {v1}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " (a:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doAuthCode:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, " e:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doEmail:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, " i:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->doIdToken:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, " wc: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->webClientId:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " f: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-boolean v1, p0, Lcom/google/games/bridge/TokenFragment$TokenRequest;->forceRefresh:Z

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    const-string v1, ")"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
