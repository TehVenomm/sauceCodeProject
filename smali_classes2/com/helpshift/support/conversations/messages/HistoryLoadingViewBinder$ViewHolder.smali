.class public Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "HistoryLoadingViewBinder.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "ViewHolder"
.end annotation


# instance fields
.field private final errorStateView:Landroid/view/View;

.field private final layoutView:Landroid/view/View;

.field private final loadingErrorTapToRetry:Landroid/view/View;

.field private final loadingStateView:Landroid/view/View;

.field private final progress:Landroid/widget/ProgressBar;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;


# direct methods
.method public constructor <init>(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;Landroid/view/View;)V
    .locals 1

    .line 67
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    .line 68
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 69
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->itemView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->history_loading_layout_view:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->layoutView:Landroid/view/View;

    .line 70
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->itemView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->loading_state_view:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->loadingStateView:Landroid/view/View;

    .line 71
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->itemView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->loading_error_state_view:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->errorStateView:Landroid/view/View;

    .line 72
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->itemView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->loading_error_tap_to_retry:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->loadingErrorTapToRetry:Landroid/view/View;

    .line 73
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->loadingErrorTapToRetry:Landroid/view/View;

    invoke-virtual {p2, p0}, Landroid/view/View;->setOnClickListener(Landroid/view/View$OnClickListener;)V

    .line 74
    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->itemView:Landroid/view/View;

    sget v0, Lcom/helpshift/R$id;->loading_progressbar:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ProgressBar;

    iput-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->progress:Landroid/widget/ProgressBar;

    .line 75
    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->access$300(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;)Landroid/content/Context;

    move-result-object p1

    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->progress:Landroid/widget/ProgressBar;

    invoke-virtual {p2}, Landroid/widget/ProgressBar;->getIndeterminateDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method

.method static synthetic access$000(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;
    .locals 0

    .line 59
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->layoutView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$100(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;
    .locals 0

    .line 59
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->loadingStateView:Landroid/view/View;

    return-object p0
.end method

.method static synthetic access$200(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;)Landroid/view/View;
    .locals 0

    .line 59
    iget-object p0, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->errorStateView:Landroid/view/View;

    return-object p0
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 0

    .line 80
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->access$400(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;)Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;

    move-result-object p1

    if-eqz p1, :cond_0

    .line 81
    iget-object p1, p0, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;

    invoke-static {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;->access$400(Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder;)Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;

    move-result-object p1

    invoke-interface {p1}, Lcom/helpshift/support/conversations/messages/HistoryLoadingViewBinder$HistoryLoadingClickListener;->onHistoryLoadingRetryClicked()V

    :cond_0
    return-void
.end method
