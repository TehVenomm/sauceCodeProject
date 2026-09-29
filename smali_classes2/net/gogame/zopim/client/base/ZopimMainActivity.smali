.class public Lnet/gogame/zopim/client/base/ZopimMainActivity;
.super Lnet/gogame/chat/BaseActivity;
.source "ZopimMainActivity.java"


# static fields
.field private static final CHAT_FRAGMENT_TAG:Ljava/lang/String; = "CHAT_FRAGMENT"

.field private static final EXTRA_CHATBOT_CONFIG:Ljava/lang/String; = "chatbot.config"

.field private static final EXTRA_ZOPIM_SESSIONCONFIG:Ljava/lang/String; = "zopim.sessionConfig"


# instance fields
.field private chatAdapter:Lnet/gogame/chat/ChatAdapter;

.field private multiChatContext:Lnet/gogame/chat/MultiChatContext;

.field private final uiContext:Lnet/gogame/chat/UIContext;


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 30
    invoke-direct {p0}, Lnet/gogame/chat/BaseActivity;-><init>()V

    .line 39
    new-instance v0, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;

    invoke-direct {v0, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$1;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    iput-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->uiContext:Lnet/gogame/chat/UIContext;

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/zopim/client/base/ZopimMainActivity;)Lnet/gogame/chat/UIContext;
    .locals 0

    .line 30
    iget-object p0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->uiContext:Lnet/gogame/chat/UIContext;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/zopim/client/base/ZopimMainActivity;)Lnet/gogame/chat/MultiChatContext;
    .locals 0

    .line 30
    iget-object p0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    return-object p0
.end method

.method static synthetic access$200(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V
    .locals 0

    .line 30
    invoke-direct {p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->createExitDialog()V

    return-void
.end method

.method private createExitDialog()V
    .locals 3

    .line 187
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x16

    if-lt v0, v1, :cond_0

    .line 188
    new-instance v0, Landroid/app/AlertDialog$Builder;

    const v1, 0x10302d2

    invoke-direct {v0, p0, v1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;I)V

    goto :goto_0

    .line 191
    :cond_0
    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p0}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    .line 193
    :goto_0
    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_exit_alert_dialog_title:I

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(I)Landroid/app/AlertDialog$Builder;

    .line 194
    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_exit_alert_dialog_message:I

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setMessage(I)Landroid/app/AlertDialog$Builder;

    .line 195
    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_end_button_caption:I

    new-instance v2, Lnet/gogame/zopim/client/base/ZopimMainActivity$7;

    invoke-direct {v2, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$7;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setPositiveButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 202
    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_cancel_button_caption:I

    new-instance v2, Lnet/gogame/zopim/client/base/ZopimMainActivity$8;

    invoke-direct {v2, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$8;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    invoke-virtual {v0, v1, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(ILandroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    .line 209
    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object v0

    .line 210
    invoke-virtual {v0}, Landroid/app/AlertDialog;->show()V

    return-void
.end method

.method public static startActivity(Landroid/content/Context;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)V
    .locals 2

    .line 69
    new-instance v0, Landroid/content/Intent;

    const-class v1, Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "zopim.sessionConfig"

    .line 70
    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 71
    invoke-virtual {p0, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return-void
.end method

.method public static startActivity(Landroid/content/Context;Lnet/gogame/chat/chatbot/ChatBotConfig;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;)V
    .locals 2

    .line 76
    new-instance v0, Landroid/content/Intent;

    const-class v1, Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-direct {v0, p0, v1}, Landroid/content/Intent;-><init>(Landroid/content/Context;Ljava/lang/Class;)V

    const-string v1, "chatbot.config"

    .line 77
    invoke-virtual {v0, v1, p1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    const-string p1, "zopim.sessionConfig"

    .line 78
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 79
    invoke-virtual {p0, v0}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    return-void
.end method


# virtual methods
.method public getChatAdapter()Lnet/gogame/chat/ChatAdapter;
    .locals 1

    .line 83
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    return-object v0
.end method

.method protected onCreate(Landroid/os/Bundle;)V
    .locals 6

    .line 88
    invoke-super {p0, p1}, Lnet/gogame/chat/BaseActivity;->onCreate(Landroid/os/Bundle;)V

    const/4 v0, 0x1

    .line 90
    invoke-virtual {p0, v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->requestWindowFeature(I)Z

    const/4 v1, 0x0

    .line 91
    invoke-virtual {p0, v1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->setFinishOnTouchOutside(Z)V

    const/4 v1, 0x0

    .line 92
    invoke-virtual {p0, v1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->setTitle(Ljava/lang/CharSequence;)V

    .line 94
    sget v1, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_activity_main:I

    invoke-virtual {p0, v1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->setContentView(I)V

    .line 96
    invoke-virtual {p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v1

    const-string v2, "chatbot.config"

    .line 97
    invoke-virtual {v1, v2}, Landroid/content/Intent;->getSerializableExtra(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v1

    check-cast v1, Lnet/gogame/chat/chatbot/ChatBotConfig;

    .line 98
    invoke-virtual {p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->getIntent()Landroid/content/Intent;

    move-result-object v2

    const-string v3, "zopim.sessionConfig"

    .line 99
    invoke-virtual {v2, v3}, Landroid/content/Intent;->getSerializableExtra(Ljava/lang/String;)Ljava/io/Serializable;

    move-result-object v2

    check-cast v2, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    .line 101
    new-instance v3, Lnet/gogame/chat/ChatAdapterViewFactory;

    iget-object v4, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->uiContext:Lnet/gogame/chat/UIContext;

    invoke-direct {v3, p0, v4, v0}, Lnet/gogame/chat/ChatAdapterViewFactory;-><init>(Landroid/content/Context;Lnet/gogame/chat/UIContext;Z)V

    .line 104
    sget v0, Lcom/zopim/android/sdk/R$id;->switchButton:I

    invoke-virtual {p0, v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/Button;

    if-eqz v2, :cond_0

    .line 106
    sget v4, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_outline_button:I

    invoke-virtual {v0, v4}, Landroid/widget/Button;->setBackgroundResource(I)V

    .line 107
    invoke-virtual {p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$color;->net_gogame_chat_outline_button:I

    .line 108
    invoke-virtual {v4, v5}, Landroid/content/res/Resources;->getColor(I)I

    move-result v4

    .line 107
    invoke-virtual {v0, v4}, Landroid/widget/Button;->setTextColor(I)V

    .line 109
    new-instance v4, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;

    invoke-direct {v4, p0, v2, v3}, Lnet/gogame/zopim/client/base/ZopimMainActivity$2;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Lnet/gogame/chat/ChatAdapterViewFactory;)V

    invoke-virtual {v0, v4}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    goto :goto_0

    .line 120
    :cond_0
    sget v4, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_outline_button_disabled:I

    invoke-virtual {v0, v4}, Landroid/widget/Button;->setBackgroundResource(I)V

    .line 122
    invoke-virtual {p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v4

    sget v5, Lcom/zopim/android/sdk/R$color;->net_gogame_chat_outline_button_disabled:I

    .line 123
    invoke-virtual {v4, v5}, Landroid/content/res/Resources;->getColor(I)I

    move-result v4

    .line 122
    invoke-virtual {v0, v4}, Landroid/widget/Button;->setTextColor(I)V

    .line 124
    new-instance v4, Lnet/gogame/zopim/client/base/ZopimMainActivity$3;

    invoke-direct {v4, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$3;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    invoke-virtual {v0, v4}, Landroid/widget/Button;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 136
    :goto_0
    new-instance v4, Lnet/gogame/chat/MultiChatContext;

    new-instance v5, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;

    invoke-direct {v5, p0, v2, v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$4;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Landroid/widget/Button;)V

    invoke-direct {v4, v5}, Lnet/gogame/chat/MultiChatContext;-><init>(Lnet/gogame/chat/MultiChatContext$Listener;)V

    iput-object v4, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    if-eqz v1, :cond_1

    .line 150
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-direct {v0, p0, v1, v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext;-><init>(Landroid/app/Activity;Lnet/gogame/chat/chatbot/ChatBotConfig;Lnet/gogame/chat/ChatAdapterViewFactory;)V

    .line 152
    iget-object v1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    invoke-virtual {v1, v0}, Lnet/gogame/chat/MultiChatContext;->addChatContext(Lnet/gogame/chat/ChatContext;)V

    goto :goto_1

    .line 154
    :cond_1
    new-instance v0, Lnet/gogame/chat/zopim/ZopimChatContext;

    iget-object v1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->uiContext:Lnet/gogame/chat/UIContext;

    invoke-direct {v0, p0, v2, v3, v1}, Lnet/gogame/chat/zopim/ZopimChatContext;-><init>(Landroidx/fragment/app/FragmentActivity;Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/UIContext;)V

    .line 156
    iget-object v1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    invoke-virtual {v1, v0}, Lnet/gogame/chat/MultiChatContext;->addChatContext(Lnet/gogame/chat/ChatContext;)V

    .line 159
    :goto_1
    new-instance v0, Lnet/gogame/chat/ChatAdapter;

    iget-object v1, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->uiContext:Lnet/gogame/chat/UIContext;

    iget-object v2, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    invoke-direct {v0, p0, v1, v3, v2}, Lnet/gogame/chat/ChatAdapter;-><init>(Landroid/content/Context;Lnet/gogame/chat/UIContext;Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatContext;)V

    iput-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    .line 161
    sget v0, Lcom/zopim/android/sdk/R$id;->closeButton:I

    invoke-virtual {p0, v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 162
    new-instance v1, Lnet/gogame/zopim/client/base/ZopimMainActivity$5;

    invoke-direct {v1, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$5;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 170
    sget v0, Lcom/zopim/android/sdk/R$id;->backButton:I

    invoke-virtual {p0, v0}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->findViewById(I)Landroid/view/View;

    move-result-object v0

    .line 171
    new-instance v1, Lnet/gogame/zopim/client/base/ZopimMainActivity$6;

    invoke-direct {v1, p0}, Lnet/gogame/zopim/client/base/ZopimMainActivity$6;-><init>(Lnet/gogame/zopim/client/base/ZopimMainActivity;)V

    invoke-virtual {v0, v1}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    if-nez p1, :cond_2

    .line 180
    new-instance p1, Lnet/gogame/chat/ChatFragment;

    invoke-direct {p1}, Lnet/gogame/chat/ChatFragment;-><init>()V

    .line 181
    sget v0, Lnet/gogame/chat/Constants;->FRAGMENT_CONTAINER:I

    const-string v1, "CHAT_FRAGMENT"

    invoke-virtual {p0, p1, v0, v1}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->addFragment(Landroidx/fragment/app/Fragment;ILjava/lang/String;)V

    :cond_2
    return-void
.end method

.method protected onStart()V
    .locals 1

    .line 215
    invoke-super {p0}, Lnet/gogame/chat/BaseActivity;->onStart()V

    .line 216
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    if-eqz v0, :cond_0

    .line 217
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/MultiChatContext;->start()V

    .line 219
    :cond_0
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    if-eqz v0, :cond_1

    .line 220
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {v0}, Lnet/gogame/chat/ChatAdapter;->start()V

    :cond_1
    return-void
.end method

.method protected onStop()V
    .locals 1

    .line 226
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    if-eqz v0, :cond_0

    .line 227
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->multiChatContext:Lnet/gogame/chat/MultiChatContext;

    invoke-virtual {v0}, Lnet/gogame/chat/MultiChatContext;->stop()V

    .line 229
    :cond_0
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    if-eqz v0, :cond_1

    .line 230
    iget-object v0, p0, Lnet/gogame/zopim/client/base/ZopimMainActivity;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {v0}, Lnet/gogame/chat/ChatAdapter;->stop()V

    .line 232
    :cond_1
    invoke-super {p0}, Lnet/gogame/chat/BaseActivity;->onStop()V

    return-void
.end method
