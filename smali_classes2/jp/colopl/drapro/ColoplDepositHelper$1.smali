.class Ljp/colopl/drapro/ColoplDepositHelper$1;
.super Ljava/lang/Object;
.source "ColoplDepositHelper.java"

# interfaces
.implements Ljp/colopl/network/HttpRequestListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/ColoplDepositHelper;->prepareDepositAsync(Ljava/lang/String;Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/ColoplDepositHelper;

.field final synthetic val$itemId:Ljava/lang/String;

.field final synthetic val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

.field final synthetic val$payload:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/ColoplDepositHelper;Ljava/lang/String;Ljava/lang/String;Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;)V
    .locals 0

    .line 247
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    iput-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$itemId:Ljava/lang/String;

    iput-object p3, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$payload:Ljava/lang/String;

    iput-object p4, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceiveError(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/Exception;)V
    .locals 3

    const-string p1, "ColoplDeposit"

    .line 285
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] prepareDepositAsync.onReceiveError : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 287
    new-instance p1, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;)V

    const/4 p2, 0x0

    .line 288
    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setSuccess(Z)V

    .line 289
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorTitle(I)V

    .line 290
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "network_error_occurred"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorMessage(I)V

    .line 292
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    if-eqz p2, :cond_0

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    invoke-interface {p2, p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;->onPrepareDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;)V

    :cond_0
    return-void
.end method

.method public onReceiveResponse(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/String;)V
    .locals 7

    const-string p1, "ColoplDeposit"

    .line 250
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] prepareDepositAsync.onReceiveResponse : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 251
    new-instance p1, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;

    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;)V

    const/4 v0, 0x1

    .line 252
    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setSuccess(Z)V

    const/4 v1, 0x0

    const/4 v2, 0x0

    .line 256
    :try_start_0
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 257
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "json string: "

    invoke-virtual {p2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v1, p2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    const-string p2, "code"

    .line 258
    invoke-virtual {v3, p2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result p2
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .line 259
    :try_start_1
    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setStatusCode(I)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    :catch_0
    move-exception v3

    goto :goto_0

    :catch_1
    move-exception v3

    const/4 p2, 0x0

    .line 261
    :goto_0
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "[IABV3] onReceiveResponse json recieve error! "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v1, v4}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 262
    invoke-virtual {v3}, Lorg/json/JSONException;->printStackTrace()V

    .line 263
    invoke-virtual {p1, v2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setSuccess(Z)V

    .line 264
    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v3}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    const-string v4, "payment_server_error_title"

    const-string v5, "string"

    iget-object v6, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v6}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v6

    invoke-virtual {v6}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v3, v4, v5, v6}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v3

    invoke-virtual {p1, v3}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorTitle(I)V

    .line 265
    iget-object v3, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v3}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v3

    invoke-virtual {v3}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    const-string v4, "payment_server_error_title"

    const-string v5, "string"

    iget-object v6, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v6}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v6

    invoke-virtual {v6}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v3, v4, v5, v6}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v3

    invoke-virtual {p1, v3}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorMessage(I)V

    .line 268
    :goto_1
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->isValidStatusCode()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 269
    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setSuccess(Z)V

    .line 270
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$itemId:Ljava/lang/String;

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setItemId(Ljava/lang/String;)V

    .line 271
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$payload:Ljava/lang/String;

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setPayload(Ljava/lang/String;)V

    goto :goto_2

    .line 273
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "[IABV3] onReceiveResponse statusCode not 100 error! "

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {v1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 275
    invoke-virtual {p1, v2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setSuccess(Z)V

    .line 276
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorTitle(I)V

    .line 277
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_message"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->setErrorMessage(I)V

    .line 280
    :goto_2
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    if-eqz p2, :cond_1

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$1;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;

    invoke-interface {p2, p1}, Ljp/colopl/drapro/ColoplDepositHelper$PrepareDepositFinishedListener;->onPrepareDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;)V

    :cond_1
    return-void
.end method
