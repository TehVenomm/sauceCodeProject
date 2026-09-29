.class Lcom/zopim/android/sdk/api/g;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/ChatConfig;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/ChatService;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/ChatService;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getDepartment()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ChatService;->access$500(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getPreChatForm()Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ChatService;->access$700(Lcom/zopim/android/sdk/api/ChatService;)Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    if-nez v0, :cond_0

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;->build()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    goto :goto_0

    :cond_0
    iget-object v0, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ChatService;->access$700(Lcom/zopim/android/sdk/api/ChatService;)Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    :goto_0
    return-object v0
.end method

.method public getTags()[Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v0}, Lcom/zopim/android/sdk/api/ChatService;->access$600(Lcom/zopim/android/sdk/api/ChatService;)[Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 2

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    iget-object v1, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v1}, Lcom/zopim/android/sdk/api/ChatService;->access$1000(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->name(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v1}, Lcom/zopim/android/sdk/api/ChatService;->access$900(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->email(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    iget-object v1, p0, Lcom/zopim/android/sdk/api/g;->a:Lcom/zopim/android/sdk/api/ChatService;

    invoke-static {v1}, Lcom/zopim/android/sdk/api/ChatService;->access$800(Lcom/zopim/android/sdk/api/ChatService;)Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->phoneNumber(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    move-result-object v0

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    return-object v0
.end method
