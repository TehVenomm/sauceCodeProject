.class Ljp/colopl/iab/IabHelper$3;
.super Ljava/lang/Object;
.source "IabHelper.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/colopl/iab/IabHelper;->consumeAsyncInternal(Ljava/util/List;Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Ljp/colopl/iab/IabHelper;

.field final synthetic val$handler:Landroid/os/Handler;

.field final synthetic val$multiListener:Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;

.field final synthetic val$purchases:Ljava/util/List;

.field final synthetic val$singleListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;


# direct methods
.method constructor <init>(Ljp/colopl/iab/IabHelper;Ljava/util/List;Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;Landroid/os/Handler;Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;)V
    .locals 0

    .line 960
    iput-object p1, p0, Ljp/colopl/iab/IabHelper$3;->this$0:Ljp/colopl/iab/IabHelper;

    iput-object p2, p0, Ljp/colopl/iab/IabHelper$3;->val$purchases:Ljava/util/List;

    iput-object p3, p0, Ljp/colopl/iab/IabHelper$3;->val$singleListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;

    iput-object p4, p0, Ljp/colopl/iab/IabHelper$3;->val$handler:Landroid/os/Handler;

    iput-object p5, p0, Ljp/colopl/iab/IabHelper$3;->val$multiListener:Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 7

    .line 962
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    .line 963
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->val$purchases:Ljava/util/List;

    invoke-interface {v1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v1

    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljp/colopl/iab/Purchase;

    .line 965
    :try_start_0
    iget-object v3, p0, Ljp/colopl/iab/IabHelper$3;->this$0:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v3, v2}, Ljp/colopl/iab/IabHelper;->consume(Ljp/colopl/iab/Purchase;)V

    .line 966
    new-instance v3, Ljp/colopl/iab/IabResult;

    const/4 v4, 0x0

    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Successful consume of sku "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljp/colopl/iab/Purchase;->getSku()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v5, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v3, v4, v2}, Ljp/colopl/iab/IabResult;-><init>(ILjava/lang/String;)V

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljp/colopl/iab/IabException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v2

    .line 969
    invoke-virtual {v2}, Ljp/colopl/iab/IabException;->getResult()Ljp/colopl/iab/IabResult;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    goto :goto_0

    .line 973
    :cond_0
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->this$0:Ljp/colopl/iab/IabHelper;

    invoke-virtual {v1}, Ljp/colopl/iab/IabHelper;->flagEndAsync()V

    .line 974
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->val$singleListener:Ljp/colopl/iab/IabHelper$OnConsumeFinishedListener;

    if-eqz v1, :cond_1

    .line 975
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->val$handler:Landroid/os/Handler;

    new-instance v2, Ljp/colopl/iab/IabHelper$3$1;

    invoke-direct {v2, p0, v0}, Ljp/colopl/iab/IabHelper$3$1;-><init>(Ljp/colopl/iab/IabHelper$3;Ljava/util/List;)V

    invoke-virtual {v1, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    .line 981
    :cond_1
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->val$multiListener:Ljp/colopl/iab/IabHelper$OnConsumeMultiFinishedListener;

    if-eqz v1, :cond_2

    .line 982
    iget-object v1, p0, Ljp/colopl/iab/IabHelper$3;->val$handler:Landroid/os/Handler;

    new-instance v2, Ljp/colopl/iab/IabHelper$3$2;

    invoke-direct {v2, p0, v0}, Ljp/colopl/iab/IabHelper$3$2;-><init>(Ljp/colopl/iab/IabHelper$3;Ljava/util/List;)V

    invoke-virtual {v1, v2}, Landroid/os/Handler;->post(Ljava/lang/Runnable;)Z

    :cond_2
    return-void
.end method
