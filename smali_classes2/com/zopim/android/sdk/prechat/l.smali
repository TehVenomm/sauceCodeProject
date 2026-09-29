.class Lcom/zopim/android/sdk/prechat/l;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/l;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/l;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$000(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/view/View;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/l;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    new-instance v1, Landroid/app/AlertDialog$Builder;

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/l;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-direct {v1, v2}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    sget v2, Lcom/zopim/android/sdk/R$string;->offline_message_send_failed:I

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->offline_message_retry_button:I

    new-instance v3, Lcom/zopim/android/sdk/prechat/n;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/prechat/n;-><init>(Lcom/zopim/android/sdk/prechat/l;)V

    invoke-virtual {v1, v2, v3}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->offline_message_cancel_button:I

    new-instance v3, Lcom/zopim/android/sdk/prechat/m;

    invoke-direct {v3, p0}, Lcom/zopim/android/sdk/prechat/m;-><init>(Lcom/zopim/android/sdk/prechat/l;)V

    invoke-virtual {v1, v2, v3}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    move-result-object v1

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$102(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;Landroid/app/AlertDialog;)Landroid/app/AlertDialog;

    return-void
.end method
