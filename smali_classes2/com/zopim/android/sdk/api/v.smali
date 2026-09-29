.class final Lcom/zopim/android/sdk/api/v;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/zopim/android/sdk/api/Chat;


# direct methods
.method constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public emailTranscript(Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public endChat()V
    .locals 0

    return-void
.end method

.method public getConfig()Lcom/zopim/android/sdk/api/ChatConfig;
    .locals 1

    new-instance v0, Lcom/zopim/android/sdk/api/w;

    invoke-direct {v0, p0}, Lcom/zopim/android/sdk/api/w;-><init>(Lcom/zopim/android/sdk/api/v;)V

    return-object v0
.end method

.method public hasEnded()Z
    .locals 1

    const/4 v0, 0x1

    return v0
.end method

.method public resend(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public resetTimeout()V
    .locals 0

    return-void
.end method

.method public send(Ljava/io/File;)V
    .locals 0

    return-void
.end method

.method public send(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public sendChatComment(Ljava/lang/String;)V
    .locals 0
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    return-void
.end method

.method public sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .locals 0
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog$Rating;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param

    return-void
.end method

.method public sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method

.method public setDepartment(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 0

    return-void
.end method

.method public setPhoneNumber(Ljava/lang/String;)V
    .locals 0

    return-void
.end method
