.class Lcom/google/games/bridge/TokenFragment$1;
.super Ljava/lang/Object;
.source "TokenFragment.java"

# interfaces
.implements Lcom/google/android/gms/common/api/ResultCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/google/games/bridge/TokenFragment;->processRequest()V
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

    .line 206
    iput-object p1, p0, Lcom/google/games/bridge/TokenFragment$1;->this$0:Lcom/google/games/bridge/TokenFragment;

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

    .line 210
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->isSuccess()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 211
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$1;->this$0:Lcom/google/games/bridge/TokenFragment;

    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v1

    invoke-virtual {v1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result v1

    .line 212
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getSignInAccount()Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;

    move-result-object p1

    .line 211
    invoke-static {v0, v1, p1}, Lcom/google/games/bridge/TokenFragment;->access$000(Lcom/google/games/bridge/TokenFragment;ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    goto :goto_0

    .line 213
    :cond_0
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v0

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result v0

    const/4 v1, 0x4

    if-ne v0, v1, :cond_1

    .line 214
    sget-object p1, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$1;->this$0:Lcom/google/games/bridge/TokenFragment;

    .line 215
    invoke-static {v0}, Lcom/google/games/bridge/TokenFragment;->access$100(Lcom/google/games/bridge/TokenFragment;)Lcom/google/android/gms/common/api/GoogleApiClient;

    move-result-object v0

    invoke-interface {p1, v0}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->getSignInIntent(Lcom/google/android/gms/common/api/GoogleApiClient;)Landroid/content/Intent;

    move-result-object p1

    .line 216
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$1;->this$0:Lcom/google/games/bridge/TokenFragment;

    const/16 v1, 0x232a

    invoke-virtual {v0, p1, v1}, Lcom/google/games/bridge/TokenFragment;->startActivityForResult(Landroid/content/Intent;I)V

    goto :goto_0

    :cond_1
    const-string v0, "TokenFragment"

    .line 218
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Error with silentSignIn: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 220
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 218
    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 221
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment$1;->this$0:Lcom/google/games/bridge/TokenFragment;

    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result p1

    const/4 v1, 0x0

    invoke-static {v0, p1, v1}, Lcom/google/games/bridge/TokenFragment;->access$000(Lcom/google/games/bridge/TokenFragment;ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    :goto_0
    return-void
.end method

.method public bridge synthetic onResult(Lcom/google/android/gms/common/api/Result;)V
    .locals 0
    .param p1    # Lcom/google/android/gms/common/api/Result;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    .line 206
    check-cast p1, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;

    invoke-virtual {p0, p1}, Lcom/google/games/bridge/TokenFragment$1;->onResult(Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;)V

    return-void
.end method
