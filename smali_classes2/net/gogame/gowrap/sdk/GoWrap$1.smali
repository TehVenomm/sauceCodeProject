.class final Lnet/gogame/gowrap/sdk/GoWrap$1;
.super Ljava/lang/Object;
.source "GoWrap.java"

# interfaces
.implements Lnet/gogame/gowrap/GoWrapDelegateV2;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/gowrap/sdk/GoWrap;->setDelegate(Lnet/gogame/gowrap/sdk/GoWrapDelegate;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;


# direct methods
.method constructor <init>(Lnet/gogame/gowrap/sdk/GoWrapDelegate;)V
    .locals 0

    .line 65
    iput-object p1, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public didCompleteRewardedAd(Ljava/lang/String;I)V
    .locals 1

    .line 106
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    invoke-interface {v0, p1, p2}, Lnet/gogame/gowrap/sdk/GoWrapDelegate;->didCompleteRewardedAd(Ljava/lang/String;I)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "goWrap"

    const-string v0, "Exception"

    .line 108
    invoke-static {p2, v0, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :goto_0
    return-void
.end method

.method public onCustomUrl(Ljava/lang/String;)V
    .locals 2

    .line 94
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 95
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    .line 96
    invoke-interface {v0, p1}, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;->onCustomUrl(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string v0, "goWrap"

    const-string v1, "Exception"

    .line 99
    invoke-static {v0, v1, p1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onMenuClosed()V
    .locals 3

    .line 82
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 83
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    .line 84
    invoke-interface {v0}, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;->onMenuClosed()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 87
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onMenuOpened()V
    .locals 3

    .line 70
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 71
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    .line 72
    invoke-interface {v0}, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;->onMenuOpened()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 75
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method

.method public onOffersAvailable()V
    .locals 3

    .line 115
    :try_start_0
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    instance-of v0, v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    if-eqz v0, :cond_0

    .line 116
    iget-object v0, p0, Lnet/gogame/gowrap/sdk/GoWrap$1;->val$delegate:Lnet/gogame/gowrap/sdk/GoWrapDelegate;

    check-cast v0, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;

    .line 117
    invoke-interface {v0}, Lnet/gogame/gowrap/sdk/GoWrapDelegateV2;->onOffersAvailable()V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v0

    const-string v1, "goWrap"

    const-string v2, "Exception"

    .line 120
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    :cond_0
    :goto_0
    return-void
.end method
