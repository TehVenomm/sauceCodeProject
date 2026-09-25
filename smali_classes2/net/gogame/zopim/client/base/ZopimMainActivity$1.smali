.class Lnet/gogame/zopim/client/base/ZopimMainActivity$1;
.super Ljava/lang/Object;
.source "ZopimMainActivity.java"

# interfaces
.implements Lnet/gogame/chat/UIContext;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/zopim/client/base/ZopimMainActivity;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;


# direct methods
.method constructor <init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V
    .locals 0

    .line 39
    iput-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)V
    .locals 1

    .line 59
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-virtual {v0, p1, p2}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method public showImage(Ljava/lang/String;)V
    .locals 3

    if-nez p1, :cond_0

    return-void

    .line 47
    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "uri"

    .line 48
    invoke-virtual {v0, v1, p1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    .line 50
    new-instance p1, Lnet/gogame/chat/ImageViewFragment;

    invoke-direct {p1}, Lnet/gogame/chat/ImageViewFragment;-><init>()V

    .line 51
    invoke-virtual {p1, v0}, Lnet/gogame/chat/ImageViewFragment;->setArguments(Landroid/os/Bundle;)V

    .line 52
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    sget v1, Lnet/gogame/chat/Constants;->FRAGMENT_CONTAINER:I

    const-string v2, "IMAGE_SHOW_FRAGMENT"

    invoke-virtual {v0, p1, v1, v2}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->replaceFragment(Landroidx/fragment/app/Fragment;ILjava/lang/String;)V

    return-void
.end method

.method public unregisterReceiver(Landroid/content/BroadcastReceiver;)V
    .locals 1

    .line 64
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-virtual {v0, p1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    return-void
.end method
