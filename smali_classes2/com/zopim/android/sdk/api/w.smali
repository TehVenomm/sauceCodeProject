.class Lcom/zopim/android/sdk/api/w;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/ChatConfig;


# instance fields
.field final synthetic a:Lcom/zopim/android/sdk/api/v;


# direct methods
.method constructor <init>(Lcom/zopim/android/sdk/api/v;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/api/w;->a:Lcom/zopim/android/sdk/api/v;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getDepartment()Ljava/lang/String;
    .locals 1

    const-string v0, ""

    return-object v0
.end method

.method public getPreChatForm()Lcom/zopim/android/sdk/prechat/PreChatForm;
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/prechat/PreChatForm$Builder;->build()Lcom/zopim/android/sdk/prechat/PreChatForm;

    move-result-object v0

    return-object v0
.end method

.method public getTags()[Ljava/lang/String;
    .locals 1

    const/4 v0, 0x0

    new-array v0, v0, [Ljava/lang/String;

    return-object v0
.end method

.method public getVisitorInfo()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;

    invoke-direct {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;-><init>()V

    invoke-virtual {v0}, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->build()Lcom/zopim/android/sdk/model/VisitorInfo;

    move-result-object v0

    return-object v0
.end method
