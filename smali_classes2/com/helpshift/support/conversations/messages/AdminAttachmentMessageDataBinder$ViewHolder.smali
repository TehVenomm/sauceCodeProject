.class public final Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "AdminAttachmentMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final attachmentIcon:Landroid/widget/ImageView;

.field final downloadButton:Landroid/view/View;

.field final fileName:Landroid/widget/TextView;

.field final fileSize:Landroid/widget/TextView;

.field final messageContainer:Landroid/view/View;

.field final messageLayout:Landroid/view/View;

.field final progress:Landroid/widget/ProgressBar;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;Landroid/view/View;)V
    .locals 2

    .line 95
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;

    .line 96
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 97
    sget v0, Lcom/helpshift/R$id;->admin_attachment_message_layout:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    .line 98
    sget v0, Lcom/helpshift/R$id;->attachment_file_name:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->fileName:Landroid/widget/TextView;

    .line 99
    sget v0, Lcom/helpshift/R$id;->attachment_file_size:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->fileSize:Landroid/widget/TextView;

    .line 100
    sget v0, Lcom/helpshift/R$id;->admin_message:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    .line 101
    sget v0, Lcom/helpshift/R$id;->download_button:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->downloadButton:Landroid/view/View;

    .line 102
    sget v0, Lcom/helpshift/R$id;->progress:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ProgressBar;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->progress:Landroid/widget/ProgressBar;

    .line 103
    sget v0, Lcom/helpshift/R$id;->attachment_icon:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ImageView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->attachmentIcon:Landroid/widget/ImageView;

    .line 104
    sget v0, Lcom/helpshift/R$id;->attachment_date:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 106
    sget v0, Lcom/helpshift/R$id;->hs_download_foreground_view:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    .line 107
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->context:Landroid/content/Context;

    .line 108
    invoke-virtual {p2}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p2

    sget v1, Lcom/helpshift/R$attr;->hs__chatBubbleMediaBackgroundColor:I

    .line 107
    invoke-static {v0, p2, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 110
    iget-object p2, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->messageContainer:Landroid/view/View;

    .line 111
    invoke-virtual {v0}, Landroid/view/View;->getBackground()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    sget v1, Lcom/helpshift/R$attr;->hs__chatBubbleMediaBackgroundColor:I

    .line 110
    invoke-static {p2, v0, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 113
    iget-object p2, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->context:Landroid/content/Context;

    iget-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->progress:Landroid/widget/ProgressBar;

    invoke-virtual {v0}, Landroid/widget/ProgressBar;->getIndeterminateDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v0

    invoke-static {p2, v0}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    .line 114
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder;->context:Landroid/content/Context;

    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminAttachmentMessageDataBinder$ViewHolder;->attachmentIcon:Landroid/widget/ImageView;

    invoke-virtual {p2}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method
