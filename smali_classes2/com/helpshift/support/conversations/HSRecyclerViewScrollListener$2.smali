.class Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$2;
.super Ljava/lang/Object;
.source "HSRecyclerViewScrollListener.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;->computeAndNotifyCallback(Landroidx/recyclerview/widget/RecyclerView;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;)V
    .locals 0

    .line 142
    iput-object p1, p0, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$2;->this$0:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 1

    .line 145
    iget-object v0, p0, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$2;->this$0:Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;

    invoke-static {v0}, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;->access$000(Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener;)Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$RecyclerViewScrollCallback;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/support/conversations/HSRecyclerViewScrollListener$RecyclerViewScrollCallback;->onScrolledToBottom()V

    return-void
.end method
