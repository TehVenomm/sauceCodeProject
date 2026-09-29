.class Lnet/gogame/zopim/client/base/ZopimMainActivity$3;
.super Ljava/lang/Object;
.source "ZopimMainActivity.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/zopim/client/base/ZopimMainActivity;->onCreate(Landroid/os/Bundle;)V
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

    .line 124
    iput-object p1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$3;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 128
    new-instance p1, Landroid/app/AlertDialog$Builder;

    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity$3;->this$0:Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {p1, v0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    sget v0, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_title:I

    .line 129
    invoke-virtual {p1, v0}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    sget v0, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_vip_only_message:I

    .line 130
    invoke-virtual {p1, v0}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object p1

    .line 131
    invoke-virtual {p1}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    return-void
.end method
