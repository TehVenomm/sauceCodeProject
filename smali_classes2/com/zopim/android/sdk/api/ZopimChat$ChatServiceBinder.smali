.class public Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;
.super Landroidx/fragment/app/Fragment;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/api/ZopimChat;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "ChatServiceBinder"
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ZopimChat$ChatServiceBinder"


# instance fields
.field private mBound:Z

.field private mConnection:Landroid/content/ServiceConnection;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/api/ac;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/ac;-><init>(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mConnection:Landroid/content/ServiceConnection;

    return-void
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->unbind()V

    return-void
.end method

.method static synthetic access$2102(Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;Z)Z
    .locals 0

    iput-boolean p1, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mBound:Z

    return p1
.end method

.method static synthetic access$2200()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method private bind()V
    .locals 4

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    if-eqz v0, :cond_1

    new-instance v0, Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    const-class v2, Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {v0, v1, v2}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getArguments()Landroid/os/Bundle;

    move-result-object v1

    const-string v2, "ACCOUNT_KEY"

    invoke-virtual {v1, v2}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getArguments()Landroid/os/Bundle;

    move-result-object v2

    const-string v3, "MACHINE_ID"

    invoke-virtual {v2, v3}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-eqz v1, :cond_0

    if-eqz v2, :cond_0

    const-string v3, "ACCOUNT_KEY"

    invoke-virtual {v0, v3, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    const-string v1, "MACHINE_ID"

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mConnection:Landroid/content/ServiceConnection;

    const/4 v3, 0x1

    invoke-virtual {v1, v0, v2, v3}, Landroidx/fragment/app/FragmentActivity;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Binding chat service with activity "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    return-void
.end method

.method private unbind()V
    .locals 3

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mBound:Z

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mConnection:Landroid/content/ServiceConnection;

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->unbindService(Landroid/content/ServiceConnection;)V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mBound:Z

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unbinding chat service from activity "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method


# virtual methods
.method protected finalize()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Service binder cleared from memory by GC"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-super {p0}, Ljava/lang/Object;->finalize()V

    return-void
.end method

.method public isBound()Z
    .locals 1

    iget-boolean v0, p0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->mBound:Z

    return v0
.end method

.method public onAttach(Landroid/app/Activity;)V
    .locals 3

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Attached to "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onDestroy()V
    .locals 3

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onDestroy()V

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "On host activity destroy "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onDetach()V
    .locals 3

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onDetach()V

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Detached from "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    return-void
.end method

.method public onPause()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onPause()V

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Host activity pause"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->unbind()V

    return-void
.end method

.method public onResume()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onResume()V

    sget-object v0, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Host activity resume"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->hasEnded()Z

    move-result v0

    if-nez v0, :cond_0

    invoke-direct {p0}, Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;->bind()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1500()Lcom/zopim/android/sdk/api/ZopimChat;

    move-result-object v0

    invoke-static {v0, p0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$1702(Lcom/zopim/android/sdk/api/ZopimChat;Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;)Lcom/zopim/android/sdk/api/ZopimChat$ChatServiceBinder;

    :cond_0
    return-void
.end method
