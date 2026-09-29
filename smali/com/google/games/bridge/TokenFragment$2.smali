.class Lcom/google/games/bridge/TokenFragment$2;
.super Ljava/lang/Object;
.source "TokenFragment.java"

# interfaces
.implements Lcom/google/android/gms/common/api/ResultCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/google/games/bridge/TokenFragment;->onConnected(Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/google/android/gms/common/api/ResultCallback<",
        "Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lcom/google/games/bridge/TokenFragment;


# direct methods
.method constructor <init>(Lcom/google/games/bridge/TokenFragment;)V
    .locals 0

    .line 403
    iput-object p1, p0, Lcom/google/games/bridge/TokenFragment$2;->this$0:Lcom/google/games/bridge/TokenFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onResult(Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;)V
    .locals 3
    .param p1    # Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 407
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->isSuccess()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 408
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$2;->this$0:Lcom/google/games/bridge/TokenFragment;

    .line 409
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v1

    .line 410
    invoke-virtual {v1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result v1

    .line 411
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getSignInAccount()Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;

    move-result-object p1

    .line 408
    invoke-static {v0, v1, p1}, Lcom/google/games/bridge/TokenFragment;->access$000(Lcom/google/games/bridge/TokenFragment;ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    goto :goto_0

    :cond_0
    const-string v0, "TokenFragment"

    .line 413
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error with silentSignIn: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 414
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 413
    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 415
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$2;->this$0:Lcom/google/games/bridge/TokenFragment;

    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v1

    .line 416
    invoke-virtual {v1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result v1

    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getSignInAccount()Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;

    move-result-object p1

    .line 415
    invoke-static {v0, v1, p1}, Lcom/google/games/bridge/TokenFragment;->access$000(Lcom/google/games/bridge/TokenFragment;ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    :goto_0
    return-void
.end method

.method public bridge synthetic onResult(Lcom/google/android/gms/common/api/Result;)V
    .locals 0
    .param p1    # Lcom/google/android/gms/common/api/Result;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 403
    check-cast p1, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;

    invoke-virtual {p0, p1}, Lcom/google/games/bridge/TokenFragment$2;->onResult(Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;)V

    return-void
.end method
