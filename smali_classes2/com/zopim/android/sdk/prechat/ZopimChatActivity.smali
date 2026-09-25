.class public Lcom/zopim/android/sdk/prechat/ZopimChatActivity;
.super Landroidx/appcompat/app/AppCompatActivity;

# interfaces
.implements Lcom/zopim/android/sdk/prechat/ChatListener;


# static fields
.field private static final EXTRA_CHAT_CONFIG:Ljava/lang/String; = "CHAT_CONFIG"

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimChatActivity"

.field private static final STATE_CHAT_INITIALIZED:Ljava/lang/String; = "CHAT_INITIALIZED"


# instance fields
.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field private mChatInitialized:Z


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Landroidx/appcompat/app/AppCompatActivity;-><init>()V

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    return-void
.end method

.method private resumeChat()V
    .locals 4

    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Resuming chat"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-static {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    xor-int/lit8 v0, v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-class v1, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v1

    if-nez v1, :cond_0

    new-instance v1, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {v1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;-><init>()V

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    sget v2, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v1, v3}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_0
    return-void
.end method

.method public static startActivity(Landroid/content/Context;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)V
    .locals 2

    new-instance v0, Landroid/content/Intent;

    const-class v1, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "CHAT_CONFIG"

    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    invoke-virtual {p0, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return-void
.end method


# virtual methods
.method public onChatEnded()V
    .locals 0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->finish()V

    return-void
.end method

.method public onChatInitialized()V
    .locals 1

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    return-void
.end method

.method public onChatLoaded(Lcom/zopim/android/sdk/api/Chat;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    return-void
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 3

    invoke-super {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;->onCreate(Landroid/os/Bundle;)V

    sget v0, Lcom/zopim/android/sdk/R$layout;->zopim_chat_activity:I

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->setContentView(I)V

    sget v0, Lcom/zopim/android/sdk/R$id;->toolbar:I

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroidx/appcompat/widget/Toolbar;

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->setSupportActionBar(Landroidx/appcompat/widget/Toolbar;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getSupportActionBar()Landroidx/appcompat/app/ActionBar;

    move-result-object v0

    const/4 v1, 0x1

    invoke-virtual {v0, v1}, Landroidx/appcompat/app/ActionBar;->setDisplayHomeAsUpEnabled(Z)V

    if-eqz p1, :cond_0

    const-string v0, "CHAT_INITIALIZED"

    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    iput-boolean p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    invoke-static {p0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    return-void

    :cond_0
    new-instance p1, Landroid/content/Intent;

    const-class v0, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {p1, p0, v0}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {p0, p1}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->stopService(Landroid/content/Intent;)Z

    move-result p1

    if-eqz p1, :cond_1

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->resumeChat()V

    return-void

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    if-eqz p1, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getIntent()Landroid/content/Intent;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object p1

    const-string v0, "zopim.action.RESUME_CHAT"

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->resumeChat()V

    return-void

    :cond_2
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object p1

    const-class v0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v0}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-nez v0, :cond_5

    const/4 v0, 0x0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    if-eqz v1, :cond_3

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    const-string v2, "CHAT_CONFIG"

    invoke-virtual {v1, v2}, Landroid/content/Intent;->hasExtra(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_3

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    const-string v1, "CHAT_CONFIG"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->getSerializableExtra(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    check-cast v0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    :cond_3
    if-eqz v0, :cond_4

    invoke-static {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;->newInstance(Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    move-result-object v0

    goto :goto_0

    :cond_4
    new-instance v0, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;-><init>()V

    :goto_0
    invoke-virtual {p1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object p1

    sget v1, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v2, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v2}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v1, v0, v2}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {p1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_5
    return-void
.end method

.method protected onDestroy()V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Activity destroyed"

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-super {p0}, Landroidx/appcompat/app/AppCompatActivity;->onDestroy()V

    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->finish()V

    invoke-super {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    :cond_0
    const/4 p1, 0x0

    return p1
.end method

.method protected onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/appcompat/app/AppCompatActivity;->onSaveInstanceState(Landroid/os/Bundle;)V

    const-string v0, "CHAT_INITIALIZED"

    iget-boolean v1, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method protected onStart()V
    .locals 2

    invoke-super {p0}, Landroidx/appcompat/app/AppCompatActivity;->onStart()V

    new-instance v0, Landroid/content/Intent;

    const-class v1, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->stopService(Landroid/content/Intent;)Z

    return-void
.end method

.method protected onStop()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0xb
    .end annotation

    invoke-super {p0}, Landroidx/appcompat/app/AppCompatActivity;->onStop()V

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0xb

    if-lt v0, v1, :cond_1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->isFinishing()Z

    move-result v0

    :goto_0
    if-eqz v0, :cond_3

    iget-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChatInitialized:Z

    if-nez v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat initialization aborted. Ending chat."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    :goto_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->finish()V

    return-void

    :cond_2
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->getSupportFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    const-class v1, Lcom/zopim/android/sdk/prechat/ZopimChatFragment;

    invoke-virtual {v1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentManager;->findFragmentByTag(Ljava/lang/String;)Landroidx/fragment/app/Fragment;

    move-result-object v0

    if-eqz v0, :cond_3

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimChatActivity;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    goto :goto_1

    :cond_3
    return-void
.end method
