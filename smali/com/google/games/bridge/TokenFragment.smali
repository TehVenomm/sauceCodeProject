.class public Lcom/google/games/bridge/TokenFragment;
.super Landroid/app/Fragment;
.source "TokenFragment.java"

# interfaces
.implements Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/google/games/bridge/TokenFragment$TokenRequest;
    }
.end annotation


# static fields
.field private static final FRAGMENT_TAG:Ljava/lang/String; = "gpg.AuthTokenSupport"

.field private static final RC_ACCT:I = 0x232a

.field private static final TAG:Ljava/lang/String; = "TokenFragment"

.field private static helperFragment:Lcom/google/games/bridge/TokenFragment;

.field private static final lock:Ljava/lang/Object;

.field private static pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;


# instance fields
.field private mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 53
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 44
    invoke-direct {p0}, Landroid/app/Fragment;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lcom/google/games/bridge/TokenFragment;ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V
    .locals 0

    .line 44
    invoke-direct {p0, p1, p2}, Lcom/google/games/bridge/TokenFragment;->onSignedIn(ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    return-void
.end method

.method static synthetic access$100(Lcom/google/games/bridge/TokenFragment;)Lcom/google/android/gms/common/api/GoogleApiClient;
    .locals 0

    .line 44
    iget-object p0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    return-object p0
.end method

.method private buildClient(Lcom/google/games/bridge/TokenFragment$TokenRequest;)V
    .locals 7

    const-string v0, "TokenFragment"

    .line 238
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Building client for: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 239
    new-instance v0, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    sget-object v1, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;->DEFAULT_GAMES_SIGN_IN:Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;

    invoke-direct {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;-><init>(Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;)V

    .line 241
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$200(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z

    move-result v1

    const/4 v2, 0x0

    const/16 v3, 0xa

    if-eqz v1, :cond_1

    .line 242
    invoke-virtual {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getWebClientId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_0

    .line 243
    invoke-virtual {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getWebClientId()Ljava/lang/String;

    move-result-object v1

    .line 244
    invoke-virtual {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getForceRefresh()Z

    move-result v4

    .line 243
    invoke-virtual {v0, v1, v4}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->requestServerAuthCode(Ljava/lang/String;Z)Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    goto :goto_0

    :cond_0
    const-string v0, "TokenFragment"

    const-string v1, "Web client ID is needed for Auth Code"

    .line 246
    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 247
    invoke-virtual {p1, v3}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setResult(I)V

    .line 248
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v1

    .line 249
    :try_start_0
    sput-object v2, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 250
    monitor-exit v1

    return-void

    :catchall_0
    move-exception p1

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw p1

    .line 255
    :cond_1
    :goto_0
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$300(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 256
    invoke-virtual {v0}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->requestEmail()Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    .line 259
    :cond_2
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$400(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z

    move-result v1

    if-eqz v1, :cond_4

    .line 260
    invoke-virtual {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getWebClientId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_3

    .line 261
    invoke-virtual {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getWebClientId()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->requestIdToken(Ljava/lang/String;)Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    goto :goto_1

    :cond_3
    const-string v0, "TokenFragment"

    const-string v1, "Web client ID is needed for ID Token"

    .line 263
    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 264
    invoke-virtual {p1, v3}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setResult(I)V

    .line 265
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v1

    .line 266
    :try_start_1
    sput-object v2, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 267
    monitor-exit v1

    return-void

    :catchall_1
    move-exception p1

    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    throw p1

    .line 271
    :cond_4
    :goto_1
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$500(Lcom/google/games/bridge/TokenFragment$TokenRequest;)[Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x0

    if-eqz v1, :cond_5

    .line 272
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$500(Lcom/google/games/bridge/TokenFragment$TokenRequest;)[Ljava/lang/String;

    move-result-object v1

    array-length v3, v1

    const/4 v4, 0x0

    :goto_2
    if-ge v4, v3, :cond_5

    aget-object v5, v1, v4

    .line 273
    new-instance v6, Lcom/google/android/gms/common/api/Scope;

    invoke-direct {v6, v5}, Lcom/google/android/gms/common/api/Scope;-><init>(Ljava/lang/String;)V

    new-array v5, v2, [Lcom/google/android/gms/common/api/Scope;

    invoke-virtual {v0, v6, v5}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->requestScopes(Lcom/google/android/gms/common/api/Scope;[Lcom/google/android/gms/common/api/Scope;)Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    add-int/lit8 v4, v4, 0x1

    goto :goto_2

    .line 277
    :cond_5
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$600(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z

    move-result v1

    if-eqz v1, :cond_6

    const-string v1, "TokenFragment"

    const-string v3, "hiding popup views for games API"

    .line 278
    invoke-static {v1, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 280
    invoke-static {}, Lcom/google/android/gms/games/Games$GamesOptions;->builder()Lcom/google/android/gms/games/Games$GamesOptions$Builder;

    move-result-object v1

    invoke-virtual {v1, v2}, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->setShowConnectingPopup(Z)Lcom/google/android/gms/games/Games$GamesOptions$Builder;

    move-result-object v1

    .line 281
    invoke-virtual {v1}, Lcom/google/android/gms/games/Games$GamesOptions$Builder;->build()Lcom/google/android/gms/games/Games$GamesOptions;

    move-result-object v1

    .line 279
    invoke-virtual {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->addExtension(Lcom/google/android/gms/auth/api/signin/GoogleSignInOptionsExtension;)Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    .line 284
    :cond_6
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$700(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_7

    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$700(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result v1

    if-nez v1, :cond_7

    .line 285
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$700(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->setAccountName(Ljava/lang/String;)Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;

    .line 289
    :cond_7
    invoke-virtual {v0}, Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions$Builder;->build()Lcom/google/android/gms/auth/api/signin/GoogleSignInOptions;

    move-result-object v0

    .line 291
    new-instance v1, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    .line 292
    invoke-virtual {p0}, Lcom/google/games/bridge/TokenFragment;->getActivity()Landroid/app/Activity;

    move-result-object v3

    invoke-direct {v1, v3}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;-><init>(Landroid/content/Context;)V

    sget-object v3, Lcom/google/android/gms/auth/api/Auth;->GOOGLE_SIGN_IN_API:Lcom/google/android/gms/common/api/Api;

    .line 293
    invoke-virtual {v1, v3, v0}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->addApi(Lcom/google/android/gms/common/api/Api;Lcom/google/android/gms/common/api/Api$ApiOptions$HasOptions;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    move-result-object v0

    .line 294
    sget-object v1, Lcom/google/android/gms/games/Games;->API:Lcom/google/android/gms/common/api/Api;

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->addApi(Lcom/google/android/gms/common/api/Api;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    .line 296
    invoke-virtual {v0, p0}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->addConnectionCallbacks(Lcom/google/android/gms/common/api/GoogleApiClient$ConnectionCallbacks;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    .line 298
    invoke-static {p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->access$600(Lcom/google/games/bridge/TokenFragment$TokenRequest;)Z

    move-result p1

    if-eqz p1, :cond_8

    .line 299
    new-instance p1, Landroid/view/View;

    invoke-virtual {p0}, Lcom/google/games/bridge/TokenFragment;->getActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-direct {p1, v1}, Landroid/view/View;-><init>(Landroid/content/Context;)V

    const/4 v1, 0x4

    .line 300
    invoke-virtual {p1, v1}, Landroid/view/View;->setVisibility(I)V

    .line 301
    invoke-virtual {p1, v2}, Landroid/view/View;->setClickable(Z)V

    .line 302
    invoke-virtual {v0, p1}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->setViewForPopups(Landroid/view/View;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    .line 304
    :cond_8
    invoke-virtual {v0}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->build()Lcom/google/android/gms/common/api/GoogleApiClient;

    move-result-object p1

    iput-object p1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    .line 305
    iget-object p1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    const/4 v0, 0x2

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->connect(I)V

    return-void
.end method

.method public static fetchToken(Landroid/app/Activity;ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)Lcom/google/android/gms/common/api/PendingResult;
    .locals 11

    .line 101
    new-instance v10, Lcom/google/games/bridge/TokenFragment$TokenRequest;

    move-object v1, v10

    move v2, p1

    move v3, p2

    move v4, p3

    move-object v5, p4

    move/from16 v6, p5

    move-object/from16 v7, p6

    move/from16 v8, p7

    move-object/from16 v9, p8

    invoke-direct/range {v1 .. v9}, Lcom/google/games/bridge/TokenFragment$TokenRequest;-><init>(ZZZLjava/lang/String;Z[Ljava/lang/String;ZLjava/lang/String;)V

    .line 106
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v1

    .line 107
    :try_start_0
    sget-object v0, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    if-nez v0, :cond_0

    .line 108
    sput-object v10, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    .line 111
    :goto_0
    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    if-nez v0, :cond_1

    const-string v0, "TokenFragment"

    .line 113
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Already a pending token request (requested == ): "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const-string v0, "TokenFragment"

    .line 114
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Already a pending token request: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v2, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const/16 v0, 0xa

    .line 115
    invoke-virtual {v10, v0}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setResult(I)V

    .line 116
    invoke-virtual {v10}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getPendingResponse()Lcom/google/android/gms/common/api/PendingResult;

    move-result-object v0

    return-object v0

    .line 121
    :cond_1
    invoke-virtual {p0}, Landroid/app/Activity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v0

    const-string v1, "gpg.AuthTokenSupport"

    invoke-virtual {v0, v1}, Landroid/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroid/app/Fragment;

    move-result-object v0

    check-cast v0, Lcom/google/games/bridge/TokenFragment;

    if-nez v0, :cond_2

    :try_start_1
    const-string v0, "TokenFragment"

    const-string v1, "Creating fragment"

    .line 125
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 126
    new-instance v0, Lcom/google/games/bridge/TokenFragment;

    invoke-direct {v0}, Lcom/google/games/bridge/TokenFragment;-><init>()V

    .line 127
    invoke-virtual {p0}, Landroid/app/Activity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/FragmentManager;->beginTransaction()Landroid/app/FragmentTransaction;

    move-result-object v1

    const-string v2, "gpg.AuthTokenSupport"

    .line 128
    invoke-virtual {v1, v0, v2}, Landroid/app/FragmentTransaction;->add(Landroid/app/Fragment;Ljava/lang/String;)Landroid/app/FragmentTransaction;

    .line 129
    invoke-virtual {v1}, Landroid/app/FragmentTransaction;->commit()I
    :try_end_1
    .catch Ljava/lang/Throwable; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    move-exception v0

    const-string v1, "TokenFragment"

    .line 131
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Cannot launch token fragment:"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/Throwable;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    const/16 v0, 0xd

    .line 132
    invoke-virtual {v10, v0}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setResult(I)V

    .line 133
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v1

    const/4 v0, 0x0

    .line 134
    :try_start_2
    sput-object v0, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 135
    monitor-exit v1

    goto :goto_1

    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v0

    :cond_2
    const-string v1, "TokenFragment"

    const-string v2, "Fragment exists.. calling processRequests"

    .line 138
    invoke-static {v1, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 139
    invoke-direct {v0}, Lcom/google/games/bridge/TokenFragment;->processRequest()V

    .line 142
    :goto_1
    invoke-virtual {v10}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->getPendingResponse()Lcom/google/android/gms/common/api/PendingResult;

    move-result-object v0

    return-object v0

    :catchall_1
    move-exception v0

    .line 111
    :try_start_3
    monitor-exit v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    throw v0
.end method

.method private onSignedIn(ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V
    .locals 3

    .line 343
    sget-object v0, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v0

    .line 344
    :try_start_0
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    const/4 v2, 0x0

    .line 345
    sput-object v2, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 346
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v1, :cond_1

    if-eqz p2, :cond_0

    .line 349
    invoke-virtual {p2}, Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;->getServerAuthCode()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setAuthCode(Ljava/lang/String;)V

    .line 350
    invoke-virtual {p2}, Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;->getEmail()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setEmail(Ljava/lang/String;)V

    .line 351
    invoke-virtual {p2}, Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;->getIdToken()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setIdToken(Ljava/lang/String;)V

    .line 353
    :cond_0
    invoke-virtual {v1, p1}, Lcom/google/games/bridge/TokenFragment$TokenRequest;->setResult(I)V

    :cond_1
    return-void

    :catchall_0
    move-exception p1

    .line 346
    :try_start_1
    monitor-exit v0
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw p1
.end method

.method private processRequest()V
    .locals 2

    .line 188
    sget-object v0, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v0

    .line 189
    :try_start_0
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 190
    monitor-exit v0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    if-nez v1, :cond_0

    return-void

    .line 198
    :cond_0
    invoke-direct {p0, v1}, Lcom/google/games/bridge/TokenFragment;->buildClient(Lcom/google/games/bridge/TokenFragment$TokenRequest;)V

    .line 199
    sget-object v1, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter v1

    .line 200
    :try_start_1
    sget-object v0, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 201
    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v0, :cond_2

    .line 203
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    sget-object v1, Lcom/google/android/gms/games/Games;->API:Lcom/google/android/gms/common/api/Api;

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/GoogleApiClient;->hasConnectedApi(Lcom/google/android/gms/common/api/Api;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 205
    sget-object v0, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    iget-object v1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->silentSignIn(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/OptionalPendingResult;

    move-result-object v0

    new-instance v1, Lcom/google/games/bridge/TokenFragment$1;

    invoke-direct {v1, p0}, Lcom/google/games/bridge/TokenFragment$1;-><init>(Lcom/google/games/bridge/TokenFragment;)V

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/OptionalPendingResult;->setResultCallback(Lcom/google/android/gms/common/api/ResultCallback;)V

    goto :goto_0

    :cond_1
    const-string v0, "TokenFragment"

    const-string v1, "No connected Games API,!!!!  Hoping for connection!"

    .line 228
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    :cond_2
    :goto_0
    const-string v0, "TokenFragment"

    const-string v1, "Done with processRequest!"

    .line 232
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :catchall_0
    move-exception v0

    .line 201
    :try_start_2
    monitor-exit v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v0

    :catchall_1
    move-exception v1

    .line 190
    :try_start_3
    monitor-exit v0
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    throw v1
.end method

.method private reset()V
    .locals 4

    .line 161
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    if-eqz v0, :cond_1

    .line 162
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    sget-object v1, Lcom/google/android/gms/games/Games;->API:Lcom/google/android/gms/common/api/Api;

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/GoogleApiClient;->hasConnectedApi(Lcom/google/android/gms/common/api/Api;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 164
    :try_start_0
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v0}, Lcom/google/android/gms/games/Games;->signOut(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    :try_end_0
    .catch Ljava/lang/RuntimeException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "TokenFragment"

    .line 166
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Caught exception when calling Games.signOut: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 167
    invoke-virtual {v0}, Ljava/lang/RuntimeException;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 166
    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 170
    :goto_0
    :try_start_1
    sget-object v0, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    iget-object v1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v0, v1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->signOut(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;
    :try_end_1
    .catch Ljava/lang/RuntimeException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    :catch_1
    move-exception v0

    const-string v1, "TokenFragment"

    .line 172
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Caught exception when calling GoogleSignInAPI.signOut: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 173
    invoke-virtual {v0}, Ljava/lang/RuntimeException;->getMessage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 172
    invoke-static {v1, v2, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 176
    :cond_0
    :goto_1
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->disconnect()V

    const/4 v0, 0x0

    .line 177
    iput-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    :cond_1
    return-void
.end method

.method public static signOut(Landroid/app/Activity;)V
    .locals 1

    .line 148
    invoke-virtual {p0}, Landroid/app/Activity;->getFragmentManager()Landroid/app/FragmentManager;

    move-result-object p0

    const-string v0, "gpg.AuthTokenSupport"

    invoke-virtual {p0, v0}, Landroid/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroid/app/Fragment;

    move-result-object p0

    check-cast p0, Lcom/google/games/bridge/TokenFragment;

    if-eqz p0, :cond_0

    .line 150
    invoke-direct {p0}, Lcom/google/games/bridge/TokenFragment;->reset()V

    .line 152
    :cond_0
    sget-object p0, Lcom/google/games/bridge/TokenFragment;->lock:Ljava/lang/Object;

    monitor-enter p0

    const/4 v0, 0x0

    .line 153
    :try_start_0
    sput-object v0, Lcom/google/games/bridge/TokenFragment;->pendingTokenRequest:Lcom/google/games/bridge/TokenFragment$TokenRequest;

    .line 154
    monitor-exit p0

    return-void

    :catchall_0
    move-exception v0

    monitor-exit p0
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method


# virtual methods
.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 1

    const/16 v0, 0x232a

    if-ne p1, v0, :cond_2

    .line 325
    sget-object p1, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    .line 326
    invoke-interface {p1, p3}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->getSignInResultFromIntent(Landroid/content/Intent;)Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 327
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->isSuccess()Z

    move-result p2

    if-eqz p2, :cond_0

    .line 328
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getSignInAccount()Lcom/google/android/gms/auth/api/signin/GoogleSignInAccount;

    move-result-object p2

    .line 329
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result p1

    invoke-direct {p0, p1, p2}, Lcom/google/games/bridge/TokenFragment;->onSignedIn(ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    goto :goto_0

    :cond_0
    const/4 p2, 0x0

    if-eqz p1, :cond_1

    .line 331
    invoke-virtual {p1}, Lcom/google/android/gms/auth/api/signin/GoogleSignInResult;->getStatus()Lcom/google/android/gms/common/api/Status;

    move-result-object p1

    invoke-virtual {p1}, Lcom/google/android/gms/common/api/Status;->getStatusCode()I

    move-result p1

    invoke-direct {p0, p1, p2}, Lcom/google/games/bridge/TokenFragment;->onSignedIn(ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    goto :goto_0

    :cond_1
    const-string p1, "TokenFragment"

    const-string p3, "Google SignIn Result is null?"

    .line 333
    invoke-static {p1, p3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    const/16 p1, 0xd

    .line 334
    invoke-direct {p0, p1, p2}, Lcom/google/games/bridge/TokenFragment;->onSignedIn(ILcom/google/android/gms/auth/api/signin/GoogleSignInAccount;)V

    :goto_0
    return-void

    .line 338
    :cond_2
    invoke-super {p0, p1, p2, p3}, Landroid/app/Fragment;->onActivityResult(IILandroid/content/Intent;)V

    return-void
.end method

.method public onConnected(Landroid/os/Bundle;)V
    .locals 1
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    .line 398
    iget-object p1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    if-nez p1, :cond_0

    return-void

    .line 401
    :cond_0
    iget-object p1, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    sget-object v0, Lcom/google/android/gms/games/Games;->API:Lcom/google/android/gms/common/api/Api;

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->hasConnectedApi(Lcom/google/android/gms/common/api/Api;)Z

    move-result p1

    if-eqz p1, :cond_1

    .line 402
    sget-object p1, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {p1, v0}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->silentSignIn(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/OptionalPendingResult;

    move-result-object p1

    new-instance v0, Lcom/google/games/bridge/TokenFragment$2;

    invoke-direct {v0, p0}, Lcom/google/games/bridge/TokenFragment$2;-><init>(Lcom/google/games/bridge/TokenFragment;)V

    invoke-virtual {p1, v0}, Lcom/google/android/gms/common/api/OptionalPendingResult;->setResultCallback(Lcom/google/android/gms/common/api/ResultCallback;)V

    goto :goto_0

    .line 422
    :cond_1
    sget-object p1, Lcom/google/android/gms/auth/api/Auth;->GoogleSignInApi:Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;

    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {p1, v0}, Lcom/google/android/gms/auth/api/signin/GoogleSignInApi;->getSignInIntent(Lcom/google/android/gms/common/api/GoogleApiClient;)Landroid/content/Intent;

    move-result-object p1

    const/16 v0, 0x232a

    .line 423
    invoke-virtual {p0, p1, v0}, Lcom/google/games/bridge/TokenFragment;->startActivityForResult(Landroid/content/Intent;I)V

    :goto_0
    return-void
.end method

.method public onConnectionSuspended(I)V
    .locals 3

    const-string v0, "TokenFragment"

    .line 436
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onConnectionSuspended() called: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    return-void
.end method

.method public onResume()V
    .locals 2

    const-string v0, "TokenFragment"

    const-string v1, "onResume called"

    .line 388
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 389
    invoke-super {p0}, Landroid/app/Fragment;->onResume()V

    .line 390
    sget-object v0, Lcom/google/games/bridge/TokenFragment;->helperFragment:Lcom/google/games/bridge/TokenFragment;

    if-nez v0, :cond_0

    .line 391
    sput-object p0, Lcom/google/games/bridge/TokenFragment;->helperFragment:Lcom/google/games/bridge/TokenFragment;

    .line 393
    :cond_0
    invoke-direct {p0}, Lcom/google/games/bridge/TokenFragment;->processRequest()V

    return-void
.end method

.method public onStart()V
    .locals 2

    const-string v0, "TokenFragment"

    const-string v1, "onStart()"

    .line 360
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 361
    invoke-super {p0}, Landroid/app/Fragment;->onStart()V

    .line 366
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    if-eqz v0, :cond_0

    .line 367
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    const/4 v1, 0x2

    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/GoogleApiClient;->connect(I)V

    :cond_0
    return-void
.end method

.method public onStop()V
    .locals 2

    const-string v0, "TokenFragment"

    const-string v1, "onStop()"

    .line 373
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 374
    invoke-super {p0}, Landroid/app/Fragment;->onStop()V

    .line 375
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->isConnected()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 376
    iget-object v0, p0, Lcom/google/games/bridge/TokenFragment;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-virtual {v0}, Lcom/google/android/gms/common/api/GoogleApiClient;->disconnect()V

    :cond_0
    return-void
.end method
