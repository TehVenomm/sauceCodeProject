.class public final Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;
.super Landroidx/recyclerview/widget/RecyclerView$ViewHolder;
.source "AdminImageAttachmentMessageDataBinder.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x14
    name = "ViewHolder"
.end annotation


# instance fields
.field final downloadButtonView:Landroid/view/View;

.field final downloadProgressbarContainer:Landroid/view/View;

.field final fileSize:Landroid/widget/TextView;

.field final messageLayout:Landroid/view/View;

.field final progressBarView:Landroid/widget/ProgressBar;

.field final roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

.field final subText:Landroid/widget/TextView;

.field final synthetic this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;


# direct methods
.method constructor <init>(Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;Landroid/view/View;)V
    .locals 2

    .line 139
    iput-object p1, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->this$0:Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;

    .line 140
    invoke-direct {p0, p2}, Landroidx/recyclerview/widget/RecyclerView$ViewHolder;-><init>(Landroid/view/View;)V

    .line 141
    sget v0, Lcom/helpshift/R$id;->admin_image_message_layout:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->messageLayout:Landroid/view/View;

    .line 142
    sget v0, Lcom/helpshift/R$id;->admin_attachment_imageview:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Lcom/helpshift/support/views/HSRoundedImageView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->roundedImageView:Lcom/helpshift/support/views/HSRoundedImageView;

    .line 143
    sget v0, Lcom/helpshift/R$id;->download_button:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadButtonView:Landroid/view/View;

    .line 144
    sget v0, Lcom/helpshift/R$id;->download_progressbar_container:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->downloadProgressbarContainer:Landroid/view/View;

    .line 145
    sget v0, Lcom/helpshift/R$id;->download_attachment_progressbar:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/ProgressBar;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->progressBarView:Landroid/widget/ProgressBar;

    .line 146
    sget v0, Lcom/helpshift/R$id;->attachment_file_size:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->fileSize:Landroid/widget/TextView;

    .line 147
    sget v0, Lcom/helpshift/R$id;->date:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v0

    check-cast v0, Landroid/widget/TextView;

    iput-object v0, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->subText:Landroid/widget/TextView;

    .line 149
    sget v0, Lcom/helpshift/R$id;->hs_download_foreground_view:I

    invoke-virtual {p2, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p2

    check-cast p2, Landroid/widget/ImageView;

    .line 150
    iget-object v0, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    .line 151
    invoke-virtual {p2}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p2

    sget v1, Lcom/helpshift/R$attr;->hs__chatBubbleMediaBackgroundColor:I

    .line 150
    invoke-static {v0, p2, v1}, Lcom/helpshift/util/Styles;->setColorFilter(Landroid/content/Context;Landroid/graphics/drawable/Drawable;I)V

    .line 153
    iget-object p1, p1, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder;->context:Landroid/content/Context;

    iget-object p2, p0, Lcom/helpshift/support/conversations/messages/AdminImageAttachmentMessageDataBinder$ViewHolder;->progressBarView:Landroid/widget/ProgressBar;

    invoke-virtual {p2}, Landroid/widget/ProgressBar;->getIndeterminateDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/support/util/Styles;->setAccentColor(Landroid/content/Context;Landroid/graphics/drawable/Drawable;)V

    return-void
.end method
