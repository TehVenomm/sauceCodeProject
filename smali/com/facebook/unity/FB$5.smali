.class final Lcom/facebook/unity/FB$5;
.super Ljava/lang/Object;
.source "FB.java"

# interfaces
.implements Lcom/facebook/GraphRequest$OnProgressCallback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/facebook/unity/FB;->UploadVideoToMediaLibrary(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$unityMessage:Lcom/facebook/unity/UnityMessage;


# direct methods
.method constructor <init>(Lcom/facebook/unity/UnityMessage;)V
    .locals 0

    .line 531
    iput-object p1, p0, Lcom/facebook/unity/FB$5;->val$unityMessage:Lcom/facebook/unity/UnityMessage;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onCompleted(Lcom/facebook/GraphResponse;)V
    .locals 2

    .line 534
    invoke-virtual {p1}, Lcom/facebook/GraphResponse;->getError()Lcom/facebook/FacebookRequestError;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 535
    iget-object v0, p0, Lcom/facebook/unity/FB$5;->val$unityMessage:Lcom/facebook/unity/UnityMessage;

    invoke-virtual {p1}, Lcom/facebook/GraphResponse;->getError()Lcom/facebook/FacebookRequestError;

    move-result-object p1

    invoke-virtual {p1}, Lcom/facebook/FacebookRequestError;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p1}, Lcom/facebook/unity/UnityMessage;->sendError(Ljava/lang/String;)V

    goto :goto_0

    .line 537
    :cond_0
    invoke-virtual {p1}, Lcom/facebook/GraphResponse;->getJSONObject()Lorg/json/JSONObject;

    move-result-object p1

    const-string v0, "video_id"

    const/4 v1, 0x0

    invoke-virtual {p1, v0, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    if-nez p1, :cond_1

    .line 539
    iget-object p1, p0, Lcom/facebook/unity/FB$5;->val$unityMessage:Lcom/facebook/unity/UnityMessage;

    const-string v0, "Response did not contain ImageID"

    invoke-virtual {p1, v0}, Lcom/facebook/unity/UnityMessage;->sendError(Ljava/lang/String;)V

    goto :goto_0

    .line 541
    :cond_1
    iget-object v0, p0, Lcom/facebook/unity/FB$5;->val$unityMessage:Lcom/facebook/unity/UnityMessage;

    const-string v1, "video_id"

    invoke-virtual {v0, v1, p1}, Lcom/facebook/unity/UnityMessage;->put(Ljava/lang/String;Ljava/io/Serializable;)Lcom/facebook/unity/UnityMessage;

    .line 542
    iget-object p1, p0, Lcom/facebook/unity/FB$5;->val$unityMessage:Lcom/facebook/unity/UnityMessage;

    invoke-virtual {p1}, Lcom/facebook/unity/UnityMessage;->send()V

    :goto_0
    return-void
.end method

.method public onProgress(JJ)V
    .locals 0

    return-void
.end method
