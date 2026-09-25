.class Lcom/zopim/android/sdk/prechat/b;
.super Ljava/lang/Object;

# interfaces
.implements Landroid/view/View$OnClickListener;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/b;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 2

    new-instance p1, Landroid/content/Intent;

    invoke-direct {p1}, Landroid/content/Intent;-><init>()V

    const-string v0, "zopim.action.CREATE_REQUEST"

    invoke-virtual {p1, v0}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/b;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroidx/fragment/app/FragmentActivity;->sendOrderedBroadcast(Landroid/content/Intent;Ljava/lang/String;)V

    return-void
.end method
