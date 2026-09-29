.class final Lcom/helpshift/support/SupportInternal$1;
.super Ljava/lang/Object;
.source "SupportInternal.java"

# interfaces
.implements Lcom/helpshift/common/FetchDataFromThread;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/SupportInternal;->getNotificationCount(Landroid/os/Handler;Landroid/os/Handler;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Lcom/helpshift/common/FetchDataFromThread<",
        "Lcom/helpshift/util/ValuePair<",
        "Ljava/lang/Integer;",
        "Ljava/lang/Boolean;",
        ">;",
        "Ljava/lang/Object;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic val$failure:Landroid/os/Handler;

.field final synthetic val$success:Landroid/os/Handler;


# direct methods
.method constructor <init>(Landroid/os/Handler;Landroid/os/Handler;)V
    .locals 0

    .line 335
    iput-object p1, p0, Lcom/helpshift/support/SupportInternal$1;->val$success:Landroid/os/Handler;

    iput-object p2, p0, Lcom/helpshift/support/SupportInternal$1;->val$failure:Landroid/os/Handler;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onDataFetched(Lcom/helpshift/util/ValuePair;)V
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/helpshift/util/ValuePair<",
            "Ljava/lang/Integer;",
            "Ljava/lang/Boolean;",
            ">;)V"
        }
    .end annotation

    if-nez p1, :cond_0

    return-void

    .line 343
    :cond_0
    iget-object v0, p0, Lcom/helpshift/support/SupportInternal$1;->val$success:Landroid/os/Handler;

    invoke-virtual {v0}, Landroid/os/Handler;->obtainMessage()Landroid/os/Message;

    move-result-object v0

    .line 344
    new-instance v1, Landroid/os/Bundle;

    invoke-direct {v1}, Landroid/os/Bundle;-><init>()V

    const-string v2, "value"

    .line 345
    iget-object v3, p1, Lcom/helpshift/util/ValuePair;->first:Ljava/lang/Object;

    check-cast v3, Ljava/lang/Integer;

    invoke-virtual {v3}, Ljava/lang/Integer;->intValue()I

    move-result v3

    invoke-virtual {v1, v2, v3}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    const-string v2, "cache"

    .line 346
    iget-object p1, p1, Lcom/helpshift/util/ValuePair;->second:Ljava/lang/Object;

    check-cast p1, Ljava/lang/Boolean;

    invoke-virtual {p1}, Ljava/lang/Boolean;->booleanValue()Z

    move-result p1

    invoke-virtual {v1, v2, p1}, Landroid/os/Bundle;->putBoolean(Ljava/lang/String;Z)V

    .line 347
    iput-object v1, v0, Landroid/os/Message;->obj:Ljava/lang/Object;

    .line 348
    iget-object p1, p0, Lcom/helpshift/support/SupportInternal$1;->val$success:Landroid/os/Handler;

    invoke-virtual {p1, v0}, Landroid/os/Handler;->sendMessage(Landroid/os/Message;)Z

    return-void
.end method

.method public bridge synthetic onDataFetched(Ljava/lang/Object;)V
    .locals 0

    .line 335
    check-cast p1, Lcom/helpshift/util/ValuePair;

    invoke-virtual {p0, p1}, Lcom/helpshift/support/SupportInternal$1;->onDataFetched(Lcom/helpshift/util/ValuePair;)V

    return-void
.end method

.method public onFailure(Ljava/lang/Object;)V
    .locals 3

    .line 353
    iget-object p1, p0, Lcom/helpshift/support/SupportInternal$1;->val$failure:Landroid/os/Handler;

    if-eqz p1, :cond_0

    .line 354
    iget-object p1, p0, Lcom/helpshift/support/SupportInternal$1;->val$failure:Landroid/os/Handler;

    invoke-virtual {p1}, Landroid/os/Handler;->obtainMessage()Landroid/os/Message;

    move-result-object p1

    .line 355
    new-instance v0, Landroid/os/Bundle;

    invoke-direct {v0}, Landroid/os/Bundle;-><init>()V

    const-string v1, "value"

    const/4 v2, -0x1

    .line 356
    invoke-virtual {v0, v1, v2}, Landroid/os/Bundle;->putInt(Ljava/lang/String;I)V

    .line 357
    iput-object v0, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    .line 358
    iget-object v0, p0, Lcom/helpshift/support/SupportInternal$1;->val$failure:Landroid/os/Handler;

    invoke-virtual {v0, p1}, Landroid/os/Handler;->sendMessage(Landroid/os/Message;)Z

    :cond_0
    return-void
.end method
