.class public Lcom/google/games/bridge/TokenResult;
.super Ljava/lang/Object;
.source "TokenResult.java"

# interfaces
.implements Lcom/google/android/gms/common/api/Result;


# instance fields
.field private authCode:Ljava/lang/String;

.field private email:Ljava/lang/String;

.field private idToken:Ljava/lang/String;

.field private status:Lcom/google/android/gms/common/api/Status;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;I)V
    .locals 1

    .line 36
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 37
    new-instance v0, Lcom/google/android/gms/common/api/Status;

    invoke-direct {v0, p4}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    iput-object v0, p0, Lcom/google/games/bridge/TokenResult;->status:Lcom/google/android/gms/common/api/Status;

    .line 38
    iput-object p1, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    .line 39
    iput-object p3, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    .line 40
    iput-object p2, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getAuthCode()Ljava/lang/String;
    .locals 1

    .line 71
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, ""

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    :goto_0
    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 1

    .line 79
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, ""

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    :goto_0
    return-object v0
.end method

.method public getIdToken()Ljava/lang/String;
    .locals 1

    .line 75
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    if-nez v0, :cond_0

    const-string v0, ""

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    :goto_0
    return-object v0
.end method

.method public getStatus()Lcom/google/android/gms/common/api/Status;
    .locals 1

    .line 65
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->status:Lcom/google/android/gms/common/api/Status;

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 68
    iget-object v0, p0, Lcom/google/games/bridge/TokenResult;->status:Lcom/google/android/gms/common/api/Status;

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result v0

    return v0
.end method

.method public setAuthCode(Ljava/lang/String;)V
    .locals 0

    .line 91
    iput-object p1, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 0

    .line 87
    iput-object p1, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    return-void
.end method

.method public setIdToken(Ljava/lang/String;)V
    .locals 0

    .line 95
    iput-object p1, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    return-void
.end method

.method public setStatus(I)V
    .locals 1

    .line 83
    new-instance v0, Lcom/google/android/gms/common/api/Status;

    invoke-direct {v0, p1}, Lcom/google/android/gms/common/api/Status;-><init>(I)V

    iput-object v0, p0, Lcom/google/games/bridge/TokenResult;->status:Lcom/google/android/gms/common/api/Status;

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 2

    .line 58
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Status: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->status:Lcom/google/android/gms/common/api/Status;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, " email: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    if-nez v1, :cond_0

    const-string v1, "<null>"

    goto :goto_0

    :cond_0
    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->email:Ljava/lang/String;

    :goto_0
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " id:"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    if-nez v1, :cond_1

    const-string v1, "<null>"

    goto :goto_1

    :cond_1
    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->idToken:Ljava/lang/String;

    :goto_1
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, " access: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    if-nez v1, :cond_2

    const-string v1, "<null>"

    goto :goto_2

    :cond_2
    iget-object v1, p0, Lcom/google/games/bridge/TokenResult;->authCode:Ljava/lang/String;

    :goto_2
    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
