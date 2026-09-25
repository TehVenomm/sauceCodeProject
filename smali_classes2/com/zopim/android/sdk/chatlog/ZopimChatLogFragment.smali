.class public Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;
.super Landroidx/fragment/app/Fragment;

# interfaces
.implements Lcom/zopim/android/sdk/chatlog/ConnectionFragment$ConnectionListener;
.implements Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment$a;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;
    }
.end annotation


# static fields
.field private static final LOG_TAG:Ljava/lang/String; = "ZopimChatLogFragment"

.field private static final STATE_ATTACH_BUTTON_ENABLED:Ljava/lang/String; = "ATTACH_BUTTON_ENABLED"

.field private static final STATE_INPUT_FIELD_ENABLED:Ljava/lang/String; = "INPUT_FILED_ENABLED"

.field private static final STATE_INPUT_FIELD_TEXT:Ljava/lang/String; = "INPUT_FILED_TEXT"

.field private static final STATE_NO_CONNECTION:Ljava/lang/String; = "NO_CONNECTION"

.field private static final STATE_SEND_BUTTON_ENABLED:Ljava/lang/String; = "SEND_BUTTON_ENABLED"

.field private static final STATE_SHOW_CHAT_END_CONFIRM_DIALOG:Ljava/lang/String; = "SHOW_CHAT_END_CONFIRM_DIALOG"

.field private static final STATE_SHOW_EMAIL_TRANSCRIPT_DIALOG:Ljava/lang/String; = "SHOW_EMAIL_TRANSCRIPT_DIALOG"

.field private static final STATE_SHOW_RECONNECT_TIMEOUT_DIALOG:Ljava/lang/String; = "SHOW_RECONNECT_TIMEOUT_DIALOG"


# instance fields
.field mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

.field private mAttachButton:Landroid/widget/ImageButton;

.field mAttachmentErrorItems:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field

.field private mChat:Lcom/zopim/android/sdk/api/Chat;

.field private mChatEndConfirmDialog:Landroid/app/AlertDialog;

.field private mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

.field mChatLogAdapter:Lcom/zopim/android/sdk/chatlog/i;

.field mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

.field private final mChatTimeoutReceiver:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;

.field private mEmailTranscriptDialog:Landroid/app/AlertDialog;

.field private final mHandler:Landroid/os/Handler;

.field private mInputField:Landroid/widget/EditText;

.field private mInputManager:Landroid/view/inputmethod/InputMethodManager;

.field private mNoConnection:Z

.field private mReconnectTimeout:J

.field private mReconnectTimeoutDialog:Landroid/app/AlertDialog;

.field mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

.field private mSendButton:Landroid/widget/ImageButton;

.field mShowReconnectFailed:Ljava/lang/Runnable;


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    new-instance v0, Landroid/os/Handler;

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/os/Handler;-><init>(Landroid/os/Looper;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    sget-wide v0, Lcom/zopim/android/sdk/api/ChatSession;->DEFAULT_RECONNECT_TIMEOUT:J

    iput-wide v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeout:J

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;

    new-instance v0, Ljava/util/LinkedList;

    invoke-direct {v0}, Ljava/util/LinkedList;-><init>()V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachmentErrorItems:Ljava/util/List;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/aj;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/aj;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mShowReconnectFailed:Ljava/lang/Runnable;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/am;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/am;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ao;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/chatlog/ao;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    return-void
.end method

.method static synthetic access$000(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Z
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->canChat()Z

    move-result p0

    return p0
.end method

.method static synthetic access$100(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/ImageButton;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    return-object p0
.end method

.method static synthetic access$1000(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/app/AlertDialog;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    return-object p0
.end method

.method static synthetic access$1102(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/app/AlertDialog;)Landroid/app/AlertDialog;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    return-object p1
.end method

.method static synthetic access$1200(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/view/View;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->hideKeyboard(Landroid/view/View;)V

    return-void
.end method

.method static synthetic access$1300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Ljava/util/LinkedHashMap;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->updateChatLogAdapter(Ljava/util/LinkedHashMap;)V

    return-void
.end method

.method static synthetic access$1400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/os/Handler;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    return-object p0
.end method

.method static synthetic access$1500(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroidx/recyclerview/widget/RecyclerView$Adapter;
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getListAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object p0

    return-object p0
.end method

.method static synthetic access$200(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/ImageButton;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    return-object p0
.end method

.method static synthetic access$300(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/api/Chat;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    return-object p0
.end method

.method static synthetic access$400(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Landroid/widget/EditText;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    return-object p0
.end method

.method static synthetic access$500()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$600(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Z
    .locals 0

    iget-boolean p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    return p0
.end method

.method static synthetic access$700(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->showEmailTranscriptDialog()V

    return-void
.end method

.method static synthetic access$800(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V
    .locals 0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->close()V

    return-void
.end method

.method static synthetic access$900(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)Lcom/zopim/android/sdk/prechat/ChatListener;
    .locals 0

    iget-object p0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    return-object p0
.end method

.method private canChat()Z
    .locals 3

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x0

    if-nez v0, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    if-eqz v0, :cond_1

    iget-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    if-nez v0, :cond_1

    goto :goto_1

    :cond_1
    const/4 v1, 0x0

    :goto_1
    return v1
.end method

.method private close()V
    .locals 1

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getFragmentManager()Landroidx/fragment/app/FragmentManager;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentManager;->beginTransaction()Landroidx/fragment/app/FragmentTransaction;

    move-result-object v0

    invoke-virtual {v0, p0}, Landroidx/fragment/app/FragmentTransaction;->remove(Landroidx/fragment/app/Fragment;)Landroidx/fragment/app/FragmentTransaction;

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentTransaction;->commit()I

    return-void
.end method

.method private getListAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v0}, Landroidx/recyclerview/widget/RecyclerView;->getAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    return-object v0
.end method

.method private hideKeyboard(Landroid/view/View;)V
    .locals 2

    if-eqz p1, :cond_0

    invoke-virtual {p1}, Landroid/view/View;->clearFocus()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputManager:Landroid/view/inputmethod/InputMethodManager;

    invoke-virtual {p1}, Landroid/view/View;->getWindowToken()Landroid/os/IBinder;

    move-result-object p1

    const/4 v1, 0x0

    invoke-virtual {v0, p1, v1}, Landroid/view/inputmethod/InputMethodManager;->hideSoftInputFromWindow(Landroid/os/IBinder;I)Z

    :cond_0
    return-void
.end method

.method private showConfirmDialog()V
    .locals 3

    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    invoke-direct {v0, v1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    sget v1, Lcom/zopim/android/sdk/R$string;->chat_end_dialog_title:I

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->chat_end_dialog_message:I

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->chat_end_dialog_confirm_button:I

    new-instance v2, Lcom/zopim/android/sdk/chatlog/av;

    invoke-direct {v2, p0}, Lcom/zopim/android/sdk/chatlog/av;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    sget v1, Lcom/zopim/android/sdk/R$string;->chat_end_dialog_cancel_button:I

    new-instance v2, Lcom/zopim/android/sdk/chatlog/au;

    invoke-direct {v2, p0}, Lcom/zopim/android/sdk/chatlog/au;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v0

    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    return-void
.end method

.method private showEmailTranscriptDialog()V
    .locals 8

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getProfile()Lcom/zopim/android/sdk/model/Profile;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Profile;->getEmail()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/Profile;->getEmail()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-nez v2, :cond_0

    const/4 v2, 0x1

    goto :goto_0

    :cond_0
    const/4 v2, 0x0

    :goto_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v3

    invoke-virtual {v3}, Landroidx/fragment/app/FragmentActivity;->getLayoutInflater()Landroid/view/LayoutInflater;

    move-result-object v3

    sget v4, Lcom/zopim/android/sdk/R$layout;->email_transcript_input_view:I

    const/4 v5, 0x0

    invoke-virtual {v3, v4, v5}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object v3

    check-cast v3, Landroid/widget/EditText;

    new-instance v4, Landroid/app/AlertDialog$Builder;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v6

    invoke-direct {v4, v6}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    const v6, 0x104000a

    invoke-virtual {v4, v6, v5}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->email_transcript_title:I

    invoke-virtual {v4, v5}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->email_transcript_message:I

    invoke-virtual {v4, v5}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->email_transcript_confirm_button:I

    new-instance v6, Lcom/zopim/android/sdk/chatlog/ax;

    invoke-direct {v6, p0}, Lcom/zopim/android/sdk/chatlog/ax;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {v4, v5, v6}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->email_transcript_cancel_button:I

    new-instance v6, Lcom/zopim/android/sdk/chatlog/aw;

    invoke-direct {v6, p0}, Lcom/zopim/android/sdk/chatlog/aw;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {v4, v5, v6}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    if-eqz v2, :cond_1

    sget v1, Lcom/zopim/android/sdk/R$string;->email_transcript_confirm_button:I

    new-instance v2, Lcom/zopim/android/sdk/chatlog/ay;

    invoke-direct {v2, p0, v0}, Lcom/zopim/android/sdk/chatlog/ay;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Lcom/zopim/android/sdk/model/Profile;)V

    invoke-virtual {v4, v1, v2}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    invoke-virtual {v4}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    goto :goto_1

    :cond_1
    invoke-virtual {v4, v3}, Landroid/app/AlertDialog$Builder;->setView(Landroid/view/View;)Landroid/app/AlertDialog$Builder;

    move-result-object v4

    invoke-virtual {v4}, Landroid/app/AlertDialog$Builder;->show()Landroid/app/AlertDialog;

    move-result-object v4

    iput-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    iget-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    const v5, 0x102000b

    invoke-virtual {v4, v5}, Landroid/app/AlertDialog;->findViewById(I)Landroid/view/View;

    move-result-object v4

    check-cast v4, Landroid/widget/TextView;

    invoke-virtual {v4}, Landroid/widget/TextView;->getLayoutParams()Landroid/view/ViewGroup$LayoutParams;

    move-result-object v5

    if-eqz v4, :cond_2

    new-instance v6, Landroid/widget/FrameLayout$LayoutParams;

    invoke-direct {v6, v5}, Landroid/widget/FrameLayout$LayoutParams;-><init>(Landroid/view/ViewGroup$LayoutParams;)V

    invoke-virtual {v4}, Landroid/widget/TextView;->getPaddingLeft()I

    move-result v5

    invoke-virtual {v3}, Landroid/widget/EditText;->getPaddingLeft()I

    move-result v7

    sub-int/2addr v5, v7

    iput v5, v6, Landroid/widget/FrameLayout$LayoutParams;->leftMargin:I

    invoke-virtual {v4}, Landroid/widget/TextView;->getPaddingRight()I

    move-result v4

    invoke-virtual {v3}, Landroid/widget/EditText;->getPaddingRight()I

    move-result v5

    add-int/2addr v4, v5

    iput v4, v6, Landroid/widget/FrameLayout$LayoutParams;->rightMargin:I

    invoke-virtual {v3, v6}, Landroid/widget/EditText;->setLayoutParams(Landroid/view/ViewGroup$LayoutParams;)V

    :cond_2
    iget-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    const/4 v5, -0x1

    invoke-virtual {v4, v5}, Landroid/app/AlertDialog;->getButton(I)Landroid/widget/Button;

    move-result-object v4

    if-eqz v4, :cond_3

    invoke-virtual {v4, v1}, Landroid/widget/Button;->setEnabled(Z)V

    new-instance v1, Lcom/zopim/android/sdk/chatlog/ah;

    invoke-direct {v1, p0, v2, v0, v3}, Lcom/zopim/android/sdk/chatlog/ah;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;ZLcom/zopim/android/sdk/model/Profile;Landroid/widget/EditText;)V

    invoke-virtual {v4, v1}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ai;

    invoke-direct {v0, p0, v4}, Lcom/zopim/android/sdk/chatlog/ai;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;Landroid/widget/Button;)V

    invoke-virtual {v3, v0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    :cond_3
    :goto_1
    return-void
.end method

.method private showKeyboard(Landroid/view/View;)V
    .locals 2

    if-eqz p1, :cond_0

    invoke-virtual {p1}, Landroid/view/View;->isEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-virtual {p1}, Landroid/view/View;->requestFocus()Z

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputManager:Landroid/view/inputmethod/InputMethodManager;

    const/4 v1, 0x1

    invoke-virtual {v0, p1, v1}, Landroid/view/inputmethod/InputMethodManager;->showSoftInput(Landroid/view/View;I)Z

    :cond_0
    return-void
.end method

.method private updateChatLogAdapter(Ljava/util/LinkedHashMap;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/LinkedHashMap<",
            "Ljava/lang/String;",
            "Lcom/zopim/android/sdk/model/ChatLog;",
            ">;)V"
        }
    .end annotation

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getListAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v0

    instance-of v0, v0, Lcom/zopim/android/sdk/chatlog/i;

    if-nez v0, :cond_0

    sget-object p1, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Aborting update. Adapter must be of type "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-class v1, Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    :cond_0
    new-instance v0, Ljava/util/TreeMap;

    invoke-direct {v0}, Ljava/util/TreeMap;-><init>()V

    invoke-virtual {p1}, Ljava/util/LinkedHashMap;->entrySet()Ljava/util/Set;

    move-result-object p1

    invoke-interface {p1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object p1

    const/4 v1, 0x1

    const/4 v2, 0x1

    :cond_1
    :goto_0
    invoke-interface {p1}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    const/4 v4, 0x0

    const/4 v5, 0x0

    if-eqz v3, :cond_d

    invoke-interface {p1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/util/Map$Entry;

    invoke-interface {v3}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-interface {v3}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/model/ChatLog;

    new-instance v7, Lcom/zopim/android/sdk/chatlog/aa;

    invoke-direct {v7}, Lcom/zopim/android/sdk/chatlog/aa;-><init>()V

    iput-object v6, v7, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getMessage()Ljava/lang/String;

    move-result-object v6

    iput-object v6, v7, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getDisplayName()Ljava/lang/String;

    move-result-object v6

    iput-object v6, v7, Lcom/zopim/android/sdk/chatlog/aa;->j:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getNick()Ljava/lang/String;

    move-result-object v6

    iput-object v6, v7, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getTimestamp()Ljava/lang/Long;

    move-result-object v6

    iput-object v6, v7, Lcom/zopim/android/sdk/chatlog/aa;->l:Ljava/lang/Long;

    sget-object v6, Lcom/zopim/android/sdk/chatlog/aq;->b:[I

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v8

    invoke-virtual {v8}, Lcom/zopim/android/sdk/model/ChatLog$Type;->ordinal()I

    move-result v8

    aget v6, v6, v8

    packed-switch v6, :pswitch_data_0

    sget-object v4, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Not showing this item in list view: "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getType()Lcom/zopim/android/sdk/model/ChatLog$Type;

    move-result-object v3

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v4, v3}, Landroid/util/Log;->v(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    :pswitch_0
    new-instance v4, Lcom/zopim/android/sdk/chatlog/t;

    invoke-direct {v4, v7}, Lcom/zopim/android/sdk/chatlog/t;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getRating()Lcom/zopim/android/sdk/model/ChatLog$Rating;

    move-result-object v5

    iput-object v5, v4, Lcom/zopim/android/sdk/chatlog/t;->a:Lcom/zopim/android/sdk/model/ChatLog$Rating;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getComment()Ljava/lang/String;

    move-result-object v3

    iput-object v3, v4, Lcom/zopim/android/sdk/chatlog/t;->b:Ljava/lang/String;

    iget-object v3, v4, Lcom/zopim/android/sdk/chatlog/t;->g:Ljava/lang/String;

    :goto_1
    invoke-virtual {v0, v3, v4}, Ljava/util/TreeMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    :pswitch_1
    sget-object v4, Lcom/zopim/android/sdk/chatlog/aq;->a:[I

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getError()Lcom/zopim/android/sdk/model/ChatLog$Error;

    move-result-object v5

    invoke-virtual {v5}, Lcom/zopim/android/sdk/model/ChatLog$Error;->ordinal()I

    move-result v5

    aget v4, v4, v5

    packed-switch v4, :pswitch_data_1

    goto :goto_3

    :pswitch_2
    iget-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachmentErrorItems:Ljava/util/List;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    invoke-interface {v4, v5}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->attachment_upload_type_error_message:I

    goto :goto_2

    :pswitch_3
    iget-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachmentErrorItems:Ljava/util/List;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    invoke-interface {v4, v5}, Ljava/util/List;->contains(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$string;->attachment_upload_size_limit_error_message:I

    :goto_2
    invoke-static {v4, v5, v1}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v4

    invoke-virtual {v4}, Landroid/widget/Toast;->show()V

    iget-object v4, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachmentErrorItems:Ljava/util/List;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    invoke-interface {v4, v5}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_2
    :goto_3
    :pswitch_4
    new-instance v4, Lcom/zopim/android/sdk/chatlog/ab;

    invoke-direct {v4, v7}, Lcom/zopim/android/sdk/chatlog/ab;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getUploadUrl()Ljava/net/URL;

    move-result-object v5

    iput-object v5, v4, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v5

    iput-object v5, v4, Lcom/zopim/android/sdk/chatlog/ab;->a:Ljava/io/File;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getProgress()I

    move-result v5

    iput v5, v4, Lcom/zopim/android/sdk/chatlog/ab;->c:I

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getError()Lcom/zopim/android/sdk/model/ChatLog$Error;

    move-result-object v3

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog$Error;->getValue()Ljava/lang/String;

    move-result-object v3

    iput-object v3, v4, Lcom/zopim/android/sdk/chatlog/ab;->d:Ljava/lang/String;

    iget-object v3, v4, Lcom/zopim/android/sdk/chatlog/ab;->b:Ljava/net/URL;

    if-eqz v3, :cond_1

    iget-object v3, v4, Lcom/zopim/android/sdk/chatlog/ab;->g:Ljava/lang/String;

    goto :goto_1

    :pswitch_5
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v4

    invoke-interface {v4}, Lcom/zopim/android/sdk/data/DataSource;->getAgents()Ljava/util/LinkedHashMap;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/LinkedHashMap;->keySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_3
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    iget-object v8, v7, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v6, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_3

    sget-object v4, Lcom/zopim/android/sdk/chatlog/aa$a;->f:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v4, v7, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v6, Lcom/zopim/android/sdk/R$string;->chat_agent_left_message:I

    invoke-virtual {v4, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    new-array v6, v1, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getDisplayName()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v6, v5

    :goto_4
    invoke-static {v4, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    iput-object v3, v7, Lcom/zopim/android/sdk/chatlog/aa;->i:Ljava/lang/String;

    :goto_5
    iget-object v3, v7, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v0, v3, v7}, Ljava/util/TreeMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_0

    :pswitch_6
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v4

    invoke-interface {v4}, Lcom/zopim/android/sdk/data/DataSource;->getAgents()Ljava/util/LinkedHashMap;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/LinkedHashMap;->keySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_4
    :goto_6
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    iget-object v8, v7, Lcom/zopim/android/sdk/chatlog/aa;->k:Ljava/lang/String;

    invoke-virtual {v6, v8}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-eqz v6, :cond_4

    if-eqz v2, :cond_5

    const/4 v2, 0x0

    goto :goto_6

    :cond_5
    sget-object v4, Lcom/zopim/android/sdk/chatlog/aa$a;->f:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v4, v7, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v6, Lcom/zopim/android/sdk/R$string;->chat_agent_joined_message:I

    invoke-virtual {v4, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    new-array v6, v1, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getDisplayName()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v6, v5

    goto :goto_4

    :pswitch_7
    sget-object v3, Lcom/zopim/android/sdk/chatlog/aa$a;->e:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v3, v7, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    goto :goto_5

    :pswitch_8
    sget-object v4, Lcom/zopim/android/sdk/chatlog/aa$a;->e:Lcom/zopim/android/sdk/chatlog/aa$a;

    iput-object v4, v7, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v6, Lcom/zopim/android/sdk/R$string;->chat_visitor_queue_message:I

    invoke-virtual {v4, v6}, Landroid/content/res/Resources;->getString(I)Ljava/lang/String;

    move-result-object v4

    new-array v6, v1, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getVisitorQueue()Ljava/lang/Integer;

    move-result-object v3

    aput-object v3, v6, v5

    goto :goto_4

    :pswitch_9
    new-instance v6, Lcom/zopim/android/sdk/chatlog/a;

    invoke-direct {v6, v7}, Lcom/zopim/android/sdk/chatlog/a;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v7

    if-eqz v7, :cond_6

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v7

    invoke-virtual {v7}, Lcom/zopim/android/sdk/model/Attachment;->getUrl()Ljava/net/URL;

    move-result-object v7

    goto :goto_7

    :cond_6
    move-object v7, v4

    :goto_7
    iput-object v7, v6, Lcom/zopim/android/sdk/chatlog/a;->a:Ljava/net/URL;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v7

    if-eqz v7, :cond_7

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v7

    invoke-virtual {v7}, Lcom/zopim/android/sdk/model/Attachment;->getName()Ljava/lang/String;

    move-result-object v7

    goto :goto_8

    :cond_7
    move-object v7, v4

    :goto_8
    iput-object v7, v6, Lcom/zopim/android/sdk/chatlog/a;->c:Ljava/lang/String;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getFile()Ljava/io/File;

    move-result-object v7

    iput-object v7, v6, Lcom/zopim/android/sdk/chatlog/a;->d:Ljava/io/File;

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v7

    if-eqz v7, :cond_8

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getAttachment()Lcom/zopim/android/sdk/model/Attachment;

    move-result-object v4

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/Attachment;->getSize()Ljava/lang/Long;

    move-result-object v4

    :cond_8
    iput-object v4, v6, Lcom/zopim/android/sdk/chatlog/a;->b:Ljava/lang/Long;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v4

    invoke-interface {v4}, Lcom/zopim/android/sdk/data/DataSource;->getAgents()Ljava/util/LinkedHashMap;

    move-result-object v4

    iget-object v7, v6, Lcom/zopim/android/sdk/chatlog/a;->k:Ljava/lang/String;

    invoke-virtual {v4, v7}, Ljava/util/LinkedHashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Lcom/zopim/android/sdk/model/Agent;

    if-eqz v4, :cond_9

    invoke-virtual {v4}, Lcom/zopim/android/sdk/model/Agent;->getAvatarUri()Ljava/lang/String;

    move-result-object v4

    iput-object v4, v6, Lcom/zopim/android/sdk/chatlog/a;->e:Ljava/lang/String;

    :cond_9
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v4

    array-length v4, v4

    new-array v4, v4, [Ljava/lang/String;

    iput-object v4, v6, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    const/4 v4, 0x0

    :goto_9
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v7

    array-length v7, v7

    if-ge v4, v7, :cond_b

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->getOptions()[Lcom/zopim/android/sdk/model/ChatLog$Option;

    move-result-object v7

    aget-object v7, v7, v4

    iget-object v8, v6, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    invoke-virtual {v7}, Lcom/zopim/android/sdk/model/ChatLog$Option;->getLabel()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v4

    invoke-virtual {v7}, Lcom/zopim/android/sdk/model/ChatLog$Option;->isSelected()Z

    move-result v8

    if-eqz v8, :cond_a

    new-array v3, v1, [Ljava/lang/String;

    invoke-virtual {v7}, Lcom/zopim/android/sdk/model/ChatLog$Option;->getLabel()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    iput-object v3, v6, Lcom/zopim/android/sdk/chatlog/a;->f:[Ljava/lang/String;

    goto :goto_a

    :cond_a
    add-int/lit8 v4, v4, 0x1

    goto :goto_9

    :cond_b
    :goto_a
    iget-object v3, v6, Lcom/zopim/android/sdk/chatlog/a;->g:Ljava/lang/String;

    invoke-virtual {v0, v3, v6}, Ljava/util/TreeMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto/16 :goto_0

    :pswitch_a
    new-instance v4, Lcom/zopim/android/sdk/chatlog/ab;

    invoke-direct {v4, v7}, Lcom/zopim/android/sdk/chatlog/ab;-><init>(Lcom/zopim/android/sdk/chatlog/aa;)V

    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->isFailed()Ljava/lang/Boolean;

    move-result-object v6

    if-nez v6, :cond_c

    goto :goto_b

    :cond_c
    invoke-virtual {v3}, Lcom/zopim/android/sdk/model/ChatLog;->isFailed()Ljava/lang/Boolean;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v5

    :goto_b
    iput-boolean v5, v4, Lcom/zopim/android/sdk/chatlog/ab;->e:Z

    iget-object v3, v7, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    goto/16 :goto_1

    :cond_d
    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getListAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object p1

    check-cast p1, Lcom/zopim/android/sdk/chatlog/i;

    :goto_c
    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result v2

    if-ge v5, v2, :cond_13

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->b(I)Lcom/zopim/android/sdk/chatlog/aa;

    move-result-object v2

    iget-object v3, v2, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    if-nez v3, :cond_e

    move-object v3, v4

    goto :goto_d

    :cond_e
    iget-object v3, v2, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v0, v3}, Ljava/util/TreeMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/zopim/android/sdk/chatlog/aa;

    :goto_d
    if-nez v3, :cond_f

    sget-object v3, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "Removed row item "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, v2, Lcom/zopim/android/sdk/chatlog/aa;->h:Lcom/zopim/android/sdk/chatlog/aa$a;

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v3, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->a(I)V

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    goto/16 :goto_e

    :cond_f
    instance-of v6, v2, Lcom/zopim/android/sdk/chatlog/ab;

    if-eqz v6, :cond_10

    instance-of v6, v3, Lcom/zopim/android/sdk/chatlog/ab;

    if-eqz v6, :cond_10

    move-object v6, v2

    check-cast v6, Lcom/zopim/android/sdk/chatlog/ab;

    move-object v7, v3

    check-cast v7, Lcom/zopim/android/sdk/chatlog/ab;

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/chatlog/ab;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-nez v8, :cond_10

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/chatlog/ab;->a(Lcom/zopim/android/sdk/chatlog/ab;)V

    sget-object v7, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Update VisitorItem "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-static {v7, v6}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    :cond_10
    instance-of v6, v2, Lcom/zopim/android/sdk/chatlog/a;

    if-eqz v6, :cond_11

    instance-of v6, v3, Lcom/zopim/android/sdk/chatlog/a;

    if-eqz v6, :cond_11

    move-object v6, v2

    check-cast v6, Lcom/zopim/android/sdk/chatlog/a;

    move-object v7, v3

    check-cast v7, Lcom/zopim/android/sdk/chatlog/a;

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/chatlog/a;->equals(Ljava/lang/Object;)Z

    move-result v8

    if-nez v8, :cond_11

    invoke-virtual {v6, v7}, Lcom/zopim/android/sdk/chatlog/a;->a(Lcom/zopim/android/sdk/chatlog/a;)V

    sget-object v7, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Update AgentItem "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-static {v7, v6}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    :cond_11
    instance-of v6, v2, Lcom/zopim/android/sdk/chatlog/t;

    if-eqz v6, :cond_12

    instance-of v6, v3, Lcom/zopim/android/sdk/chatlog/t;

    if-eqz v6, :cond_12

    check-cast v2, Lcom/zopim/android/sdk/chatlog/t;

    move-object v6, v3

    check-cast v6, Lcom/zopim/android/sdk/chatlog/t;

    invoke-virtual {v2, v6}, Lcom/zopim/android/sdk/chatlog/t;->equals(Ljava/lang/Object;)Z

    move-result v7

    if-nez v7, :cond_12

    invoke-virtual {v2, v6}, Lcom/zopim/android/sdk/chatlog/t;->a(Lcom/zopim/android/sdk/chatlog/t;)V

    sget-object v6, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "Update ChatRatingItem "

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v7, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v6, v2}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v5}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    :cond_12
    iget-object v2, v3, Lcom/zopim/android/sdk/chatlog/aa;->g:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/util/TreeMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    :goto_e
    add-int/lit8 v5, v5, 0x1

    goto/16 :goto_c

    :cond_13
    invoke-virtual {v0}, Ljava/util/TreeMap;->values()Ljava/util/Collection;

    move-result-object v0

    invoke-interface {v0}, Ljava/util/Collection;->iterator()Ljava/util/Iterator;

    move-result-object v0

    :goto_f
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_14

    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/chatlog/aa;

    sget-object v3, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Added RowItem "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {p1, v2}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/chatlog/aa;)V

    invoke-virtual {p1}, Lcom/zopim/android/sdk/chatlog/i;->getItemCount()I

    move-result v2

    invoke-virtual {p1, v2}, Lcom/zopim/android/sdk/chatlog/i;->notifyItemChanged(I)V

    sget-object v2, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    const-string v3, "Auto-scroll"

    invoke-static {v2, v3}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {v2}, Landroidx/recyclerview/widget/RecyclerView;->getLayoutManager()Landroidx/recyclerview/widget/RecyclerView$LayoutManager;

    move-result-object v2

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getListAdapter()Landroidx/recyclerview/widget/RecyclerView$Adapter;

    move-result-object v3

    invoke-virtual {v3}, Landroidx/recyclerview/widget/RecyclerView$Adapter;->getItemCount()I

    move-result v3

    sub-int/2addr v3, v1

    invoke-virtual {v2, v3}, Landroidx/recyclerview/widget/RecyclerView$LayoutManager;->scrollToPosition(I)V

    goto :goto_f

    :cond_14
    return-void

    nop

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_a
        :pswitch_9
        :pswitch_8
        :pswitch_7
        :pswitch_6
        :pswitch_5
        :pswitch_1
        :pswitch_0
    .end packed-switch

    :pswitch_data_1
    .packed-switch 0x1
        :pswitch_4
        :pswitch_3
        :pswitch_2
    .end packed-switch
.end method


# virtual methods
.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 6

    invoke-super {p0, p1, p2, p3}, Landroidx/fragment/app/Fragment;->onActivityResult(IILandroid/content/Intent;)V

    sget-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    new-instance v5, Lcom/zopim/android/sdk/chatlog/at;

    invoke-direct {v5, p0}, Lcom/zopim/android/sdk/chatlog/at;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    move v2, p1

    move v3, p2

    move-object v4, p3

    invoke-virtual/range {v0 .. v5}, Lcom/zopim/android/sdk/attachment/ImagePicker;->getFilesFromActivityOnResult(Landroid/content/Context;IILandroid/content/Intent;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V

    return-void
.end method

.method public onAttach(Landroid/app/Activity;)V
    .locals 1

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onAttach(Landroid/app/Activity;)V

    instance-of v0, p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz v0, :cond_0

    check-cast p1, Lcom/zopim/android/sdk/prechat/ChatListener;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    :cond_0
    return-void
.end method

.method public onConnected()V
    .locals 2

    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->canChat()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    const/4 v1, 0x1

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v0}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v0

    if-nez v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v0}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v0

    invoke-interface {v0}, Landroid/text/Editable;->length()I

    move-result v0

    if-lez v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v0}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v0

    if-nez v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    :cond_1
    return-void
.end method

.method public onCreate(Landroid/os/Bundle;)V
    .locals 4

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onCreate(Landroid/os/Bundle;)V

    const/4 v0, 0x1

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->setHasOptionsMenu(Z)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->resume(Landroidx/fragment/app/FragmentActivity;)Lcom/zopim/android/sdk/api/Chat;

    move-result-object v0

    iput-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getReconnectTimeout()Ljava/lang/Long;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Long;->longValue()J

    move-result-wide v0

    iput-wide v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeout:J

    if-nez p1, :cond_0

    new-instance p1, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;

    invoke-direct {p1}, Lcom/zopim/android/sdk/chatlog/ConnectionToastFragment;-><init>()V

    new-instance v0, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;

    invoke-direct {v0}, Lcom/zopim/android/sdk/chatlog/ConnectionFragment;-><init>()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getChildFragmentManager()Landroidx/fragment/app/FragmentManager;

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

    :cond_0
    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    const-string v0, "input_method"

    invoke-virtual {p1, v0}, Landroidx/fragment/app/FragmentActivity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/view/inputmethod/InputMethodManager;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputManager:Landroid/view/inputmethod/InputMethodManager;

    return-void
.end method

.method public onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V
    .locals 1

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onCreateOptionsMenu(Landroid/view/Menu;Landroid/view/MenuInflater;)V

    sget v0, Lcom/zopim/android/sdk/R$menu;->chat_log_menu:I

    invoke-virtual {p2, v0, p1}, Landroid/view/MenuInflater;->inflate(ILandroid/view/Menu;)V

    return-void
.end method

.method public onCreateView(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;
    .locals 2
    .param p2    # Landroid/view/ViewGroup;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param
    .param p3    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    sget p3, Lcom/zopim/android/sdk/R$layout;->zopim_chat_log_fragment:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    sget p2, Lcom/zopim/android/sdk/R$id;->recycler_view:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroidx/recyclerview/widget/RecyclerView;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    new-instance p2, Landroidx/recyclerview/widget/LinearLayoutManager;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    const/4 v1, 0x1

    invoke-direct {p2, p3, v1, v0}, Landroidx/recyclerview/widget/LinearLayoutManager;-><init>(Landroid/content/Context;IZ)V

    iget-object p3, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    invoke-virtual {p3, p2}, Landroidx/recyclerview/widget/RecyclerView;->setLayoutManager(Landroidx/recyclerview/widget/RecyclerView$LayoutManager;)V

    new-instance p2, Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    invoke-direct {p2, p3, v0}, Lcom/zopim/android/sdk/chatlog/i;-><init>(Landroid/content/Context;Ljava/util/List;)V

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogAdapter:Lcom/zopim/android/sdk/chatlog/i;

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogAdapter:Lcom/zopim/android/sdk/chatlog/i;

    iget-object p3, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-virtual {p2, p3}, Lcom/zopim/android/sdk/chatlog/i;->a(Lcom/zopim/android/sdk/api/Chat;)V

    iget-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mRecyclerView:Landroidx/recyclerview/widget/RecyclerView;

    iget-object p3, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogAdapter:Lcom/zopim/android/sdk/chatlog/i;

    invoke-virtual {p2, p3}, Landroidx/recyclerview/widget/RecyclerView;->setAdapter(Landroidx/recyclerview/widget/RecyclerView$Adapter;)V

    return-object p1
.end method

.method public onDisconnected()V
    .locals 2

    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    const/4 v1, 0x0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v0}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v0}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    :cond_1
    return-void
.end method

.method public onHideToast()V
    .locals 2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mShowReconnectFailed:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    return-void
.end method

.method public onOptionsItemSelected(Landroid/view/MenuItem;)Z
    .locals 2

    invoke-interface {p1}, Landroid/view/MenuItem;->getItemId()I

    move-result v0

    const v1, 0x102002c

    if-ne v1, v0, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v1}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->close()V

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1

    :cond_0
    sget v1, Lcom/zopim/android/sdk/R$id;->end_chat:I

    if-ne v1, v0, :cond_3

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {p1}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result p1

    if-eqz p1, :cond_1

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->close()V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    if-eqz p1, :cond_2

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatListener:Lcom/zopim/android/sdk/prechat/ChatListener;

    invoke-interface {p1}, Lcom/zopim/android/sdk/prechat/ChatListener;->onChatEnded()V

    goto :goto_0

    :cond_1
    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->showConfirmDialog()V

    :cond_2
    :goto_0
    const/4 p1, 0x1

    return p1

    :cond_3
    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onOptionsItemSelected(Landroid/view/MenuItem;)Z

    move-result p1

    return p1
.end method

.method public onPause()V
    .locals 5

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onPause()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->hideKeyboard(Landroid/view/View;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    const/4 v1, 0x1

    xor-int/2addr v0, v1

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0xb

    const/4 v4, 0x0

    if-lt v2, v3, :cond_1

    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->isChangingConfigurations()Z

    move-result v0

    if-nez v0, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    goto :goto_0

    :cond_1
    if-eqz v0, :cond_0

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {v0}, Landroidx/fragment/app/FragmentActivity;->isFinishing()Z

    move-result v0

    if-eqz v0, :cond_0

    :goto_0
    if-eqz v1, :cond_2

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    const-class v3, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {v1, v2, v3}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->startService(Landroid/content/Intent;)Landroid/content/ComponentName;

    :cond_2
    return-void
.end method

.method public onResume()V
    .locals 4

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onResume()V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    const-class v3, Lcom/zopim/android/sdk/widget/ChatWidgetService;

    invoke-direct {v1, v2, v3}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->stopService(Landroid/content/Intent;)Z

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChat:Lcom/zopim/android/sdk/api/Chat;

    invoke-interface {v0}, Lcom/zopim/android/sdk/api/Chat;->hasEnded()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->hideKeyboard(Landroid/view/View;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v0, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setFocusable(Z)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v0, v1}, Landroid/widget/EditText;->setEnabled(Z)V

    sget-object v0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->LOG_TAG:Ljava/lang/String;

    const-string v1, "Resuming expired chat. Disable all input elements."

    invoke-static {v0, v1}, Lcom/zopim/android/sdk/api/Logger;->v(Ljava/lang/String;Ljava/lang/String;)V

    :cond_0
    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 3

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onSaveInstanceState(Landroid/os/Bundle;)V

    const-string v0, "SEND_BUTTON_ENABLED"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v1}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "ATTACH_BUTTON_ENABLED"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v1}, Landroid/widget/ImageButton;->isEnabled()Z

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "INPUT_FILED_ENABLED"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->isEnabled()Z

    move-result v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "INPUT_FILED_TEXT"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v1}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putString(Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "NO_CONNECTION"

    iget-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "SHOW_RECONNECT_TIMEOUT_DIALOG"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    const/4 v2, 0x0

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    invoke-virtual {v1}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v1

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    :goto_0
    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "SHOW_CHAT_END_CONFIRM_DIALOG"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    if-eqz v1, :cond_1

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    invoke-virtual {v1}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v1

    goto :goto_1

    :cond_1
    const/4 v1, 0x0

    :goto_1
    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    const-string v0, "SHOW_EMAIL_TRANSCRIPT_DIALOG"

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    invoke-virtual {v1}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v2

    :cond_2
    invoke-virtual {p1, v0, v2}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    return-void
.end method

.method public onShowToast()V
    .locals 4

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mShowReconnectFailed:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacks(Ljava/lang/Runnable;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mShowReconnectFailed:Ljava/lang/Runnable;

    iget-wide v2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeout:J

    invoke-virtual {v0, v1, v2, v3}, Landroid/os/Handler;->postDelayed(Ljava/lang/Runnable;J)Z

    return-void
.end method

.method public onStart()V
    .locals 4

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStart()V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    invoke-interface {v0}, Lcom/zopim/android/sdk/data/DataSource;->getChatLog()Ljava/util/LinkedHashMap;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->updateChatLogAdapter(Ljava/util/LinkedHashMap;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addChatLogObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->addAgentsObserver(Ljava/util/Observer;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;

    new-instance v2, Landroid/content/IntentFilter;

    const-string v3, "chat.action.TIMEOUT"

    invoke-direct {v2, v3}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v1, v2}, Landroidx/fragment/app/FragmentActivity;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    return-void
.end method

.method public onStop()V
    .locals 2

    invoke-super {p0}, Landroidx/fragment/app/Fragment;->onStop()V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    const/4 v1, 0x0

    invoke-virtual {v0, v1}, Landroid/os/Handler;->removeCallbacksAndMessages(Ljava/lang/Object;)V

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v0

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mReconnectTimeoutDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->dismiss()V

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatEndConfirmDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->dismiss()V

    :cond_1
    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->isShowing()Z

    move-result v0

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mEmailTranscriptDialog:Landroid/app/AlertDialog;

    invoke-virtual {v0}, Landroid/app/AlertDialog;->dismiss()V

    :cond_2
    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatLogObserver:Lcom/zopim/android/sdk/data/observers/ChatLogObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteChatLogObserver(Ljava/util/Observer;)V

    invoke-static {}, Lcom/zopim/android/sdk/api/ZopimChat;->getDataSource()Lcom/zopim/android/sdk/data/DataSource;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAgentsObserver:Lcom/zopim/android/sdk/data/observers/AgentsObserver;

    invoke-interface {v0, v1}, Lcom/zopim/android/sdk/data/DataSource;->deleteAgentsObserver(Ljava/util/Observer;)V

    invoke-virtual {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mChatTimeoutReceiver:Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment$ChatTimeoutReceiver;

    invoke-virtual {v0, v1}, Landroidx/fragment/app/FragmentActivity;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    return-void
.end method

.method public onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
    .locals 0

    invoke-super {p0, p1, p2}, Landroidx/fragment/app/Fragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V

    sget p2, Lcom/zopim/android/sdk/R$id;->input_field:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    sget p2, Lcom/zopim/android/sdk/R$id;->send_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageButton;

    iput-object p2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    sget p2, Lcom/zopim/android/sdk/R$id;->attach_button:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    check-cast p1, Landroid/widget/ImageButton;

    iput-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    new-instance p2, Lcom/zopim/android/sdk/chatlog/ag;

    invoke-direct {p2, p0}, Lcom/zopim/android/sdk/chatlog/ag;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {p1, p2}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    new-instance p2, Lcom/zopim/android/sdk/chatlog/ar;

    invoke-direct {p2, p0}, Lcom/zopim/android/sdk/chatlog/ar;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    new-instance p2, Lcom/zopim/android/sdk/chatlog/as;

    invoke-direct {p2, p0}, Lcom/zopim/android/sdk/chatlog/as;-><init>(Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;)V

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 4
    .param p1    # Landroid/os/Bundle;
        .annotation build Landroidx/annotation/Nullable;
        .end annotation
    .end param

    invoke-super {p0, p1}, Landroidx/fragment/app/Fragment;->onViewStateRestored(Landroid/os/Bundle;)V

    const/4 v0, 0x0

    if-eqz p1, :cond_2

    const-string v1, "SEND_BUTTON_ENABLED"

    const/4 v2, 0x1

    invoke-virtual {p1, v1, v2}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {v3, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    const-string v1, "ATTACH_BUTTON_ENABLED"

    invoke-virtual {p1, v1, v2}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iget-object v3, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mAttachButton:Landroid/widget/ImageButton;

    invoke-virtual {v3, v1}, Landroid/widget/ImageButton;->setEnabled(Z)V

    const-string v1, "INPUT_FILED_ENABLED"

    invoke-virtual {p1, v1, v2}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v2, v1}, Landroid/widget/EditText;->setEnabled(Z)V

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v2, v1}, Landroid/widget/EditText;->setFocusable(Z)V

    const-string v1, "INPUT_FILED_TEXT"

    invoke-virtual {p1, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    iget-object v2, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {v2, v1}, Landroid/widget/EditText;->setText(Ljava/lang/CharSequence;)V

    const-string v1, "NO_CONNECTION"

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    iput-boolean v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mNoConnection:Z

    const-string v1, "SHOW_RECONNECT_TIMEOUT_DIALOG"

    invoke-virtual {p1, v1, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    const-string v2, "SHOW_CHAT_END_CONFIRM_DIALOG"

    invoke-virtual {p1, v2, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result v2

    const-string v3, "SHOW_EMAIL_TRANSCRIPT_DIALOG"

    invoke-virtual {p1, v3, v0}, Landroid/os/Bundle;->getBoolean(Ljava/lang/String;Z)Z

    move-result p1

    if-eqz v1, :cond_0

    iget-object v0, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mHandler:Landroid/os/Handler;

    iget-object v1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mShowReconnectFailed:Ljava/lang/Runnable;

    invoke-virtual {v0, v1}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_0
    if-eqz v2, :cond_1

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->showConfirmDialog()V

    :cond_1
    if-eqz p1, :cond_3

    invoke-direct {p0}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->showEmailTranscriptDialog()V

    goto :goto_0

    :cond_2
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mSendButton:Landroid/widget/ImageButton;

    invoke-virtual {p1, v0}, Landroid/widget/ImageButton;->setEnabled(Z)V

    :cond_3
    :goto_0
    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-virtual {p1}, Landroid/widget/EditText;->isEnabled()Z

    move-result p1

    if-eqz p1, :cond_4

    iget-object p1, p0, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->mInputField:Landroid/widget/EditText;

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/chatlog/ZopimChatLogFragment;->showKeyboard(Landroid/view/View;)V

    :cond_4
    return-void
.end method
