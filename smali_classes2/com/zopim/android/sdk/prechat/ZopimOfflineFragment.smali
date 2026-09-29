.class public Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;
.super Landroidx/fragment/app/Fragment;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ZopimOfflineFragment"

.field public static final STATE_MENU_ITEM_ENABLED:Ljava/lang/String; = "MENU_ITEM_ENABLED"

.field private static final STATE_PROGRESS_VISIBITLITY:Ljava/lang/String; = "PROGRESS_VISIBILITY"


# instance fields
.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field private mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

.field private mEmailEdit:Landroid/widget/EditText;

.field mFormsObserver:Lcom/zopim/android/sdk/data/observers/FormsObserver;

.field private mHandler:Landroid/os/Handler;

.field private mMenu:Landroid/view/Menu;

.field private mMessageEdit:Landroid/widget/EditText;

.field private mNameEdit:Landroid/widget/EditText;

.field private mProgressBar:Landroid/view/View;

.field private mSendTimeoutDialog:Landroid/app/AlertDialog;

.field mShowSendTimeoutDialog:Ljava/lang/Runnable;

.field private mStateMenuItemEnabled:Z

.field private mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mStateMenuItemEnabled:Z

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mHandler:Landroid/os/Handler;

    new-instance v0, Lcom/zopim/android/sdk/prechat/l;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/l;-><init>(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mShowSendTimeoutDialog:Ljava/lang/Runnable;

    new-instance v0, Lcom/zopim/android/sdk/prechat/o;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/prechat/o;-><init>(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mFormsObserver:Lcom/zopim/android/sdk/data/observers/FormsObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/view/View;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mProgressBar:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$102(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;Landroid/app/AlertDialog;)Landroid/app/AlertDialog;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mSendTimeoutDialog:Landroid/app/AlertDialog;

    return-object p1
.end method

.method static synthetic access$200(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Lcom/zopim/android/sdk/api/Chat;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    return-object p0
.end method

.method static synthetic access$300(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->close()V

    return-void
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    return-object p0
.end method

.method static synthetic access$500(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->sendOfflineMessage()V

    return-void
.end method

.method static synthetic access$600(Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mHandler:Landroid/os/Handler;

    return-object p0
.end method

.method private close()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    return-void
.end method

.method private sendOfflineMessage()V
    .locals 8

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getVisibility()I

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v3

    if-eqz v3, :cond_0

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->offline_name_error_message:I

    invoke-virtual {v4, v5}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    sget v4, Lcom/zopim/android/sdk/R$string;->offline_name_error_hint:I

    invoke-virtual {v3, v4}, Landroid/widget/EditText;->setHint(I)V

    move-object v3, v0

    const/4 v0, 0x0

    goto :goto_1

    :cond_0
    :goto_0
    move-object v3, v0

    const/4 v0, 0x1

    goto :goto_1

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v0

    goto :goto_0

    :goto_1
    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v4}, Landroid/widget/EditText;->getVisibility()I

    move-result v4

    if-nez v4, :cond_2

    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v4}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v4

    sget-object v5, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {v5, v4}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v5

    invoke-virtual {v5}, Ljava/util/regex/Matcher;->matches()Z

    move-result v5

    if-nez v5, :cond_3

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    sget v6, Lcom/zopim/android/sdk/R$string;->offline_email_error_message:I

    invoke-virtual {v5, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v5}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    sget v5, Lcom/zopim/android/sdk/R$string;->offline_email_error_hint:I

    invoke-virtual {v0, v5}, Landroid/widget/EditText;->setHint(I)V

    const/4 v0, 0x0

    goto :goto_2

    :cond_2
    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v4

    :cond_3
    :goto_2
    iget-object v5, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v5}, Landroid/widget/EditText;->getVisibility()I

    move-result v5

    if-nez v5, :cond_4

    iget-object v5, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v5}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->isEmpty()Z

    move-result v6

    if-eqz v6, :cond_5

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    sget v7, Lcom/zopim/android/sdk/R$string;->offline_message_error_message:I

    invoke-virtual {v6, v7}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v0, v6}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    sget v6, Lcom/zopim/android/sdk/R$string;->offline_message_error_hint:I

    invoke-virtual {v0, v6}, Landroid/widget/EditText;->setHint(I)V

    const/4 v0, 0x0

    goto :goto_3

    :cond_4
    const/4 v5, 0x0

    :cond_5
    :goto_3
    if-eqz v0, :cond_7

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0, v3, v4, v5}, Lcom/zopim/android/sdk/api/Chat;->sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-nez v0, :cond_6

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mShowSendTimeoutDialog:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    goto :goto_4

    :cond_6
    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mProgressBar:Landroid/view/View;

    invoke-virtual {v0, v2}, Landroid/view/View;->setVisibility(I)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mShowSendTimeoutDialog:Ljava/lang/Runnable;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getInitializationTimeout()Ljava/lang/Long;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v2

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    goto :goto_4

    :cond_7
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    sget v2, Lcom/zopim/android/sdk/R$string;->offline_validation_error_message:I

    invoke-static {v0, v2, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v0

    invoke-virtual {v0}, Landroid/widget/Toast;->show()V

    :goto_4
    return-void
.end method

.method private setViewVisibility(Landroid/view/View;I)V
    .locals 1

    if-nez p1, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->LOG_TAG:Ljava/lang/String;

    const-string p2, "View must not be null. Can not apply visibility change"

    invoke-static {p1, p2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    if-eqz p2, :cond_2

    const/4 v0, 0x4

    if-eq p2, v0, :cond_1

    const/16 v0, 0x8

    if-eq p2, v0, :cond_1

    goto :goto_0

    :cond_1
    invoke-virtual {p1, v0}, Landroid/view/View;->setVisibility(I)V

    goto :goto_0

    :cond_2
    const/4 p2, 0x0

    invoke-virtual {p1, p2}, Landroid/view/View;->setVisibility(I)V

    :goto_0
    return-void
.end method


# virtual methods
.method public onAttach(Landroid/app/Activity;)V
    .locals 1

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    :cond_0
    return-void
.end method

.method public onConnected()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->start_chat:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    :cond_0
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 4

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    const/4 v0, 0x1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->setHasOptionsMenu(Z)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/ChatConfig;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    :cond_0
    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    if-nez p1, :cond_1

    new-instance p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;-><init>()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$id;->toast_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, p1, v3}, Landroidx/fragment/app/FragmentTransaction;->add(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    const-class p1, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-virtual {p1}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v1, v0, p1}, Landroidx/fragment/app/FragmentTransaction;->add(Landroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    :cond_1
    return-void
.end method

.method public onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V
    .locals 1

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V

    sget v0, Lcom/zopim/android/sdk/R$menu;->chat_offline_message_menu:I

    invoke-virtual {p2, v0, p1}, Landroid/view/MenuInflater;->inflate(ILandroid/view/Menu;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->send:I

    invoke-interface {p1, p2}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object p2

    iget-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mStateMenuItemEnabled:Z

    invoke-interface {p2, v0}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 1
    .param p2    # Landroid/view/ViewGroup;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_offline_message_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDisconnected()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->start_chat:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v1

    if-nez v1, :cond_0

    const/4 v1, 0x1

    invoke-interface {v0, v1}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    :cond_0
    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->close()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-interface {v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    :cond_0
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    :cond_1
    sget v0, Lcom/zopim/android/sdk/R$id;->send:I

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v1

    if-ne v0, v1, :cond_2

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->sendOfflineMessage()V

    const/4 p1, 0x1

    return p1

    :cond_2
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->send:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    const-string v1, "MENU_ITEM_ENABLED"

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v0

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "PROGRESS_VISIBILITY"

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mProgressBar:Landroid/view/View;

    invoke-virtual {v1}, Landroid/view/View;->getVisibility()I

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    return-void
.end method

.method public onStart()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mFormsObserver:Lcom/zopim/android/sdk/data/observers/FormsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addFormsObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mFormsObserver:Lcom/zopim/android/sdk/data/observers/FormsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteFormsObserver(Ljava/util/Observer;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 4
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->name:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    sget p2, Lcom/zopim/android/sdk/R$id;->email:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    sget p2, Lcom/zopim/android/sdk/R$id;->message:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    iput-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    sget p2, Lcom/zopim/android/sdk/R$id;->progress:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mProgressBar:Landroid/view/View;

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$string;->required_field_template:I

    invoke-virtual {p2, v0}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p2

    const/4 v0, 0x1

    new-array v1, v0, [Ljava/lang/Object;

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getHint()Ljava/lang/CharSequence;

    move-result-object v2

    const/4 v3, 0x0

    aput-object v2, v1, v3

    invoke-static {p2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setHint(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v1, Lcom/zopim/android/sdk/R$string;->required_field_template:I

    invoke-virtual {p2, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p2

    new-array v1, v0, [Ljava/lang/Object;

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getHint()Ljava/lang/CharSequence;

    move-result-object v2

    aput-object v2, v1, v3

    invoke-static {p2, v1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setHint(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    sget v1, Lcom/zopim/android/sdk/R$string;->required_field_template:I

    invoke-virtual {p2, v1}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p2

    new-array v0, v0, [Ljava/lang/Object;

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getHint()Ljava/lang/CharSequence;

    move-result-object v1

    aput-object v1, v0, v3

    invoke-static {p2, v0}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setHint(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object p1

    const/16 p2, 0x8

    if-eqz p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_0

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setVisibility(I)V

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mVisitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->setVisibility(I)V

    :cond_1
    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 2
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onViewStateRestored(Landroid/os/Bundle;)V

    if-eqz p1, :cond_0

    const-string v0, "MENU_ITEM_ENABLED"

    const/4 v1, 0x1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mStateMenuItemEnabled:Z

    const-string v0, "PROGRESS_VISIBILITY"

    const/16 v1, 0x8

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->getInt(Ljava/lang/String;I)I

    move-result p1

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->mProgressBar:Landroid/view/View;

    invoke-direct {p0, v0, p1}, Lcom/zopim/android/sdk/prechat/ZopimOfflineFragment;->setViewVisibility(Landroid/view/View;I)V

    :cond_0
    return-void
.end method
