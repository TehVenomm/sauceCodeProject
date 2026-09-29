.class Lcom/zopim/android/sdk/prechat/j;
.super Landroid/content/BroadcastReceiver;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/j;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 2

    new-instance p1, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;-><init>()V

    iget-object p2, p0, Lcom/zopim/android/sdk/prechat/j;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p2

    invoke-virtual {p2}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v1, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p2, v0, p1, v1}, Landroidx/fragment/app/FragmentTransaction;->replace(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/j;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {p2, p1}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {p2}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    return-void
.end method
