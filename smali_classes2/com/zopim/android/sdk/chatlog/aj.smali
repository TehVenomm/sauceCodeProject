.class Lcom/zopim/android/sdk/chatlog/aj;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    new-instance v1, Landroid/app/AlertDialog$Builder;

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/aj;->a:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-direct {v1, v2}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    sget v2, Lcom/zopim/android/sdk/R$string;->reconnect_timeout_title:I

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->reconnect_timeout_message:I

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->reconnect_timeout_confirm_button:I

    new-instance v3, Lcom/zopim/android/sdk/chatlog/al;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/chatlog/al;-><init>(Lcom/zopim/android/sdk/chatlog/aj;)V

    invoke-virtual {v1, v2, v3}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->reconnect_timeout_cancel_button:I

    new-instance v3, Lcom/zopim/android/sdk/chatlog/ak;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/chatlog/ak;-><init>(Lcom/zopim/android/sdk/chatlog/aj;)V

    invoke-virtual {v1, v2, v3}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->access$1102(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/app/AlertDialog;)Landroid/app/AlertDialog;

    return-void
.end method
