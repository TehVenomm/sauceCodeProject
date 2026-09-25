.class Lcom/zopim/android/sdk/prechat/d;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/lang/Runnable;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$000(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Landroid/view/View;

    move-result-object v0

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$100(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->access$200(Lcom/zopim/android/sdk/prechat/ZopimChatFragment;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getPreChatForm()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->newInstance(Lcom/zopim/android/sdk/prechat/PreChatForm;)Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    :goto_0
    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v0, v3}, Landroidx/fragment/app/FragmentTransaction;->replace(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v1, v0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    goto :goto_1

    :cond_0
    new-instance v0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;-><init>()V

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/d;->a:Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v1}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    goto :goto_0

    :goto_1
    return-void
.end method
