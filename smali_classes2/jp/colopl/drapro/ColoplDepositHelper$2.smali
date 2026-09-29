.class Ljp/colopl/drapro/ColoplDepositHelper$2;
.super Ljava/lang/Object;
.source "ColoplDepositHelper.java"

# interfaces
.implements Ljp/colopl/network/HttpRequestListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/drapro/ColoplDepositHelper;->postDepositAsync(Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/drapro/ColoplDepositHelper;

.field final synthetic val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

.field final synthetic val$purchase:Ljp/colopl/iab/Purchase;


# direct methods
.method constructor <init>(Ljp/colopl/drapro/ColoplDepositHelper;Ljp/colopl/iab/Purchase;Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;)V
    .locals 0

    .line 320
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    iput-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$purchase:Ljp/colopl/iab/Purchase;

    iput-object p3, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onReceiveError(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/Exception;)V
    .locals 3

    const-string p1, "ColoplDeposit"

    .line 365
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] postDepositAsync.onReceiveError : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 367
    new-instance p1, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;)V

    const/4 p2, 0x0

    .line 368
    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    .line 369
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorTitle(I)V

    .line 370
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_message"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorMessage(I)V

    .line 372
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    if-eqz p2, :cond_0

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    invoke-interface {p2, p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;->onPostDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V

    :cond_0
    return-void
.end method

.method public onReceiveResponse(Ljp/colopl/network/HttpPostAsyncTask;Ljava/lang/String;)V
    .locals 5

    const-string p1, "ColoplDeposit"

    .line 323
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "[IABV3] postDepositAsync.onReceiveResponse : "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 325
    new-instance p1, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;

    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;-><init>(Ljp/colopl/drapro/ColoplDepositHelper;)V

    .line 326
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setPurchase(Ljp/colopl/iab/Purchase;)V

    const/4 v0, 0x1

    .line 327
    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    .line 328
    iget-object v1, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-virtual {v1}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p1, v1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setPurchasedSku(Ljava/lang/String;)V

    const/4 v1, 0x0

    .line 332
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, p2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string p2, "code"

    .line 333
    invoke-virtual {v2, p2}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result p2

    .line 334
    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setStatusCode(I)V

    .line 335
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setResultData(Ljava/lang/String;)V

    const-string p2, "ColoplDeposit"

    .line 336
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "[IABV3] postDepositAsync json response : "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v3, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {p2, v2}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p2

    .line 338
    invoke-virtual {p2}, Lorg/json/JSONException;->printStackTrace()V

    .line 339
    invoke-virtual {p1, v1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    .line 340
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v2, "payment_server_error_title"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v4}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v4

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p2, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorTitle(I)V

    .line 341
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v2, "payment_server_error_message"

    const-string v3, "string"

    iget-object v4, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v4}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v4

    invoke-virtual {v4}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p2, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorMessage(I)V

    .line 344
    :goto_0
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->isValidStatusCode()Z

    move-result p2

    if-eqz p2, :cond_0

    .line 345
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    iget-object v1, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-virtual {p2, v1}, Ljp/colopl/drapro/ColoplDepositHelper;->removeUndepositedPurchase(Ljp/colopl/iab/Purchase;)Z

    .line 346
    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    goto :goto_1

    .line 348
    :cond_0
    invoke-virtual {p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->isAlreadyCancelled()Z

    move-result p2

    if-eqz p2, :cond_1

    .line 350
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    iget-object v1, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$purchase:Ljp/colopl/iab/Purchase;

    invoke-virtual {p2, v1}, Ljp/colopl/drapro/ColoplDepositHelper;->removeUndepositedPurchase(Ljp/colopl/iab/Purchase;)Z

    .line 351
    invoke-virtual {p1, v0}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    goto :goto_1

    .line 354
    :cond_1
    invoke-virtual {p1, v1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setSuccess(Z)V

    .line 355
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_title"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorTitle(I)V

    .line 356
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {p2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object p2

    invoke-virtual {p2}, Ljp/colopl/drapro/StartActivity;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string v0, "payment_server_error_message"

    const-string v1, "string"

    iget-object v2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-static {v2}, Ljp/colopl/drapro/ColoplDepositHelper;->access$000(Ljp/colopl/drapro/ColoplDepositHelper;)Ljp/colopl/drapro/StartActivity;

    move-result-object v2

    invoke-virtual {v2}, Ljp/colopl/drapro/StartActivity;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, v0, v1, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, p2}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->setErrorMessage(I)V

    .line 360
    :goto_1
    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    if-eqz p2, :cond_2

    iget-object p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$2;->val$listener:Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;

    invoke-interface {p2, p1}, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositFinishedListener;->onPostDepositFinished(Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;)V

    :cond_2
    return-void
.end method
