.class Lcom/helpshift/support/fragments/SearchFragment$1;
.super Landroid/os/Handler;
.source "SearchFragment.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/fragments/SearchFragment;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/SearchFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/SearchFragment;)V
    .locals 0

    .line 43
    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    invoke-direct {p0}, Landroid/os/Handler;-><init>()V

    return-void
.end method


# virtual methods
.method public handleMessage(Landroid/os/Message;)V
    .locals 2

    if-eqz p1, :cond_3

    .line 46
    invoke-virtual {p1}, Landroid/os/Message;->getData()Landroid/os/Bundle;

    move-result-object v0

    if-eqz v0, :cond_3

    iget-object v0, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    if-nez v0, :cond_0

    goto :goto_0

    .line 49
    :cond_0
    invoke-virtual {p1}, Landroid/os/Message;->getData()Landroid/os/Bundle;

    move-result-object v0

    const-string v1, "key_search_query"

    .line 50
    invoke-virtual {v0, v1}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 52
    iget-object v1, p0, Lcom/helpshift/support/fragments/SearchFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    iget-object v1, v1, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    if-eqz v1, :cond_2

    iget-object v1, p0, Lcom/helpshift/support/fragments/SearchFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    iget-object v1, v1, Lcom/helpshift/support/fragments/SearchFragment;->currentQuery:Ljava/lang/String;

    invoke-virtual {v1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 53
    iget-object p1, p1, Landroid/os/Message;->obj:Ljava/lang/Object;

    check-cast p1, Ljava/util/List;

    if-nez p1, :cond_1

    .line 55
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    .line 57
    :cond_1
    iget-object v0, p0, Lcom/helpshift/support/fragments/SearchFragment$1;->this$0:Lcom/helpshift/support/fragments/SearchFragment;

    invoke-virtual {v0, p1}, Lcom/helpshift/support/fragments/SearchFragment;->updateSearchResult(Ljava/util/List;)V

    :cond_2
    return-void

    :cond_3
    :goto_0
    return-void
.end method
