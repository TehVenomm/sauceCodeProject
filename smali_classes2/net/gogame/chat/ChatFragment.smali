.class public Lnet/gogame/chat/ChatFragment;
.super Landroidx/fragment/app/Fragment;
.source "ChatFragment.java"


# static fields
.field public static final PICK_IMAGE_RESULT_CODE:I = 0x1389


# instance fields
.field private chatAdapter:Lnet/gogame/chat/ChatAdapter;


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 35
    invoke-direct {p0}, Landroidx/fragment/app/Fragment;-><init>()V

    return-void
.end method

.method static synthetic access$000(Lnet/gogame/chat/ChatFragment;)Lnet/gogame/chat/ChatAdapter;
    .locals 0

    .line 35
    iget-object p0, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    return-object p0
.end method

.method static synthetic access$100(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/text/Editable;)Z
    .locals 0

    .line 35
    invoke-direct {p0, p1, p2}, Lnet/gogame/chat/ChatFragment;->isSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)Z

    move-result p0

    return p0
.end method

.method static synthetic access$200(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/text/Editable;)V
    .locals 0

    .line 35
    invoke-direct {p0, p1, p2}, Lnet/gogame/chat/ChatFragment;->updateSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)V

    return-void
.end method

.method private isSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)Z
    .locals 1

    .line 134
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    if-eqz p1, :cond_0

    .line 135
    iget-object p1, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {p1}, Lnet/gogame/chat/ChatAdapter;->getChatContext()Lnet/gogame/chat/ChatContext;

    move-result-object p1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    :goto_0
    const/4 v0, 0x0

    if-eqz p1, :cond_1

    .line 138
    invoke-interface {p1}, Lnet/gogame/chat/ChatContext;->isAttachmentSupported()Z

    move-result p1

    goto :goto_1

    :cond_1
    const/4 p1, 0x0

    :goto_1
    if-eqz p1, :cond_2

    .line 140
    invoke-virtual {p2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p1}, Lorg/apache/commons/lang3/StringUtils;->trimToNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-eqz p1, :cond_3

    :cond_2
    const/4 v0, 0x1

    :cond_3
    return v0
.end method

.method private send(Landroid/net/Uri;)V
    .locals 7

    const/4 v0, 0x1

    .line 166
    :try_start_0
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    invoke-static {v1, p1}, Lnet/gogame/chat/ContentUtils;->getFilename(Landroid/content/Context;Landroid/net/Uri;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 170
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v2

    invoke-virtual {v2}, Landroidx/fragment/app/FragmentActivity;->getCacheDir()Ljava/io/File;

    move-result-object v2

    .line 171
    new-instance v3, Ljava/io/File;

    invoke-direct {v3, v2, v1}, Ljava/io/File;-><init>(Ljava/io/File;Ljava/lang/String;)V

    .line 172
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    invoke-virtual {v1}, Landroidx/fragment/app/FragmentActivity;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v1

    .line 173
    invoke-virtual {v1, p1}, Landroid/content/ContentResolver;->openInputStream(Landroid/net/Uri;)Ljava/io/InputStream;

    move-result-object p1
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 175
    :try_start_1
    new-instance v1, Ljava/io/FileOutputStream;

    invoke-direct {v1, v3}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 177
    :try_start_2
    invoke-static {p1, v1}, Lnet/gogame/chat/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 179
    :try_start_3
    invoke-static {v1}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    .line 182
    invoke-virtual {v3}, Ljava/io/File;->length()J

    move-result-wide v1

    const-wide/32 v4, 0x380000

    cmp-long v6, v1, v4

    if-lez v6, :cond_0

    .line 183
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    sget v2, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_image_too_big_message:I

    invoke-static {v1, v2, v0}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object v1

    .line 184
    invoke-virtual {v1}, Landroid/widget/Toast;->show()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    .line 192
    :try_start_4
    invoke-static {p1}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/InputStream;)V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_0

    return-void

    .line 188
    :cond_0
    :try_start_5
    iget-object v1, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {v1}, Lnet/gogame/chat/ChatAdapter;->getChatContext()Lnet/gogame/chat/ChatContext;

    move-result-object v1

    invoke-virtual {v3}, Ljava/io/File;->getName()Ljava/lang/String;

    move-result-object v2

    .line 189
    invoke-virtual {v3}, Ljava/io/File;->toURI()Ljava/net/URI;

    move-result-object v4

    invoke-virtual {v4}, Ljava/net/URI;->toString()Ljava/lang/String;

    move-result-object v4

    .line 188
    invoke-static {v4}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v4

    invoke-interface {v1, v2, v4}, Lnet/gogame/chat/ChatContext;->registerImage(Ljava/lang/String;Landroid/net/Uri;)V

    .line 190
    iget-object v1, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {v1}, Lnet/gogame/chat/ChatAdapter;->getChatContext()Lnet/gogame/chat/ChatContext;

    move-result-object v1

    invoke-interface {v1, v3}, Lnet/gogame/chat/ChatContext;->send(Ljava/io/File;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_1

    .line 192
    :try_start_6
    invoke-static {p1}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/InputStream;)V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_0

    goto :goto_0

    :catchall_0
    move-exception v2

    .line 179
    :try_start_7
    invoke-static {v1}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    throw v2
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_1

    :catchall_1
    move-exception v1

    .line 192
    :try_start_8
    invoke-static {p1}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    throw v1

    .line 168
    :cond_1
    new-instance v1, Ljava/io/IOException;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Cannot determine filename for "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-direct {v1, p1}, Ljava/io/IOException;-><init>(Ljava/lang/String;)V

    throw v1
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_0

    .line 195
    :catch_0
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p1

    sget v1, Lcom/zopim/android/sdk/R$string;->net_gogame_chat_error_sending_picture_message:I

    invoke-static {p1, v1, v0}, Landroid/widget/Toast;->makeText(Landroid/content/Context;II)Landroid/widget/Toast;

    move-result-object p1

    .line 196
    invoke-virtual {p1}, Landroid/widget/Toast;->show()V

    :goto_0
    return-void
.end method

.method private updateSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)V
    .locals 0

    .line 144
    invoke-direct {p0, p1, p2}, Lnet/gogame/chat/ChatFragment;->isSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)Z

    move-result p2

    if-eqz p2, :cond_0

    .line 145
    sget p2, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_send_message_icon:I

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setBackgroundResource(I)V

    goto :goto_0

    .line 147
    :cond_0
    sget p2, Lcom/zopim/android/sdk/R$drawable;->net_gogame_chat_attachment_icon:I

    invoke-virtual {p1, p2}, Landroid/widget/ImageButton;->setBackgroundResource(I)V

    :goto_0
    return-void
.end method


# virtual methods
.method public onActivityResult(IILandroid/content/Intent;)V
    .locals 1

    .line 153
    invoke-super {p0, p1, p2, p3}, Landroidx/fragment/app/Fragment;->onActivityResult(IILandroid/content/Intent;)V

    const/16 v0, 0x1389

    if-ne p1, v0, :cond_1

    const/4 p1, -0x1

    if-ne p2, p1, :cond_1

    if-nez p3, :cond_0

    const-string p1, "zopim-client"

    const-string p2, "No data"

    .line 156
    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    return-void

    .line 160
    :cond_0
    invoke-virtual {p3}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object p1

    invoke-direct {p0, p1}, Lnet/gogame/chat/ChatFragment;->send(Landroid/net/Uri;)V

    :cond_1
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
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    .line 45
    invoke-virtual {p0}, Lnet/gogame/chat/ChatFragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object p3

    check-cast p3, Lnet/gogame/zopim/client/base/ZopimMainActivity;

    invoke-virtual {p3}, Lnet/gogame/zopim/client/base/ZopimMainActivity;->getChatAdapter()Lnet/gogame/chat/ChatAdapter;

    move-result-object p3

    iput-object p3, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    .line 47
    sget p3, Lcom/zopim/android/sdk/R$layout;->net_gogame_chat_fragment_chat:I

    const/4 v0, 0x0

    invoke-virtual {p1, p3, p2, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;Z)Landroid/view/View;

    move-result-object p1

    .line 50
    sget p2, Lcom/zopim/android/sdk/R$id;->listView:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ListView;

    .line 51
    iget-object p3, p0, Lnet/gogame/chat/ChatFragment;->chatAdapter:Lnet/gogame/chat/ChatAdapter;

    invoke-virtual {p2, p3}, Landroid/widget/ListView;->setAdapter(Landroid/widget/ListAdapter;)V

    const/4 p3, 0x0

    .line 52
    invoke-virtual {p2, p3}, Landroid/widget/ListView;->setDivider(Landroid/graphics/drawable/Drawable;)V

    .line 56
    sget p2, Lcom/zopim/android/sdk/R$id;->editText:I

    invoke-virtual {p1, p2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/EditText;

    const/4 p3, 0x4

    .line 57
    invoke-virtual {p2, p3}, Landroid/widget/EditText;->setImeOptions(I)V

    .line 58
    new-instance p3, Lnet/gogame/chat/ChatFragment$1;

    invoke-direct {p3, p0, p0, p2}, Lnet/gogame/chat/ChatFragment$1;-><init>(Lnet/gogame/chat/ChatFragment;Lnet/gogame/chat/ChatFragment;Landroid/widget/EditText;)V

    invoke-virtual {p2, p3}, Landroid/widget/EditText;->setOnEditorActionListener(Landroid/widget/TextView$OnEditorActionListener;)V

    .line 70
    new-instance p3, Lnet/gogame/chat/ChatFragment$2;

    invoke-direct {p3, p0, p0, p2}, Lnet/gogame/chat/ChatFragment$2;-><init>(Lnet/gogame/chat/ChatFragment;Lnet/gogame/chat/ChatFragment;Landroid/widget/EditText;)V

    invoke-virtual {p2, p3}, Landroid/widget/EditText;->setOnKeyListener(Landroid/view/View$OnKeyListener;)V

    .line 84
    sget p3, Lcom/zopim/android/sdk/R$id;->sendButton:I

    invoke-virtual {p1, p3}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p3

    check-cast p3, Landroid/widget/ImageButton;

    .line 85
    new-instance v0, Lnet/gogame/chat/ChatFragment$3;

    invoke-direct {v0, p0, p3, p2, p0}, Lnet/gogame/chat/ChatFragment$3;-><init>(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;Landroid/widget/EditText;Lnet/gogame/chat/ChatFragment;)V

    invoke-virtual {p3, v0}, Landroid/widget/ImageButton;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 109
    new-instance v0, Lnet/gogame/chat/ChatFragment$4;

    invoke-direct {v0, p0, p3}, Lnet/gogame/chat/ChatFragment$4;-><init>(Lnet/gogame/chat/ChatFragment;Landroid/widget/ImageButton;)V

    invoke-virtual {p2, v0}, Landroid/widget/EditText;->addTextChangedListener(Landroid/text/TextWatcher;)V

    .line 127
    invoke-virtual {p2}, Landroid/widget/EditText;->getText()Landroid/text/Editable;

    move-result-object p2

    invoke-direct {p0, p3, p2}, Lnet/gogame/chat/ChatFragment;->updateSendButton(Landroid/widget/ImageButton;Landroid/text/Editable;)V

    return-object p1
.end method
