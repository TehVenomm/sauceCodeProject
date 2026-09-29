.class Lnet/gogame/chat/ChatAdapterViewFactory$6;
.super Ljava/lang/Object;
.source "ChatAdapterViewFactory.java"

# interfaces
.implements Landroid/view/View$OnClickListener;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lnet/gogame/chat/ChatAdapterViewFactory;->getRatingView(Landroid/view/View;Landroid/view/ViewGroup;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)Landroid/view/View;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

.field final synthetic val$rating:Lnet/gogame/chat/ChatContext$Rating;

.field final synthetic val$ratingListener:Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;


# direct methods
.method constructor <init>(Lnet/gogame/chat/ChatAdapterViewFactory;Lnet/gogame/chat/ChatContext$Rating;Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;)V
    .locals 0

    .line 336
    iput-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    iput-object p2, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$rating:Lnet/gogame/chat/ChatContext$Rating;

    iput-object p3, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$ratingListener:Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public onClick(Landroid/view/View;)V
    .locals 1

    .line 340
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-static {p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->access$200(Lnet/gogame/chat/ChatAdapterViewFactory;)Z

    move-result p1

    if-nez p1, :cond_1

    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$rating:Lnet/gogame/chat/ChatContext$Rating;

    sget-object v0, Lnet/gogame/chat/ChatContext$Rating;->GOOD:Lnet/gogame/chat/ChatContext$Rating;

    if-eq p1, v0, :cond_0

    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$rating:Lnet/gogame/chat/ChatContext$Rating;

    sget-object v0, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    if-ne p1, v0, :cond_1

    .line 343
    :cond_0
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->this$0:Lnet/gogame/chat/ChatAdapterViewFactory;

    invoke-static {p1}, Lnet/gogame/chat/ChatAdapterViewFactory;->access$300(Lnet/gogame/chat/ChatAdapterViewFactory;)V

    return-void

    .line 346
    :cond_1
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$rating:Lnet/gogame/chat/ChatContext$Rating;

    sget-object v0, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    if-ne p1, v0, :cond_2

    .line 347
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$ratingListener:Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;

    sget-object v0, Lnet/gogame/chat/ChatContext$Rating;->UNRATED:Lnet/gogame/chat/ChatContext$Rating;

    invoke-interface {p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;->onRatingChanged(Lnet/gogame/chat/ChatContext$Rating;)V

    goto :goto_0

    .line 349
    :cond_2
    iget-object p1, p0, Lnet/gogame/chat/ChatAdapterViewFactory$6;->val$ratingListener:Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;

    sget-object v0, Lnet/gogame/chat/ChatContext$Rating;->BAD:Lnet/gogame/chat/ChatContext$Rating;

    invoke-interface {p1, v0}, Lnet/gogame/chat/ChatAdapterViewFactory$RatingListener;->onRatingChanged(Lnet/gogame/chat/ChatContext$Rating;)V

    :goto_0
    return-void
.end method
