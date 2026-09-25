.class public interface abstract Lcom/zopim/android/sdk/api/ChatApi;
.super Ljava/lang/Object;


# virtual methods
.method public abstract emailTranscript(Ljava/lang/String;)Z
.end method

.method public abstract endChat()V
.end method

.method public abstract resend(Ljava/lang/String;)V
.end method

.method public abstract send(Ljava/io/File;)V
    .param p1    # Ljava/io/File;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
.end method

.method public abstract send(Ljava/lang/String;)V
.end method

.method public abstract sendChatComment(Ljava/lang/String;)V
    .param p1    # Ljava/lang/String;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
.end method

.method public abstract sendChatRating(Lcom/zopim/android/sdk/model/ChatLog$Rating;)V
    .param p1    # Lcom/zopim/android/sdk/model/ChatLog$Rating;
        .annotation build Landroidx/annotation/NonNull;
        .end annotation
    .end param
.end method

.method public abstract sendOfflineMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Z
.end method

.method public abstract setDepartment(Ljava/lang/String;)V
.end method

.method public abstract setEmail(Ljava/lang/String;)V
.end method

.method public abstract setName(Ljava/lang/String;)V
.end method

.method public abstract setPhoneNumber(Ljava/lang/String;)V
.end method
