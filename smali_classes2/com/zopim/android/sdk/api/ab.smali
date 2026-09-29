.class Lcom/zopim/android/sdk/api/ab;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/ChatConfig;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ZopimChat;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ZopimChat;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/ab;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getDepartment()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ab;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$000(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    move-result-object v0

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->department:Ljava/lang/String;

    return-object v0
.end method

.method public getPreChatForm()Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ab;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$000(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    move-result-object v0

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->preChatForm:Lcom/zopim/android/sdk/prechat/PreChatForm;

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;->build()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    :cond_0
    return-object v0
.end method

.method public getTags()[Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ab;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$000(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    move-result-object v0

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->tags:[Ljava/lang/String;

    return-object v0
.end method

.method public getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/ab;->a:Lcom/zopim/android/sdk/api/ZopimChat;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ZopimChat;->access$000(Lcom/zopim/android/sdk/api/ZopimChat;)Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;

    move-result-object v0

    iget-object v0, v0, Lcom/zopim/android/sdk/api/ZopimChat$SessionConfig;->visitorInfo:Lcom/zopim/android/sdk/model/VisitorInfo;

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    :cond_0
    return-object v0
.end method
