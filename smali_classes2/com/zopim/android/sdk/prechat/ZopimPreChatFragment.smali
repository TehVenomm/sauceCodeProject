.class public Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;
.super Landroidx/fragment/app/Fragment;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;


# static fields
.field private static final EXTRA_PRE_CHAT_CONFIG:Ljava/lang/String; = "PRE_CHAT_CONFIG"

.field private static final LOG_TAG:Ljava/lang/String; = "ZopimPreChatFragment"

.field private static final STATE_MENU_ITEM_ENABLED:Ljava/lang/String; = "MENU_ITEM_ENABLED"


# instance fields
.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field private mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

.field private mDepartmentSpinner:Landroid/widget/Spinner;

.field private mEmailEdit:Landroid/widget/EditText;

.field private mHandler:Landroid/os/Handler;

.field private mMenu:Landroid/view/Menu;

.field private mMessageEdit:Landroid/widget/EditText;

.field private mNameEdit:Landroid/widget/EditText;

.field private mPhoneNumberEdit:Landroid/widget/EditText;

.field private mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

.field private mStateMenuItemEnabled:Z


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mStateMenuItemEnabled:Z

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mHandler:Landroid/os/Handler;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;)Landroid/widget/Spinner;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    return-object p0
.end method

.method private close()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    return-void
.end method

.method public static newInstance(Lcom/zopim/android/sdk/prechat/PreChatForm;)Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;
    .locals 2

    if-nez p0, :cond_0

    sget-object p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Pre chat form must not be null. Will use default pre chat form."

    invoke-static {p0, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    new-instance p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;-><init>()V

    return-object p0

    :cond_0
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "PRE_CHAT_CONFIG"

    invoke-virtual {v0, v1, p0}, Landroid/os/Bundle;->putSerializable(Ljava/lang/String;Ljava/io/Serializable;)V

    new-instance p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;-><init>()V

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setArguments(Landroid/os/Bundle;)V

    return-object p0
.end method

.method private safeIsEmpty(Ljava/lang/String;)Z
    .locals 0

    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-eqz p1, :cond_0

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 p1, 0x1

    :goto_1
    return p1
.end method

.method private setupVisitorField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Landroid/widget/EditText;Ljava/lang/String;)V
    .locals 2

    sget-object v0, Lcom/zopim/android/sdk/prechat/s;->a:[I

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->ordinal()I

    move-result p1

    aget p1, v0, p1

    const/16 v0, 0x8

    packed-switch p1, :pswitch_data_0

    sget-object p1, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string p2, "Unknown pre chat forn config type."

    invoke-static {p1, p2}, Lcom/zopim/android/sdk/api/Logger;->w(Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_1

    :pswitch_0
    invoke-direct {p0, p3}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->safeIsEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_0

    invoke-virtual {p2, p3}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    :cond_0
    :pswitch_1
    invoke-direct {p0, p3}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->safeIsEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_1

    goto :goto_0

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget p3, Lcom/zopim/android/sdk/R$string;->required_field_template:I

    invoke-virtual {p1, p3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    const/4 p3, 0x1

    new-array p3, p3, [Ljava/lang/Object;

    const/4 v0, 0x0

    invoke-virtual {p2}, Landroid/widget/EditText;->getHint()Ljava/lang/CharSequence;

    move-result-object v1

    aput-object v1, p3, v0

    invoke-static {p1, p3}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p2, p1}, Landroid/widget/EditText;->setHint(Ljava/lang/CharSequence;)V

    goto :goto_1

    :pswitch_2
    invoke-direct {p0, p3}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->safeIsEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_2

    invoke-virtual {p2, p3}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    goto :goto_1

    :pswitch_3
    invoke-direct {p0, p3}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->safeIsEmpty(Ljava/lang/String;)Z

    move-result p1

    if-nez p1, :cond_2

    :goto_0
    :pswitch_4
    invoke-virtual {p2, v0}, Landroid/widget/EditText;->setVisibility(I)V

    :cond_2
    :goto_1
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_4
        :pswitch_3
        :pswitch_2
        :pswitch_0
        :pswitch_1
    .end packed-switch
.end method


# virtual methods
.method public onAttach(Landroid/app/Activity;)V
    .locals 1

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    :cond_0
    return-void
.end method

.method public onConnected()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

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

.method public onCreate(Landroid/os/Bundle;)V
    .locals 4

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    const/4 v0, 0x1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setHasOptionsMenu(Z)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getArguments()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "PRE_CHAT_CONFIG"

    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getSerializable(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v0

    instance-of v1, v0, Lcom/zopim/android/sdk/prechat/PreChatForm;

    if-eqz v1, :cond_0

    check-cast v0, Lcom/zopim/android/sdk/prechat/PreChatForm;

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    :cond_0
    if-nez p1, :cond_1

    new-instance p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;-><init>()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

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

    sget v0, Lcom/zopim/android/sdk/R$menu;->pre_chat_menu:I

    invoke-virtual {p2, v0, p1}, Landroid/view/MenuInflater;->inflate(ILandroid/view/Menu;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->start_chat:I

    invoke-interface {p1, p2}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object p2

    iget-boolean v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mStateMenuItemEnabled:Z

    invoke-interface {p2, v0}, Landroid/view/MenuItem;->setEnabled(Z)Landroid/view/MenuItem;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

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

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_pre_chat_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    return-object p1
.end method

.method public onDisconnected()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

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

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 6

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_0

    invoke-direct {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->close()V

    :cond_0
    sget v0, Lcom/zopim/android/sdk/R$id;->start_chat:I

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v1

    if-ne v0, v1, :cond_10

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {p1}, Landroid/widget/Spinner;->getVisibility()I

    move-result p1

    const/4 v0, 0x1

    const/4 v1, 0x0

    if-nez p1, :cond_2

    sget-object p1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v2

    invoke-virtual {p1, v2}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-nez p1, :cond_1

    sget-object p1, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v2}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v2

    invoke-virtual {p1, v2}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_2

    :cond_1
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {p1}, Landroid/widget/Spinner;->getSelectedItemPosition()I

    move-result p1

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {v2}, Landroid/widget/Spinner;->getCount()I

    move-result v2

    sub-int/2addr v2, v0

    if-le p1, v2, :cond_2

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {p1}, Landroid/widget/Spinner;->getSelectedView()Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/TextView;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_departments_error_message:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getText(I)Ljava/lang/CharSequence;

    move-result-object v2

    invoke-virtual {p1, v2}, Landroid/widget/TextView;->setError(Ljava/lang/CharSequence;)V

    sget v2, Lcom/zopim/android/sdk/R$string;->pre_chat_departments_error_hint:I

    invoke-virtual {p1, v2}, Landroid/widget/TextView;->setText(I)V

    const/4 p1, 0x0

    goto :goto_0

    :cond_2
    const/4 p1, 0x1

    :goto_0
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getVisibility()I

    move-result v2

    if-nez v2, :cond_3

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    sget-object v3, Lcom/zopim/android/sdk/prechat/s;->a:[I

    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getEmail()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->ordinal()I

    move-result v4

    aget v3, v3, v4

    packed-switch v3, :pswitch_data_0

    goto :goto_2

    :pswitch_0
    sget-object v3, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/regex/Matcher;->matches()Z

    move-result v2

    if-nez v2, :cond_3

    :goto_1
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_email_error_message:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    sget v2, Lcom/zopim/android/sdk/R$string;->pre_chat_email_error_hint:I

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setHint(I)V

    const/4 p1, 0x0

    goto :goto_2

    :pswitch_1
    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_3

    sget-object v3, Landroid/util/Patterns;->EMAIL_ADDRESS:Ljava/util/regex/Pattern;

    invoke-virtual {v3, v2}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/regex/Matcher;->matches()Z

    move-result v2

    if-nez v2, :cond_3

    goto :goto_1

    :cond_3
    :goto_2
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getVisibility()I

    move-result v2

    if-nez v2, :cond_5

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getName()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_4

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getName()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_5

    :cond_4
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_5

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_name_error_message:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    sget v2, Lcom/zopim/android/sdk/R$string;->pre_chat_name_error_hint:I

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setHint(I)V

    const/4 p1, 0x0

    :cond_5
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getVisibility()I

    move-result v2

    if-nez v2, :cond_7

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getPhoneNumber()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_6

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getPhoneNumber()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_7

    :cond_6
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_7

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_phone_error_message:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    sget v2, Lcom/zopim/android/sdk/R$string;->pre_chat_phone_error_hint:I

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setHint(I)V

    const/4 p1, 0x0

    :cond_7
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getVisibility()I

    move-result v2

    if-nez v2, :cond_9

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getMessage()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_8

    sget-object v2, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getMessage()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_9

    :cond_8
    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_9

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_message_error_message:I

    invoke-virtual {v2, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setError(Ljava/lang/CharSequence;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    sget v2, Lcom/zopim/android/sdk/R$string;->pre_chat_message_error_hint:I

    invoke-virtual {p1, v2}, Landroid/widget/EditText;->setHint(I)V

    const/4 p1, 0x0

    :cond_9
    if-eqz p1, :cond_f

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {p1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object p1

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    invoke-virtual {v2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v2

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {v3}, Landroid/widget/Spinner;->getSelectedItem()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    invoke-virtual {v4}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p1}, Ljava/lang/String;->isEmpty()Z

    move-result v5

    if-nez v5, :cond_a

    iget-object v5, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v5, p1}, Lcom/zopim/android/sdk/api/Chat;->setName(Ljava/lang/String;)V

    :cond_a
    invoke-virtual {v1}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_b

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1, v1}, Lcom/zopim/android/sdk/api/Chat;->setEmail(Ljava/lang/String;)V

    :cond_b
    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_c

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1, v2}, Lcom/zopim/android/sdk/api/Chat;->setPhoneNumber(Ljava/lang/String;)V

    :cond_c
    if-eqz v3, :cond_d

    invoke-virtual {v3}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_d

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1, v3}, Lcom/zopim/android/sdk/api/Chat;->setDepartment(Ljava/lang/String;)V

    :cond_d
    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result p1

    if-nez p1, :cond_e

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1, v4}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/lang/String;)V

    goto :goto_3

    :cond_e
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    const-string v1, " "

    invoke-interface {p1, v1}, Lcom/zopim/android/sdk/api/Chat;->send(Ljava/lang/String;)V

    :goto_3
    new-instance p1, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;-><init>()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$id;->chat_fragment_container:I

    const-class v3, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;

    invoke-virtual {v3}, Ljava/lang/Class;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, p1, v3}, Landroidx/fragment/app/FragmentTransaction;->replace(ILandroidx/fragment/app/Fragment;Ljava/lang/String;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    goto :goto_4

    :cond_f
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    sget v1, Lcom/zopim/android/sdk/R$string;->pre_chat_validation_error_message:I

    invoke-static {p1, v1, v0}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object p1

    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    :goto_4
    return v0

    :cond_10
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    nop

    :pswitch_data_0
    .packed-switch 0x2
        :pswitch_1
        :pswitch_1
        :pswitch_0
        :pswitch_0
    .end packed-switch
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMenu:Landroid/view/Menu;

    sget v1, Lcom/zopim/android/sdk/R$id;->start_chat:I

    invoke-interface {v0, v1}, Landroid/view/Menu;->findItem(I)Landroid/view/MenuItem;

    move-result-object v0

    const-string v1, "MENU_ITEM_ENABLED"

    invoke-interface {v0}, Landroid/view/MenuItem;->isEnabled()Z

    move-result v0

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->isFinishing()Z

    move-result v0

    if-eqz v0, :cond_0

    sget-object v0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Chat aborted. Ending chat."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->i(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->endChat()V

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-interface {v0}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    :cond_0
    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 6
    .param p2    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    iget-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p2}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object p2

    invoke-interface {p2}, Lcom/zopim/android/sdk/api/ChatConfig;->getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object p2

    sget v0, Lcom/zopim/android/sdk/R$id;->name:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/EditText;

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    sget v0, Lcom/zopim/android/sdk/R$id;->email:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/EditText;

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    sget v0, Lcom/zopim/android/sdk/R$id;->phoneNumber:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/EditText;

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    sget v0, Lcom/zopim/android/sdk/R$id;->departments:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/Spinner;

    iput-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    sget v0, Lcom/zopim/android/sdk/R$id;->message:I

    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/EditText;

    iput-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    sget-object p1, Lcom/zopim/android/sdk/prechat/s;->a:[I

    iget-object v0, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->ordinal()I

    move-result v0

    aget p1, p1, v0

    const/16 v0, 0x8

    const/4 v1, 0x1

    if-eq p1, v1, :cond_4

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object p1

    invoke-interface {p1}, Lcom/zopim/android/sdk/data/DataSource;->getDepartments()Ljava/util/Map;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Map;->values()Ljava/util/Collection;

    move-result-object p1

    if-eqz p1, :cond_4

    invoke-interface {p1}, Ljava/util/Collection;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_0

    goto/16 :goto_1

    :cond_0
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/Department;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/Department;->getName()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_0

    :cond_1
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object p1

    sget v3, Lcom/zopim/android/sdk/R$string;->pre_chat_departments_hint:I

    invoke-virtual {p1, v3}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object p1

    sget-object v3, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v4

    invoke-virtual {v3, v4}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-nez v3, :cond_2

    sget-object v3, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->REQUIRED_EDITABLE:Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    iget-object v4, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {v4}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getDepartment()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object v4

    invoke-virtual {v3, v4}, Lcom/zopim/android/sdk/prechat/PreChatForm$Field;->equals(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_3

    :cond_2
    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    sget v4, Lcom/zopim/android/sdk/R$string;->required_field_template:I

    invoke-virtual {v3, v4}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v3

    new-array v4, v1, [Ljava/lang/Object;

    const/4 v5, 0x0

    aput-object p1, v4, v5

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p1

    :cond_3
    invoke-virtual {v2, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    new-instance p1, Lcom/zopim/android/sdk/prechat/q;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v3

    sget v4, Lcom/zopim/android/sdk/R$layout;->spinner_list_item:I

    invoke-direct {p1, p0, v3, v4, v2}, Lcom/zopim/android/sdk/prechat/q;-><init>(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;Landroid/content/Context;ILjava/util/List;)V

    sget v3, Lcom/zopim/android/sdk/R$layout;->support_simple_spinner_dropdown_item:I

    invoke-virtual {p1, v3}, Landroid/widget/ArrayAdapter;->setDropDownViewResource(I)V

    iget-object v3, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {v3, p1}, Landroid/widget/Spinner;->setAdapter(Landroid/widget/SpinnerAdapter;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    sub-int/2addr v2, v1

    invoke-virtual {p1, v2}, Landroid/widget/Spinner;->setSelection(I)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    new-instance v1, Lcom/zopim/android/sdk/prechat/r;

    invoke-direct {v1, p0}, Lcom/zopim/android/sdk/prechat/r;-><init>(Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;)V

    invoke-virtual {p1, v1}, Landroid/widget/Spinner;->setOnItemSelectedListener(Landroid/widget/AdapterView$OnItemSelectedListener;)V

    goto :goto_2

    :cond_4
    :goto_1
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setVisibility(I)V

    :goto_2
    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getName()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object p1

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mNameEdit:Landroid/widget/EditText;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/VisitorInfo;->getName()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p0, p1, v1, v2}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setupVisitorField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Landroid/widget/EditText;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getEmail()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object p1

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mEmailEdit:Landroid/widget/EditText;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/VisitorInfo;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-direct {p0, p1, v1, v2}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setupVisitorField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Landroid/widget/EditText;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getPhoneNumber()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object p1

    iget-object v1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPhoneNumberEdit:Landroid/widget/EditText;

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/VisitorInfo;->getPhoneNumber()Ljava/lang/String;

    move-result-object p2

    invoke-direct {p0, p1, v1, p2}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setupVisitorField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Landroid/widget/EditText;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mPreChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    invoke-virtual {p1}, Lcom/zopim/android/sdk/prechat/PreChatForm;->getMessage()Lcom/zopim/android/sdk/prechat/PreChatForm$Field;

    move-result-object p1

    iget-object p2, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mMessageEdit:Landroid/widget/EditText;

    const/4 v1, 0x0

    invoke-direct {p0, p1, p2, v1}, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->setupVisitorField(Lcom/zopim/android/sdk/prechat/PreChatForm$Field;Landroid/widget/EditText;Ljava/lang/String;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->getConfig()Lcom/zopim/android/sdk/api/ChatConfig;

    move-result-object p1

    if-eqz p1, :cond_5

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/ChatConfig;->getDepartment()Ljava/lang/String;

    move-result-object v1

    :cond_5
    if-eqz v1, :cond_7

    invoke-static {}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getInstance()Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;

    move-result-object p1

    invoke-virtual {p1}, Lcom/zopim/android/sdk/data/LivechatDepartmentsPath;->getData()Ljava/util/LinkedHashMap;

    move-result-object p1

    invoke-virtual {p1}, Ljava/util/LinkedHashMap;->values()Ljava/util/Collection;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object p1

    :cond_6
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result p2

    if-eqz p2, :cond_7

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object p2

    check-cast p2, Lcom/zopim/android/sdk/model/Department;

    if-eqz p2, :cond_6

    invoke-virtual {p2}, Lcom/zopim/android/sdk/model/Department;->getName()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v1, p2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p2

    if-eqz p2, :cond_6

    iget-object p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mDepartmentSpinner:Landroid/widget/Spinner;

    invoke-virtual {p1, v0}, Landroid/widget/Spinner;->setVisibility(I)V

    :cond_7
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

    move-result p1

    iput-boolean p1, p0, Lcom/zopim/android/sdk/prechat/ZopimPreChatFragment;->mStateMenuItemEnabled:Z

    :cond_0
    return-void
.end method
