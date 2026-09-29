.class Lcom/zopim/android/sdk/prechat/p;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/o;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/o;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$600(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/os/Handler;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v1, v1, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    iget-object v1, v1, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mShowSendTimeoutDialog:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$000(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/view/View;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->offline_sent_confirmation_message:I

    const/4 v2, 0x1

    invoke-static {v0, v1, v2}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$200(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$300(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/p;->a:Lcom/zopim/android/sdk/prechat/o;

    iget-object v0, v0, Lcom/zopim/android/sdk/prechat/o;->a:Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->access$400(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    return-void
.end method
