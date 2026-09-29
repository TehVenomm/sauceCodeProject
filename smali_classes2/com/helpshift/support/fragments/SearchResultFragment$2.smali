.class Lcom/helpshift/support/fragments/SearchResultFragment$2;
.super Ljava/lang/Object;
.source "SearchResultFragment.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/fragments/SearchResultFragment;->onViewCreated(Landroid/view/View;Landroid/os/Bundle;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/fragments/SearchResultFragment;


# direct methods
.method constructor <init>(Lcom/helpshift/support/fragments/SearchResultFragment;)V
    .locals 0

    .line 62
    iput-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment$2;->this$0:Lcom/helpshift/support/fragments/SearchResultFragment;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 65
    iget-object p1, p0, Lcom/helpshift/support/fragments/SearchResultFragment$2;->this$0:Lcom/helpshift/support/fragments/SearchResultFragment;

    iget-object p1, p1, Lcom/helpshift/support/fragments/SearchResultFragment;->searchResultListener:Lcom/helpshift/support/contracts/SearchResultListener;

    invoke-interface {p1}, Lcom/helpshift/support/contracts/SearchResultListener;->sendAnyway()V

    return-void
.end method
