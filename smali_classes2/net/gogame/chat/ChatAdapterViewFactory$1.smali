.class Lnet/gogame/chat/ChatAdapterViewFactory$1;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"

# interfaces
.implements Lcom/squareup/picasso/Callback;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatAdapterViewFactory;->getAgentAttachmentView(Landroid/view/View;Landroid/view/ViewGroup;ZLjava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$attachmentFromAgentImageView:Landroid/widget/ImageView;

.field final synthetic val$attachmentUri:Ljava/lang/String;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapterViewFactory;Ljava/lang/String;Landroid/widget/ImageView;)V
    .locals 0

    .line 147
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->val$attachmentUri:Ljava/lang/String;

    iput-object p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->val$attachmentFromAgentImageView:Landroid/widget/ImageView;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onError()V
    .locals 0

    return-void
.end method

.method public onSuccess()V
    .locals 2

    .line 151
    iget-object v0, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-static {v0}, Lnet/gogame/chat/ChatAdapterViewFactory;->access$000(Lnet/gogame/chat/ChatAdapterViewFactory;)Landroid/content/Context;

    move-result-object v0

    invoke-static {v0}, Lcom/squareup/picasso/Picasso;->with(Landroid/content/Context;)Lcom/squareup/picasso/Picasso;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->val$attachmentUri:Ljava/lang/String;

    .line 152
    invoke-virtual {v0, v1}, Lcom/squareup/picasso/Picasso;->load(Ljava/lang/String;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->val$attachmentFromAgentImageView:Landroid/widget/ImageView;

    .line 153
    invoke-virtual {v1}, Landroid/widget/ImageView;->getDrawable()Landroid/graphics/drawable/Drawable;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->placeholder(Landroid/graphics/drawable/Drawable;)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    const/16 v1, 0x190

    .line 154
    invoke-virtual {v0, v1, v1}, Lcom/squareup/picasso/RequestCreator;->resize(II)Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    .line 155
    invoke-virtual {v0}, Lcom/squareup/picasso/RequestCreator;->centerCrop()Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    .line 156
    invoke-virtual {v0}, Lcom/squareup/picasso/RequestCreator;->onlyScaleDown()Lcom/squareup/picasso/RequestCreator;

    move-result-object v0

    iget-object v1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$1;->val$attachmentFromAgentImageView:Landroid/widget/ImageView;

    .line 157
    invoke-virtual {v0, v1}, Lcom/squareup/picasso/RequestCreator;->into(Landroid/widget/ImageView;)V

    return-void
.end method
